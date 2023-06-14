declare module "jquery" {
	export = $;
}

let selectedYear;
let calendar, modalWindowHandler;
let maxOpenedMonth, minOpenedMonth;
let selectedColor;
let nowDay, nowMonth, nowYear;
let selectedDayGrid;
let isLoading = false;

document.addEventListener('DOMContentLoaded', async function () {
	calendar = document.querySelector('.calendar-grid');
	modalWindowHandler = document.querySelector('.manage-task-window-handler');

	let todayDate = new Date();
	nowDay = todayDate.getDate();
	nowMonth = todayDate.getMonth() + 1;
	nowYear = todayDate.getFullYear();

	await GetStartedMonthes(nowYear, nowMonth, nowDay);

	calendar.addEventListener('scroll', function () {
		OnScrollCalendarGrow();
	})

	document.querySelector('.previous-year').addEventListener('click', function () {
		GetStartedMonthes(selectedYear - 1, 6)
	})
	document.querySelector('.next-year').addEventListener('click', function () {
		GetStartedMonthes(selectedYear + 1, 6)
	})
})

async function GetStartedMonthes(year : number, month : number, day? : number) {
	isLoading = true;
	if (year == nowYear && day == null) {
		month = nowMonth;
		day = nowDay;
	}
	selectedYear = year;

	maxOpenedMonth = Number(month) + 1;
	if (maxOpenedMonth > 12) {
		maxOpenedMonth = 12;
	}
	minOpenedMonth = Number(month) - 1;
	if (minOpenedMonth < 1) {
		minOpenedMonth = 1;
	}

	let url = '/tasks/' + year + '/' + month;
	if (day != null) {
		url += '/' + day;
	}

	let prevMonthUrl = '/tasks/' + year + '/' + (Number(month) - 1) + '/' + true;
	let nextMonthUrl = '/tasks/' + year + '/' + (Number(month) + 1);

	calendar.innerHTML = "";
	calendar.insertAdjacentHTML("beforeend", await AsyncAjaxGet(prevMonthUrl));
	calendar.insertAdjacentHTML("beforeend", await AsyncAjaxGet(url));
	calendar.insertAdjacentHTML("beforeend", await AsyncAjaxGet(nextMonthUrl));

	document.querySelector('.total-year').innerHTML = selectedYear;

	let selectedDay = document.querySelector('.selected-day');
	if (selectedDay != null) {
		await SelectDay(selectedDay);
	}

	calendar.scrollTop = calendar.scrollHeight / 4;

	await AddListenerToGrids();
	isLoading = false;
}

async function AddListenerToGrids() {
	for (const grid of document.querySelectorAll('.grid-item')) {
		if (grid.getAttribute('listener') !== 'true') {
			grid.addEventListener('click', async function () {
				await SelectDay(grid);
			})
			grid.addEventListener('mouseover', function () {
				document.querySelector('.month-info').innerHTML = grid.id;
			})
			grid.setAttribute('listener', 'true');
		}
	}
}

async function AddMonth(year, isScrollTop) {
	isLoading = true;
	let url;
	if (isScrollTop) {
		minOpenedMonth -= 1;
		for await (const grid of document.querySelectorAll('.empty-grid')) {
			grid.classList.add('old-grid')
		}
		url = '/tasks/' + year + '/' + (Number(minOpenedMonth)) + '/' + true;
		calendar.insertAdjacentHTML("afterbegin", await AsyncAjaxGet(url));
		for await (const grid of document.querySelectorAll('.old-grid')) {
			grid.remove();
		}
	}
	else {
		maxOpenedMonth += 1;
		url = '/tasks/' + year + '/' + (Number(maxOpenedMonth));
		calendar.insertAdjacentHTML("beforeend", await AsyncAjaxGet(url))
	}
	await AddListenerToGrids();
	isLoading = false
}

async function OnScrollCalendarGrow() {
	if (maxOpenedMonth < 12 && isLoading == false && calendar.scrollTop >= calendar.scrollHeight - window.innerHeight - 150) {
		await AddMonth(selectedYear, false);
	}

	else if (minOpenedMonth > 1 && isLoading == false && calendar.scrollTop < 300) {
		await AddMonth(selectedYear, true);
	}
}

async function SelectDay(grid) {
	if (selectedDayGrid != null) {
		selectedDayGrid.querySelector('.manage-grid').style.visibility = 'hidden';
		selectedDayGrid.classList.remove('selected-day')
	}
	grid.classList.add('selected-day');
	grid.querySelector('.manage-grid').style.visibility = 'visible';

	let manageGridButton = grid.querySelector('.manage-grid');
	if (manageGridButton != null) {
		manageGridButton.addEventListener('click', async function () {
			let year = grid.querySelector("[name='year']").value;
			let month = grid.querySelector("[name='month']").value;
			let day = grid.querySelector("[name='day']").value;
			await OpenManageWindow(year, month, day);
		})
	}

	await UpdateSelectedMonth('#' + grid.id);
	selectedDayGrid = grid;
}

async function UpdateSelectedMonth(selectedDayId) {
	for await (const oldMonthGrid of document.querySelectorAll('.total-month')) {
		oldMonthGrid.classList.remove('total-month')
	}
	for await (const selectedMonthGrid of document.querySelectorAll(selectedDayId)) {
		selectedMonthGrid.classList.add('total-month')
	}
}

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