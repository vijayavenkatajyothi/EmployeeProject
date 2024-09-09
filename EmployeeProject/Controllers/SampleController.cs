using EmployeeProject.Dto;
using EmployeeProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
    //    [HttpPost(nameof(AddOrUpdateEmployeeDetails))]
    //    public IActionResult AddOrUpdateEmployeeDetails([FromBody] EmployeeAddDto request)
    //    {
    //        var result = employeeService.AddOrUpdateEmployeeDetails(request);
    //        return Ok(result);
    //    }
        //public object AddOrUpdateEmployeeDetails([FromBody] EmployeeAddDto request)
        //{


        //    var insert = new EmployeeListDto();
        //    insert.EmployeeName = request.EmployeeName;
        //    insert.EmpId = request.Id;
        //    insert.Address = request.EmpDetails.Address;
        //    insert.City = request.EmpDetails.City;
        //    insert.MobileNumber = request.EmpDetails.MobileNumber;
        //    insert.CompanyName = request.EmpJobDet.CompanyName;
        //    insert.Designation = request.EmpJobDet.Designation;
        //    insert.Location = request.EmpJobDet.Location;
        //    insert.Salary = request.EmpJobDet.Salary;
        //    var empdetInsertion = sample.Add<EmployeeListDto>(insert);
        //    return empdetInsertion;

        //    //if (request.Id == 0)
        //    //{
        //    //var employee = new Employee
        //    //{
        //    //    EmployeeName=request.EmployeeName,
        //    //}

        //    //    var employeeDetails = new EmployeeDetail
        //    //    {
        //    //        EmpId = request.Id,
        //    //        MobileNumber = request.EmpDetails.MobileNumber,
        //    //        Address = request.EmpDetails.Address,
        //    //        City = request.EmpDetails.City,
        //    //    };
        //    //    var employeeDetailsInsert = Add<EmployeeDetail, EmployeeDetail>(employeeDetails);
        //    //    var empJobDet = new EmpJobDet
        //    //    {
        //    //        EmpId = request.Id,
        //    //        CompanyName = request.EmpJobDet.CompanyName,
        //    //        Designation = request.EmpJobDet.Designation,
        //    //        Location = request.EmpJobDet.Location,
        //    //        Salary = request.EmpJobDet.Salary,
        //    //    };
        //    //    var empJobDataInsert=Add <EmpJobDet,bool>(empJobDet);

        //    //}
        //}


        public void GetAllEmployeeDetails()
        {
            Console.WriteLine("Employee Data");
        }
    }
}
