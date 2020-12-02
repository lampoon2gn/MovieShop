using MovieShop.Core.Entities;
using MovieShop.Core.Models.Request;
using MovieShop.Core.Models.Response;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using MovieShop.Infrastructure.Data;
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

        //this is constructor injection
        public MovieServices(IMovieRepository repository)
        {
            //new is convinient to use, but we need to avoid it to promote decoupling
            //using DI, its easier to make sure we dont break the existing code
            //_repository = new MovieRepository(new MovieShopDbContext(options:null));
            _repo = repository;
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
            return new MovieDetailsResponseModel();

        }

        public Task<IEnumerable<MovieResponseModel>> GetMoviesByGenre(int genreId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetMoviesCount(string title = "")
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReviewMovieResponseModel>> GetReviewsForMovie(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MovieResponseModel>> GetTopRatedMovies()
        {
            throw new NotImplementedException();
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
                    ReleaseDate = movie.ReleaseDate.Value,
                    Title = movie.Title
                });
            }
            return movieResponseModel;
        }

        public Task<MovieDetailsResponseModel> UpdateMovie(MovieCreateRequestModel movieCreateRequest)
        {
            throw new NotImplementedException();
        }
    }
}
