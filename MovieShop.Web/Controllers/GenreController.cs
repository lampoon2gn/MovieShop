using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.Web.Controllers
{
    public class GenreController : Controller
    {
        /// <summary>
        /// Return a list of all movie genres
        /// </summary>
        /// <returns>List of genres</returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}
