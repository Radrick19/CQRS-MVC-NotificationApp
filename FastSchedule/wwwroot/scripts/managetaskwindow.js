var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
function OpenManageWindow(year, month, day) {
    return __awaiter(this, void 0, void 0, function* () {
        let url = '/task/' + year + '/' + month + '/' + day;
        modalWindowHandler.innerHTML = "";
        modalWindowHandler.insertAdjacentHTML("beforeend", yield AsyncAjaxGet(url));
        document.querySelector('.background').setAttribute("style", "-webkit-filter:blur(8px) contrast(70%);");
        AddListenersToModalWindow();
        modalWindowHandler.querySelector('.close-button').addEventListener('click', function () {
            modalWindowHandler.innerHTML = "";
            document.querySelector('.background').setAttribute("style", "-webkit-filter:none");
        });
        selectedColor = document.querySelector('.color').id;
        document.getElementById(selectedColor).setAttribute("style", "border-style:solid");
    });
}
function AddTask() {
    return __awaiter(this, void 0, void 0, function* () {
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
            yield AsyncAjaxPost(url);
            calendarGrid = new CalendarGrid(year, month);
            OpenManageWindow(year, month, day);
        }
    });
}
function ValidateData(year, month, day, label, time, reminder, repeat, description, color) {
    var nowDate = new Date();
    let splitedDate = time.split(':');
    var selectedDate = new Date(year, month, day, splitedDate[0], splitedDate[1]);
    if (year == null || month == null || day == null || month > 12 || month < 1 || day < 1 || day > 31) {
        alert("������ � �����");
        return false;
    }
    if (label == null || label == '') {
        alert("�� ������� �������� ������");
        return false;
    }
    if (label.length > 55) {
        alert("������ ������� ��������");
        return false;
    }
    if (time != null && nowDate >= selectedDate) {
        alert("������ ���������� ������ � �������");
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
        });
    }
    document.querySelector('.add-button').addEventListener('click', function () {
        AddTask();
    });
}
//# sourceMappingURL=managetaskwindow.js.map