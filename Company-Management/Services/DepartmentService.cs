using Company_Management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Management.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly CompanyManagementContext _company;

        public DepartmentService(CompanyManagementContext companyManagementContext)
        {
            _company = companyManagementContext;
        }
        //
        //---------------------------ADD DEPARTMENT SERVICES---------------------
        //
        
    }
}
