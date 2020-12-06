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
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetails(int id)
        {
            var detailResponse = await _userService.GetUserDetails(id);
            if (detailResponse != null)
            {
                return Ok(detailResponse);
            }
            return BadRequest(new { message = "Incorrect User ID" });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser(UserRegisterRequestModel req)
        {
            if (ModelState.IsValid)
            {
                await _userService.CreateUser(req);
                return Ok();
            }
            return BadRequest(new { message = "Please correct the input information"});
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser(UserLoginRequestModel req)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _userService.ValidateUser(req.Email,req.Password);
                if(loginResult == null)
                {
                    return BadRequest(new { message = "Please check your credentials" });
                }
                return Ok(loginResult);
            }
            return BadRequest(new { message = "Please check your credentials" });
        }


    }
}
