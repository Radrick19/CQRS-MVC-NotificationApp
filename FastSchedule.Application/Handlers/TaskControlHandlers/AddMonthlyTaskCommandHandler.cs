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
    public class AddMonthlyTaskCommandHandler : IRequestHandler<AddMonthlyTaskCommand, MonthlyTaskDto>
    {
        private readonly IRepository<MonthlyTask> _monthlyTaskRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddMonthlyTaskCommandHandler(IRepository<MonthlyTask> monthlyTaskRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _monthlyTaskRepository = monthlyTaskRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MonthlyTaskDto> Handle(AddMonthlyTaskCommand request, CancellationToken cancellationToken)
        {
            await _monthlyTaskRepository.AddAsync(_mapper.Map<MonthlyTask>(request.MonthlyTask));
            await _unitOfWork.SaveChangesAsync();
            return request.MonthlyTask;
        }
    }
}
