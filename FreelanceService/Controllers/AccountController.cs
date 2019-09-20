using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Interfaces;
using FreelanceService.BLL.Models;
using FreelanceService.Common.Encrypt;
using FreelanceService.Common.Salt;
using FreelanceService.Web.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FreelanceService.Web.Controllers
{
    public class AccountController : Controller
    {
        IEmailService _emailService;
        IUserService _userService;

        public AccountController(IEmailService emailService, IUserService userService)
        {
            _emailService = emailService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.FindUserByEmail(model.Email);
                if (user != null && SHA256Encrypt.checkHashSha256(model.Password,user.PassHash, user.DynamicSalt))
                {
                    
                    await Authenticate(user);
                    return RedirectToAction("Profile", "Profile");
                }
               ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            var dynamicSalt = GenerateSalt.GetDinamicSalt();
            // if (ModelState.IsValid)
            //  {
            // var user = await _unitOfWork.UserRepos.FindByEmail(model.Email);
            var user = await _userService.FindUserByEmail(model.Email);
            if (user == null)
            {
                var newUser = new UserDTO
                {
                    Email = model.Email,
                    DynamicSalt = dynamicSalt,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    City = model.City,
                    PassHash = SHA256Encrypt.getHashSha256(model.Password, dynamicSalt),
                    Phone = model.Phone,
                    Role = model.Role,
                    RegistrationDateTime = DateTime.Now,
                };

                await _userService.AddUser(newUser);
                await _userService.CommitAsync();
                await _emailService.SendEmailAsync(newUser.Email, "Succses registration", "You Login:" + newUser.Email + " You Pass:" + model.Password);
                await Authenticate(newUser);

                return RedirectToAction("Profile", "Profile");
            }
            else
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            // }
            return View(model);
        }

        private async Task Authenticate(UserDTO user)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, RoleNameFromInt.GetName(user.Role))
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "AuthCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
