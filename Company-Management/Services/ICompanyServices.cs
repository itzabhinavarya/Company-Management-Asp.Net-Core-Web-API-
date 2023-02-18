using Company_Management.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Management.Services
{
    public interface ICompanyServices
    {
        Task<GenericResult<string>> SetupCompany(CompanyModel companyModel, HttpResponse httpResponse);
    }
}
