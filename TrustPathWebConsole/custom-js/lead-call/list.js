$(function () {

    $(document).off('click', '.add-lead-call').on('click', '.add-lead-call', function (e) {
        e.preventDefault();
        var leadId = $(this).data('lead-id');
        if (leadId) {
            fnCreate(leadId);
        }
    });

    $(document).off('click', '.icon-edit').on('click', '.icon-edit', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        if (id) fnEdit(id);
    });

    $(document).off('click', '.icon-delete').on('click', '.icon-delete', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        if (id) fnDeleteLeadCall(id);
    });
});

function fnCreate(leadId) {
    fnHandleAjaxRequest({
        url: '/LeadCall/Create',
        type: 'GET',
        data: { leadId: leadId },
        onSuccess: function (html) {
            $('#divCreate').html(html);
            $('#modal-lead-call').modal('show');
        },
        onError: function () {
            showToastr("error", "Failed to load Lead Call form");
        }
    });
}

function fnEdit(id) {
    fnHandleAjaxRequest({
        url: '/LeadCall/Get/' + id,
        onSuccess: function (html) {
            $('#divCreate').html(html);
            $('#modal-lead-call').modal('show');
        }
    });
}

function fnDelete(id) {
    fnHandleAjaxRequest({
        url: '/LeadCall/Delete/' + id,
        dataType: 'json',
        onSuccess: function (res) {
            showToastr(res.NotificationType, res.NotificationMsg);
            $('#frmList').submit();
        }
    });
}