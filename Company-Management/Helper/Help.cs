using Company_Management.Helper.HelperModel;
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
        public static ClaimDTO GetClaims(HttpRequest httpRequest)
        {
            ClaimDTO claimDTO;
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
                claimDTO = new ClaimDTO()
                {
                    MID = userclaims.FirstOrDefault(x => x.Type == "Member ID : ").Value,
                    UserId = userclaims.FirstOrDefault(x => x.Type == "User ID : ").Value,
                };
            }
            else
            {
                claimDTO = null;
            }
            return claimDTO;
        }
    }
}
