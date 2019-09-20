using FreelanceService.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceService.BLL.Interfaces
{
   public interface IResponseService
    {
        Task AddResponse(ResponseDTO entity);
        Task<ResponseDTO> FindResponseById(int id);
        Task<IEnumerable<ResponseDTO>> GetAll();
        Task Update(ResponseDTO entity);
        Task CommitAsync();
    }
}
