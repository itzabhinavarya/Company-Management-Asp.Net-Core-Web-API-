using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Management.Models
{
    public class ManagerModel
    {
        public int Id { get; set; }
        public string Specialization { get; set; }
        public int DeptId { get; set; }
    }
}
