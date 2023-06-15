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
    public class AddTaskCommand : IRequest<ScheduleTaskDto>
    {
        public ScheduleTaskDto Task { get; set; }

        public AddTaskCommand(ScheduleTaskDto everydayTask)
        {
            Task = everydayTask;
        }
    }
}
