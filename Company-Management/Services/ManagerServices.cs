using Company_Management.Data;
using Company_Management.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Management.Services
{
    public class ManagerServices : IManagerServices
    {
        private readonly companymanagementContext _company;

        public ManagerServices(companymanagementContext company)
        {
            _company = company;
        }
        
        public async Task<GenericResult<string>> ManagerSetup(ManagerModel managerModel,string MId)
        {
            GenericResult<string> genericResult = new GenericResult<string>();
            var emp = await _company.ReportingManagers.Where(x => x.Id == managerModel.Id).FirstOrDefaultAsync();
            var Manager = new ReportingManager()
            {
                ManagerId = int.Parse(emp.Id),
                Id = MId,
                Specialization = managerModel.Specialization,
                CreatedBy = MId,
                UpdatedBy = MId,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                Dstatus = "A",
            };
            await _company.ReportingManagers.AddAsync(Manager);
            _company.SaveChanges();
            genericResult.Status = "Success";
            genericResult.Message = "Manager Added Successfully..";
            return genericResult;
        }
    }
}
