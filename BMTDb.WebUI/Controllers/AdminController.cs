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

        //Admin/Movie
        public IActionResult MovieList(int page = 1)
        {
            const int pageSize = 10;
            var adminItemListModel = new AdminItemListModel()
            {
                AdminPageInfo = new AdminPageInfo()
                {
                    TotalAdminItem = _movieService.GetMovieCount(),
                    AdminCurrentPage = page,
                    AdminItemPerPage = pageSize,
                },
                Movies = _movieService.GetMovies(page, pageSize)
            };
            return View(adminItemListModel);
        }

        [HttpGet]
        public IActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMovie(AdminMovieModel model)
        {
            var entity = new Movie()
            {
                Title = model.Title,
                Director = model.Director,
                MovieTagline = model.MovieTagline,
                MovieInfo = model.MovieInfo,
                MoviePoster = model.MoviePoster,
                MovieBackdrop = model.MovieBackdrop,
                ReleaseDate = model.ReleaseDate,
                RunTime = model.RunTime,
                Budget = model.Budget,
                MovieRatings = model.MovieRatings,
                IMDBId = model.IMDBId,
                TMDbId = model.TMDbId,
                MovieLogo = model.MovieLogo,
                Trailer = model.Trailer
            };

            _movieService.Create(entity);

            return RedirectToAction("MovieList");
        }

        [HttpGet]
        public IActionResult EditMovie(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _movieService.GetMovieDetails((int)id);

            var model = new AdminMovieModel()
            {
                MovieId = entity.MovieId,
                Title = entity.Title,
                Director = entity.Director,
                MovieTagline = entity.MovieTagline,
                MovieInfo = entity.MovieInfo,
                MoviePoster = entity.MoviePoster,
                MovieBackdrop = entity.MovieBackdrop,
                ReleaseDate = entity.ReleaseDate,
                RunTime = entity.RunTime,
                MovieRatings = entity.MovieRatings,
                Budget = entity.Budget,
                IMDBId = entity.IMDBId,
                TMDbId = entity.TMDbId,
                MovieLogo = entity.MovieLogo,
                Trailer = entity.Trailer,
                SelectedGenres = entity.MovieGenres.Select(i => i.Genre).ToList(),
                SelectedStudios = entity.MovieStudios.Select(i => i.Studios).ToList(),
                SelectedCrews = entity.MovieCrews.Select(i => i.Person).ToList()
            };

            ViewBag.Genres = _genreService.GetAll();
            ViewBag.Studios = _studioService.GetAll();
            ViewBag.Persons = _personService.GetAll();

            return View(model);
        }

        [HttpPost]
        public IActionResult EditMovie(AdminMovieModel model, int[] genreIds , int[] studioIds, int[] crewIds)
        {
            var entity = _movieService.GetMovieDetails(model.MovieId);

            if (entity == null)
            {
                return NotFound();
            }

            entity.Title = model.Title;
            entity.Director = model.Director;
            entity.MovieTagline = model.MovieTagline;
            entity.MovieInfo = model.MovieInfo;
            entity.MoviePoster = model.MoviePoster;
            entity.MovieBackdrop = model.MovieBackdrop;
            entity.ReleaseDate = model.ReleaseDate;
            entity.RunTime = model.RunTime;
            entity.MovieRatings = model.MovieRatings;
            entity.Budget = model.Budget;
            entity.IMDBId = model.IMDBId;
            entity.TMDbId = model.TMDbId;
            entity.MovieLogo = model.MovieLogo;
            entity.Trailer = model.Trailer;

            _movieService.Update(entity, genreIds, studioIds, crewIds);

            return RedirectToAction("MovieList");
        }

        public IActionResult DeleteMovie(int MovieId)
        {
            var entity = _movieService.GetById(MovieId);

            if (entity != null)
            {
                _movieService.Delete(entity);
            }

            return RedirectToAction("MovieList");
        }

    }

}
