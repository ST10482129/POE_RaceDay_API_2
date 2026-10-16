
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using POE_RaceDay_API.Controllers;
using POE_RaceDay_API.DTOs;
using Xunit;

namespace TestProject3
{
    public class ResultControllerTests
    {
        private ResultController CreateController()
        {
            var settings = new Dictionary<string, string?>();

            IConfiguration configuration =
                new ConfigurationBuilder()
                    .AddInMemoryCollection(settings)
                    .Build();

            var controller = new ResultController(configuration);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            return controller;
        }

        [Fact]
        public async Task CreateResult_InvalidEnrolmentID_ReturnsBadRequest()
        {
            // Arrange
            var controller = CreateController();

            var request = new ResultRequest
            {
                EnrolmentID = 0,
                Position = 1,
                FinishTime = "00:28:35"
            };

            // Act
            var result = await controller.CreateResult(request);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public async Task CreateResult_InvalidPosition_ReturnsBadRequest()
        {
            // Arrange
            var controller = CreateController();

            var request = new ResultRequest
            {
                EnrolmentID = 1,
                Position = 0,
                FinishTime = "00:28:35"
            };

            // Act
            var result = await controller.CreateResult(request);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public async Task CreateResult_InvalidFinishTime_ReturnsBadRequest()
        {
            // Arrange
            var controller = CreateController();

            var request = new ResultRequest
            {
                EnrolmentID = 1,
                Position = 1,
                FinishTime = "invalid-time"
            };

            // Act
            var result = await controller.CreateResult(request);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public async Task CreateResult_NegativeFinishTime_ReturnsBadRequest()
        {
            // Arrange
            var controller = CreateController();

            var request = new ResultRequest
            {
                EnrolmentID = 1,
                Position = 1,
                FinishTime = "-00:10:00"
            };

            // Act
            var result = await controller.CreateResult(request);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public async Task CreateResult_FinishTimeOver24Hours_ReturnsBadRequest()
        {
            // Arrange
            var controller = CreateController();

            var request = new ResultRequest
            {
                EnrolmentID = 1,
                Position = 1,
                FinishTime = "24:00:00"
            };

            // Act
            var result = await controller.CreateResult(request);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public async Task CreateResult_MissingOrganiserID_ReturnsUnauthorized()
        {
            // Arrange
            var controller = CreateController();

            var request = new ResultRequest
            {
                EnrolmentID = 1,
                Position = 1,
                FinishTime = "00:28:35"
            };

            // Act
            var result = await controller.CreateResult(request);

            // Assert
            var unauthorized =
                Assert.IsType<UnauthorizedObjectResult>(result);

            Assert.Equal(401, unauthorized.StatusCode);
        }
    }
}
