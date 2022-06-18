using BMTDb.WebUI.Identity;
using BMTDb.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BMTDb.WebUI.EmailServices;
using Newtonsoft.Json;

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
                CreateMessage("Succesfully singin", "success", "fa-solid fa-plus");
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
                    $"<div>Username: {user.UserName} </div>" +
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
                    CreateMessage("Account is confirmed", "success", "fa - solid fa-plus");
                    return View();
                }
            }
            CreateMessage("Account is not confirmed", "error", "fa - solid fa-exclamation");
            return View();
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                CreateMessage("Email is not valid", "error", "fa - solid fa-exclamation");
                return View();
            }

            var user = await _userManager.FindByEmailAsync((Email));

            if (user == null)
            {
                CreateMessage("Email is not valid", "error", "fa - solid fa-exclamation");
                return View();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            //token url
            var url = Url.Action("ResetPassword", "Account", new
            {
                userId = user.Id,
                token = token
            });

            //password reset email
            await _emailSender.SendEmailAsync
                (Email, "BMTDb - Password Reset",
                $"<h3>BMTDb</h3>" +
                $"<div>Reset password request for {user.UserName}</div>" +
                $"<div><a href='https://localhost:44384{url}'>RESET</a></div>");
            return RedirectToAction("SignIn", "Account");
        }

        public IActionResult ResetPassword(string userId,string token)
        {
            if (userId == null || token == null)
            {
                CreateMessage("Unable to reset password","error", "fa-solid fa-exclamation");
                return RedirectToAction("Index", "Home");
            }

            var model = new ResetPasswordModel 
            { 
                Token = token,
            };

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                CreateMessage("Unable to reset password", "error", "fa-solid fa-exclamation");
                return RedirectToAction("Index", "Home");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)
            {
                CreateMessage("Password is changed. Please Login Again", "success", "fa-solid fa-plus");
                return RedirectToAction("SignIn", "Account");
            }

            return View(model);
        }

        private void CreateMessage(string Message, string MessageType, string MessageIcon)
        {
            NotificationModel msg = new NotificationModel()
            {
                MessageType = MessageType,
                Message = Message,
                MessageIcon = MessageIcon
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);
        }
    }
}