$(document).ready(function () {

    $(".btn-submit").click(function () {
        if (!fnValidation()) return;

        // 1️⃣ Get values
        var phone = $('#PhoneNumber').val();
        var message = $('#Message').val();
        $('#CommunicationMode').val('Whatsapp'); // ￼ FORCE DEFAULT

        //if (!phone || !message) {
        //    alert('Phone number or message missing');
        //    return;
        //}

        // 2️⃣ Clean phone number (+91, space remove)
        phone = phone.replace(/\D/g, '');

        // 3️⃣ Submit form (DB ma message save thase)
        $('#frmWhatsAppMessage').submit();

        // 4️⃣ Open WhatsApp
        var url = "https://wa.me/" + phone + "?text=" + encodeURIComponent(message);
        window.open(url, '_blank');
    });

    var notificationType = $.trim($('#NotificationType').val());
    var notificationMsg = $.trim($('#NotificationMsg').val());

    if (notificationType != null && notificationType != undefined && notificationType != '') {
        if (notificationMsg != null && notificationMsg != undefined && notificationMsg != '') {

            showToastr(notificationType, notificationMsg)
            $('.modal').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('#frmList').submit();
        }
    }
});

function fnValidation() {
    var isValid = true;
    var message = $.trim($("#Message").val());

    //Status
    if (message == null || message == "" || message == undefined) {
        $("#Message").next("span").text("Message is required!");
        isValid = false;
    } else {
        $("#Message").next("span").text("");
    }

    return isValid;
}

