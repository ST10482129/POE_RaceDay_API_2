
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POE_RaceDay_API.Controllers;
using POE_RaceDay_API.DTOs;
using Xunit;

namespace TestProject3
{
    public class CategoryControllerTests
    {
        private CategoryController CreateController()
        {
            // Validation tests return before accessing the database.
            var controller = new CategoryController(null!);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            return controller;
        }

        [Fact]
        public async Task CreateCategory_EmptyCategoryName_ReturnsBadRequest()
        {
            // Arrange
            var controller = CreateController();

            var request = new CategoryRequest
            {
                CategoryName = "",
                Distance = 10,
                EntryFee = 100,
                EventID = 1
            };

            // Act
            var result = await controller.CreateCategory(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateCategory_WhitespaceCategoryName_ReturnsBadRequest()
        {
            // Arrange
            var controller = CreateController();

            var request = new CategoryRequest
            {
                CategoryName = "   ",
                Distance = 10,
                EntryFee = 100,
                EventID = 1
            };

            // Act
            var result = await controller.CreateCategory(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateCategory_ZeroDistance_ReturnsBadRequest()
        {
            // Arrange
            var controller = CreateController();

            var request = new CategoryRequest
            {
                CategoryName = "10 km Run",
                Distance = 0,
                EntryFee = 100,
                EventID = 1
            };

            // Act
            var result = await controller.CreateCategory(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateCategory_NegativeDistance_ReturnsBadRequest()
        {
            // Arrange
            var controller = CreateController();

            var request = new CategoryRequest
            {
                CategoryName = "10 km Run",
                Distance = -5,
                EntryFee = 100,
                EventID = 1
            };

            // Act
            var result = await controller.CreateCategory(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateCategory_NegativeEntryFee_ReturnsBadRequest()
        {
            // Arrange
            var controller = CreateController();

            var request = new CategoryRequest
            {
                CategoryName = "10 km Run",
                Distance = 10,
                EntryFee = -1,
                EventID = 1
            };

            // Act
            var result = await controller.CreateCategory(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateCategory_ZeroEventID_ReturnsBadRequest()
        {
            // Arrange
            var controller = CreateController();

            var request = new CategoryRequest
            {
                CategoryName = "10 km Run",
                Distance = 10,
                EntryFee = 100,
                EventID = 0
            };

            // Act
            var result = await controller.CreateCategory(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateCategory_NegativeEventID_ReturnsBadRequest()
        {
            // Arrange
            var controller = CreateController();

            var request = new CategoryRequest
            {
                CategoryName = "10 km Run",
                Distance = 10,
                EntryFee = 100,
                EventID = -1
            };

            // Act
            var result = await controller.CreateCategory(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
