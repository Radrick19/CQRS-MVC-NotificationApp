var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __asyncValues = (this && this.__asyncValues) || function (o) {
    if (!Symbol.asyncIterator) throw new TypeError("Symbol.asyncIterator is not defined.");
    var m = o[Symbol.asyncIterator], i;
    return m ? m.call(o) : (o = typeof __values === "function" ? __values(o) : o[Symbol.iterator](), i = {}, verb("next"), verb("throw"), verb("return"), i[Symbol.asyncIterator] = function () { return this; }, i);
    function verb(n) { i[n] = o[n] && function (v) { return new Promise(function (resolve, reject) { v = o[n](v), settle(resolve, reject, v.done, v.value); }); }; }
    function settle(resolve, reject, d, v) { Promise.resolve(v).then(function(v) { resolve({ value: v, done: d }); }, reject); }
};
let selectedYear;
let calendar, modalWindowHandler;
let maxOpenedMonth, minOpenedMonth;
let selectedColor;
let nowDay, nowMonth, nowYear;
let selectedDayGrid;
let isLoading = false;
document.addEventListener('DOMContentLoaded', function () {
    return __awaiter(this, void 0, void 0, function* () {
        calendar = document.querySelector('.calendar-grid');
        modalWindowHandler = document.querySelector('.manage-task-window-handler');
        let todayDate = new Date();
        nowDay = todayDate.getDate();
        nowMonth = todayDate.getMonth() + 1;
        nowYear = todayDate.getFullYear();
        yield GetStartedMonthes(nowYear, nowMonth, nowDay);
        calendar.addEventListener('scroll', function () {
            OnScrollCalendarGrow();
        });
        document.querySelector('.previous-year').addEventListener('click', function () {
            GetStartedMonthes(selectedYear - 1, 6);
        });
        document.querySelector('.next-year').addEventListener('click', function () {
            GetStartedMonthes(selectedYear + 1, 6);
        });
    });
});
function GetStartedMonthes(year, month, day) {
    return __awaiter(this, void 0, void 0, function* () {
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
        calendar.insertAdjacentHTML("beforeend", yield AsyncAjaxGet(prevMonthUrl));
        calendar.insertAdjacentHTML("beforeend", yield AsyncAjaxGet(url));
        calendar.insertAdjacentHTML("beforeend", yield AsyncAjaxGet(nextMonthUrl));
        document.querySelector('.total-year').innerHTML = selectedYear;
        let selectedDay = document.querySelector('.selected-day');
        if (selectedDay != null) {
            yield SelectDay(selectedDay);
        }
        calendar.scrollTop = calendar.scrollHeight / 4;
        yield AddListenerToGrids();
        isLoading = false;
    });
}
function AddListenerToGrids() {
    return __awaiter(this, void 0, void 0, function* () {
        for (const grid of document.querySelectorAll('.grid-item')) {
            if (grid.getAttribute('listener') !== 'true') {
                grid.addEventListener('click', function () {
                    return __awaiter(this, void 0, void 0, function* () {
                        yield SelectDay(grid);
                    });
                });
                grid.addEventListener('mouseover', function () {
                    document.querySelector('.month-info').innerHTML = grid.id;
                });
                grid.setAttribute('listener', 'true');
            }
        }
    });
}
function AddMonth(year, isScrollTop) {
    var _a, e_1, _b, _c, _d, e_2, _e, _f;
    return __awaiter(this, void 0, void 0, function* () {
        isLoading = true;
        let url;
        if (isScrollTop) {
            minOpenedMonth -= 1;
            try {
                for (var _g = true, _h = __asyncValues(document.querySelectorAll('.empty-grid')), _j; _j = yield _h.next(), _a = _j.done, !_a; _g = true) {
                    _c = _j.value;
                    _g = false;
                    const grid = _c;
                    grid.classList.add('old-grid');
                }
            }
            catch (e_1_1) { e_1 = { error: e_1_1 }; }
            finally {
                try {
                    if (!_g && !_a && (_b = _h.return)) yield _b.call(_h);
                }
                finally { if (e_1) throw e_1.error; }
            }
            url = '/tasks/' + year + '/' + (Number(minOpenedMonth)) + '/' + true;
            calendar.insertAdjacentHTML("afterbegin", yield AsyncAjaxGet(url));
            try {
                for (var _k = true, _l = __asyncValues(document.querySelectorAll('.old-grid')), _m; _m = yield _l.next(), _d = _m.done, !_d; _k = true) {
                    _f = _m.value;
                    _k = false;
                    const grid = _f;
                    grid.remove();
                }
            }
            catch (e_2_1) { e_2 = { error: e_2_1 }; }
            finally {
                try {
                    if (!_k && !_d && (_e = _l.return)) yield _e.call(_l);
                }
                finally { if (e_2) throw e_2.error; }
            }
        }
        else {
            maxOpenedMonth += 1;
            url = '/tasks/' + year + '/' + (Number(maxOpenedMonth));
            calendar.insertAdjacentHTML("beforeend", yield AsyncAjaxGet(url));
        }
        yield AddListenerToGrids();
        isLoading = false;
    });
}
function OnScrollCalendarGrow() {
    return __awaiter(this, void 0, void 0, function* () {
        if (maxOpenedMonth < 12 && isLoading == false && calendar.scrollTop >= calendar.scrollHeight - window.innerHeight - 150) {
            yield AddMonth(selectedYear, false);
        }
        else if (minOpenedMonth > 1 && isLoading == false && calendar.scrollTop < 300) {
            yield AddMonth(selectedYear, true);
        }
    });
}
function SelectDay(grid) {
    return __awaiter(this, void 0, void 0, function* () {
        if (selectedDayGrid != null) {
            selectedDayGrid.querySelector('.manage-grid').style.visibility = 'hidden';
            selectedDayGrid.classList.remove('selected-day');
        }
        grid.classList.add('selected-day');
        grid.querySelector('.manage-grid').style.visibility = 'visible';
        let manageGridButton = grid.querySelector('.manage-grid');
        if (manageGridButton != null) {
            manageGridButton.addEventListener('click', function () {
                return __awaiter(this, void 0, void 0, function* () {
                    let year = grid.querySelector("[name='year']").value;
                    let month = grid.querySelector("[name='month']").value;
                    let day = grid.querySelector("[name='day']").value;
                    yield OpenManageWindow(year, month, day);
                });
            });
        }
        yield UpdateSelectedMonth('#' + grid.id);
        selectedDayGrid = grid;
    });
}
function UpdateSelectedMonth(selectedDayId) {
    var _a, e_3, _b, _c, _d, e_4, _e, _f;
    return __awaiter(this, void 0, void 0, function* () {
        try {
            for (var _g = true, _h = __asyncValues(document.querySelectorAll('.total-month')), _j; _j = yield _h.next(), _a = _j.done, !_a; _g = true) {
                _c = _j.value;
                _g = false;
                const oldMonthGrid = _c;
                oldMonthGrid.classList.remove('total-month');
            }
        }
        catch (e_3_1) { e_3 = { error: e_3_1 }; }
        finally {
            try {
                if (!_g && !_a && (_b = _h.return)) yield _b.call(_h);
            }
            finally { if (e_3) throw e_3.error; }
        }
        try {
            for (var _k = true, _l = __asyncValues(document.querySelectorAll(selectedDayId)), _m; _m = yield _l.next(), _d = _m.done, !_d; _k = true) {
                _f = _m.value;
                _k = false;
                const selectedMonthGrid = _f;
                selectedMonthGrid.classList.add('total-month');
            }
        }
        catch (e_4_1) { e_4 = { error: e_4_1 }; }
        finally {
            try {
                if (!_k && !_d && (_e = _l.return)) yield _e.call(_l);
            }
            finally { if (e_4) throw e_4.error; }
        }
    });
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