namespace EmployeeProject.Dto
{
    public class EmployeeListDto
    {
        public long Id { get; set; }
        public string EmployeeName { get; set; }
        public long EmpId {  get; set; }
        public long? MobileNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }
        public long? Salary { get; set; }
    }
}
