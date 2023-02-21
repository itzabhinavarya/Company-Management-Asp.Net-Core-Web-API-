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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentInterface;

        public DepartmentController(IDepartmentService departmentInterface)
        {
            _departmentInterface = departmentInterface;
        }
        [HttpPost("Department")]
        public async Task<IActionResult> AddDepartment(DepartmentModel departmentModel)
        {
            string c = Help.GetClaims(Request).MID;
            GenericResult<string> data =await _departmentInterface.AddDepartmentAsync(departmentModel,c);
            return Ok(data);
        }
    }
}
