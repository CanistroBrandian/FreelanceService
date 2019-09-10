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
        }
    }
}
