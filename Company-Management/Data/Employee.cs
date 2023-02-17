using System;
using System.Collections.Generic;

#nullable disable

namespace Company_Management.Data
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public string Id { get; set; }
        public int? DepartmentId { get; set; }
        public string EmployeeFullName { get; set; }
        public string Email { get; set; }
        public DateTime? DateofJoining { get; set; }
        public string EmployementType { get; set; }
        public int? WorkingDays { get; set; }
        public int? WeekOffDays { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string Dstatus { get; set; }

        public virtual DepartmentTable Department { get; set; }
        public virtual MemberTable IdNavigation { get; set; }
    }
}
