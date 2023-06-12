using FastSchedule.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Domain.Interfaces
{
    public interface ITask
    {
        Guid Guid { get; set; }
        string Color { get; set; }
        string Label { get; set; }
        int UserId { get; set; }
        [ForeignKey("UserId")]
        User User { get; set; }
        string? Description { get; set; }
        TimeOnly? EventTime { get; set; }
        TimeSpan? PreNotifyTime { get; set; }
    }
}
