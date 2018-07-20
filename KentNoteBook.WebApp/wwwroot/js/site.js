
$(function () {

	$("#btnToggleSidebar").click(function () {
		$("body").toggleClass("push-right").removeClass("sidebar-collapsed").removeClass("main-container-expanded");
	});
	$("section.main-container").click(function () {
		$("body").removeClass("push-right").removeClass("sidebar-collapsed").removeClass("main-container-expanded");
	});

	$("#btnCollopseLeftNavMenu").click(function () {
		$(this).find(".fa").toggleClass("fa-angle-left").toggleClass("fa-angle-right");

		if ($(this).find(".fa").hasClass("fa-angle-right")) {
			$(".sidebar").addClass("sidebar-collapsed");
			$(".main-container").addClass("main-container-expanded");
		} else {
			$(".sidebar").removeClass("sidebar-collapsed");
			$(".main-container").removeClass("main-container-expanded");
		}
	});

	$(document).scroll(function () {
		$(this).scrollTop() > 100 ? $(".scroll-to-top").fadeIn() : $(".scroll-to-top").fadeOut()
	});
	$('[data-toggle="tooltip"]').tooltip();

	$(document).on("click", "a.scroll-to-top", function (o) {
		var t = $(this);
		$("html, body").stop().animate({ scrollTop: $(t.attr("href")).offset().top - 70 }), o.preventDefault()
	});

	// launch the modal dialog
	$('#modal_dialog_layout').on('show.bs.modal', function (event) {

		var $modal = $(this);
		var $modalCaller = $(event.relatedTarget) // Button that triggered the modal

		var title = $modalCaller.data("modalTitle");
		var url = $modalCaller.data("modalUrl");
		var size = $modalCaller.data("modalSize");

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
			$.validator.unobtrusive.parse($modal.find("form"));
			$.bindDatePicker($modal);
			$.bindAjaxForm($modal.find("form[ajax-form='true']"));


		}).fail(function (jqXHR, textStatus, errorThrown) {
			$modal.find(".modal-body").html(errorThrown);
		});

	});

	// register the events to confirm dialog
	$('#modal_confirm_layout').on('show.bs.modal', function (event) {

		var $modal = $(this);
		var $modalCaller = $(event.relatedTarget) // Button that triggered the modal
		var $form = $modalCaller.parents("form");
		var $alertPanel = $($modalCaller.data("alertPanel"));
		var $updatePanel = $($modalCaller.data("updatePanel"));

		var title = $modalCaller.data("modalTitle");
		var url = $modalCaller.data("url");
		var callback = $modalCaller.data("ajaxCallback");

		if (title) {
			$modal.find(".modal-title").html(title);
		}

		$modal.find(".btn-danger").off("click");
		$modal.find(".btn-danger").on("click", function () {
			var $submit = $(this);

			$submit.attr("disabled", true);

			// submit the POST request
			$.ajax({
				method: 'POST',
				url: url,
				data: $form.serialize(),
				cache: false,
				beforeSend: function (xhr) {
					var accessToken = localStorage.getItem("access_token");
					xhr.setRequestHeader("Authorization", "Bearer " + accessToken);
				},
			}).done(function (data, textStatus, jqXHR) {
				if (data && data.Code) {

					$alertPanel.success();

					$.bindAjaxPanel($updatePanel);

				} else {
					$alertPanel.fail(data.Data);
				}

				typeof (callback) === "function" && callback(data);

				$submit.removeAttr("disabled");

				$modal.modal("hide");

			}).fail(function (jqXHR, textStatus, errorThrown) {
				$alertPanel.fail();
			});
		});
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
		$form.on("click", "button[type='submit'][data-ajax-request]", function (e) {
			var validationInfo = $form.data("unobtrusiveValidation");
			if (validationInfo && validationInfo.validate && !validationInfo.validate()) {

				var validator = $form.data("validator");
				validator && validator.focusInvalid();

				return false;
			}
			$form.find(".validation-summary-errors").addClass("validation-summary-valid").removeClass("validation-summary-errors");

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

		$wrapper.each(function () {
			$(this).data("kendoGrid") && $(this).data("kendoGrid").dataSource.read();
		});

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
	}
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
	}
});
