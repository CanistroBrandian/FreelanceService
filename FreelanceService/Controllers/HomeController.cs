using AutoMapper;
using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Interfaces;
using FreelanceService.BLL.Services;
using FreelanceService.DAL.Interfaces;
using FreelanceService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        public async Task<IActionResult> Index()
        {
            try
            {

                var job = await _jobService.GetAll();
                
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
