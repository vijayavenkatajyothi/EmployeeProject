namespace EmployeeProject.Dto
{
    public class EmployeeAddDto
    {
        public long Id { get; set; }

        public string EmployeeName { get; set; }
        public EmployeeDetailsDto EmpDetails { get; set; }
        public EmpJobDetDto EmpJobDet { get; set; }


    }
    public class EmployeeDesignationDto
    {
        public long Id { get; set; }
        public long EmpId { get; set; }
        public long CompanyId { get; set; }
        public long DesignationId { get; set; }
        public long Salary { get; set; }
    }
    public class EmployeeAddNewDto
    {
        public long Id { get; set; }
        public string EmployeeName { get; set; }
        public EmployeeDetailsDto EmpDetails { get; set; }
        public EmployeeDesignationDto EmpDesignation { get; set; }



    }
    public class EmloyeeDetailsGetData
    {
        public long Id { get; set; }
        public string EmployeeName { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public long MobileNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }    
        public long Salary { get; set; }
        public long CompanyId { get; set; }
        public long DesignationId { get;set; }

    }




}
