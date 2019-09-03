using System.Data;

namespace FreelanceService.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IProjectRepository ProjectRepos { get; }
        ITaskRepository TaskRepos { get; }
        ICategoryRepository CategoryRepos { get; }
        IReviewRepository ReviewRepos { get; }
        IResponseRepository ResponseRepos { get; }
        IUserRepository UserRepos { get; }
        void Commit();
        void Rollback();
    }
}
