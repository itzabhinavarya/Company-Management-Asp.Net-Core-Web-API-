using System;
using System.Collections.Generic;

#nullable disable

namespace Company_Management.Data
{
    public partial class ReportingManager
    {
        public ReportingManager()
        {
            DepartmentTables = new HashSet<DepartmentTable>();
            Employees = new HashSet<Employee>();
        }

        public string Id { get; set; }
        public string Specialization { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Dstatus { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int ManagerId { get; set; }

        public virtual MemberTable IdNavigation { get; set; }
        public virtual ICollection<DepartmentTable> DepartmentTables { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
