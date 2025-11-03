using DisasterAlleviationApp.Data;
using DisasterAlleviationApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DisasterAlleviationApp.Repositories
{
    public class DisasterRepository : IDisasterRepository
    {
        private readonly AppDbContext _context;

        public DisasterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DisasterIncident>> GetAllIncidentsAsync()
        {
            return await _context.DisasterIncidents
                .Include(d => d.ReportedByUser)
                .Where(d => d.IsActive)
                .OrderByDescending(d => d.ReportedDate)
                .ToListAsync();
        }

        public async Task<DisasterIncident> GetIncidentByIdAsync(int id)
        {
            return await _context.DisasterIncidents
                .Include(d => d.ReportedByUser)
                .FirstOrDefaultAsync(d => d.Id == id && d.IsActive);
        }

        public async Task<DisasterIncident> CreateIncidentAsync(DisasterIncident incident)
        {
            _context.DisasterIncidents.Add(incident);
            await _context.SaveChangesAsync();
            return incident;
        }

        public async Task UpdateIncidentAsync(DisasterIncident incident)
        {
            _context.DisasterIncidents.Update(incident);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIncidentAsync(int id)
        {
            var incident = await GetIncidentByIdAsync(id);
            if (incident != null)
            {
                incident.IsActive = false;
                await UpdateIncidentAsync(incident);
            }
        }

        public async Task<List<DisasterIncident>> GetActiveIncidentsAsync()
        {
            return await _context.DisasterIncidents
                .Include(d => d.ReportedByUser)
                .Where(d => d.IsActive && d.IsVerified)
                .OrderByDescending(d => d.ReportedDate)
                .ToListAsync();
        }
    }
}