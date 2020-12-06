using MovieShop.Core.Entities;
using MovieShop.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Core.RepositoryInterfaces
{
    public interface IMovieRepository : IAsyncRepository<Movie>
    {
        Task<IEnumerable<TopRatedMoviesResponseModel>> GetTopRatedMovies();
        Task<IEnumerable<Movie>> GetMoviesByGenre(Expression<Func<Movie, bool>> filter);
        Task<IEnumerable<Movie>> GetHighestRevenueMovies();
        Task<Movie> GetByNameAsync(string title);
        Task<IEnumerable<TopPurchasedResponseModel>> GetTopPurchasedAsync();
        

    }
}
