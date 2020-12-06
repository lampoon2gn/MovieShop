using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastController : ControllerBase
    {
        private readonly ICastService _service;
        public CastController(ICastService service)
        {
            _service = service;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCastById(int id)
        {
            var response = await _service.GetCastById(id);

            if (response==null)
            {
                return BadRequest("Please check the cast id you entered");
            }
            return Ok(response);
        }
    }
}
