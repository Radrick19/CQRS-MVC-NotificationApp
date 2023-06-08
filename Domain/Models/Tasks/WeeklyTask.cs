using FastSchedule.Domain.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Domain.Models
{
    public class WeeklyTask : BaseTask
    {
        public IEnumerable<DayOfWeek> EventDaysOfWeek { get; set; }

        public WeeklyTask(string label, User user, TimeSpan? preNotifyTime, string? description = null, TimeOnly? eventTime = null, params DayOfWeek[] eventDaysOfWeek)
    : base(label, user, preNotifyTime, description, eventTime)
        {
            EventDaysOfWeek = eventDaysOfWeek;
        }
    }
}
