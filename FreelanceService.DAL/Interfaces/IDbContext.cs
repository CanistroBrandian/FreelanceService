using FreelanceService.DAL.Concrate;
using System.Collections.Generic;

namespace FreelanceService.DAL.Interfaces
{
    public interface IDbContext
    {
        void Execute(string sql, object param = null);
        IEnumerable<T> Query<T>(string sql, object param = null) where T : class;
        IReadOnlyList<ICommand> GetQueue();
    }
}
