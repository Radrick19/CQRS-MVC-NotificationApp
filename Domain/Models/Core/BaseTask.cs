using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Domain.Models.Core
{
    public abstract class BaseTask : Entity
    {
        public string Label { get; set; }
        public User User { get; set; }
        public string? Description { get; set; }
        public TimeOnly? EventTime { get; set; }
        public TimeSpan? PreNotifyTime { get; set; }

        public BaseTask(string label, User user, TimeSpan? preNotifyTime, string? description = null, TimeOnly? eventTime = null)
        {
            Label = label;
            User = user;
            Description = description;
            EventTime = eventTime;
            PreNotifyTime = preNotifyTime;
        }

        protected BaseTask()
        {

        }
    }
}
