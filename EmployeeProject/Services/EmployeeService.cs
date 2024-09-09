using AutoMapper;
using Azure;
using EmployeeProject.Dto;
using EmployeeProject.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.ValueGeneration.Internal;

namespace EmployeeProject.Services
{
    public class EmployeeService
    {
        private readonly SampleDbContext sample;
     
        private readonly IMapper mapper;
        public EmployeeService(SampleDbContext sample)
        {

            this.sample = sample;

        }
        public bool AddEmployee([FromBody] AddEmpDto request)
        {
            if (request.Id == 0)
            {
                var insert = new Employee();
                insert.EmployeeName = request.EmployeeName;

                var empdet = new EmployeeDetail();
                var empAdddet = new List<EmployeeDetail>();
                empdet.EmpId = insert.Id;
                empdet.MobileNumber = request.empDetails.MobileNumber;
                empdet.Address = request.empDetails.Address;
                empdet.City = request.empDetails.City;
                empAdddet.Add(empdet);
                insert.EmployeeDetails = empAdddet;

                var empjob = new EmpJobDet();
                var empjobDet = new List<EmpJobDet>(); 
                empjob.EmpId = insert.Id;
                empjob.CompanyId = request.empJobDett.CompanyId;
                empjob.DesignationId = request.empJobDett.DesignationId;
                empjob.Salary = request.empJobDett.Salary;
                empjobDet.Add(empjob);
                insert.EmpJobDets = empjobDet;
               // var isDuplicate = (from e in sample.Employees
               //                    where insert.EmployeeName.ToLower().Trim().Contains(e.EmployeeName.ToLower().Trim())
               //                    select e).ToList();
               //var data=(from ins in insert
               //          join dup in isDuplicate on new {ins.EmployeeName} equals new {dup.EmployeeName} into grouped
               //          where !grouped.Any()
               //          select ins).ToList();


                var empinsertion = sample.Add<Employee>(insert);
                sample.SaveChanges();
                var insertion = new List<object>() { empinsertion };
                return true;
            }
            else
            {
                var employeeUpdate = sample.Employees.Where(i => i.Id == request.Id).Select(i => i).FirstOrDefault();
                employeeUpdate.EmployeeName = request.EmployeeName;

                var empDetailsDet = new List<EmployeeDetail>();
                var empDesignationDet = new List<EmpJobDet>();

                var empDetailsData = sample.EmployeeDetails.Where(i => i.EmpId == request.Id).Select(j => j).FirstOrDefault();
                empDetailsData.MobileNumber = request.empDetails.MobileNumber;
                empDetailsData.Address = request.empDetails.Address;
                empDetailsData.City = request.empDetails.City;

                empDetailsDet.Add(empDetailsData);


                var empDesignationData = sample.EmpJobDets.Where(j => j.EmpId == request.Id).Select(k => k).FirstOrDefault();
                empDesignationData.CompanyId = request.empJobDett.CompanyId;
                empDesignationData.DesignationId = request.empJobDett.DesignationId;
                empDesignationData.Salary = request.empJobDett.Salary;

                empDesignationDet.Add(empDesignationData);

                var updation = sample.Update<Employee>(employeeUpdate);
                sample.SaveChanges();mnb  
                var empUpdation = new List<object>() { updation };
                return true;
            }
            return false;



        }


        public object GetEmployeeDeatilsNew()
        {
            var data = (from e in sample.Employees
                        join ed in sample.EmployeeDetails on e.Id equals ed.EmpId
                        join ej in sample.EmpJobDets on e.Id equals ej.EmpId
                        join c in sample.Companies on ej.CompanyId equals c.Id
                        join d in sample.Designations on ej.DesignationId equals d.Id
                        select new EmloyeeDetailsGetData
                        {
                            Id = e.Id,
                            EmployeeName = e.EmployeeName,
                            CompanyName = c.CompanyName,
                            Designation = d.Designation1,
                            MobileNumber = ed.MobileNumber.GetValueOrDefault(),
                            Address = ed.Address,
                            City = ed.City,
                            Salary = ej.Salary.GetValueOrDefault()

                        }).Distinct().ToList();

