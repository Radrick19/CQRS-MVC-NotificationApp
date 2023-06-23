using FastSchedule.Application.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Commands
{
    public class AddUserCommand : IRequest<UserDto>
    {
        public AddUserCommand(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
