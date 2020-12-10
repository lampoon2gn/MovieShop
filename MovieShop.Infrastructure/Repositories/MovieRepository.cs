using Microsoft.EntityFrameworkCore;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Response;
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
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<TopRatedMoviesResponseModel>> GetTopRatedMovies()
        {
            var topRatedMovies = await _dbContext.Reviews.Include(m => m.Movie)
                                                 .GroupBy(r => new
                                                 {
                                                     Id = r.MovieId,
                                                     r.Movie.PosterUrl,
                                                     r.Movie.Title,
                                                     r.Movie.ReleaseDate
                                                 })
                                                 .OrderByDescending(g => g.Average(m => m.Rating))
                                                 .Select(m => new TopRatedMoviesResponseModel
                                                 {
                                                     Id = m.Key.Id,
                                                     PosterUrl = m.Key.PosterUrl,
                                                     Title = m.Key.Title,
                                                     ReleaseDate = m.Key.ReleaseDate,
                                                     Rating = m.Average(x => x.Rating)
                                                 })
                                                 .Take(50)
                                                 .ToListAsync();

            return topRatedMovies;
        }
        /*public async Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId)
        {
            var movies = await _
        }*/
        public async Task<IEnumerable<Movie>> GetHighestRevenueMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(50).ToListAsync();
            //skip 10 and take 50
            //in sql-> offset 10 and fetch 50 next rows
            return movies;
        }

        public override async Task<Movie> GetByIdAsync(int id)
        {
            var movie = await _dbContext.Movies
                                        .Include(m => m.MovieCasts).ThenInclude(m => m.Cast)
                                        .Include(m => m.Genres)//.ThenInclude(m => m.Name)
                                        .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null) return null;
            var movieRating = await _dbContext.Reviews.Where(r => r.MovieId == id).DefaultIfEmpty()
                                              .AverageAsync(r => r == null ? 0 : r.Rating);
            //if (movieRating > 0) movie.Rating = movieRating;
            return movie;
        }
        public async Task<Movie> GetByNameAsync(string title)
        {
            var movie = await _dbContext.Movies
                                        .FirstOrDefaultAsync(m => m.Title == title);
            if (movie == null) return null;
     
            //if (movieRating > 0) movie.Rating = movieRating;
            return movie;
        }

        public async Task<IEnumerable<TopPurchasedResponseModel>> GetTopPurchasedAsync()
        {
            /*var topPurchasedMovies = await _dbContext.Purchases.Include(m => m.Movie)
                                                     .GroupBy(r => new {
                                                         Id = r.MovieId,
                                                         r.Movie.PosterUrl,
                                                         r.Movie.Title,
                                                         r.Movie.ReleaseDate
                                                     })
                                                     .OrderByDescending(g => g.Count(m => m.))
                                                     .Select(m => new Movie
                                                     {
                                                         Id = m.Key.Id,
                                                         PosterUrl = m.Key.PosterUrl,
                                                         Title = m.Key.Title,
                                                         ReleaseDate = m.Key.ReleaseDate,
                                                         Purchases = m.Count(x => x.MovieId)
                                                     })
                                                     .Take(20)
                                                     .ToListAsync();*/
            return (from movie in _dbContext.Movies
                    from purchase in _dbContext.Purchases
                    where purchase.MovieId == movie.Id
                    group purchase by movie into movieGroups
                    select new TopPurchasedResponseModel
                    {
                        movie = movieGroups.Key,
                        numOfPurchases = movieGroups.Count()
                    }
                    ).OrderByDescending(x => x.numOfPurchases).Distinct().Take(20);
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenre(Expression<Func<Movie, bool>> filter)
        {
            return await _dbContext.Set<Movie>().Where(filter).Include(m=>m.Genres).ToListAsync();
        }

        
    }
}
