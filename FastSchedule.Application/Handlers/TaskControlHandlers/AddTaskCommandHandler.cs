using AutoMapper;
using FastSchedule.Application.Commands;
using FastSchedule.Application.Dto;
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
    public class AddTaskCommandHandler : IRequestHandler<AddTaskCommand, ScheduleTaskDto>
    {
        private readonly IRepository<ScheduleTask> _tasksRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddTaskCommandHandler(IRepository<ScheduleTask> dailyTasksRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _tasksRepository = dailyTasksRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ScheduleTaskDto> Handle(AddTaskCommand request, CancellationToken cancellationToken)
        {
            await _tasksRepository.AddAsync(_mapper.Map<ScheduleTask>(request.Task));
            await _unitOfWork.SaveChangesAsync();
            return request.Task;
        }
    }
}
