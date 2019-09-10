using FreelanceService.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FreelanceService.DAL.Interfaces
{
    public interface IReviewRepository
    {
        Task AddReview(Review entity);
        Task<IEnumerable<Review>> GetAll();
        Task<Review> FindById(int id);
        Task Remove(int id);
        Task Update(Review entity);
    }
}
