class Task {
  public Year: number;
  public Month: number;
  public Day: number;
  public Label: string;
  public Time: string;
  public ReminderType: ReminderType;
  public RepeatType: RepeatType;
  public Description: string;
  public Guid: string;
  private _Color: string;
  public get Color(): string {
    return this._Color;
  }
  public set Color(value: string) {
    let selectedColor = document.getElementById(this.Color);
    if (selectedColor != null) {
      selectedColor.setAttribute("style", "border-style:none");
    }
    this._Color = value;
    document.getElementById(value).setAttribute("style", "border-style:solid");
  }

  constructor(
    year: number,
    month: number,
    day: number,
    label: string,
    reminderType: ReminderType,
    repeatType: RepeatType,
    color: string,
    time?: string,
    description?: string,
    guid?: string
  ) {
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
