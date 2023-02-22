using Company_Management.Data;
using Company_Management.DTO;
using Company_Management.Helper.HelperModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Company_Management.Services
{
    public class GetEmployeeServices : IGetEmployeeServices
    {
        private readonly companymanagementContext _company;

        public GetEmployeeServices(companymanagementContext company)
        {
            _company = company;
        }
        public async Task<GenericResult<EmployeeDTO>> GetEmployeeById(int id,ClaimDTO claimDTO)
        {
            GenericResult<EmployeeDTO> genericResult = new GenericResult<EmployeeDTO>();
            try
            {
                var mid = claimDTO.MID;
                var UT = await _company.UserTables.Where(x => x.Id == mid && x.UserId == id).FirstOrDefaultAsync();
                var EPD = await _company.EmployeePersonalDetails.Where(x => x.Mid == mid && x.EmpId == id).FirstOrDefaultAsync();
                var emp = await _company.Employees.Where(x => x.EmployeeId == id && x.Id == mid).FirstOrDefaultAsync();
                IList<EmployeeGetQualificationModel> qualification = _company.Qualifications.Where(x => x.MemberId == mid && x.EmpId == id).Select(x => new EmployeeGetQualificationModel()
                {
                    InstituteName = x.InstituteName,
                    QualificationName = x.QualificationName,
                    QualificationStartYear = x.QualificationStartYear,
                    QualificationEndYear = x.QualificationEndYear,
                    Score = x.Score,
                    City = x.City,
                    State = x.State
                }).ToList();
                var res = new EmployeeDTO()
                {
                    employeePersonalDetails = new EmployeePersonalDetailsDTO()
                    {
                        PermannentHouseNo = EPD.PermanentHouseNo,
                        PermannentAddressLine = EPD.PermanentAddressLine,
                        PermannentLocality = EPD.PermanentLocality,
                        PermannentPincode = EPD.PermanentPinCode,
                        PermannentCity = EPD.PermanentCity,
                        PermannentState = EPD.PermanentState,
                        CurrentHouseNo = EPD.CurrentHouseNo,
                        CurrentAddressLine = EPD.CurrentAddressLine,
                        CurrentLocality = EPD.CurrentLocality,
                        CurrentPinCode = EPD.CurrentPinCode,
                        CurrentCity = EPD.CurrentCity,
                        CurrentState = EPD.CurrentState,
                        AlternateEmail = EPD.AlterNateEmail,
                        AlternatePhoneNo = EPD.AlterNatePhoneNo
                    },
                    EmployeeTableModel = new GetEmp()
                    {
                        EmployeeFullName = emp.EmployeeFullName,
                        EmpployementType = emp.EmployementType,
                        WorkingDays = (int)emp.WorkingDays,
                        DOJ = (DateTime)emp.DateofJoining
                    },
                    userDTO = new EmployeeGetUser()
                    {
                        Name = emp.EmployeeFullName,
                        Email = UT.Email,
                        PhoneNo = UT.PhoneNo,
                        Role = UT.Role,
                        Status = UT.Status
                    },
                    qualificationModel = qualification,
                };
                if (res != null)
                {
                    genericResult.Status = "Success";
                    genericResult.Message = "Details Fetched Successfully";
                    genericResult.Data = res;
                }
                else
                {
                    genericResult.Status = "Failed";
                    genericResult.Message = "No Data Available";
                }
            }
            catch (Exception err)
            {
                genericResult.Status = "Error";
                genericResult.Message = "Internal Server Error";
                Transaction.Current.Rollback();
            }
            return genericResult;
        }
    }
}
