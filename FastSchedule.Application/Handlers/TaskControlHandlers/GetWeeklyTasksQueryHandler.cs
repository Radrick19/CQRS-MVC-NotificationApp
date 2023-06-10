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
    public class GetWeeklyTasksQueryHandler : IRequestHandler<GetWeeklyTasksQuery, IEnumerable<WeeklyTaskDto>>
    {
        private readonly IRepository<WeeklyTask> _weeklyTasksRepository;
        private readonly IMapper _mapper;

        public GetWeeklyTasksQueryHandler(IRepository<WeeklyTask> weeklyTasksRepository, IMapper mapper)
        {
            _weeklyTasksRepository = weeklyTasksRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WeeklyTaskDto>> Handle(GetWeeklyTasksQuery request, CancellationToken cancellationToken)
        {
           return await Task.FromResult(_weeklyTasksRepository
               .Get()
               .Where(task=>task.UserId == request.UserId)
               .Select(task=> _mapper.Map<WeeklyTaskDto>(task)));
        }
    }
}
