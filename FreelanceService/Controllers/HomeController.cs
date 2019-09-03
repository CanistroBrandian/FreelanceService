using FreelanceService.BLL.DTO;
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
        IDbContext _db;
        private readonly IConfiguration _config;


        public HomeController(IDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;

        }

        public IActionResult Index()
        {
            try
            {
                // EmailModel emailModel = new EmailModel();
                //var asd = new EmailModel
                //{
                //    NameUser = "User",
                //    Subject = "asd",
                //    Description = "description"
                //};
                //EmailService email = new EmailService(_config);
                //email.SendAsync(asd);

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
