using Company_Management.Helper;
using Company_Management.Models;
using Company_Management.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyServices _company;

        public CompanyController(ICompanyServices companyService)
        {
            _company = companyService;
        }
        [HttpPost("AddCompany")]
        public async Task<IActionResult> AddCompany(CompanyModel companyModel)
        {
            string claim = Help.GetClaims(Request).MID;
            GenericResult<string> s =await _company.SetupCompany(companyModel,claim);
            return Ok(s);
        }
    }
}
