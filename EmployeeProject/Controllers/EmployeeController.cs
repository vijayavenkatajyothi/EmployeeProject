using EmployeeProject.Dto;
using EmployeeProject.Models;
using EmployeeProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private SampleDbContext sample;
        private EmployeeService employeeService;
        public EmployeeController(SampleDbContext sample, EmployeeService employeeService)
        {
            this.sample = sample;
            this.employeeService = employeeService;
        }

        [HttpPost(nameof(AddEmployee))]
        public IActionResult AddEmployee([FromBody] AddEmpDto request)
        {
            var result = employeeService.AddEmployee(request);
            return Ok(result);
        }

        [HttpGet(nameof(GetEmployeeDetailsNew))]
        public IActionResult GetEmployeeDetailsNew()
        {
            var result = employeeService.GetEmployeeDeatilsNew();
            return Ok(result);
        }
        [HttpGet(nameof(GetEmployeeDetailsById))]
        public IActionResult GetEmployeeDetailsById(long EmpId)
        {
            var result = employeeService.GetEmloyeeDetailsById(EmpId);
            return Ok(result);  

        }
        [HttpDelete(nameof(DeleteEmployeeDetailsById))]
        public IActionResult DeleteEmployeeDetailsById(long EmpId)
        {
            var result = employeeService.DeleteEmployeeDetailsById(EmpId);
            return Ok(result);

        }
        //[HttpGet(nameof(GetAllEmployees))]
        //public IActionResult  GetAllEmployees()
        //{
        //    var result = employeeService.GetAllEmployees();
        //    return Ok(result);
        //}
        [HttpGet(nameof(GetEmployeeDetails))]
        public IActionResult GetEmployeeDetails()
        {
            var data = new List<Employee>();
            var result = (from e in sample.Employees
                          join ed in sample.EmployeeDetails on e.Id equals ed.EmpId
                          select e).ToList();

            //result = (from e in sample.Employees
            //          join ej in sample.EmpJobDets on e.Id equals ej.EmpId
            //          select new EmpJobDet
            //          {
            //              EmpId = e.Id,
            //              CompanyName = ej.CompanyName, 
            //              Designation = ej.Designation,
            //              Salary = ej.Salary,
            //              Location = ej.Location
            //          }).ToList();


            return Ok(result);
        }

        [HttpGet(nameof(GetAllEmployees))]
        public IActionResult GetAllEmployees()
        {
            var data = employeeService.GetAllEmployees();
            return Ok(data);
        }

        [HttpGet(nameof(ReadEmployeeDetails))]
        public IActionResult ReadEmployeeDetails(long CompanyId ,long salary,long DesignationId=0)
        {
            var response = employeeService.ReadEmployeeDetails(CompanyId,salary, DesignationId);
            return Ok(response);

        }

        //[HttpGet(nameof(GetEmployeeDetailsById))]
        //public IActionResult GetEmployeeDetailsById(long Id)
        //{
        //    var result = (from e in sample.Employees
        //                  where e.Id == Id
        //                  select new EmployeesDto
        //                  {
        //                      Id = e.Id,
        //                      EmployeeName = e.EmployeeName,

        //                  }).ToList();
        //    return Ok(result);
        //}
        [HttpGet(nameof(GetElementByName))]
        public IActionResult GetElementByName (string Name)
        {
            var result = (from e in sample.Employees
                          where e.EmployeeName == Name
                          select new EmployeesDto
                          {
                              Id = e.Id,
                              EmployeeName = e.EmployeeName,
                          }).ToList();
            return Ok(result);
        }
      
       
        [HttpPost(nameof(InsertNewEmployee))]
        public IActionResult InsertNewEmployee([FromBody]EmployeeAddDto request)
        {
            var result = employeeService.InsertNewEmployee(request);
            return Ok(result);
        }

        [HttpPost(nameof(CreateNewEmployee))]
        public IActionResult CreateNewEmployee([FromBody] EmployeeAddNewDto request)
        {
            var response = employeeService.CreateNewEmployee( request);
            return Ok(response);
        }
        [HttpPost(nameof(InsertNewEmployeeOne))]
        public ActionResult InsertNewEmployeeOne(EmployeeAddDto request)
        {
            Employee emp = new Employee()
            {
                EmployeeName = request.EmployeeName
            };
            EmployeeDetail employeeDetail = new EmployeeDetail()
            {
                Address = request.EmpDetails.Address,
                City = request.EmpDetails.City,
                MobileNumber = request.EmpDetails.MobileNumber,
            };
            EmpJobDet empJob = new EmpJobDet()
            {
               // CompanyName = request.EmpJobDet.CompanyName,
               // Designation = request.EmpJobDet.Designation,
               // Location = request.EmpJobDet.Location,
                Salary = request.EmpJobDet.Salary
            };
            sample.Employees.Add(emp);
            employeeDetail.EmpId = emp.Id;
            empJob.EmpId = emp.Id;
            sample.EmployeeDetails.Add(employeeDetail);
            sample.EmpJobDets.Add(empJob);
            sample.SaveChanges();
            return Ok(sample);
        }
        //[HttpDelete(nameof(DeleteEmployee))]
        //public IActionResult DeleteEmployee(int empId) {
        //    var result = employeeService.DeleteEmployee(empId);
        //    return Ok(result);
        //}

      




    }

}
