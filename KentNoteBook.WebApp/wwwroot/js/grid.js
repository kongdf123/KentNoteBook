/*
	Kendo UI grid extend method
*/
$.fn.extend({
	dataGridBind: function (dataSourceUrl, criteria, columns) {
		var container = this;

		var options = {
			columns: columns || [],
			autoBind: true,
			sortable: true,
			reorderable: true,
			scrollable: true,
			resizable: true,
			columnMenu: true,

			filterable: {
				mode: "menu",
				extra: false,
				operators: {
					string: {
						contains: "Contains",
						eq: "Equal to"
					},
					number: {
						eq: "Equal to",
						gt: ">",
						lt: "<",
						gte: ">=",
						lte: "<="
					},
					date: {
						gt: ">",
						lt: "<",
						gte: ">=",
						lte: "<="
					},
					enums: {
						eq: "Equal to",
						neq: "Not equal to"
					}
				},
				messages: {
					info: "Filter:",
					selectValue: "-"
				}
			},

			pageable: (criteria.PaginationEnabled === false) ? false : {
				refresh: true,
				pageSizes: [10, 20, 50, 100, 200],
				buttonCount: 8
			},

			dataBound: function () {
				//workaround: header and body width alignment
				var grid = $(container).data("kendoGrid");
				for (var i = columns.length - 1; i >= 0; i--) {
					if (!columns[i].hidden) {
						grid.autoFitColumn(i);
						break;
					}
				}

				$(container).find("tbody tr[role=row]").each(function () {
					var $tr = $(this);
					var rowData = grid.dataItem($tr);
					if (rowData.IsHighlighted) {
						$tr.addClass("text-warning");
					}
					else if (rowData.IsMuted) {
						$tr.addClass("text-muted-lighter");
					}

					$tr.on("dblclick", function () {
						$tr.find(".k-grid-edit").click();
					});
				});

				//$.traversal($(container).find("tbody"));
			},

			dataSource: new kendo.data.DataSource({
				batch: true,
				serverPaging: true,
				serverFiltering: true,
				serverSorting: true,
				pageSize: criteria.Limit,
				schema: {
					model: (function () {
						var dataTypes = {
							"0": "object",
							"1": "string",
							"2": "date",
							"3": "number",
							"4": "boolean",
						};

						var m = {};
						m.fields = {};
						for (var i = 0; i < columns.length; i++) {
							var col = columns[i];
							if (col.DataType) {
								m.fields[col.field] = { type: dataTypes[col.type + ""] };
							}
							if (col.columns) {
								for (var j = 0; j < col.columns.length; j++) {
									var innerCol = col.columns[j];
									if (innerCol.type) {
										m.fields[innerCol.field] = { type: innerCol.type };
									}
								}
							}
						}
						return m;
					})(),
					data: function (d) {
						return d.Data || d.data;
					},
					total: function (d) {
						return d.TotalCount || d.totalCount;
					}
				},
				sort: [
					{ field: criteria.SortBy, dir: (criteria.SortDirection === "Descending" || criteria.SortDirection === 1 ? "desc" : "asc") }
				],
				filter: (function () {
					var given = [];
					var operators = {
						"0": "eq",
						"1": "neq",
						"2": "contains",
						"3": "gt",
						"4": "gte",
						"5": "lt",
						"6": "lte"
					};
					criteria.PostFilters.forEach(function (item, index) {
						given.push({
							field: item.Field,
							value: item.Value,
							operator: operators[item.Operator]
						});
					});
					return given;
				})(),
				transport: {
					read: function (o) {
						$.ajax({
							url: dataSourceUrl,
							type: "POST",
							dataType: "json",

							beforeSend: function (xhr) {
								// send the JWT to server
								xhr.setRequestHeader("Authorization", "Bearer " + localStorage.getItem("access_token"));
							},

							success: function (d) {
								o.success(d);
							},
							error: function (err) {
								var $gridBody = $(this);	// .find(".k-grid-content")
								$gridBody.empty();
								$gridBody.append(err.responseText);
							},

							// data is the criteria to post back;
							// the function is to merge updated parameters (from UI changes) back into criteria
							data: (function () {
								var dataSource = o.data;
								// pagination
								criteria.Limit = dataSource.take || criteria.Limit;
								criteria.Offset = dataSource.skip || 0;
								// filtering
								criteria.PostFilters.splice(0);
								if (dataSource.filter && dataSource.filter.filters) {
									dataSource.filter.filters.forEach(function (item, index) {
										criteria.PostFilters.push(item);
									});
								}
								// ordering
								if (dataSource.sort && dataSource.sort.length > 0) {
									criteria.SortBy = dataSource.sort[0].field;
									criteria.SortDirection = dataSource.sort[0].dir + "ending";
								}

								// support the paging function in razor page
								if ($(container).parents("form").length) {
									criteria.__RequestVerificationToken = $(container).parents("form").find('input:hidden[name="__RequestVerificationToken"]').val();
								}
								return criteria;
							})()
						});
					}
				}
			}),
		};
		$(container).kendoGrid(options);
	}
});