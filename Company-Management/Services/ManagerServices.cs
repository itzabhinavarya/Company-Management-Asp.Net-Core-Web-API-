using Company_Management.Data;
using Company_Management.Models;
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
        
        public async Task<GenericResult<string>> ManagerSetup(ManagerModel managerModel,string c)
        {
            GenericResult<string> genericResult = new GenericResult<string>();
            var data = new ReportingManager()
            {
                Id = c,
                Name = managerModel.Name,
                Specialization = managerModel.Specialization,
                CreatedBy = c,
                UpdatedBy = c,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                Dstatus = "V"
            };
            await _company.ReportingManagers.AddAsync(data);
            _company.SaveChanges();
            genericResult.Status = "Success";
            genericResult.Message = "Manager Added Successfully..";
            return genericResult;
        }
    }
}
