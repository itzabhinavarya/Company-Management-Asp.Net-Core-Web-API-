using System;
using System.Collections.Generic;

#nullable disable

namespace Company_Management.Data
{
    public partial class UserTable
    {
        public string Id { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string Dstatus { get; set; }
        public int UserId { get; set; }

        public virtual MemberTable IdNavigation { get; set; }
    }
}
