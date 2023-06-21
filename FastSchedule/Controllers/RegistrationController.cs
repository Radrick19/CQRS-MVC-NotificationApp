using FastSchedule.Application.Commands;
using FastSchedule.Application.Queries;
using FastSchedule.MVC.ViewModels.LoginAndRegistration;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FastSchedule.MVC.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IMediator _mediator;

        public RegistrationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel viewModel)
        {
            if(!ModelState.IsValid || !await ValidateData(viewModel))
            {
                return View("Index",viewModel);
            }
            await _mediator.Send(new AddUserCommand(viewModel.Login, viewModel.Email, viewModel.Password));
            return Ok();
        }

        private async Task<bool> ValidateData(RegistrationViewModel viewModel)
        {
            // Логин уже занят
            if(await _mediator.Send(new IsUserLoginExistQuery(viewModel.Login)))
            {
                ModelState.AddModelError(nameof(viewModel.Login), "Данный логин уже занят");
            }
            // Почта уже занята
            if(await _mediator.Send(new IsUserEmailExistQuery(viewModel.Email))) 
            {
                ModelState.AddModelError(nameof(viewModel.Email), "Данная почта уже занята");
            }
            return ModelState.IsValid;
        }
    }
}
