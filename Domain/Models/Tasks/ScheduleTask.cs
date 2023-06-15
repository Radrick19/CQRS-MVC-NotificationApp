using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastSchedule.Domain.Infrastucture.Enums;
using FastSchedule.Domain.Models.Core;

namespace FastSchedule.Domain.Models.Tasks
{
    public class ScheduleTask : Entity
    {
        public Guid Guid { get; set; }
        public string Color { get; set; }
        public string Label { get; set; }
        public int UserId { get; set; }
        public TaskType TaskType { get; set; }
        public RemindType RemindType { get; set; }
        public DateOnly EventDate { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public string? Description { get; set; }
        public TimeOnly? EventTime { get; set; }
        public TimeSpan? PreNotifyTime { get; set; }

        public ScheduleTask(Guid guid, string color, string label, int userId, TaskType taskType, DateOnly eventDate, RemindType remindType,
            string? description = null, TimeOnly? eventTime = null, TimeSpan? preNotifyTime = null)
        {
            Guid = guid;
            Color = color;
            Label = label;
            UserId = userId;
            TaskType = taskType;
            EventDate = eventDate;
            Description = description;
            EventTime = eventTime;
            PreNotifyTime = preNotifyTime;
            RemindType = remindType;
        }

        protected ScheduleTask()
        {

        }
    }
}
