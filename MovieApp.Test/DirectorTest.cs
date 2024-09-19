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
using MovieApp.Server.Repositories.Seeding;
using System.ComponentModel.DataAnnotations;

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
        public async Task Create_ReturnsOkResult()
        {
            //Arrange
            DirectorFormDTO directorData = new DirectorFormDTO
            {
                Name = "Koen Mortier",
                DateOfBirth = "2024-09-11T22:00:00.000Z",
                Bio = "blabla",
                Location = "Brussels",
                Nationality = "Belgium",
            };

            _mockDirectorService.Setup(service => service.Create(It.IsAny<Director>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(directorData);

            // Assert
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult, "Expected OkResult but got null.");
        }

        [TestMethod]
        public async Task Put_ReturnsOkResult()
        {
            //Arrange
            DirectorFormDTO directorData = new DirectorFormDTO
            {
                Name = "Koen Mortier",
                DateOfBirth = "2024-09-11T22:00:00.000Z",
                Bio = "blabla",
                Location = "Brussels",
                Nationality = "Belgium",
            };

            _mockDirectorService.Setup(service => service.FindDirector(3))
                .ReturnsAsync(director3);

            _mockDirectorService.Setup(service => service.Update(It.IsAny<Director>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Put(3, directorData);

            // Assert
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult, "Expected OkResult but got null.");
        }

        [TestMethod]
        public async Task Validation_MissingName_ReturnsError()
        {
            // Arrange
            DirectorFormDTO directorData = new DirectorFormDTO
            {
               DateOfBirth = "2024-09-11T22:00:00.000Z",
               Bio = "blabla",
                Location = "Brussels",
               Nationality = "Belgium"
            };
            var context = new ValidationContext(directorData, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isModelStateValid = Validator.TryValidateObject(directorData, context, results, true);

            // Assert
            Assert.IsFalse(isModelStateValid, "Model should be invalid due to missing Name.");

            var nameError = results.FirstOrDefault(r => r.MemberNames.Contains("Name"));
            Assert.IsNotNull(nameError, "Expected validation error for missing Name.");
            Assert.AreEqual("Name is required.", nameError.ErrorMessage);
        }

        [TestMethod]
        public async Task Validation_DateOfBirth_ReturnsError()
        {
            // Arrange
            DirectorFormDTO directorData = new DirectorFormDTO
            {
                Name = "Koen Mortier",
                Bio = "blabla",
                Location = "Brussels",
                Nationality = "Belgium"
            };
            var context = new ValidationContext(directorData, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isModelStateValid = Validator.TryValidateObject(directorData, context, results, true);

            // Assert
            Assert.IsFalse(isModelStateValid, "Model should be invalid due to missing Date of birth.");

            var nameError = results.FirstOrDefault(r => r.MemberNames.Contains("DateOfBirth"));
            Assert.IsNotNull(nameError, "Expected validation error for missing Date of birth.");
            Assert.AreEqual("Date of birth is required.", nameError.ErrorMessage);
        }

        [TestMethod]
        public async Task Validation_Location_ReturnsError()
        {
            // Arrange
            DirectorFormDTO directorData = new DirectorFormDTO
            {
                Name = "Koen Mortier",
                DateOfBirth = "2024-09-11T22:00:00.000Z",
                Bio = "blabla",
                Nationality = "Belgium"
            };
            var context = new ValidationContext(directorData, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isModelStateValid = Validator.TryValidateObject(directorData, context, results, true);

            // Assert
            Assert.IsFalse(isModelStateValid, "Model should be invalid due to missing Location.");

            var nameError = results.FirstOrDefault(r => r.MemberNames.Contains("Location"));
            Assert.IsNotNull(nameError, "Expected validation error for missing Location.");
            Assert.AreEqual("Location is required.", nameError.ErrorMessage);
        }

        [TestMethod]
        public async Task Validation_Country_ReturnsError()
        {
            // Arrange
            DirectorFormDTO directorData = new DirectorFormDTO
            {
                Name = "Jean Claude Van Damme",
                DateOfBirth = "2024-09-11T22:00:00.000Z",
                Bio = "blabla",
                Location = "Brussels"
            };
            var context = new ValidationContext(directorData, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isModelStateValid = Validator.TryValidateObject(directorData, context, results, true);

            // Assert
            Assert.IsFalse(isModelStateValid, "Model should be invalid due to missing Country.");

            var nameError = results.FirstOrDefault(r => r.MemberNames.Contains("Nationality"));
            Assert.IsNotNull(nameError, "Expected validation error for missing Country.");
            Assert.AreEqual("Country is required.", nameError.ErrorMessage);
        }

        [TestMethod]
        public async Task Delete_Director_ReturnsOk()
        {
            // Arrange
            int directorId = 3;
            var director = new Director
            {
                Name = "Koen Mortier",
                DateOfBirth = new DateOnly(1963, 12, 18),
                Bio = "blabla",
                Location = "Brussel",
                Nationality = "Belgium"
            };

            _mockDirectorService.Setup(service => service.FindDirector(directorId))
                .ReturnsAsync(director);

            _mockDirectorService.Setup(service => service.Remove(directorId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(directorId);

            // Assert
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult, "Expected OkResult but got null.");

            _mockDirectorService.Verify(service => service.Remove(directorId), Times.Once);
        }

        [TestMethod]
        public async Task Delete_NonExistingDirector_ReturnsNotFound()
        {
            // Arrange
            int directorId = 3;

            _mockDirectorService.Setup(service => service.FindDirector(directorId))
                .ReturnsAsync((Director)null);    

            // Act
            var result = await _controller.Delete(directorId);

            // Assert
            var notFoundResult = result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);

            _mockDirectorService.Verify(service => service.Remove(It.IsAny<int>()), Times.Never);
        }

    }
}
