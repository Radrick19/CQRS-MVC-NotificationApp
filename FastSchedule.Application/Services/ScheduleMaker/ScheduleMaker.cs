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
        private IMediator _mediator;

        public ScheduleMaker(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Schedule> GetMonthlySchedule(int year, int month, int userId)
        {
            DayOfWeek startDayOfWeek = new DateTime(year, month, 1).DayOfWeek;
            List<Day> days = new List<Day>();

            var tasks = await _mediator.Send(new GetTasksQuery(userId));

            IEnumerable<DateOnly> daysOfMonth = Enumerable
                .Range(1, DateTime.DaysInMonth(year, month))
                .Select(day => new DateOnly(year, month, day))
                .AsEnumerable();


            foreach (var day in daysOfMonth)
            {
                Day daySchedule = new Day
                {
                    Date = day,
                    Tasks = new List<ScheduleTaskDto>(),
                    CompletedTasks = new List<ScheduleTaskDto>(),
                    OverdueTasks = new List<ScheduleTaskDto>()
                };

                daySchedule.Tasks
                    .AddRange(tasks
                    .Where(task => !task.IsDeleted && task.EventDate == day && task.TaskType == TaskType.Onetime && !task.CompletedDays.HasDate(day) 
                     && !task.DeletedDays.HasDate(day)));

                daySchedule.Tasks
                    .AddRange(tasks
                    .Where(task => !task.IsDeleted && task.TaskType == TaskType.Daily && task.EventDate <= day && !task.CompletedDays.HasDate(day) 
                    && !task.DeletedDays.HasDate(day)));

                daySchedule.Tasks
                    .AddRange(tasks
                    .Where(task => !task.IsDeleted && task.EventDate.DayOfWeek == day.DayOfWeek && task.TaskType == TaskType.Weekly && task.EventDate <= day 
                    && !task.CompletedDays.HasDate(day) && !task.DeletedDays.HasDate(day)));

                daySchedule.Tasks
                    .AddRange(tasks
                    .Where(task => !task.IsDeleted && task.EventDate.Day == day.Day && task.TaskType == TaskType.Monthly && task.EventDate <= day 
                    && !task.CompletedDays.HasDate(day) && !task.DeletedDays.HasDate(day)));

                daySchedule.Tasks
                    .AddRange(tasks
                    .Where(task => !task.IsDeleted && task.EventDate.Month == day.Month && task.EventDate.Day == day.Day && task.TaskType == TaskType.Yearly 
                    && task.EventDate <= day && !task.CompletedDays.HasDate(day) && !task.DeletedDays.HasDate(day)));

                daySchedule.CompletedTasks
                    .AddRange(tasks
                    .Where(task => task.CompletedDays != null && task.CompletedDays.HasDate(day) && !task.DeletedDays.HasDate(day)));

                var resultDay = new Day() { Date = day };

                if (daySchedule.Tasks.Count() != 0)
                {
                    if(day < DateOnly.FromDateTime(DateTime.Now))
                    {
                        resultDay.OverdueTasks = daySchedule.Tasks.OrderBy(task => task.EventTime == null).ThenBy(task => task.EventTime).ToList();
                    }
                    else
                    {
                        resultDay.OverdueTasks = daySchedule.Tasks
                            .Where(task => task.EventTime != null && task.EventDate <= DateOnly.FromDateTime(DateTime.Now) && task.EventTime < TimeOnly.FromDateTime(DateTime.Now))
                            .OrderBy(task => task.EventTime == null).ThenBy(task => task.EventTime)
                            .ToList();

                        resultDay.Tasks = daySchedule.Tasks
                            .Where(task=> (task.EventDate > DateOnly.FromDateTime(DateTime.Now)) || task.EventTime == null || task.EventTime > TimeOnly.FromDateTime(DateTime.Now))
                            .OrderBy(task => task.EventTime == null).ThenBy(task => task.EventTime)
                            .ToList();
                    }

                }
                if (daySchedule.CompletedTasks.Count() != 0)
                {
                    resultDay.CompletedTasks = daySchedule.CompletedTasks.OrderBy(task => task.EventTime == null).ThenBy(task => task.EventTime).ToList();
                }
                if(resultDay.CompletedTasks != null && resultDay.CompletedTasks.Count() > 0 && (resultDay.Tasks == null || resultDay.Tasks.Count() == 0) && (resultDay.OverdueTasks == null 
                    || resultDay.OverdueTasks.Count() == 0)) 
                {
                    resultDay.IsDayComplete = true;
                }
                days.Add(resultDay);
            }
            return new Schedule(startDayOfWeek, days);
        }

        public async Task<Day> GetDaySchedule(int year, int month, int day, int userId) 
        {
            var tasks = await _mediator.Send(new GetTasksQuery(userId));
            var date = new DateOnly(year, month, day);
            Day daySchedule = new Day
            {
                Date = date,
                Tasks = new List<ScheduleTaskDto>(),
                CompletedTasks = new List<ScheduleTaskDto>()
            };

            daySchedule.Tasks
                .AddRange(tasks
                .Where(task => !task.IsDeleted && task.EventDate == date && task.TaskType == TaskType.Onetime && !task.CompletedDays.HasDate(date)
                 && !task.DeletedDays.HasDate(date)));

            daySchedule.Tasks
                .AddRange(tasks
                .Where(task => !task.IsDeleted && task.TaskType == TaskType.Daily && task.EventDate <= date && !task.CompletedDays.HasDate(date)
                && !task.DeletedDays.HasDate(date)));

            daySchedule.Tasks
                .AddRange(tasks
                .Where(task => !task.IsDeleted && task.EventDate.DayOfWeek == date.DayOfWeek && task.TaskType == TaskType.Weekly && task.EventDate <= date
                && !task.CompletedDays.HasDate(date) && !task.DeletedDays.HasDate(date)));

            daySchedule.Tasks
                .AddRange(tasks
                .Where(task => !task.IsDeleted && task.EventDate.Day == date.Day && task.TaskType == TaskType.Monthly && task.EventDate <= date
                && !task.CompletedDays.HasDate(date) && !task.DeletedDays.HasDate(date)));

            daySchedule.Tasks
                .AddRange(tasks
                .Where(task => !task.IsDeleted && task.EventDate.Month == date.Month && task.EventDate.Day == date.Day && task.TaskType == TaskType.Yearly
                && task.EventDate <= date && !task.CompletedDays.HasDate(date) && !task.DeletedDays.HasDate(date)));

            daySchedule.CompletedTasks
                .AddRange(tasks
                .Where(task => task.CompletedDays != null && task.CompletedDays.HasDate(date) && !task.DeletedDays.HasDate(date)));

            var resultDay = new Day() { Date = date };

            if (daySchedule.Tasks.Count() != 0)
            {
                if (date < DateOnly.FromDateTime(DateTime.Now))
                {
                    resultDay.OverdueTasks = daySchedule.Tasks.OrderBy(task => task.EventTime == null).ThenBy(task => task.EventTime).ToList();
                }
                else
                {
                    resultDay.OverdueTasks = daySchedule.Tasks
                        .Where(task => task.EventTime != null && task.EventDate <= DateOnly.FromDateTime(DateTime.Now) && task.EventTime < TimeOnly.FromDateTime(DateTime.Now))
                        .OrderBy(task => task.EventTime == null).ThenBy(task => task.EventTime)
                        .ToList();

                    resultDay.Tasks = daySchedule.Tasks
                        .Where(task => (task.EventDate > DateOnly.FromDateTime(DateTime.Now)) || task.EventTime == null || task.EventTime > TimeOnly.FromDateTime(DateTime.Now))
                        .OrderBy(task => task.EventTime == null).ThenBy(task => task.EventTime)
                        .ToList();
                }
            }
            if (daySchedule.CompletedTasks.Count() != 0)
            {
                resultDay.CompletedTasks = daySchedule.CompletedTasks.OrderBy(task => task.EventTime == null).ThenBy(task => task.EventTime).ToList();
            }
            return resultDay;
        }
    }
}
