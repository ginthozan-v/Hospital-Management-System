$('form').submit(function (e) {
    e.preventDefault();

    var _this = $(this);
    var _form = _this.closest("form");
    var isvalid = _form.valid();

    if (isvalid) {
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
    }
});

