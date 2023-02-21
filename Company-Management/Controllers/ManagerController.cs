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
    public class ManagerController : ControllerBase
    {
        private readonly IManagerServices _managerServices;

        public ManagerController(IManagerServices managerServices)
        {
            _managerServices = managerServices;
        }
        [HttpPost("AddManager")]
        public async Task<IActionResult> AddManager(ManagerModel managerModel)
        {
            string c = Help.GetClaims(Request).MID;
            GenericResult<string> data = await _managerServices.ManagerSetup(managerModel, c);
            return Ok(data);
        }
    }
}
