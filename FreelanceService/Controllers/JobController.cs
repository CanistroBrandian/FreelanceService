using AutoMapper;
using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Interfaces;
using FreelanceService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreelanceService.Web.Controllers
{
    public class JobController : Controller
    {
        IJobService _jobService;
        IMapper _mapper;
        IUserService _userService;
        IResponseService _responseService;

        public JobController(IJobService jobService,
            IMapper mapper,
            IUserService userService,
            IResponseService responseService)
        {
            _jobService = jobService;
            _mapper = mapper;
            _userService = userService;
            _responseService = responseService;
        }
        public async Task<IActionResult> Job(int id)
        {
            var view = await _jobService.FindJobByIdJob(id);
            var map = _mapper.Map<JobDTO, JobDetailsViewModel>(view);
            return View(map);
        }

        [HttpPost]
        [Authorize(Roles = "Исполнитель")]
        public async Task<IActionResult> Response(JobDetailsViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userExecutor = await _userService.FindUserByEmail(User.Identity.Name);
                var job = await _jobService.FindJobByIdJob(model.Id);
                var responseViewModel = new ResponseAddViewModel
                {
                    Description = model.ResponseAddViewModel.Description,
                    Price = model.ResponseAddViewModel.Price
                };
                var mapResponse = _mapper.Map<ResponseAddViewModel, ResponseDTO>(responseViewModel);
                 await _responseService.AddResponse(mapResponse,userExecutor.Id,job.Id);
            }
            else RedirectToAction("Login", "Account");
            return RedirectToAction("Job");
        }


    }
}