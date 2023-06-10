using FastSchedule.Application.Queries;
using FastSchedule.Application.Services.ScheduleMaker;
using FastSchedule.Application.Services.ScheduleMaker.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Handlers
{
    public class GetScheduleQueryHandler : IRequestHandler<GetScheduleQuery, Schedule>
    {
        private readonly IScheduleMaker _scheduleMaker;

        public GetScheduleQueryHandler(IScheduleMaker scheduleMaker)
        {
            _scheduleMaker = scheduleMaker;
        }

        public async Task<Schedule> Handle(GetScheduleQuery request, CancellationToken cancellationToken)
        {
            return await _scheduleMaker.GenerateMonthlySchedule(request.Year, request.Month, request.UserId);
        }
    }
}
