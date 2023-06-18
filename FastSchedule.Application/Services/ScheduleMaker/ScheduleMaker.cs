using FastSchedule.Application.Dto;
using FastSchedule.Application.Queries;
using FastSchedule.Application.Services.ScheduleMaker;
using FastSchedule.Application.Services.ScheduleMaker.Models;
using FastSchedule.Domain.Infrastucture.Enums;
using FastSchedule.Domain.Infrastucture.Extensions;
using FastSchedule.Domain.Interfaces;
using FastSchedule.Domain.Models.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Services.ScheduleMaker
{
    public class ScheduleMaker : IScheduleMaker
    {
        private readonly DateOnly _nowDate;
        private readonly TimeOnly _nowTime;
        private readonly IMediator _mediator;

        public ScheduleMaker(IMediator mediator)
        {
            _mediator = mediator;
            _nowDate = DateOnly.FromDateTime(DateTime.Now);
            _nowTime = TimeOnly.FromDateTime(DateTime.Now);
        }

        public async Task<Schedule> GetMonthlySchedule(int year, int month, int userId)
        {
            DayOfWeek startDayOfWeek = new DateTime(year, month, 1).DayOfWeek;
            var tasks = await _mediator.Send(new GetTasksQuery(userId));

            IEnumerable<DateOnly> daysOfMonth = Enumerable
                .Range(1, DateTime.DaysInMonth(year, month))
                .Select(day => new DateOnly(year, month, day))
                .AsEnumerable();

            List<Day> days = new List<Day>();
            foreach (var day in daysOfMonth)
            {
                days.Add(GetDay(tasks, day));
            }
            return new Schedule(startDayOfWeek, days);
        }

        public async Task<Day> GetDaySchedule(int year, int month, int day, int userId) 
        {
            var tasks = await _mediator.Send(new GetTasksQuery(userId));
            var date = new DateOnly(year, month, day);
            return GetDay(tasks, date);
        }

        private Day GetDay(IEnumerable<ScheduleTaskDto> tasks, DateOnly date)
        {
            // Список задач без удалённых задач, удалённых дней и завершённых дней и дней до объявления задачи
            var filteredTasks = tasks.Where(task => !task.IsDeleted && !task.CompletedDays.HasDate(date) && !task.DeletedDays.HasDate(date) && task.EventDate <= date);

            Day daySchedule = new Day
            {
                Date = date,
                Tasks = new List<ScheduleTaskDto>(),
                CompletedTasks = new List<ScheduleTaskDto>(),
                OverdueTasks = new List<ScheduleTaskDto>(),
            };

            //Одноразовые задачи
            daySchedule.Tasks
                .AddRange(filteredTasks
                .Where(task => task.TaskType == TaskType.Onetime && task.EventDate == date));

            // Ежедневные задачи
            daySchedule.Tasks
                .AddRange(filteredTasks
                .Where(task => task.TaskType == TaskType.Daily));

            // Еженедельные задачи
            daySchedule.Tasks
                .AddRange(filteredTasks
                .Where(task => task.TaskType == TaskType.Weekly && task.EventDate.DayOfWeek == date.DayOfWeek));

            // Ежемесячные задачи
            daySchedule.Tasks
                .AddRange(filteredTasks
                .Where(task => task.TaskType == TaskType.Monthly && task.EventDate.Day == date.Day));

            // Ежегодные здачи
            daySchedule.Tasks
                .AddRange(filteredTasks
                .Where(task => task.TaskType == TaskType.Yearly && task.EventDate.Month == date.Month && task.EventDate.Day == date.Day));

            // Завершённые задачи
            daySchedule.CompletedTasks
                .AddRange(tasks
                .Where(task => task.CompletedDays != null && task.CompletedDays.HasDate(date) && !task.DeletedDays.HasDate(date))
                .OrderBy(task => task.EventTime == null)
                .ThenBy(task => task.EventTime)
                .ToList());

            // Просроченные задачи
            daySchedule.OverdueTasks
                .AddRange(daySchedule.Tasks
                .Where(task => (task.EventDate < _nowDate) || (task.EventDate == _nowDate && (task.EventTime < _nowTime || task.EventTime == null)))
                .OrderBy(task => task.EventTime == null)
                .ThenBy(task => task.EventTime)
                .ToList());

            //Список актуальных задач
            daySchedule.Tasks = daySchedule.Tasks
                .Where(task => task.EventDate > _nowDate || (task.EventDate == _nowDate && (task.EventTime == null || task.EventTime > _nowTime)))
                .ToList();

            daySchedule.Tasks = daySchedule.Tasks
                .OrderBy(task => task.EventTime == null)
                .ThenBy(task => task.EventTime)
                .ToList();

            // Если есть завершённые задачи, нет выполняемых задач и просроченных задач то день завешённый
            if (daySchedule.Tasks.Count() == 0 && daySchedule.OverdueTasks.Count() == 0 && daySchedule.CompletedTasks.Count() > 0)
                daySchedule.IsDayComplete = true;

            return daySchedule;
        }
    }
}
