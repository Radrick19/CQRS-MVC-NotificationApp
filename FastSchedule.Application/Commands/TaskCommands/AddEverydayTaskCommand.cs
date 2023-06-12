using FastSchedule.Application.Dto;
using FastSchedule.Domain.Models.Tasks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Commands.TaskCommands
{
    public class AddEverydayTaskCommand : IRequest<EverydayTaskDto>
    {
        public EverydayTaskDto EverydayTask { get; set; }

        public AddEverydayTaskCommand(EverydayTaskDto everydayTask)
        {
            EverydayTask = everydayTask;
        }
    }
}
