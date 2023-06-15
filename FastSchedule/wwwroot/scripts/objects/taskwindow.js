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
        this.ModalWindowHandler = document.querySelector('.manage-task-window-handler');
        this.Open(year, month, day);
    }
    Open(year, month, day) {
        return __awaiter(this, void 0, void 0, function* () {
            let url = '/task/' + year + '/' + month + '/' + day;
            let self = this;
            this.ModalWindowHandler.innerHTML = "";
            this.ModalWindowHandler.insertAdjacentHTML("beforeend", yield AsyncAjaxGet(url));
            this.Task = new Task(year, month, day, '', 0, 0, document.querySelector('.color').id);
            document.querySelector('.background').setAttribute("style", "-webkit-filter:blur(8px) contrast(70%);");
            this.CloseButton = this.ModalWindowHandler.querySelector('.close-button');
            this.CloseButton.addEventListener('click', function () {
                self.ModalWindowHandler.innerHTML = "";
                document.querySelector('.background').setAttribute("style", "-webkit-filter:none");
            });
            for (const color of this.ModalWindowHandler.querySelectorAll('.color')) {
                color.addEventListener('click', function () {
                    self.Task.Color = color.id;
                });
            }
            this.ModalWindowHandler.querySelector('.add-button').addEventListener('click', function () {
                self.AddTask();
            });
        });
    }
    AddTask() {
        return __awaiter(this, void 0, void 0, function* () {
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
                let result = yield AsyncAjaxPost(url);
                if (result) {
                    TaskAddEvent(this.Task.Year, this.Task.Month, this.Task.Day);
                }
                else {
                    alert('error');
                }
            }
        });
    }
    ValidateData(task) {
        var nowDate = new Date();
        let splitedDate = task.Time.split(':');
        var selectedDate = new Date(task.Year, task.Month, task.Day, Number(splitedDate[0]), Number(splitedDate[1]));
        if (task.Year == null || task.Month == null || task.Day == null || task.Month > 12 || task.Month < 1 || task.Day < 1 || task.Day > 31) {
            alert("������ � �����");
            return false;
        }
        if (task.Label == null || task.Label == '') {
            alert("�� ������� �������� ������");
            return false;
        }
        if (task.Label.length > 55) {
            alert("������ ������� ��������");
            return false;
        }
        if (task.Time != null && task.Time != '' && nowDate >= selectedDate) {
            alert("������ ���������� ������ � �������");
            return false;
        }
        return true;
    }
}
//# sourceMappingURL=taskwindow.js.map