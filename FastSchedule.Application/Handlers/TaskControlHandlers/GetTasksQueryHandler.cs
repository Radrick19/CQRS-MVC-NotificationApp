using AutoMapper;
using FastSchedule.Application.Dto;
using FastSchedule.Application.Queries;
using FastSchedule.Domain.Interfaces;
using FastSchedule.Domain.Models;
using FastSchedule.Domain.Models.Tasks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Handlers.TaskControlHandlers
{
    internal class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, IEnumerable<ScheduleTaskDto>>
    {
        private readonly IRepository<ScheduleTask> _tasksRepository;
        private readonly IMapper _mapper;

        public GetTasksQueryHandler(IRepository<ScheduleTask> dailyTasksRepository, IMapper mapper)
        {
            _tasksRepository = dailyTasksRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ScheduleTaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_tasksRepository
                .Get()
                .Where(task=> task.UserId == request.UserId)
                .Select(task => _mapper.Map<ScheduleTaskDto>(task)));
        }
    }
}
