declare module "jquery" {
	export = $;
}

let nowDay: number, nowMonth: number, nowYear: number;
let calendarGrid: CalendarGrid;
let manageWindow: TaskWindow;
let isLoading : boolean = false;

document.addEventListener('DOMContentLoaded', function () {
	let todayDate = new Date();
	nowDay = todayDate.getDate();
	nowMonth = todayDate.getMonth() + 1;
	nowYear = todayDate.getFullYear();

	calendarGrid = new CalendarGrid(nowYear, nowMonth);

	document.querySelector('.previous-year').addEventListener('click', function () {
		calendarGrid = new CalendarGrid(calendarGrid.SelectedYear - 1, 6);
	})
	document.querySelector('.next-year').addEventListener('click', function () {
		calendarGrid = new CalendarGrid(calendarGrid.SelectedYear + 1, 6);
	})
})

async function OpenManageWindow(year, month, day) {
	manageWindow = new TaskWindow(year, month, day);
}

async function TaskAddEvent(year, month, day) {
	calendarGrid = new CalendarGrid(nowYear, nowMonth);
	OpenManageWindow(year, month, day);
}

function AsyncAjaxGet(url) {
	return new Promise<string>((resolve, reject) => {
		$.ajax({
			type: 'GET',
			dataType: 'html',
			url: url,
			success: function (data) {
				return resolve(data);
			},
			error: function (err) {
				return reject(err);
			}
		})
	})
}

function AsyncAjaxPost(url) {
	return new Promise<boolean>((resolve, reject) => {
		$.ajax({
			type: 'POST',
			url: url,
			dataType: 'json',
			success: function (data) {
				return resolve(data);
			},
			error: function (err) {
				return reject(false);
			}
		})
	})
}