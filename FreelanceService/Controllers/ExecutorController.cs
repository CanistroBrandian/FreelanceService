﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Interfaces;
using FreelanceService.Common.Enum;
using FreelanceService.Web.Models;
using FreelanceService.Web.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceService.Web.Controllers
{
    public class ExecutorController : Controller
    {
        IUserService _userService;
        IJobService _jobService;
        IResponseService _responseService;
        ICategoryService _categoryService;
        IMapper _mapper;


        IViewModelValidationService _validationService;

        /// <summary>
        /// Dependency Injection for jobService and userService
        /// </summary>
        /// <param name="jobService"></param>
        /// <param name="userService"></param>
        public ExecutorController(
            IJobService jobService,
            IUserService userService,
            IViewModelValidationService validationService,
            IResponseService responseService,
            IMapper mapper,
            ICategoryService categoryService)
        {
            _jobService = jobService;
            _userService = userService;
            _validationService = validationService;
            _responseService = responseService;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        [HttpGet]
        [Authorize(Roles = "Исполнитель")]
        public async Task<IActionResult> MyResponses()
        {
            var user = await _userService.FindUserByEmail(User.Identity.Name);
            var responsesOfExecutor = await _responseService.FindResponseByUserExecutorId(user.Id);
            var mapResponseOfExecutor = _mapper.Map<IEnumerable<ResponseDTO>, IEnumerable<ResponseListOfExecutors>>(responsesOfExecutor);
            return View(mapResponseOfExecutor);
        }

        [HttpGet]
        [Authorize(Roles = "Исполнитель")]
        public async Task<IActionResult> ResponseDetails(int responseId)
        {
            var responseOfExecutor = await _responseService.FindResponseById(responseId);
            var mapResponseDetails = _mapper.Map<ResponseDTO, ResponseDetailsViewModel>(responseOfExecutor);
            var job = await _jobService.FindJobById(mapResponseDetails.JobId);
            mapResponseDetails.CategoryId = job.CategoryId;
            var categoryDto = await _categoryService.FindCategoryById(mapResponseDetails.CategoryId);
           
            mapResponseDetails.CategoryName = categoryDto.Name;
            mapResponseDetails.StatusJob = job.Status;
            return View(mapResponseDetails);
        }

        [HttpGet]
        [Authorize(Roles = "Исполнитель")]
        public async Task<ViewResult> EditResponse(int responseId)
        {
            var job = await _responseService.FindResponseById(responseId);
            var mapJob = _mapper.Map<ResponseDTO, ResponseEditViewModel>(job);
            return View(mapJob);
        }

        [HttpPost]
        [Authorize(Roles = "Исполнитель")]
        public async Task<IActionResult> EditResponse(ResponseEditViewModel model)
        {
            var responseDTO = await _responseService.FindResponseById(model.Id);
            var mapJob = _mapper.Map<ResponseEditViewModel, ResponseDTO>(model);
            responseDTO.Price = mapJob.Price; //вынести на bll
            responseDTO.Description = mapJob.Description;
            await _responseService.Update(responseDTO);

            return RedirectToAction("MyResponses");
        }

        [HttpGet]
        [Authorize(Roles = "Исполнитель")]
        public async Task<IActionResult> SendForVerification(int jobId)
        {
            return View(jobId);
        }

        [HttpPost]
        [Authorize(Roles = "Исполнитель")]
        public async Task<IActionResult> ConfirmSendForVerification(int jobId)
        {
            var statusCode = (int)JobStatusEnum.WaitingForConfirmation;
            await _jobService.UpdateStatusJob(jobId, statusCode);
            return RedirectToAction("MyResponses");
        }

        [HttpGet]
        [Authorize(Roles = "Исполнитель")]
        public async Task<IActionResult> CancelResponse(int jobId)
        {
            var user = await _userService.FindUserByEmail(User.Identity.Name);  //перенести логику в responsesrvice
            var response = await _responseService.FindResponseByIdExecutorAndJobId(user.Id, jobId);
            await _responseService.Remove(response.Id);
            return RedirectToAction("MyResponses");
        }
    }
}