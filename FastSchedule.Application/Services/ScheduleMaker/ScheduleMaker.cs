using FastSchedule.Application.Queries.TaskQueries;
using FastSchedule.Application.Services.ScheduleMaker;
using FastSchedule.Application.Services.ScheduleMaker.Models;
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

            var dailyTasks = await _mediator.Send(new GetDailyTasksQuery(userId));
            var weeklyTasks = await _mediator.Send(new GetWeeklyTasksQuery(userId));
            var monthlyTasks = await _mediator.Send(new GetMonthlyTasksQuery(userId));

            IEnumerable<DateOnly> daysOfMonth = Enumerable
                .Range(1, DateTime.DaysInMonth(year, month))
                .Select(day => new DateOnly(year, month, day))
                .AsEnumerable();


            foreach (var day in daysOfMonth)
            {
                Day daySchedule = new Day
                {
                    Date = day,
                    Tasks = new List<ITask>()
                };
                daySchedule.Tasks.AddRange(dailyTasks
                            .Where(task => task.EventDay == day)
                            .Cast<ITask>());
                daySchedule.Tasks.AddRange(weeklyTasks
                            .Where(task => task.EventDaysOfWeek.Any(dayOfWeek => dayOfWeek == day.DayOfWeek))
                            .Cast<ITask>());
                daySchedule.Tasks.AddRange(monthlyTasks
                            .Where(task => task.EventDaysOfMonth.Any(daysOfMonth => daysOfMonth == day.Day))
                            .Cast<ITask>());

                if(daySchedule.Tasks.Count == 0)
                {
                    days.Add(new Day() { Date = day});
                    continue;
                }
                days.Add(daySchedule);
            }
            return new Schedule(startDayOfWeek, days);
        }
    }
}
