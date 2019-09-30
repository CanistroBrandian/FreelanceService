using AutoMapper;
using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Interfaces;
using FreelanceService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        IMapper _mapper;
        public HomeController(IJobService jobService, IUserService userService, IMapper mapper)
        {
            _jobService = jobService;
            _userService = userService;
            _mapper = mapper;
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
                var map = _mapper.Map <IEnumerable<JobDTO>, IEnumerable<JobViewModel>> (search);
                var view = await PaginatedListModel<JobViewModel>.Create(map.AsQueryable(), pageNumber ?? 1, pageSize);
                return View(view);
            
            }
            catch
            {
                throw;
            }
        }

    }
}
