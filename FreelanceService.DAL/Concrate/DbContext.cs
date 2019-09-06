using Dapper;
using FreelanceService.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;

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

        public void Execute(string sql, object param = null)
        {
            Queue.Add(new ExecuteCommand(sql, param));

        }

        public IEnumerable<T> Query<T>(string sql, object param = null) where T: class
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                return _connection.Query<T>(sql, param);
            }   
        }

        public IReadOnlyList<ICommand> GetQueue()
        {
            return Queue.AsReadOnly();
        }

    }

}
