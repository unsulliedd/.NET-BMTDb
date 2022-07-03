using BMTDb.WebUI.Identity;
using BMTDb.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BMTDb.WebUI.ViewComponents
{
    public class ProfileNavbarViewComponent : ViewComponent
    {
        private readonly UserManager<User> _userManager;
        public ProfileNavbarViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userid = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userid);

            return View(new UserProfileModel()
            {
                UserName = user.UserName,
                ProfilePic = user.ProfilePic,
            });
        }
    }
}