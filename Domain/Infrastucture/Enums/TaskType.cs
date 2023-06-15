using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Domain.Infrastucture.Enums
{
    public enum TaskType
    {
        Onetime = 0,
        Daily,
        Weekly,
        Monthly,
        Yearly
    }
}
