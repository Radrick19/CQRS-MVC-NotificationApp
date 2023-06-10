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
        public IEnumerable<int> EventDaysOfMonth
        {
            get { return eventDaysOfMonth; }
            set
            {
                foreach(var day in value)
                {
                    if (day < 1 || day > 31)
                    {
                        throw new ArgumentException("Wrong data for event days of month");
                    }
                }
                eventDaysOfMonth = value;
            }
        }

        private IEnumerable<int> eventDaysOfMonth;

        public MonthlyTask(Guid guid, string label, int userId, TimeSpan? preNotifyTime = null, string? description = null, TimeOnly? eventTime = null, params int[] eventDaysOfMonth)
    : base(guid, label, userId, preNotifyTime, description, eventTime)
        {
            EventDaysOfMonth = eventDaysOfMonth;
        }

        protected MonthlyTask()
        {
            
        }
    }
}
