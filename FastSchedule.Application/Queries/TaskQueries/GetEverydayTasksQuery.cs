using FastSchedule.Application.Dto;
using FastSchedule.Domain.Models.Tasks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Queries.TaskQueries
{
    public class GetEverydayTasksQuery : IRequest<IEnumerable<EverydayTaskDto>>
    {
        public int UserId { get; set; }

        public GetEverydayTasksQuery(int userId)
        {
            UserId = userId;
        }
    }
}
