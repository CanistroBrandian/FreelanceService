using Dapper;
using FreelanceService.DAL.Interfaces;
using FreelanceService.DAL.Repositories;
using System.Collections.Generic;
using System.Data;
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

    public interface ICommand
    {
        void Execute(IDbTransaction transaction);
    }

    public abstract class Command : ICommand
    {
        public Command(string sql, object parameters)
        {
            Sql = sql;
            Params = parameters;
        }
        public string Sql { get; private set; }

        public object Params { get; private set; }

        public abstract void Execute(IDbTransaction transaction);
    }

    public class ExecuteCommand : Command
    {
        public ExecuteCommand(string sql, object parameters) : base(sql, parameters)
        {
        }

        public override void Execute(IDbTransaction transaction)
        {
            transaction.Connection.Execute(Sql, Params, transaction);
        }
    }
}
