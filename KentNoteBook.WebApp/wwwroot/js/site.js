
$(function () {

	// launch the modal dialog
	$('#modal_dialog_layout').on('show.bs.modal', function (event) {

		var $modal = $(this);
		var $modalTriggger = $(event.relatedTarget) // Button that triggered the modal

		var title = $modalTriggger.data("modalTitle");
		var url = $modalTriggger.data("modalUrl");
		var size = $modalTriggger.data("modalSize");

		$modal.find(".modal-title").html(title);

		if (size) {
			$modal.find(".modal-dialog").addClass("modal-" + size);
		}

		// load dialog content
		$.ajax({
			method: 'GET',
			url: url,
			cache: false,
			beforeSend: function (xhr) {
				var accessToken = localStorage.getItem("access_token");
				xhr.setRequestHeader("Authorization", "Bearer " + accessToken);
			},
		}).done(function (data, textStatus, jqXHR) {
			$modal.find(".modal-body").html(data);

			// Execute the js script in the page
			$modal.find("script").each(function () {
				eval($(this).text());
			});

			// Render some plugins manualy
			$.bindDatePicker($modal);
			$.bindAjaxForm($modal.find("form[ajax-form='true']"));


		}).fail(function (jqXHR, textStatus, errorThrown) {
			$modal.find(".modal-body").html(errorThrown);
		});

	});

	// register the events to confirm dialog
	$('#modal_confirm_layout').on('show.bs.modal', function (event) {

		// TODO :

		//var $modal = $(this);
		//var $modalTriggger = $(event.relatedTarget) // Button that triggered the modal

		//var title = $modalTriggger.data("modalTitle");
		//var url = $modalTriggger.data("modalUrl");
		//var size = $modalTriggger.data("modalSize");

		//$modal.find(".modal-title").html(title);

		//if (size) {
		//	$modal.find(".modal-dialog").addClass("modal-" + size);
		//}

		//// load dialog content
		//$.ajax({
		//	method: 'GET',
		//	url: url,
		//	cache: false,
		//	beforeSend: function (xhr) {
		//		var accessToken = localStorage.getItem("access_token");
		//		xhr.setRequestHeader("Authorization", "Bearer " + accessToken);
		//	},
		//}).done(function (data, textStatus, jqXHR) {
		//	$modal.find(".modal-body").html(data);

		//	// Execute the js script in the page
		//	$modal.find("script").each(function () {
		//		eval($(this).text());
		//	});

		//	// Render some plugins manualy
		//	$.bindDatePicker($modal);
		//	$.bindAjaxForm($modal.find("form[ajax-form='true']"));


		//}).fail(function (jqXHR, textStatus, errorThrown) {
		//	$modal.find(".modal-body").html(errorThrown);
		//});

	});

	$.bindDatePicker($(document));
	$.bindAjaxForm($("form[ajax-form='true']"));
});

$.extend({

	bindDatePicker: function ($wrapper) {
		$wrapper.find(".date-picker").daterangepicker({
			autoApply: true,
			autoUpdateInput: true,
			singleDatePicker: true,
			showDropdowns: true,
			timePicker: true,
			timePicker24Hour: true,
			timePickerSeconds: true,
			timePickerIncrement: 1,
			locale: {
				cancelLabel: 'Clear',
				format: 'YYYY/MM/DD H:mm:ss'
			}
		});

		$wrapper.find(".date-picker").on('apply.daterangepicker', function (ev, picker) {
			//debugger;
			$(this).val(picker.startDate.format('YYYY/MM/DD H:mm:ss'));
		});
	},

	bindAjaxForm: function ($form) {
		$form.on("click", "button[type='submit'][data-ajax-request]", function () {
			var $submit = $(this);

			var $alertPanel = $($form.data("alertPanel"));
			var $updatePanel = $($form.data("updatePanel"));

			var callback = $form.data("ajaxCallback");

			$submit.attr("disabled", true);

			$.ajax({
				method: 'POST',
				url: $form.attr("action"),
				data: $form.serialize(),
				cache: false,
			}).done(function (data, textStatus, jqXHR) {
				if (data && data.Code) {
					if ($alertPanel && $alertPanel.length) {
						$alertPanel.html("<div class='alert alert-success' role='alert'>Successful.</div>");
					}

					if ($updatePanel && $updatePanel.length) {
						$updatePanel.each(function () {
							$(this).data("kendoGrid") && $(this).data("kendoGrid").dataSource.read();
						});
					}

				} else {
					if ($alertPanel && $alertPanel.length) {
						$alertPanel.html("<div class='alert alert-danger' role='alert'>" + data.Data + "</div>");
					}
				}

				typeof (callback) === "function" && callback(data);

				$submit.removeAttr("disabled");

			}).fail(function (jqXHR, textStatus, errorThrown) {
				if ($alertPanel && $alertPanel.length) {
					$alertPanel.html("<div class='alert alert-danger' role='alert'>Failure</div>");
				}
			});

			return false;
		});
	},

	bindAjaxPanel: function ($wrapper) {
		$wrapper.find("[ajax-panel]").each(function () {
			var $container = $(this);
			var url = $(this).data("url");

			$.ajax({
				method: 'GET',
				url: url,
				cache: false,
				beforeSend: function (xhr) {
					var accessToken = localStorage.getItem("access_token");
					xhr.setRequestHeader("Authorization", "Bearer " + accessToken);
				},
			}).done(function (data, textStatus, jqXHR) {
				$container.html(data);

				// Execute the js script in the page
				$container.find("script").each(function () {
					eval($(this).text());
				});

				// Render some plugins manualy
				$.bindAjaxPanel($container);

			}).fail(function (jqXHR, textStatus, errorThrown) {
				$container.html(errorThrown);
			});
		});
	},

	bindGridRowsSelect: function () {

	},
});

