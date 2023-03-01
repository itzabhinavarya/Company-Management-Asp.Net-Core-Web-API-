using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Management.Models
{
    public class AddEmployeeExcelModel
    {
        public EmployeePersonalDetailsModelxls employeePersonalDetails { get; set; }
        public UserModelxls userModel { get; set; }
        public QualificationModelxls qualificationModel { get; set; }
        public EmployeeTableModelxls EmployeeTableModel { get; set; }
    }
        //------------------------User Personal Details----------------------------
        public class EmployeePersonalDetailsModelxls
        {
            public string PermannentHouseNo { get; set; }
            public string PermannentAddressLine { get; set; }
            public string PermannentLocality { get; set; }
            public string PermannentPincode { get; set; }
            public string PermannentCity { get; set; }
            public string PermannentState { get; set; }
            public string CurrentHouseNo { get; set; }
            public string CurrentAddressLine { get; set; }
            public string CurrentLocality { get; set; }
            public string CurrentPinCode { get; set; }
            public string CurrentCity { get; set; }
            public string CurrentState { get; set; }
            public string AlternatePhoneNo { get; set; }
            public string AlternateEmail { get; set; }
        }
        //------------------------------User Table------------------------------------------
        public class UserModelxls
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string PhoneNo { get; set; }
            public string Role { get; set; }
            public string Status { get; set; }
        }
        //------------------------------Qualification------------------------------------------
        public class QualificationModelxls
        {
            public string QualificationName { get; set; }
            public string QualificationStartYear { get; set; }
            public string QualificationEndYear { get; set; }
            public string InstituteName { get; set; }
            public string Score { get; set; }
            public string State { get; set; }
            public string City { get; set; }
        }
        //-------------------------------------Employee Details-------------------------------------
        public class EmployeeTableModelxls
        {
            public string EmployeeFullName { get; set; }
            public DateTime DOJ { get; set; }
            public string EmployementType { get; set; }
            public int WorkingDays { get; set; }
            public int DepartmentId { get; set; }
        }
}
