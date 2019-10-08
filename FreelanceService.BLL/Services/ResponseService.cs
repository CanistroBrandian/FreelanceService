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

      public async  Task<ResponseDTO> FindResponseByIdExecutorAndJobId(int userId_executor, int jobId)
        {
            var entity = await _uow.ResponseRepos.FindResponseByIdExecutorAndJobId(userId_executor, jobId);
            return _mapper.Map<Response, ResponseDTO>(entity);
        }

        public async Task<IEnumerable<ResponseDTO>> GetAll()
        {
            var mapResponse = _mapper.Map<IEnumerable<Response>, IEnumerable<ResponseDTO>>(await _uow.ResponseRepos.GetAll());
            return mapResponse;
        }

        public async Task<IEnumerable<ResponseDTO>> GetAllResponseOfJob(int jobId)
        {
            var responses = await _uow.ResponseRepos.GetAllResponseOfJob(jobId);
            var mapResponse = _mapper.Map<IEnumerable<Response>, IEnumerable<ResponseDTO>>(responses);
            return mapResponse;
        }

        public async Task<IEnumerable<ResponseDTO>> FindResponseByUserExecutorId(int userId_Executor) {
            var responses = await _uow.ResponseRepos.FindResponseByUserExecutorId(userId_Executor);
            var mapResponse = _mapper.Map<IEnumerable<Response>, IEnumerable<ResponseDTO>>(responses);
            return mapResponse;
        }

        public async Task Update(ResponseDTO entity)
        {
            var mapJob = _mapper.Map<ResponseDTO, Response>(entity);
            await _uow.ResponseRepos.Update(mapJob);
            await CommitAsync();
        }

        public async Task Remove(int id)
        {

            await _uow.ResponseRepos.Remove(id);
            await CommitAsync();
        }

        public async Task CommitAsync()
        {
            await _uow.CommitAsync();
        }
    }
}
