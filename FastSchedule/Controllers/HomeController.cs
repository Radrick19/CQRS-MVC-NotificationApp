using FastSchedule.Application.Queries;
using FastSchedule.Application.Services.ScheduleMaker.Models;
using FastSchedule.Domain.Interfaces;
using FastSchedule.MVC.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Identity.Client;

namespace FastSchedule.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("tasks/{year}/{month}")]
        [HttpGet("tasks/{year}/{month}/{day}")]
        public async Task<IActionResult> TasksCalendar(int? year = null, int? month = null, int? day = null)
        {
            DateOnly defaultDate;
            if (year.HasValue && month.HasValue && day.HasValue)
            {
                defaultDate = new DateOnly(year.Value, month.Value, day.Value);
            }
            else if (year.HasValue && month.HasValue)
            {
                defaultDate = new DateOnly(year.Value, month.Value, 1);
            }
            else
            {
                defaultDate = DateOnly.FromDateTime(DateTime.Now);
            }
            Schedule schedule = await _mediator.Send(new GetScheduleQuery(defaultDate.Year, defaultDate.Month, 1));
            var selectedDayTasks = schedule.Days.FirstOrDefault(day => day.Date == defaultDate).Tasks;
            HomeViewModel viewModel = new HomeViewModel()
            {
                Schedule = schedule,
                SelectedDate = defaultDate,
                SelectedDayTasks = selectedDayTasks,
            };
            return PartialView(viewModel);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
