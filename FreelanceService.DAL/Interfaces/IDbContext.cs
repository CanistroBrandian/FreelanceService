using FreelanceService.DAL.Repositories;

namespace FreelanceService.DAL.Interfaces
{
    public interface IDbContext
    {
        UserRepository UserRepos { get; }
        ProjectRepository ProjectRepos { get; }
        CategoryRepository CategoryRepos { get; }
        ResponseRepository ResponseRepos { get; }
        ReviewRepository ReviewRepos { get; }
        TaskRepository TaskRepos { get; }
        void Commit();
        void Rollback();
    }
}
