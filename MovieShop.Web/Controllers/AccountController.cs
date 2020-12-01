using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Models.Request;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieShop.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        //we need to show the empty register page
        //http:localhost/account/register GET
        public async Task<IActionResult> Register()
        {

            return View();
        }

        [HttpPost]
        //when user hits submit button
        public async Task<IActionResult> Register(UserRegisterRequestModel model)
        {
            //true when all validations in the model are passed
            if (ModelState.IsValid)
            {
                //send the model to our service
                await _userService.CreateUser(model);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestModel loginRequest, string returnUrl =null)
        {
            returnUrl ??= Url.Content("~/");
            if (!ModelState.IsValid) return View();

            var user = await _userService.ValidateUser(loginRequest.Email, loginRequest.Password);

            //user not exist or wrong password
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }

            //if user is validated
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname,  user.LastName),
                new Claim(ClaimTypes.NameIdentifier,  user.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //AuthenticationScheme will look in startup and check the default auth
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyAccount()
        {
            return View();
        }
    }
}
