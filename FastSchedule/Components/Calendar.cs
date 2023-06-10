using FastSchedule.Application.Services.ScheduleMaker.Models;
using FastSchedule.MVC.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace FastSchedule.MVC.Components
{
    public class Calendar : ViewComponent
    {
        public IViewComponentResult Invoke(Schedule schedule, DateOnly selectedDate)
        {
            CalendarViewModel viewModel = new CalendarViewModel
            {
                Schedule = schedule,
                SelectedDate = selectedDate
            };
            return View(viewModel);
        }
    }
}
