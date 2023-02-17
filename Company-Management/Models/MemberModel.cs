using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Management.Models
{
    public class MemberModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string OTP { get; set; }
        public string Password { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
    }
}
