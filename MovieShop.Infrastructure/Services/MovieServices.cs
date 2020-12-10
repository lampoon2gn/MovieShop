using AutoMapper;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Services
{
    public class MovieServices : IMovieService
    {
        //readonly ensures it will not be changed outside the constructor, so new will not be accidentally used
        private readonly IMovieRepository _repo;
        private readonly IAsyncRepository<Genre> _genreRepo;
        private readonly IAsyncRepository<Cast> _castRepo;
        private readonly IMapper _mapper;

        //this is constructor injection
        public MovieServices(IAsyncRepository<Cast> castRepo, IMovieRepository repository, IAsyncRepository<Genre> genreRepo, IMapper mapper)
        {
            //new is convinient to use, but we need to avoid it to promote decoupling
            //using DI, its easier to make sure we dont break the existing code
            //_repository = new MovieRepository(new MovieShopDbContext(options:null));
            _repo = repository;
            _genreRepo = genreRepo;
            _mapper = mapper;
            _castRepo = castRepo;
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
            //if (movie == null) throw new NotFoundException("Movie", id);
            //var favoritesCount = await _favoriteRepository.GetCountAsync(f => f.MovieId == id);
            var response = new MovieDetailsResponseModel
            {
                Id = movie.Id,
                Title = movie.Title,
                PosterUrl = movie.PosterUrl,
                BackdropUrl = movie.BackdropUrl,
                //Rating =,
                Overview = movie.Overview,
                Tagline = movie.Tagline,
                Budget = movie.Budget,
                Revenue = movie.Revenue,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl,
                ReleaseDate = movie.ReleaseDate,
                RunTime = movie.RunTime,
                Price = movie.Price,
                Genres = movie.Genres.ToList()
                
                //FavoriteCount = ;

            };
            var castList = new List<MovieDetailsResponseModel.CastResponseModel>();
            foreach (var cast in movie.MovieCasts)
            {
                var c = await _castRepo.GetByIdAsync(cast.CastId);
                var responseModel = new MovieDetailsResponseModel.CastResponseModel();
                PropertyCopy.Copy(responseModel,c);
                castList.Add(responseModel);
            }

            response.Casts = castList;
            //response.FavoritesCount = favoritesCount;
            return response;

        }

        public async Task<IEnumerable<MovieResponseModel>> GetMoviesByGenre(int genreId)
        {
            var genre = await _genreRepo.GetByIdAsync(genreId);
            var movies = await _repo.GetMoviesByGenre(m => m.Genres.Contains(genre));
            var res = new List<MovieResponseModel>();
            foreach (var movie in movies)
            {
                var t = new MovieResponseModel();
                PropertyCopy.Copy(t, movie);
                res.Add(t);

            }

            
            return res;
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
            var updateResult = await _repo.UpdateAsync(movie);
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
