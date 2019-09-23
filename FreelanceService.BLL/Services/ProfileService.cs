using AutoMapper;
using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Interfaces;
using FreelanceService.BLL.Models;
using FreelanceService.Common.Enum;
using FreelanceService.DAL.Interfaces;
using System.Threading.Tasks;

namespace FreelanceService.BLL.Services
{
    public class ProfileService :IProfileService
    {

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ProfileService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ProfileViewModel> GetProfile(UserDTO model)
        {

            var user = _mapper.Map<UserDTO, ProfileViewModel>(model);
            user.Role = RoleNameFromInt.GetName(model.Role);
            user.City = CityNameFromInt.GetName(model.City);
            return user;
        }

        public async Task CommitAsync()
        {
            await _uow.CommitAsync();
        }
    }
}
