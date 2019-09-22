using FreelanceService.BLL.Interfaces;
using FreelanceService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FreelanceService.Web.Controllers
{
    public class HomeController : Controller
    {

        IUserService _userService;
        IJobService _jobService;
        public HomeController(IJobService jobService, IUserService userService)
        {
            _jobService = jobService;
            _userService = userService;
        }

        [Authorize(Roles = "Заказчик")]
        public async Task<IActionResult> Index()
        {
            try
            {
                
                var job = await _jobService.GetAllJobsOfCustomer(await _userService.FindUserByEmail(User.Identity.Name));
                return View(job);
            }
            catch
            {
                throw;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
