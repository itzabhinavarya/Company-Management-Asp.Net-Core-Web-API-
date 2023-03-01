using Company_Management.Helper;
using Company_Management.Models;
using Company_Management.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Company_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddEmployeeByExcelController : ControllerBase
    {
        private readonly IAddEmployeeByExcel _addEmployeeByExcel;

        public AddEmployeeByExcelController(IAddEmployeeByExcel addEmployeeByExcel)
        {
            _addEmployeeByExcel = addEmployeeByExcel;
        }

        [HttpGet("GenerateExcel")]
        public IActionResult GenerateExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage package = new ExcelPackage();
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Test");

            worksheet.Cells[1, 1].Value = "Email";
            worksheet.Cells[1, 2].Value = "Password";
            worksheet.Cells[1, 3].Value = "PhoneNo";
            worksheet.Cells[1, 4].Value = "Role";
            worksheet.Cells[1, 5].Value = "Status";
            worksheet.Cells[1, 6].Value = "EmployeeFullName";
            worksheet.Cells[1, 7].Value = "DOJ";
            worksheet.Cells[1, 8].Value = "EmployementType";
            worksheet.Cells[1, 9].Value = "WorkingDays";
            worksheet.Cells[1, 10].Value = "DepartmentId";
            worksheet.Cells[1, 11].Value = "QualificationName";
            worksheet.Cells[1, 12].Value = "QualificationStartYear";
            worksheet.Cells[1, 13].Value = "QualificationEndYear";
            worksheet.Cells[1, 14].Value = "InstituteName";
            worksheet.Cells[1, 15].Value = "Score";
            worksheet.Cells[1, 16].Value = "State";
            worksheet.Cells[1, 17].Value = "City";
            worksheet.Cells[1, 18].Value = "PermannentHouseNo";
            worksheet.Cells[1, 19].Value = "PermannentAddressLine";
            worksheet.Cells[1, 20].Value = "PermannentLocality";
            worksheet.Cells[1, 21].Value = "PermannentPincode";
            worksheet.Cells[1, 22].Value = "PermannentCity";
            worksheet.Cells[1, 23].Value = "PermannentState";
            worksheet.Cells[1, 24].Value = "CurrentHouseNo";
            worksheet.Cells[1, 25].Value = "CurrentAddressLine";
            worksheet.Cells[1, 26].Value = "CurrentLocality";
            worksheet.Cells[1, 27].Value = "CurrentPinCode";
            worksheet.Cells[1, 28].Value = "CurrentCity";
            worksheet.Cells[1, 29].Value = "CurrentState";
            worksheet.Cells[1, 30].Value = "AlternatePhoneNo";
            worksheet.Cells[1, 31].Value = "AlternateEmail";


            using (var range = worksheet.Cells[1, 1, 1, 31])  //Address "A1:F1"
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                range.Style.Font.Color.SetColor(Color.White);
            }

            using (var memory = new MemoryStream())
            {
                package.SaveAs(memory);
                var content = memory.ToArray();
                string ContentType = "Application/octet-stream";
                string FileName = "EmployeeDetails.xlsx";
                return File(content, ContentType, FileName);
            }
        }


        [HttpPost("UploadExcel/{mid}")]
        public async Task<IActionResult> UploadExcel(IFormFile file , CancellationToken cancellationToken , string mid)
        {
            //var mid = Help.GetClaims(Request);
            //var data = _addEmployeeByExcel.AddEmployee(employeeExcelModel, mid);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            //ExcelPackage package = new ExcelPackage();

            List<AddEmployeeExcelModel> excelModel = new List<AddEmployeeExcelModel>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];


                    UserModelxls user = new UserModelxls()
                    {
                        Email = worksheet.Cells[2, 1].Value.ToString().Trim(),
                        Password = worksheet.Cells[2, 2].Value.ToString().Trim(),
                        PhoneNo = worksheet.Cells[2, 3].Value.ToString().Trim(),
                        Role = worksheet.Cells[2, 4].Value.ToString().Trim(),
                        Status = worksheet.Cells[2, 5].Value.ToString().Trim(),
                    };

                    EmployeeTableModelxls Employee = new EmployeeTableModelxls()
                    {
                        EmployeeFullName = worksheet.Cells[2, 6].Value.ToString().Trim(),
                        DOJ = (DateTime)worksheet.Cells[2, 7].Value,
                        EmployementType = worksheet.Cells[2, 8].Value.ToString().Trim(),
                        WorkingDays = int.Parse(worksheet.Cells[2, 9].Value.ToString().Trim()),
                        DepartmentId = int.Parse(worksheet.Cells[2, 10].Value.ToString().Trim()),
                    };
                    QualificationModelxls Qualification = new QualificationModelxls()
                    {
                        QualificationName = worksheet.Cells[2, 11].Value.ToString().Trim(),
                        QualificationStartYear = worksheet.Cells[2, 12].Value.ToString().Trim(),
                        QualificationEndYear = worksheet.Cells[2, 13].Value.ToString().Trim(),
                        InstituteName = worksheet.Cells[2, 14].Value.ToString().Trim(),
                        Score = worksheet.Cells[2, 15].Value.ToString().Trim(),
                        State = worksheet.Cells[2,16].Value.ToString().Trim(),
                        City = worksheet.Cells[2, 17].Value.ToString().Trim(),
                    };
                    EmployeePersonalDetailsModelxls EmpPersonalDetails = new EmployeePersonalDetailsModelxls()
                    {
                        PermannentHouseNo = worksheet.Cells[2, 18].Value.ToString().Trim(),
                        PermannentAddressLine = worksheet.Cells[2, 19].Value.ToString().Trim(),
                        PermannentLocality = worksheet.Cells[2, 20].Value.ToString().Trim(),
                        PermannentPincode = worksheet.Cells[2, 21].Value.ToString().Trim(),
                        PermannentCity = worksheet.Cells[2, 22].Value.ToString().Trim(),
                        PermannentState = worksheet.Cells[2, 23].Value.ToString().Trim(),
                        CurrentHouseNo = worksheet.Cells[2, 24].Value.ToString().Trim(),
                        CurrentAddressLine = worksheet.Cells[2, 25].Value.ToString().Trim(),
                        CurrentLocality = worksheet.Cells[2, 26].Value.ToString().Trim(),
                        CurrentPinCode = worksheet.Cells[2, 27].Value.ToString().Trim(),
                        CurrentCity = worksheet.Cells[2, 28].Value.ToString().Trim(),
                        CurrentState = worksheet.Cells[2, 29].Value.ToString().Trim(),
                        AlternatePhoneNo = worksheet.Cells[2, 30].Value.ToString().Trim(),
                        AlternateEmail = worksheet.Cells[2, 31].Value.ToString().Trim(),
                    };

                    AddEmployeeExcelModel emp = new AddEmployeeExcelModel()
                    {
                        userModel = user,
                        EmployeeTableModel = Employee,
                        qualificationModel = Qualification,
                        employeePersonalDetails = EmpPersonalDetails
                    };
                    //var mid = Help.GetClaims(Request);
                    var data = _addEmployeeByExcel.AddEmployee(emp,mid);

                }


                return Ok("Inserted Successfully...");





                //ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Add-Employee");







                //worksheet.Cells[1, 1].Value = "Email";
                //worksheet.Cells[1, 2].Value = "Password";
                //worksheet.Cells[1, 3].Value = "PhoneNo";
                //worksheet.Cells[1, 4].Value = "Role";
                //worksheet.Cells[1, 5].Value = "Status";
                //worksheet.Cells[1, 6].Value = "EmployeeFullName";
                //worksheet.Cells[1, 7].Value = "DOJ";
                //worksheet.Cells[1, 8].Value = "EmployementType";
                //worksheet.Cells[1, 9].Value = "WorkingDays";
                //worksheet.Cells[1, 10].Value = "DepartmentId";
                //worksheet.Cells[1, 11].Value = "QualificationName";
                //worksheet.Cells[1, 12].Value = "QualificationStartYear";
                //worksheet.Cells[1, 13].Value = "QualificationEndYear";
                //worksheet.Cells[1, 14].Value = "InstituteName";
                //worksheet.Cells[1, 15].Value = "Score";
                //worksheet.Cells[1, 16].Value = "State";
                //worksheet.Cells[1, 17].Value = "City";
                //worksheet.Cells[1, 18].Value = "PermannentHouseNo";
                //worksheet.Cells[1, 19].Value = "PermannentAddressLine";
                //worksheet.Cells[1, 20].Value = "PermannentLocality";
                //worksheet.Cells[1, 21].Value = "PermannentPincode";
                //worksheet.Cells[1, 22].Value = "PermannentCity";
                //worksheet.Cells[1, 23].Value = "PermannentState";
                //worksheet.Cells[1, 24].Value = "CurrentHouseNo";
                //worksheet.Cells[1, 25].Value = "CurrentAddressLine";
                //worksheet.Cells[1, 26].Value = "CurrentLocality";
                //worksheet.Cells[1, 27].Value = "CurrentPinCode";
                //worksheet.Cells[1, 28].Value = "CurrentCity";
                //worksheet.Cells[1, 29].Value = "CurrentState";
                //worksheet.Cells[1, 30].Value = "AlternatePhoneNo";
                //worksheet.Cells[1, 31].Value = "AlternateEmail";



                //worksheet.Cells[2, 1].Value = employeeExcelModel.userModel.Email;
                //worksheet.Cells[2, 2].Value = employeeExcelModel.userModel.Password;
                //worksheet.Cells[2, 3].Value = employeeExcelModel.userModel.PhoneNo;
                //worksheet.Cells[2, 4].Value = employeeExcelModel.userModel.Role;
                //worksheet.Cells[2, 5].Value = employeeExcelModel.userModel.Status;
                //worksheet.Cells[2, 6].Value = employeeExcelModel.EmployeeTableModel.EmployeeFullName;
                //worksheet.Cells[2, 7].Value = employeeExcelModel.EmployeeTableModel.DOJ;
                //worksheet.Cells[2, 8].Value = employeeExcelModel.EmployeeTableModel.EmployementType;
                //worksheet.Cells[2, 9].Value = employeeExcelModel.EmployeeTableModel.WorkingDays;
                //worksheet.Cells[2, 10].Value = employeeExcelModel.EmployeeTableModel.DepartmentId;
                //worksheet.Cells[2, 11].Value = employeeExcelModel.qualificationModel.QualificationName;
                //worksheet.Cells[2, 12].Value = employeeExcelModel.qualificationModel.QualificationStartYear;
                //worksheet.Cells[2, 13].Value = employeeExcelModel.qualificationModel.QualificationEndYear;
                //worksheet.Cells[2, 14].Value = employeeExcelModel.qualificationModel.InstituteName;
                //worksheet.Cells[2, 15].Value = employeeExcelModel.qualificationModel.Score;
                //worksheet.Cells[2, 16].Value = employeeExcelModel.qualificationModel.State;
                //worksheet.Cells[2, 17].Value = employeeExcelModel.qualificationModel.City;
                //worksheet.Cells[2, 18].Value = employeeExcelModel.employeePersonalDetails.PermannentHouseNo;
                //worksheet.Cells[2, 19].Value = employeeExcelModel.employeePersonalDetails.PermannentAddressLine;
                //worksheet.Cells[2, 20].Value = employeeExcelModel.employeePersonalDetails.PermannentLocality;
                //worksheet.Cells[2, 21].Value = employeeExcelModel.employeePersonalDetails.PermannentPincode;
                //worksheet.Cells[2, 22].Value = employeeExcelModel.employeePersonalDetails.PermannentCity;
                //worksheet.Cells[2, 23].Value = employeeExcelModel.employeePersonalDetails.PermannentState;
                //worksheet.Cells[2, 24].Value = employeeExcelModel.employeePersonalDetails.CurrentHouseNo;
                //worksheet.Cells[2, 25].Value = employeeExcelModel.employeePersonalDetails.CurrentAddressLine;
                //worksheet.Cells[2, 26].Value = employeeExcelModel.employeePersonalDetails.CurrentLocality;
                //worksheet.Cells[2, 27].Value = employeeExcelModel.employeePersonalDetails.CurrentPinCode;
                //worksheet.Cells[2, 28].Value = employeeExcelModel.employeePersonalDetails.CurrentCity;
                //worksheet.Cells[2, 29].Value = employeeExcelModel.employeePersonalDetails.CurrentState;
                //worksheet.Cells[2, 30].Value = employeeExcelModel.employeePersonalDetails.AlternatePhoneNo;
                //worksheet.Cells[2, 31].Value = employeeExcelModel.employeePersonalDetails.AlternateEmail;



            }


        }

    }
}
