using Microsoft.AspNetCore.Mvc;

namespace AnimalWorld.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
