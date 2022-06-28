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

        public UserController(IMovieService movieService, UserManager<User> userManager)
        {
            _movieService = movieService;
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
    }
}