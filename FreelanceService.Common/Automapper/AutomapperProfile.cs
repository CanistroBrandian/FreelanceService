using AutoMapper;
using FreelanceService.BLL.DTO;
using FreelanceService.;
using FreelanceService.Common.Salt;
using FreelanceService.DAL.Entities;

namespace FreelanceService.Common.Automapper
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
            CreateMap<Job, JobViewDTO>().ReverseMap();
            CreateMap<CreateJobViewModel, JobDTO>().ReverseMap();
            CreateMap<Review, ReviewDTO>().ReverseMap();
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<Response, ResponseDTO>().ReverseMap();

            CreateMap<UserDTO, ProfileViewModel>().ReverseMap();
        }
    }
}
