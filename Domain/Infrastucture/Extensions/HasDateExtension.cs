using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Domain.Infrastucture.Extensions
{
    public static class HasDateExtension
    {
        public static bool HasDate(this IEnumerable<DateOnly> enumerable, DateOnly lookingDate)
        {
            if (enumerable != null)
            {
                foreach (var date in enumerable)
                {
                    if (lookingDate.Equals(date))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
