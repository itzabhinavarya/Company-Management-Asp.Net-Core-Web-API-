using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Company_Management.Helper
{
    public  class Help
    {
        //------------------------VERIFY TOKEN-------------------------
        public static string GetClaims(HttpRequest httpRequest)
        {
            string res;
            //GenericResult<WelcomeModel> Auth = new GenericResult<WelcomeModel>();
            var identity = httpRequest.HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userclaims = identity.Claims;
                //var ClaimData = new WelcomeModel()
                //{
                //    Name = userclaims.FirstOrDefault(x => x.Type == "Name : ").Value,
                //    Email = userclaims.FirstOrDefault(x => x.Type == "Email : ").Value,
                //    Mobile = userclaims.FirstOrDefault(x => x.Type == "Phone : ").Value
                //};
                res = userclaims.FirstOrDefault(x => x.Type == "Member ID : ").Value;
            }
            else
            {
                res = "Member Id Doesn't Exist in Token";
            }
            return res;
        }
    }
}
