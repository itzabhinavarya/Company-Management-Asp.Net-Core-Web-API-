using Company_Management.Helper.HelperModel;
using Company_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Management.Services
{
    public interface IAddEmployeeByExcel
    {
        Task<GenericResult<string>> AddEmployee(AddEmployeeExcelModel employeeModel, string MID);
    }
}
