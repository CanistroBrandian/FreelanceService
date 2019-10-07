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

        public async Task AddResponse(ResponseDTO response, int userExecitorId, int jobId)
        {
            
            var mapResponse = _mapper.Map<ResponseDTO, Response>(response);
            await _uow.ResponseRepos.AddResponse(mapResponse, userExecitorId, jobId);          
            await CommitAsync();
        }

        public async Task<ResponseDTO> FindResponseById(int id)
        {

            var entity = await _uow.ResponseRepos.FindResponseById(id);
            return _mapper.Map<Response, ResponseDTO>(entity);
        }

        public async Task<ResponseDTO> FindResponseByJobId(int id)
        {

            var entity = await _uow.ResponseRepos.FindResponseByJobId(id);
            return _mapper.Map<Response, ResponseDTO>(entity);
        }

        public async Task<IEnumerable<ResponseDTO>> GetAll()
        {
            var result = _mapper.Map<IEnumerable<Response>, IEnumerable<ResponseDTO>>(await _uow.ResponseRepos.GetAll());
            return result;
        }

        public async Task<IEnumerable<ResponseDTO>> GetAllResponseOfJob(int jobId)
        {
        
            var responses = await _uow.ResponseRepos.GetAllResponseOfJob(jobId);
            var map = _mapper.Map<IEnumerable<Response>, IEnumerable<ResponseDTO>>(responses);
            return map;
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
