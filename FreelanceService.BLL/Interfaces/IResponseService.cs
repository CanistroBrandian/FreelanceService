using FreelanceService.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelanceService.BLL.Interfaces
{
    public interface IResponseService
    {
        Task AddResponse(ResponseDTO entity, int userExecutorId, int jobId);
        Task<ResponseDTO> FindResponseById(int id);
        Task<ResponseDTO> FindResponseByJobId(int id);
        Task<IEnumerable<ResponseDTO>> FindResponseByUserExecutorId(int userId_Executor);
        Task<ResponseDTO> FindResponseByIdExecutorAndJobId(int userId_executor, int jobId);
        Task<IEnumerable<ResponseDTO>> GetAll();
        Task<IEnumerable<ResponseDTO>> GetAllResponseOfJob(int jobId);
        Task Update(ResponseDTO entity);
        Task Remove(int id);
        Task CommitAsync();
    }
}
