using FastSchedule.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Domain.Commands
{
    public class AddDailyTaskCommand : IRequest<DailyTask>
    {
        public DailyTask DailyTask { get; set; }

        public AddDailyTaskCommand(DailyTask dailyTask)
        {
            DailyTask = dailyTask;
        }
    }
}
