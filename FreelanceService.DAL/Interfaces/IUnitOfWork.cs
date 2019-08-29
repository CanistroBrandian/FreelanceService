using System;
using System.Data;

namespace FreelanceService.DAL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void Begin();
        void Commit();
        void Rollback();
    }
}
