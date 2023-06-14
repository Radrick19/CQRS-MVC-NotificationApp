declare module "jquery" {
	export = $;
}

let modalWindowHandler;
let selectedColor;
let nowDay, nowMonth, nowYear;
let isLoading = false;
let calendarGrid : CalendarGrid;

document.addEventListener('DOMContentLoaded', async function () {
	modalWindowHandler = document.querySelector('.manage-task-window-handler');

	let todayDate = new Date();
	nowDay = todayDate.getDate();
	nowMonth = todayDate.getMonth() + 1;
	nowYear = todayDate.getFullYear();

	calendarGrid = new CalendarGrid(nowYear, nowMonth);

	calendarGrid.CalendarHandler.addEventListener('scroll', function () {
		calendarGrid.OnScrollCheck();
	})

	document.querySelector('.previous-year').addEventListener('click', function () {
		calendarGrid = new CalendarGrid(calendarGrid.SelectedYear - 1, 6);
	})
	document.querySelector('.next-year').addEventListener('click', function () {
		calendarGrid = new CalendarGrid(calendarGrid.SelectedYear + 1, 6);
	})
})

function AsyncAjaxGet(url) {
	return new Promise(function (resolve, reject) {
		$.ajax({
			type: 'GET',
			dataType: 'html',
			url: url,
			success: function (data) {
				resolve(data);
			},
			error: function (err) {
				reject(err)
			}
		})
	})
}

function AsyncAjaxPost(url) {
	return new Promise(function (resolve, reject) {
		$.ajax({
			type: 'POST',
			url: url,
			//success: function () {
			//	resolve();
			//},
			//error: function (err) {
			//	reject(err)
			//}
		})
	})
}