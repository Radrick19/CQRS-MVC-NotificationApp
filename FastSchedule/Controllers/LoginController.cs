using FastSchedule.Application.Dto;
using FastSchedule.Application.Queries;
using FastSchedule.MVC.ViewModels.LoginAndRegistration;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FastSchedule.MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            UserDto? user;
            if(await _mediator.Send(new IsUserLoginExistQuery(viewModel.EmailOrLogin)))
            {
                user = await _mediator.Send(new GetUserByLoginPasswordQuery(viewModel.EmailOrLogin, viewModel.Password));
                if(user == null)
                    ModelState.AddModelError("", "Неверное имя пользователя или пароль");
            }
            else if(await _mediator.Send(new IsUserEmailExistQuery(viewModel.EmailOrLogin)))
            {
                user = await _mediator.Send(new GetUserByEmailPasswordQuery(viewModel.EmailOrLogin, viewModel.Password));
                if (user == null)
                    ModelState.AddModelError("", "Неверное имя пользователя или пароль");
            }
            else
            {
                ModelState.AddModelError("", "Неверное имя пользователя или пароль");
                return View("Index", viewModel);
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Неверное имя пользователя или пароль");
                return View("Index", viewModel);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
