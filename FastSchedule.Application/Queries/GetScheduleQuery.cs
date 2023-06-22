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
        public string UserGuid { get; set; }

        public GetScheduleQuery(int year, int month, string userGuid)
        {
            UserGuid = userGuid;
            Year = year;
            Month = month;
        }
    }
}
