using FreelanceService.DAL.Interfaces;
using FreelanceService.DAL.Repositories;
using System.Data;

namespace FreelanceService.DAL.Concrate
{
    public class UnitOfWork : IUnitOfWork
    {
        protected IDbTransaction _transaction;

        public UnitOfWork(IDbConnection connection)
        {
            _transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            
        }

        public IDbTransaction Transaction =>
            _transaction;

      
        public void Commit()
        {
            try
            {
                _transaction.Commit();
                _transaction.Connection?.Close();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction.Connection?.Dispose();
                _transaction = null;
            }
        }

        public void Rollback()
        {
            try
            {
                _transaction.Rollback();
                _transaction.Connection?.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction.Connection?.Dispose();
                _transaction = null;
            }
        }
    }
}
