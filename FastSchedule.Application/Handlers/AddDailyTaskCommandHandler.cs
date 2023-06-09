using FastSchedule.Domain.Commands;
using FastSchedule.Domain.Interfaces;
using FastSchedule.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Handlers
{
    public class AddDailyTaskCommandHandler : IRequestHandler<AddDailyTaskCommand, DailyTask>
    {
        private readonly IRepository<DailyTask> _dailyTasksRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddDailyTaskCommandHandler(IRepository<DailyTask> dailyTasksRepository, IUnitOfWork unitOfWork)
        {
            _dailyTasksRepository = dailyTasksRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DailyTask> Handle(AddDailyTaskCommand request, CancellationToken cancellationToken)
        {
            await _dailyTasksRepository.AddAsync(request.DailyTask);
            await _unitOfWork.SaveChangesAsync();
            return request.DailyTask;
        }
    }
}
