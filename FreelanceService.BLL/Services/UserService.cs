using AutoMapper;
using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Interfaces;
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

        public async Task AddUser(UserRegistrationDTO model)
        {
            model.DynamicSalt = GenerateSalt.GetDinamicSalt();
            model.PassHash = SHA256Encrypt.getHashSha256WithSalt(model.Password, model.DynamicSalt);
            var user = _mapper.Map<UserRegistrationDTO, User>(model);
            await _uow.UserRepos.AddUser(user);
            await CommitAsync();
        }

        public async Task<UserDTO> FindUserByEmail(string email)
        {

            var entity = await _uow.UserRepos.FindByEmail(email);
            await CommitAsync();
            return _mapper.Map<User, UserDTO>(entity);
        }


        public async Task<UserDTO> FindUserById(int id)
        {

            var entity = await _uow.UserRepos.FindById(id);
            return _mapper.Map<User, UserDTO>(entity);
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var result = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(await _uow.UserRepos.GetAll());
            return result;
        }

        public async Task Update(UserProfileEditDTO editModel, UserDTO userDTO)
        {
            var map = _mapper.Map<UserProfileEditDTO, User>(editModel);
            map.Id = userDTO.Id;
            map.Email = userDTO.Email;
            await _uow.UserRepos.Update(map);
            await CommitAsync();
        }


        public async Task<bool> ResetPasswordAsync(UserDTO user, string token, string newPassword)
        {
            if (token == user.VerifyCodeForResetPass)
            {
                var map = _mapper.Map<UserDTO, User>(user);
                map.PassHash = SHA256Encrypt.getHashSha256WithSalt(newPassword, user.DynamicSalt);
                await _uow.UserRepos.ResetPassword(map);
                await CommitAsync();
                return true;
            }
            else return false;

        }

        public async Task<string> GeneratePasswordResetTokenAsync(UserDTO model)
        {
            model.VerifyCodeForResetPass = SHA256Encrypt.getHashSha256(model.Email);
            var user = _mapper.Map<UserDTO, User>(model);
            await _uow.UserRepos.Update(user);
            await CommitAsync();
            return model.VerifyCodeForResetPass;
        }

        private async Task CommitAsync()
        {
            await _uow.CommitAsync();
        }
    }
}
