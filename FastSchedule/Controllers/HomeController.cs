using FastSchedule.Application.Commands;
using FastSchedule.Application.Dto;
using FastSchedule.Application.Queries;
using FastSchedule.Application.Services.ScheduleMaker.Models;
using FastSchedule.Domain.Infrastucture.Enums;
using FastSchedule.Domain.Interfaces;
using FastSchedule.Domain.Models;
using FastSchedule.Domain.Models.Tasks;
using FastSchedule.MVC.Infrastructure.Enums;
using FastSchedule.MVC.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Identity.Client;
using System.Transactions;

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
        [HttpGet("tasks/{year}/{month}/{isStartMonth:bool}")]
        public async Task<IActionResult> TasksCalendar(int year, int month, bool isStartMonth = false)
        {
            Schedule schedule = await _mediator.Send(new GetScheduleQuery(year, month, 2));
            int? weekGap = null;
            if (isStartMonth)
                weekGap = schedule.StartDayOfWeek == 0 ? 6 : (int)schedule.StartDayOfWeek - 1;

            HomeViewModel viewModel = new HomeViewModel()
            {
                Schedule = schedule,
                StartWeekGap = weekGap,
                TodayDate = DateOnly.FromDateTime(DateTime.Now),
            };
            return PartialView(viewModel);
        }

        [HttpGet("task/{year}/{month}/{day}")]
        public async Task<IActionResult> ManageTask(int year, int month, int day)
        {
            Day daySchdule = await _mediator.Send(new GetDailyScheduleQuery(year,month, day));

            return PartialView(daySchdule);
        }

        [HttpPost("addtask/{year}/{month}/{day}/{label}/{reminder}/{repeat}/{color}")]
        [HttpPost("addtaskwithdesc/{year}/{month}/{day}/{label}/{reminder}/{repeat}/{color}/{description}")]
        [HttpPost("addtask/{year}/{month}/{day}/{label}/{reminder}/{repeat}/{color}/{time}")]
        [HttpPost("addtaskwithdesc/{year}/{month}/{day}/{label}/{reminder}/{repeat}/{color}/{description}/{time}")]
        public async Task<bool> AddTask(int year, int month, int day, string label, int reminder, int repeat, string color, 
            string? description = null, string? time = null)
        {
            try
            {
                ScheduleTaskDto task = new ScheduleTaskDto()
                {
                    Guid = Guid.NewGuid(),
                    EventDate = new DateOnly(year, month, day),
                    Label = label,
                    TaskType = (TaskType)repeat,
                    Color = color,
                    Description = description,
                    UserId = 2,
                };

                if (time != null)
                {
                    var splitedTime = time.Split(':').Select(number => Convert.ToInt32(number)).ToArray();
                    task.EventTime = new TimeOnly(splitedTime[0], splitedTime[1]);
                }

                RemindType remindType = (RemindType)reminder;

                if(remindType == RemindType.FifteenMinutes)
                {
                    task.PreNotifyTime = TimeSpan.FromMinutes(15);
                }
                else if(remindType == RemindType.HalfHour)
                {
                    task.PreNotifyTime = TimeSpan.FromMinutes(30);
                }
                else if(remindType == RemindType.Hour)
                {
                    task.PreNotifyTime = TimeSpan.FromHours(1);
                }
                else if (remindType == RemindType.SixHour)
                {
                    task.PreNotifyTime = TimeSpan.FromHours(6);
                }
                else if (remindType == RemindType.Day)
                {
                    task.PreNotifyTime = TimeSpan.FromDays(1);
                }

                await _mediator.Send(new AddTaskCommand(task));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