            return data;
        }
        public object GetEmloyeeDetailsById(long EmpId)
        {
            var data = (from e in sample.Employees
                        join ed in sample.EmployeeDetails on e.Id equals ed.EmpId
                        join ej in sample.EmpJobDets on e.Id equals ej.EmpId
                        join c in sample.Companies on ej.CompanyId equals c.Id
                        join d in sample.Designations on ej.DesignationId equals d.Id
                        where e.Id == EmpId
                        select new EmloyeeDetailsGetData

                        {
                            Id = e.Id,
                            EmployeeName = e.EmployeeName,
                            CompanyName = c.CompanyName,
                            Designation = d.Designation1,
                            MobileNumber = ed.MobileNumber.GetValueOrDefault(),
                            Address = ed.Address,
                            City = ed.City,
                            Salary = ej.Salary.GetValueOrDefault(),
                            CompanyId = c.Id,
                            DesignationId = d.Id


                        }).FirstOrDefault();
            return data;
        }

        public object DeleteEmployeeDetailsById(long EmpId)
        {
            var data = sample.Employees.Where(k => k.Id == EmpId).Select(i => i).ToList();
            var empdetailss = new EmployeeDetail();

            var empDetailsData = sample.EmployeeDetails.Where(k => k.EmpId == EmpId).Select(i => i).ToList();


            var empJobDetailsData = sample.EmpJobDets.Where(k => k.EmpId == EmpId).Select(i => i).ToList();

            var deleteEmployeeDetail = empDetailsData.Select(k =>
            {
                sample.EmployeeDetails.Remove(k);
                return k;
            }).ToList();
            var deleteEmpJobData = empJobDetailsData.Select(k =>
            {
                sample.EmpJobDets.Remove(k);
                return k;
            }).ToList();

            var deleteEmployee = data.Select(k =>
            {
                sample.Employees.Remove(k);

                return k;
            }).ToList();

            sample.SaveChanges();
            return deleteEmployee.Count > 0;
            // var res=sample.SaveChanges();
            // return res;

        }
        //public object GetAllEmployees()
        //{
        //    var Employees = new List<Employee>();
        //    try
        //    {
        //        Employees = (from e in sample.Employees
        //                     join ed in sample.EmployeeDetails on e.Id equals ed.EmpId
        //                     select e).ToList();
        //        return Employees;

        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;

        //    }
        //}

        public EmployeeDto GetAllEmployees()
        {
            var result = new EmployeeDto();

            var employeeDet = (from e in sample.Employees
                               select new EmployeesDto
                               {
                                   Id = e.Id,
                                   EmployeeName = e.EmployeeName,
                               }).ToList();
            var employeeDetailsData = (from e in employeeDet
                                       join ed in sample.EmployeeDetails on e.Id equals ed.EmpId
                                       select new EmployeeDetailsDto
                                       {
                                           EmpId = e.Id,
                                           Address = ed.Address,
                                           City = ed.City,
                                           MobileNumber = ed.MobileNumber
                                       }).ToList();
            var employeeJobDetails = (from e in employeeDet
                                      join ej in sample.EmpJobDets on e.Id equals ej.EmpId
                                      select new EmpJobDetDto
                                      {
                                          EmpId = e.Id,
                                          //  CompanyName = ej.CompanyName,
                                          // Designation = ej.Designation,
                                          Salary = ej.Salary,
                                          //  Location = ej.Location,

                                      }).ToList();
            foreach (var employee in employeeDet)
            {
                employee.employeeDetailsDtos = employeeDetailsData.Where(K => K.EmpId == employee.Id).ToList();

                employee.empJobDetDtos = employeeJobDetails.Where(K => K.EmpId == employee.Id).ToList();

            }
            result.employeesDto = employeeDet;
            return result;

            ////employeeDet=employeeDet.Select(k =>
            ////{
            ////    k.employeeDetailsDtos.Where(j => j.Id == k.Id).ToList();
            ////    k.empJobDetDtos.Where(j => j.Id == k.Id).ToList();
            ////}).ToList();
        }

