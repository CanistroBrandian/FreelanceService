using AutoMapper;
using FreelanceService.BLL.DTO;
using FreelanceService.DAL.Entities;
using FreelanceService.Web.Models;
using System.Collections.Generic;

namespace FreelanceService.BLL.Automapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<User, UserRegistrationDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserProfileEditDTO, User>().ReverseMap();
            CreateMap<ProfileEditViewModel, UserProfileEditDTO>().ReverseMap();
            CreateMap<UserDTO, ProfileEditViewModel>().ReverseMap();
            CreateMap<UserRegistrationDTO, RegisterViewModel>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Job, JobDTO>().ReverseMap();
            CreateMap<CreateJobViewModel, JobDTO>().ReverseMap();
            CreateMap<Review, ReviewDTO>().ReverseMap();
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<Response, ResponseDTO>().ReverseMap();
            CreateMap<PaginatedListModel<JobViewModel>, PaginatedListModel<JobDTO>>().ReverseMap();
            CreateMap<ResponseAddViewModel, ResponseDTO>().ReverseMap();
            CreateMap<CreateJobViewModel, CreateJobViewModel>().ReverseMap();
            CreateMap<ResponseDTO,ResponseListOfExecutors>().ForMember(dest => dest.ResponseId, opt => opt.MapFrom(src => src.Id));
            CreateMap<UserDTO, ResponseListOfExecutors>().ForMember(dest => dest.FirstNameExecutor, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastNameExecutor, opt => opt.MapFrom(src => src.LastName));
            CreateMap<UserDTO, UserExecutorViewModel>().ReverseMap();
            CreateMap<ResponseDTO, ResponseEditViewModel>().ReverseMap();
            CreateMap<ResponseDTO,ResponseDetailsViewModel>().ForMember(dest => dest.ResponseId, opt => opt.MapFrom(src => src.Id));
            CreateMap<JobEditViewModel, JobDTO>().ReverseMap();
            CreateMap<JobEditDTO, JobDTO>().ReverseMap();
            CreateMap<MyJobDetailsViewModel, JobDTO>().ReverseMap();
            CreateMap<JobDetailsViewModel, JobDTO>().ReverseMap();
            CreateMap<JobViewModel, JobDTO>().ReverseMap();
            CreateMap<UserDTO, ProfileViewModel>().ReverseMap();
        }
    }
}
