using AutoMapper;
using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Interfaces;
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

        public async Task AddUser(UserDTO entity)
        {
            var user = _mapper.Map<UserDTO, User>(entity);
            await _uow.UserRepos.AddUser(user);

        }

        public async Task<UserDTO> FindUserByEmail(string email)
        {
            if (email == null)
                throw new Exception("Поле Email не введено");
            var entity = await _uow.UserRepos.FindByEmail(email);

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

        public async Task Update(UserDTO entity)
        {

            var user = _mapper.Map<UserDTO, User>(entity);
            await _uow.UserRepos.Update(user);
        }


        public async Task CommitAsync()
        {
            await _uow.CommitAsync();
        }

    }
}
