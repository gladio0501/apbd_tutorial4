using System;
using Xunit;

namespace LegacyApp
{
    public class UserServiceTests
    {
        [Fact]
        public void TestAddUser()
        {
            // Arrange
            var userService = new UserService();
            string firstName = "John";
            string lastName = "Doe";
            string email = "john.doe@example.com";
            DateTime dateOfBirth = new DateTime(1990, 1, 1);
            int clientId = 1;

            // Act
            bool result = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

            // Assert
            Assert.True(result, "TestAddUser failed");
        }

        [Fact]
        public void TestValidate()
        {
            // Arrange
            var userService = new UserService();
            string firstName = "John";
            string lastName = "Doe";
            string email = "john.doe@example.com";
            DateTime dateOfBirth = new DateTime(1990, 1, 1);

            // Act
            bool result = userService.Validate(firstName, lastName, email, dateOfBirth);

            // Assert
            Assert.True(result, "TestValidate failed");
        }

        [Fact]
        public void TestCalculate()
        {
            // Arrange
            var userService = new UserService();
            var client = new Client { Type = "VeryImportantClient" };
            var user = new User { DateOfBirth = new DateTime(1990, 1, 1) };

            // Act
            int result = userService.Calculate(client, user);

            // Assert
            Assert.Equal(40000, result);
        }
    }
}