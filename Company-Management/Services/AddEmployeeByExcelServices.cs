using Company_Management.Data;
using Company_Management.Helper.HelperModel;
using Company_Management.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Company_Management.Services
{
    public class AddEmployeeByExcelServices : IAddEmployeeByExcel
    {
        private readonly companymanagementContext _company;

        public AddEmployeeByExcelServices(companymanagementContext com)
        {
            _company = com;
        }

        public async Task<GenericResult<string>> AddEmployee(AddEmployeeExcelModel employeeModel, string MID)
        {
            GenericResult<string> genericResult = new GenericResult<string>();
            UserTable userData = null;

            userData = new UserTable()
            {
                Id = MID,
                //UserId = int.Parse(claimDTO.UserId),
                Email = employeeModel.userModel.Email,
                PhoneNo = employeeModel.userModel.PhoneNo,
                Password = employeeModel.userModel.Password,
                Role = employeeModel.userModel.Role,
                Status = employeeModel.userModel.Status,
                LastLogin = DateTime.Now,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                Dstatus = "V",
                CreatedBy = MID,
                UpdatedBy = MID,
            };
            await _company.UserTables.AddAsync(userData);
            await _company.SaveChangesAsync();

            var Empdata = new Employee()
            {
                Id = MID,
                CreatedBy = MID,
                UpdatedBy = MID,
                EmployeeFullName = employeeModel.EmployeeTableModel.EmployeeFullName,
                DateofJoining = employeeModel.EmployeeTableModel.DOJ,
                EmployementType = employeeModel.EmployeeTableModel.EmployementType,
                WorkingDays = employeeModel.EmployeeTableModel.WorkingDays,
                Email = employeeModel.userModel.Email,
                DepartmentId = employeeModel.EmployeeTableModel.DepartmentId,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                Dstatus = "V",
                EmployeeId = userData.UserId
            };
            await _company.Employees.AddAsync(Empdata);

            var EmpQualification = new Qualification()
            {
                MemberId = MID,
                EmpId = Empdata.EmployeeId,
                QualificationName = employeeModel.qualificationModel.QualificationName,
                InstituteName = employeeModel.qualificationModel.InstituteName,
                Score = employeeModel.qualificationModel.Score,
                City = employeeModel.qualificationModel.City,
                State = employeeModel.qualificationModel.State,
                QualificationStartYear = employeeModel.qualificationModel.QualificationStartYear,
                QualificationEndYear = employeeModel.qualificationModel.QualificationEndYear,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                Dstatus = "V",
            };
            await _company.Qualifications.AddAsync(EmpQualification);



            var PersonalData = new EmployeePersonalDetail()
            {
                Mid = MID,
                EmpId = Empdata.EmployeeId,
                CurrentHouseNo = employeeModel.employeePersonalDetails.CurrentHouseNo,
                CurrentAddressLine = employeeModel.employeePersonalDetails.CurrentAddressLine,
                CurrentLocality = employeeModel.employeePersonalDetails.CurrentLocality,
                CurrentPinCode = employeeModel.employeePersonalDetails.CurrentPinCode,
                CurrentCity = employeeModel.employeePersonalDetails.CurrentCity,
                CurrentState = employeeModel.employeePersonalDetails.CurrentState,
                PermanentHouseNo = employeeModel.employeePersonalDetails.PermannentHouseNo,
                PermanentAddressLine = employeeModel.employeePersonalDetails.PermannentAddressLine,
                PermanentLocality = employeeModel.employeePersonalDetails.PermannentLocality,
                PermanentPinCode = employeeModel.employeePersonalDetails.PermannentPincode,
                PermanentCity = employeeModel.employeePersonalDetails.PermannentCity,
                PermanentState = employeeModel.employeePersonalDetails.PermannentState,
                AlterNateEmail = employeeModel.employeePersonalDetails.AlternateEmail,
                AlterNatePhoneNo = employeeModel.employeePersonalDetails.AlternatePhoneNo
            };

            await _company.EmployeePersonalDetails.AddAsync(PersonalData);

            genericResult.Status = "Success";
            genericResult.Message = "Employee Added Successfully";

            await _company.SaveChangesAsync();

            return genericResult;

        }
    }
}
