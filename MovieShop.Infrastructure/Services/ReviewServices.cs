using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Services
{
    public class ReviewServices : IReviewService
    {
        private readonly IReviewRepository _repo;
        public ReviewServices(IReviewRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Review>> GetReviewsByMovieId(int movieId)
        {
            return await _repo.GetReviewsByMovieId(r=>r.MovieId==movieId);
        }
    }
}
