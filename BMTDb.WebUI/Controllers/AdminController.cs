#pragma warning disable IDE0090 // Use 'new(...)'

using BMTDb.Entity;
using BMTDb.Service.Abstract;
using BMTDb.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

            NotificationModel msg = new NotificationModel()                 
            {
                Message = $"{entity.Title} is added",
                MessageType = "add",
                MessageIcon = "fa - solid fa - plus"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);


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

            NotificationModel msg = new NotificationModel()                 //Notification
            {
                Message = $"{entity.Title} is updated",
                MessageType = "update",
                MessageIcon = "fa-solid fa-pen"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("MovieList");
        }

        public IActionResult DeleteMovie(int MovieId)
        {
            var entity = _movieService.GetById(MovieId);

            if (entity != null)
            {
                _movieService.Delete(entity);
            }

            NotificationModel msg = new NotificationModel()                 //Notification
            {
                Message = $"{entity?.Title} is deleted",
                MessageType = "delete",
                MessageIcon = "fa - solid fa - trash - can"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("MovieList");
        }

        //Admin/Person
        public IActionResult PersonList(int page=1)
        {
            const int pageSize = 20;
            var adminItemListModel = new AdminItemListModel()
            {
                AdminPageInfo = new AdminPageInfo()
                {
                    TotalAdminItem = _personService.GetPersonCount(),
                    AdminCurrentPage = page,
                    AdminItemPerPage = pageSize,
                },
                Persons = _personService.GetPersons(page, pageSize)
            };
            return View(adminItemListModel);
        }

        [HttpGet]
        public IActionResult AddPerson()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPerson(AdminPersonModel model)
        {
            var entity = new Person()
            {
                Name = model.Name,
                Biography = model.Biography,
                PhotoUrl = model.PhotoUrl,
                Birthday = model.Birthday,
                PlaceOfBirth = model.PlaceOfBirth,
                Job = model.Job,
                Deathday = model.Deathday,
                Imdb_Id = model.Imdb_Id,
            };

            NotificationModel msg = new NotificationModel()                 //Notification
            {
                Message = $"{entity.Name} is added",
                MessageType = "add",
                MessageIcon = "fa - solid fa - plus"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            _personService.Create(entity);

            return RedirectToAction("PersonList");
        }

        [HttpGet]
        public IActionResult EditPerson(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _personService.GetById((int)id);

            var model = new AdminPersonModel()
            {
                PersonId = entity.PersonId,
                Name = entity.Name,
                Biography = entity.Biography,
                PhotoUrl = entity.PhotoUrl,
                Birthday = entity.Birthday,
                PlaceOfBirth = entity.PlaceOfBirth,
                Job = entity.Job,
                Deathday = entity.Deathday,
                Imdb_Id = entity.Imdb_Id
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult EditPerson(AdminPersonModel model)
        {
            var entity = _personService.GetById(model.PersonId);

            if (entity == null)
            {
                return NotFound();
            }

            entity.Name = model.Name;
            entity.Biography = model.Biography;
            entity.PhotoUrl = model.PhotoUrl;
            entity.Birthday = model.Birthday;
            entity.PlaceOfBirth = model.PlaceOfBirth;
            entity.Job = model.Job;
            entity.Deathday = model.Deathday;
            entity.Imdb_Id = model.Imdb_Id;

            _personService.Update(entity);

            NotificationModel msg = new NotificationModel()                 //Notification
            {
                Message = $"{entity.Name} is updated",
                MessageType = "update",
                MessageIcon = "fa-solid fa-pen"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("PersonList");
        }

        public IActionResult DeletePerson(int PersonId)
        {
            var entity = _personService.GetById(PersonId);

            if (entity != null)
            {
                _personService.Delete(entity);
            }

            NotificationModel msg = new NotificationModel()                 //Notification
            {
                Message = $"{entity?.Name} is deleted",
                MessageType = "delete",
                MessageIcon = "fa - solid fa - trash - can"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("PersonList");
        }
    }

}
