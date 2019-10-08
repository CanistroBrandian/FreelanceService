using FreelanceService.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FreelanceService.DAL.Interfaces
{
    public interface IResponseRepository
    {
        Task AddResponse(Response entity, int userExecutorId, int jobId);
        Task <IEnumerable<Response>> GetAllResponseOfJob(int idJob);
        Task <IEnumerable<Response>> GetAll();
        Task<Response> FindResponseById(int id);
        Task<Response> FindResponseByJobId(int id);
        Task<Response> FindResponseByIdExecutorAndJobId(int userId_executor, int jobId);
        Task<IEnumerable<Response>> FindResponseByUserExecutorId(int id);
        Task Remove(int id);
        Task Update(Response entity);
    }
}
