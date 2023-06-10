using FastSchedule.Domain.Interfaces;
using FastSchedule.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastSchedule.Application.Dto
{
    public class WeeklyTaskDto : ITask
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Label { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public string? Description { get; set; }
        public TimeOnly? EventTime { get; set; }
        public TimeSpan? PreNotifyTime { get; set; }
        public IEnumerable<DayOfWeek> EventDaysOfWeek { get; set; }
    }
}
