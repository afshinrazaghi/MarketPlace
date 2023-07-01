
function confirmStore(url, requestId) {
    Swal.fire({
        title: 'اخطار',
        text: 'أیا از انجام عملیات اطمینان دارید؟',
        icon: 'warning',
        showCancelButton: true,
        showCloseButton:true,
        confirmButtonText: 'تایید',
        cancelButtonText: 'انصراف',
    }).then((result) => {
        debugger;
        $("#ajax-url-item-" + requestId).find("a.btn-success").hide();
        if (result.isConfirmed) {
            $.post(url, { requestId: requestId }).then(function (data, textStatus, jqXHR) {
                if (data.status === 'success') {
                    showMessage('موفقیت', data.message);
                    $("#ajax-url-item-" + requestId).find("a.btn-success").hide();
                }
                else {
                    showMessage('خطا', data.message);
                }
            })
        }
        
    });



}

