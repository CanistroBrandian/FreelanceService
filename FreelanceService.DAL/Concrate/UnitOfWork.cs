using FreelanceService.DAL.Interfaces;
using FreelanceService.DAL.Repositories;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FreelanceService.DAL.Concrate
{
    public class UnitOfWork : IUnitOfWork
    {
        protected IDbContext _dbContext;
        private readonly string _connectionString;
        public UnitOfWork(string connectionString, IDbContext dbContext)
        {
            _dbContext = dbContext;
            _connectionString = connectionString;
        }
        private IUserRepository user;
        private IProjectRepository project;
        private IJobRepository job;
        private ICategoryRepository category;
        private IReviewRepository review;
        private IResponseRepository response;

        public IProjectRepository ProjectRepos =>
            project ?? (project = new ProjectRepository(_dbContext));
        public IJobRepository JobRepos =>
          job ?? (job = new JobRepository(_dbContext));
        public ICategoryRepository CategoryRepos =>
           category ?? (category = new CategoryRepository(_dbContext));
        public IReviewRepository ReviewRepos =>
           review ?? (review = new ReviewRepository(_dbContext));
        public IResponseRepository ResponseRepos =>
           response ?? (response = new ResponseRepository(_dbContext));
        public IUserRepository UserRepos =>
           user ?? (user = new UserRepository(_dbContext));
      
        public async Task CommitAsync()
        {
            using (var _connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                var _transaction = _connection.BeginTransaction();
                try
                {
                    foreach (var command in _dbContext.GetQueue())
                    {
                       await command.ExecuteAsync(_transaction);

                    }
                    _transaction.Commit();
                    _connection.Close();
                }
                catch
                {
                    _transaction.Rollback();
                    throw;
                }
                finally
                {
                    _connection.Close();
                    _transaction?.Dispose();
                }
            }           
        }

        public void Rollback()
        {
            //    var _transaction = _connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            //    try
            //    {
            //        _transaction.Rollback();
            //        _transaction.Connection?.Close();
            //    }
            //    catch
            //    {
            //        throw;
            //    }
            //    finally
            //    {
            //        _transaction?.Dispose();
            //        _transaction.Connection?.Dispose();
            //        _transaction = null;
            //    }
            //}
        }
    }
}
