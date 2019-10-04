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
            if (id != 0)
            {
                var entity = await _uow.UserRepos.FindUserById(id);
                return _mapper.Map<User, UserDTO>(entity);
            }
            else return null;
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var result = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(await _uow.UserRepos.GetAll());
            return result;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersExecutorsOfResponse(int jobId)
        {
            var listUserExecutorId = new List<int>();
          //var mapResponseDTO = _mapper.Map<IEnumerable<ResponseDTO>, IEnumerable<Response>>(responseDTO);
          var allResponseOfJob = await _uow.ResponseRepos.GetAllResponseOfJob(jobId);

            foreach(var item in allResponseOfJob)          
                listUserExecutorId.Add(item.UserId_Executor);
                    
            var allUsersExecutorsOfResponse = await _uow.UserRepos.GetAllUsersExecutorsOfResponse(listUserExecutorId);
            var mapUserDTO = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(allUsersExecutorsOfResponse);
            return mapUserDTO;
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
