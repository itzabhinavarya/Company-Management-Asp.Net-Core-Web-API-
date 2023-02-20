using Company_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Management.Services
{
    public interface IDepartmentService
    {
        Task<GenericResult<string>> AddDepartmentAsync(DepartmentModel Dept, string c);
    }
}
