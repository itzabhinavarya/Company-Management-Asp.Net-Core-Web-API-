using Company_Management.Helper;
using Company_Management.Helper.HelperModel;
using Company_Management.Models;
using Company_Management.Services;
using Microsoft.AspNetCore.Authorization;
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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices _employeeServices;

        public EmployeeController(IEmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
        }

        [HttpPost("AddEmployee/{TempId}")]
        [Authorize]
        public async Task<IActionResult> AddEmployee(EmployeeModel employeeModel ,[FromRoute] int TempId)
        {
            var data = await _employeeServices.AddEmployee(employeeModel,Help.GetClaims(Request),TempId);
            return Ok(data);
        }
    }
}
