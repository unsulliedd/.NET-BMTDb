using Microsoft.AspNetCore.Mvc;

namespace BMTDb.WebUI.Controllers
{
    public class TvShowController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
