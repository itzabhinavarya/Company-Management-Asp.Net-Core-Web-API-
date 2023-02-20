using Company_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Management.Services
{
    public interface IManagerServices
    {
        Task<GenericResult<string>> ManagerSetup(ManagerModel managerModel, string c);
    }
}
