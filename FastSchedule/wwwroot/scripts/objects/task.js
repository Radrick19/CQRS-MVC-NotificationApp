class Task {
    get Color() {
        return this._Color;
    }
    set Color(value) {
        let selectedColor = document.getElementById(this.Color);
        if (selectedColor != null) {
            selectedColor.setAttribute("style", "border-style:none");
        }
        this._Color = value;
        document.getElementById(value).setAttribute("style", "border-style:solid");
    }
    constructor(year, month, day, label, reminderType, repeatType, color, time, description, guid) {
        this.Year = year;
        this.Month = month;
        this.Day = day;
        this.Label = label;
        this.ReminderType = reminderType;
        this.RepeatType = repeatType;
        this.Color = color;
        this.Time = time;
        this.Description = description;
        this.Guid = guid;
    }
}