        public object ReadEmployeeDetails(long CompanyId, long salary, long DesignationId = 0)
        {
            var data = (from e in sample.Employees
                        join ed in sample.EmployeeDetails on e.Id equals ed.EmpId
                        join ej in sample.EmpJobDets on e.Id equals ej.EmpId
                       // join c in sample.CompanyDetails on ej.CompanyId equals c.Id
                        join d in sample.Designations on ej.DesignationId equals d.Id
                        where ej.CompanyId == CompanyId
                         && ej.Salary >= salary
                         && ej.DesignationId == (DesignationId != 0 ? DesignationId : ej.DesignationId)
                        select new AllEmployeeDetailsDto
                        {
                            Id = e.Id,
                            EmployeeName = e.EmployeeName,
                            MobileNumber = ed.MobileNumber,
                            Address = ed.Address,
                            City = ed.City,
                          //  CompanyId = c.Id,
                            // CompanyName = c.CompanyName,
                           // Location = c.Location,
                            DesignationId = d.Id,
                            Designation = d.Designation1,
                            Salary = ej.Salary

                        }).ToList();
            return data;
        }

        public bool InsertNewEmployee([FromBody] EmployeeAddDto request)
        {
            if (request.Id == 0)
            {
                var employeeInsert = new Employee();
                employeeInsert.EmployeeName = request.EmployeeName;
                var employeeDet = new EmployeeDetail();
                var empdet = new List<EmployeeDetail>();
                employeeDet.EmpId = request.Id;
                employeeDet.Address = request.EmpDetails.Address;
                employeeDet.MobileNumber = request.EmpDetails.MobileNumber;
                employeeDet.City = request.EmpDetails.City;
                var empjobDet = new EmpJobDet();
                var empjbDetail = new List<EmpJobDet>();

                empjobDet.EmpId = request.Id;
                //  empjobDet.CompanyName = request.EmpJobDet.CompanyName;
                //  empjobDet.Designation = request.EmpJobDet.Designation;
                //// empjobDet.Location = request.EmpJobDet.Location;
                empjobDet.Salary = request.EmpJobDet.Salary;
                empdet.Add(employeeDet);
                empjbDetail.Add(empjobDet);
                employeeInsert.EmployeeDetails = empdet;
                employeeInsert.EmpJobDets = empjbDetail;
                var insertion = sample.Add<Employee>(employeeInsert);
                //var empDetInsert = sample.Add<EmployeeDetail>(employeeDet);
                //var empJobDetInsert = sample.Add<EmpJobDet>(empjobDet);
                sample.SaveChanges();
                var empinsert = new List<object>() { insertion };
                return true;
            }
            else
            {

                var empupdate = sample.Employees.Where(i => i.Id == request.Id).Select(i => i).FirstOrDefault();
                empupdate.EmployeeName = request.EmployeeName;
                var empdetData = new List<EmployeeDetail>();
                var empjobDetData = new List<EmpJobDet>();
                var empdet = sample.EmployeeDetails.Where(i => i.EmpId == request.Id).Select(i => i).FirstOrDefault();
                empdet.Address = request.EmpDetails.Address;
                empdet.MobileNumber = request.EmpDetails.MobileNumber;
                empdet.City = request.EmpDetails.City;

                var empjobDet = sample.EmpJobDets.Where(i => i.EmpId == request.Id).Select(i => i).FirstOrDefault();
                //empjobDet.CompanyName=request.EmpJobDet.CompanyName;
                //empjobDet.Designation=request.EmpJobDet.Designation;
                //empjobDet.Location=request.EmpJobDet.Location;
                empjobDet.Salary = request.EmpJobDet.Salary;

                empdetData.Add(empdet);
                empjobDetData.Add(empjobDet);


                empupdate.EmployeeDetails = empdetData;
                empupdate.EmpJobDets = empjobDetData;
                var updation = sample.Update<Employee>(empupdate);
                sample.SaveChanges();
                var empupdation = new List<Object> { updation };
                return true;

                //var empdet = new EmployeeDetail();
                //empdet.Id = request.EmpDetails.Id;
                //empdet.EmpId = request.Id;
                //empdet.Address = request.EmpDetails.Address;
                //empdet.MobileNumber = request.EmpDetails.MobileNumber;
                //empdet.City = request.EmpDetails.City;

                //var empjob = new EmpJobDet();
                //empjob.Id = request.EmpDetails.Id;
                //empjob.EmpId = request.Id;
                //empjob.CompanyName = request.EmpJobDet.CompanyName;
                //empjob.Designation = request.EmpJobDet.Designation;
                //empjob.Location = request.EmpJobDet.Location;
                //empjob.Salary = request.EmpJobDet.Salary;

                //empdetData.Add(empdet);
                //empjobDetData.Add(empjob);
                //empupdate.EmployeeDetails = empdetData;
                //empupdate.EmpJobDets = empjobDetData;
                //var updation = sample.Update<Employee>(empupdate);
                //var empdetupdate = sample.Update<EmployeeDetail>(empdet);
                //var empjobupdate = sample.Update<EmpJobDet>(empjob);
                //sample.SaveChanges();
                ////sample.Update(updation);
                //var empupdation = new List<Object>() { updation, empdetupdate, empjobupdate };
                ////sample.Update(empupdation);

                //return true;
            }
            return false;

        }

