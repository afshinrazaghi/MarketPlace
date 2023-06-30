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


