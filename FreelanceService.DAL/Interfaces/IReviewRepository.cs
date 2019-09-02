using FreelanceService.DAL.Entities;
using System.Collections.Generic;

namespace FreelanceService.DAL.Interfaces
{
    public interface IReviewRepository
    {
        void AddReview(Review entity);
        IEnumerable<Review> GetAll();
        Review Find(int id);
        void Remove(int id);
        void Update(Review entity);
    }
}
