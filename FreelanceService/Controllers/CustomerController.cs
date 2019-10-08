using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Interfaces;
using FreelanceService.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceService.Web.Controllers
{
    public class CustomerController : Controller
    {
        IJobService _jobService;
        IMapper _mapper;
        IUserService _userService;
        IResponseService _responseService;

        public CustomerController(IJobService jobService,
            IMapper mapper,
            IUserService userService,
            IResponseService responseService)
        {
            _jobService = jobService;
            _mapper = mapper;
            _userService = userService;
            _responseService = responseService;
        }


        /// <summary>
        /// View create new job of customer
        /// </summary>
        /// <returns>View Profile/CreateJob</returns>
        [HttpGet]
        public IActionResult CreateJob()
        {
            return View();
        }

        /// <summary>
        /// Request to create a new job for the customer
        /// </summary>
        /// <param name="model"></param>
        /// <returns>View Home/Index</returns>
        [Authorize(Roles = "Заказчик")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateJob(CreateJobViewModel model)
        {
            try
            {
                var modelDTO = _mapper.Map<CreateJobViewModel, JobDTO>(model);
                var user = await _userService.FindUserByEmail(User.Identity.Name);
                await _jobService.AddJob(modelDTO, user);
                return RedirectToAction("MyJobs");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Заказчик,Исполнитель")]
        public async Task<IActionResult> MyJobs()
        {
            var user = await _userService.FindUserByEmail(User.Identity.Name);
            var jobs = await _jobService.GetAllJobsOfCustomer(user.Id);
            var map = _mapper.Map<IEnumerable<JobDTO>, IEnumerable<JobViewModel>>(jobs);
            return View(map);
        }

        [HttpGet]
        [Authorize(Roles = "Заказчик")]
        public async Task<ViewResult> EditJob(int jobId)
        {
            var job = await _jobService.FindJobById(jobId);
            var mapJob = _mapper.Map<JobDTO, JobEditViewModel>(job);
            return View(mapJob);
        }

        [HttpPost]
        [Authorize(Roles = "Заказчик")]
        public async Task<IActionResult> EditJob(JobEditViewModel model)
        {
            var modelDTO = _mapper.Map<JobEditViewModel, JobDTO>(model);
            await _jobService.Update(modelDTO);
            return RedirectToAction("MyJobs");
        }

        [HttpGet]
        [Authorize(Roles = "Заказчик")]
        public async Task<ViewResult> DeleteJob(int jobId)
        {

            return View(jobId);
        }

        [HttpPost]
        [Authorize(Roles = "Заказчик")]
        public async Task<IActionResult> ConfirmDeleteJob(int jobId)
        {

            await _jobService.Remove(jobId);
            return RedirectToAction("MyJobs");
        }


        [HttpGet]
        [Authorize(Roles = "Заказчик")]
        public async Task<ViewResult> MyJobDetails(int jobId)
        {
            var job = await _jobService.FindJobById(jobId);
            var allResponsesOfJob = await _responseService.GetAllResponseOfJob(jobId);
            var mapResponsesOfJob = _mapper.Map<IEnumerable<ResponseDTO>, IEnumerable<ResponseListOfExecutors>>(allResponsesOfJob);
            var mapJobDetails = _mapper.Map<JobDTO, MyJobDetailsViewModel>(job);
            mapJobDetails.ResponseListOfExecutors = mapResponsesOfJob;
            return View(mapJobDetails);
        }

        [HttpGet]
        [Authorize(Roles = "Заказчик")]
        public async Task<IActionResult> SelectExecutorForJob(int jobId, int userId_Executor)
        {
            await _jobService.SelectExecutorForJob(jobId, userId_Executor);
            return RedirectToAction("MyJobs");
        }

    }
}