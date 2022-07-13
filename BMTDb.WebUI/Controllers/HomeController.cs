#pragma warning disable IDE0052 // Remove unread private members

using BMTDb.Service.Abstract;
using BMTDb.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BMTDb.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;

        public HomeController(ILogger<HomeController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        public IActionResult Index()
        {
            var movieViewModel = new MovieViewModel()
            {
                Movies = _movieService.GetByPopularity()
            };
            return View(movieViewModel);
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}