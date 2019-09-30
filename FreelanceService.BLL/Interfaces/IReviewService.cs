using FreelanceService.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelanceService.BLL.Interfaces
{
    public interface IReviewService
    {
        Task AddReview(ReviewDTO entity);
        Task<ReviewDTO> FindReviewById(int id);
        Task<IEnumerable<ReviewDTO>> GetAll();
        Task Update(ReviewDTO entity);
        Task CommitAsync();
    }
}
