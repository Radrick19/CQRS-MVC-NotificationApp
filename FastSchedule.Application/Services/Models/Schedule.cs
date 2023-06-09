using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Services.Models
{
    public class Schedule
    {
        public DayOfWeek StartDayOfWeek { get; set; }
        public IEnumerable<Day> Days { get; set; }

        public Schedule(DayOfWeek startDayOfWeek, IEnumerable<Day> days)
        {
            StartDayOfWeek = startDayOfWeek;
            Days = days;
        }
    }
}
