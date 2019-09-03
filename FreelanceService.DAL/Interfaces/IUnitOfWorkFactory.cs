using FreelanceService.DAL.Concrate;

namespace FreelanceService.DAL.Interfaces
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
