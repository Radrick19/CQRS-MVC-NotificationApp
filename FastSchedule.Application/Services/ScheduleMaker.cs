
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastSchedule.Application.Services.Models;

namespace FastSchedule.Application.Services
{
    public class ScheduleMaker : IScheduleMaker
    {
        public Task<Schedule> GenerateMonthlySchedule(DateTime firstDayOfMonth)
        {
            DayOfWeek firstDayOfWeekInMonth = firstDayOfMonth.DayOfWeek;
            List<Day> days = new List<Day>();
            int daysInMonth = DateTime.DaysInMonth(firstDayOfMonth.Year, firstDayOfMonth.Month);
            for (int i = 0; i < daysInMonth; i++)
            {

            }
            throw new NotImplementedException();
        }
    }
}
