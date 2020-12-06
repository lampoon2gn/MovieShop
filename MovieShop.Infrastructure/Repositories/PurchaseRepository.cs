using Microsoft.EntityFrameworkCore;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Repositories
{
    public class PurchaseRepository : EfRepository<Purchase>,IPurchaseRepository
    {
        public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public Task<IEnumerable<Purchase>> GetAllPurchases(int pageSize = 30, int pageIndex = 0)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Purchase>> GetAllPurchasesByMovie(int movieId, int pageSize = 30, int pageIndex = 0)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Purchase>> GetLatestPurchasedAsync()
        {
            var movie = await _dbContext.Purchases
                                        .Where(m => m.PurchaseDateTime >= DateTime.Now.Date.AddDays(-7))
                                        .Take(20)
                                        .OrderByDescending(m => m.PurchaseDateTime).ToListAsync();


            if (movie == null) return null;

            //if (movieRating > 0) movie.Rating = movieRating;
            return movie;
        }
    }
}
