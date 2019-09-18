using FreelanceService.BLL.Models;
using FreelanceService.Common.Enum;
using FreelanceService.DAL.Entities;

using FreelanceService.DAL.Interfaces;
using FreelanceService.Web.Helpers;
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
        [Authorize(Roles = "2")]
        public async Task<ActionResult> IndexCustomer()
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
        [Authorize(Roles = "1")]
        public async Task<ActionResult> IndexExecutor()
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

        [Authorize(Roles = "1,2")]
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
                return RedirectToAction(nameof(IndexCustomer));
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
