using EmployeeProject.Dto;
using EmployeeProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeProject.Services
{
    public class Companyservices
    {
        private readonly SampleDbContext sample;
        public Companyservices(SampleDbContext sample)
        {
            this.sample = sample;
        }

        public object GetAllCompanyDetails()
        {
            var res = (from c in sample.Companies
                       select c).ToList();
            return res;
        }


        public object GetCompanyDetailsById(long CompanyId)
        {
            var res = (from c in sample.Companies
                       where c.Id == CompanyId
                       select new NewCompanyDetailsDto
                       {
                           Id = c.Id,
                           CompanyName = c.CompanyName,
                       }).ToList(); 
            return res;
        }

        public object DeleteCompanyDetailsById(long CompanyId)
        {
            var response = sample.Companies.Where(i => i.Id == CompanyId).Select(i => i).FirstOrDefault();

            var empjobdet=sample.EmpJobDets.Where(i=>i.CompanyId ==CompanyId).Select(i=>i).ToList();
            var empjobDetailDelete=empjobdet.Select(k=>
            {
                sample.EmpJobDets.Remove(k);
                return k;
            }).ToList();
               
            var data=  sample.Companies.Remove(response);
            sample.SaveChanges();
            return data !=null;
        }

        public bool AddCompanyDetails(NewCompanyDetailsDto request)
        {

            if(request.Id== 0)
            {
                var data = new Company();
                data.CompanyName = request.CompanyName;
                var companyinsert =sample.Add<Company>(data);
                sample.SaveChanges();
                return true;

            }
            else
            {
                var update= sample.Companies.Where(i=>i.Id == request.Id).Select(i=>i).FirstOrDefault();
                update.CompanyName= request.CompanyName;
                var updation=sample.Update<Company>(update);
                sample.SaveChanges();
                return true;
            }
            return false;
        }
       
        public object GetAllDesignationDetails()
        {
            var res= (from d in sample.Designations
                         select d).ToList();
            return res;
        }
        public object GetCompany(long CompanyId)
        {
            var result = from c in sample.Companies
                         where c.Id == CompanyId
                         select c;
            return result;
        }

        public object GetCompanyDetails(long CompanyId) {
            var data = (from c in sample.Companies
                       // join cd in sample.CompanyDetails on c.Id equals cd.CompanyId
                        join ej in sample.EmpJobDets on c.Id equals ej.CompanyId
                        join d in sample.Designations on ej.DesignationId equals d.Id
                        join e in sample.Employees on ej.EmpId equals e.Id
                        where c.Id == CompanyId
                        select new CompanyDetailsDto
                        {
                            EmpId=e.Id,
                            EmployeeName=e.EmployeeName,
                            Id=c.Id,
                            CompanyName=c.CompanyName,
                           // Address=cd.Address,
                          //  Location=cd.Location,
                            DesignationName=d.Designation1,
                            salary=ej.Salary.GetValueOrDefault()

                        }).ToList();
           return data;
        }

        public object GetDesignation(long DesignationId)
        {
            var res=(from  d in sample.Designations
                     where d.Id == DesignationId
                     select d).ToList();
            return res;
        }
    }

}
