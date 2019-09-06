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

        public override async Task Execute(IDbTransaction transaction)
        {
           await transaction.Connection.ExecuteAsync(Sql, Params, transaction);
        }
    }
}
