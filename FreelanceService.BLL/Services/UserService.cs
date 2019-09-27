using AutoMapper;
using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Interfaces;
using FreelanceService.BLL.Models;
using FreelanceService.Common.Encrypt;
using FreelanceService.Common.Salt;
using FreelanceService.DAL.Entities;
using FreelanceService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelanceService.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task AddUser(RegisterViewModel registrationModel)
        {
            var model = _mapper.Map<RegisterViewModel, UserRegistrationDTO>(registrationModel);
            model.DynamicSalt = GenerateSalt.GetDinamicSalt();
            model.PassHash = SHA256Encrypt.getHashSha256WithSalt(registrationModel.ConfirmPassword, model.DynamicSalt);
            var user = _mapper.Map<UserRegistrationDTO, User>(model);
            await _uow.UserRepos.AddUser(user);
            await CommitAsync();
        }

        public async Task<UserDTO> FindUserByEmail(string email)
        {
            if (email == null)
                throw new Exception("Поле Email не введено");
            var entity = await _uow.UserRepos.FindByEmail(email);
            await CommitAsync();
            return _mapper.Map<User, UserDTO>(entity);
        }


        public async Task<UserDTO> FindUserById(int id)
        {
            if (id == 0)
                throw new Exception("Поле Id не введено");
            var entity = await _uow.UserRepos.FindById(id);
            return _mapper.Map<User, UserDTO>(entity);
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var result = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(await _uow.UserRepos.GetAll());
            return result;
        }

        public async Task Update(ProfileEditViewModel editModel, UserDTO userDTO)
        {
            
            var model = _mapper.Map<ProfileEditViewModel, UserProfileEditDTO>(editModel);
            var map = _mapper.Map<UserProfileEditDTO, User>(model);
            map.Id = userDTO.Id;
            map.Email = userDTO.Email;
            await _uow.UserRepos.Update(map);
            await CommitAsync();
        }


        public async Task CommitAsync()
        {
            await _uow.CommitAsync();
        }
    }
}
