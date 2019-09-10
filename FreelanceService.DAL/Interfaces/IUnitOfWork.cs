using System.Data;
using System.Threading.Tasks;

namespace FreelanceService.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IProjectRepository ProjectRepos { get; }
        IJobRepository JobRepos { get; }
        ICategoryRepository CategoryRepos { get; }
        IReviewRepository ReviewRepos { get; }
        IResponseRepository ResponseRepos { get; }
        IUserRepository UserRepos { get; }
        Task CommitAsync();
        void Rollback();
    }
}
