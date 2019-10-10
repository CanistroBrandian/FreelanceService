using AutoMapper;
using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Interfaces;
using FreelanceService.Web.Models;
using FreelanceService.Web.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelanceService.Web.Controllers
{
    public class MyJobsController : Controller
    {
        IUserService _userService;
        IJobService _jobService;
        IResponseService _responseService;
        IMapper _mapper;


        IViewModelValidationService _validationService;

        /// <summary>
        /// Dependency Injection for jobService and userService
        /// </summary>
        /// <param name="jobService"></param>
        /// <param name="userService"></param>
        public MyJobsController(
            IJobService jobService,
            IUserService userService,
            IViewModelValidationService validationService,
            IResponseService responseService,
            IMapper mapper)
        {
            _jobService = jobService;
            _userService = userService;
            _validationService = validationService;
            _responseService = responseService;
            _mapper = mapper;
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
            //валидация на присуттвие такой работы
            await _jobService.Remove(jobId);
            return RedirectToAction("MyJobs");
        }


        [HttpGet]
        [Authorize(Roles = "Заказчик")]
        public async Task<ViewResult> MyJobDetails(int jobId)
        {
            var job = await _jobService.FindJobById(jobId);
            var allResponsesOfJob = await _responseService.GetAllResponseOfJob(jobId); //bll вынести
            var mapResponsesOfJob = _mapper.Map<IEnumerable<ResponseDTO>, IEnumerable<ResponseListOfExecutors>>(allResponsesOfJob);
            var mapJobDetails = _mapper.Map<JobDTO, MyJobDetailsViewModel> (job);
            mapJobDetails.ResponseListOfExecutors = mapResponsesOfJob;
            return View(mapJobDetails);
        }

        [HttpGet]
        [Authorize(Roles = "Заказчик")]
        public async Task<IActionResult> SelectExecutorForJob(int jobId, int userId_Executor)
        {
            //валидация
            await _jobService.SelectExecutorForJob(jobId, userId_Executor);
            return RedirectToAction("MyJobs");
        }

        [HttpGet]
        [Authorize(Roles = "Заказчик")]
        public async Task<IActionResult> CancelResponse(int jobId, int userId_Executor)
        {
            //валдиация
            await _jobService.SelectExecutorForJob(jobId, userId_Executor);
            return RedirectToAction("MyJobs");
        }

    }
}