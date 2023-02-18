using Company_Management.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Management.Services
{
    public class GenericResult<T> : IGenricresult<T>
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

       
       
    }
    public interface IGenricresult<T>
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

    }
}
