using Microsoft.AspNetCore.Mvc;

namespace FastSchedule.MVC.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
