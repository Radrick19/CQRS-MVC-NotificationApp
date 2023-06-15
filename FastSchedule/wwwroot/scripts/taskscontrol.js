var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
let nowDay, nowMonth, nowYear;
let calendarGrid;
let manageWindow;
let isLoading = false;
document.addEventListener('DOMContentLoaded', function () {
    let todayDate = new Date();
    nowDay = todayDate.getDate();
    nowMonth = todayDate.getMonth() + 1;
    nowYear = todayDate.getFullYear();
    calendarGrid = new CalendarGrid(nowYear, nowMonth);
    document.querySelector('.previous-year').addEventListener('click', function () {
        calendarGrid = new CalendarGrid(calendarGrid.SelectedYear - 1, 6);
    });
    document.querySelector('.next-year').addEventListener('click', function () {
        calendarGrid = new CalendarGrid(calendarGrid.SelectedYear + 1, 6);
    });
});
function OpenManageWindow(year, month, day) {
    return __awaiter(this, void 0, void 0, function* () {
        manageWindow = new TaskWindow(year, month, day);
    });
}
function TaskAddEvent(year, month, day) {
    return __awaiter(this, void 0, void 0, function* () {
        calendarGrid = new CalendarGrid(nowYear, nowMonth);
        OpenManageWindow(year, month, day);
    });
}
function AsyncAjaxGet(url) {
    return new Promise((resolve, reject) => {
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
        });
    });
}
function AsyncAjaxPost(url) {
    return new Promise((resolve, reject) => {
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
        });
    });
}
//# sourceMappingURL=taskscontrol.js.map