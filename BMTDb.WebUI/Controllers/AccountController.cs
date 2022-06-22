#pragma warning disable IDE0037 // Use inferred member name
#pragma warning disable IDE0059 // Unnecessary assignment of a value

using BMTDb.WebUI.Identity;
using BMTDb.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BMTDb.WebUI.EmailServices;
using BMTDb.WebUI.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace BMTDb.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
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
                TempData.Put("message", new NotificationModel
                {
                    Message = "Successfully logged in",
                    MessageType = "success",
                    MessageIcon = "fa-solid fa-check"
                });
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
            TempData.Put("message", new NotificationModel
            {
                Message = "Signed out with success",
                MessageType = "success",
                MessageIcon = "fa-solid fa-check"
            });
            return Redirect("~/");
        }

        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
            if(userId == null || token==null)
            {
                TempData.Put("message", new NotificationModel
                {
                    Message = "Invalid Token",
                    MessageType = "error",
                    MessageIcon = "fa-exclamation"
                });
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if(user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");                    //Adds User to User Role on Account Verification

                    TempData.Put("message", new NotificationModel
                    {
                        Message = "Account is confirmed",
                        MessageType = "success",
                        MessageIcon = "fa-solid fa-check"
                    });
                    return View();
                }
            }
            TempData.Put("message", new NotificationModel
            {
                Message = "Account is not confirmed",
                MessageType = "error",
                MessageIcon = "fa-exclamation"
            });
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
                TempData.Put("message", new NotificationModel
                {
                    Message = "Email is not valid",
                    MessageType = "error",
                    MessageIcon = "fa-exclamation"
                }); 
                return View();
            }

            var user = await _userManager.FindByEmailAsync((Email));

            if (user == null)
            {
                TempData.Put("message", new NotificationModel
                {
                    Message = "Email is not valid",
                    MessageType = "error",
                    MessageIcon = "fa-exclamation"
                });
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
                TempData.Put("message", new NotificationModel
                {
                    Message = "Unable to reset password",
                    MessageType = "error",
                    MessageIcon = "fa-exclamation"
                });
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
                TempData.Put("message", new NotificationModel
                {
                    Message = "Unable to reset password",
                    MessageType = "error",
                    MessageIcon = "fa-exclamation"
                });
                return RedirectToAction("Index", "Home");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)
            {
                TempData.Put("message", new NotificationModel
                {
                    Message= "Password is changed. Please Login Again",
                    MessageType="success",
                    MessageIcon= "fa-solid fa-check"
                });
                return RedirectToAction("SignIn", "Account");
            }

            return View(model);
        }

        public IActionResult AccessDenied()
        { 
            return View(); 
        }
    }
}