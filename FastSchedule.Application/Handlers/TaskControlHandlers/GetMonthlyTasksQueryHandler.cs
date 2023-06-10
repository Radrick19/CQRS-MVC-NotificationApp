using AutoMapper;
using FastSchedule.Application.Dto;
using FastSchedule.Application.Queries.TaskQueries;
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
    public class GetMonthlyTasksQueryHandler : IRequestHandler<GetMonthlyTasksQuery, IEnumerable<MonthlyTaskDto>>
    {
        private readonly IRepository<MonthlyTask> _monthlyTasksRepository;
        private readonly IMapper _mapper;

        public GetMonthlyTasksQueryHandler(IRepository<MonthlyTask> monthlyTasksRepository, IMapper mapper)
        {
            _monthlyTasksRepository = monthlyTasksRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MonthlyTaskDto>> Handle(GetMonthlyTasksQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_monthlyTasksRepository
                .Get()
                .Where(task=> task.UserId == request.UserId)
                .Select(task=> _mapper.Map<MonthlyTaskDto>(task)));
        }
    }
}
