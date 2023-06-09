using FastSchedule.Application.Queries;
using FastSchedule.Domain.Interfaces;
using FastSchedule.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Handlers
{
    internal class GetDailyTasksQueryHandler : IRequestHandler<GetDailyTasksQuery, IEnumerable<DailyTask>>
    {
        private readonly IRepository<DailyTask> _dailyTasksRepository;

        public GetDailyTasksQueryHandler(IRepository<DailyTask> dailyTasksRepository)
        {
            _dailyTasksRepository = dailyTasksRepository;
        }

        public Task<IEnumerable<DailyTask>> Handle(GetDailyTasksQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_dailyTasksRepository.Get());
        }
    }
}
