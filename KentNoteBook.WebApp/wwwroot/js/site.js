
$(function () {
	$.ajaxSetup({
		cache: false,
	});

	$(document).on("click", "#btnToggleSidebar", function () {
		$("body").toggleClass("push-right");
	});

	$(document).scroll(function () {
		$(this).scrollTop() > 100 ? $(".scroll-to-top").fadeIn() : $(".scroll-to-top").fadeOut()
	});
	$('[data-toggle="tooltip"]').tooltip();

	$(document).on("click", "a.scroll-to-top", function (e) {

		$("html, body").stop().animate({ scrollTop: $($(this).attr("href")).offset().top - 70 });

		e.preventDefault()
	});

	// launch the modal dialog
	$('#modal_dialog_layout').on('show.bs.modal', function (event) {
		var $modal = $(this);
		var $modalCaller = $(event.relatedTarget) // Button that triggered the modal
		var $modalBody = $modal.find(".modal-body");

		var title = $modalCaller.data("modalTitle");
		var url = $modalCaller.data("modalUrl");
		var size = $modalCaller.data("modalSize");

		$modal.find(".modal-title").html(title);

		if (size) {
			$modal.find(".modal-dialog").addClass("modal-" + size);
		}

		// load dialog content
		$.renderPartial($modalBody, url);
	});

	// register the events to confirm dialog
	$('#modal_confirm_layout').on('show.bs.modal', function (event) {

		var $modal = $(this);
		var $modalCaller = $(event.relatedTarget) // Button that triggered the modal

		var $form = $modalCaller.parents("form");
		var $submit = $modal.find(".btn-danger");

		var title = $modalCaller.data("modalTitle");
		if (title) {
			$modal.find(".modal-title").html(title);
		}

		$submit.data("url", $modalCaller.data("url"));
		$submit.data("alertPanel", $modalCaller.data("alertPanel"));
		$submit.data("updatePanel", $modalCaller.data("updatePanel"));
		$submit.data("ajaxCallback", function (d) {

			var callback = $modalCaller.data("ajaxCallback");
			typeof (callback) === "function" && callback(data);
			$modal.modal("hide");
		});

		$.bindAjaxForm($form, $submit);
	});

	// handle some ajax elements
	$.bindDatePicker($(document));
	$.bindAjaxPanel($(document));

	$("[ajax-form=true]").each(function () {
		var $form = $(this);
		var $submit = $form.find("[ajax-button=true]");

		$.bindAjaxForm($form, $submit);
	});

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
			$(this).val(picker.startDate.format('YYYY/MM/DD H:mm:ss'));
		});
	},

	bindAjaxLink: function ($link) {

		$link.click(function (e) {

			e.preventDefault();

			var url = $(this).data("url");
			var panel = $($(this).data("updatePanel"));
			var callback = $(this).data("ajaxCallback");

			$.renderPartial(panel, url, callback);
		});
	},

	bindAjaxForm: function ($form, $submit) {

		$submit.off("click");
		$submit.on("click", function (e) {
			e.preventDefault();

			var validationInfo = $form.data("unobtrusiveValidation");
			if (validationInfo && validationInfo.validate && !validationInfo.validate()) {

				var validator = $form.data("validator");
				validator && validator.focusInvalid();
				return false;
			}

			// the page is valid
			$form.find(".validation-summary-errors").addClass("validation-summary-valid").removeClass("validation-summary-errors");

			var $alertPanel = $($submit.data("alertPanel"));
			var $updatePanel = $($submit.data("updatePanel"));

			var callback = $submit.data("ajaxCallback");

			$submit.attr("disabled", true);

			$.ajax({
				method: 'POST',
				url: $submit.data("url") || $submit.attr("formaction"),
				data: $form.serialize(),
				beforeSend: $.ajaxBeforeSend,
			}).done(function (data, textStatus, jqXHR) {
				if (data && data.Code) {

					$updatePanel.data("kendoGrid") && $updatePanel.data("kendoGrid").dataSource.read();

					$alertPanel.success();
					$.bindAjaxPanel($updatePanel);
				} else {
					$alertPanel.fail(data.Data);
				}

				typeof (callback) === "function" && callback(data);

				$submit.removeAttr("disabled");

			}).fail(function (jqXHR, textStatus, errorThrown) {
				$alertPanel.fail();
			});
		});
	},

	bindAjaxPanel: function ($wrapper) {
		if ($wrapper === null || $wrapper.length === 0) {
			return;
		}

		$wrapper.find("[ajax-panel]").each(function () {
			var $container = $(this);
			var url = $(this).data("url");

			$.renderPartial($container, url);
		});
	},

	renderPartial: function ($panel, url, callback) {

		$panel.each(function () {
			var $container = $(this);

			$container.showLoading();

			$.ajax({
				method: 'GET',
				url: url,
				beforeSend: $.ajaxBeforeSend,
				statusCode: {
					401: function () {
						alert("page not found");
					}
				}
			}).done(function (data, textStatus, jqXHR) {
				$container.html(data);

				// Execute the js script in the page
				//$container.find("script").each(function () {
				//	eval($(this).text());
				//});

				// Render some plugins manualy
				$.bindDatePicker($container);
				$.bindAjaxLink($container.find('[ajax-link=true]'));

				// handle the validation and submit for the ajax form
				var $form = $container.find("form[ajax-form='true']");
				var $submit = $form.find("[ajax-button=true]");
				$.validator.unobtrusive.parse($form);
				$.bindAjaxForm($form, $submit);

				typeof (callback) === "function" && callback(data);

			}).fail(function (jqXHR, textStatus, errorThrown) {
				debugger;
				$container.html(errorThrown);
			});
		});
	},
	ajaxBeforeSend: function (jqXHR, settings) {
		var accessToken = localStorage.getItem("access_token");
		jqXHR.setRequestHeader("Authorization", "Bearer " + accessToken);
	},
	isTokenValid: function () {
		var isAuthenticated = false;

		$.ajax({
			method: 'POST',
			url: "/Auth/CheckToken",
			async: false,
			beforeSend: $.ajaxBeforeSend,
			statusCode: {
				401: function () {
					alert("page not found");
				}
			},
		}).done(function (data, textStatus, jqXHR) {
			if (data && data.Code) {
				isAuthenticated = true;
			}
		}).fail(function (jqXHR, textStatus, errorThrown) {
			debugger;
		});

		return isAuthenticated;
	},
});

$.fn.extend({
	warn: function (msg) {
		return this.message("warning", msg || "Warning");
	},
	fail: function (msg) {
		return this.message("danger", msg || "Error");
	},
	success: function (msg) {
		return this.message("success", msg || "Successful");
	},
	message: function (type, msg) {
		if (Array.isArray(msg)) {
			msg = msg.join("<br/>");
		}

		return this.each(function () {
			var htmlString = "<div class='alert alert-" + type + " alert-dismissible fade show' role='alert'>";
			htmlString += "<p class='mb-1'>" + msg + "</p>";
			htmlString += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'>";
			htmlString += "<span aria-hidden='true'>&times;</span>";
			htmlString += "</button>";
			htmlString += "</div>";

			$(this).html(htmlString);
		});
	},
	showLoading: function () {
		return this.each(function () {
			$(this).html("<span class='pl-3'><i class='fa fa-fw fa-spinner fa-spin mr-1'></i>Processing...</span>");
		});
	},
});
