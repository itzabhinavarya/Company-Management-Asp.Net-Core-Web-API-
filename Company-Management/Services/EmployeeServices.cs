using Company_Management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Management.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly CompanyManagementContext _company;

        public EmployeeServices(CompanyManagementContext company)
        {
            _company = company;
        }

    }
}
