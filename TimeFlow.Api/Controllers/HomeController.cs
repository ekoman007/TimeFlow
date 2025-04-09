using Microsoft.AspNetCore.Mvc;

namespace TimeFlow.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
