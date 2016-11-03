/*jslint browser: true*/
/*global $, jQuery, alert*/
/* =================================
===  CONTACT FORM          ====
=================================== */
$("#contact-form").submit(function (e) {
	e.preventDefault();
	var name = $("#name").val();
	var email = $("#email").val();
	var budget = $("#budget").val();
	var subject = $("#subject").val();
	var message = $("#message").val();
	var dataString = 'name=' + name + '&email=' + email + '&subject=' + subject + '&budget=' + budget + '&message=' + message;

	function isValidEmail(emailAddress) {
		var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
		return pattern.test(emailAddress);
	};

	if (isValidEmail(email) && (message.length > 10) && (name.length > 1)) {

		$.ajax({
			type: "POST",
			url: "sendmail.php",
			data: dataString,
			success: function () {
				$('.success').fadeIn(1000);
				$('.error').fadeOut(500);
			}
		});

	} else {
		if (name.length < 2) {
			$('.error').html('Invalid Name - Please use your correct name').fadeIn(1000);
			$('.success').fadeOut(500);
		}
		if (message.length < 11) {
			$('.error').html('Message is too short. Should be more than 10 character').fadeIn(1000);
			$('.success').fadeOut(500);
		}

		if ((name.length < 2) && (message.length < 11)) {
			$('.error').html('Valid name & More than 10 characters in message is required').fadeIn(1000);
			$('.success').fadeOut(500);
		}
	}

	return false;
});

/* ================================
===  OTHER FIXES 		       ====
================================= */
$('input,textarea').focus(function () {
	$(this).data('placeholder', $(this).attr('placeholder'))
		.attr('placeholder', '');
}).blur(function () {
	$(this).attr('placeholder', $(this).data('placeholder'));
});

/* BOOTSTRAP FIX */
if (navigator.userAgent.match(/IEMobile\/10\.0/)) {
	var msViewportStyle = document.createElement('style')
	msViewportStyle.appendChild(
		document.createTextNode(
			'@-ms-viewport{width:auto!important}'
		)
	)
	document.querySelector('head').appendChild(msViewportStyle)
}
