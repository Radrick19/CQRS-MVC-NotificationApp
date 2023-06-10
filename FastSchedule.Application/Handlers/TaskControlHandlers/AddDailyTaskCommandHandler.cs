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
    public class AddDailyTaskCommandHandler : IRequestHandler<AddDailyTaskCommand, DailyTaskDto>
    {
        private readonly IRepository<DailyTask> _dailyTasksRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddDailyTaskCommandHandler(IRepository<DailyTask> dailyTasksRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _dailyTasksRepository = dailyTasksRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DailyTaskDto> Handle(AddDailyTaskCommand request, CancellationToken cancellationToken)
        {
            await _dailyTasksRepository.AddAsync(_mapper.Map<DailyTask>(request.DailyTask));
            await _unitOfWork.SaveChangesAsync();
            return request.DailyTask;
        }
    }
}
