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
        public async Task<GenericResult<string>> AddEmployee(EmployeeModel employeeModel,ClaimDTO claimDTO , int Tempid)
        {
            GenericResult<string> genericResult = new GenericResult<string>();
            UserTable userData = null;

            try
            {
                UserTable ExistUser = await _company.UserTables.Where(x => x.Id == claimDTO.MID && x.UserId == Tempid).FirstOrDefaultAsync();
                
                if (ExistUser != null)
                {
                    //var userData = new UserTable()
                    //{
                    ExistUser.Id = claimDTO.MID;
                    //UserId = int.Parse(claimDTO.UserId),
                    ExistUser.Email = employeeModel.userModel.Email;
                    ExistUser.PhoneNo = employeeModel.userModel.PhoneNo;
                    ExistUser.Password = employeeModel.userModel.Password;
                    ExistUser.Role = employeeModel.userModel.Role;
                    ExistUser.Status = employeeModel.userModel.Status;
                    ExistUser.LastLogin = DateTime.Now;
                    ExistUser.CreatedOn = DateTime.Now;
                    ExistUser.UpdatedOn = DateTime.Now;
                    ExistUser.Dstatus = "V";
                    ExistUser.CreatedBy = claimDTO.MID;
                    ExistUser.UpdatedBy = claimDTO.MID;
                    //};
                    await _company.SaveChangesAsync();

                    var ExistEmp =await _company.Employees.Where(x => x.Id == claimDTO.MID && x.EmployeeId == Tempid).FirstOrDefaultAsync();
                    
                    ExistEmp.Id = claimDTO.MID;
                    ExistEmp.CreatedBy = claimDTO.MID;
                    ExistEmp.UpdatedBy = claimDTO.MID;
                    ExistEmp.EmployeeFullName = employeeModel.EmployeeTableModel.EmployeeFullName;
                    ExistEmp.DateofJoining = employeeModel.EmployeeTableModel.DOJ;
                    ExistEmp.EmployementType = employeeModel.EmployeeTableModel.EmpployementType;
                    ExistEmp.WorkingDays = employeeModel.EmployeeTableModel.WorkingDays;
                    ExistEmp.Email = employeeModel.userModel.Email;
                    ExistEmp.DepartmentId = employeeModel.EmployeeTableModel.DepartmentId;
                    ExistEmp.CreatedOn = DateTime.Now;
                    ExistEmp.UpdatedOn = DateTime.Now;
                    ExistEmp.Dstatus = "V";
                    ExistEmp.EmployeeId = ExistUser.UserId;

                    await _company.SaveChangesAsync(); 

                    List<Qualification> ExistQual = await _company.Qualifications.Where(x => x.MemberId == claimDTO.MID && x.EmpId == Tempid).ToListAsync();

                    for(int i = 0 ; i < employeeModel.qualificationModel.Count ; i++)
                    {
                        for (int j = i ; j < ExistQual.Count;)
                        {
                            ExistQual[j].MemberId = claimDTO.MID;
                            ExistQual[j].EmpId = ExistEmp.EmployeeId;
                            ExistQual[j].QualificationName = employeeModel.qualificationModel[i].QualificationName;
                            ExistQual[j].InstituteName = employeeModel.qualificationModel[i].InstituteName;
                            ExistQual[j].Score = employeeModel.qualificationModel[i].Score;
                            ExistQual[j].City = employeeModel.qualificationModel[i].City;
                            ExistQual[j].State = employeeModel.qualificationModel[i].State;
                            ExistQual[j].QualificationStartYear = employeeModel.qualificationModel[i].QualificationStartYear;
                            ExistQual[j].QualificationEndYear = employeeModel.qualificationModel[i].QualificationEndYear;
                            ExistQual[j].CreatedOn = DateTime.Now;
                            ExistQual[j].UpdatedOn = DateTime.Now;
                            ExistQual[j].Dstatus = "V";
                            //_company.Qualifications.Update(data);
                            await _company.SaveChangesAsync();
                            break;
                        }
                    }
                    await _company.SaveChangesAsync();

                    var ExistPData = await _company.EmployeePersonalDetails.Where(x => x.Mid == claimDTO.MID && x.EmpId == Tempid).FirstOrDefaultAsync();

                    ExistPData.Mid = claimDTO.MID;
                    ExistPData.EmpId = ExistEmp.EmployeeId;  
                    ExistPData.CurrentHouseNo = employeeModel.employeePersonalDetails.CurrentHouseNo;
                    ExistPData.CurrentAddressLine = employeeModel.employeePersonalDetails.CurrentAddressLine;
                    ExistPData.CurrentLocality = employeeModel.employeePersonalDetails.CurrentLocality;
                    ExistPData.CurrentPinCode = employeeModel.employeePersonalDetails.CurrentPinCode;
                    ExistPData.CurrentCity = employeeModel.employeePersonalDetails.CurrentCity;
                    ExistPData.CurrentState = employeeModel.employeePersonalDetails.CurrentState;
                    ExistPData.PermanentHouseNo = employeeModel.employeePersonalDetails.PermannentHouseNo;
                    ExistPData.PermanentAddressLine = employeeModel.employeePersonalDetails.PermannentAddressLine;
                    ExistPData.PermanentLocality = employeeModel.employeePersonalDetails.PermannentLocality;
                    ExistPData.PermanentPinCode = employeeModel.employeePersonalDetails.PermannentPincode;
                    ExistPData.PermanentCity = employeeModel.employeePersonalDetails.PermannentCity;
                    ExistPData.PermanentState = employeeModel.employeePersonalDetails.PermannentState;
                    ExistPData.AlterNateEmail = employeeModel.employeePersonalDetails.AlternateEmail;
                    ExistPData.AlterNatePhoneNo = employeeModel.employeePersonalDetails.AlternatePhoneNo;

                    await _company.SaveChangesAsync();


                    genericResult.Status = "Success";
                    genericResult.Message = "Employee Updated Successfully";
                }
                else
                {
                    userData = new UserTable()
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
                        DepartmentId = employeeModel.EmployeeTableModel.DepartmentId,
                        CreatedOn = DateTime.Now,
                        UpdatedOn = DateTime.Now,
                        Dstatus = "V",
                        EmployeeId = userData.UserId
                    };

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
                    genericResult.Status = "Success";
                    genericResult.Message = "Employee Added Successfully";
                }
                await _company.SaveChangesAsync();
            }
            catch (Exception err)
            {
                genericResult.Status = "Server Error";
                genericResult.Message = "Internal Server Error";
                Transaction.Current.Rollback();
            }
            return genericResult;
        }
    }
}
