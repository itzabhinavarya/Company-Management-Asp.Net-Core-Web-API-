using Company_Management.Helper;
using Company_Management.Helper.HelperModel;
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
    public class GetAllEmployeeController : ControllerBase
    {
        private readonly IGetAllEmployeeServices _employee;

        public GetAllEmployeeController(IGetAllEmployeeServices employee)
        {
            _employee = employee;
        }

        [HttpGet("GetAllEmployee/{type}")]
        [Authorize]
        public async Task<IActionResult> GetAllEmployee([FromRoute] string type)
        {
            //var claim = Help.GetClaims(Request);
            var MID = Help.GetClaims(Request);
            //var UID = Help.GetClaims(Request).UserId;
            var data =await _employee.GetAllEmployee(type,MID);
            return Ok(data);
        }
    }
}
