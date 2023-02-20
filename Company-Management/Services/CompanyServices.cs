using Company_Management.Data;
using Company_Management.Helper;
using Company_Management.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Company_Management.Services
{
    public class CompanyServices : ICompanyServices
    {
        private readonly CompanyManagementContext _company;

        public CompanyServices(CompanyManagementContext companyManagementContext)
        {
            _company = companyManagementContext;
        }
        ////------------------------VERIFY TOKEN-------------------------
        //public static string VerifyToken(HttpRequest httpRequest)
        //{
        //    string res;
        //    //GenericResult<WelcomeModel> Auth = new GenericResult<WelcomeModel>();
        //    var identity = httpRequest.HttpContext.User.Identity as ClaimsIdentity;
        //    if (identity != null)
        //    {
        //        var userclaims = identity.Claims;
        //        //var ClaimData = new WelcomeModel()
        //        //{
        //        //    Name = userclaims.FirstOrDefault(x => x.Type == "Name : ").Value,
        //        //    Email = userclaims.FirstOrDefault(x => x.Type == "Email : ").Value,
        //        //    Mobile = userclaims.FirstOrDefault(x => x.Type == "Phone : ").Value
        //        //};
        //        res = userclaims.FirstOrDefault(x => x.Type == "Member ID : ").Value;
        //    }
        //    else
        //    {
        //        res = null;
        //    }
        //    return res;
        //}

        //
        //-----------------------COMPANY SERVICES--------------------
        //
        public async Task<GenericResult<string>> SetupCompany(CompanyModel companyModel,string MID)
        {
            GenericResult<string> genericResult = new GenericResult<string>();
            //string MID = Help.VerifyToken(httpRequest);
            //if (MID != null)
            //{
                var newCompany = new CompanyTable()
                {
                    Id = MID,
                    CompanyName = companyModel.CompanyName,
                    CompanyMobieNo = companyModel.CompanyMobieNo,
                    CompanyAddress = companyModel.CompanyAddress,
                    CompanyCity = companyModel.CompanyCity,
                    CompanyState = companyModel.CompanyState,
                    CompanyCountry = companyModel.CompanyCountry,
                    Dstatus = "V",
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    CreatedBy = MID,
                    UpdatedBy = MID,
                };
                await _company.CompanyTables.AddAsync(newCompany);
                await _company.SaveChangesAsync();

                genericResult.Status = "Success";
                genericResult.Message = "Company Added Successfully...";
            //}
            //else
            //{
            //    genericResult.Status = "Failed";
            //    genericResult.Message = "Unauthorized";
            //}
            return genericResult;
        }
    }
}
