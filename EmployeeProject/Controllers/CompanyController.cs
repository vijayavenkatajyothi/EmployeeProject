using EmployeeProject.Dto;
using EmployeeProject.Models;
using EmployeeProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private SampleDbContext sample;
        private Companyservices companyservices;

        public CompanyController(SampleDbContext sample, Companyservices companyservices)
        {
            this.sample = sample;
            this.companyservices = companyservices;
        }

        [HttpPost(nameof(AddCompanyDetails))]
        public IActionResult AddCompanyDetails([FromBody] NewCompanyDetailsDto request)
        {
            var result = companyservices.AddCompanyDetails(request);
            return Ok(result);
        }

        [HttpGet(nameof(GetAllCompanyDetails))]

        public IActionResult GetAllCompanyDetails()
        {
            var result = companyservices.GetAllCompanyDetails();
            return Ok(result);
        }


        [HttpGet(nameof(GetCompanyDetailsById))]
        public IActionResult GetCompanyDetailsById(long CompanyId)
        {
            var result=companyservices.GetCompanyDetailsById(CompanyId);
            return Ok(result);
        }

        [HttpDelete(nameof(DeleteCompanyDetailsById))]
        public IActionResult DeleteCompanyDetailsById(long CompanyId)
        {
            var result = companyservices.DeleteCompanyDetailsById(CompanyId);
            return Ok(result);

        }















        [HttpGet(nameof(GetAllDesignationDeatils))]
        public IActionResult GetAllDesignationDeatils()
        {
            var result = companyservices.GetAllDesignationDetails();
            return Ok(result);
        }
        [HttpGet(nameof(GetCompany))]
        public IActionResult GetCompany(long CompanyId)
        {
            var response = companyservices.GetCompany(CompanyId);
            return Ok(response);
        }

        [HttpGet(nameof(GetCompanyDetails))]
        public IActionResult GetCompanyDetails(long CompanyId)
        {
            var response = companyservices.GetCompanyDetails(CompanyId);
            return Ok(response);
        }
        [HttpGet(nameof(GetDesignation))]
        public IActionResult GetDesignation(long DesignationId)
        {
            var result = companyservices.GetDesignation(DesignationId);
            return Ok(result);
        }
       

    }
}
