using Company_Management.Data;
using Company_Management.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

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

            try
            {
                var emp = await _company.Employees.Where(x => x.EmployeeId == managerModel.Id).FirstOrDefaultAsync();
                var Manager = new ReportingManager()
                {
                    ManagerId = emp.EmployeeId,
                    Id = MId,
                    Specialization = managerModel.Specialization,
                    CreatedBy = MId,
                    UpdatedBy = MId,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    Dstatus = "A",
                };
                await _company.ReportingManagers.AddAsync(Manager);
                var depdata = await _company.DepartmentTables.Where(x => x.DepartmentId == managerModel.DeptId).FirstOrDefaultAsync();
                depdata.ManagerId = Manager.ManagerId;
                _company.DepartmentTables.Update(depdata);
                var EmpData = await _company.Employees.Where(x => x.DepartmentId == managerModel.DeptId).ToListAsync();
                foreach (var item in EmpData)
                {
                    item.ManagerId = managerModel.Id;
                    _company.Employees.Update(item);
                }
                var userData = await _company.UserTables.Where(x => x.UserId == emp.EmployeeId).FirstOrDefaultAsync();
                userData.Role = "Manager";
                userData.Status = "Sub-Admin";
                userData.UpdatedOn = DateTime.Now;
                _company.UserTables.Update(userData);
                await _company.SaveChangesAsync();
                genericResult.Status = "Success";
                genericResult.Message = "Manager Added Successfully...";
                genericResult.Data = null;
            }
            catch (Exception ex)
            {
                genericResult.Status = "Failed";
                genericResult.Message = "Manager Can't Added";
                genericResult.Data = null;
                Transaction.Current.Rollback();
            }
            return genericResult;
        }
    }
}
