using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Services
{
    

    public class CastServices : ICastService
    {
        private readonly IAsyncRepository<Cast> _repo;

        public CastServices(IAsyncRepository<Cast> repo)
        {
            _repo = repo;
        }
        public async Task<Cast> GetCastById(int id)
        {
            return await _repo.GetByIdAsync(id);
        }
    }
}
