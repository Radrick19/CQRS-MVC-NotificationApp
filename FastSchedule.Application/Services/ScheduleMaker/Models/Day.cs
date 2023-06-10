using FastSchedule.Domain.Interfaces;
using FastSchedule.Domain.Models.Tasks;

namespace FastSchedule.Application.Services.ScheduleMaker.Models
{
    public class Day
    {
        public DateOnly Date { get; set; }
        public List<ITask>? Tasks { get; set; }
    }
}
