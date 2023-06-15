using AutoMapper;
using FastSchedule.Application.Commands;
using FastSchedule.Application.Dto;
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
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, ScheduleTask>
    {
        private readonly IRepository<ScheduleTask> _taskRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTaskCommandHandler(IRepository<ScheduleTask> taskRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ScheduleTask> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            _taskRepository.Update(request.Task);
            await _unitOfWork.SaveChangesAsync();
            return request.Task;
        }
    }
}
