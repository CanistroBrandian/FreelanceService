using AutoMapper;
using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Interfaces;
using FreelanceService.BLL.Interfaces.ValidationServices;
using FreelanceService.Common.Enum;
using FreelanceService.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelanceService.Web.Controllers
{
    public class CustomerController : Controller
    {
        IJobService _jobService;
        IMapper _mapper;
        IUserService _userService;
        IResponseService _responseService;
        ICategoryService _categoryService;
        IValidateJob _validateJob;

        public CustomerController(IJobService jobService,
            IMapper mapper,
            IUserService userService,
            IResponseService responseService,
            ICategoryService categoryService,
            IValidateJob validaterUser)
        {
            _jobService = jobService;
            _mapper = mapper;
            _userService = userService;
            _responseService = responseService;
            _categoryService = categoryService;
            _validateJob = validaterUser;
        }


        /// <summary>
        /// View create new job of customer
        /// </summary>
        /// <returns>View Profile/CreateJob</returns>
        [HttpGet]
        public async Task<IActionResult> CreateJob()
        {
            var allCategories = await _categoryService.GetAll();
            var jobViewModel = new CreateJobViewModel
            {
                CategoryDTOs = allCategories
            };
            return View(jobViewModel);
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
            var validateModel = _validateJob.ValidateNewJob(model.FinishedDateTime, model.Price);
            if (ModelState.IsValid)
            {
                if (validateModel)
                {
                    var modelDTO = _mapper.Map<CreateJobViewModel, JobDTO>(model);
                    var user = await _userService.FindUserByEmail(User.Identity.Name);
                    await _jobService.AddJob(modelDTO, user);
                    return RedirectToAction("MyJobs");
                }
                else ModelState.AddModelError("", "Проверьте введеные данные");
                model.CategoryDTOs = await _categoryService.GetAll();
            }
            else ModelState.AddModelError("", "Проверьте введеные данные");
            return View(model);
        }

        /// <summary>
        /// Displays a list of all customer orders
        /// </summary>
        /// <param name="pageNumber">Сurrent page state</param>
        /// <returns>View Customer/MyJobs</returns>
        [Authorize(Roles = "Заказчик")]
        public async Task<IActionResult> MyJobs(int? pageNumber)
        {
            var pageSize = 3;

            var user = await _userService.FindUserByEmail(User.Identity.Name);
            var jobs = await _jobService.GetAllJobsOfCustomer(user.Id);
            foreach(var item in jobs)
            {
                var categoryDto = await _categoryService.FindCategoryById(item.CategoryId);
                item.CategoryName = categoryDto.Name;
            }
            var mapJobViewModel = _mapper.Map<IEnumerable<JobDTO>, IEnumerable<JobViewModel>>(jobs);
            var view = PaginatedListModel<JobViewModel>.Create(mapJobViewModel, pageNumber ?? 1, pageSize);

            return View(view);
        }

        /// <summary>
        /// Job editing form
        /// </summary>
        /// <param name="jobId">id job parameter</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Заказчик")]
        public async Task<ViewResult> EditJob(int jobId)
        {
            var job = await _jobService.FindJobById(jobId);
            var mapJob = _mapper.Map<JobDTO, JobEditViewModel>(job);
            mapJob.CategoryDTOs = await _categoryService.GetAll();
            return View(mapJob);
        }

        /// <summary>
        /// Sends a request to edit job data
        /// </summary>
        /// <param name="model">model is JobEditViewModel</param>
        /// <returns>View Customer/EditJob</returns>
        [HttpPost]
        [Authorize(Roles = "Заказчик")]
        public async Task<IActionResult> EditJob(JobEditViewModel model)
        {
            var validate = _validateJob.ValidateEditJob(model.FinishedDateTime, model.Price);
            if (ModelState.IsValid)
            {
                if (validate)
                {
                    var modelDTO = _mapper.Map<JobEditViewModel, JobDTO>(model);
                    await _jobService.Update(modelDTO);
                    return RedirectToAction("MyJobs");
                }
                else ModelState.AddModelError("", "Проверьте введеные данные");
            }
            return View(model);
        }

        /// <summary>
        /// displays a form for deleting a job
        /// </summary>
        /// <param name="jobId">parameter of the selected job</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Заказчик")]
        public async Task<ViewResult> DeleteJob(int jobId)
        {
            return View(jobId);
        }

        /// <summary>
        /// Job removal request
        /// </summary>
        /// <param name="jobId">parameter id for job</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Заказчик")]
        public async Task<IActionResult> ConfirmDeleteJob(int jobId)
        {
            // сделать валидацию по статусу 
            await _jobService.Remove(jobId);
            return RedirectToAction("MyJobs");
        }

        /// <summary>
        /// Displays detailed job information
        /// </summary>
        /// <param name="jobId">parameter id for job</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Заказчик")]
        public async Task<ViewResult> MyJobDetails(int jobId)
        {
            var job = await _jobService.FindJobById(jobId);
            var allResponsesOfJob = await _responseService.GetAllResponseOfJob(jobId);
            var userExecutor = await _userService.FindUserById(job.UserId_Executor);
            var mapResponsesOfJob = _mapper.Map<IEnumerable<ResponseDTO>, IEnumerable<ResponseListOfExecutors>>(allResponsesOfJob);
            var mapJobDetails = _mapper.Map<JobDTO, MyJobDetailsViewModel>(job);
            var categoryOfJob = await _categoryService.FindCategoryById(mapJobDetails.CategoryId);
            mapJobDetails.ResponseListOfExecutors = mapResponsesOfJob;
            mapJobDetails.UserExecutor = userExecutor;
            mapJobDetails.Category = categoryOfJob;
            return View(mapJobDetails);
        }

        /// <summary>
        /// confirmation of the request for job performed
        /// </summary>
        /// <param name="jobId">parameter id for job</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Заказчик")]
        public async Task<IActionResult> ConfirmationRequestFromExecutorOnJob(int jobId)
        {
            var statusCode = (int)JobStatusEnum.Done;
            await _jobService.UpdateStatusJob(jobId, statusCode);
            return RedirectToAction("MyJobs");
        }

        /// <summary>
        /// assigns an executor to the selected job
        /// </summary>
        /// <param name="jobId">parameter id for job</param>
        /// <param name="userId_Executor">parameter id for Executor</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Заказчик")]
        public async Task<IActionResult> SelectExecutorForJob(int jobId, int userId_Executor)
        {
            await _jobService.SelectExecutorForJob(jobId, userId_Executor);
            return RedirectToAction("MyJobs");
        }

    }
}