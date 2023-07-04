using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JLG
{
    public class ClsPDDData
    {

    }

    public class ClsUploadFile
    {
        public int FileID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string UploadedOn { get; set; }
        public int UploadedBy { get; set; }

    }

    public class ClsUploadPDDData
    {
        public int BranchId { get; set; }
        public string LAN { get; set; }
        public string Barcode { get; set; }
        public string State { get; set; }
        public string BC_Name { get; set; }
        public string Customer_Name { get; set; }
        public string Exaternalcustno { get; set; }
        public string BC_Branch { get; set; }
        public string Center_Name { get; set; }

        public string GroupID { get; set; }
        public string Group_Name { get; set; }
        public string Disbursed_Amount { get; set; }
        public string Disbursement_Mode { get; set; }
        public string Disbursement_Date { get; set; }
        public string Inward_Date { get; set; }
        public int UploadFileID { get; set; }
    }

    public class ClsUploadArchivalData
    {

        public string Barcode { get; set; }
        public string ArchivalDate { get; set; }
        public string LotNo { get; set; }
        public string CartonNo { get; set; }
        public string FileNo { get; set; }
        public int UploadFileID { get; set; }

    }
    public class ClsBranchData
    {

        public string BranchName { get; set; }
        public int LocId { get; set; }
        public string LocName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Pincode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string EmailId { get; set; }
        public string PhoneNo { get; set; }
        public string IsActive { get; set; }

    }

    public class ClsBCMailList
    {
        public int ID { get; set; }
        public string PDD_BC_Name { get; set; }
        public string PDD_BC_Code { get; set; }
        public string PDD_BC_To_Email { get; set; }
        public string PDD_BC_CC_Email { get; set; }
        public string PDD_BC_BCC_Email { get; set; }
        public string Report_Type { get; set; }
        public string IsActive { get; set; }
    }
}