async function OpenManageWindow(year, month, day) {
	let url = '/task/' + year + '/' + month + '/' + day;
	modalWindowHandler.innerHTML = "";
	modalWindowHandler.insertAdjacentHTML("beforeend", await AsyncAjaxGet(url));
	document.querySelector('.background').setAttribute("style", "-webkit-filter:blur(8px) contrast(70%);");
	AddListenersToModalWindow();
	modalWindowHandler.querySelector('.close-button').addEventListener('click', function () {
		modalWindowHandler.innerHTML = "";
		document.querySelector('.background').setAttribute("style", "-webkit-filter:none");
	})
	selectedColor = document.querySelector('.color').id;
	document.getElementById(selectedColor).setAttribute("style", "border-style:solid");
}

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
		await GetStartedMonthes(year, month, day);
		OpenManageWindow(year, month, day);
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

function AddListenersToModalWindow() {
	for (const color of document.querySelectorAll('.color')) {
		color.addEventListener('click', function () {
			if (selectedColor != null) {
				document.getElementById(selectedColor).setAttribute("style", "border-style:none");
			}
			selectedColor = color.id;
			color.setAttribute("style", "border-style:solid");
		})
	}
	document.querySelector('.add-button').addEventListener('click', function () {
		AddTask();
	})
}