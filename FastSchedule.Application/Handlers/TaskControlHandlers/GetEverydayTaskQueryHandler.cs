using AutoMapper;
using FastSchedule.Application.Dto;
using FastSchedule.Application.Queries.TaskQueries;
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
    public class GetEverydayTaskQueryHandler : IRequestHandler<GetEverydayTasksQuery, IEnumerable<EverydayTaskDto>>
    {
        private readonly IRepository<EverydayTask> _everyDayTaskRepository;
        private readonly IMapper _mapper;

        public GetEverydayTaskQueryHandler(IRepository<EverydayTask> everyDayTaskRepository, IMapper mapper)
        {
            _everyDayTaskRepository = everyDayTaskRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EverydayTaskDto>> Handle(GetEverydayTasksQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_everyDayTaskRepository
                .Get()
                .Where(task => task.UserId == request.UserId)
                .Select(task => _mapper.Map<EverydayTaskDto>(task)));
        }
    }
}
