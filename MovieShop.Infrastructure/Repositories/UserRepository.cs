using MovieShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using MovieShop.Core.RepositoryInterfaces;
using System.Threading.Tasks;
using MovieShop.Infrastructure.Data;

namespace MovieShop.Infrastructure.Repositories
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public Task<User> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
