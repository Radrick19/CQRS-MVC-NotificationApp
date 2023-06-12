using FastSchedule.Application.Commands.TaskCommands;
using FastSchedule.Application.Dto;
using FastSchedule.Application.Queries;
using FastSchedule.Application.Services.ScheduleMaker.Models;
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
        [HttpGet("tasks/{year}/{month}/{day}")]
        public async Task<IActionResult> TasksCalendar(int year, int month, int? day = null, bool isStartMonth = false)
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
        public async Task<bool> AddTask(int year, int month, int day, string label, int reminder, int repeat, string color, string? description = null, string? time = null)
        {
            try
            {
                RepeatType repeatType = (RepeatType)repeat;
                RemindType remindType = (RemindType)reminder;
                ITask task;
                if (repeatType == RepeatType.None)
                {
                    var onetimeTaskDto = new OnetimeTaskDto();
                    onetimeTaskDto.EventDay = new DateOnly(year, month, day);
                    task = onetimeTaskDto;
                }
                else if (repeatType == RepeatType.EveryDay)
                {
                    task = new EverydayTaskDto();
                }
                else if (repeatType == RepeatType.EveryWeek)
                {
                    var weeklyTaskDto = new WeeklyTaskDto();
                    weeklyTaskDto.EventDayOfWeek = new DateTime(year, month, day).DayOfWeek;
                    task = weeklyTaskDto;
                }
                else
                {
                    var monthlyTaskDto = new MonthlyTaskDto();
                    monthlyTaskDto.EventDayOfMonth = day;
                    task = monthlyTaskDto;
                }
                task.Label = label;
                task.Color = color;
                task.Guid = Guid.NewGuid();
                task.UserId = 2;
                if (description != null)
                {
                    task.Description = description;
                }
                if (time != null)
                {
                    var splitedTime = time.Split(':').Select(number => Convert.ToInt32(number)).ToArray();
                    task.EventTime = new TimeOnly(splitedTime[0], splitedTime[1]);
                }

                if (repeatType == RepeatType.None)
                {
                    await _mediator.Send(new AddDailyTaskCommand(task as OnetimeTaskDto));
                }
                else if (repeatType == RepeatType.EveryDay)
                {
                    await _mediator.Send(new AddEverydayTaskCommand(task as EverydayTaskDto));
                }
                else if (repeatType == RepeatType.EveryWeek)
                {
                    await _mediator.Send(new AddWeeklyTaskCommand(task as WeeklyTaskDto));
                }
                else
                {
                    await _mediator.Send(new AddMonthlyTaskCommand(task as MonthlyTaskDto));
                }
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
