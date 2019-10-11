using AutoMapper;
using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Interfaces;
using FreelanceService.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelanceService.Web.Controllers
{
    public class JobController : Controller
    {
        IJobService _jobService;
        IMapper _mapper;
        IUserService _userService;
        IResponseService _responseService;
        ICategoryService _categoryService;

        public JobController(IJobService jobService,
            IMapper mapper,
            IUserService userService,
            IResponseService responseService,
            ICategoryService categoryService)
        {
            _jobService = jobService;
            _mapper = mapper;
            _userService = userService;
            _responseService = responseService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Job(int id) //нейминг 
        {
            var jobDTO = await _jobService.FindJobById(id);
            var userCustomer = await _userService.FindUserById(jobDTO.UserId_Customer);
            var allResponsesOfJob = await _responseService.GetAllResponseOfJob(jobDTO.Id);          
            var mapResponsesOfJob = _mapper.Map<IEnumerable<ResponseDTO>, IEnumerable<ResponseListOfExecutors>>(allResponsesOfJob);
            var mapJobDelails = _mapper.Map<JobDTO, JobDetailsViewModel>(jobDTO);//вынести в bll
            mapJobDelails.UserCustomer = userCustomer;
            mapJobDelails.UserExecutors = await _userService.GetAllUsersExecutorsOfResponse(jobDTO.Id);
            mapJobDelails.Category = await _categoryService.FindCategoryById(mapJobDelails.CategoryId);
            mapJobDelails.ResponseListOfExecutors = mapResponsesOfJob;

           // var getAllUsersExecutorsOfResponse = await _userService.GetAllUsersExecutorsOfResponse(jobDTO.Id);
            // mapJobDelails.userDTOs = getAllUsersExecutorsOfResponse;

            return View(mapJobDelails);
        }

        [HttpPost]
        [Authorize(Roles = "Исполнитель")]
        public async Task<IActionResult> Response(JobDetailsViewModel model) //нейминг 
        {
            var userExecutor = await _userService.FindUserByEmail(User.Identity.Name);
            var job = await _jobService.FindJobById(model.Id);
            var responseViewModel = new ResponseAddViewModel //вынести в bll
            {
                Description = model.ResponseAddViewModel.Description,
                Price = model.ResponseAddViewModel.Price
            };
            var mapResponse = _mapper.Map<ResponseAddViewModel, ResponseDTO>(responseViewModel);
            await _responseService.AddResponse(mapResponse, userExecutor.Id, job.Id);

            return RedirectToAction("Job", new { job.Id });
        }
    }
}