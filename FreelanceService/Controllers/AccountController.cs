using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using FreelanceService.DAL.Entities;
using FreelanceService.DAL.Interfaces;
using FreelanceService.BLL.Models;

using System;
using FreelanceService.BLL.Interfaces;
using FreelanceService.BLL.DTO;

namespace FreelanceService.Web.Controllers
{
    public class AccountController : Controller
    {
        IUnitOfWork _unitOfWork;
        IEmailService _emailService;
        IUserService _userService;

        public AccountController(IUnitOfWork uow, IEmailService emailService, IUserService userService)
        {
            _unitOfWork = uow;
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
               // var user = await _unitOfWork.UserRepos.FindByEmail(model.Email);
                var user = await _userService.FindUserByEmail(model.Email);
                if (user != null)
                {
                    await Authenticate(model.Email);

                    return RedirectToAction("Index", "Profile");
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
            var registrationDateTime = DateTime.Now;
            if (ModelState.IsValid)
            {
               // var user = await _unitOfWork.UserRepos.FindByEmail(model.Email);
                var user = await _userService.FindUserByEmail(model.Email);
                if (user == null)
                {
                    var newUser = new UserDTO
                    {
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        City = model.City,
                        PassHash = model.Password,
                        Phone = model.Phone,
                        Role = model.Role,
                        RegistrationDateTime = registrationDateTime,
                        DynamicSalt = model.Password
                    };

                    await _userService.AddUser(newUser);
                    await _userService.CommitAsync();
                    await _emailService.SendEmailAsync(newUser.Email, "Succses registration", "You Login:" + newUser.Email + " You Pass:" + newUser.Password);
                    await Authenticate(model.Email);

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
