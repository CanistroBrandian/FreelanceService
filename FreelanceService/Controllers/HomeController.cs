using FreelanceService.DAL.Interfaces;
using FreelanceService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FreelanceService.Web.Controllers
{
    public class HomeController : Controller
    {

        IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork uow)
        {
            _unitOfWork = uow;

        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var users = await _unitOfWork.UserRepos.GetAll();
                
                return View(users);
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
