using BMTDb.WebUI.Identity;
using BMTDb.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BMTDb.WebUI.ViewComponents
{
    public class ProfileHeaderViewComponent : ViewComponent
    {
        private readonly UserManager<User> _userManager;
        public ProfileHeaderViewComponent(UserManager<User> userManager)
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
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                ProfilePic = user.ProfilePic,
                CreationDate = user.CreationDate,
            });
        }
    }
}