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
    public class AddDailyTaskCommandHandler : IRequestHandler<AddDailyTaskCommand, OnetimeTaskDto>
    {
        private readonly IRepository<OnetimeTask> _dailyTasksRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddDailyTaskCommandHandler(IRepository<OnetimeTask> dailyTasksRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _dailyTasksRepository = dailyTasksRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OnetimeTaskDto> Handle(AddDailyTaskCommand request, CancellationToken cancellationToken)
        {
            await _dailyTasksRepository.AddAsync(_mapper.Map<OnetimeTask>(request.DailyTask));
            await _unitOfWork.SaveChangesAsync();
            return request.DailyTask;
        }
    }
}
