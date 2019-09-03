using Dapper;
using FreelanceService.DAL.Interfaces;
using FreelanceService.DAL.Repositories;
using System.Data;

namespace FreelanceService.DAL.Concrate
{
    public class UnitOfWork : IUnitOfWork
    {
        protected IDbTransaction _transaction;
        protected IDbContext _dbContext;

        public UnitOfWork(IDbConnection connection)
        {
            _transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            _dbContext = new DbContext();
         
            
        }


        private IUserRepository user;
        private IProjectRepository project;
        private ITaskRepository task;
        private ICategoryRepository category;
        private IReviewRepository review;
        private IResponseRepository response;

     

        public IProjectRepository ProjectRepos =>
            project ?? (project = new ProjectRepository(_dbContext));
        public ITaskRepository TaskRepos =>
          task ?? (task = new TaskRepository(_dbContext));
        public ICategoryRepository CategoryRepos =>
           category ?? (category = new CategoryRepository(_dbContext));
        public IReviewRepository ReviewRepos =>
           review ?? (review = new ReviewRepository(_dbContext));
        public IResponseRepository ResponseRepos =>
           response ?? (response = new ResponseRepository(_dbContext));
        public IUserRepository UserRepos =>
           user ?? (user = new UserRepository(_dbContext));


        public IDbTransaction Transaction =>
            _transaction;

      
        public void Commit()
        {
            try
            {
                foreach(var command in _dbContext.GetQueue())
                {
                    if(command is ExecureCommand execureCommand)
                    {
                        _transaction.Connection.Execute(execureCommand.Sql, execureCommand.Params);
                    }
                    else if (command is QueryCommand queryCommand)
                    {
                        _transaction.Connection.Query(queryCommand.Sql, queryCommand.Params);
                    }

                }
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
