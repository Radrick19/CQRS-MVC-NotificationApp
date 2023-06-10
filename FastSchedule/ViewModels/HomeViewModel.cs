using FastSchedule.Application.Services.ScheduleMaker.Models;
using FastSchedule.Domain.Interfaces;

namespace FastSchedule.MVC.ViewModels
{
    public class HomeViewModel
    {
        public int Year {  get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public DateOnly SelectedDate { get; set; }
        public IEnumerable<ITask>? SelectedDayTasks { get; set; }   
        public Schedule Schedule { get; set; }
    }
}
