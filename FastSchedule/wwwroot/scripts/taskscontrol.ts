declare module "jquery" {
	export = $;
}

let nowDay: number, nowMonth: number, nowYear: number;
let calendarGrid: CalendarGrid;
let manageWindow: TaskWindow;
let isLoading: boolean = false;
let modalWindowOpened: boolean = false;

const monthDictionary = {
	1: 'Январь',
	2: 'Февраль',
	3: 'Март',
	4: 'Апрель',
	5: 'Май',
	6: 'Июнь',
	7: 'Июль',
	8: 'Август',
	9: 'Сентябрь',
	10: 'Октябрь',
	11: 'Ноябрь',
	12: 'Декабрь',
};


document.addEventListener('DOMContentLoaded', function () {
	let todayDate = new Date();
	nowDay = todayDate.getDate();
	nowMonth = todayDate.getMonth() + 1;
	nowYear = todayDate.getFullYear();

	calendarGrid = new CalendarGrid(nowYear, nowMonth, nowDay);

	document.querySelector('.previous-year').addEventListener('click', function () {
		calendarGrid = new CalendarGrid(Number(calendarGrid.SelectedYear) - 1, 6);
	})
	document.querySelector('.next-year').addEventListener('click', function () {
		calendarGrid = new CalendarGrid(Number(calendarGrid.SelectedYear) + 1, 6);
	})
})

async function OpenManageWindow(year: number, month: number, day: number) {
	modalWindowOpened = true;
	manageWindow = new TaskWindow(year, month, day);
}

async function TasksUpdateEvent(year: number, month: number, day?: number) {
	calendarGrid = new CalendarGrid(year, month, day);
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