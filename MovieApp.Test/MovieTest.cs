using Moq;
using MovieApp.Server.Controllers;
using MovieApp.Server.Services;
using MovieApp.Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieApp.Server.DTOs;
using Microsoft.AspNetCore.Hosting;

namespace MovieApp.Test
{
    [TestClass]
    public class MovieTest
    {
        private readonly Mock<MovieService> _mockMovieService;
        private readonly Mock<IWebHostEnvironment> _mockWebHostEnvironment;
        private readonly MovieController _controller;

        public MovieTest()
        {
            _mockMovieService = new Mock<MovieService>();
            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            _controller = new MovieController(_mockMovieService.Object, _mockWebHostEnvironment.Object);
        }

        [TestMethod]
        public void TestFindById()
        {
        }
    }
}