using BMTDb.Entity;
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

        public IActionResult Index(string genre, string studio)
        {
            var movieViewModel = new MovieViewModel()
            {
                Movies = _movieService.GetMoviebyFilter(genre, studio)
            };
            return View(movieViewModel);
        }

        public IActionResult Details(string? Title)
        {
            if (Title == null)
            {
                return View("Error");
            }

            Movie movie = _movieService.GetMovieDetails(Title);

            if (Title == null)
            {
                return View("Error");
            }
            return View(new MovieDetailModel
            {
                Movie = movie,
                Genres = movie.MovieGenres.Select(i => i.Genre).ToList(),
                Studios = movie.MovieStudios.Select(i => i.Studios).ToList(),
                Persons = movie.MovieCrews.Select(i => i.Person).ToList()
            });
        }
    }
}