using EmployeeAPI.DTO;
using EmployeeDAL.Data;
using EmployeeDAL.Models;
using EmployeeDAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;
        private readonly FileLogger _logger = FileLogger.Instance(); // Singleton usage

        public EmployeeController(EmployeeDbContext context)
        {
            _repo = EmployeeRepository.GetInstance(context); // Singleton usage
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repo.GetEmployeesAsync();
            if (result == null || result.Count == 0)
            {
                return NotFound("No employees found.");
            }

            var names = "";
            foreach (var item in result)
            {
                names += item.Name + ", ";
            }

            _logger.Log($"Total employees retrieved: {result.Count}", "\n", $"##### Names : {names}");
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            var result = await _repo.GetEmployeeByIdAsync(id);
            if (result == null)
            {
                return NotFound($"Employee with Id {id} not found.");
            }
            _logger.Log($"Employee retrieved: {result.Id}, Name: {result.Name}, Salary: {result.Salary}, DepartmentId: {result.DepartmentId}, EmailId: {result.EmailId}, JoiningDate: {result.JoiningDate}, Status: {result.Status}");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeDTO dto)
        {
            var employee = new Employee
            {
                Name = dto.Name,
                Salary = dto.Salary,
                DepartmentId = dto.DepartmentId,
                EmailId = dto.EmailId,
                JoiningDate = DateTime.Now,
                Status = true
            };

            var result = await _repo.CreateEmployeeAsync(employee);

            _logger.Log($"Employee created: {result.Id}, Name: {result.Name}, Salary: {result.Salary}, DepartmentId: {result.DepartmentId}, EmailId: {result.EmailId}, JoiningDate: {result.JoiningDate}, Status: {result.Status}");
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EmployeeUpdateDTO dto)
        {
            var employee = await _repo.GetEmployeeByIdAsync(dto.Id);
            if (employee == null)
            {
                return NotFound($"Employee with Id {dto.Id} not found.");
            }

            employee.Name = dto.Name;
            employee.Salary = dto.Salary;
            employee.DepartmentId = dto.DepartmentId;
            employee.EmailId = dto.EmailId;
            employee.Status = dto.Status;

            await _repo.UpdateEmployeeAsync(employee);
            _logger.Log($"Employee updated: {employee.Id}, Name: {employee.Name}, Salary: {employee.Salary}, DepartmentId: {employee.DepartmentId}, EmailId: {employee.EmailId}, JoiningDate: {employee.JoiningDate}, Status: {employee.Status}");
            return Ok("Employee updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _repo.SoftDeleteEmployeeAsync(id);
            if (!success) return NotFound();
            _logger.Log($"Employee with Id {id} deleted.");
            return Ok();
        }
    }
}
