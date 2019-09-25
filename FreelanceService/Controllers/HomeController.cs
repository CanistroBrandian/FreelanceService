using FreelanceService.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FreelanceService.Web.Controllers
{
    /// <summary>
    /// Display all jobs in system
    /// </summary>
    public class HomeController : Controller
    {

        IUserService _userService;
        IJobService _jobService;
        public HomeController(IJobService jobService, IUserService userService)
        {
            _jobService = jobService;
            _userService = userService;
        }


        /// <summary>
        /// View all jobs
        /// </summary>
        /// <returns>View Home/Index</returns>
        public async Task<IActionResult> Index(string sortOrder)
        {
            try
            {
                ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
                ViewData["PriceSortParm"] = sortOrder == "Price" ? "Price_desc" : "Price";
                var list = await _jobService.GetAllSorting(sortOrder);
                return View(list);
            }
            catch
            {
                throw;
            }
        }

    }
}
