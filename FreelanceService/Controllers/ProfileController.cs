using AutoMapper;
using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Interfaces;
using FreelanceService.Common.Enum;
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

        /// <summary>
        /// Redirection to the profile of the executor or customer
        /// </summary>
        [Authorize(Roles = "Исполнитель,Заказчик")]
        public ActionResult Profile()
        {
            if (User.IsInRole("Исполнитель"))
                return RedirectToAction(nameof(ProfileExecutor));
            else return RedirectToAction(nameof(ProfileCustomer));
        }

        /// <summary>
        /// View ProfileCustomer
        /// </summary>
        /// <returns>View Profile/ProfileCusomer</returns>
        [Authorize(Roles = "Заказчик")]
        public async Task<ActionResult> ProfileCustomer()
        {
            var profile = await GetProfile();
            return View(profile);
        }

        /// <summary>
        /// View Profile Executor
        /// </summary>
        /// <returns>View Profile/ProfileExecutorr</returns>
        [Authorize(Roles = "Исполнитель")]
        public async Task<ActionResult> ProfileExecutor()
        {
            var profile = await GetProfile();
            return View(profile);
        }

        /// <summary>
        /// View edit profile
        /// </summary>
        /// <returns>View Profile/Edit</returns>
        public ActionResult Edit()
        {
            return View();
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
            try
            {
                var modelDTO = _mapper.Map<ProfileEditViewModel, UserProfileEditDTO>(model);
                var user = await _userService.FindUserByEmail(User.Identity.Name);
                await _userService.Update(modelDTO, user);
                var newuser = await _userService.FindUserByEmail(User.Identity.Name);
                await Authenticate(newuser);
                return RedirectToAction(nameof(Profile));
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// View create new job of customer
        /// </summary>
        /// <returns>View Profile/CreateJob</returns>
        [HttpGet]
        public IActionResult CreateJob()
        {
            return View();
        }

        /// <summary>
        /// Request to create a new job for the customer
        /// </summary>
        /// <param name="model"></param>
        /// <returns>View Home/Index</returns>
        [Authorize(Roles = "Заказчик")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateJob(CreateJobViewModel model)
        {
            try
            {
                var modelDTO = _mapper.Map<CreateJobViewModel, JobDTO>(model);
                var user = await _userService.FindUserByEmail(User.Identity.Name);
                await _jobService.AddJob(modelDTO, user);
                return RedirectToAction("MyJobs","MyJobs");
            }
            catch
            {
                return View();
            }
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
                new Claim(ClaimsIdentity.DefaultRoleClaimType, Common.Enum.RoleNameFromInt.GetName(user.Role))
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "AuthCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        private async Task<ProfileViewModel> GetProfile()
        {
            var userDTO = await _userService.FindUserByEmail(User.Identity.Name);
            var map = _mapper.Map<UserDTO, ProfileViewModel>(userDTO);
            map.Role = RoleNameFromInt.GetName(userDTO.Role);
            map.City = CityNameFromInt.GetName(userDTO.City);
            return map;
        }
    }
}
