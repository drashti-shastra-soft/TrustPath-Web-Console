var emailAddressRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

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

    $('#PhoneNumber').mask('+91 00000 00000', {
        translation: {
            '0': { pattern: /[0-9]/ }
        },
        onKeyPress: function (val, e, field, options) {
            if (!val.startsWith('+91')) {
                field.val('+91 ' + val.replace(/[^\d]/g, '').substring(0, 10).replace(/(\d{5})(\d{0,5})/, '$1 $2'));
            }
        }
    });

    $('#EmailAddress').on('input', function () {
        let val = $(this).val();

        // Allow only valid email characters
        val = val.replace(/[^a-zA-Z0-9@._\-]/g, '');

        // Prevent double @ symbols
        const parts = val.split('@');
        if (parts.length > 2) {
            val = parts[0] + '@' + parts[1];
        }

        $(this).val(val);
    });

    $('#name').on('input', function () {
        this.value = this.value.replace(/[^a-zA-Z\s]/g, '');
    });
});

function fnValidation() {
    var isValid = true;

    var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    var name = $.trim($("#Name").val());
    var emailAddress = $.trim($("#EmailAddress").val());
    var phoneNumber = $.trim($("#PhoneNumber").val());
    var stateId = $.trim($("#StateId").val());
    var businessTypeId = $.trim($("#BusinessTypeId").val());
    var sourceId = $.trim($("#SourceId").val());
    var assignLead = $.trim($("#AssignLead").val());
    var requirement = $.trim($("#Requirement").val());
    var leadStatus = $.trim($("#LeadStatus").val());


    // Name
    if (name == null || name == "" || name == undefined) {
        $("#Name").next("span").text("Name is required!");
        isValid = false;
    } else {
        $('#Name').next('span').text("");
    }

    // Email Address
    if (emailAddress == null || emailAddress == "" || emailAddress == undefined) {
        $("#EmailAddress").next("span").text("Email Address is required!");
        isValid = false;
    } else {
        $('#EmailAddress').next('span').text("");
    }

    // Mobile Number
    if (phoneNumber == null || phoneNumber == "" || phoneNumber == undefined) {
        $("#PhoneNumber").next("span").text("Phone Number is required!");
        isValid = false;
    } else {
        $("#PhoneNumber").next("span").text("");
    }

    // State
    if (stateId == null || stateId == "" || stateId == undefined) {
        $("#StateId").next("span").text("State is required!");
        isValid = false;
    } else {
        $("#StateId").next("span").text("");
    }

    // State
    if (businessTypeId == null || businessTypeId == "" || businessTypeId == undefined) {
        $("#BusinessTypeId").next("span").text("Business Type is required!");
        isValid = false;
    } else {
        $("#BusinessTypeId").next("span").text("");
    }

    // Source
    if (sourceId == null || sourceId == "" || sourceId == undefined || sourceId == "Select Source") {
        $("#SourceId").next("span").text("Source is required!");
        isValid = false;
    } else {
        $("#SourceId").next("span").text("");
    }

    // Assign Lead
    if (assignLead == null || assignLead == "" || assignLead == undefined) {
        $("#AssignLead").next("span").text("Assign Lead is required!");
        isValid = false;
    } else {
        $("#AssignLead").next("span").text("");
    }

    // Lead Status
    if (leadStatus == null || leadStatus == "" || leadStatus == undefined) {
        $("#LeadStatus").next("span").text("Lead Status is required!");
        isValid = false;
    } else {
        $("#LeadStatus").next("span").text("");
    }

    // Requirement
    if (requirement == null || requirement == "" || requirement == undefined) {
        $("#Requirement").next("span").text("Requirement is required!");
        isValid = false;
    } else {
        $("#Requirement").next("span").text("");
    }

    return isValid;
}

