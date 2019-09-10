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

        IUnitOfWork _unitOfWork;
        IMapper _mapper;
        IUserService _userService;

        public HomeController(IUnitOfWork uow, IMapper mapper, IUserService userService)
        {
            _unitOfWork = uow;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {  
                var users = _unitOfWork.UserRepos.GetAll();
                var serviceUsers = await _userService.GetAll();
             
                return View(serviceUsers);
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
