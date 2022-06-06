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

        public IActionResult Index(string genre, string studio, int page = 1)
        {
            const int pageSize = 20;
            var movieViewModel = new MovieViewModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItem = _movieService.GetCountbyFilter(genre, studio),
                    CurrentPage = page,
                    ItemPerPage = pageSize,
                    CurrentGenre = genre,
                    CurrentStudio = studio
                },
                Movies = _movieService.GetMoviebyFilter(genre, studio, page, pageSize)
            };
            return View(movieViewModel);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }

            Movie movies = _movieService.GetMovieDetails((int)id);

            if (id == null)
            {
                return View("Error");
            }
            return View(new MovieDetailModel
            {
                Movie = movies,
                Genres =  movies.MovieGenres.Select(i => i.Genre).ToList(),
                Studios = movies.MovieStudios.Select(i => i.Studios).ToList(),
                Persons = movies.MovieCrews.Select(i => i.Person).ToList()
            });
        }
        public IActionResult Search(string q)
        {
            var movieViewModel = new MovieViewModel()
            {
                Movies = _movieService.GetSearchResult(q)
            };

            return View(movieViewModel);
        }
    }
}