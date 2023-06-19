using FastSchedule.MVC.ViewModels.LoginAndRegistration;
using Microsoft.AspNetCore.Mvc;

namespace FastSchedule.MVC.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register(RegistrationViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return View("Index",viewModel);
            }
            throw new NotImplementedException();
        }

        private bool ValidateData(RegistrationViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}
