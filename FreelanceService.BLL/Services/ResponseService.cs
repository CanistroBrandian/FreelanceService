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
    public  class ResponseService:IResponseService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ResponseService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task AddResponse(ResponseDTO entity)
        {
            var response = _mapper.Map<ResponseDTO, Response>(entity);
            await _uow.ResponseRepos.AddResponse(response);

        }

        public async Task<ResponseDTO> FindResponseById(int id)
        {
            if (id == 0)
                throw new Exception("Поле Id не введено");
            var entity = await _uow.ResponseRepos.FindById(id);
            return _mapper.Map<Response, ResponseDTO>(entity);
        }

        public async Task<IEnumerable<ResponseDTO>> GetAll()
        {
            var result = _mapper.Map<IEnumerable<Response>, IEnumerable<ResponseDTO>>(await _uow.ResponseRepos.GetAll());
            return result;
        }

        public async Task Update(ResponseDTO entity)
        {

            var response = _mapper.Map<ResponseDTO, Response>(entity);
            await _uow.ResponseRepos.Update(response);
        }

        public async Task Remove(ResponseDTO entity)
        {

            await _uow.ResponseRepos.Remove(entity.Id);
        }

        public async Task CommitAsync()
        {
            await _uow.CommitAsync();
        }
    }
}
