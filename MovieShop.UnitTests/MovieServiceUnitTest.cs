using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;
using MovieShop.Core.Models.Response;
using System.Linq;

namespace MovieShop.UnitTests
{
    [TestClass]
    public class MovieServiceUnitTest
    {
        private MovieServices _sut;
        private static List<Movie> _movies;
        private Mock<IMovieRepository> _MockMovieRepo;

        [TestInitialize]
        // [OneTimeSetup] in nUnit
        public void OneTimeSetup()
        {
            _MockMovieRepo = new Mock<IMovieRepository>();
            // SUT System under Test MovieService => GetTopRevenueMovies
            _sut = new MovieServices(_MockMovieRepo.Object);
            _MockMovieRepo.Setup(m => m.GetHighestRevenueMovies()).ReturnsAsync(_movies);
        }
        [ClassInitialize]
        public static void SetUp(TestContext context)
        {
            _movies = new List<Movie>
            {
                new Movie {Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
                new Movie {Id = 2, Title = "Avatar", Budget = 1200000},
                new Movie {Id = 3, Title = "Star Wars: The Force Awakens", Budget = 1200000},
                new Movie {Id = 4, Title = "Titanic", Budget = 1200000},
                new Movie {Id = 5, Title = "Inception", Budget = 1200000},
                new Movie {Id = 6, Title = "Avengers: Age of Ultron", Budget = 1200000},
                new Movie {Id = 7, Title = "Interstellar", Budget = 1200000},
                new Movie {Id = 8, Title = "Fight Club", Budget = 1200000},
                new Movie {Id = 9, Title = "The Lord of the Rings: The Fellowship of the Ring", Budget = 1200000},
                new Movie {Id = 10, Title = "The Dark Knight", Budget = 1200000},
                new Movie {Id = 11, Title = "The Hunger Games", Budget = 1200000},
                new Movie {Id = 12, Title = "Django Unchained", Budget = 1200000},
                new Movie {Id = 13, Title = "The Lord of the Rings: The Return of the King", Budget = 1200000},
                new Movie {Id = 14, Title = "Harry Potter and the Philosopher's Stone", Budget = 1200000},
                new Movie {Id = 15, Title = "Iron Man", Budget = 1200000},
                new Movie {Id = 16, Title = "Furious 7", Budget = 1200000}
            };
        }

        [TestMethod]
        public async Task TestListOfHighestGrossingFromFakeData()
        {
            //AAA
            //Arrange, Act, Assert
            //SUT(System Under Test) MovieService -> GetTopRevenueMovies

            //Arrange
            //process of mocking objects/data/methods...
            //_sut = new MovieServices(new MockMovieRepo());

            //Act
            var movies = await _sut.GetTopRevenueMovies();

            //check the actual output with expected value

            //Assert
            Assert.IsNotNull(movies);
            Assert.IsInstanceOfType(movies,typeof(IEnumerable<MovieResponseModel>));
            Assert.AreEqual(16,movies.Count());
        }
    }

    
}
