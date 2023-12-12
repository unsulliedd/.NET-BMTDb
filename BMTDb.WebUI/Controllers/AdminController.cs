#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.

using BMTDb.Entity;
using BMTDb.Service.Abstract;
using BMTDb.WebUI.Extensions;
using BMTDb.WebUI.Identity;
using BMTDb.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BMTDb.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        private readonly IProductionCompanyService _studioService;
        private readonly IPersonService _personService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;


        public AdminController(IMovieService movieService, IPersonService personService, 
            IGenreService genreService, IProductionCompanyService studioService,
            RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IConfiguration configuration)
        {
            _movieService = movieService;
            _personService = personService;
            _genreService = genreService;
            _studioService = studioService;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public IActionResult AdminDashboard()
        {
            return View(new AdminItemListModel()
            {
                Movies = _movieService.GetAll(),
                Genres = _genreService.GetAll(),
                ProductionCompanies = _studioService.GetAll(),
                Persons = _personService.GetAll(),
            });
        }

        //Admin/Movie
        public IActionResult MovieList(int page = 1)
        {
            const int pageSize = 20;
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
        [ValidateAntiForgeryToken]
        public IActionResult AddMovie(AdminMovieModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Movie()
                {
                    Title = model.Title,
                    Director = model.Director,
                    Tagline = model.MovieTagline,
                    Info = model.MovieInfo,
                    Poster = model.MoviePoster,
                    Backdrop = model.MovieBackdrop,
                    ReleaseDate = model.ReleaseDate,
                    RunTime = model.RunTime,
                    Budget = model.Budget,
                    Ratings = model.MovieRatings,
                    IMDBId = model.IMDBId,
                    TMDbId = model.TMDbId,
                    Logo = model.MovieLogo,
                    Trailer = model.Trailer
                };

                if (_movieService.Create(entity))
                {
                    TempData.Put("message", new NotificationModel
                    {
                        Message = $"\"{entity.Title}\" is added",
                        MessageType = "add",
                        MessageIcon = "fa-solid fa-plus"
                    });
                    return RedirectToAction("MovieList");
                }
                TempData.Put("message", new NotificationModel
                {
                    Message = _movieService.ErrorMessage,
                    MessageType = "error",
                    MessageIcon = "fa-solid fa-exclamation"
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
                MovieTagline = entity.Tagline,
                MovieInfo = entity.Info,
                MoviePoster = entity.Poster,
                MovieBackdrop = entity.Backdrop,
                ReleaseDate = entity.ReleaseDate,
                RunTime = entity.RunTime,
                MovieRatings = entity.Ratings,
                Budget = entity.Budget,
                IMDBId = entity.IMDBId,
                TMDbId = entity.TMDbId,
                MovieLogo = entity.Logo,
                Trailer = entity.Trailer,
                SelectedGenres = entity.MovieGenres.Select(i => i.Genre).ToList(),
                SelectedStudios = entity.MovieProductionCompanies.Select(i => i.ProductionCompanies).ToList(),
            };

            ViewBag.Genres = _genreService.GetAll();
            ViewBag.Studios = _studioService.GetAll();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                entity.Tagline = model.MovieTagline;
                entity.Info = model.MovieInfo;
                entity.Poster = model.MoviePoster;
                entity.Backdrop = model.MovieBackdrop;
                entity.ReleaseDate = model.ReleaseDate;
                entity.RunTime = model.RunTime;
                entity.Ratings = model.MovieRatings;
                entity.Budget = model.Budget;
                entity.IMDBId = model.IMDBId;
                entity.TMDbId = model.TMDbId;
                entity.Logo = model.MovieLogo;
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
        [ValidateAntiForgeryToken]
        public IActionResult AddPerson(AdminPersonModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Person()
                {
                    Name = model.Name,
                    Biography = model.Biography,
                    ProfilePicture = model.PhotoUrl,
                    Birthday = model.Birthday,
                    Place_of_Birth = model.PlaceOfBirth,
                    Known_for_Department = model.Job,
                    Deathday = model.Deathday,
                    ImdbId = model.Imdb_Id,
                };

                if (_personService.Create(entity))
                {
                    TempData.Put("message", new NotificationModel
                    {
                        Message = $"\"{entity.Name}\" is added",
                        MessageType = "add",
                        MessageIcon = "fa-solid fa-plus"
                    });
                    return RedirectToAction("PersonList");
                }
                TempData.Put("message", new NotificationModel
                {
                    Message = _personService.ErrorMessage,
                    MessageType = "error",
                    MessageIcon = "fa-solid fa-exclamation"
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
                PhotoUrl = entity.ProfilePicture,
                Birthday = entity.Birthday,
                PlaceOfBirth = entity.Place_of_Birth,
                Job = entity.Known_for_Department,
                Deathday = entity.Deathday,
                Imdb_Id = entity.ImdbId
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                entity.ProfilePicture = model.PhotoUrl;
                entity.Birthday = model.Birthday;
                entity.Place_of_Birth = model.PlaceOfBirth;
                entity.Known_for_Department = model.Job;
                entity.Deathday = model.Deathday;
                entity.ImdbId = model.Imdb_Id;

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
                MessageIcon = "fa-solid fa-trash-can"
            });
            return RedirectToAction("PersonList");
        }

        //Users
        public IActionResult UserList()
        {
            return View(_userManager.Users);
        }

        public async Task<IActionResult> UserEdit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var selectedRoles = await _userManager.GetRolesAsync(user);
                var roles = _roleManager.Roles.Select(i => i.Name);

                ViewBag.Roles = roles;
                return View(new AdminUserEditModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Birthday = user.Birthday,
                    Gender = user.Gender,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    SelectedRoles = selectedRoles
                });
            }
            return Redirect("~/admin/user/list");
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(AdminUserEditModel model, string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.UserName = model.UserName;
                    user.Birthday = model.Birthday;
                    user.Gender = model.Gender;
                    user.Email = model.Email;
                    user.EmailConfirmed = model.EmailConfirmed;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);
                        selectedRoles ??= Array.Empty<string>();
                        await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray<string>());
                        await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles).ToArray<string>());
                        TempData.Put("message", new NotificationModel
                        {
                            Message = $"{user.UserName} is updated",
                            MessageType = "update",
                            MessageIcon = "fa-solid fa-pen"
                        });
                        return Redirect("/admin/user/list");
                    }
                }
                return Redirect("/admin/user/list");
            }

            return View(model);

        }

        public async Task<IActionResult> UserDelete(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if(user != null) 
            { 
                await _userManager.DeleteAsync(user);
                TempData.Put("message", new NotificationModel
                {
                    Message = $"{user.UserName} is deleted",
                    MessageType = "delete",
                    MessageIcon = "fa-solid fa-trash-can"
                });
                return RedirectToAction("UserList");
            }
            TempData.Put("message", new NotificationModel
            {
                Message = "Unknown Error",
                MessageType = "error",
                MessageIcon = "fa - solid fa - exclamation"
            });
            return RedirectToAction("UserList");
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
                    TempData.Put("message", new NotificationModel
                    {
                        Message = "\"Role\" is added",
                        MessageType = "add",
                        MessageIcon = "fa-solid fa-plus"
                    });
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

        public async Task<IActionResult> RoleDelete(string RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
                TempData.Put("message", new NotificationModel
                {
                    Message = "Role Successfully Deleted",
                    MessageType = "delete",
                    MessageIcon = "fa-solid fa-trash-can"
                });
                return RedirectToAction("RoleList");

            }
            TempData.Put("message", new NotificationModel
            {
                Message = "Unknown Error",
                MessageType = "error",
                MessageIcon = "fa - solid fa - exclamation"
            });
            return RedirectToAction("RoleList");
        }

        public async Task<IActionResult> UpdateDatabase()
        {
            string json = "start";
            var idlist = new List<IdList>();
            var personDatabase = ""; //Path to json file
            using StreamReader jsonfile = new(personDatabase);
            json = jsonfile.ReadToEnd();
            idlist = JsonConvert.DeserializeObject<List<IdList>>(json);
            if (idlist != null)
            {
                foreach (var TmdbId in idlist.Select(i => i.id))
                {
                    string TMDBapiKey = _configuration["ApiKeys:TmdbApiKey"];
                    var baseAddressTMDB = new Uri("http://api.themoviedb.org/3/");
                    using var httpClientTMDB = new HttpClient { BaseAddress = baseAddressTMDB };
                    httpClientTMDB.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");
                    using var responseTMDB = await httpClientTMDB.GetAsync("person/" + TmdbId + "?api_key=" + TMDBapiKey + "&language=en-US");
                    string TMDBapiResponse = await responseTMDB.Content.ReadAsStringAsync();

                    TMDbApiPersonModel? TMDBrootObject = JsonConvert.DeserializeObject<TMDbApiPersonModel>(TMDBapiResponse);
                    if (TMDBrootObject != null)
                    {
                        var entity = new Person()
                        {
                            Name = TMDBrootObject.name,
                            Adult = TMDBrootObject.adult,
                            Biography = TMDBrootObject.biography,
                            Birthday = TMDBrootObject.birthday,
                            Deathday = TMDBrootObject.deathday,
                            Gender = TMDBrootObject.gender,
                            Homepage = TMDBrootObject.homepage,
                            ImdbId = TMDBrootObject.imdb_id,
                            Known_for_Department = TMDBrootObject.known_for_department,
                            Place_of_Birth = TMDBrootObject.place_of_birth,
                            Popularity = TMDBrootObject.popularity,
                            ProfilePicture = TMDBrootObject.profile_path,
                        };
                        _personService.Create(entity);
                        return RedirectToAction("Dashboard", "Admin");
                    }
                    TempData.Put("message", new NotificationModel
                    {
                        Message = "Api response returned null",
                        MessageType = "error",
                        MessageIcon = "fa-solid fa-exclamation"
                    });
                    return RedirectToAction("Dashboard", "Admin");
                }
            }
            TempData.Put("message", new NotificationModel
            {
                Message = "Id List is null",
                MessageType = "error",
                MessageIcon = "fa-solid fa-exclamation"
            });
            return RedirectToAction("Dashboard", "Admin");
        }

        public async Task<IActionResult> AddMovieFromTmdbAsync(string TmdbId) 
        {
            string TMDBapiKey = _configuration["ApiKeys:TmdbApiKey"];

            var baseAddressTMDB = new Uri("http://api.themoviedb.org/3/");
            using var httpClientTMDB = new HttpClient { BaseAddress = baseAddressTMDB };

            httpClientTMDB.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");
            using var responseTMDB = await httpClientTMDB.GetAsync("movie/" + TmdbId + "?api_key=" + TMDBapiKey + "&append_to_response=videos,images,credits&language=en");
            string TMDBapiResponse = await responseTMDB.Content.ReadAsStringAsync();

            TMDbApiMovieDetail? TMDBrootObject = JsonConvert.DeserializeObject<TMDbApiMovieDetail>(TMDBapiResponse);

            if (TMDBrootObject != null)
            {
                var entity = new Movie()
                {
                    Title = TMDBrootObject.title,
                    Director = TMDBrootObject.credits.crew.Where(i=>i.job == "Director").Select(i=>i.name).FirstOrDefault(),
                    Tagline = TMDBrootObject.tagline,
                    Info = TMDBrootObject.overview,
                    Poster   = Url.Content("https://image.tmdb.org/t/p/original" + TMDBrootObject.poster_path),
                    Backdrop = Url.Content("https://image.tmdb.org/t/p/original" + TMDBrootObject.backdrop_path),
                    ReleaseDate = Convert.ToDateTime(TMDBrootObject.release_date),
                    Original_Language = TMDBrootObject.original_language,
                    RunTime = TMDBrootObject.runtime,
                    Budget = TMDBrootObject.budget,
                    Ratings = null,
                    Popularity = TMDBrootObject.popularity,
                    IMDBId = TMDBrootObject.imdb_id,
                    TMDbId = TMDBrootObject.id.ToString(),
                    Logo = Url.Content("https://image.tmdb.org/t/p/original" 
                                + TMDBrootObject.images.logos.Select(i => i.file_path).FirstOrDefault()),
                    Trailer = Url.Content("https://youtube.com/embed/" 
                                + TMDBrootObject.videos.results.Where(i => i.type == "Trailer").Select(i => i.key).FirstOrDefault())
                };
                if (_movieService.Create(entity))
                {
                    TempData.Put("message", new NotificationModel
                    {
                        Message = $"\"{entity.Title}\" is added",
                        MessageType = "add",
                        MessageIcon = "fa-solid fa-plus"
                    });
                    return RedirectToAction("Dashboard", "Admin");

                }
                TempData.Put("message", new NotificationModel
                {
                    Message = "Unkown Error",
                    MessageType = "error",
                    MessageIcon = "fa-solid fa-exclamation"
                });
                return RedirectToAction("Dashboard", "Admin");
            }
            TempData.Put("message", new NotificationModel
            {
                Message = "Api Response returned null",
                MessageType = "error",
                MessageIcon = "fa-solid fa-exclamation"
            });
            return RedirectToAction("Dashboard", "Admin");
        }

    }
}