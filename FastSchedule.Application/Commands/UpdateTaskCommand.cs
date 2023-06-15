using FastSchedule.Application.Dto;
using FastSchedule.Domain.Models.Tasks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Commands
{
    public class UpdateTaskCommand : IRequest<ScheduleTask>
    {
        public ScheduleTask Task { get; set; }

        public UpdateTaskCommand(ScheduleTask task)
        {
            Task = task;
        }

    }
}
