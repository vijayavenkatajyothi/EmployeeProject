namespace EmployeeProject.Dto
{
    public class EmpJobDetDto
    {
        public long Id { get; set; }
        public long EmpId { get; set; }

        public string CompanyName { get; set; }

        public string Designation { get; set; }

        public string Location { get; set; }

        public long? Salary { get; set; }
    }

}
