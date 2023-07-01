class DateGrid {
  public Year: number;
  public Month: number;
  public Day: number;
  public Element: HTMLElement;
  constructor(year: number, month: number, day: number, element: HTMLElement) {
    this.Year = year;
    this.Month = month;
    this.Day = day;
    this.Element = element;
  }
}
