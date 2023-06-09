using FastSchedule.Application.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Services
{
    public interface IScheduleMaker
    {
        public Task<Schedule> GenerateMonthlySchedule(DateTime firstDayOfMonth);
    }
}
