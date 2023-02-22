using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Management.DTO
{
    public class AllEmployeeDTO
    {
        public GetEmployeePersonalDetailsModel employeePersonalDetails { get; set; }
        public GetUser userDTO { get; set; }
        public List<GetQualificationModel> qualificationModel { get; set; }
        public GetEmployee EmployeeTableModel { get; set; }

    }
    //------------------------User Personal Details----------------------------
    public class GetEmployeePersonalDetailsModel
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
    public class GetUser
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
    }
    //------------------------------Qualification------------------------------------------
    public class GetQualificationModel
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
    public class GetEmployee
    {
        public string EmployeeFullName { get; set; }
        public DateTime DOJ { get; set; }
        public string EmpployementType { get; set; }
        public int WorkingDays { get; set; }
        //public int DepartmentId { get; set; }
    }
}
