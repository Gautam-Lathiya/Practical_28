using EmployeeDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDAL.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int? id);
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task<Employee> UpdateEmployeeAsync(Employee employee);
        Task<bool> SoftDeleteEmployeeAsync(int id);
    }
}
