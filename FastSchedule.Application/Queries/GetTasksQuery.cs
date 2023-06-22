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
    public class GetTasksQuery : IRequest<IEnumerable<ScheduleTaskDto>>
    {
        public string UserGuid { get; set; }

        public GetTasksQuery(string userGuid)
        {
            UserGuid = userGuid;
        }
    }
}
