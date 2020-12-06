using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Models.Request;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IPurchaseService _purchaseService;
        public AdminController(IMovieService movieService, IPurchaseService purchaseService)
        {
            _movieService = movieService;
            _purchaseService = purchaseService;
        }
        [HttpPost]
        [Route("movie")]
        public async Task<IActionResult> CreateMovieByAdminAsync(MovieCreateRequestModel model)
        {
            if (ModelState.IsValid)
            {
                //send the model to our service
                var response = await _movieService.CreateMovie(model);
                if (response == null)
                {
                    return BadRequest("Something went wrong creating the movie");
                }
                return Ok(response);
            }
            return BadRequest("Please check the info you provided");
        }

        [HttpPut]
        [Route("movie")]
        public async Task<IActionResult> UpdateMovieByAdminAsync(MovieCreateRequestModel model)
        {
            if (ModelState.IsValid)
            {
                //send the model to our service
                var response = await _movieService.UpdateMovie(model);
                if (response == null)
                {
                    return BadRequest("Something went wrong updating the movie");
                }
                return Ok(response);
            }
            return BadRequest("Please check the info you provided");
        }

        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> GetLatestPurchasedMoviesByAdminAsync()
        {
            var response = await _purchaseService.GetLatestPurchasedAsync();
            if (response == null)
            {
                return BadRequest("Something went wrong updating the movie");
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("top")]
        public async Task<IActionResult> GetTopPurchasedMoviesByAdminAsync()
        {
            var response = await _movieService.GetTopPurchasedAsync();
            if (response == null)
            {
                return BadRequest("Something went wrong updating the movie");
            }
            return Ok(response);
        }
    }
}
