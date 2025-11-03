using DisasterAlleviationApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DisasterAlleviationApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<DisasterIncident> DisasterIncidents { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<VolunteerTask> VolunteerTasks { get; set; }
        public DbSet<VolunteerAssignment> VolunteerAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User configuration
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // DisasterIncident configuration
            modelBuilder.Entity<DisasterIncident>()
                .HasOne(d => d.ReportedByUser)
                .WithMany()
                .HasForeignKey(d => d.ReportedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Donation configuration
            modelBuilder.Entity<Donation>()
                .HasOne(d => d.DonatedByUser)
                .WithMany()
                .HasForeignKey(d => d.DonatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Donation>()
                .HasOne(d => d.DisasterIncident)
                .WithMany()
                .HasForeignKey(d => d.DisasterIncidentId)
                .OnDelete(DeleteBehavior.SetNull);

            // Volunteer configuration
            modelBuilder.Entity<Volunteer>()
                .HasOne(v => v.User)
                .WithMany()
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // VolunteerTask configuration
            modelBuilder.Entity<VolunteerTask>()
                .HasOne(t => t.DisasterIncident)
                .WithMany()
                .HasForeignKey(t => t.DisasterIncidentId)
                .OnDelete(DeleteBehavior.SetNull);

            // VolunteerAssignment configuration
            modelBuilder.Entity<VolunteerAssignment>()
                .HasOne(a => a.Volunteer)
                .WithMany()
                .HasForeignKey(a => a.VolunteerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VolunteerAssignment>()
                .HasOne(a => a.Task)
                .WithMany()
                .HasForeignKey(a => a.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}