using Microsoft.AspNetCore.Authorization;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        //private readonly IReviewService _reviewService;

        public UserController(IUserService userService)
        {
            _userService = userService;
            //_reviewService = reviewService;
        }

        [Authorize]
        [HttpPost]
        [Route("purchase")]
        public async Task<IActionResult> MakePurchase(PurchaseRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.PurchaseMovie(model);
                return Ok();
            }
            return BadRequest("Please check the info you entered");
        }

        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> MarkFavorite(FavoriteRequestModel model)
        {
            if (ModelState.IsValid)
            {
                await _userService.AddFavorite(model);
                return Ok();
            }
            return BadRequest("Please check the info you entered");
        }
        [HttpPost]
        [Route("unfavorite")]
        public async Task<IActionResult> Unfavorite(FavoriteRequestModel model)
        {
            if (ModelState.IsValid)
            {
                await _userService.RemoveFavorite(model);
                return Ok();
            }
            return BadRequest("Please check the info you entered");
        }
        [HttpGet]
        [Route("{userid:int}/movie/{movieid:int}/favorite")]
        public async Task<IActionResult> CheckFavorite(int userid, int movieid)
        {
            var res = await _userService.FavoriteExists(userid, movieid);

            return Ok(res);
        }

        [HttpPost]
        [Route("review")]
        public async Task<IActionResult> UserPostReview(ReviewRequestModel model)
        {
            if (ModelState.IsValid)
            {
                await _userService.AddMovieReview(model);
                return Ok();
            }
            return BadRequest("Please check the info you entered");
        }
        [HttpPut]
        [Route("review")]
        public async Task<IActionResult> UserUpdateReview(ReviewRequestModel model)
        {
            if (ModelState.IsValid)
            {
                await _userService.UpdateMovieReview(model);
                return Ok();
            }
            return BadRequest("Please check the info you entered");
        }

        [HttpDelete]
        [Route("{userid:int}/movie/{movieid:int}")]
        public async Task<IActionResult> UserDeleteReview(int userid, int movieid)
        {
            if (ModelState.IsValid)
            {
                await _userService.DeleteMovieReview(userid, movieid) ;
                return Ok();
            }
            return BadRequest("Please check the info you entered");
        }

        [HttpGet]
        [Route("{id:int}/purchases")]
        public async Task<IActionResult> GetAllMoviesPurchasedByUser(int id)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.GetAllPurchasesForUser(id) ;
                return Ok(res);
            }
            return BadRequest("Please check the info you entered");
        }

        [HttpGet]
        [Route("{id:int}/favorites")]
        public async Task<IActionResult> GetAllMoviesFavoritedByUser(int id)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.GetAllFavoritesForUser(id);
                return Ok(res);
            }
            return BadRequest("Please check the info you entered");
        }

        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> GetAllReviewsByUser(int id)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.GetAllReviewsByUser (id);
                return Ok(res);
            }
            return BadRequest("Please check the info you entered");
        }

    }
}
