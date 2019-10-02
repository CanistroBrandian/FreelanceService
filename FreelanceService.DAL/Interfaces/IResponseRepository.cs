using FreelanceService.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FreelanceService.DAL.Interfaces
{
    public interface IResponseRepository
    {
        Task AddResponse(Response entity, int userExecutorId, int jobId);
        Task <IEnumerable<Response>> GetAll();
        Task<Response> FindById(int id);
        Task Remove(int id);
        Task Update(Response entity);
    }
}
