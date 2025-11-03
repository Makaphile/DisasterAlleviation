using DisasterAlleviationApp.Data;
using DisasterAlleviationApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DisasterAlleviationApp.Repositories
{
    public class DonationRepository : IDonationRepository
    {
        private readonly AppDbContext _context;

        public DonationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Donation>> GetAllDonationsAsync()
        {
            return await _context.Donations
                .Include(d => d.DonatedByUser)
                .Include(d => d.DisasterIncident)
                .OrderByDescending(d => d.DonationDate)
                .ToListAsync();
        }

        public async Task<Donation> GetDonationByIdAsync(int id)
        {
            return await _context.Donations
                .Include(d => d.DonatedByUser)
                .Include(d => d.DisasterIncident)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Donation> CreateDonationAsync(Donation donation)
        {
            _context.Donations.Add(donation);
            await _context.SaveChangesAsync();
            return donation;
        }

        public async Task UpdateDonationAsync(Donation donation)
        {
            _context.Donations.Update(donation);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Donation>> GetDonationsByUserAsync(int userId)
        {
            return await _context.Donations
                .Include(d => d.DisasterIncident)
                .Where(d => d.DonatedByUserId == userId)
                .OrderByDescending(d => d.DonationDate)
                .ToListAsync();
        }
    }
}