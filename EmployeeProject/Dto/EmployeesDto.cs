namespace EmployeeProject.Dto
{
    public class EmployeesDto
    {
        public long Id { get; set; }
        public  string EmployeeName { get; set; }
        public List<EmployeeDetailsDto> employeeDetailsDtos { get; set; }
        public List<EmpJobDetDto> empJobDetDtos { get; set; }
    }

}
