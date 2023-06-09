using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastSchedule.Domain.Models.Core;

namespace FastSchedule.Domain.Models.Tasks
{
    public abstract class BaseTask : Entity
    {
        public Guid Guid { get; set; }
        public string Label { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public string? Description { get; set; }
        public TimeOnly? EventTime { get; set; }
        public TimeSpan? PreNotifyTime { get; set; }

        public BaseTask(Guid guid, string label, int userId, TimeSpan? preNotifyTime, string? description = null, TimeOnly? eventTime = null)
        {
            Guid = guid;
            Label = label;
            UserId = userId;
            Description = description;
            EventTime = eventTime;
            PreNotifyTime = preNotifyTime;
        }

        protected BaseTask()
        {

        }
    }
}
