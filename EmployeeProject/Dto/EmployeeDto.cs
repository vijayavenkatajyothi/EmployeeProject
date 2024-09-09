using EmployeeProject.Models;

namespace EmployeeProject.Dto
{
    public class EmployeeDto
    {
        public List <EmployeesDto>employeesDto { get; set; }
        
    }
    public class NewEmployeeDto
    {
        public List<EmployeeAddDto> employeeAddDetailsData { get; set; }
    }

    public class AllEmployeeDetailsDto
    {
        public long Id { get; set; }
        public string EmployeeName { get; set; }
        public long EmpId { get; set; }
        public long? MobileNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public long DesignationId { get; set; }
        public string Designation { get; set; }

        public string Location { get; set; }
        public long? Salary { get; set; }

    }
}
