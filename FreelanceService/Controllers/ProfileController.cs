﻿using FreelanceService.BLL.Models;
using FreelanceService.DAL.Entities;
using FreelanceService.DAL.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;


namespace FreelanceService.Web.Controllers
{
    public class ProfileController : Controller
    {
        IUnitOfWork _unitOfWork;

        public ProfileController(IUnitOfWork uow)
        {
            _unitOfWork = uow;

        }

        // GET: Profile
        [Authorize]
        public async Task<ActionResult> Index()
        {

            var user = await _unitOfWork.UserRepos.FindByEmail(User.Identity.Name);

            var viewProfile = new ProfileViewModel
            {
                City = user.City,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Role = user.Role
            };

            return View(viewProfile);
        }

        public ActionResult Edit()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProfileViewModel model)
        {
            try
            {
                var user = await _unitOfWork.UserRepos.FindByEmail(User.Identity.Name);
                await _unitOfWork.UserRepos.Update(new User
                {
                    Id = user.Id,
                    DynamicSalt = user.DynamicSalt,
                    PassHash = user.PassHash,
                    Rating = user.Rating,
                    RegistrationDateTime = user.RegistrationDateTime,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.FirstName,
                    City = model.City,
                    Role = model.Role,
                    Phone = model.Phone,
                });
              await  _unitOfWork.CommitAsync();
                await Authenticate(model.Email);
                return RedirectToAction(nameof(Index));
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
