using DisasterAlleviationApp.Models;

namespace DisasterAlleviationApp.Repositories
{
    public interface IDisasterRepository
    {
        Task<List<DisasterIncident>> GetAllIncidentsAsync();
        Task<DisasterIncident> GetIncidentByIdAsync(int id);
        Task<DisasterIncident> CreateIncidentAsync(DisasterIncident incident);
        Task UpdateIncidentAsync(DisasterIncident incident);
        Task DeleteIncidentAsync(int id);
        Task<List<DisasterIncident>> GetActiveIncidentsAsync();
    }
}