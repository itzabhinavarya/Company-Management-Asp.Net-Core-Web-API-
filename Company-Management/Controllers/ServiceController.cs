using Company_Management.Data;
using Company_Management.DTO;
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
    [Route("api")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IService _service;

        public ServiceController(IService service)
        {
            _service = service;
        }

        [HttpPost("AddMember")]
        public async Task<IActionResult> AddUser(MemberModel memberModel)
        {
            var data =await _service.AddMember(memberModel);
            return Ok(data);
        }

        [HttpPost("OTP")]
        public async Task<IActionResult> GetOtp([FromBody]OTPModel otpModel)
        {
            var otp = await _service.GetOTP(otpModel);
            return Ok(otp);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Token(CredentialModel cred)
        {
            GenericResult<LoginDTO> result = await _service.Login(cred);
            return Ok(result);
        }
    }
}
