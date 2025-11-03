using DisasterAlleviationApp.Models;

namespace DisasterAlleviationApp.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> CreateUserAsync(User user);
        Task<bool> UserExistsAsync(string email);
        Task<bool> ValidateUserAsync(string email, string password);
    }
}