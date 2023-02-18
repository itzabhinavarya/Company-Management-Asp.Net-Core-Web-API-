using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Management.DTO
{
    public class LoginDTO
    {
        public string UserID { get; set; }
        public string MemberID { get; set; }
        public string Token { get; set; }
    }
}
