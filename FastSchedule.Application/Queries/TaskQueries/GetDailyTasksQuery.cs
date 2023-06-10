using FastSchedule.Application.Dto;
using FastSchedule.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Queries.TaskQueries
{
    public class GetDailyTasksQuery : IRequest<IEnumerable<DailyTaskDto>>
    {
        public int UserId { get; set; }

        public GetDailyTasksQuery(int userId)
        {
            UserId = userId;
        }
    }
}
