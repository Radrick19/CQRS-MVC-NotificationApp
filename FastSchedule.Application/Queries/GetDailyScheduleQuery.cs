using FastSchedule.Application.Services.ScheduleMaker.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Queries
{
    public class GetDailyScheduleQuery : IRequest<Day>
    {
        public GetDailyScheduleQuery(int year, int month, int day, int userId)
        {
            Year = year;
            Month = month;
            Day = day;
            UserId = userId;
        }

        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int UserId { get; set; }
    }
}
