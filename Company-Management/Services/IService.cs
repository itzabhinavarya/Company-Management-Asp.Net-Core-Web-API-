using Company_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Management.Services
{
    public interface IService
    {
        Task<GenericResult<MemberModel>> AddMember(MemberModel memberModel);
        Task<GenericResult<string>> GetOTP(OTPModel OtpModel);
    }
}
