class TaskWindow {
	public ModalWindowHandler: any;
	public CloseButton: Element;
	public Task: Task

	constructor(year: number, month: number, day: number) {
		this.ModalWindowHandler = document.querySelector('.manage-task-window-handler');
		this.Open(year, month, day);
	}

	private async Open(year: number, month: number, day: number) {
		let url = '/task/' + year + '/' + month + '/' + day;
		let self = this;
		this.ModalWindowHandler.innerHTML = "";
		this.ModalWindowHandler.insertAdjacentHTML("beforeend", await AsyncAjaxGet(url));

		this.Task = new Task(year, month, day, '', 0, 0, document.querySelector('.color').id)

		document.querySelector('.background').setAttribute("style", "-webkit-filter:blur(8px) contrast(70%);");

		this.CloseButton = this.ModalWindowHandler.querySelector('.close-button');

		this.CloseButton.addEventListener('click', function () {

			self.ModalWindowHandler.innerHTML = "";
			document.querySelector('.background').setAttribute("style", "-webkit-filter:none");
		})

		for (const color of this.ModalWindowHandler.querySelectorAll('.color')) {
			color.addEventListener('click', function () {
				self.Task.Color = color.id;
			})
		}

		this.ModalWindowHandler.querySelector('.add-button').addEventListener('click', function () {
			self.AddTask();
		})

	}

	private async AddTask() {
		this.Task.Label = this.ModalWindowHandler.querySelector("[name='label']").value;
		this.Task.Time = this.ModalWindowHandler.querySelector("[name='time']").value;
		this.Task.ReminderType = this.ModalWindowHandler.querySelector("[name='reminder']").value;
		this.Task.RepeatType = this.ModalWindowHandler.querySelector("[name='repeat']").value;
		this.Task.Description = this.ModalWindowHandler.querySelector("[name='description']").value;
		if (this.ValidateData(this.Task)) {
			let url;
			if (this.Task.Description != null && this.Task.Description != '') {
				url = 'addtaskwithdesc/' + this.Task.Year + '/' + this.Task.Month + '/' + this.Task.Day + '/' + this.Task.Label + '/' + this.Task.ReminderType + '/' + this.Task.RepeatType + '/' + this.Task.Color + '/' + this.Task.Description;
			}
			else {
				url = 'addtask/' + this.Task.Year + '/' + this.Task.Month + '/' + this.Task.Day + '/' + this.Task.Label + '/' + this.Task.ReminderType + '/' + this.Task.RepeatType + '/' + this.Task.Color;
			}
			if (this.Task.Time != null && this.Task.Time != '') {
				url += '/' + this.Task.Time;
			}
			let result : boolean = await AsyncAjaxPost(url);
			if (result) {
				TaskAddEvent(this.Task.Year, this.Task.Month, this.Task.Day);
			}
			else {
				alert('error');
			}
		}
	}

	private ValidateData(task : Task) : boolean {
		var nowDate = new Date()
		let splitedDate = task.Time.split(':');
		var selectedDate = new Date(task.Year, task.Month, task.Day, Number(splitedDate[0]), Number(splitedDate[1]));

		if (task.Year == null || task.Month == null || task.Day == null || task.Month > 12 || task.Month < 1 || task.Day < 1 || task.Day > 31) {
			alert("Ошибка с датой");
			return false;
		}
		if (task.Label == null || task.Label == '') {
			alert("Не указано название задачи")
			return false;
		}
		if (task.Label.length > 55) {
			alert("Сликом длинное название")
			return false;
		}
		if (task.Time != null && task.Time != '' && nowDate >= selectedDate) {
			alert("Нельзя установить задачу в прошлое")
			return false;
		}
		return true;
	}
}