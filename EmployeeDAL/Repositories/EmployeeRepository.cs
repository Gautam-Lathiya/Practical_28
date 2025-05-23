using EmployeeDAL.Data;
using EmployeeDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDAL.Repositories
{
    public sealed class EmployeeRepository : IEmployeeRepository
    {
        private static EmployeeDbContext _context;

        // Singleton Instance
        private static Lazy<EmployeeRepository> _instance = new Lazy<EmployeeRepository>(() => new EmployeeRepository());

        // Private constructor
        private EmployeeRepository()
        {
            
        }

        public static EmployeeRepository GetInstance(EmployeeDbContext context)
        {
            _context = context;
            
            return _instance.Value;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            employee.JoiningDate = DateTime.Now;
            employee.Status = true;
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> SoftDeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            employee.Status = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int? id)
        {
            if (id == null) return null;
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return null;
            return employee;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            var query = _context.Employees.ToListAsync();
            
            return await query;
        }
    }
}
