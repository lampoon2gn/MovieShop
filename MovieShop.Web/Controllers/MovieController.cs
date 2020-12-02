using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Models.Request;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.Web.Controllers
{
    public class MovieController : Controller
    {

        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        /// <summary>
        /// Returns top 20 movies
        /// </summary>
        /// <returns>List of 20 movies</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// List movies in a certain genre
        /// </summary>
        /// <param name="genreId"></param>
        /// <returns>List of movies</returns>
        public IActionResult GetMovieByGenre(int genreId)
        {
            return View();
        }

        /// <summary>
        /// Return details of a certain movie
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns>A single movie</returns>
        public IActionResult Details(int movieId)
        {
            return View();
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddMovie()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie(MovieCreateRequestModel model)
        {
            if (ModelState.IsValid)
            {
                //send the model to our service
                await _movieService.CreateMovie(model);
            }
            return View();
        }
    }
}
