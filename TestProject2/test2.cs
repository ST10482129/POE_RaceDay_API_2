
using Xunit;
using POE_RaceDay_API.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace POE_RaceDay_API.Tests
{
    public class UserTests
    {
        [Fact]
        public void User_DefaultValues_AreCorrect()
        {
            // Arrange
            var user = new User();

            // Assert
            Assert.Equal(0, user.UserID);
            Assert.Equal(string.Empty, user.FullName);
            Assert.Equal(string.Empty, user.Email);
            Assert.Equal(string.Empty, user.PasswordHash);
            Assert.Equal("Participant", user.Role);
            Assert.Null(user.PhoneNumber);
            Assert.NotEqual(default(DateTime), user.CreatedAt);
            Assert.NotNull(user.Events);
            Assert.NotNull(user.Enrolments);
        }

        [Fact]
        public void User_CanSetProperties()
        {
            // Arrange
            var user = new User();

            // Act
            user.UserID = 1;
            user.FullName = "John Smith";
            user.Email = "john@example.com";
            user.PasswordHash = "hashed_password";
            user.Role = "Admin";
            user.PhoneNumber = "0712345678";

            // Assert
            Assert.Equal(1, user.UserID);
            Assert.Equal("John Smith", user.FullName);
            Assert.Equal("john@example.com", user.Email);
            Assert.Equal("hashed_password", user.PasswordHash);
            Assert.Equal("Admin", user.Role);
            Assert.Equal("0712345678", user.PhoneNumber);
        }

        [Fact]
        public void User_DefaultCollections_AreEmpty()
        {
            // Arrange
            var user = new User();

            // Assert
            Assert.Empty(user.Events);
            Assert.Empty(user.Enrolments);
        }

        [Fact]
        public void User_CreatedAt_IsSetToUtcTime()
        {
            // Arrange
            var beforeCreation = DateTime.UtcNow;
            var user = new User();
            var afterCreation = DateTime.UtcNow;

            // Assert
            Assert.InRange(
                user.CreatedAt,
                beforeCreation,
                afterCreation
            );

            Assert.Equal(DateTimeKind.Utc, user.CreatedAt.Kind);
        }

        [Fact]
        public void User_RequiredProperties_HaveRequiredAttribute()
        {
            // Arrange
            var type = typeof(User);

            // Assert
            Assert.NotNull(type.GetProperty(nameof(User.FullName))!
                .GetCustomAttributes(typeof(RequiredAttribute), true).Length > 0
                ? true : null);

            Assert.NotNull(type.GetProperty(nameof(User.Email))!
                .GetCustomAttributes(typeof(RequiredAttribute), true).Length > 0
                ? true : null);

            Assert.NotNull(type.GetProperty(nameof(User.PasswordHash))!
                .GetCustomAttributes(typeof(RequiredAttribute), true).Length > 0
                ? true : null);
        }
    }
}
