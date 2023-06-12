using FastSchedule.Domain.Interfaces;
using FastSchedule.Domain.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Domain.Models
{
    public class MonthlyTask : BaseTask, ITask
    {
        public int EventDayOfMonth
        {
            get { return eventDayOfMonth; }
            set
            {
                if (value < 1 || value > 31)
                {
                    throw new ArgumentException("Wrong data for event days of month");
                }
                eventDayOfMonth = value;
            }
        }

        private int eventDayOfMonth;

        public MonthlyTask(Guid guid, string label, int userId, int eventDayOfMonth, TimeSpan? preNotifyTime = null, string? description = null, TimeOnly? eventTime = null)
    : base(guid, label, userId, preNotifyTime, description, eventTime)
        {
            EventDayOfMonth = eventDayOfMonth;
        }

        protected MonthlyTask()
        {
            
        }
    }
}
