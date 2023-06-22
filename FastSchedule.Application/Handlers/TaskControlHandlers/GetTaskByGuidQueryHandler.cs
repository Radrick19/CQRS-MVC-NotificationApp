using AutoMapper;
using FastSchedule.Application.Dto;
using FastSchedule.Application.Queries;
using FastSchedule.Domain.Interfaces;
using FastSchedule.Domain.Models.Tasks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Handlers.TaskControlHandlers
{
    public class GetTaskByGuidQueryHandler : IRequestHandler<GetTaskByGuidQuery, ScheduleTask>
    {
        private readonly IRepository<ScheduleTask> _taskRepository;
        private readonly IMapper _mapper;

        public GetTaskByGuidQueryHandler(IRepository<ScheduleTask> taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<ScheduleTask> Handle(GetTaskByGuidQuery request, CancellationToken cancellationToken)
        {
            return await _taskRepository.FirstOrDefaultAsync(task=> task.Guid == request.Guid && task.User.Guid == new Guid(request.UserGuid));
        }
    }
}
