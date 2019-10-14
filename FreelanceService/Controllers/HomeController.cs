using AutoMapper;
using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Interfaces;
using FreelanceService.Web.Models;
using Microsoft.AspNetCore.Authorization;
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
        ICategoryService _categoryService;
        public HomeController(IJobService jobService, 
            IUserService userService, 
            IMapper mapper,
            ICategoryService categoryService)
        {
            _jobService = jobService;
            _userService = userService;
            _mapper = mapper;
            _categoryService = categoryService;
        }


        [Authorize(Roles = "Исполнитель,Заказчик")]
        public async Task<ActionResult> Executors(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            int pageSize = 3;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["RatingSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Rating_desc" : "";
            ViewData["RegistrationDateTimeSortParm"] = sortOrder == "RegistrationDateTime" ? "RegistrationDateTime_desc" : "RegistrationDateTime";

            if (searchString != null)
                pageNumber = 1;
            else
                searchString = currentFilter;
            ViewData["CurrentFilter"] = searchString;

            var list = await _userService.GetAllSorting(sortOrder);
            var search = await _userService.Search(searchString, list);
            var mapAllExecutor = _mapper.Map<IEnumerable<UserDTO>, IEnumerable<UserExecutorViewModel>>(search);
            var view = PaginatedListModel<UserExecutorViewModel>.Create(mapAllExecutor.AsQueryable(), pageNumber ?? 1, pageSize);
            return View(view);
        }


        /// <summary>
        /// View all jobs
        /// </summary>
        /// <returns>View Home/Index</returns>
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {

            int pageSize = 3;

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
            var allCategory = await _categoryService.GetAll();
            var mapJobView = _mapper.Map<IEnumerable<JobDTO>, IEnumerable<JobViewModel>>(search);
           // mapJobView.
            var view = PaginatedListModel<JobViewModel>.Create(mapJobView.AsQueryable(), pageNumber ?? 1, pageSize);
            return View(view);

        }
    }
}
