using Moq;
using MovieApp.Server.Controllers;
using MovieApp.Server.Services;
using MovieApp.Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieApp.Server.DTOs;
using Microsoft.AspNetCore.Hosting;
using MovieApp.Server.Repositories;

namespace MovieApp.Test
{
    [TestClass]
    public class MovieTest
    {
        Mock<IMovieService> _mockMovieService = null!;
        Mock<IWebHostEnvironment> _mockWebHostEnvironment = null!;
        Mock<IMovieRepository> mockRepository = null!;
        MovieController _controller = null!;
        Movie movie1 = null!;
        Movie movie2 = null!;

        [TestInitialize]
        public void Initialize()
        {
            //mockRepository = new Mock<IMovieRepository>();
            //var repository = mockRepository.Object;
            _mockMovieService = new Mock<IMovieService>(MockBehavior.Strict);
            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            _controller = new MovieController(_mockMovieService.Object, _mockWebHostEnvironment.Object);

            movie1 = new Movie {
                MovieId = 1,
                Name = "First movie",
                ReleaseDate = new DateOnly(2023, 1, 1),
                Description = "Description 1",
                Genres = new List<Genre>{Genre.Action, Genre.Thriller},
                DirectorId = 1,

            };

            movie1.ActorMovies.Add(new ActorMovie
            {
                ActorId = 1,
                Movie = movie1
            });

            movie2 = new Movie { 
                MovieId = 2, 
                Name = "Second movie", 
                ReleaseDate = new DateOnly(2023, 1, 2), 
                Description = "Description 2",
                Genres = new List<Genre> { Genre.Comedy, Genre.Adventure },
                DirectorId = 2,
            };

            movie2.ActorMovies.Add(new ActorMovie
            {
                ActorId = 2,
                Movie = movie2
            });
        }

        [TestMethod]
        public async Task TestFindAll_ReturnsOkResult_WithListOfMovies()
        {
            // Arrange
            var movies = new List<Movie>
            {
                movie1,
                movie2
            };

            _mockMovieService.Setup(service => service.GetMovies(It.IsAny<string>(), It.IsAny<int?>()))
                             .ReturnsAsync(movies);

            // Act
            var result = await _controller.FindAll(null, null);

            // Assert
            // Assert
            Assert.IsNotNull(result);
            //var value = result.Value as List<MovieDTO>;
            //var returnValue = Assert.IsInstanceOfType<List<MovieDTO>>(result.Value);
            //Assert.AreEqual(2, returnValue.Count);
        }

        [TestMethod]
        public void TestFindById()
        {
        }
    }
}