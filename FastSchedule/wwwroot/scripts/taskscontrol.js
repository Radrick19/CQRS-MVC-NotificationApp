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
let modalWindowOpened = false;
const monthDictionary = {
    1: "Январь",
    2: "Февраль",
    3: "Март",
    4: "Апрель",
    5: "Май",
    6: "Июнь",
    7: "Июль",
    8: "Август",
    9: "Сентябрь",
    10: "Октябрь",
    11: "Ноябрь",
    12: "Декабрь",
};
document.addEventListener("DOMContentLoaded", function () {
    let todayDate = new Date();
    nowDay = todayDate.getDate();
    nowMonth = todayDate.getMonth() + 1;
    nowYear = todayDate.getFullYear();
    calendarGrid = new CalendarGrid(nowYear, nowMonth, nowDay);
    document
        .querySelector(".previous-year")
        .addEventListener("click", function () {
        calendarGrid = new CalendarGrid(Number(calendarGrid.SelectedYear) - 1, 6);
    });
    document.querySelector(".next-year").addEventListener("click", function () {
        calendarGrid = new CalendarGrid(Number(calendarGrid.SelectedYear) + 1, 6);
    });
});
function OpenManageWindow(year, month, day) {
    return __awaiter(this, void 0, void 0, function* () {
        modalWindowOpened = true;
        manageWindow = new TaskWindow(year, month, day);
    });
}
function TasksUpdateEvent(year, month, day) {
    return __awaiter(this, void 0, void 0, function* () {
        calendarGrid = new CalendarGrid(year, month, day);
        OpenManageWindow(year, month, day);
    });
}
function AsyncAjaxGet(url) {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: "GET",
            dataType: "html",
            url: url,
            success: function (data) {
                return resolve(data);
            },
            error: function (err) {
                return reject(err);
            },
        });
    });
}
function AsyncAjaxPost(url) {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: "POST",
            url: url,
            dataType: "json",
            success: function (data) {
                return resolve(data);
            },
            error: function (err) {
                return reject(false);
            },
        });
    });
}
