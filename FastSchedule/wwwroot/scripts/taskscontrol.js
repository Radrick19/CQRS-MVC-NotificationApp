let prevMonthData, nextMonthData, totalMonthData;
let totalYear;
let calendar;
let maxOpenedMonth, minOpenedMonth;
let calendarIncreases = false;
let changeYear = false;
let modalWindowHandler;

let nowDay, nowMonth, nowYear;

document.addEventListener('DOMContentLoaded', async function () {
	calendar = document.querySelector('.calendar-grid');
	let todayDate = new Date();
	let nowDay = String(todayDate.getDate()).padStart(2, '0');
	let nowMonth = String(todayDate.getMonth() + 1).padStart(2, '0');
	let nowYear = todayDate.getFullYear();

	await GetTasks(nowYear, nowMonth, nowDay);

	calendar.addEventListener('scroll', function () {
		OnScrollCalendarGrow();
	})

	document.querySelector('.previous-year').addEventListener('click', function () {
		GetTasks(totalYear - 1, 6)
	})

	document.querySelector('.next-year').addEventListener('click', function () {
		GetTasks(totalYear + 1, 6)
	})

	modalWindowHandler = document.querySelector('.manage-task-window-handler');
})

function AddListenerToGrids() {

	for (const grid of document.querySelectorAll('.grid-item')) {
		if (grid.getAttribute('listener') !== 'true') {
			grid.addEventListener('click', async function () {
				let selectedDay = document.querySelector('.selected-day');
				if (selectedDay != null) {
					selectedDay.querySelector('.manage-grid').style.visibility = 'hidden';
					selectedDay.classList.remove('selected-day')
				}
				grid.classList.add('selected-day');
				grid.querySelector('.manage-grid').style.visibility = 'visible';
				await UpdateSelectedMonth('#' + grid.id);
			})
			let manageGridBtn = grid.querySelector('.manage-grid');
			if (manageGridBtn != null) {
				manageGridBtn.addEventListener('click', async function () {
					await OpenManageWindow(grid);
				})
			}
			grid.addEventListener('mouseover', function () {
				document.querySelector('.month-info').innerHTML = grid.id;
			})
			grid.setAttribute('listener', 'true');
		}
	}
}

async function OpenManageWindow(grid) {
	let year = grid.querySelector("[name='year']").value;
	let month = grid.querySelector("[name='month']").value;
	let day = grid.querySelector("[name='day']").value;
	let url = '/task/' + year + '/' + month + '/' + day;
	modalWindowHandler.insertAdjacentHTML("beforeend", await AsyncAjax(url));
	document.querySelector('.background').setAttribute("style", "-webkit-filter:blur(8px) contrast(70%);");
	AddListenersInModalWindow();
	modalWindowHandler.querySelector('.close-button').addEventListener('click', function () {
		modalWindowHandler.innerHTML = "";
		document.querySelector('.background').setAttribute("style", "-webkit-filter:none");
	})
	selectedColor = document.querySelector('.color').id;
	document.getElementById(selectedColor).setAttribute("style", "border-style:solid");
}

async function UpdateSelectedMonth(selectedDayId) {
	for await (const oldMonthGrid of document.querySelectorAll('.total-month')) {
		oldMonthGrid.classList.remove('total-month')
	}
	for await (const selectedMonthGrid of document.querySelectorAll(selectedDayId)) {
		selectedMonthGrid.classList.add('total-month')
	}
}

