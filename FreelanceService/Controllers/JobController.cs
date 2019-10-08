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
            var jobDTO = await _jobService.FindJobById(id);
            var userCustomer = await _userService.FindUserById(jobDTO.UserId_Customer);
            var allResponsesOfJob = await _responseService.GetAllResponseOfJob(jobDTO.Id);          
            var mapResponsesOfJob = _mapper.Map<IEnumerable<ResponseDTO>, IEnumerable<ResponseListOfExecutors>>(allResponsesOfJob);
            var mapJobDelails = _mapper.Map<JobDTO, JobDetailsViewModel>(jobDTO);
            mapJobDelails.UserId_Customer = jobDTO.UserId_Customer;
            mapJobDelails.UserId_Executor = jobDTO.UserId_Executor;
            mapJobDelails.FirstNameCustomer = userCustomer.FirstName;
            mapJobDelails.LastNameCustmoer = userCustomer.LastName;
            mapJobDelails.ResponseListOfExecutors = mapResponsesOfJob;
           // var getAllUsersExecutorsOfResponse = await _userService.GetAllUsersExecutorsOfResponse(jobDTO.Id);
            // mapJobDelails.userDTOs = getAllUsersExecutorsOfResponse;

            return View(mapJobDelails);
        }

        [HttpPost]
        [Authorize(Roles = "Исполнитель")]
        public async Task<IActionResult> Response(JobDetailsViewModel model)
        {
            var userExecutor = await _userService.FindUserByEmail(User.Identity.Name);
            var job = await _jobService.FindJobById(model.Id);
            var responseViewModel = new ResponseAddViewModel
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