using Azure.Core;
using FastSchedule.Application.Dto;
using FastSchedule.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Queries.UserQueries
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public int UserId { get; set; }

        public GetUserByIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}
