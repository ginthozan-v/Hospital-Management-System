//Active navigation
$(function () {
    var url = window.location.href;

    $("#myDIV a").each(function () {
        if (url == (this.href)) {
            $(this).closest("a").addClass("mm-active");
            $(this).parent().parent().parent().children("a").addClass("mm-active");
        }
    });
});


//toast

$(document).ready(function () {
    setTimeout(function () {
        $('#toast').show('All').delay(2000).hide(400);
    }, 600);
});



//Calendar
$(document).ready(function ()  {
    $("#listDay").click(function () {
        $("#listDayView").removeClass('none');
        $("#listWeekView").addClass('none');
        $("#listDay").addClass('active');
        $("#listWeek").removeClass('active');
        $("#DateTime").removeClass('none');
    });

    $("#listWeek").click(function () {
        $("#listWeekView").removeClass('none');
        $("#listDayView").addClass('none');
        $("#listDay").removeClass('active');
        $("#listWeek").addClass('active');
        $("#DateTime").addClass('none');
    });
});


//Drug Count
$(document).ready(function () {
    $('#Dosage').prop("readonly", true).prop("disabled", true);
    $(document).on('click', '.Dosageplus', function () {
        $('.Dosagecnt').val(parseInt($('.Dosagecnt').val()) + 1);
    });
    $(document).on('click', '.Dosageminus', function () {
        $('.Dosagecnt').val(parseInt($('.Dosagecnt').val()) - 1);
        if ($('.Dosagecnt').val() == 0) {
            $('.Dosagecnt').val(1);
        }
    });
});

$(document).ready(function () {
    $('.count').prop('disabled', true);

    $(document).on('click', '.Durationplus', function () {
        $('.Duratiocnt').val(parseInt($('.Duratiocnt').val()) + 1);
    });
    $(document).on('click', '.Durationminus', function () {
        $('.Duratiocnt').val(parseInt($('.Duratiocnt').val()) - 1);
        if ($('.Duratiocnt').val() == 0) {
            $('.Duratiocnt').val(1);
        }
    });
});