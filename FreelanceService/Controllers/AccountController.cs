//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using FreelanceService.BLL.DTO;
//using FreelanceService.DAL.Entities;
//using FreelanceService.DAL.Interfaces;

//namespace FreelanceService.Web.Controllers
//{
//    public class AccountController: Controller
//    {
//        private readonly IUnitOfWork _unitOfWork;
//       public AccountController(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        [HttpGet]
//        public IActionResult Login()
//        {
//            return View();
//        }
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Login(LoginModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                User user = await _unitOfWork.UserRepos.Find() .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
//                if (user != null)
//                {
//                    await Authenticate(model.Email); // аутентификация

//                    return RedirectToAction("Index", "Home");
//                }
//                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
//            }
//            return View(model);
//        }
//        [HttpGet]
//        public IActionResult Register()
//        {
//            return View();
//        }
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Register(RegisterModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                User user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
//                if (user == null)
//                {
//                    // добавляем пользователя в бд
//                    _unitOfWork.Users.Add(new User { Email = model.Email, Password = model.Password });
//                    await _unitOfWork.Commit();

//                    await Authenticate(model.Email); // аутентификация

//                    return RedirectToAction("Index", "Home");
//                }
//                else
//                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
//            }
//            return View(model);
//        }

//        private async Task Authenticate(string userName)
//        {
//            // создаем один claim
//            var claims = new List<Claim>
//            {
//                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
//            };
//            // создаем объект ClaimsIdentity
//            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
//            // установка аутентификационных куки
//            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
//        }

//        public async Task<IActionResult> Logout()
//        {
//            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
//            return RedirectToAction("Login", "Account");
//        }
//    }
//}
