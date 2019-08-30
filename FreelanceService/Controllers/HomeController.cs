using FreelanceService.DAL;
using FreelanceService.DAL.Interfaces;
using FreelanceService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FreelanceService.Controllers
{
    public class HomeController : Controller
    {

        IUserRepository _repo;
        IUnitOfWork _uow;
       
        public HomeController(IUserRepository repo, IUnitOfWork uow)
        {
            _repo = repo;
            _uow = uow;

        }
        public IActionResult Index()
        {
           
                _uow.Begin();
                try
                {
                _uow.Commit();
                    return View(_repo.GetAll());
                }
                catch
                {
                _uow.Rollback();
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
