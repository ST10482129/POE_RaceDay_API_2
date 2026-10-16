
using System;
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
    public class EventControllerTests
    {
        private EventController CreateController()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(
                    new Dictionary<string, string?>())
                .Build();

            var controller = new EventController(configuration);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            return controller;
        }

        [Fact]
        public async Task CreateEvent_EmptyEventName_ReturnsBadRequest()
        {
            var controller = CreateController();

            var request = new EventRequest
            {
                EventName = "",
                Location = "Johannesburg",
                Description = "Race event",
                EventDate = DateTime.UtcNow.AddDays(10)
            };

            var result = await controller.CreateEvent(request);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateEvent_EmptyLocation_ReturnsBadRequest()
        {
            var controller = CreateController();

            var request = new EventRequest
            {
                EventName = "RaceDay Marathon",
                Location = "",
                Description = "Annual marathon",
                EventDate = DateTime.UtcNow.AddDays(10)
            };

            var result = await controller.CreateEvent(request);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateEvent_PastDate_ReturnsBadRequest()
        {
            var controller = CreateController();

            var request = new EventRequest
            {
                EventName = "RaceDay Marathon",
                Location = "Johannesburg",
                Description = "Annual marathon",
                EventDate = DateTime.UtcNow.AddDays(-1)
            };

            var result = await controller.CreateEvent(request);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateEvent_MissingOrganiserID_ReturnsUnauthorized()
        {
            var controller = CreateController();

            var request = new EventRequest
            {
                EventName = "RaceDay Marathon",
                Location = "Johannesburg",
                Description = "Annual marathon",
                EventDate = DateTime.UtcNow.AddDays(10)
            };

            var result = await controller.CreateEvent(request);

            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task UpdateEvent_EmptyEventName_ReturnsBadRequest()
        {
            var controller = CreateController();

            var request = new EventRequest
            {
                EventName = "",
                Location = "Johannesburg",
                Description = "Updated race",
                EventDate = DateTime.UtcNow.AddDays(10)
            };

            var result = await controller.UpdateEvent(1, request);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateEvent_EmptyLocation_ReturnsBadRequest()
        {
            var controller = CreateController();

            var request = new EventRequest
            {
                EventName = "Updated Race",
                Location = "",
                Description = "Updated race",
                EventDate = DateTime.UtcNow.AddDays(10)
            };

            var result = await controller.UpdateEvent(1, request);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateEvent_MissingOrganiserID_ReturnsUnauthorized()
        {
            var controller = CreateController();

            var request = new EventRequest
            {
                EventName = "Updated Race",
                Location = "Johannesburg",
                Description = "Updated race",
                EventDate = DateTime.UtcNow.AddDays(10)
            };

            var result = await controller.UpdateEvent(1, request);

            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task DeleteEvent_MissingOrganiserID_ReturnsUnauthorized()
        {
            var controller = CreateController();

            var result = await controller.DeleteEvent(1);

            Assert.IsType<UnauthorizedObjectResult>(result);
        }
    }
}
