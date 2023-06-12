using FastSchedule.Domain.Interfaces;
using FastSchedule.Domain.Models.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FastSchedule.Domain.Models
{
    public class OnetimeTask : BaseTask, ITask
    {
        public DateOnly EventDay { get; set; }

        public OnetimeTask(Guid guid, string label, int userId, DateOnly eventDay, TimeSpan? preNotifyTime = null, string? description = null, TimeOnly? eventTime = null)
            : base(guid, label, userId, preNotifyTime, description, eventTime)
        {
            EventDay = eventDay;
        }

        protected OnetimeTask()
        {
            
        }
    }
}
