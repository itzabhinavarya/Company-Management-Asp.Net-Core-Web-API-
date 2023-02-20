using System;
using System.Collections.Generic;

#nullable disable

namespace Company_Management.Data
{
    public partial class EmployeePersonalDetail
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public string Mid { get; set; }
        public string PermanentHouseNo { get; set; }
        public string PermanentAddressLine { get; set; }
        public string PermanentLocality { get; set; }
        public string PermanentPinCode { get; set; }
        public string PermanentCity { get; set; }
        public string PermanentState { get; set; }
        public string CurrentHouseNo { get; set; }
        public string CurrentAddressLine { get; set; }
        public string CurrentLocality { get; set; }
        public string CurrentPinCode { get; set; }
        public string CurrentCity { get; set; }
        public string CurrentState { get; set; }
        public string AlterNatePhoneNo { get; set; }
        public string AlterNateEmail { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? CreatedBy { get; set; }
        public DateTime? UpdatedBy { get; set; }
        public string Dstatus { get; set; }

        public virtual Employee Emp { get; set; }
        public virtual MemberTable MidNavigation { get; set; }
    }
}
