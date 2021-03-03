$(document).ready(function () {
	$(":input[date-picker]").datepicker({
		minDate: new Date(),
	});
	$(":input[datetime-picker]").datetimepicker({
		minDate: new Date(),
		timeFormat: "HH:mm:ss"
	});
	$(":input[time-picker]").timepicker({
		timeFormat: "HH:mm"
	});

	
});