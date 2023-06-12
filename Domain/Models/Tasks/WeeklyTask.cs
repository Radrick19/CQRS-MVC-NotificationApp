using FastSchedule.Domain.Interfaces;
using FastSchedule.Domain.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Domain.Models
{
    public class WeeklyTask : BaseTask, ITask
    {
        public DayOfWeek EventDayOfWeek { get; set; }

        public WeeklyTask(Guid guid, string label, int userId, DayOfWeek eventDayOfWeek, TimeSpan? preNotifyTime = null, string? description = null, TimeOnly? eventTime = null)
            : base(guid, label, userId, preNotifyTime, description, eventTime)
        {
            EventDayOfWeek = eventDayOfWeek;
        }

        protected WeeklyTask()
        {
            
        }
    }
}
