using Company_Management.Helper;
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
    public class GetEmployeeController : ControllerBase
    {
        private readonly IGetEmployeeServices _employee;

        public GetEmployeeController(IGetEmployeeServices employee)
        {
            _employee = employee;
        }
        [HttpGet("GetEmployeeById/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetEmployeeById([FromRoute]int Id)
        {
            var claim = Help.GetClaims(Request);
            var data =await _employee.GetEmployeeById(Id, claim);
            return Ok(data);
        }
    }
}
