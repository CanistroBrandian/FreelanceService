using System.Data;

namespace FreelanceService.DAL.Interfaces
{
    public interface IUnitOfWork 
    {
        IDbTransaction Transaction { get; }
        void Commit();
        void Rollback();
    }
}
