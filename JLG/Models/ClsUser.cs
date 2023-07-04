using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JLG
{
    public class ClsUser
    {
        public int UserId { get; set; }
        public string User_Type_Code { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DeptId { get; set; }
        public int BranchId { get; set; }
        public string Login_Password { get; set; }
        public int Password_Expiry_Days { get; set; }
        public string Password_Expiry_Date { get; set; }
        public int Attempt_Count { get; set; }
        public string Last_LogIn_DateTime { get; set; }
        public string IsActive { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedOn { get; set; }
        public string Login_IPAddress { get; set; }
        public int GroupId { get; set; }
        public string BranchList { get; set; }
        public string UserType { get; set; }
        public string reset { get; set; }
        public int CompanyId { get; set; }
        public int LocationId { get; set; }
        public string LocationList { get; set; }
        public string dashboard { get; set; }
        public string Branch { get; set; }
        public string Reason { get; set; }
        public string EmailID { get; set; }
        public string LoginText { get; set; }
        public string MobileNo { get; set; }


        public DateTime inActivationDate { get; set; } // Changes done by Kapil Singhal on 9th Dec. 2022

        public int inActivationDone_By { get; set; } // Changes done by Kapil Singhal on 9th Dec. 2022

    }
}