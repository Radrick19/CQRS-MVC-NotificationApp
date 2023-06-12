using FastSchedule.Application.Commands.TaskCommands;
using FastSchedule.Application.Dto;
using FastSchedule.Application.Queries.TaskQueries;
using FastSchedule.Application.Services.ScheduleMaker;
using FastSchedule.Application.Services.ScheduleMaker.Models;
using FastSchedule.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;

namespace FastSchedule.MVC.Controllers
{
    [Route("api/[controller]")]
    public class TestDbController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IScheduleMaker _scheduleMaker;

        public TestDbController(IMediator mediator, IScheduleMaker scheduleMaker)
        {
            _mediator = mediator;
            _scheduleMaker = scheduleMaker;
        }

        //[HttpPost]
        //public async Task<OnetimeTaskDto> TestMethod()
        //{
        //    var command = new AddDailyTaskCommand(new OnetimeTaskDto{
        //        Guid = Guid.NewGuid(),
        //        Label = "newTesik",
        //        UserId = 1,
        //        EventDay = new DateOnly(2023, 6, 9)
        //    });
        //    var addedTask = await _mediator.Send(command);
        //    return addedTask;
        //}

        [HttpPost]
        public async Task<UserDto> AddUser()
        {
            var command = new AddUserCommand("radrick", "radricksh@gmail.com", "259");
            var addedTask = await _mediator.Send(command);
            return addedTask;
        }

        [HttpGet]
        public async Task<IEnumerable<OnetimeTaskDto>> TestMethodGet()
        {
            var query = new GetDailyTasksQuery(1);
            var tasks = await _mediator.Send(query);
            return tasks;
        }

        [HttpGet]
        [Route("MakeSchedule")]
        public async Task<Schedule> MakeSchedule()
        {
            return await _scheduleMaker.GenerateMonthlySchedule(2023, 6, 1);
        }
    }
}
