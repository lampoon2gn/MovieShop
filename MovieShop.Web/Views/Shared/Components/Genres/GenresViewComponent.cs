using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.Web.Views.Shared.Components.Genres
{
    public class GenresViewComponent:ViewComponent
    {
        private readonly IGenreService _service;
        public GenresViewComponent(IGenreService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var genres = await _service.GetAllGenres();
            return View(genres);
        }
    }
}
