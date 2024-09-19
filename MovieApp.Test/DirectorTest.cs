using Microsoft.AspNetCore.Hosting;
using Moq;
using MovieApp.Server.Models;
using MovieApp.Server.DTOs;
using MovieApp.Server.Controllers;
using MovieApp.Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace MovieApp.Test
{
    [TestClass]
    public class DirectorTest
    {
        Mock<IDirectorService> _mockDirectorService = null!;
        Mock<IWebHostEnvironment> _mockWebHostEnvironment = null!;
        DirectorController _controller = null!;
        Director director1 = null!;
        Director director2 = null!;
        Director director3 = new Director
        {
            DirectorId = 3,
            Name = "Steven Spielberg",
            DateOfBirth = new DateOnly(1946, 12, 18),
            Bio = "Bio 3",
            Location = "Los Angeles",
            Nationality = "United States"
        };

        [TestInitialize]
        public void Initialize()
        {
            _mockDirectorService = new Mock<IDirectorService>(MockBehavior.Strict);
            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            _mockWebHostEnvironment.Setup(env => env.WebRootPath).Returns(@"C:\Users\cursist\source\repos\MovieApp\MovieApp.Server\wwwroot");
            _controller = new DirectorController(_mockDirectorService.Object, _mockWebHostEnvironment.Object);
        }

        [TestMethod]
        public async Task TestFindAll_ReturnsOkResult_WithListOfDirectors()
        {
            //Arrange
            director1 = new Director
            {
                DirectorId = 1,
                Name = "Roel Sax",
                DateOfBirth = new DateOnly(1990, 10, 16),
                Bio = "Bio 1",
                Location = "Hasselt",
                Nationality = "Belgium"
            };

            director2 = new Director
            {
                DirectorId = 2,
                Name = "Erik van Looy",
                DateOfBirth = new DateOnly(1962, 4, 26),
                Bio = "Bio 2",
                Location = "Antwerpen",
                Nationality = "Belgium"
            };

            var directors = new List<Director> { director1, director2 };

            _mockDirectorService.Setup(service => service.GetDirectors())
                .ReturnsAsync(directors);

            //Act
            var result = await _controller.FindAll() as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            var actual = result.Value as List<DirectorDTO>;
            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual("Roel Sax", actual[0].Name);
            Assert.AreEqual("Erik van Looy", actual[1].Name);
        }

        [TestMethod]
        public async Task TestFindById_ReturnsOkResult() 
        {
            // Arrange
            _mockDirectorService.Setup(service => service.FindDirector(3))
                .ReturnsAsync(director3);

            // Act
            var result = await _controller.FindById(3) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            var actual = result.Value as DirectorDTO;
            Assert.AreEqual("Steven Spielberg", actual.Name);
        }

        [TestMethod]
        public async Task Create_MissingName_ReturnsBadRequest()
        {
            // Arrange
            DirectorFormDTO directorData = new DirectorFormDTO
            {
               DateOfBirth = "2024-09-11T22:00:00.000Z",
               Bio = "blabla",
               Location = "Brussels",
               Nationality = "Belgium"
            };

            // Act
            var result = await _controller.Create(directorData);
            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult, "Expected BadRequestObjectResult but got null.");
            var modelState = badRequestResult.Value as SerializableError;
            Assert.IsTrue(modelState.ContainsKey("Name"), "Expected error for missing name.");
        }
    }
}
