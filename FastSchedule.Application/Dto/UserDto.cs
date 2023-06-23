using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
