//ModalPopup
$(function () {
    $('.btn-popup').click(function () {
        var url = $(this).data("url");
        $.ajax({
            url: url,
            cache: false,
            success: function (data) {
                $('#modalContent').html(data);
                $('#divModalShow').modal('show');
            }
        });
    });

    $('#divModalShow').on('hidden.bs.modal', function () {
        $(this).removeData('bs.modal');
    });
});