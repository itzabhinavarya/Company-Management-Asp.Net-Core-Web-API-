using System;
using System.Collections.Generic;

#nullable disable

namespace Company_Management.Data
{
    public partial class Otp
    {
        public int Otpid { get; set; }
        public string Otp1 { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public int? IsVerified { get; set; }
    }
}
