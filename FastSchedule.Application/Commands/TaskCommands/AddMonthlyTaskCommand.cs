using FastSchedule.Application.Dto;
using FastSchedule.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Commands.TaskCommands
{
    public class AddMonthlyTaskCommand : IRequest<MonthlyTaskDto>
    {
        public MonthlyTaskDto MonthlyTask { get; set; }

        public AddMonthlyTaskCommand(MonthlyTaskDto monthlyTask)
        {
            MonthlyTask = monthlyTask;
        }
    }
}
