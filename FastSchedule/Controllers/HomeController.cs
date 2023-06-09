using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace FastSchedule.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
