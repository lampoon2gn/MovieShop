using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Entities;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _service;
        public GenresController(IGenreService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("Genres")]
        public async Task<IActionResult> GetAllGenres()
        {
            var res =await _service.GetAllGenres();
            if (res==null)
            {
                return BadRequest("Something went wrong while getting genres");
            }
            return Ok(res);
        }
    }
}
