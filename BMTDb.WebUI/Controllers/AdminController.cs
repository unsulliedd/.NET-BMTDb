using BMTDb.Entity;
using BMTDb.Service.Abstract;
using BMTDb.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BMTDb.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        private readonly IStudioService _studioService;
        private readonly IPersonService _personService;

        public AdminController(IMovieService movieService, IPersonService personService,
            IGenreService genreService, IStudioService studioService)
        {
            _movieService = movieService;
            _personService = personService;
            _genreService = genreService;
            _studioService = studioService;
        }

        public IActionResult AdminDashboard()
        {
            return View(new AdminItemListModel()
            {
                Movies = _movieService.GetAll(),
                Genres = _genreService.GetAll(),
                Studios = _studioService.GetAll(),
                Persons = _personService.GetAll(),
            });
        }
    }
}