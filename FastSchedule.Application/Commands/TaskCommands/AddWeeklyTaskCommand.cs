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
    public class AddWeeklyTaskCommand : IRequest<WeeklyTaskDto>
    {
        public WeeklyTaskDto WeeklyTask { get; set; }

        public AddWeeklyTaskCommand(WeeklyTaskDto weeklyTask)
        {
            WeeklyTask = weeklyTask;
        }
    }
}
