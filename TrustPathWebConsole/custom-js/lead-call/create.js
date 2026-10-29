$(document).ready(function () {

    $(".btn-submit").click(function () {
        if (fnValidation()) {
            $('#frmCreate').submit();
        }
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

    var description = $.trim($("#Description").val());
    var communicationMode = $.trim($("#CommunicationMode").val());
    var followUp = $.trim($("#UpComeFollowUp").val());

    // Description
    if (description == null || description == "" || description == undefined) {
        $("#Description").next("span").text("Description is required!");
        isValid = false;
    } else {
        $('#Description').next('span').text("");
    }

    // Communication Mode
    if (communicationMode == null || communicationMode == "" || communicationMode == undefined) {
        $("#CommunicationMode").next("span").text("Communication Mode is required!");
        isValid = false;
    } else {
        $("#CommunicationMode").next("span").text("");
    }

    // Upcoming FollowUp date/time
    if (followUp) {
        var parts = followUp.split("T");
        var dateParts = parts[0].split("-");

        var selectedDay = new Date(
            parseInt(dateParts[0]),
            parseInt(dateParts[1]) - 1,
            parseInt(dateParts[2])
        );

        var now = new Date();
        var today = new Date(now.getFullYear(), now.getMonth(), now.getDate());

        if (selectedDay.getTime() === today.getTime()) {
            $("#UpComeFollowUp").next("span").text("Follow-up cannot be today!");
            isValid = false;
        } else {
            $("#UpComeFollowUp").next("span").text("");
        }
    } else {
        // No date selected → do nothing, no validation message
        $("#UpComeFollowUp").next("span").text("");
    }

    return isValid;
}