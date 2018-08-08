using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KentNoteBook.Infrastructure.Html.TreeView
{
	public static class TreeViewHtmlHelperExtension
	{
		public static IHtmlContent TreeView(this IHtmlHelper htmlHelper, List<TreeViewNode> nodes, bool showCheckbox = true, string actionsHtml = "") {
			if ( nodes == null ) {
				throw new ArgumentNullException(nameof(nodes), "Please provide the treeview data.");
			}

			var htmlBuilder = new HtmlContentBuilder();
			var rootMenus = nodes.Where(x => x.ParentId == null).ToArray();

			htmlBuilder.AppendHtmlLine("<ul class='list-group list-group-flush tree-view-custom'>");

			foreach ( var node in rootMenus ) {
				BuildMenuTree(node, nodes, htmlBuilder, showCheckbox, actionsHtml);
			}

			htmlBuilder.AppendHtmlLine("</ul>");
			htmlBuilder.AppendHtmlLine(Scripts);
			return htmlBuilder;
		}

		static void BuildMenuTree(TreeViewNode node, List<TreeViewNode> sources, HtmlContentBuilder htmlBuilder, bool showCheckbox = true, string actionsHtml = "") {
			var children = sources.Where(x => x.ParentId != null && node.Id != null && x.ParentId.ToString() == node.Id.ToString());

			htmlBuilder.AppendHtmlLine($"<li class='list-group-item {(children.Any() ? "" : "pl-4")} border-top'>");

			if ( children.Any() ) {
				htmlBuilder.AppendHtmlLine("<a class='collapse-icon' href='#'><i class='fa fa-fw fa-plus'></i></a>");
			}

			if ( showCheckbox ) {
				htmlBuilder.AppendHtmlLine("<div class='custom-control custom-checkbox d-inline'>");
				htmlBuilder.AppendHtmlLine($"<input type='checkbox' class='custom-control-input' id='node_{node.Id}' name='idArray' value='{node.Id}'>");
				htmlBuilder.AppendHtmlLine($"<label class='custom-control-label' for='node_{node.Id}'> {node.Name}</label>");
				htmlBuilder.AppendHtmlLine("</div>");
			}

			// actions
			//if ( editUrl != "" ) {
			//	htmlBuilder.AppendHtmlLine($"<a href='#' class='ml-1' data-toggle='modal' data-target='#modal_dialog_layout' data-modal-title='Edit' data-modal-url='{editUrl}/{node.Id}' data-modal-size='lg'><i class='fa fa-fw fa-edit'></i></a>");
			//}
			//if ( removeUrl != "" ) {
			//	htmlBuilder.AppendHtmlLine($"<a href='#' class='text-danger' data-toggle='modal' data-target='#modal_confirm_layout' data-url='{removeUrl}?handler=Remove&idArray={node.Id}' data-alert-panel='#alert_panel' data-update-panel='#menuContainer'><i class='fa fa-fw fa-remove'></i></a>");
			//}
			htmlBuilder.AppendHtmlLine(actionsHtml.Replace("#=Id #", node.Id == null ? "" : node.Id.ToString()));

			if ( children.Any() ) {
				htmlBuilder.AppendHtmlLine($"<ul class='list-group list-group-flush tree-view-custom d-none mt-1 pt-2' id='collapse_{node.Id}'>");

				foreach ( var child in children ) {
					BuildMenuTree(child, sources, htmlBuilder, showCheckbox, actionsHtml);
				}

				htmlBuilder.AppendHtmlLine("</ul>");
			}

			htmlBuilder.AppendHtmlLine("</li>");
		}

		#region js snippts

		static string Scripts = @"<script type='text/javascript'>
				$('.tree-view-custom .collapse-icon').click(function () {

					$(this).siblings('.list-group').toggleClass('d-none').toggleClass('d-block');
					$(this).find('i.fa').toggleClass('fa-plus').toggleClass('fa-minus');

					if ($(this).siblings('.list-group').length && $(this).find('i.fa').hasClass('fa-plus')) {
						$(this).parent().removeClass('pb-0');
					} else {
						$(this).parent().addClass('pb-0');
					}
				});

				// check all
				$('.tree-view-custom .custom-checkbox').click(function () {
					var state = $(this).find(':checkbox').prop('checked');

					// set the state of sub-checkboxes
					$(this).siblings('.list-group').find(':checkbox').prop('checked', state);

					// existing less than one sub-checkbox that is unchecked
					if (!state) {
						var $self = $(this);
						var parentCheckBox = $self.parent().parent().siblings('.custom-checkbox');

						while (parentCheckBox.length) {
							parentCheckBox.find(':checkbox').prop('checked', false);

							$self = parentCheckBox;
							parentCheckBox = $self.parent().parent().siblings('.custom-checkbox');
						}
					}

					// all sub-checkeboxes are checked
					var isAllChecked = true;
					$(this).parent().parent().find(':checkbox').each(function () {
						if (!$(this).prop('checked')) {
							isAllChecked = false;
							return;
						}
					});
					if (isAllChecked) {

						var $self = $(this);
						var parentCheckBox = $self.parent().parent().siblings('.custom-checkbox');

						while (parentCheckBox.length) {
							parentCheckBox.find(':checkbox').prop('checked', true);

							$self = parentCheckBox;
							parentCheckBox = $self.parent().parent().siblings('.custom-checkbox');
						}
					}
				});
			</script>";

		#endregion
	}
}
