﻿using System;
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
        public IEnumerable<DateOnly>? CompletedDays { get; set; } = null;
        public IEnumerable<DateOnly>? DeletedDays { get; set; } = null;
        public bool IsDeleted { get; set; } = false;
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public string? Description { get; set; }
        public TimeOnly? EventTime { get; set; }
        public TimeSpan? PreNotifyTime { get; set; }

        protected ScheduleTask()
        {

        }
    }
}
