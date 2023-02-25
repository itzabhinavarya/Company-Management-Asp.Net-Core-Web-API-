using Company_Management.Helper.HelperModel;
using Company_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Management.Services
{
    public interface IEmployeeServices
    {
        //Task<GenericResult<string>> EmployeeQualification(EmployeeModel employee, ClaimDTO claimDTO);
        Task<GenericResult<string>> AddEmployee(EmployeeModel employeeModel, ClaimDTO claimDTO, int Tempid);
    }
}
