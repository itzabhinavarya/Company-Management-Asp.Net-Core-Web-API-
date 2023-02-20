using System;
using System.Collections.Generic;

#nullable disable

namespace Company_Management.Data
{
    public partial class DepartmentTable
    {
        public DepartmentTable()
        {
            Employees = new HashSet<Employee>();
        }

        public string Id { get; set; }
        public int? ManagerId { get; set; }
        public string DepartmentName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string Dstatus { get; set; }
        public int DepartmentId { get; set; }

        public virtual MemberTable IdNavigation { get; set; }
        public virtual ReportingManager Manager { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
