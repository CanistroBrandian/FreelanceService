using AutoMapper;
using FreelanceService.BLL.DTO;
using FreelanceService.DAL.Entities;
using FreelanceService.Models;
using System.Collections.Generic;

namespace FreelanceService.BLL.Automapper
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile()
        {
            CreateMap<User, UserRegistrationDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserProfileEditDTO, User>().ReverseMap();
            CreateMap<ProfileEditViewModel, UserProfileEditDTO>().ReverseMap();
            CreateMap<UserRegistrationDTO, RegisterViewModel>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Job, JobDTO>().ReverseMap();
            CreateMap<CreateJobViewModel, JobDTO>().ReverseMap();
            CreateMap<Review, ReviewDTO>().ReverseMap();
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<Response, ResponseDTO>().ReverseMap();
            CreateMap<PaginatedListModel<JobViewModel>, PaginatedListModel<JobDTO>>().ReverseMap();

            CreateMap<JobDTO, JobViewModel>().ReverseMap();
            CreateMap<JobViewModel, JobDTO>().ReverseMap();
            CreateMap<UserDTO, ProfileViewModel>().ReverseMap();
        }
    }
}
