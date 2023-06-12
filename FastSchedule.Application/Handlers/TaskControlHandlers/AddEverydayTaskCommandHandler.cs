using AutoMapper;
using FastSchedule.Application.Commands.TaskCommands;
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
    public class AddEverydayTaskCommandHandler : IRequestHandler<AddEverydayTaskCommand, EverydayTaskDto>
    {
        private readonly IRepository<EverydayTask> _everydayTaskRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddEverydayTaskCommandHandler(IRepository<EverydayTask> everydayTaskRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _everydayTaskRepository = everydayTaskRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EverydayTaskDto> Handle(AddEverydayTaskCommand request, CancellationToken cancellationToken)
        {
            await _everydayTaskRepository.AddAsync(_mapper.Map<EverydayTask>(request.EverydayTask));
            await _unitOfWork.SaveChangesAsync();
            return request.EverydayTask;
        }
    }
}
