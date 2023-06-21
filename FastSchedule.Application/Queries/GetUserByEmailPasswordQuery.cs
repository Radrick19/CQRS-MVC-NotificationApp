using FastSchedule.Application.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Queries
{
    public class GetUserByEmailPasswordQuery : IRequest<UserDto?>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public GetUserByEmailPasswordQuery(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
