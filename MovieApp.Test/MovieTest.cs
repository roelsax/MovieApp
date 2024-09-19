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
using System.Text.Json;

namespace MovieApp.Test
{
    [TestClass]
    public class MovieTest
    {
        Mock<IMovieService> _mockMovieService = null!;
        Mock<IWebHostEnvironment> _mockWebHostEnvironment = null!;
        MovieController _controller = null!;
        Movie movie1 = null!;
        Movie movie2 = null!;
        Movie movie3 = new Movie
        {
            MovieId = 3,
            Name = "Third movie",
            ReleaseDate = new DateOnly(2023, 1, 2),
            Description = "Description 3",
            Genres = new List<Genre> { Genre.Comedy, Genre.Adventure },
            DirectorId = 3,
        };

        [TestInitialize]
        public void Initialize()
        {
            _mockMovieService = new Mock<IMovieService>(MockBehavior.Strict);
            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            _mockWebHostEnvironment.Setup(env => env.WebRootPath).Returns(@"C:\Users\cursist\source\repos\MovieApp\MovieApp.Server\wwwroot");
            _controller = new MovieController(_mockMovieService.Object, _mockWebHostEnvironment.Object);
        }

        [TestMethod]
        public async Task TestFindAll_ReturnsOkResult_WithListOfMovies()
        {
            // Arrange
            movie1 = new Movie
            {
                MovieId = 1,
                Name = "First movie",
                ReleaseDate = new DateOnly(2023, 1, 1),
                Description = "Description 1",
                Genres = new List<Genre> { Genre.Action, Genre.Thriller },
                DirectorId = 1,
            };

            movie2 = new Movie
            {
                MovieId = 2,
                Name = "Second movie",
                ReleaseDate = new DateOnly(2023, 1, 2),
                Description = "Description 2",
                Genres = new List<Genre> { Genre.Comedy, Genre.Adventure },
                DirectorId = 2,
            };

            var movies = new List<Movie>
            {
                movie1,
                movie2
            };

            _mockMovieService.Setup(service => service.GetMovies(It.IsAny<string>(), It.IsAny<int?>()))
                             .ReturnsAsync(movies);

            // Act
            
            var result = await _controller.FindAll(null, null) as OkObjectResult;
            // Assert
            Assert.IsNotNull(result);
            var actual = result.Value as List<MovieDTO>;
            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual("First movie", actual[0].Name);
            Assert.AreEqual("Second movie", actual[1].Name);
        }

        [TestMethod]
        public async Task TestFindById_ReturnsOkResult()
        {
            //arrange

            _mockMovieService.Setup(service => service.FindMovie(3))
                             .ReturnsAsync(movie3);

            // Act
            var result = await _controller.FindById(3) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            var actual = result.Value as MovieDTO;
            Assert.AreEqual("Third movie", actual.Name);
        }

        [TestMethod]
        public async Task Create_MissingReleaseDate_ReturnsBadRequest()
        {
            // Arrange
            var movieJson = JsonDocument.Parse(@"{ ""name"": ""Test Movie"", ""description"": ""A test movie"", ""directorId"": 1 }").RootElement;

            // Act
            var result = await _controller.Create(movieJson);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult, "Expected BadRequestObjectResult but got null.");
            var modelState = badRequestResult.Value as SerializableError;
            Assert.IsTrue(modelState.ContainsKey("releaseDate"), "Expected error for missing release date.");
        }

