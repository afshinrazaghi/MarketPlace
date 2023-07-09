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


$(document).ready(function () {
    let editors = $("[ckeditor]");
    if (editors.length > 0) {
        $.getScript('/js/ckeditor.js', function () {
            editors.each(function (index, item) {
                let id = $(item).attr("ckeditor");
                ClassicEditor.create(document.querySelector('[ckeditor="' + id + '"]'),
                    {
                        toolbar: {
                            items: [
                                'heading',
                                '|',
                                'bold',
                                'italic',
                                'link',
                                '|',
                                'fontSize',
                                'fontColor',
                                '|',
                                'imageUpload',
                                'blockQuote',
                                'insertTable',
                                'undo',
                                'redo',
                                'codeBlock'
                            ]
                        },
                        language: 'fa',
                        table: {
                            contentToolbar: [
                                'tableColumn',
                                'tableRow',
                                'mergeTableCells'
                            ]
                        },
                        licenseKey: '',
                        simpleUpload: {
                            // The URL that the images are uploaded to.
                            uploadUrl: '/Upload/UploadImage'
                        }

                    })
                    .then(editor => {
                        window.editor = editor;
                    }).catch(err => {
                        console.error(err);
                    });
            });
        });

    }
});

