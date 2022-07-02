using BMTDb.Entity;
using BMTDb.Service.Abstract;
using BMTDb.WebUI.Extensions;
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

        public UserController(IMovieService movieService, UserManager<User> userManager, IWatchlistService watchlistService)
        {
            _movieService = movieService;
            _watchlistService = watchlistService;
            _userManager = userManager;
        }

        public async Task<IActionResult> UserProfile()
        {
            var userid = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userid);
            if (user != null)
            {
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
                });
            }
            return Redirect("~/User/UserProfile");
        }

        [HttpPost]
        public async Task<IActionResult> ProfileEdit(UserDetailsModel model)
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
                        MessageIcon = "fa - solid fa - exclamation"
                    });
                }
                return Redirect("~/User/UserProfile");
            }

            return View(model);
        }

        [Authorize]
        public IActionResult Watchlist()
        {
            var watchlist = _watchlistService.GetWatchlistbyUserId(_userManager.GetUserId(User));
            return View(new WatchlistModel
            {
                WatchlistId = watchlist.Id,
                WatchlistItems = watchlist.WatchlistItems.Select(i => new WatchlistItemModel()
                {
                    WatchlistItemId = i.Id,
                    Title = i.Movie.Title,
                    MoviePoster = i.Movie.MoviePoster,
                    Director = i.Movie.Director,
                    MovieRatings = i.Movie.MovieRatings,
                    RunTime = i.Movie.RunTime,
                    Status = i.Movie.Status,
                    ReleaseDate = i.Movie.ReleaseDate,
                    AddedDate = i.AddedDate
                }).ToList()
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddtoWatchlist(int MovieId, DateTime AddedDate)
        {
            var userId = _userManager.GetUserId(User);
            _watchlistService.AddtoWatchlist(userId, MovieId, AddedDate);
            return RedirectToAction("Index","Movie");
        }

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
                    Trailer = model.Trailer,
                };

                if (file != null)
                {
                    var extention = Path.GetExtension(file.FileName);
                    var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                    entity.MoviePoster = randomName;
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
                        MessageIcon = "fa - solid fa - plus"
                    });
                    return RedirectToAction("Index", "Movie");
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
    }
}