using FastSchedule.Application.Dto;
using FastSchedule.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Queries
{
    public class GetUserByGuidQuery : IRequest<UserDto>
    {
        public string UserGuid { get; set; }

        public GetUserByGuidQuery(string userGuid)
        {
            UserGuid = userGuid;
        }
    }
}