function AsyncAjax(url) {
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
			dataType: 'bool',
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

async function OnScrollCalendarGrow() {

	if (calendar.scrollTop >= calendar.scrollHeight - 1250 && calendarIncreases == false && maxOpenedMonth < 12 && changeYear == false) {
		calendarIncreases = true;
		let url = '/tasks/' + totalYear + '/' + (Number(maxOpenedMonth) + 1);
		maxOpenedMonth += 1;
		calendar.insertAdjacentHTML("beforeend", await AsyncAjax(url))
		AddListenerToGrids();
		calendarIncreases = false
	}

	else if (calendar.scrollTop <= 150 && calendarIncreases == false && minOpenedMonth > 1 && changeYear == false) {
		calendarIncreases = true;
		for await(const grid of document.querySelectorAll('.empty-grid')) {
			grid.classList.add('old-grid')
		}
		let url = '/tasks/' + totalYear + '/' + (Number(minOpenedMonth) - 1) + '/' + true;
		minOpenedMonth -= 1;
		calendar.insertAdjacentHTML("afterbegin", await AsyncAjax(url));
		for await (const grid of document.querySelectorAll('.old-grid')) {
			grid.remove();
		}
		AddListenerToGrids();
		calendarIncreases = false
	}
}

async function GetTasks(year, month, day) {
	changeYear = true;
	if (year == nowYear) {
		month = nowMonth;
		day = nowDay;
	}

	totalYear = year;

	maxOpenedMonth = Number(month) + 1;
	minOpenedMonth = Number(month) - 1;

	let url;
	if (day != null && day != '') {
		url = '/tasks/' + year + '/' + month + '/' + day;
	}
	else {
		url = '/tasks/' + year + '/' + month;
	}
	let prevMonthUrl = '/tasks/' + year + '/' + (Number(month) - 1) + '/' + true;
	let nextMonthUrl = '/tasks/' + year + '/' + (Number(month) + 1);

	calendar.innerHTML = "";

	let prev = await AsyncAjax(prevMonthUrl);
	let now = await AsyncAjax(url);
	let next = await AsyncAjax(nextMonthUrl);

	calendar.insertAdjacentHTML("beforeend", prev);
	calendar.insertAdjacentHTML("beforeend", now);
	calendar.insertAdjacentHTML("beforeend", next);

	document.querySelector('.total-year').innerHTML = totalYear;

	let selectedDay = document.querySelector('.selected-day');
	if (selectedDay != null) {
		let selectedDayManageBtn = selectedDay.querySelector('.manage-grid');
		if (selectedDayManageBtn != null) {
			selectedDayManageBtn.style.visibility = 'visible';
		}
		let todayId = '#' + selectedDay.id;
		for (const grid of document.querySelectorAll(todayId)) {
			grid.classList.add('total-month')
		}
	}


	calendar.scrollTop = calendar.scrollHeight / 4;

	AddListenerToGrids();
	changeYear = false;
}

let selectedColor;

async function AddTask() {
	let year = modalWindowHandler.querySelector("[name='year']").value;
	let month = modalWindowHandler.querySelector("[name='month']").value;
	let day = modalWindowHandler.querySelector("[name='day']").value;
	let label = modalWindowHandler.querySelector("[name='label']").value;
	let time = modalWindowHandler.querySelector("[name='time']").value;
	let reminder = modalWindowHandler.querySelector("[name='reminder']").value;
	let repeat = modalWindowHandler.querySelector("[name='repeat']").value;
	let description = modalWindowHandler.querySelector("[name='description']").value;
	let color = selectedColor.replace('#', '');
	if (ValidateData(year, month, day, label, time, reminder, repeat, description, selectedColor)) {
		let url;
		if (description != null && description != '') {
			url = 'addtaskwithdesc/' + year + '/' + month + '/' + day + '/' + label + '/' + reminder + '/' + repeat + '/' + color + '/' + description;
			if (time != null && time != '') {
				url += '/' + time;
			}
		}
		else {
			url = 'addtask/' + year + '/' + month + '/' + day + '/' + label + '/' + reminder + '/' + repeat + '/' + color;
			if (time != null && time != '') {
				url += '/' + time;
			}
		}
		await AsyncAjaxPost(url);
		location.reload();
	}
}

function ValidateData(year, month, day, label, time, reminder, repeat, description, color) {
	var nowDate = new Date()
	let splitedDate = time.split(':');
	var selectedDate = new Date(year, month, day, splitedDate[0], splitedDate[1]);

	if (year == null || month == null || day == null || month > 12 || month < 1 || day < 1 || day > 31) {
		alert("Ошибка с датой");
		return false;
	}
	if (label == null || label == '') {
		alert("Не указано название задачи")
		return false;
	}
	if (label.length > 55) {
		alert("Сликом длинное название")
		return false;
	}
	if (time != null && nowDate >= selectedDate) {
		alert("Нельзя установить задачу в прошлое")
		return false;
	}
	return true;
}

function AddListenersInModalWindow() {
	for (const color of document.querySelectorAll('.color')) {
		color.addEventListener('click', function () {
			if (selectedColor != null) {
				document.getElementById(selectedColor).setAttribute("style", "border-style:none");
			}
			selectedColor = color.id;
			color.setAttribute("style", "border-style:solid");
		})
	}
	document.querySelector('.add-button').addEventListener('click', function(){
		AddTask();
	})
}