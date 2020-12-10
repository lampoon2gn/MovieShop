using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.API.Controllers
{
    //attribute based routing
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase//Controller works too, but contains more bloat
    {
        private readonly IMovieService _movieService;
        private readonly IReviewService _reviewService;

        public MoviesController(IMovieService movieService, IReviewService reviewService)
        {
            _movieService = movieService;
            _reviewService = reviewService;
        }

        //api/movies/toprevenue
        [HttpGet]
        [Route("toprevenue")]
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            //call our service and call the method

            var movies = await _movieService.GetTopRevenueMovies();
            

            //remember to return http status code

            if (!movies.Any())
            {
                return NotFound("No movies found");
            }
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {

            var movie = await _movieService.GetMovieAsync(id);


            //remember to return http status code

            if (movie==null)
            {
                return NotFound("Something went wrong looking for movie with this id");
            }
            return Ok(movie);
        }

        [HttpGet]
        [Route("toprated")]
        public async Task<IActionResult> GetTopRatedMovies()
        {

            var movie = await _movieService.GetTopRatedMovies();


            //remember to return http status code

            if (movie == null)
            {
                return NotFound("Something went wrong looking top rated movies");
            }
            return Ok(movie);
        }

        [HttpGet]
        [Route("genre/{genreid:int}")]
        public async Task<IActionResult> GetMovieByGenre(int genreid)
        {
            //call our service and call the method

            var movies = await _movieService.GetMoviesByGenre(genreid);


            //remember to return http status code

            if (movies==null)
            {
                return NotFound("No movies found in this genre");
            }
            return Ok(movies);
        }

        [HttpGet]
        [Route("{movieid:int}/reviews")]
        public async Task<IActionResult> GetReviewsByMovieId(int movieid)
        {
            //call our service and call the method

            var movies = await _reviewService.GetReviewsByMovieId(movieid);


            //remember to return http status code

            if (!movies.Any())
            {
                return NotFound("No movies found in this genre");
            }
            return Ok(movies);
        }
    }
}
