function showMessage(title, message, theme) {
    window.createNotification({
        cloneOnClick: true,
        displayCloseButton: false,
        positionClass: 'nfc-bottom-right',
        showDuration: 4000,
        theme: theme !== '' ? theme : 'success'
    })({
        title: title !== '' ? title : 'اعلان',
        message: decodeURI(message)
    });
}

function FillCurrentPage(currentPage) {
    debugger;
    $("#CurrentPage").val(currentPage);
    $("#filter-form").submit();
}

function OnSuccessRejectItem(res) {
    if (res.status === 'Success') {
        showMessage('اعلان موفقیت', res.message);
        $("#ajax-url-item-" + res.data.id + " a.btn-danger").hide();
        $("#reject-modal-" + res.data.id).modal('toggle');
    }
    else {
        showMessage('اعلان عدم موفقیت', res.message);
    }
}


