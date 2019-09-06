using FreelanceService.DAL.Interfaces;
using System.Data;

namespace FreelanceService.DAL.Concrate
{
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
}
