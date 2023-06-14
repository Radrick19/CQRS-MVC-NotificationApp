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
class CalendarGrid {
    get maxOpenedMonth() {
        return this._maxOpenedMonth;
    }
    set maxOpenedMonth(value) {
        if (value > 12) {
            this.maxOpenedMonth = 12;
        }
        else {
            this._maxOpenedMonth = value;
        }
    }
    get minOpenedMonth() {
        return this._minOpenedMonth;
    }
    set minOpenedMonth(value) {
        if (value < 1) {
            this._minOpenedMonth = 1;
        }
        else {
            this._minOpenedMonth = value;
        }
    }
    OnScrollCheck() {
        return __awaiter(this, void 0, void 0, function* () {
            if (this.maxOpenedMonth < 12 && isLoading == false && this.CalendarHandler.scrollTop >= this.CalendarHandler.scrollHeight - window.innerHeight - 150) {
                this.maxOpenedMonth += 1;
                yield this.AddMonth(this.SelectedYear, this.maxOpenedMonth, false);
                this.OnScrollCheck();
            }
            else if (this.minOpenedMonth > 1 && isLoading == false && this.CalendarHandler.scrollTop < 300) {
                this.minOpenedMonth -= 1;
                yield this.AddMonth(this.SelectedYear, this.minOpenedMonth, true);
                this.OnScrollCheck();
            }
        });
    }
    GetStartedMonthes(year, month) {
        return __awaiter(this, void 0, void 0, function* () {
            isLoading = true;
            if (year == nowYear) {
                month = nowMonth;
            }
            this.SelectedYear = year;
            document.querySelector('.total-year').innerHTML = this.SelectedYear.toString();
            this.maxOpenedMonth = Number(month) + 1;
            this.minOpenedMonth = Number(month) - 1;
            this.CalendarHandler.innerHTML = "";
            if (this.minOpenedMonth != 1) {
                yield this.AddMonth(year, this.minOpenedMonth, true);
            }
            yield this.AddMonth(year, month, false);
            if (this.maxOpenedMonth != 12) {
                yield this.AddMonth(year, this.maxOpenedMonth, false);
            }
            let selectedDay = document.querySelector('.selected-day');
            if (selectedDay != null) {
                yield this.SelectDay(selectedDay);
            }
            this.CalendarHandler.scrollTop = this.CalendarHandler.scrollHeight / 4;
            isLoading = false;
        });
    }
    AddMonth(year, month, isOnTop) {
        var _a, e_1, _b, _c, _d, e_2, _e, _f;
        return __awaiter(this, void 0, void 0, function* () {
            isLoading = true;
            let url;
            if (isOnTop) {
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
                url = '/tasks/' + year + '/' + (Number(month)) + '/' + true;
                this.CalendarHandler.insertAdjacentHTML("afterbegin", yield AsyncAjaxGet(url));
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
                ;
                url = '/tasks/' + year + '/' + (Number(month));
                this.CalendarHandler.insertAdjacentHTML("beforeend", yield AsyncAjaxGet(url));
            }
            for (let day = 1; day <= this.DaysInMonth(year, Number(month)); day++) {
                let gridToAdd = document.getElementById(year + '.' + (Number(month)) + '.' + day);
                let dateGrid = new DateGrid(year, (Number(month) - 1), day, gridToAdd);
                $(gridToAdd).data("year", dateGrid.Year.toString());
                $(gridToAdd).data("month", dateGrid.Month.toString());
                $(gridToAdd).data("day", dateGrid.Day.toString());
                this.Dates.push(dateGrid);
            }
            for (const grid of document.querySelectorAll('.grid-item')) {
                if (grid.getAttribute('listener') !== 'true') {
                    let self = this;
                    grid.addEventListener('click', function () {
                        return __awaiter(this, void 0, void 0, function* () {
                            yield self.SelectDay(grid);
                        });
                    });
                    grid.addEventListener('mouseover', function () {
                        document.querySelector('.month-info').innerHTML = grid.id;
                    });
                    grid.setAttribute('listener', 'true');
                }
            }
            isLoading = false;
        });
    }
    SelectDay(grid) {
        return __awaiter(this, void 0, void 0, function* () {
            if (grid.id != '') {
                if (this.SelectedDayGrid != null) {
                    this.SelectedDayGrid.querySelector('.manage-grid').style.visibility = 'hidden';
                    this.SelectedDayGrid.classList.remove('selected-day');
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
                this.SelectedDayGrid = grid;
                let selectedDayYear = $(grid).data("year");
                let selectedDayMonth = $(grid).data("month");
                this.UpdateSelectedMonth(selectedDayYear, selectedDayMonth);
            }
        });
    }
    UpdateSelectedMonth(selectedYear, selectedMonth) {
        var _a, e_3, _b, _c, _d, e_4, _e, _f;
        return __awaiter(this, void 0, void 0, function* () {
            let selectedMonthGrids = this.Dates.filter(date => date.Month == selectedMonth && date.Year == selectedYear);
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
                for (var _k = true, selectedMonthGrids_1 = __asyncValues(selectedMonthGrids), selectedMonthGrids_1_1; selectedMonthGrids_1_1 = yield selectedMonthGrids_1.next(), _d = selectedMonthGrids_1_1.done, !_d; _k = true) {
                    _f = selectedMonthGrids_1_1.value;
                    _k = false;
                    const selectedMonthGrid = _f;
                    selectedMonthGrid.Element.classList.add('total-month');
                }
            }
            catch (e_4_1) { e_4 = { error: e_4_1 }; }
            finally {
                try {
                    if (!_k && !_d && (_e = selectedMonthGrids_1.return)) yield _e.call(selectedMonthGrids_1);
                }
                finally { if (e_4) throw e_4.error; }
            }
        });
    }
    DaysInMonth(year, month) {
        return new Date(year, month, 0).getDate();
    }
    constructor(year, month) {
        this.Dates = new Array;
        this.CalendarHandler = document.querySelector('.calendar-grid');
        this.GetStartedMonthes(year, month);
    }
}
//# sourceMappingURL=calendargrid.js.map