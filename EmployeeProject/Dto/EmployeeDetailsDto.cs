namespace EmployeeProject.Dto
{
    public class EmployeeDetailsDto
    {
       public long Id { get; set; }
        public long EmpId { get; set; }
        public long? MobileNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
    public class EmpJobDetailsDto
    {
        public long Id { get; set; }
        public long EmpId { get; set; }
        public long CompanyId { get; set; }
        public long DesignationId { get; set; }
        public long Salary { get; set; }
    }
    public class AddEmpDto
    {
        public long Id { get; set; }
        public string EmployeeName { get; set; }

        public EmployeeDetailsDto empDetails { get; set; }
        public EmpJobDetailsDto empJobDett { get; set; }
    }
}
