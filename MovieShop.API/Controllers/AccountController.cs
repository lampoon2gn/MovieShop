using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieShop.Core.Models.Request;
using MovieShop.Core.Models.Response;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        public AccountController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
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
                    return Unauthorized(new { message = "Please check your credentials" });
                }
                //success, generate JWT here
                var token = GenerateJWT(loginResult);
                return Ok(new { token });
            }
            return BadRequest(new { message = "Please check the info you entered" });
        }

        private string GenerateJWT(UserLoginResponseModel userLoginResponseModel)
        {
            var claims = new List<Claim> 
            {
                new Claim(ClaimTypes.NameIdentifier, userLoginResponseModel.Id.ToString()),

                new Claim(JwtRegisteredClaimNames.GivenName, userLoginResponseModel.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, userLoginResponseModel.LastName),
                new Claim(JwtRegisteredClaimNames.Email, userLoginResponseModel.Email)
            };

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenSettings:PrivateKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var expires = DateTime.UtcNow.AddHours(_config.GetValue<double>("TokenSettings:ExpirationHours"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Audience = _config["TokenSettings:Audience"],
                Issuer = _config["TokenSettings:Issuer"],
                SigningCredentials = credentials,
                Expires = expires

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var encodedToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(encodedToken);
        }

    }
}
