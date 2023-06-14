var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
let modalWindowHandler;
let selectedColor;
let nowDay, nowMonth, nowYear;
let isLoading = false;
let calendarGrid;
document.addEventListener('DOMContentLoaded', function () {
    return __awaiter(this, void 0, void 0, function* () {
        modalWindowHandler = document.querySelector('.manage-task-window-handler');
        let todayDate = new Date();
        nowDay = todayDate.getDate();
        nowMonth = todayDate.getMonth() + 1;
        nowYear = todayDate.getFullYear();
        calendarGrid = new CalendarGrid(nowYear, nowMonth);
        calendarGrid.CalendarHandler.addEventListener('scroll', function () {
            calendarGrid.OnScrollCheck();
        });
        document.querySelector('.previous-year').addEventListener('click', function () {
            calendarGrid = new CalendarGrid(calendarGrid.SelectedYear - 1, 6);
        });
        document.querySelector('.next-year').addEventListener('click', function () {
            calendarGrid = new CalendarGrid(calendarGrid.SelectedYear + 1, 6);
        });
    });
});
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
                reject(err);
            }
        });
    });
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
        });
    });
}
//# sourceMappingURL=taskscontrol.js.map