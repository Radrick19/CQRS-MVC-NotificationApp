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
		url = '/tasks/' + year + '/' + month + '/' + day
	}
	else {
		url = '/tasks/' + year + '/' + month
	}
	$.ajax({
		type: 'GET',
		dataType: 'html',
		url: url,
		success: function (data) {
			document.querySelector('.todaytasks-and-calendar').innerHTML = data;
		}
	})
}