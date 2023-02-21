using Company_Management.Data;
using Company_Management.Helper;
using Company_Management.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Management.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly companymanagementContext _company;

        public DepartmentService(companymanagementContext companyManagementContext)
        {
            _company = companyManagementContext;
        }
        //
        //---------------------------ADD DEPARTMENT SERVICES---------------------
        //
        public async Task<GenericResult<string>> AddDepartmentAsync(DepartmentModel Dept, string c)
        {
            GenericResult<string> output = new GenericResult<string>();

            foreach (var Dname in Dept.departments)
            {
                var data = new DepartmentTable()
                {
                    Id = c,
                    DepartmentName = Dname.DepartmentName,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    CreatedBy = c,
                    Dstatus = "A"
                };
                await _company.DepartmentTables.AddAsync(data);
            }
            await _company.SaveChangesAsync();
            output.Status = "Success";
            output.Message = "Department Added Successfully";
            return output;
        }

    }
}
