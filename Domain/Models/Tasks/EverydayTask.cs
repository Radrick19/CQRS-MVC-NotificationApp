using FastSchedule.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Domain.Models.Tasks
{
    public class EverydayTask : BaseTask, ITask
    {
        public EverydayTask(Guid guid, string label, int userId, TimeSpan? preNotifyTime = null, string? description = null, TimeOnly? eventTime = null)
            : base(guid, label, userId, preNotifyTime, description, eventTime)
        {

        }

        protected EverydayTask()
        {
            
        }
    }
}
