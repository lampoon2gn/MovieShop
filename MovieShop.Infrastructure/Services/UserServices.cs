using MovieShop.Core.Entities;
using MovieShop.Core.Models.Request;
using MovieShop.Core.Models.Response;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using MovieShop.Infrastructure.Exceptions;
using MovieShop.Infrastructure.Helper;
using MovieShop.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Services
{
    
    public class UserServices : IUserService
    {
        //do DI here
        private readonly IUserRepository _userRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly ICryptoService _encryptionService;
        private readonly IAsyncRepository<Purchase> _purchaseRepo;
        private readonly IAsyncRepository<Favorite> _favoriteRepo;
        private readonly IReviewRepository _reviewRepo;
        public UserServices(IUserRepository repo, 
            ICryptoService encryptionService, 
            IMovieRepository movieRepository, 
            IAsyncRepository<Purchase> purchaseRepo,
            IAsyncRepository<Favorite> favoriteRepo,
            IReviewRepository reviewRepo
            )
        {
            _userRepository = repo;
            _encryptionService = encryptionService;
            _movieRepository = movieRepository;
            _purchaseRepo = purchaseRepo;
            _favoriteRepo = favoriteRepo;
            _reviewRepo = reviewRepo;
        }

        public async Task<Favorite> AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            var fav = new Favorite();
            PropertyCopy.Copy(fav, favoriteRequest);

            //var movie = await _movieRepository.GetByIdAsync(purchase.MovieId);
            //var user = await _userRepository.GetByIdAsync(purchase.UserId);
            //purchase.Movie = movie;
            //purchase.User = user;
            //purchase.Id = null;
            return await _favoriteRepo.AddAsync(fav);
        }

        public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            var rev = new Review();
            PropertyCopy.Copy(rev, reviewRequest);
            var res = await _reviewRepo.AddAsync(rev);

        }

        public async Task<UserRegisterResponseModel> CreateUser(UserRegisterRequestModel requestModel)
        {
            //make sure the email doesn't already exist
            //need to send email to our user repo and see if the data exists
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);

            if (dbUser != null && string.Equals(dbUser.Email, requestModel.Email, StringComparison.CurrentCultureIgnoreCase))
                throw new Exception("Email Already Exits");

            //create a salt
            var salt = _encryptionService.CreateSalt();

            var hashedPassword = _encryptionService.HashPassword(requestModel.Password, salt);
            var user = new User
            {
                Email = requestModel.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName
            };
            var createdUser = await _userRepository.AddAsync(user);
            var response = new UserRegisterResponseModel
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName
            };
            return response;
        }

        public async Task DeleteMovieReview(int userId, int movieId)
        {
            var res = await _reviewRepo.ListAsync(f => f.UserId == userId && f.MovieId == movieId);
            Review rev = res.First();
            await _reviewRepo.DeleteAsync(rev);
        }

        public async Task<bool> FavoriteExists(int id, int movieId)
        {
            var fav = await _favoriteRepo.ListAsync(f=>f.UserId==id&&f.MovieId==movieId);
            if (fav.Count()!=0)
            {
                return true;
            }
            return false;
        }

        public Task<IEnumerable<Movie>> GetAllFavoritesForUser(int id)
        {
            var fav = _favoriteRepo.ListAsync(p => p.UserId == id).Result;
            List<int> movieId = new List<int>();
            foreach (var movie in fav)
            {
                movieId.Add(movie.MovieId);
            }
            var movies = _movieRepository.ListAsync(m => movieId.Contains(m.Id));
            return movies;
        }

        public Task<IEnumerable<Movie>> GetAllPurchasesForUser(int id)
        {
            var pur = _purchaseRepo.ListAsync(p => p.UserId == id).Result;
            List<int> movieId = new List<int>();
            foreach (var movie in pur)
            {
                movieId.Add(movie.MovieId);
            }
            var movies = _movieRepository.ListAsync(m => movieId.Contains(m.Id));
            return movies;
        }

        public async Task<IEnumerable<Review>> GetAllReviewsByUser(int id)
        {
            var reviews = await _reviewRepo.ListAsync(r=>r.UserId==id);
            return reviews;
        }

        public async Task<User> GetUser(string email)
        {
            var user = await _userRepository.ListAsync(u => u.Email.Equals(email));
            if (user==null)
            {
                throw new UserNotFoundException(email);
            }
            return user.ToList()[0];
        }

        public async Task<UserRegisterResponseModel> GetUserDetails(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var details = new UserRegisterResponseModel();
            PropertyCopy.Copy(details, user);
            return details;
        }

        public Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<Purchase> PurchaseMovie(PurchaseRequestModel purchaseRequest)
        {
            var purchase = new Purchase();
            PropertyCopy.Copy(purchase, purchaseRequest);

            var movie = await _movieRepository.GetByIdAsync(purchase.MovieId);
            var user = await _userRepository.GetByIdAsync(purchase.UserId);
            purchase.Movie = movie;
            purchase.User = user;
            //purchase.Id = null;
            return await _purchaseRepo.AddAsync(purchase);
           
        }

        public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            var fav = await _favoriteRepo.ListAsync(f=>f.UserId==favoriteRequest.UserId && f.MovieId==favoriteRequest.MovieId);
            
            await _favoriteRepo.DeleteAsync(fav.First());
        }

        public async Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            var res = await _reviewRepo.ListAsync(f => f.UserId == reviewRequest.UserId && f.MovieId == reviewRequest.MovieId);
            Review rev = res.First();
            rev.ReviewText = reviewRequest.ReviewText;
            rev.Rating = reviewRequest.Rating;
            await _reviewRepo.UpdateAsync(rev);
        }

        public async Task<UserLoginResponseModel> ValidateUser(string email, string password)
        {
            //check if user exists
            var user = await _userRepository.GetUserByEmail(email);
            //user is not registerd
            if (user == null) return null;
            var hashedPassword = _encryptionService.HashPassword(password, user.Salt);
            var isSuccess = user.HashedPassword == hashedPassword;

            var response = new UserLoginResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth
            };

            //var userRoles = roles.ToList();
            //if (userRoles.Any())
            //{
            //    response.Roles = userRoles.Select(r => r.Role.Name).ToList();
            //}

            //if success, return response, else return null
            return isSuccess ? response : null;


        }

    }
}
