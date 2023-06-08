using FastSchedule.Domain.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Domain.Models
{
    public class User : Entity
    {
        public Guid Guid { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public User(Guid guid, string login, string email, string password, string salt)
        {
            Guid = guid;
            Login = login;
            Email = email;
            Password = password;
            Salt = salt;
        }

        protected User()
        {

        }
    }
}
