using BMTDb.Entity;
using BMTDb.Service.Abstract;
using BMTDb.WebUI.Extensions;
using BMTDb.WebUI.Identity;
using BMTDb.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BMTDb.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        private readonly IStudioService _studioService;
        private readonly IPersonService _personService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public AdminController(IMovieService movieService, IPersonService personService, 
            IGenreService genreService, IStudioService studioService,
            RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _movieService = movieService;
            _personService = personService;
            _genreService = genreService;
            _studioService = studioService;
            _roleManager = roleManager;
            _userManager = userManager;
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
            if (ModelState.IsValid)
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

                if (_movieService.Create(entity))
                {
                    TempData.Put("message", new NotificationModel
                    {
                        Message = $"\"{entity.Title}\" is added",
                        MessageType = "add",
                        MessageIcon = "fa - solid fa - plus"
                    });
                    return RedirectToAction("MovieList");
                }
                TempData.Put("message", new NotificationModel
                {
                    Message = _movieService.ErrorMessage,
                    MessageType = "error",
                    MessageIcon = "fa - solid fa - exclamation"
                });

            }
            return View(model);
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
            };

            ViewBag.Genres = _genreService.GetAll();
            ViewBag.Studios = _studioService.GetAll();

            return View(model);
        }

        [HttpPost]
        public IActionResult EditMovie(AdminMovieModel model, int[] genreIds , int[] studioIds, int[] crewIds)
        {
            if (ModelState.IsValid)
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

                if(_movieService.Update(entity, genreIds, studioIds, crewIds))
                {
                    TempData.Put("message", new NotificationModel
                    {
                        Message = $"\"{entity.Title}\" is updated",
                        MessageType = "update",
                        MessageIcon = "fa-solid fa-pen"
                    });
                    return RedirectToAction("MovieList");
                }
                TempData.Put("message", new NotificationModel
                {
                    Message = _movieService.ErrorMessage,
                    MessageType = "error",
                    MessageIcon = "fa - solid fa - exclamation"
                });
            }
            ViewBag.Genres = _genreService.GetAll();
            ViewBag.Studios = _studioService.GetAll();
            return View(model);
        }

        public IActionResult DeleteMovie(int MovieId)
        {
            var entity = _movieService.GetById(MovieId);

            if (entity != null)
            {
                _movieService.Delete(entity);
            }
            TempData.Put("message", new NotificationModel
            {
                Message = $"\"{entity?.Title}\" is deleted",
                MessageType = "delete",
                MessageIcon = "fa-solid fa-trash-can"
            });
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
            if (ModelState.IsValid)
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

                if (_personService.Create(entity))
                {
                    TempData.Put("message", new NotificationModel
                    {
                        Message = $"\"{entity.Name}\" is added",
                        MessageType = "add",
                        MessageIcon = "fa - solid fa - plus"
                    });
                    return RedirectToAction("PersonList");
                }
                TempData.Put("message", new NotificationModel
                {
                    Message = _personService.ErrorMessage,
                    MessageType = "error",
                    MessageIcon = "fa - solid fa - exclamation"
                });
            }
            return View(model);
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
            if (ModelState.IsValid)
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

                TempData.Put("message", new NotificationModel
                {
                    Message = $"{entity.Name} is updated",
                    MessageType = "update",
                    MessageIcon = "fa-solid fa-pen"
                });
                return RedirectToAction("PersonList");
            }
            return View(model);
        }

        public IActionResult DeletePerson(int PersonId)
        {
            var entity = _personService.GetById(PersonId);

            if (entity != null)
            {
                _personService.Delete(entity);
            }

            TempData.Put("message", new NotificationModel
            {
                Message = $"{entity?.Name} is deleted",
                MessageType = "delete",
                MessageIcon = "fa - solid fa - trash - can"
            });
            return RedirectToAction("PersonList");
        }

        //Roles
        public IActionResult RoleList()
        {
            return View(_roleManager.Roles);
        }

        [HttpGet]
        public IActionResult RoleCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoleCreate(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(model.Name));
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }

                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> RoleEdit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            var members = new List<User>();
            var nonmembers = new List<User>();

            var userList = await _userManager.Users.ToListAsync();

            foreach (var user in userList)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    members.Add(user);
                }
            
                else
                {
                    nonmembers.Add(user);
                }
            }

            var model = new RoleDetails()
            {
                Role = role,
                Members = members,
                NonMembers = nonmembers
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoleEdit(RoleEditModel model)
        {
            if (ModelState.IsValid) { 
                foreach (var userId in model.IdsToAdd ?? Array.Empty<string>())
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if (result.Succeeded)
                        {
                            TempData.Put("message", new NotificationModel
                            {
                                Message = "Role Successfully Added",
                                MessageType = "success",
                                MessageIcon = "fa-solid fa-check"
                            });
                        }
                    }
                }

                foreach (var userId in model.IdsToDelete ?? Array.Empty<string>())
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (result.Succeeded)
                        {
                            TempData.Put("message", new NotificationModel
                            {
                                Message = "Role Successfully Removed",
                                MessageType = "success",
                                MessageIcon = "fa-solid fa-check"
                            });
                        }
                    }
                }
            }
            return Redirect("/admin/role/" + model.RoleId);
        }

    }
}
