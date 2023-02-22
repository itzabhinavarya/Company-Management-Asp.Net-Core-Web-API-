using Company_Management.Data;
using Company_Management.DTO;
using Company_Management.Helper;
using Company_Management.Helper.HelperModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Company_Management.Services
{
    public class GetAllEmployeeServices : IGetAllEmployeeServices
    {
        private readonly companymanagementContext _company;

        public GetAllEmployeeServices(companymanagementContext company)
        {
            _company = company;
        }

        public async Task<GenericResult<GetUser>> GetAllEmployee(string type,ClaimDTO claimDTO)
        {
            var output = new GenericResult<GetUser>();
            try
            {
                var MId = claimDTO.MID;
                IList<GetUser> Employee = _company.Employees.Where(x => x.Id == MId && (type != null ? x.DepartmentId == int.Parse(type) : true)).Select(
                    x => new GetUser()
                    {
                        Id = x.EmployeeId,
                        Name = x.EmployeeFullName,
                        Email = _company.UserTables.Where(u => u.UserId == x.EmployeeId).Select(x => x.Email).FirstOrDefault(),
                        PhoneNo = _company.UserTables.Where(u => u.UserId == x.EmployeeId).Select(x => x.PhoneNo).FirstOrDefault(),
                        Role = _company.UserTables.Where(u => u.UserId == x.EmployeeId).Select(x => x.Role).FirstOrDefault(),
                        Status = _company.UserTables.Where(u => u.UserId == x.EmployeeId).Select(x => x.Status).FirstOrDefault()
                    }).ToList();
                if (Employee.Count > 0)
                {
                    output.Status = "Success";
                    output.Message = "Data Fetched Successfully";
                    output.Data = Employee;
                }
                else
                {
                    output.Status = "Error";
                    output.Message = "Data not found";
                }
            }
            catch (Exception err)
            {
                output.Status = "Error";
                output.Message = "Internal Server Error";
            }
            return output;
        }
    }
}
