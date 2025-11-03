using DisasterAlleviationApp.Models;

namespace DisasterAlleviationApp.Repositories
{
    public interface IVolunteerRepository
    {
        Task<Volunteer> RegisterVolunteerAsync(Volunteer volunteer);
        Task<Volunteer> GetVolunteerByUserIdAsync(int userId);
        Task<List<VolunteerTask>> GetAvailableTasksAsync();
        Task<VolunteerTask> GetTaskByIdAsync(int taskId);
        Task<VolunteerAssignment> AssignVolunteerToTaskAsync(VolunteerAssignment assignment);
        Task<List<VolunteerAssignment>> GetVolunteerAssignmentsAsync(int volunteerId);
        Task<VolunteerTask> CreateTaskAsync(VolunteerTask task);
    }
}