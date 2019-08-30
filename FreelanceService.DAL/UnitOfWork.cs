using FreelanceService.DAL.Interfaces;
using System.Data;

namespace FreelanceService.DAL
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IDbConnection connection)
        {
            _connection = connection;
        }

        IDbConnection _connection = null;
        IDbTransaction _transaction = null;

        IDbConnection IUnitOfWork.Connection
        {
            get { return _connection; }
        }
        IDbTransaction IUnitOfWork.Transaction
        {
            get { return _transaction; }
        }

        public void Begin()
        {
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();
            _transaction = null;
            _connection.Close();
        }
    }

}
