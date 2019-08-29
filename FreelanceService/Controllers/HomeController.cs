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
            using (DalSession dalSession = new DalSession())
            {
                UnitOfWork unitOfWork = dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    unitOfWork.Commit();
                    return View(_repo.GetAll());
                }
                catch
                {
                    unitOfWork.Rollback();
                    throw;
                }
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
