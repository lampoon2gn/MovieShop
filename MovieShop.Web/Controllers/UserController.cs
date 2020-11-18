using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.Web.Controllers
{
    public class UserController : Controller
    {
        /*public IActionResult Index()
        {
            return View();
        }*/

        /// <summary>
        /// Insert user into system
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IActionResult Create(object user)
        {
            return View();
        }

        /// <summary>
        /// Insert user details into system
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Details(int userId)
        {
            return View();
        }
    }
}
