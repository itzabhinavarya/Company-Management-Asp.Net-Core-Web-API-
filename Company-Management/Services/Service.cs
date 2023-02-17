using Company_Management.Data;
using Company_Management.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Management.Services
{
    public class Service : IService
    {
        private readonly CompanyManagementContext _company;
        public Service(CompanyManagementContext companyManagementContext)
        {
            _company = companyManagementContext;
        }
        //----------------------------GENERATE ID FOR USER----------------------------------- 
        public async Task<string> GenerateId(MemberModel memberModel)
        {
            //var existid = _company.MemberTables.Where(x => x.Email == memberModel.Email || x.PhoneNo == memberModel.PhoneNo).Select(x => x.Id).FirstOrDefault();
            var existid = _company.MemberTables.OrderBy(x => x.CreatedOn).Select(x => x.Id).LastOrDefault();
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
        //----------------------------END OF ID GENERATE METHOD----------------------------------- 
        //----------------------------OTP GENERATE METHOD----------------------------------- 
        public async Task<GenericResult<string>> GetOTP(OTPModel OtpModel)
        {
            GenericResult<string> genericResult = new GenericResult<string>();
            var existUser =  _company.MemberTables.Where(x => x.Email == OtpModel.Email || x.PhoneNo == OtpModel.PhoneNo).Select(x=>x.Id).FirstOrDefault();
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
        //
        //----------------------------USER REGISTRATION SERVICES----------------------------------- 
        public async Task<GenericResult<MemberModel>> AddMember(MemberModel memberModel)
        {
            GenericResult<MemberModel> genericResult = new GenericResult<MemberModel>();
            var validOtp = await _company.Otps.Where(x=>x.PhoneNo == memberModel.PhoneNo && x.Email == memberModel.Email && x.IsVerified == 0).FirstOrDefaultAsync();
            var member =await _company.MemberTables.Where(x => x.PhoneNo == memberModel.PhoneNo || x.Email == memberModel.Email).FirstOrDefaultAsync();
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
                            UserId = MemberData.Id,
                            Id = MemberData.Id,
                            PhoneNo = MemberData.PhoneNo,
                            Email = MemberData.Email,
                            Password = memberModel.Password,
                            LastLogin = DateTime.Now,
                            Role = memberModel.Role,
                            Status = memberModel.Status,
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
                        //enericResult.Data = MemberData;
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
    }
}
