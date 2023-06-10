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
    public class GetMonthlyTasksQuery : IRequest<IEnumerable<MonthlyTaskDto>>
    {
        public int UserId { get; set; }

        public GetMonthlyTasksQuery(int userId)
        {
            UserId = userId;
        }
    }
}
