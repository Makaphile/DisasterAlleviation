using Xunit;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using DisasterAlleviationApp.Models;

namespace DisasterAlleviationApp.IntegrationTests
{
    public class UserRegistrationIntegrationTests : IClassFixture<WebApplicationFactory>
    {
        private readonly WebApplicationFactory _factory;
        private readonly HttpClient _client;

        public UserRegistrationIntegrationTests(WebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task RegisterUser_WithValidData_ShouldReturnSuccess()
        {
            // Arrange
            var userData = new
            {
                FirstName = "Integration",
                LastName = "Test",
                Email = "integration@test.com",
                Password = "Password123!"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/Account/Register", userData);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
        }

        [Fact]
        public async Task HomePage_ShouldReturnSuccess()
        {
            // Act
            var response = await _client.GetAsync("/");

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}