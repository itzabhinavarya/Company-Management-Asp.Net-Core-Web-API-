using System;
using System.Collections.Generic;

#nullable disable

namespace Company_Management.Data
{
    public partial class MemberTable
    {
        public MemberTable()
        {
            CompanyTables = new HashSet<CompanyTable>();
            Employees = new HashSet<Employee>();
            ReportingManagers = new HashSet<ReportingManager>();
            UserTables = new HashSet<UserTable>();
        }

        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string Dstatus { get; set; }

        public virtual ICollection<CompanyTable> CompanyTables { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<ReportingManager> ReportingManagers { get; set; }
        public virtual ICollection<UserTable> UserTables { get; set; }
    }
}
