using FastSchedule.Application.Dto;
using FastSchedule.Domain.Models.Tasks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Queries
{
    public class GetTaskByGuidQuery : IRequest<ScheduleTask>
    {
        public Guid Guid { get; set; }
        public string UserGuid { get; set; }

        public GetTaskByGuidQuery(Guid guid, string userGuid)
        {
            Guid = guid;
            UserGuid = userGuid;
        }
    }
}
