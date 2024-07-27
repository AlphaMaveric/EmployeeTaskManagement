using EmployeeTaskManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace EmployeeTaskManagement.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<EmployeeTask> Tasks { get; set; }
        public DbSet<EmployeeDocument> Documents { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmployeeTask>()
                .HasKey(task => task.TaskId);

            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId); 

            modelBuilder.Entity<EmployeeTask>()
                .HasOne(et => et.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(et => et.UserId);

            modelBuilder.Entity<EmployeeDocument>()
           .HasKey(d => d.DocumentId);

            modelBuilder.Entity<EmployeeDocument>()
                .HasOne(d => d.Task)
                .WithMany(t => t.Documents)
                .HasForeignKey(d => d.TaskId);
        }
    }
}
