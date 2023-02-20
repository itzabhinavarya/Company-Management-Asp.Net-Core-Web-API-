using Company_Management.Data;
using Company_Management.DTO;
using Company_Management.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Company_Management.Services
{
    public class Service : IService
    {
        private readonly CompanyManagementContext _company;

        public IConfiguration _config { get; }

        public Service(CompanyManagementContext companyManagementContext , IConfiguration configuration)
        {
            _company = companyManagementContext;
            _config = configuration;
        }

        //----------------------------GENERATE ID FOR USER----------------------------------- 
        public async Task<string> GenerateId(MemberModel memberModel)
        {
            var existid =await _company.MemberTables.OrderBy(x => x.CreatedOn).Select(x => x.Id).LastOrDefaultAsync();
            var value = "UNI";
            var digit = 100;
            if (existid == null)
            {
                var Id = value + digit;
                return Id;
            }
            else
            {
                int i = int.Parse(existid.Remove(0, 3));
                i++;
                string Id = value + i;
                return Id;
            }
        }
        //----------------------------END OF ID GENERATE METHOD-----------------------------
        //
        //----------------------------OTP GENERATE METHOD----------------------------------- 
        public async Task<GenericResult<string>> GetOTP(OTPModel OtpModel)
        {
            GenericResult<string> genericResult = new GenericResult<string>();
            var existUser =await  _company.MemberTables.Where(x => x.Email == OtpModel.Email && x.PhoneNo == OtpModel.PhoneNo).Select(x=>x.Id).FirstOrDefaultAsync();
            if(existUser == null)
            {
                Random random = new Random();
                int min = 1000;
                int max = 9999;
                int otp = random.Next(min, max);

                var OTPData = new Otp()
                {
                    Otp1 = otp.ToString(),
                    Email = OtpModel.Email,
                    PhoneNo = OtpModel.PhoneNo,
                    IsVerified = 0
                };
                genericResult.Status = "Success";
                genericResult.Message = "OTP Generated Successfully....";
                genericResult.Data = otp;
                _company.Otps.Add(OTPData);
                _company.SaveChanges();
                return genericResult;
            }
            else
            {
                genericResult.Status = "Failed";
                genericResult.Message = "User Already Exists ... Can't Generate OTP .... Try to login";
                return genericResult;
            }
        }
        //----------------------------OTP GENERATE METHOD ENDS----------------------------------- 

        //----------------------------USER REGISTRATION SERVICES----------------------------------- 
        public async Task<GenericResult<MemberModel>> AddMember(MemberModel memberModel)
        {
            GenericResult<MemberModel> genericResult = new GenericResult<MemberModel>();
            var validOtp =await _company.Otps.Where(x=>x.PhoneNo == memberModel.PhoneNo && x.Email == memberModel.Email && x.IsVerified == 0).OrderBy(x=>x.Otpid).LastOrDefaultAsync();
            var member =await _company.MemberTables.Where(x => x.PhoneNo == memberModel.PhoneNo && x.Email == memberModel.Email).FirstOrDefaultAsync();
            if (validOtp != null)
            {
                if (memberModel.OTP == validOtp.Otp1)
                {
                    if (member == null)
                    {
                        var MemberData = new MemberTable()
                        {
                            Id = await GenerateId(memberModel),
                            FullName = memberModel.FullName,
                            Email = memberModel.Email,
                            PhoneNo = memberModel.PhoneNo,
                            CreatedOn = DateTime.Now,
                            UpdatedOn = DateTime.Now,
                            Dstatus = "V",
                        };
                        var UserData = new UserTable()
                        {
                            //UserId = MemberData.Id,
                            Id = MemberData.Id,
                            PhoneNo = MemberData.PhoneNo,
                            Email = MemberData.Email,
                            Password = memberModel.Password,
                            LastLogin = DateTime.Now,
                            Role = "Owner",
                            Status = "Admin",
                            CreatedOn = DateTime.Now,
                            UpdatedOn = DateTime.Now,
                            Dstatus = "V"
                        };
                        validOtp.IsVerified = 1;
                        MemberData.CreatedBy = MemberData.Id;
                        MemberData.UpdatedBy = MemberData.Id;
                        UserData.CreatedBy = MemberData.Id;
                        UserData.UpdatedBy = MemberData.Id;
                        genericResult.Status = "Success";
                        genericResult.Message = "User Added Successfully";
                        //genericResult.Data = MemberData;
                        _company.Otps.Update(validOtp);
                        await _company.MemberTables.AddAsync(MemberData);
                        _company.UserTables.Add(UserData);
                        await _company.SaveChangesAsync();
                        return genericResult;
                    }
                    genericResult.Status = "Failed";
                    genericResult.Message = "User Already Exist. Try to login";
                    return genericResult;
                }
            }
            genericResult.Status = "Failed";
            genericResult.Message = "Invalid OTP";
            return genericResult;
        }
        //----------------------------END OF USER REGISTRATION SERVICES----------------------------------- 
        //
        //----------------------------Login Service----------------------------------------
        //
        public async Task<GenericResult<LoginDTO>> Login(CredentialModel credentialModel)
        {
            GenericResult<LoginDTO> genericResult = new GenericResult<LoginDTO>();

            var phone = await _company.UserTables.Where(x=>x.PhoneNo == credentialModel.Cred && x.Password == credentialModel.Password).FirstOrDefaultAsync();
            var email = await _company.UserTables.Where(x=>x.Email == credentialModel.Cred && x.Password == credentialModel.Password).FirstOrDefaultAsync();
            //var existData = await _company.UserTables.Where(x => x.PhoneNo == credentialModel.Cred || x.Email == credentialModel.Cred).FirstOrDefaultAsync();
            var type = credentialModel.Type.ToLower();
            if(type == "email")
            {
                if (email == null)
                {
                    genericResult.Status = "Failed";
                    genericResult.Message = "Invalid Email";
                    return genericResult;
                }
            }
            if (type == "phone")
            {
                if (phone == null)
                {
                    genericResult.Status = "Failed";
                    genericResult.Message = "Invalid Phone";
                    return genericResult;
                }
            }
            var exist = await _company.UserTables.Where(x => x.Email == credentialModel.Cred && x.Password == credentialModel.Password || x.PhoneNo == credentialModel.Cred && x.Password == credentialModel.Password).FirstOrDefaultAsync();
            if (exist != null)
            {
                var token = GenerateToken(exist);
                var newData = new LoginDTO()
                {
                    UserID = exist.UserId.ToString(),
                    MemberID = exist.Id,
                    Status = exist.Status,
                    Token = token
                };
                genericResult.Status = "Success";
                genericResult.Message = "Token Generated Successfully";
                genericResult.Data = newData;
                return genericResult;
            }
            else
            {
                genericResult.Status = "Failed";
                genericResult.Message = "Invalid Password";
                return genericResult;
            }
        }
        //
        //----------------------------Authentication And Authorization -----------------------------------
        //
        public string GenerateToken(UserTable cred)
        {
            var IssuerSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var SigninCredentials = new SigningCredentials(IssuerSigninKey,SecurityAlgorithms.HmacSha256);
            
            var Claims = new[]
            {
                new Claim("Member ID : ",cred.Id),
                new Claim("User ID : ",cred.UserId.ToString())
            };
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                Claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: SigninCredentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        //
        //
        //-----------------------------





    }
}
