using FastSchedule.Domain.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Domain.Models
{
    public class MonthlyTask : BaseTask
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

        public MonthlyTask(string label, User user, TimeSpan? preNotifyTime, string? description = null, TimeOnly? eventTime = null, params int[] eventDaysOfMonth)
    : base(label, user, preNotifyTime, description, eventTime)
        {
            EventDaysOfMonth = eventDaysOfMonth;
        }
    }
}
