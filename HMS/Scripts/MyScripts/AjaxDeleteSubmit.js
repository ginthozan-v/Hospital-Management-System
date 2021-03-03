$('form').submit(function (e) {
    e.preventDefault();
    $.ajax({
        url: this.action,
        type: this.method,
        data: $(this).serialize(),
        success: function (result) {
            if (result.success) {
                $('#divModalShow').modal('hide');
                setTimeout(function () {
                    location.reload();
                }, 500);
            }
            else {
                $('#modalContent').html(result);
            }
        }
    });
});