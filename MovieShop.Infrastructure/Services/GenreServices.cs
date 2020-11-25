using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Services
{
    public class GenreServices:IGenreService
    {
        private readonly IAsyncRepository<Genre> _repository;
        public GenreServices(IAsyncRepository<Genre> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            var genres = await _repository.ListAllAsync();
            return genres;
        }
    }
}
