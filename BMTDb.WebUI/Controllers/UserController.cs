#pragma warning disable CS8602 // Dereference of a possibly null reference.

using BMTDb.Entity;
using BMTDb.Service.Abstract;
using BMTDb.WebUI.Extensions;
using BMTDb.WebUI.Filters;
using BMTDb.WebUI.Identity;
using BMTDb.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BMTDb.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IMovieService _movieService;
        private readonly IWatchlistService _watchlistService;
        private readonly IFavouriteService _favouriteService;
        private readonly UserActivityFilter _userActivityFilter;

        public UserController(IMovieService movieService, UserManager<User> userManager, IWatchlistService watchlistService, 
            IFavouriteService favouriteService, UserActivityFilter userActivityFilter)
        {
            _movieService = movieService;
            _watchlistService = watchlistService;
            _userManager = userManager;
            _favouriteService = favouriteService;
            _userActivityFilter = userActivityFilter;
        }

        public async Task<IActionResult> UserProfile()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userActivity = _userActivityFilter.GetUserActivity(user.UserName);
            var movies = _movieService.GetUserMovielist(userActivity.Select(i => i.Data).ToList(),user.UserName);

            if (user != null)
            {
                if (movies != null)
                {
                    return View(new UserProfileModel()
                    {
                        UserName = user.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Gender = user.Gender,
                        ProfilePic = user.ProfilePic,
                        CreationDate = user.CreationDate,
                        Movie = movies,
                    });
                }
                return View(new UserProfileModel()
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    ProfilePic = user.ProfilePic,
                    CreationDate = user.CreationDate,
                });
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult RemoveFromRecentlyViewed(int movieId)
        {
            var username = _userManager.GetUserName(User);
            _movieService.RemoveFromRecentlyViewed(username, movieId);
            return RedirectToAction("UserProfile", "User");
        }

        public async Task<IActionResult> ProfileEdit()
        {
            var userid = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userid);
            if (user != null)
            {
                return View(new UserDetailsModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Birthday = user.Birthday,
                    Gender = user.Gender,
                    Email = user.Email,
                    ProfilePic = user.ProfilePic,
                });
            }
            return Redirect("~/User/UserProfile");
        }

        [HttpPost]
        public async Task<IActionResult> ProfileEdit(UserDetailsModel model, IFormFile file )
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
                    user.ProfilePic = model?.ProfilePic;

                    if (file != null)
                    {
                        var extention = Path.GetExtension(file.FileName);
                        var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                        user.ProfilePic = randomName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\UserPictures", randomName);

                        using var stream = new FileStream(path, FileMode.Create);
                        await file.CopyToAsync(stream);
                    }

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        TempData.Put("message", new NotificationModel
                        {
                            Message = $"Profile Successfully Updated",
                            MessageType = "update",
                            MessageIcon = "fa-solid fa-pen"
                        });
                        return Redirect("~/User/UserProfile");
                    }
                    TempData.Put("message", new NotificationModel
                    {
                        Message = "Unknown Error, Try Again Later",
                        MessageType = "error",
                        MessageIcon = "fa-solid fa-exclamation"
                    });
                }
                return Redirect("~/User/UserProfile");
            }

            return View(model);
        }

        //Favourite
        [Authorize]
        public IActionResult Favourite(string sortOrder)
        {
            var favourite = _favouriteService.GetFavouritebyUserId(_userManager.GetUserId(User));
            if (favourite != null)
            {
                if (sortOrder == null)
                {
                    return View(new FavouriteModel
                    {
                        FavouriteId = favourite.Id,
                        FavouriteItems = favourite.FavouriteItems.Select(i => new FavouriteItemModel()
                        {
                            FavouriteItemId = i.Id,
                            MovieId = i.MovieId,
                            Title = i.Movie.Title,
                            MoviePoster = i.Movie.Poster,
                            MovieBackdrop = i.Movie.Backdrop,
                            Director = i.Movie.Director,
                            MovieInfo = i.Movie.Info,
                            MovieTagline = i.Movie.Tagline,
                            MovieRatings = i.Movie.Ratings,
                            RunTime = i.Movie.RunTime,
                            Status = i.Movie.Status,
                            ReleaseDate = i.Movie.ReleaseDate,
                            AddedDate = i.AddedDate,
                            Genres = i.Movie.MovieGenres.Select(i => i.Genre).ToList(),
                            ProductionCompanies = i.Movie.MovieProductionCompanies.Select(i => i.ProductionCompanies).ToList(),

                        }).ToList()
                    });
                }
                else if (sortOrder.Contains("Ascending"))
                {
                    return View(new FavouriteModel
                    {
                        FavouriteId = favourite.Id,
                        FavouriteItems = favourite.FavouriteItems.Select(i => new FavouriteItemModel()
                        {
                            FavouriteItemId = i.Id,
                            MovieId = i.MovieId,
                            Title = i.Movie.Title,
                            MoviePoster = i.Movie.Poster,
                            MovieBackdrop = i.Movie.Backdrop,
                            Director = i.Movie.Director,
                            MovieInfo = i.Movie.Info,
                            MovieTagline = i.Movie.Tagline,
                            MovieRatings = i.Movie.Ratings,
                            RunTime = i.Movie.RunTime,
                            Status = i.Movie.Status,
                            ReleaseDate = i.Movie.ReleaseDate,
                            AddedDate = i.AddedDate,
                            Genres = i.Movie.MovieGenres.Select(i => i.Genre).ToList(),
                            ProductionCompanies = i.Movie.MovieProductionCompanies.Select(i => i.ProductionCompanies).ToList(),

                        }).OrderBy(i => i.GetType().GetProperty(sortOrder.Split('_').Last()).GetValue(i)).ToList()
                    });
                }
                else
                {
                    return View(new FavouriteModel
                    {
                        FavouriteId = favourite.Id,
                        FavouriteItems = favourite.FavouriteItems.Select(i => new FavouriteItemModel()
                        {
                            FavouriteItemId = i.Id,
                            MovieId = i.MovieId,
                            Title = i.Movie.Title,
                            MoviePoster = i.Movie.Poster,
                            MovieBackdrop = i.Movie.Backdrop,
                            Director = i.Movie.Director,
                            MovieInfo = i.Movie.Info,
                            MovieTagline = i.Movie.Tagline,
                            MovieRatings = i.Movie.Ratings,
                            RunTime = i.Movie.RunTime,
                            Status = i.Movie.Status,
                            ReleaseDate = i.Movie.ReleaseDate,
                            AddedDate = i.AddedDate,
                            Genres = i.Movie.MovieGenres.Select(i => i.Genre).ToList(),
                            ProductionCompanies = i.Movie.MovieProductionCompanies.Select(i => i.ProductionCompanies).ToList(),

                        }).OrderByDescending(i => i.GetType().GetProperty(sortOrder.Split('_').Last()).GetValue(i)).ToList()
                    });
                }
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddtoFavourite(int MovieId, DateTime AddedDate)
        {
            var userId = _userManager.GetUserId(User);
            _favouriteService.AddtoFavourite(userId, MovieId, AddedDate);
            return RedirectToAction("Index","Movie");
        }

        [Authorize]
        [HttpPost]
        public IActionResult RemoveFromFavourite(int movieId)
        {
            var userId = _userManager.GetUserId(User);
            _favouriteService.RemoveFromFavourite(userId, movieId);
            return RedirectToAction("Favourite", "User");
        }

        //Watchlist
        [Authorize]
        public IActionResult Watchlist(string sortOrder)
        {
            var watchlist = _watchlistService.GetWatchlistbyUserId(_userManager.GetUserId(User));
            if (watchlist != null)
            {
                if (sortOrder == null)
                {
                    return View(new WatchlistModel
                    {
                        WatchlistId = watchlist.Id,
                        WatchlistItems = watchlist.WatchlistItems.Select(i => new WatchlistItemModel()
                        {
                            WatchlistItemId = i.Id,
                            MovieId = i.MovieId,
                            Title = i.Movie.Title,
                            MoviePoster = i.Movie.Poster,
                            MovieBackdrop = i.Movie.Backdrop,
                            Director = i.Movie.Director,
                            MovieInfo = i.Movie.Info,
                            MovieTagline = i.Movie.Tagline,
                            MovieRatings = i.Movie.Ratings,
                            RunTime = i.Movie.RunTime,
                            Status = i.Movie.Status,
                            ReleaseDate = i.Movie.ReleaseDate,
                            AddedDate = i.AddedDate,
                            Genres = i.Movie.MovieGenres.Select(i => i.Genre).ToList(),
                            ProductionCompanies = i.Movie.MovieProductionCompanies.Select(i => i.ProductionCompanies).ToList(),

                        }).ToList()
                    });
                }
                else if(sortOrder.Contains("Ascending"))
                {
                    return View(new WatchlistModel
                    {
                        WatchlistId = watchlist.Id,
                        WatchlistItems = watchlist.WatchlistItems.Select(i => new WatchlistItemModel()
                        {
                            WatchlistItemId = i.Id,
                            MovieId = i.MovieId,
                            Title = i.Movie.Title,
                            MoviePoster = i.Movie.Poster,
                            MovieBackdrop = i.Movie.Backdrop,
                            Director = i.Movie.Director,
                            MovieInfo = i.Movie.Info,
                            MovieTagline = i.Movie.Tagline,
                            MovieRatings = i.Movie.Ratings,
                            RunTime = i.Movie.RunTime,
                            Status = i.Movie.Status,
                            ReleaseDate = i.Movie.ReleaseDate,
                            AddedDate = i.AddedDate,
                            Genres = i.Movie.MovieGenres.Select(i => i.Genre).ToList(),
                            ProductionCompanies = i.Movie.MovieProductionCompanies.Select(i => i.ProductionCompanies).ToList(),

                        }).OrderBy(i => i.GetType().GetProperty(sortOrder.Split('_').Last()).GetValue(i)).ToList()
                    });
                }
                else
                {
                    return View(new WatchlistModel
                    {
                        WatchlistId = watchlist.Id,
                        WatchlistItems = watchlist.WatchlistItems.Select(i => new WatchlistItemModel()
                        {
                            WatchlistItemId = i.Id,
                            MovieId = i.MovieId,
                            Title = i.Movie.Title,
                            MoviePoster = i.Movie.Poster,
                            MovieBackdrop = i.Movie.Backdrop,
                            Director = i.Movie.Director,
                            MovieInfo = i.Movie.Info,
                            MovieTagline = i.Movie.Tagline,
                            MovieRatings = i.Movie.Ratings,
                            RunTime = i.Movie.RunTime,
                            Status = i.Movie.Status,
                            ReleaseDate = i.Movie.ReleaseDate,
                            AddedDate = i.AddedDate,
                            Genres = i.Movie.MovieGenres.Select(i => i.Genre).ToList(),
                            ProductionCompanies = i.Movie.MovieProductionCompanies.Select(i => i.ProductionCompanies).ToList(),

                        }).OrderByDescending(i => i.GetType().GetProperty(sortOrder).GetValue(i)).ToList()
                    });
                }
            }
            
            else
            {
                return View();
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddtoWatchlist(int MovieId, DateTime AddedDate)
        {
            var userId = _userManager.GetUserId(User);
            _watchlistService.AddtoWatchlist(userId, MovieId, AddedDate);
            return RedirectToAction("Index", "Movie");
        }

        [Authorize]
        [HttpPost]
        public IActionResult RemoveFromWatchlist(int movieId)
        {
            var userId = _userManager.GetUserId(User);
            _watchlistService.RemoveFromWatchlist(userId, movieId);
            return RedirectToAction("Watchlist", "User");
        }

        //UserAddMovie
        [Authorize]
        [HttpGet]
        public IActionResult UserAddMovie()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserAddMovie(UserMovieModel model,IFormFile file)
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
                    IMDBId = model.IMDBId,
                    TMDbId = model.TMDbId,
                    Logo = model.MovieLogo,
                    Trailer = model.Trailer,
                };

                if (file != null)
                {
                    var extention = Path.GetExtension(file.FileName);
                    var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                    entity.Poster = randomName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\img\\Content",randomName);

                    using var stream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(stream);
                }

                if (_movieService.Create(entity))
                {
                    TempData.Put("message", new NotificationModel
                    {
                        Message = $"\"{entity.Title}\" is added",
                        MessageType = "add",
                        MessageIcon = "fa-solid fa-plus"
                    });
                    return RedirectToAction("Index", "Movie");
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
    }
}