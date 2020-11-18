using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.Web.Controllers
{
    public class CastController : Controller
    {

        /*public IActionResult Index()
        {
            return View();
        }*/

        /// <summary>
        /// Returns details of a certain cast
        /// </summary>
        /// <param name="castId"></param>
        /// <returns></returns>
        public IActionResult Details(int castId)
        {
            return View();
        }
    }
}
