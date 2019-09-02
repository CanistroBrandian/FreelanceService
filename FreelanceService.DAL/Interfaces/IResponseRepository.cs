using FreelanceService.DAL.Entities;
using System.Collections.Generic;

namespace FreelanceService.DAL.Interfaces
{
    public interface IResponseRepository
    {
        void AddResponse(Response entity);
        IEnumerable<Response> GetAll();
        Response Find(int id);
        void Remove(int id);
        void Update(Response entity);
    }
}
