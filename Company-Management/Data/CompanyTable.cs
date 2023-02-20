using System;
using System.Collections.Generic;

#nullable disable

namespace Company_Management.Data
{
    public partial class CompanyTable
    {
        public int CompanyId { get; set; }
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyMobieNo { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyState { get; set; }
        public string CompanyCountry { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string Dstatus { get; set; }

        public virtual MemberTable IdNavigation { get; set; }
    }
}
