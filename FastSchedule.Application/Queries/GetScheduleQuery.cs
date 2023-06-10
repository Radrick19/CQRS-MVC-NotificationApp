using FastSchedule.Application.Services.ScheduleMaker.Models;
using FastSchedule.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Queries
{
    public class GetScheduleQuery : IRequest<Schedule>
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int UserId { get; set; }

        public GetScheduleQuery(int year, int month, int userId)
        {
            UserId = userId;
            Year = year;
            Month = month;
        }
    }
}
