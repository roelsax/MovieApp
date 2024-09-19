using Microsoft.AspNetCore.Hosting;
using Moq;
using MovieApp.Server.Controllers;
using MovieApp.Server.Services;
using MovieApp.Server.Models;
using MovieApp.Server.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Test
{
    [TestClass]
    public class ActorTest
    {
        Mock<IActorService> _mockActorService = null!;
        Mock<IWebHostEnvironment> _mockWebHostEnvironment = null!;
        ActorController _controller = null!;
        Actor actor1 = null!;
        Actor actor2 = null!;
        Actor actor3 = new Actor
        {
            ActorId = 3,
            Name = "Matthias Schoenaerts",
            DateOfBirth = new DateOnly(1977, 12, 8),
            Bio = "Bio 3",
            Location = "Antwerpen",
            Nationality = "Belgium"
        };


        [TestInitialize]
        public void Initialize()
        {
            _mockActorService = new Mock<IActorService>(MockBehavior.Strict);
            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            _mockWebHostEnvironment.Setup(env => env.WebRootPath).Returns(@"C:\Users\cursist\source\repos\MovieApp\MovieApp.Server\wwwroot");
            _controller = new ActorController(_mockActorService.Object, _mockWebHostEnvironment.Object);
        }

        [TestMethod]
        public async Task TestFindAll_ReturnsOkResult_WithListOfActors() 
        {
            // Arrange
            actor1 = new Actor
            {
                ActorId = 1,
                Name = "Roel Sax",
                DateOfBirth = new DateOnly(1990, 10, 16),
                Bio = "Bio 1",
                Location = "Hasselt",
                Nationality = "Belgium"
            };

            actor2 = new Actor
            {
                ActorId = 2,
                Name = "Johnny Depp",
                DateOfBirth = new DateOnly(1963, 6, 9),
                Bio = "Bio 2",
                Location = "Los Angeles",
                Nationality = "United States"
            };

            var actors = new List<Actor> { actor1, actor2 };

            _mockActorService.Setup(service => service.GetActors())
                .ReturnsAsync(actors);

            // Act
            var result = await _controller.FindAll() as OkObjectResult;
            //Assert
            Assert.IsNotNull(result);
            var actual = result.Value as List<ActorDTO>;
            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual("Roel Sax", actual[0].Name);
            Assert.AreEqual("Johnny Depp", actual[1].Name);
        }

        [TestMethod]
        public async Task TestFindById_ReturnsOkResult() 
        {
            // Arrange
            _mockActorService.Setup(service => service.FindActor(3))
                .ReturnsAsync(actor3);

            // Act
            var result = await _controller.FindById(3) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            var actual = result.Value as ActorDTO;
            Assert.AreEqual("Matthias Schoenaerts", actual.Name);
        }

        [TestMethod]
        public async Task TestCreate_ReturnsOkResult()
        {
            //Arrange
            ActorFormDTO actorData = new ActorFormDTO
            {
                Name = "Jean Claude Van Damme",
                DateOfBirth = "2024-09-11T22:00:00.000Z",
                Bio = "blabla",
                Location = "Brussels",
                Nationality = "Belgium",
            };

            _mockActorService.Setup(service => service.Create(It.IsAny<Actor>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(actorData);

            // Assert
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult, "Expected OkResult but got null.");
        }

        [TestMethod]
        public async Task TestPut_ReturnsOkResult()
        {
            //Arrange
            ActorFormDTO actorData = new ActorFormDTO
            {
                Name = "Jean Claude Van Damme",
                DateOfBirth = "2024-09-11T22:00:00.000Z",
                Bio = "blabla",
                Location = "Brussels",
                Nationality = "Belgium",
            };


            _mockActorService.Setup(service => service.FindActor(3))
                .ReturnsAsync(actor3);

            _mockActorService.Setup(service => service.Update(It.IsAny<Actor>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Put(3, actorData);

            // Assert
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult, "Expected OkResult but got null.");
            _mockActorService.Setup(service => service.Update(It.IsAny<Actor>()));
        }

        [TestMethod]
        public async Task Validation_MissingName_ReturnsError()
        {
            // Arrange
            ActorFormDTO actorData = new ActorFormDTO
            {
                DateOfBirth = "2024-09-11T22:00:00.000Z",
                Bio = "blabla",
                Location = "Brussels",
                Nationality = "Belgium"
            };
            var context = new ValidationContext(actorData, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isModelStateValid = Validator.TryValidateObject(actorData, context, results, true);

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
            ActorFormDTO actorData = new ActorFormDTO
            {
                Name = "Jean Claude Van Damme",
                Bio = "blabla",
                Location = "Brussels",
                Nationality = "Belgium"
            };
            var context = new ValidationContext(actorData, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isModelStateValid = Validator.TryValidateObject(actorData, context, results, true);

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
            ActorFormDTO actorData = new ActorFormDTO
            {
                Name = "Jean Claude Van Damme",
                DateOfBirth = "2024-09-11T22:00:00.000Z",
                Bio = "blabla",
                Nationality = "Belgium"
            };
            var context = new ValidationContext(actorData, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isModelStateValid = Validator.TryValidateObject(actorData, context, results, true);

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
            ActorFormDTO actorData = new ActorFormDTO
            {
                Name = "Jean Claude Van Damme",
                DateOfBirth = "2024-09-11T22:00:00.000Z",
                Bio = "blabla",
                Location = "Brussels"
            };
            var context = new ValidationContext(actorData, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isModelStateValid = Validator.TryValidateObject(actorData, context, results, true);

            // Assert
            Assert.IsFalse(isModelStateValid, "Model should be invalid due to missing Country.");

            var nameError = results.FirstOrDefault(r => r.MemberNames.Contains("Nationality"));
            Assert.IsNotNull(nameError, "Expected validation error for missing Country.");
            Assert.AreEqual("Country is required.", nameError.ErrorMessage);
        }

        [TestMethod]
        public async Task Delete_Actor_ReturnsOk()
        {
            // Arrange
            int actorId = 3;
            var actor = new Actor
            {
                Name = "Brad Pitt",
                DateOfBirth = new DateOnly(1963, 12, 18),
                Bio = "blabla",
                Location = "Los Angeles",
                Nationality = "United States"
            };

            _mockActorService.Setup(service => service.FindActor(actorId))
                             .ReturnsAsync(actor);

            _mockActorService.Setup(service => service.Remove(actorId))
                             .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(actorId);

            // Assert
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult, "Expected OkResult but got null.");

            _mockActorService.Verify(service => service.Remove(actorId), Times.Once);
        }

        [TestMethod]
        public async Task Delete_NonExistingMovie_ReturnsNotFound()
        {
            // Arrange
            int actorId = 3;

            _mockActorService.Setup(service => service.FindActor(actorId))
                             .ReturnsAsync((Actor) null);

            // Act
            var result = await _controller.Delete(actorId);

            // Assert
            var notFoundResult = result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);

            _mockActorService.Verify(service => service.Remove(It.IsAny<int>()), Times.Never);
        }
    }
}
