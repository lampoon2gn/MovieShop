using MovieShop.Core.Entities;
using MovieShop.Core.Models.Request;
using MovieShop.Core.Models.Response;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using MovieShop.Infrastructure.Data;
using MovieShop.Infrastructure.Exceptions;
using MovieShop.Infrastructure.Helper;
using MovieShop.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Services
{
    public class MovieServices : IMovieService
    {
        //readonly ensures it will not be changed outside the constructor, so new will not be accidentally used
        private readonly IMovieRepository _repo;
        private readonly IAsyncRepository<Genre> _genreRepo;

        //this is constructor injection
        public MovieServices(IMovieRepository repository, IAsyncRepository<Genre> genreRepo)
        {
            //new is convinient to use, but we need to avoid it to promote decoupling
            //using DI, its easier to make sure we dont break the existing code
            //_repository = new MovieRepository(new MovieShopDbContext(options:null));
            _repo = repository;
            _genreRepo = genreRepo;
        }

        public async Task<MovieDetailsResponseModel> CreateMovie(MovieCreateRequestModel movieCreateRequest)
        {

            //check movie doesn't already exist
            var dbMovie = await _repo.GetByNameAsync(movieCreateRequest.Title);
            if (dbMovie != null && string.Equals(dbMovie.Title, movieCreateRequest.Title, StringComparison.CurrentCultureIgnoreCase))
                throw new Exception("Movie Already Exits");

            //proceed to add the movie
            var movie = new Movie();
            //add content from request to the entity
            PropertyCopy.Copy(movie, movieCreateRequest);

            var createdMovie = await _repo.AddAsync(movie);
            var response = new MovieDetailsResponseModel();
            PropertyCopy.Copy(response, createdMovie);
            return response;
        }

        public async Task<MovieDetailsResponseModel> GetMovieAsync(int id)
        {
            var movie = await _repo.GetByIdAsync(id);
            var res = new MovieDetailsResponseModel();
            PropertyCopy.Copy(res, movie);
            return res;

        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId)
        {
            var genre = await _genreRepo.GetByIdAsync(genreId);
            var movies = await _repo.GetMoviesByGenre(m => m.Genres.Contains(genre));
            
            return movies;
        }

        public Task<int> GetMoviesCount(string title = "")
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReviewMovieResponseModel>> GetReviewsForMovie(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TopRatedMoviesResponseModel>> GetTopRatedMovies()
        {
            var movies = await _repo.GetTopRatedMovies();
            return movies;

        }

        public async Task<IEnumerable<MovieResponseModel>> GetTopRevenueMovies()
        {
            var movies = await _repo.GetHighestRevenueMovies();
            // Map our Movie Entity to MovieResponseModel
            var movieResponseModel = new List<MovieResponseModel>();
            foreach (var movie in movies)
            {
                movieResponseModel.Add(new MovieResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    //ReleaseDate = movie.ReleaseDate.Value,
                    Title = movie.Title
                });
            }
            return movieResponseModel;
        }

        public async Task<MovieDetailsResponseModel> UpdateMovie(MovieCreateRequestModel movieCreateRequest)
        {
            //find the movie
            var movie = new Movie();
            PropertyCopy.Copy(movie,movieCreateRequest);
            var updateResult = _repo.UpdateAsync(movie);
            var movieResult = new MovieDetailsResponseModel();
            PropertyCopy.Copy(movieResult, updateResult);

            return movieResult;
        }

        public async Task<IEnumerable<TopPurchasedResponseModel>> GetTopPurchasedAsync()
        {
            return await _repo.GetTopPurchasedAsync();
        }

        public Task<IEnumerable<TopPurchasedResponseModel>> GetReviewsByMovieId(int movieid)
        {
            throw new NotImplementedException();
        }


    }
}
