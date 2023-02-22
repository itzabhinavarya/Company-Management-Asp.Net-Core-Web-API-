using Company_Management.DTO;
using Company_Management.Helper.HelperModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Management.Services
{
    public interface IGetAllEmployeeServices
    {
        Task<GenericResult<GetUser>> GetAllEmployee(string type, ClaimDTO claimDTO);
    }
}
