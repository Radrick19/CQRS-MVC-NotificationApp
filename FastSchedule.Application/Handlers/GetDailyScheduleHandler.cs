using FastSchedule.Application.Queries;
using FastSchedule.Application.Services.ScheduleMaker;
using FastSchedule.Application.Services.ScheduleMaker.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Handlers
{
    public class GetDailyScheduleHandler : IRequestHandler<GetDailyScheduleQuery, Day>
    {
        private readonly IScheduleMaker _scheduleMaker;

        public GetDailyScheduleHandler(IScheduleMaker scheduleMaker)
        {
            _scheduleMaker = scheduleMaker;
        }

        public async Task<Day> Handle(GetDailyScheduleQuery request, CancellationToken cancellationToken)
        {
            var monthSchedule = await _scheduleMaker.GenerateMonthlySchedule(request.Year, request.Month, 2);
            var daySchedule = monthSchedule.Days.FirstOrDefault(day=> day.Date.Day == request.Day);
            return daySchedule;
        }
    }
}
