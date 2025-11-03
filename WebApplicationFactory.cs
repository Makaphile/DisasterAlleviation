using DisasterAlleviationApp.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DisasterAlleviationApp.IntegrationTests
{
    public class WebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the existing context configuration
                services.RemoveAll(typeof(DbContextOptions<AppDbContext>));

                // Add in-memory database
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDatabase");
                });
            });
        }
    }
}