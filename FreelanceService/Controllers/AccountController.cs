using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using FreelanceService.DAL.Entities;
using FreelanceService.DAL.Interfaces;
using FreelanceService.BLL.Models;
using Task = System.Threading.Tasks.Task;
using System;
using FreelanceService.BLL.Interfaces;

namespace FreelanceService.Web.Controllers
{
    public class AccountController : Controller
    {
        IUnitOfWork _unitOfWork;
        IEmailService _emailService;

        public AccountController(IUnitOfWork uow, IEmailService emailService)
        {
            _unitOfWork = uow;
            _emailService = emailService;

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
                var user = await _unitOfWork.UserRepos.FindByEmail(model.Email);
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
                var user = await _unitOfWork.UserRepos.FindByEmail(model.Email);
                if (user == null)
                {
                   await _unitOfWork.UserRepos.AddUser(new User
                    {
                        Email = model.Email,
                        PassHash = model.Password,
                        DynamicSalt = model.Password,
                        FirstName = model.FirstName,
                        LastName = model.FirstName,
                        Phone = model.Phone,
                        Role = model.Role,
                        RegistrationDateTime = registrationDateTime,
                    });
                    _unitOfWork.Commit();
                    await _emailService.SendEmailAsync(model.Email, "Succses registration", "You Login:" + model.Email + " You Pass:" + model.Password);
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
