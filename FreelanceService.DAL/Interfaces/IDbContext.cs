using FreelanceService.DAL.Concrate;
using FreelanceService.DAL.Repositories;
using System.Collections.Generic;

namespace FreelanceService.DAL.Interfaces
{
    public interface IDbContext
    {
        void Execute(string sql, object param);
        void Query(string sql, object param);

        IReadOnlyList<Command> GetQueue();
    }
}
