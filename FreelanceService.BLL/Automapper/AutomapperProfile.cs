using AutoMapper;
using FreelanceService.BLL.DTO;
using FreelanceService.DAL.Entities;

namespace FreelanceService.BLL.Automapper
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Job, JobDTO>().ReverseMap();
            CreateMap<Review, ReviewDTO>().ReverseMap();
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<Response, ResponseDTO>().ReverseMap();
        }
    }
}
