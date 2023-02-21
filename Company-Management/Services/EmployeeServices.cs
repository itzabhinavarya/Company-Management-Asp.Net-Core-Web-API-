using Company_Management.Data;
using Company_Management.Helper.HelperModel;
using Company_Management.Models;
using System;
using System.Threading.Tasks;

namespace Company_Management.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly companymanagementContext _company;

        public EmployeeServices(companymanagementContext company)
        {
            _company = company;
        }
        //public async Task<GenericResult<string>> EmployeeQualification(EmployeeModel employee,ClaimDTO claimDTO)
        //{
        //    GenericResult<string> genericResult = new GenericResult<string>();
        //    foreach(var data in employee.qualificationModel)
        //    {
        //        var EmpQualification = new Qualification()
        //        {
        //            MemberId = claimDTO.MID,
        //            EmpId = int.Parse(claimDTO.UserId),
        //            QualificationName = data.QualificationName,
        //            InstituteName = data.InstituteName,
        //            Score = data.Score,
        //            City = data.City,
        //            State = data.State,
        //            QualificationStartYear = data.QualificationStartYear,
        //            QualificationEndYear = data.QualificationEndYear,
        //            CreatedOn = DateTime.Now,
        //            UpdatedOn = DateTime.Now,
        //            Dstatus = "V",
        //        };
        //        await _company.Qualifications.AddAsync(EmpQualification);
        //    }
        //    genericResult.Status = "Success";
        //    genericResult.Message = "Qualification Added Successfully";
        //    await _company.SaveChangesAsync();
        //    return genericResult;
        //}
        //
        public async Task<GenericResult<string>> AddEmployee(EmployeeModel employeeModel,ClaimDTO claimDTO)
        {
            GenericResult<string> genericResult = new GenericResult<string>();
            var userData = new UserTable()
            {
                Id = claimDTO.MID,
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
                CreatedBy = claimDTO.MID,
                UpdatedBy = claimDTO.MID,
            };

            await _company.UserTables.AddAsync(userData);
            await _company.SaveChangesAsync();

            var Empdata = new Employee()
            {
                Id = claimDTO.MID,
                CreatedBy = claimDTO.MID,
                UpdatedBy = claimDTO.MID,
                EmployeeFullName = employeeModel.EmployeeTableModel.EmployeeFullName,
                DateofJoining = employeeModel.EmployeeTableModel.DOJ,
                EmployementType = employeeModel.EmployeeTableModel.EmpployementType,
                WorkingDays = employeeModel.EmployeeTableModel.WorkingDays,
                Email = employeeModel.userModel.Email,
                //DepartmentId = employeeModel.EmployeeTableModel.DepartmentId,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                Dstatus = "V",
                EmployeeId = userData.UserId
            };

            await _company.Employees.AddAsync(Empdata);
            await _company.SaveChangesAsync();

            foreach (var data in employeeModel.qualificationModel)
            {
                var EmpQualification = new Qualification()
                {
                    MemberId = claimDTO.MID,
                    EmpId = Empdata.EmployeeId,
                    QualificationName = data.QualificationName,
                    InstituteName = data.InstituteName,
                    Score = data.Score,
                    City = data.City,
                    State = data.State,
                    QualificationStartYear = data.QualificationStartYear,
                    QualificationEndYear = data.QualificationEndYear, 
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    Dstatus = "V",
                };
                await _company.Qualifications.AddAsync(EmpQualification);
            }



            var PersonalData = new EmployeePersonalDetail()
            {
                Mid = claimDTO.MID,
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
            await _company.SaveChangesAsync();

            genericResult.Status = "Success";
            genericResult.Message = "Qualification Added Successfully";
            await _company.SaveChangesAsync();
            return genericResult;
        }
    }
}
