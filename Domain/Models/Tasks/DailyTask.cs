using FastSchedule.Domain.Models.Core;

namespace FastSchedule.Domain.Models
{
    public class DailyTask : BaseTask
    {
        public DateOnly EventDay { get; set; }

        public DailyTask(string label, User user, DateOnly eventDay, TimeSpan? preNotifyTime, string? description = null, TimeOnly? eventTime = null)
            : base(label, user, preNotifyTime, description, eventTime)
        {
            EventDay = eventDay;
        }
    }
}
