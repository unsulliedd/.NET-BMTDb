using BMTDb.WebUI.Identity;
using BMTDb.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BMTDb.WebUI.EmailServices;

namespace BMTDb.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private IEmailSender _emailSender;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public IActionResult SignIn(string? ReturnUrl = null)
        {
            return View(new SignInModel()
            {
                ReturnUrl = ReturnUrl,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            if (!ModelState.IsValid) 
            { 
                return View(model); 
            }
            var user = await _userManager.FindByNameAsync(model.UserName);

            if(user == null)
            {
                ModelState.AddModelError("UserName", "Username is not valid");
                return View(model);
            }

            if(!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Please Confirm Your Email");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl ?? "~/");
            }
            ModelState.AddModelError("Password", "Password is wrong");
            return View(model);
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User()
            {
                UserName = model.UserName,
                Email = model.Email

            };
            var result = await _userManager.CreateAsync(user,model.Password);
            if (result.Succeeded)
            {
                //token url
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = token
                });
                //confirm email
                await _emailSender.SendEmailAsync
                    (model.Email, "BMTDb - Confirm Your Mail", 
                    $"<h3>BMTDb</h3>" +
                    $"<div>Please confirm your email.</div> " +
                    $"<div><a href='https://localhost:44384{url}'>CONFIRM</a></div>");
                return RedirectToAction("SignIn", "Account");
            }
            ModelState.AddModelError("Password", "Password must contain upper/lower characters and numbers " +
                "Password cannot be less than 8 characters");
            return View(model);
        }

        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }

        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
            if(userId == null || token==null)
            {
                TempData["message"] ="Invalid Token";
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if(user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    TempData["message"] = "Account is confirmed";
                    return View();
                }
            }
            TempData["message"] = "Account is not confirmed";
            return View();
        }
        
    }
}
