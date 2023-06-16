var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
class TaskWindow {
    constructor(year, month, day) {
        this.IsDeleteTypeSelectOpen = false;
        this.Year = year;
        this.Month = month;
        this.Day = day;
        this.ModalWindowHandler = document.querySelector('.manage-task-window-handler');
        this.Open(year, month, day);
    }
    Open(year, month, day) {
        return __awaiter(this, void 0, void 0, function* () {
            let url = '/get/' + year + '/' + month + '/' + day;
            let self = this;
            this.ModalWindowHandler.innerHTML = "";
            this.ModalWindowHandler.insertAdjacentHTML("beforeend", yield AsyncAjaxGet(url));
            document.querySelector('.background').setAttribute("style", "-webkit-filter:blur(8px) contrast(70%);");
            this.CloseButton = this.ModalWindowHandler.querySelector('.close-button');
            this.CloseButton.addEventListener('click', function () {
                self.ModalWindowHandler.innerHTML = "";
                document.querySelector('.background').setAttribute("style", "-webkit-filter:none");
                modalWindowOpened = false;
            });
            for (const task of this.ModalWindowHandler.querySelectorAll('.update-link')) {
                task.addEventListener('click', function () {
                    self.OpenUpdateWindow(task.id);
                });
            }
            for (const task of this.ModalWindowHandler.querySelectorAll('.complete-task')) {
                task.addEventListener('click', function () {
                    self.AddCompletedDay(task.value, self.Year, self.Month, self.Day);
                });
            }
            for (const task of this.ModalWindowHandler.querySelectorAll('.uncomplete-task')) {
                task.addEventListener('click', function () {
                    self.RemoveCompletedDay(task.value, self.Year, self.Month, self.Day);
                });
            }
            let deletedSelectedCompletedDay = document.querySelector('.delete-selected-completed-day');
            if (deletedSelectedCompletedDay != null) {
                deletedSelectedCompletedDay.addEventListener('click', function () {
                    return __awaiter(this, void 0, void 0, function* () {
                        self.RemoveDay(deletedSelectedCompletedDay.id, self.Year, self.Month, self.Day);
                    });
                });
            }
            for (const button of this.ModalWindowHandler.querySelectorAll('.delete-button')) {
                let typeChoiseMenu = button.querySelector('.delete-type-choice');
                let deleteSelectedDayButton = typeChoiseMenu.querySelector('.delete-selected-day');
                let deleteAllTasksButton = typeChoiseMenu.querySelector('.delete-all-tasks');
                let closeButton = typeChoiseMenu.querySelector('.close-type-choice');
                closeButton.addEventListener('click', function () {
                    return __awaiter(this, void 0, void 0, function* () {
                        typeChoiseMenu.style.display = 'none';
                        yield setTimeout(function () { self.IsDeleteTypeSelectOpen = false; }, 100);
                    });
                });
                deleteSelectedDayButton.addEventListener('click', function () {
                    return __awaiter(this, void 0, void 0, function* () {
                        self.RemoveDay(button.id, self.Year, self.Month, self.Day);
                    });
                });
                deleteAllTasksButton.addEventListener('click', function () {
                    return __awaiter(this, void 0, void 0, function* () {
                        self.RemoveTask(button.id);
                    });
                });
                button.addEventListener('click', function () {
                    if (self.IsDeleteTypeSelectOpen == false) {
                        self.IsDeleteTypeSelectOpen = true;
                        typeChoiseMenu.style.display = 'flex';
                    }
                });
            }
            let AddTaskDiv = this.ModalWindowHandler.querySelector('.add-new-task');
            if (AddTaskDiv != null) {
                this.Task = new Task(year, month, day, '', 0, 0, document.querySelector('.color').id);
                for (const color of this.ModalWindowHandler.querySelectorAll('.color')) {
                    color.addEventListener('click', function () {
                        self.Task.Color = color.id;
                    });
                }
                this.ModalWindowHandler.querySelector('.add-button').addEventListener('click', function () {
                    self.AddTask();
                });
            }
            let timeInput = this.ModalWindowHandler.querySelector("[name='time']");
            let remindBlock = this.ModalWindowHandler.querySelector('.remind');
            timeInput.addEventListener('input', function () {
                if (timeInput.value != null && timeInput.value != '') {
                    remindBlock.style.display = 'block';
                }
                else {
                    timeInput.value = null;
                    remindBlock.style.display = 'none';
                }
            });
        });
    }
    OpenUpdateWindow(guid) {
        return __awaiter(this, void 0, void 0, function* () {
            let url = 'update/' + guid;
            let self = this;
            this.ModalWindowHandler.innerHTML = "";
            this.ModalWindowHandler.insertAdjacentHTML("beforeend", yield AsyncAjaxGet(url));
            this.Task = new Task(this.Year, this.Month, this.Day, '', 0, 0, this.ModalWindowHandler.querySelector('.preSelectedColor').id);
            let backButton = this.ModalWindowHandler.querySelector('.back-button');
            let updateButton = this.ModalWindowHandler.querySelector('.update-button');
            backButton.addEventListener('click', function () {
                self.ModalWindowHandler.innerHTML = "";
                self.Open(self.Year, self.Month, self.Day);
            });
            updateButton.addEventListener('click', function () {
                self.UpdateTask();
            });
            for (const color of this.ModalWindowHandler.querySelectorAll('.color')) {
                color.addEventListener('click', function () {
                    self.Task.Color = color.id;
                });
            }
            let timeInput = this.ModalWindowHandler.querySelector("[name='time']");
            let remindBlock = this.ModalWindowHandler.querySelector('.remind');
            timeInput.addEventListener('input', function () {
                if (timeInput.value != null && timeInput.value != '') {
                    remindBlock.style.display = 'block';
                }
                else {
                    timeInput.value = null;
                    remindBlock.style.display = 'none';
                }
            });
            timeInput.dispatchEvent(new Event('input'));
        });
    }
    UpdateTask() {
        return __awaiter(this, void 0, void 0, function* () {
            this.FillTask();
            if (this.ValidateData(this.Task)) {
                let url;
                if (this.Task.Description != null && this.Task.Description != '') {
                    url = 'updatewithdesc/' + this.Task.Guid + '/' + this.Task.Year + '/' + this.Task.Month + '/' + this.Task.Day + '/' + this.Task.Label + '/' + this.Task.ReminderType + '/' + this.Task.RepeatType + '/' + this.Task.Color + '/' + this.Task.Description;
                }
                else {
                    url = 'update/' + this.Task.Guid + '/' + this.Task.Year + '/' + this.Task.Month + '/' + this.Task.Day + '/' + this.Task.Label + '/' + this.Task.ReminderType + '/' + this.Task.RepeatType + '/' + this.Task.Color;
                }
                if (this.Task.Time != null && this.Task.Time != '') {
                    url += '/' + this.Task.Time;
                }
                let result = yield AsyncAjaxPost(url);
                if (result) {
                    TasksUpdateEvent(this.Task.Year, this.Task.Month, this.Task.Day);
                }
                else {
                    alert('error');
                }
            }
        });
    }
    AddTask() {
        return __awaiter(this, void 0, void 0, function* () {
            this.FillTask();
            if (this.ValidateData(this.Task)) {
                let url;
                if (this.Task.Description != null && this.Task.Description != '') {
                    url = 'addwithdesc/' + this.Task.Year + '/' + this.Task.Month + '/' + this.Task.Day + '/' + this.Task.Label + '/' + this.Task.ReminderType + '/' + this.Task.RepeatType + '/' + this.Task.Color + '/' + this.Task.Description;
                }
                else {
                    url = 'add/' + this.Task.Year + '/' + this.Task.Month + '/' + this.Task.Day + '/' + this.Task.Label + '/' + this.Task.ReminderType + '/' + this.Task.RepeatType + '/' + this.Task.Color;
                }
                if (this.Task.Time != null && this.Task.Time != '') {
                    url += '/' + this.Task.Time;
                }
                let result = yield AsyncAjaxPost(url);
                if (result) {
                    TasksUpdateEvent(this.Task.Year, this.Task.Month, this.Task.Day);
                }
                else {
                    alert('error');
                }
            }
        });
    }
    AddCompletedDay(guid, year, month, day) {
        return __awaiter(this, void 0, void 0, function* () {
            let url = 'complete/' + guid + '/' + year + '/' + month + '/' + day;
            let result = yield AsyncAjaxPost(url);
            if (result) {
                TasksUpdateEvent(this.Year, this.Month, this.Day);
            }
            else {
                alert("error");
            }
        });
    }
    RemoveCompletedDay(guid, year, month, day) {
        return __awaiter(this, void 0, void 0, function* () {
            let url = 'uncomplete/' + guid + '/' + year + '/' + month + '/' + day;
            let result = yield AsyncAjaxPost(url);
            if (result) {
                TasksUpdateEvent(this.Year, this.Month, this.Day);
            }
            else {
                alert("error");
            }
        });
    }
    RemoveDay(guid, year, month, day) {
        return __awaiter(this, void 0, void 0, function* () {
            let url = 'delete/' + guid + '/' + year + '/' + month + '/' + day;
            let result = yield AsyncAjaxPost(url);
            if (result) {
                TasksUpdateEvent(this.Year, this.Month, this.Day);
            }
            else {
                alert("error");
            }
        });
    }
    RemoveTask(guid) {
        return __awaiter(this, void 0, void 0, function* () {
            let url = 'delete/' + guid;
            let result = yield AsyncAjaxPost(url);
            if (result) {
                TasksUpdateEvent(this.Year, this.Month, this.Day);
            }
            else {
                alert("error");
            }
        });
    }
    ValidateData(task) {
        var nowDate = new Date();
        let splitedDate = task.Time.split(':');
        var selectedDate = new Date(task.Year, task.Month, task.Day, Number(splitedDate[0]), Number(splitedDate[1]));
        if (task.Year == null || task.Month == null || task.Day == null || task.Month > 12 || task.Month < 1 || task.Day < 1 || task.Day > 31) {
            alert("Ошибка с датой");
            return false;
        }
        if (task.Label == null || task.Label == '') {
            alert("Не указано название задачи");
            return false;
        }
        if (task.Label.length > 55) {
            alert("Сликом длинное название");
            return false;
        }
        if (task.Time != null && task.Time != '' && nowDate >= selectedDate) {
            alert("Нельзя установить задачу в прошлое");
            return false;
        }
        return true;
    }
    FillTask() {
        this.Task.Label = this.ModalWindowHandler.querySelector("[name='label']").value;
        this.Task.Time = this.ModalWindowHandler.querySelector("[name='time']").value;
        this.Task.ReminderType = this.ModalWindowHandler.querySelector("[name='reminder']").value;
        this.Task.RepeatType = this.ModalWindowHandler.querySelector("[name='repeat']").value;
        this.Task.Description = this.ModalWindowHandler.querySelector("[name='description']").value;
        let guidInput = this.ModalWindowHandler.querySelector("[name='guid']");
        if (guidInput != null) {
            this.Task.Guid = guidInput.value;
        }
    }
}
