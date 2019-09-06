using Dapper;
using FreelanceService.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FreelanceService.DAL.Concrate
{
    public class DbContext : IDbContext
    {
        private List<ICommand> Queue { get; set; } = new List<ICommand>();

        private readonly string _connectionString;

        public DbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task Execute(string sql, object param = null)
        {
            Queue.Add(new ExecuteCommand(sql, param));

        }

        public async Task<IEnumerable<T>> Query<T>(string sql, object param = null) where T : class
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                return await _connection.QueryAsync<T>(sql, param);
            }
        }
        public async Task<T> QueryFirst<T>(string sql, object param = null) where T : class
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                return await _connection.QueryFirstOrDefaultAsync<T>(sql, param);
            }
        }

        public IReadOnlyList<ICommand> GetQueue()
        {
            return Queue.AsReadOnly();
        }

    }

}
