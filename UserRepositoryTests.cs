using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using DisasterAlleviationApp.Data;
using DisasterAlleviationApp.Models;
using DisasterAlleviationApp.Repositories;

namespace DisasterAlleviationApp.UnitTests.Repositories
{
    public class UserRepositoryTests : TestBase
    {
        [Fact]
        public async Task CreateUserAsync_WithValidUser_ShouldCreateUser()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var repository = new UserRepository(context);
            var user = new User
            {
                FirstName = "Test",
                LastName = "User",
                Email = "test@example.com",
                PasswordHash = "hashedpassword"
            };

            // Act
            var result = await repository.CreateUserAsync(user);

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().Be("test@example.com");
            result.Id.Should().BeGreaterThan(0);

            var usersInDb = await context.Users.ToListAsync();
            usersInDb.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetUserByEmailAsync_WithExistingEmail_ShouldReturnUser()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var repository = new UserRepository(context);
            var user = new User
            {
                FirstName = "Test",
                LastName = "User",
                Email = "test@example.com",
                PasswordHash = "hashedpassword"
            };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetUserByEmailAsync("test@example.com");

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().Be("test@example.com");
        }
    }
}