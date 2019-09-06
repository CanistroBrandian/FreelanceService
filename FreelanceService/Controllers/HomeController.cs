using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Helpers;
using FreelanceService.BLL.Interfaces;
using FreelanceService.BLL.Services;
using FreelanceService.DAL.Interfaces;
using FreelanceService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace FreelanceService.Web.Controllers
{
    public class HomeController : Controller
    {

        IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork uow)
        {
            _unitOfWork = uow;

        }

        public IActionResult Index()
        {
            try
            {
                var users = _unitOfWork.UserRepos.GetAll();
                
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
