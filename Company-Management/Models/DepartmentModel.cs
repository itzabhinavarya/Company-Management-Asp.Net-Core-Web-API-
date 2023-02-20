using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Management.Models
{
    public class Department
    {
        public string DepartmentName { get; set; }
    }
    public class DepartmentModel
    {
        public List<Department> departments { get; set; }
    }
}
