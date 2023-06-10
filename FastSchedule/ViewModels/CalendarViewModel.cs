using FastSchedule.Application.Services.ScheduleMaker.Models;

namespace FastSchedule.MVC.ViewModels
{
    public class CalendarViewModel
    {
        public Schedule Schedule{ get; set; }
        public DateOnly SelectedDate { get; set; }
    }
}
