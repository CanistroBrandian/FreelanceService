using Dapper;
using FreelanceService.DAL.Interfaces;
using FreelanceService.DAL.Repositories;
using System.Collections.Generic;
using System.Data;

namespace FreelanceService.DAL.Concrate
{
    public class DbContext : IDbContext
    {
        //IDbTransaction _transaction;

        private List<Command> Queue { get; set; } = new List<Command>();

        //public DbContext(IDbTransaction transaction)
        //{
        //    _transaction = transaction;
        //}

        public void Execute(string sql, object param)
        {
            Queue.Add(new ExecureCommand(sql, param));
            //_transaction.Connection..Execute(sql, param);
        }

        public void Query(string sql, object param)
        {
            Queue.Add(new QueryCommand(sql, param));
            //_transaction.Connection.Query(sql, param);
        }

        public IReadOnlyList<Command> GetQueue()
        {
            return Queue.AsReadOnly();
        }

    }

    public abstract class Command
    {
        public Command(string sql, object parameters)
        {
            Sql = sql;
            Params = parameters;
        }
        public string Sql { get; private set; }

        public object Params { get; private set; }
    }

    public class ExecureCommand : Command
    {
        public ExecureCommand(string sql, object parameters) : base(sql, parameters)
        {
        }
    }

    public class QueryCommand : Command
    {
        public QueryCommand(string sql, object parameters) : base(sql, parameters)
        {
        }
    }
}
