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
            IMapper mapper)
        {
            _jobService = jobService;
            _userService = userService;
            _validationService = validationService;
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
            await _jobService.Update(model.Id, modelDTO);
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
        public async Task<ViewResult> ViewDetailsJob(int jobId)
        {
            var job = await _jobService.FindJobById(jobId);
            var mapJob = _mapper.Map<JobDTO, MyJobDetaiksViewModel> (job);
            return View(mapJob);
        }



    }
}