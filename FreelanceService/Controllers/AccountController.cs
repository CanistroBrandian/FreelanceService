using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Interfaces;
using FreelanceService.BLL.Models;
using FreelanceService.Common.Encrypt;
using FreelanceService.Web.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FreelanceService.Web.Controllers
{
    /// <summary>
    /// The controller is responsible for user registration, login and authentication
    /// </summary>
    public class AccountController : Controller
    {
        IEmailService _emailService;
        IUserService _userService;

        /// <summary>
        /// Dependency Injection User and Email service
        /// </summary>
        /// <param name="emailService"></param>
        /// <param name="userService"></param>
        public AccountController(IEmailService emailService, IUserService userService)
        {
            _emailService = emailService;
            _userService = userService;
        }
        /// <summary>
        /// User authorization page. If the user is authorized, then he will be redirected to his profile
        /// </summary>
        /// <returns>View Account/Login</returns>
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Profile", "Profile");
            return View();
        }
        /// <summary>
        /// The client side sends the LoginViewModel to the server to authorize the user. With successful authorization redirects to user profile and added authorization cookies
        /// </summary>
        /// <param name="model">model of type LoginVewModel</param>
        /// <returns>View Profile/Profile</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.FindUserByEmail(model.Email);
                if (user != null && SHA256Encrypt.checkHashSha256(model.Password, user.PassHash, user.DynamicSalt))
                {
                    await Authenticate(user);
                    return RedirectToAction("Profile", "Profile");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        /// <summary>
        /// User registration page.
        /// </summary>
        /// <returns>View Account/Register</returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// The client side sends the RegistrationViewModel to add a new user. If the registration is successful, the user is authenticate
        /// </summary>
        /// <param name="model">model of type RegisterViewModel</param>
        /// <returns>View Profile/Profile</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = await _userService.FindUserByEmail(model.Email);
            if (user == null)
            {
                await _userService.AddUser(model);
                await _userService.CommitAsync();
                await _emailService.SendEmailAsync(model.Email, "Succses registration", "You Login:" + model.Email + " You Pass:" + model.Password);
                await Authenticate(await _userService.FindUserByEmail(model.Email));
                return RedirectToAction("Profile", "Profile");
            }
            else
               ModelState.AddModelError("", "Пользователь с таким Email существует");
            }
            return View(model);
        }

        /// <summary>
        /// User authentication method
        /// </summary>
        /// <param name="user">model type of UserDTO</param>
        /// <returns></returns>
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


    }
}
