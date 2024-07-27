using EmployeeTaskManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;


namespace EmployeeTaskManagement.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                context.Database.Migrate();

                if (context.Users.Any())
                {
                    return;  
                }

                SeedData(context);                
            }
        }

        public static void Reseed(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                ClearData(context);
                SeedData(context);
            }
        }

        private static void SeedData(ApplicationDbContext context)
        {
            var manager = new User
            {
                UserName = "Roy Belvins",
                Email = "roy.belvins@thinkbridge.com",
                Role = "Manager"
            };

            var manager2 = new User
            {
                UserName = "Mark Bullow",
                Email = "mark.bullow@thinkbridge.com",
                Role = "Manager"
            };

            var employee = new User
            {
                UserName = "Tom Drury",
                Email = "tom.drury@thinkbridge.com",
                Role = "Employee",
                Manager = manager
            };

            var employee2 = new User
            {
                UserName = "Max Colwell",
                Email = "max.colwell@thinkbridge.com",
                Role = "Employee",
                Manager = manager
            };

            var employee3 = new User
            {
                UserName = "Raj Malhotra",
                Email = "raj.malhotra@thinkbridge.com",
                Role = "Employee",
                Manager = manager
            };

            var employee4 = new User
            {
                UserName = "Bhavana S",
                Email = "bhavana.s@thinkbridge.com",
                Role = "Employee",
                Manager = manager
            };

            var employee5 = new User
            {
                UserName = "Ray Mahon",
                Email = "ray.mahon@thinkbridge.com",
                Role = "Employee",
                Manager = manager2
            };

            var employee6 = new User
            {
                UserName = "Tommy Trey",
                Email = "tommy.trey@thinkbridge.com",
                Role = "Employee",
                Manager = manager2
            };

            var employee7 = new User
            {
                UserName = "Ravi Jadeja",
                Email = "ravi.jadeja@thinkbridge.com",
                Role = "Employee",
                Manager = manager2
            };

            context.Users.Add(manager);
            context.Users.AddRange(employee,employee2,employee3,employee4,employee5,employee6,employee7);
            context.SaveChanges();

            context.Tasks.AddRange(new EmployeeTask
            {
                Title = "Dev Setup",
                Description = "Set up developer environment",
                DueDate = DateTime.Now.AddDays(-3),
                IsCompleted = false,
                Notes = "Identify packages necessary",
                Documents = null,
                UserId = employee.UserId
            }, new EmployeeTask
            {
                Title = "Dev Setup",
                Description = "Set up developer environment",
                DueDate = DateTime.Now.AddDays(7),
                IsCompleted = false,
                Notes = "Identify packages necessary",
                Documents = null,
                UserId = employee2.UserId
            }, new EmployeeTask
            {
                Title = "Dev Setup",
                Description = "Set up developer environment",
                DueDate = DateTime.Now.AddDays(-3),
                IsCompleted = false,
                Notes = "Identify packages necessary",
                Documents = null,
                UserId = employee3.UserId
            }, new EmployeeTask
            {
                Title = "Dev Setup",
                Description = "Set up developer environment",
                DueDate = DateTime.Now.AddDays(7),
                IsCompleted = false,
                Notes = "Identify packages necessary",
                Documents = null,
                UserId = employee4.UserId
            }, new EmployeeTask
            {
                Title = "Qa Setup",
                Description = "Set up qa environment",
                DueDate = DateTime.Now.AddDays(5),
                IsCompleted = false,
                Notes = "Identify packages necessary",
                Documents = null,
                UserId = employee5.UserId
            }, new EmployeeTask
            {
                Title = "QA Setup",
                Description = "Set up qa environment",
                DueDate = DateTime.Now.AddDays(3),
                IsCompleted = false,
                Notes = "Identify packages necessary",
                Documents = null,
                UserId = employee6.UserId
            }, new EmployeeTask
            {
                Title = "Dev Setup",
                Description = "Set up developer environment",
                DueDate = DateTime.Now.AddDays(1),
                IsCompleted = false,
                Notes = "Identify packages necessary",
                Documents = null,
                UserId = employee7.UserId
            }, new EmployeeTask
            {
                Title = "QA Setup",
                Description = "Set up qa environment",
                DueDate = DateTime.Now.AddDays(3),
                IsCompleted = false,
                Notes = "Identify packages necessary",
                Documents = null,
                UserId = employee7.UserId
            });

            context.SaveChanges();

        }

            private static void ClearData(ApplicationDbContext context)
        {
            context.Tasks.RemoveRange(context.Tasks);
            context.Users.RemoveRange(context.Users);
            context.SaveChanges();
        }
    }
}