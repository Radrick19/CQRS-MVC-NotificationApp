using AutoMapper;
using FastSchedule.Application.Dto;
using FastSchedule.Application.Queries.TaskQueries;
using FastSchedule.Domain.Interfaces;
using FastSchedule.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Handlers.TaskControlHandlers
{
    internal class GetDailyTasksQueryHandler : IRequestHandler<GetDailyTasksQuery, IEnumerable<OnetimeTaskDto>>
    {
        private readonly IRepository<OnetimeTask> _dailyTasksRepository;
        private readonly IMapper _mapper;

        public GetDailyTasksQueryHandler(IRepository<OnetimeTask> dailyTasksRepository, IMapper mapper)
        {
            _dailyTasksRepository = dailyTasksRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OnetimeTaskDto>> Handle(GetDailyTasksQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_dailyTasksRepository
                .Get()
                .Where(task=> task.UserId == request.UserId)
                .Select(task => _mapper.Map<OnetimeTaskDto>(task)));
        }
    }
}
