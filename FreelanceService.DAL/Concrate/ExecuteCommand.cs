using Dapper;
using System.Data;
using System.Threading.Tasks;

namespace FreelanceService.DAL.Concrate
{
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
