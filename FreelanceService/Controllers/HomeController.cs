using FreelanceService.DAL.Interfaces;
using FreelanceService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FreelanceService.Controllers
{
    public class HomeController : Controller
    {

        IDbContext _db;
       

        public HomeController(IDbContext db)
        {
            _db = db;

        }

        public IActionResult Index()
        {
            try
            {

               var query = _db.UserRepos.GetAll();
                _db.Commit();
                return View(query);
            }
            catch
            {
                _db.Rollback();
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
