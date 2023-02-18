using Company_Management.Data;
using Company_Management.DTO;
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
        Task<GenericResult<LoginDTO>> Login(CredentialModel credentialModel);
    }
}
