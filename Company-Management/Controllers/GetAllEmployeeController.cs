using Company_Management.Helper;
using Company_Management.Helper.HelperModel;
using Company_Management.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetAllEmployeeController : ControllerBase
    {
        private readonly IGetAllEmployeeServices _employee;

        public GetAllEmployeeController(IGetAllEmployeeServices employee)
        {
            _employee = employee;
        }

        [HttpGet("GetAllEmployee/{mid}")]
        //[Authorize]
        public async Task<IActionResult> GetAllEmployee([FromQuery] string type , [FromRoute] string mid)
        {
            //var claim = Help.GetClaims(Request);
            //var MID = Help.GetClaims(Request);
            //var UID = Help.GetClaims(Request).UserId;
            //var dataCount =await _employee.GetAllEmployee(type,MID);
            var dataCount =await _employee.GetAllEmployee(type,mid);


            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage package = new ExcelPackage();
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Test");

            worksheet.Cells[1,1].Value = "Id";
            worksheet.Cells[1,2].Value = "Name";
            worksheet.Cells[1,3].Value = "Email";
            worksheet.Cells[1,4].Value = "PhoneNo";
            worksheet.Cells[1,5].Value = "Role";
            worksheet.Cells[1,6].Value = "Status";


            int j = 2;
            for (int i = 0; i < dataCount.Count ; i++)
            {
                worksheet.Cells[j,1].Value = dataCount[i].Id;
                worksheet.Cells[j,2].Value = dataCount[i].Name;
                worksheet.Cells[j,3].Value = dataCount[i].Email;
                worksheet.Cells[j,4].Value = dataCount[i].PhoneNo;
                worksheet.Cells[j,5].Value = dataCount[i].Role;
                worksheet.Cells[j,6].Value = dataCount[i].Status;
                j++;
            }
            using (var range = worksheet.Cells[1, 1, 1, 6])  //Address "A1:A5"
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
                return File(content, "Application/octet-stream", "EmployeeDetails.xlsx");
            }
            //return Ok(data);
        }
    }
}
