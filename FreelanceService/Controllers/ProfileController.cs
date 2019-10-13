using AutoMapper;
using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Interfaces;
using FreelanceService.Common.Enum;
using FreelanceService.Common.Extensions;
using FreelanceService.Common.Helpers;
using FreelanceService.Web.Models;
using FreelanceService.Web.Validation;
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
    /// <summary>
    /// Сontroller is responsible for displaying profile information, profile edit, adding new jobs by the customer
    /// </summary>
    public class ProfileController : Controller
    {
        IUserService _userService;
        IJobService _jobService;
        IMapper _mapper;


        IViewModelValidationService _validationService;

        /// <summary>
        /// Dependency Injection for jobService and userService
        /// </summary>
        /// <param name="jobService"></param>
        /// <param name="userService"></param>
        public ProfileController(
            IJobService jobService,
            IUserService userService,
            IViewModelValidationService validationService,
            IMapper mapper)
        {
            _jobService = jobService;
            _userService = userService;
            _validationService = validationService;
            _mapper = mapper;
        }


        [Authorize(Roles = "Исполнитель,Заказчик")]
        public async Task<ActionResult> Profile(int? userId)
        {
            if (userId == null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await _userService.FindUserByEmail(User.Identity.Name);
                    var checkUser = User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType); //нейминг checkUser
                    if (Equals(user.Email, checkUser))
                    {
                        var myprofile = await GetProfile(user.Id);
                        return View(myprofile);
                    }
                }
            }
            var profile = await GetProfile(userId.Value);
            return View(profile);
        }


        /// <summary>
        /// View edit profile
        /// </summary>
        /// <returns>View Profile/Edit</returns>
        public async Task<ActionResult> Edit()
        {
            var user = await _userService.FindUserByEmail(User.Identity.Name);
            var mapUserViewModel = _mapper.Map<UserDTO, ProfileEditViewModel>(user);
            return View(mapUserViewModel);
        }

        /// <summary>
        /// Request to edit user data
        /// </summary>
        /// <param name="model">model type of ProfileEditViewModel</param>
        /// <returns>View Profile/Profike</returns>
        [Authorize(Roles = "Исполнитель,Заказчик")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProfileEditViewModel model)
        {
            var validationResult = _validationService.Validate(model);
            if (!validationResult.IsValid)
            {
                ModelState.Merge(validationResult.ModelState);
                return View();
            }

            var modelDTO = _mapper.Map<ProfileEditViewModel, UserProfileEditDTO>(model);
            var user = await _userService.FindUserByEmail(User.Identity.Name);
            await _userService.Update(modelDTO, user);
            var newuser = await _userService.FindUserByEmail(User.Identity.Name);
            await Authenticate(newuser);
            return RedirectToAction(nameof(Profile));

        }

        /// <summary>
        /// Logout user
        /// </summary>
        /// <returns>View Account/User</returns>
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        private async Task Authenticate(UserDTO user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.DisplayName())
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "AuthCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        private async Task<ProfileViewModel> GetProfile()
        {
            var userDTO = await _userService.FindUserByEmail(User.Identity.Name);
            var map = _mapper.Map<UserDTO, ProfileViewModel>(userDTO);
            map.Role = userDTO.Role.DisplayName();
            map.City = userDTO.City.DisplayName();
            return map;
        }

        private async Task<ProfileViewModel> GetProfile(int userId)
        {
            var userDTO = await _userService.FindUserById(userId);
            var map = _mapper.Map<UserDTO, ProfileViewModel>(userDTO);
            map.Role = userDTO.Role.DisplayName();
            map.City = userDTO.City.DisplayName();
            return map;
        }
    }
}
