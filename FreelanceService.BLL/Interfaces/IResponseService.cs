using FreelanceService.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelanceService.BLL.Interfaces
{
    public interface IResponseService
    {
        Task AddResponse(ResponseDTO entity, int userExecutorId, int jobId);
        Task<ResponseDTO> FindResponseById(int id);
        Task<IEnumerable<ResponseDTO>> GetAll();
        Task Update(ResponseDTO entity);
        Task CommitAsync();
    }
}