        [TestMethod]
        public async Task Create_InvalidReleaseDateFormat_ReturnsBadRequest()
        {
            // Arrange
            var movieJson = JsonDocument.Parse(@"{ ""name"": ""Test Movie"", ""releaseDate"": ""invalid-date"", ""description"": ""A test movie"", ""directorId"": 1 }").RootElement;
            
            // Act
            var result = await _controller.Create(movieJson);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult, "Expected BadRequestObjectResult but got null.");
            var modelState = badRequestResult.Value as SerializableError;
            Assert.IsTrue(modelState.ContainsKey("releaseDate"), "Expected error for invalid release date format.");
            Assert.AreEqual("Invalid release date format.", ((string[])modelState["releaseDate"])[0]);
        }

        [TestMethod]
        public async Task Create_MissingDirectorId_ReturnsBadRequest()
        {
            // Arrange
            var movieJson = JsonDocument.Parse(@"{ ""name"": ""Test Movie"", ""releaseDate"": ""2023-09-01"", ""description"": ""A test movie"" }").RootElement;

            // Act
            var result = await _controller.Create(movieJson);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult, "Expected BadRequestObjectResult but got null.");
            var modelState = badRequestResult.Value as SerializableError;
            Assert.IsTrue(modelState.ContainsKey("Director"), "Expected error for missing director.");
            Assert.AreEqual("Director is required.", ((string[])modelState["Director"])[0]);
        }

        [TestMethod]
        public async Task Create_MissingName_ReturnsBadRequest()
        {
            // Arrange
            var movieJson = JsonDocument.Parse(@"{ ""releaseDate"": ""2023-09-01"", ""description"": ""A test movie"", ""directorId"": 1  }").RootElement;

            // Act
            var result = await _controller.Create(movieJson);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult, "Expected BadRequestObjectResult but got null.");
            var modelState = badRequestResult.Value as SerializableError;
            Assert.IsTrue(modelState.ContainsKey("Name"), "Expected error for missing name.");
            Assert.AreEqual("Name is required.", ((string[])modelState["Name"])[0]);
        }

        [TestMethod]
        public async Task Create_ValidMovie_ReturnsOk()
        {
            //arrange
            var movieData = new
            {
                name = "Fourth movie",
                releaseDate = new DateOnly(2023, 1, 2),
                description = "Description 4",
                genres = new[]  {
                    new {id = 0, name = "Action" },
                    new {id = 14, name = "Horror"}
                },
                directorId = 3,
            };
            string jsonString = JsonSerializer.Serialize(movieData);
            
            using JsonDocument jsonDocument = JsonDocument.Parse(jsonString);

            var movieJson = jsonDocument.RootElement;

            _mockMovieService.Setup(service => service.Create(It.IsAny<Movie>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(movieJson);

            // Assert
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult, "Expected OkResult but got null.");
        }

        [TestMethod]
        public async Task Create_MissingGenres_ReturnsBadRequest()
        {
            //arrange
            var movieData = new
            {
                name = "Fourth movie",
                releaseDate = new DateOnly(2023, 1, 2),
                description = "Description 4",
                directorId = 3,
            };
            string jsonString = JsonSerializer.Serialize(movieData);

            using JsonDocument jsonDocument = JsonDocument.Parse(jsonString);

            var movieJson = jsonDocument.RootElement;

            // Act
            var result = await _controller.Create(movieJson);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult, "Expected BadRequestObjectResult but got null.");
            var modelState = badRequestResult.Value as SerializableError;
            Assert.IsTrue(modelState.ContainsKey("Genres"), "Expected error for missing Genres.");
            Assert.AreEqual("Genres is required.", ((string[])modelState["Genres"])[0]);
        }

        [TestMethod]
        public async Task Put_MissingName_ReturnsBadRequest()
        {
            // Arrange
            var movieJson = JsonDocument.Parse(@"{ ""releaseDate"": ""2023-09-01"", ""description"": ""A test movie"", ""directorId"": 1  }").RootElement;
            _mockMovieService.Setup(service => service.FindMovie(3))
                             .ReturnsAsync(movie3);
            // Act
            var result = await _controller.Put(3, movieJson);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult, "Expected BadRequestObjectResult but got null.");
            var modelState = badRequestResult.Value as SerializableError;
            Assert.IsTrue(modelState.ContainsKey("Name"), "Expected error for missing name.");
            Assert.AreEqual("Name is required.", ((string[])modelState["Name"])[0]);
        }

        [TestMethod]
        public async Task Put_MissingReleaseData_ReturnsBadRequest()
        {
            // Arrange
            var movieJson = JsonDocument.Parse(@"{ ""name"": ""Test Movie"", ""description"": ""A test movie"", ""directorId"": 1 }").RootElement;
            _mockMovieService.Setup(service => service.FindMovie(3))
                             .ReturnsAsync(movie3);
            // Act
            var result = await _controller.Put(3,movieJson);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult, "Expected BadRequestObjectResult but got null.");
            var modelState = badRequestResult.Value as SerializableError;
            Assert.IsTrue(modelState.ContainsKey("releaseDate"), "Expected error for missing release date.");
        }

        [TestMethod]
        public async Task Put_InvalidReleaseDateFormat_ReturnsBadRequest()
        {
            // Arrange
            var movieJson = JsonDocument.Parse(@"{ ""name"": ""Test Movie"", ""releaseDate"": ""invalid-date"", ""description"": ""A test movie"", ""directorId"": 1 }").RootElement;
            _mockMovieService.Setup(service => service.FindMovie(3))
                             .ReturnsAsync(movie3);

            // Act
            var result = await _controller.Put(3,movieJson);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult, "Expected BadRequestObjectResult but got null.");
            var modelState = badRequestResult.Value as SerializableError;
            Assert.IsTrue(modelState.ContainsKey("releaseDate"), "Expected error for invalid release date format.");
            Assert.AreEqual("Invalid release date format.", ((string[])modelState["releaseDate"])[0]);
        }

        [TestMethod]
        public async Task Put_MissingDirectorId_ReturnsBadRequest()
        {
            // Arrange
            var movieJson = JsonDocument.Parse(@"{ ""name"": ""Test Movie"", ""releaseDate"": ""2023-09-01"", ""description"": ""A test movie"" }").RootElement;
            _mockMovieService.Setup(service => service.FindMovie(3))
                             .ReturnsAsync(movie3);

            // Act
            var result = await _controller.Put(3,movieJson);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult, "Expected BadRequestObjectResult but got null.");
            var modelState = badRequestResult.Value as SerializableError;
            Assert.IsTrue(modelState.ContainsKey("Director"), "Expected error for missing director.");
            Assert.AreEqual("Director is required.", ((string[])modelState["Director"])[0]);
        }

        [TestMethod]
        public async Task Put_ValidMovie_ReturnsOk()
        {
            //arrange
            var movieData = new
            {
                name = "Fourth movie",
                releaseDate = new DateOnly(2023, 1, 2),
                description = "Description 4",
                genres = new[]  {
                    new {id = 0, name = "Action" },
                    new {id = 14, name = "Horror"}
                },
                directorId = 3,
            };
            string jsonString = JsonSerializer.Serialize(movieData);

            using JsonDocument jsonDocument = JsonDocument.Parse(jsonString);

            var movieJson = jsonDocument.RootElement;
            _mockMovieService.Setup(service => service.FindMovie(3))
                             .ReturnsAsync(movie3);

            _mockMovieService.Setup(service => service.Update(It.IsAny<Movie>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Put(3, movieJson);

            // Assert
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult, "Expected OkResult but got null.");
            _mockMovieService.Verify(service => service.Update(It.IsAny<Movie>()));

        }

        [TestMethod]
        public async Task Delete_Movie_ReturnsOk()
        {
            // Arrange
            int movieId = 3;
            var movie = new Movie { MovieId = movieId, Name = "Sample Movie" };

            _mockMovieService.Setup(service => service.FindMovie(movieId))
                             .ReturnsAsync(movie);

            _mockMovieService.Setup(service => service.Remove(movieId))
                             .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(movieId);

            // Assert
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult, "Expected OkResult but got null.");

            _mockMovieService.Verify(service => service.Remove(movieId), Times.Once);
        }

        [TestMethod]
        public async Task Delete_NonExistingMovie_ReturnsNotFound()
        {
            // Arrange
            int movieId = 3;

            _mockMovieService.Setup(service => service.FindMovie(movieId))
                             .ReturnsAsync((Movie)null);

            // Act
            var result = await _controller.Delete(movieId);

            // Assert
            var notFoundResult = result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);

            _mockMovieService.Verify(service => service.Remove(It.IsAny<int>()), Times.Never);
        }
    }
}