namespace EmployeeProject.Dto
{
    public class CompanyDetailsDto
    {

        public long Id { get; set; }
        public String CompanyName { get;set; }

        public string Address { get; set; }
        public string Location { get; set; }
        public long salary { get; set; }
        public string DesignationName { get; set; }
        public long EmpId { get; set; }
        public string EmployeeName { get; set; }

    }
    public class NewCompanyDetailsDto
    {
        public long Id { get; set; }
        public string CompanyName { get; set; }
    }

}
