using Microsoft.AspNetCore.Mvc;

namespace BMTDb.WebUI.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
