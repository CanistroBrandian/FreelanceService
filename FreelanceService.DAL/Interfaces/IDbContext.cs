using FreelanceService.DAL.Concrate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelanceService.DAL.Interfaces
{
    public interface IDbContext
    {
        Task Execute(string sql, object param = null);
        Task<IEnumerable<T>> Query<T>(string sql, object param = null) where T : class;
        Task<T> QueryFirst<T>(string sql, object param = null) where T : class;
        IReadOnlyList<ICommand> GetQueue();
    }
}
