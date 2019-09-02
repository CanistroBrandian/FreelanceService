using FreelanceService.DAL.Interfaces;
using FreelanceService.DAL.Repositories;

namespace FreelanceService.DAL.Concrate
{
    public class DbContext : IDbContext
    {
        private IUnitOfWorkFactory unitOfWorkFactory;

        private UnitOfWork unitOfWork;

        private UserRepository user;
        private ProjectRepository project;
        private TaskRepository task;
        private CategoryRepository category;
        private ReviewRepository review;
        private ResponseRepository response;

        public DbContext(IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        public ProjectRepository ProjectRepos =>
            project ?? (project = new ProjectRepository(UnitOfWork));
        public TaskRepository TaskRepos =>
          task ?? (task = new TaskRepository(UnitOfWork));
        public CategoryRepository CategoryRepos =>
           category ?? (category = new CategoryRepository(UnitOfWork));
        public ReviewRepository ReviewRepos =>
           review ?? (review = new ReviewRepository(UnitOfWork));
        public ResponseRepository ResponseRepos =>
           response ?? (response = new ResponseRepository(UnitOfWork));
        public UserRepository UserRepos =>
           user ?? (user = new UserRepository(UnitOfWork));

  

        protected UnitOfWork UnitOfWork =>
            unitOfWork ?? (unitOfWork = unitOfWorkFactory.Create());


        public void Commit()
        {
            try
            {
                UnitOfWork.Commit();
            }
            finally
            {
                Reset();
            }
        }

        public void Rollback()
        {
            try
            {
                UnitOfWork.Rollback();
            }
            finally
            {
                Reset();
            }
        }

        private void Reset()
        {
            unitOfWork = null;
            user = null;
        }
    }
}
