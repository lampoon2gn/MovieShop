using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.Web.Controllers
{
    public class MovieController : Controller
    {
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
    }
}
