using FastSchedule.Application.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Queries
{
    public class GetUserByLoginPasswordQuery : IRequest<UserDto?>
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public GetUserByLoginPasswordQuery(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
