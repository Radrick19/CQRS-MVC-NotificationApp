using FastSchedule.Application.Dto;
using FastSchedule.Application.Queries;
using FastSchedule.Application.Services.ScheduleMaker;
using FastSchedule.Application.Services.ScheduleMaker.Models;
using FastSchedule.Domain.Infrastucture.Enums;
using FastSchedule.Domain.Interfaces;
using FastSchedule.Domain.Models.Tasks;
using MediatR;
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

        public async Task<Schedule> GenerateMonthlySchedule(int year, int month, int userId)
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
                    Tasks = new List<ScheduleTaskDto>()
                };

                daySchedule.Tasks
                    .AddRange(tasks
                    .Where(task => task.EventDate == day && task.TaskType == TaskType.Onetime));

                daySchedule.Tasks
                    .AddRange(tasks
                    .Where(task => task.TaskType == TaskType.Daily && task.EventDate <= day));

                daySchedule.Tasks
                    .AddRange(tasks
                    .Where(task => task.EventDate.DayOfWeek == day.DayOfWeek && task.TaskType == TaskType.Weekly && task.EventDate <= day));

                daySchedule.Tasks
                    .AddRange(tasks
                    .Where(task => task.EventDate.Day == day.Day && task.TaskType == TaskType.Monthly && task.EventDate <= day));

                daySchedule.Tasks
                    .AddRange(tasks
                    .Where(task => task.EventDate.Month == day.Month && task.EventDate.Day == day.Day && task.TaskType == TaskType.Yearly && task.EventDate <= day));

                if(daySchedule.Tasks.Count == 0)
                {
                    days.Add(new Day() { Date = day});
                    continue;
                }
                daySchedule.Tasks = daySchedule.Tasks.OrderBy(task=> task.EventTime == null).ThenBy(task => task.EventTime).ToList();
                days.Add(daySchedule);
            }
            return new Schedule(startDayOfWeek, days);
        }
    }
}
