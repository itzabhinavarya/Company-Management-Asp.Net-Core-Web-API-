using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Management.DTO
{
    public class LoginDTO
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
