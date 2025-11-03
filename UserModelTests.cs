using Xunit;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using DisasterAlleviationApp.Models;

namespace DisasterAlleviationApp.UnitTests.Models
{
    public class UserModelTests
    {
        [Fact]
        public void User_WithValidData_ShouldPassValidation()
        {
            // Arrange
            var user = new User
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PasswordHash = "hashedpassword"
            };

            var validationContext = new ValidationContext(user);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);

            // Assert
            isValid.Should().BeTrue();
            validationResults.Should().BeEmpty();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("invalid-email")]
        public void User_WithInvalidEmail_ShouldFailValidation(string invalidEmail)
        {
            // Arrange
            var user = new User
            {
                FirstName = "John",
                LastName = "Doe",
                Email = invalidEmail,
                PasswordHash = "hashedpassword"
            };

            var validationContext = new ValidationContext(user);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);

            // Assert
            isValid.Should().BeFalse();
            validationResults.Should().Contain(v => v.MemberNames.Contains("Email"));
        }
    }
}