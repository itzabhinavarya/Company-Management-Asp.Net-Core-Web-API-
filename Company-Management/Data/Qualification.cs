using System;
using System.Collections.Generic;

#nullable disable

namespace Company_Management.Data
{
    public partial class Qualification
    {
        public int QualificationId { get; set; }
        public int? EmpId { get; set; }
        public string QualificationName { get; set; }
        public string QualificationStartYear { get; set; }
        public string QualificationEndYear { get; set; }
        public string InstituteName { get; set; }
        public string Score { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string MemberId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? CreatedBy { get; set; }
        public DateTime? UpdatedBy { get; set; }
        public string Dstatus { get; set; }

        public virtual Employee Emp { get; set; }
        public virtual MemberTable Member { get; set; }
    }
}
