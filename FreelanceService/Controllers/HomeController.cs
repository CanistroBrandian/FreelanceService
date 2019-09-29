using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Interfaces;
using FreelanceService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            try
            {
                int pageSize = 5;

                ViewData["CurrentSort"] = sortOrder;
                ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
                ViewData["PriceSortParm"] = sortOrder == "Price" ? "Price_desc" : "Price";

                if (searchString != null)
                    pageNumber = 1;
                else
                    searchString = currentFilter;
                ViewData["CurrentFilter"] = searchString;

                var list = await _jobService.GetAllSorting(sortOrder);
                var search = _jobService.Search(searchString, list);
                return View(await PaginatedListModel<JobDTO>.Create(search.AsQueryable(), pageNumber ?? 1, pageSize));
            
            }
            catch
            {
                throw;
            }
        }

    }
}
