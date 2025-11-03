using DisasterAlleviationApp.Models;

namespace DisasterAlleviationApp.Repositories
{
    public interface IDonationRepository
    {
        Task<List<Donation>> GetAllDonationsAsync();
        Task<Donation> GetDonationByIdAsync(int id);
        Task<Donation> CreateDonationAsync(Donation donation);
        Task UpdateDonationAsync(Donation donation);
        Task<List<Donation>> GetDonationsByUserAsync(int userId);
    }
}