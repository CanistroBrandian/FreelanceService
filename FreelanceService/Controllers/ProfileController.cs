using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Interfaces;
using FreelanceService.BLL.Models;
using FreelanceService.Web.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;


namespace FreelanceService.Web.Controllers
{
    public class ProfileController : Controller
    {
        IUserService _userService;
        IJobService _jobService;

        public ProfileController(IJobService jobService, IUserService userService)
        {
            _jobService = jobService;
            _userService = userService;
        }

        [Authorize(Roles = "Исполнитель,Заказчик")]
        public async Task<ActionResult> Profile()
        {

            if (User.IsInRole("Исполнитель"))
                return RedirectToAction(nameof(ProfileExecutor));
            else  return RedirectToAction(nameof(ProfileCustomer));
        }

        // GET: Profile
        [Authorize(Roles = "Заказчик")]
        public async Task<ActionResult> ProfileCustomer()
        {

            var user = await _userService.FindUserByEmail(User.Identity.Name);
            var viewProfile = new ProfileViewModel
            {
                City = CityNameFromInt.GetName(user.City),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Role = RoleNameFromInt.GetName(user.Role)
            };

            return View(viewProfile);
        }
        [Authorize(Roles = "Исполнитель")]
        public async Task<ActionResult> ProfileExecutor()
        {

            var user = await _userService.FindUserByEmail(User.Identity.Name);

            var viewProfile = new ProfileViewModel
            {
                City = CityNameFromInt.GetName(user.City),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Role = RoleNameFromInt.GetName(user.Role)
            };

            return View(viewProfile);
        }

        public ActionResult Edit()
        {
            return View();
        }

        [Authorize(Roles = "Исполнитель,Заказчик")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProfileEditViewModel model)
        {
            try
            {
                var user = await _userService.FindUserByEmail(User.Identity.Name);
                await _userService.Update(new UserDTO
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.FirstName,
                    City = model.City,
                    Phone = model.Phone,
                    Role = model.Role
                });
                await _userService.CommitAsync();
                await Authenticate(model.Email);
                return RedirectToAction(nameof(Profile));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult CreateJob()
        {
            return View();
        }

        [Authorize(Roles = "Заказчик")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateJob(CreateJobViewModel model)
        {
            try
            {
        
                await _jobService.AddJob(new JobDTO
                {
                    Name = model.Name,
                    Description = model.Description,
                    CategoryId = model.CategoryId,
                    City = model.City,
                    FinishedDateTime = model.FinishedDateTime,
                    Price = model.Price,
                    RegistrationJobDateTime = DateTime.Now,
                    StartDateTime = DateTime.Now
                });
                await _jobService.CommitAsync();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
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

    }
}
