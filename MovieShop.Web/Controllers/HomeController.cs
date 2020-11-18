using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(); //when view is returned, by default, the view with the same name as the action method will be return
                           //for example, here, Views/Home/Index.cshtml will be returned
                           //you can do return View("View_name") to sepecify which view to return

                           //HttpContext will provide you with all the info regarding your HTTP Request
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
