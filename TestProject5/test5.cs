
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POE_RaceDay_API.Controllers;
using POE_RaceDay_API.DTOs;
using Xunit;

namespace TestProject3
{
    public class EnrolmentControllerTests
    {
        private EnrolmentController CreateController(
            string? participantId = null)
        {
            // These tests return before accessing the database.
            var controller = new EnrolmentController(null!);

            var httpContext = new DefaultHttpContext();

            if (participantId != null)
            {
                httpContext.User = new ClaimsPrincipal(
                    new ClaimsIdentity(
                        new[]
                        {
                            new Claim(
                                ClaimTypes.NameIdentifier,
                                participantId)
                        },
                        "TestAuthentication"));
            }

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            return controller;
        }

        [Fact]
        public async Task CreateEnrolment_InvalidCategoryID_ReturnsBadRequest()
        {
            // Arrange
            var controller = CreateController();

            var request = new EnrolmentRequest
            {
                CategoryID = 0
            };

            // Act
            var result = await controller.CreateEnrolment(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateEnrolment_NegativeCategoryID_ReturnsBadRequest()
        {
            // Arrange
            var controller = CreateController();

            var request = new EnrolmentRequest
            {
                CategoryID = -1
            };

            // Act
            var result = await controller.CreateEnrolment(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateEnrolment_MissingParticipantID_ReturnsUnauthorized()
        {
            // Arrange
            var controller = CreateController();

            var request = new EnrolmentRequest
            {
                CategoryID = 1
            };

            // Act
            var result = await controller.CreateEnrolment(request);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task CreateEnrolment_InvalidParticipantID_ReturnsUnauthorized()
        {
            // Arrange
            var controller = CreateController("invalid");

            var request = new EnrolmentRequest
            {
                CategoryID = 1
            };

            // Act
            var result = await controller.CreateEnrolment(request);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task CreateEnrolment_ValidCategoryAndParticipantID_DoesNotReturnValidationError()
        {
            // Arrange
            var controller = CreateController("123");

            var request = new EnrolmentRequest
            {
                CategoryID = 1
            };

            // Act
            var result = await controller.CreateEnrolment(request);

            // Assert
            Assert.IsNotType<BadRequestObjectResult>(result);
            Assert.IsNotType<UnauthorizedObjectResult>(result);
        }
    }
}
