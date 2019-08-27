using System.Data;

namespace FreelanceService.DAL.Repositories
{
    public abstract class RepositoryBase
    {
      //  protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection Connection { get; }

        public RepositoryBase(IDbConnection connection)
        {
            Connection = connection;
        }
    }
}
