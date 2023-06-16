using FastSchedule.Application.Services.ScheduleMaker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Services.ScheduleMaker
{
    public interface IScheduleMaker
    {
        public Task<Schedule> GetMonthlySchedule(int year, int month, int userId);
        public Task<Day> GetDaySchedule(int year, int month, int day, int userId);
    }

}
