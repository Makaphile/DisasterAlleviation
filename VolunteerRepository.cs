using DisasterAlleviationApp.Data;
using DisasterAlleviationApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DisasterAlleviationApp.Repositories
{
    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly AppDbContext _context;

        public VolunteerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Volunteer> RegisterVolunteerAsync(Volunteer volunteer)
        {
            _context.Volunteers.Add(volunteer);
            await _context.SaveChangesAsync();
            return volunteer;
        }

        public async Task<Volunteer> GetVolunteerByUserIdAsync(int userId)
        {
            return await _context.Volunteers
                .Include(v => v.User)
                .FirstOrDefaultAsync(v => v.UserId == userId && v.IsActive);
        }

        public async Task<List<VolunteerTask>> GetAvailableTasksAsync()
        {
            return await _context.VolunteerTasks
                .Include(t => t.DisasterIncident)
                .Where(t => t.Status == "Open" && t.CurrentVolunteers < t.MaxVolunteers)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<VolunteerTask> GetTaskByIdAsync(int taskId)
        {
            return await _context.VolunteerTasks
                .Include(t => t.DisasterIncident)
                .FirstOrDefaultAsync(t => t.Id == taskId);
        }

        public async Task<VolunteerAssignment> AssignVolunteerToTaskAsync(VolunteerAssignment assignment)
        {
            _context.VolunteerAssignments.Add(assignment);

            // Update task volunteer count
            var task = await GetTaskByIdAsync(assignment.TaskId);
            if (task != null)
            {
                task.CurrentVolunteers++;
                if (task.CurrentVolunteers >= task.MaxVolunteers)
                {
                    task.Status = "In Progress";
                }
                _context.VolunteerTasks.Update(task);
            }

            await _context.SaveChangesAsync();
            return assignment;
        }

        public async Task<List<VolunteerAssignment>> GetVolunteerAssignmentsAsync(int volunteerId)
        {
            return await _context.VolunteerAssignments
                .Include(a => a.Task)
                .ThenInclude(t => t.DisasterIncident)
                .Include(a => a.Volunteer)
                .ThenInclude(v => v.User)
                .Where(a => a.VolunteerId == volunteerId)
                .OrderByDescending(a => a.AssignmentDate)
                .ToListAsync();
        }

        public async Task<VolunteerTask> CreateTaskAsync(VolunteerTask task)
        {
            _context.VolunteerTasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }
    }
}