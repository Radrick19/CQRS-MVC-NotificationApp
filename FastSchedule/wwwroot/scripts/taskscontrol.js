/*const tasksCalendarHandler = document.querySelector('.todaytasks-and-calendar');*/

document.addEventListener('DOMContentLoaded', function () {
	let todayDate = new Date();
	let day = String(todayDate.getDate()).padStart(2, '0');
	let month = String(todayDate.getMonth() + 1).padStart(2, '0');
	let year = todayDate.getFullYear();
	GetTasks(year, month, day);
})

function GetTasks(year, month, day) {
	let url;
	if (day != null && day != '') {
		url = '/tasks/' + year + '/' + month + '/' + day;
	}
	else {
		url = '/tasks/' + year + '/' + month;
	}
	urlNextMonth = '/tasks/' + year + '/' + (Number(month) + 1);
	urlPrevMonth = '/tasks/' + year + '/' + (Number(month) - 1);
	$.ajax({
		type: 'GET',
		dataType: 'html',
		url: urlNextMonth,
		success: function (data) {
			document.querySelector('.calendar-handler').innerHTML += data;
		}
	})
	$.ajax({
		type: 'GET',
		dataType: 'html',
		url: url,
		success: function (data) {
			document.querySelector('.calendar-handler').innerHTML += data;
		}
	})
	$.ajax({
		type: 'GET',
		dataType: 'html',
		url: urlNextMonth,
		success: function (data) {
			document.querySelector('.calendar-handler').innerHTML += data;
		}
	})
}