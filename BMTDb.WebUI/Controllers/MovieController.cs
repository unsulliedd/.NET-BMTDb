using BMTDb.Service.Abstract;
using BMTDb.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BMTDb.WebUI.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public IActionResult Index()
        {
            var movieViewModel = new MovieViewModel()
            {
                Movies = _movieService.GetAll()
            };
            return View(movieViewModel);
        }
    }
}