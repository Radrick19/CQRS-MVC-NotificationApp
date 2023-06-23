using Azure.Core;
using FastSchedule.Application.Dto;
using FastSchedule.Application.Queries;
using FastSchedule.MVC.ViewModels.LoginAndRegistration;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
            UserDto? user = null;
            if(await _mediator.Send(new IsUserLoginExistQuery(viewModel.Login)))
                user = await _mediator.Send(new GetUserByLoginPasswordQuery(viewModel.Login, viewModel.Password));

            if (user == null)
            {
                ModelState.AddModelError("", "Неверное имя пользователя или пароль");
                return View("Index", viewModel);
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Sid, user.Guid.ToString())
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "Cookies");
            await HttpContext.SignInAsync(new ClaimsPrincipal(identity));
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var route = HttpContext.Request.Headers["Referer"].ToString();
            return Redirect("/login");
        }
    }
}
