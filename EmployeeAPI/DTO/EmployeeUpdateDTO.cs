namespace EmployeeAPI.DTO
{
    public class EmployeeUpdateDTO
    {
        public int Id { get; set; }                    // Required to identify which employee to update
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
        public string EmailId { get; set; }
        public bool Status { get; set; }
    }
}
