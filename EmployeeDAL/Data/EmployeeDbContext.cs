using EmployeeDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDAL.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial department values (optional)
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "IT" },
                new Department { Id = 2, Name = "Admin" },
                new Department { Id = 3, Name = "HR" },
                new Department { Id = 4, Name = "Sales" },
                new Department { Id = 5, Name = "On-site" }
            );
        }
    }
}
