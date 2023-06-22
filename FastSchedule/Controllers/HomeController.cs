using FastSchedule.Application.Commands;
using FastSchedule.Application.Dto;
using FastSchedule.Application.Queries;
using FastSchedule.Application.Services.ScheduleMaker.Models;
using FastSchedule.Domain.Infrastucture.Enums;
using FastSchedule.MVC.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FastSchedule.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get/{year}/{month}")]
        [HttpGet("get/{year}/{month}/{isStartMonth:bool}")]
        public async Task<IActionResult> GetMonth(int year, int month, bool isStartMonth = false)
        {
            var userGuid = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            Schedule schedule = await _mediator.Send(new GetScheduleQuery(year, month, userGuid));
            int? weekGap = null;
            if (isStartMonth)
                weekGap = schedule.StartDayOfWeek == 0 ? 6 : (int)schedule.StartDayOfWeek - 1;

            MonthViewModel viewModel = new MonthViewModel()
            {
                Schedule = schedule,
                StartWeekGap = weekGap,
                TodayDate = DateOnly.FromDateTime(DateTime.Now),
            };
            return PartialView(viewModel);
        }

        [HttpGet("get/{year}/{month}/{day}")]
        public async Task<IActionResult> TasksWindow(int year, int month, int day)
        {
            var userGuid = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            Day daySchdule = await _mediator.Send(new GetDailyScheduleQuery(year, month, day, userGuid));

            return PartialView(daySchdule);
        }

        [HttpGet("update/{guid}")]
        public async Task<IActionResult> UpdateTaskWindow(string guid)
        {
            var userGuid = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            var tasks = await _mediator.Send(new GetTasksQuery(userGuid));
            var task = tasks.FirstOrDefault(task => task.Guid == new Guid(guid));
            var viewModel = new ModalWindowViewModel(task, isAddWindow: false);
            return PartialView(viewModel);
        }

        [HttpPost("update/{guid}/{year}/{month}/{day}/{label}/{reminder}/{repeat}/{color}")]
        [HttpPost("update/{guid}/{year}/{month}/{day}/{label}/{reminder}/{repeat}/{color}/{time}")]
        [HttpPost("updatewithdesc/{guid}/{year}/{month}/{day}/{label}/{reminder}/{repeat}/{color}/{description}")]
        [HttpPost("updatewithdesc/{guid}/{year}/{month}/{day}/{label}/{reminder}/{repeat}/{color}/{description}/{time}")]
        public async Task<bool> UpdateTask(string guid, string year, string month, string day, string label, int reminder, int repeat, string color,
            string? description = null, string? time = null)
        {
            var userGuid = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            try
            {
                var task = await _mediator.Send(new GetTaskByGuidQuery(new Guid(guid), userGuid));
                task.Label = label;
                if (task.TaskType != (TaskType)repeat)
                {
                    task.EventDate = new DateOnly(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day));
                }
                task.TaskType = (TaskType)repeat;
                task.Description = description;
                task.Color = color;
                if (time != null)
                {
                    var splitedTime = time.Split(':').Select(number => Convert.ToInt32(number)).ToArray();
                    task.EventTime = new TimeOnly(splitedTime[0], splitedTime[1]);
                }
                RemindType remindType = (RemindType)reminder;
                task.RemindType = remindType;
                if (remindType == RemindType.FifteenMinutes)
                {
                    task.PreNotifyTime = TimeSpan.FromMinutes(15);
                }
                else if (remindType == RemindType.HalfHour)
                {
                    task.PreNotifyTime = TimeSpan.FromMinutes(30);
                }
                else if (remindType == RemindType.Hour)
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
                await _mediator.Send(new UpdateTaskCommand(task));
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost("add/{year}/{month}/{day}/{label}/{reminder}/{repeat}/{color}")]
        [HttpPost("add/{year}/{month}/{day}/{label}/{reminder}/{repeat}/{color}/{time}")]
        [HttpPost("addwithdesc/{year}/{month}/{day}/{label}/{reminder}/{repeat}/{color}/{description}")]
        [HttpPost("addwithdesc/{year}/{month}/{day}/{label}/{reminder}/{repeat}/{color}/{description}/{time}")]
        public async Task<bool> AddTask(int year, int month, int day, string label, int reminder, int repeat, string color,
            string? description = null, string? time = null)
        {
            var userGuid = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            var user = await _mediator.Send(new GetUserByGuidQuery(userGuid));
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
                    UserId = user.Id,
                };

                if (time != null)
                {
                    var splitedTime = time.Split(':').Select(number => Convert.ToInt32(number)).ToArray();
                    task.EventTime = new TimeOnly(splitedTime[0], splitedTime[1]);
                }

                RemindType remindType = (RemindType)reminder;
                task.RemindType = remindType;

                if (remindType == RemindType.FifteenMinutes)
                {
                    task.PreNotifyTime = TimeSpan.FromMinutes(15);
                }
                else if (remindType == RemindType.HalfHour)
                {
                    task.PreNotifyTime = TimeSpan.FromMinutes(30);
                }
                else if (remindType == RemindType.Hour)
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

        [HttpPost("complete/{guid}/{year}/{month}/{day}")]
        public async Task<bool> AddCompletedDay(string guid, int year, int month, int day)
        {
            var userGuid = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            try
            {
                var task = await _mediator.Send(new GetTaskByGuidQuery(new Guid(guid), userGuid));
                var date = new DateOnly(year, month, day);
                List<DateOnly> completedDays;
                if (task.CompletedDays != null)
                    completedDays = task.CompletedDays.ToList();
                else
                    completedDays = new List<DateOnly>();

                completedDays.Add(date);
                task.CompletedDays = completedDays.AsEnumerable();
                await _mediator.Send(new UpdateTaskCommand(task));
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost("uncomplete/{guid}/{year}/{month}/{day}")]
        public async Task<bool> RemoveCompletedDay(string guid, int year, int month, int day)
        {
            var userGuid = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            try
            {
                var task = await _mediator.Send(new GetTaskByGuidQuery(new Guid(guid), userGuid));
                var date = new DateOnly(year, month, day);
                List<DateOnly> completedDays = task.CompletedDays.ToList();
                completedDays.Remove(date);
                task.CompletedDays = completedDays.AsEnumerable();
                await _mediator.Send(new UpdateTaskCommand(task));
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost("delete/{guid}/{year}/{month}/{day}")]
        public async Task<bool> DeleteDay(string guid, int year, int month, int day)
        {
            var userGuid = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            try
            {
                var task = await _mediator.Send(new GetTaskByGuidQuery(new Guid(guid), userGuid));
                var date = new DateOnly(year, month, day);
                List<DateOnly> deletedDays;
                if (task.DeletedDays != null)
                    deletedDays = task.DeletedDays.ToList();
                else
                    deletedDays = new List<DateOnly>();

                deletedDays.Add(date);
                task.DeletedDays = deletedDays.AsEnumerable();
                await _mediator.Send(new UpdateTaskCommand(task));
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost("delete/{guid}")]
        public async Task<bool> DeleteTask(string guid)
        {
            var userGuid = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid).Value;
            try
            {
                var task = await _mediator.Send(new GetTaskByGuidQuery(new Guid(guid), userGuid));
                task.IsDeleted = true;
                await _mediator.Send(new UpdateTaskCommand(task));
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
