using FastSchedule.Domain.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Services.Models
{
    public class Day
    {
        public DateOnly Date { get; set; }
        public IEnumerable<BaseTask>? Tasks { get; set; }

        public Day(DateOnly date, IEnumerable<BaseTask> tasks = null)
        {
            Date = date;
            Tasks = tasks;
        }
    }
}