        public bool CreateNewEmployee([FromBody] EmployeeAddNewDto request)
        {
            if (request.Id == 0)
            {
                var employeeInsert = new Employee();
                employeeInsert.EmployeeName = request.EmployeeName;

                var employeeDet = new EmployeeDetail();
                var empdet = new List<EmployeeDetail>();
                employeeDet.EmpId = request.Id;
                employeeDet.Address = request.EmpDetails.Address;
                employeeDet.MobileNumber = request.EmpDetails.MobileNumber;
                employeeDet.City = request.EmpDetails.City;
                empdet.Add(employeeDet);

                var empDesignationDet = new EmpJobDet();
                var empDesignation = new List<EmpJobDet>();
                empDesignationDet.EmpId = request.Id;
                empDesignationDet.CompanyId = request.EmpDesignation.CompanyId;
                empDesignationDet.DesignationId = request.EmpDesignation.DesignationId;
                empDesignationDet.Salary = request.EmpDesignation.Salary;
                empDesignation.Add(empDesignationDet);

                employeeInsert.EmployeeDetails = empdet;
                employeeInsert.EmpJobDets = empDesignation;

                var insertion = sample.Add<Employee>(employeeInsert);
                sample.SaveChanges();
                var empinsert = new List<object>() { insertion };
                return true;

            }
            else
            {
                var employeeUpdate = sample.Employees.Where(i => i.Id == request.Id).Select(i => i).FirstOrDefault();
                employeeUpdate.EmployeeName = request.EmployeeName;

                var empDetailsDet = new List<EmployeeDetail>();
                var empDesignationDet = new List<EmpJobDet>();

                var empDetailsData = sample.EmployeeDetails.Where(i => i.EmpId == request.Id).Select(j => j).FirstOrDefault();
                empDetailsData.MobileNumber = request.EmpDetails.MobileNumber;
                empDetailsData.Address = request.EmpDetails.Address;
                empDetailsData.City = request.EmpDetails.City;

                empDetailsDet.Add(empDetailsData);


                var empDesignationData = sample.EmpJobDets.Where(j => j.EmpId == request.Id).Select(k => k).FirstOrDefault();
                empDesignationData.CompanyId = request.EmpDesignation.CompanyId;
                empDesignationData.DesignationId = request.EmpDesignation.DesignationId;
                empDesignationData.Salary = request.EmpDesignation.Salary;

                empDesignationDet.Add(empDesignationData);

                var updation = sample.Update<Employee>(employeeUpdate);
                sample.SaveChanges();
                var empUpdation = new List<object>() { updation };
                return true;
            }

            return false;
        }



    }
}
