using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DisasterAlleviationApp.Data;
using DisasterAlleviationApp.Models;
using DisasterAlleviationApp.Repositories;

namespace DisasterAlleviationApp.IntegrationTests
{
    public class DatabaseIntegrationTests : IClassFixture<WebApplicationFactory>
    {
        private readonly WebApplicationFactory _factory;

        public DatabaseIntegrationTests(WebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CompleteUserWorkflow_ShouldWorkCorrectly()
        {
            // Arrange
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userRepository = new UserRepository(context);

            // Act - Create User
            var user = new User
            {
                FirstName = "Workflow",
                LastName = "Test",
                Email = "workflow@test.com",
                PasswordHash = "hashedpassword"
            };
            var createdUser = await userRepository.CreateUserAsync(user);

            // Assert - User Creation
            createdUser.Should().NotBeNull();
            createdUser.Email.Should().Be("workflow@test.com");

            // Act - Check User Exists
            var userExists = await userRepository.UserExistsAsync("workflow@test.com");

            // Assert - User Exists
            userExists.Should().BeTrue();

            // Act - Retrieve User
            var retrievedUser = await userRepository.GetUserByEmailAsync("workflow@test.com");

            // Assert - User Retrieval
            retrievedUser.Should().NotBeNull();
            retrievedUser.Id.Should().Be(createdUser.Id);
        }
    }
}