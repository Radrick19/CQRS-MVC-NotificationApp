using FastSchedule.Application.Services.ScheduleMaker.Models;
using FastSchedule.Domain.Interfaces;

namespace FastSchedule.MVC.ViewModels
{
    public class HomeViewModel
    {
        public int? StartWeekGap { get; set; }
        public DateOnly TodayDate { get; set; }
        public Schedule Schedule { get; set; }
    }
}
