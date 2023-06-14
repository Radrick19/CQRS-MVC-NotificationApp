class CalendarGrid {

    public Dates: DateGrid[] = new Array<DateGrid>;
	public SelectedYear: number;
	public SelectedDayGrid: any;

	public CalendarHandler : any;

	private _maxOpenedMonth : number;
    public get maxOpenedMonth() {
        return this._maxOpenedMonth;
    }
    public set maxOpenedMonth(value) {
		if (value > 12) {
			this.maxOpenedMonth = 12;
		}
		else {
			this._maxOpenedMonth = value;
		}
	}

    private _minOpenedMonth : number;
    public get minOpenedMonth() {
        return this._minOpenedMonth;
    }
    public set minOpenedMonth(value) {
		if (value < 1) {
			this._minOpenedMonth = 1;
		}
		else {
			this._minOpenedMonth = value;
		}
    }

	async OnScrollCheck() {
		if (this.maxOpenedMonth < 12 && isLoading == false && this.CalendarHandler.scrollTop >= this.CalendarHandler.scrollHeight - window.innerHeight - 150) {
			this.maxOpenedMonth += 1;
			await this.AddMonth(this.SelectedYear, this.maxOpenedMonth, false);
			this.OnScrollCheck();
		}

		else if (this.minOpenedMonth > 1 && isLoading == false && this.CalendarHandler.scrollTop < 300) {
			this.minOpenedMonth -= 1;
			await this.AddMonth(this.SelectedYear, this.minOpenedMonth, true);
			this.OnScrollCheck();
		}

	}

    async GetStartedMonthes(year: number, month: number) {
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
			await this.AddMonth(year, this.minOpenedMonth, true);
		}
		await this.AddMonth(year, month, false);

		if (this.maxOpenedMonth != 12) {
			await this.AddMonth(year, this.maxOpenedMonth, false);
		}

		let selectedDay = document.querySelector('.selected-day');
		if (selectedDay != null) {
			await this.SelectDay(selectedDay);
		}

		this.CalendarHandler.scrollTop = this.CalendarHandler.scrollHeight / 4;

		isLoading = false;
	}

	async AddMonth(year: number, month: number, isOnTop: boolean) {
		isLoading = true;
		let url;
		if (isOnTop) {
			for await (const grid of document.querySelectorAll('.empty-grid')) {
				grid.classList.add('old-grid')
			}
			url = '/tasks/' + year + '/' + (Number(month)) + '/' + true;
			this.CalendarHandler.insertAdjacentHTML("afterbegin", await AsyncAjaxGet(url));
			for await (const grid of document.querySelectorAll('.old-grid')) {
				grid.remove();
			}
		}
		else {;
			url = '/tasks/' + year + '/' + (Number(month));
			this.CalendarHandler.insertAdjacentHTML("beforeend", await AsyncAjaxGet(url))
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
				grid.addEventListener('click', async function () {
					await self.SelectDay(grid);
				})
				grid.addEventListener('mouseover', function () {
					document.querySelector('.month-info').innerHTML = grid.id;
				})
				grid.setAttribute('listener', 'true');
			}
		}
		isLoading = false
	}

	async SelectDay(grid) {
		if (grid.id != '') {
			if (this.SelectedDayGrid != null) {
				this.SelectedDayGrid.querySelector('.manage-grid').style.visibility = 'hidden';
				this.SelectedDayGrid.classList.remove('selected-day')
			}
			grid.classList.add('selected-day');
			grid.querySelector('.manage-grid').style.visibility = 'visible';
			let manageGridButton = grid.querySelector('.manage-grid');
			if (manageGridButton != null) {
				manageGridButton.addEventListener('click', async function () {
					let year = grid.querySelector("[name='year']").value;
					let month = grid.querySelector("[name='month']").value;
					let day = grid.querySelector("[name='day']").value;
					await OpenManageWindow(year, month, day);
				})
			}
			this.SelectedDayGrid = grid;
			let selectedDayYear = $(grid).data("year");
			let selectedDayMonth = $(grid).data("month");
			this.UpdateSelectedMonth(selectedDayYear, selectedDayMonth);
		}
	}

	async UpdateSelectedMonth(selectedYear: number, selectedMonth: number) {
		let selectedMonthGrids = this.Dates.filter(date => date.Month == selectedMonth && date.Year == selectedYear)
		for await (const oldMonthGrid of document.querySelectorAll('.total-month')) {
			oldMonthGrid.classList.remove('total-month')
		}
		for await (const selectedMonthGrid of selectedMonthGrids) {
			selectedMonthGrid.Element.classList.add('total-month')
		}
}

    private DaysInMonth(year: number, month: number) : number {
        return new Date(year, month, 0).getDate();
    }

	constructor(year: number, month: number) {
		this.CalendarHandler = document.querySelector('.calendar-grid');
		this.GetStartedMonthes(year, month);
    }
}