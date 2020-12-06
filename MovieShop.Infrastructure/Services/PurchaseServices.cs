using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Services
{
    public class PurchaseServices:IPurchaseService
    {
        private readonly IPurchaseRepository _repo;

        public PurchaseServices(IPurchaseRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<Purchase>> GetLatestPurchasedAsync()
        {
            var purchaseHistory = await _repo.GetLatestPurchasedAsync();
            return purchaseHistory;
        }
    }
}
