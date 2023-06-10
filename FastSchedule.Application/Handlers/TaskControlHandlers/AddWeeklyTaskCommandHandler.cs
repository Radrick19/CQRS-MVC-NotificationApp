using AutoMapper;
using FastSchedule.Application.Commands.TaskCommands;
using FastSchedule.Application.Dto;
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
    public class AddWeeklyTaskCommandHandler : IRequestHandler<AddWeeklyTaskCommand, WeeklyTaskDto>
    {
        private readonly IRepository<WeeklyTask> _weeklyTaskRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddWeeklyTaskCommandHandler(IRepository<WeeklyTask> weeklyTaskRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _weeklyTaskRepository = weeklyTaskRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<WeeklyTaskDto> Handle(AddWeeklyTaskCommand request, CancellationToken cancellationToken)
        {
            await _weeklyTaskRepository.AddAsync(_mapper.Map<WeeklyTask>(request.WeeklyTask));
            await _unitOfWork.SaveChangesAsync();
            return request.WeeklyTask;
        }
    }
}
