using AutoMapper;
using FreelanceService.BLL.DTO;
using FreelanceService.BLL.Interfaces;
using FreelanceService.DAL.Entities;
using FreelanceService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceService.BLL.Services
{
   public class ReviewService:IReviewService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ReviewService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task AddReview(ReviewDTO entity)
        {
            var review = _mapper.Map<ReviewDTO, Review>(entity);
            await _uow.ReviewRepos.AddReview(review);

        }


        public async Task<ReviewDTO> FindReviewById(int id)
        {
            if (id == 0)
                throw new Exception("Поле Id не введено");
            var entity = await _uow.ReviewRepos.FindById(id);
            return _mapper.Map<Review, ReviewDTO>(entity);
        }

        public async Task<IEnumerable<ReviewDTO>> GetAll()
        {
            var result = _mapper.Map<IEnumerable<Review>, IEnumerable<ReviewDTO>>(await _uow.ReviewRepos.GetAll());
            return result;
        }

        public async Task Update(ReviewDTO entity)
        {

            var review = _mapper.Map<ReviewDTO, Review>(entity);
            await _uow.ReviewRepos.Update(review);
        }

        public async Task Remove(ResponseDTO entity)
        {
            await _uow.ReviewRepos.Remove(entity.Id);
        }

        public async Task CommitAsync()
        {
            await _uow.CommitAsync();
        }
    }
}
