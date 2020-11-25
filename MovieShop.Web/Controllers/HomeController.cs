using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieShop.Core.ServiceInterfaces;
using MovieShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;
        public HomeController(ILogger<HomeController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetTopRevenueMovies();

            return View(movies);

            //var testdata = "list of movies";
            //ViewBag.myproperty = testdata;//viewbag is sent to view automatically,viewbag is dynamic, so no intellisense
            //return View();
            
            //pass data from controller to vie
            //viewbag->dynamic type
            //viewdata->object type
            //strongly typed models

            //when view is returned, by default, the view with the same name as the action method will be return
            //for example, here, Views/Home/Index.cshtml will be returned
            //you can do return View("View_name") to sepecify which view to return

            //HttpContext will provide you with all the info regarding your HTTP Request

            //controllers will call services => repo
            //navigation => list of genres as a dropdown
            //showing top 20 highest revenue movies as movie cardss...with images(card in bootstrap)
            //need card image, movie id/title and maybe rating
            //movie entity has all the above but also more unecessary
            //Models can be used to solve this based on your ui/api requirements.
            //we call them models/viewmodles in MVC
            //DTO- data transfer objects in API

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
