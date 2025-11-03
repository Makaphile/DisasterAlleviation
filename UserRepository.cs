using DisasterAlleviationApp.Data;
using DisasterAlleviationApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DisasterAlleviationApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> ValidateUserAsync(string email, string password)
        {
            var user = await GetUserByEmailAsync(email);
            return user != null && user.PasswordHash == HashPassword(password) && user.IsActive;
        }

        private string HashPassword(string password)
        {
            // In a real application, use proper password hashing like BCrypt
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}