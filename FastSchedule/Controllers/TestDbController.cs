using FastSchedule.Application.Queries;
using FastSchedule.Domain.Commands;
using FastSchedule.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FastSchedule.MVC.Controllers
{
    [Route("api/[controller]")]
    public class TestDbController : Controller
    {
        private readonly IMediator _mediator;

        public TestDbController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<DailyTask> TestMethod()
        {
            var command = new AddDailyTaskCommand(new DailyTask(Guid.NewGuid(), "NewTestLabel", 1, new DateOnly(23, 6, 9)));
            var addedTask = await _mediator.Send(command);
            return addedTask;
        }
        [HttpGet]
        public async Task<IEnumerable<DailyTask>> TestMethodGet()
        {
            var query = new GetDailyTasksQuery();
            var tasks = await _mediator.Send(query);
            return tasks;
        }
    }
}
