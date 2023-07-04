using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Data;
using System.Globalization;

namespace JLG.Forms
{
    public partial class frmPDD_DataUpload : System.Web.UI.Page
    {
        public string strLogFilePath = ConfigurationManager.AppSettings["LogFilePath"].ToString() + "LogFolder\\";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["LoginID"] == null)
                {
                    Response.Redirect("../frmLogin.aspx?msg=Session time out.", false);
                    return;
                }
                if (!IsPostBack)
                {
                    BindBranch();
                    BindGridData();
                    btnExportToExcel.Visible = true;
                    btnExportToExcel.Enabled = true;
                    //BindUploadedFileGrid(Convert.ToInt32(Session["LoginID"]));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }

        }

        //private void BindUploadedFileGrid(int id)
        //{
        //    DataTable dtUploadedFileTbl = new DataTable();
        //    dtUploadedFileTbl = ClsUploadData.GetUploadedFileDetails(id);
        //    if(dtUploadedFileTbl.Rows.Count > 0)
        //    {
        //        gvUpdloadDataDiv.DataSource = dtUploadedFileTbl;
        //        gvUpdloadDataDiv.DataBind();
        //        gvUpdloadDataDiv.Visible = true;
        //    }
        //    else
        //    {
        //        gvUpdloadDataDiv.EmptyDataText = "No File Uploaded";
        //        gvUpdloadDataDiv.ShowHeaderWhenEmpty = true;
        //    }
        //}

        private void BindBranch()
        {
            try
            {
                DataTable dt = CommonData.GetBranch();
                ddlBranch.DataSource = dt;
                ddlBranch.DataValueField = "Branch_Id";
                ddlBranch.DataTextField = "Branch_Name";
                ddlBranch.DataBind();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }
        private void BindGridData()
        {
            try
            {
                DataTable dtData = new DataTable();
                int LoginID = Convert.ToInt32(Session["LoginID"].ToString());

                dtData = ClsUploadData.GetGridData(LoginID);
                if (dtData != null && dtData.Rows.Count > 0)
                {
                    gvData.DataSource = dtData;
                    gvData.DataBind();
                    btnExportToExcel.Visible = true;
                    btnExportToExcel.Enabled = true;
                    hflogFileName.Value = dtData.Rows[0]["ErrorFileName"].ToString();
                    if (hflogFileName.Value.Contains(".txt") == false) hflogFileName.Value = hflogFileName.Value + ".txt";

                    hfLogFilePath.Value = strLogFilePath;
                    if (File.Exists(strLogFilePath + hflogFileName.Value.ToString()))
                    {
                        lnkbtnLogFile.Visible = false;
                        
                    }
                    else
                    {
                        lnkbtnLogFile.Visible = false;
                       
                    }
                    
                }
                else
                {
                    btnExportToExcel.Visible = false;
                    //Page.ClientScript.RegisterClientScriptBlock(GetType(), "msgbox", "alert('Record not found.');", true);
                }
            }
            catch (Exception ex)
            {
                CommonDB.LogError(this.Page, "BindGridData", "Error :" + Convert.ToString(ex.Message));
            }
        }

        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            string UploadShtName = "";
            string LogFileFullPath = "";
            string CurrentFileName = "";
            OleDbConnection connection = null; 
            try
            {
                int setTag = 0;
                Boolean isUpload = true;
                Boolean isValid = true;
                string alldata = "";
                int uCount = 0;
                int rCount = 0;
                char uploadStatus;
                char readStatus;
                string fileType = "";

                string ExcelFormat = ConfigurationManager.AppSettings["ExcelFormat"].ToString();

                int BrID = 0;
                //if (txtInwardDate.Text.Trim() == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Date can not be blank' );", true);
                //    return;
                //}

                if (ddlBranch.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please select the branch' );", true);
                    return;
                }
                else
                {
                    BrID = Convert.ToInt32(ddlBranch.SelectedValue.ToString());
                }

                int LoginID = Convert.ToInt32(Session["LoginID"].ToString());
                string path = ConfigurationManager.AppSettings["UploadLocation"].ToString() + Path.GetFileNameWithoutExtension(FileUpload1.FileName) + DateTime.Now.ToString("ddMMyyyyssmm") + ".xls";
                hdnRpath.Value = path;

                int count = 0;
                
                if (FileUpload1.HasFile)
                {
                    FileUpload1.SaveAs(path);

                    ClsUploadFile objfileupload = new ClsUploadFile();
                    objfileupload.FileName = Path.GetFileName(FileUpload1.FileName);
                    //objfileupload.FilePath = Path.GetFullPath(FileUpload1.FileName);
                    objfileupload.FilePath = path;
                    objfileupload.UploadedBy = LoginID;
                    CurrentFileName = objfileupload.FileName;

                    int FileUploadID = 0;

                    uploadStatus = 'Y';
                    readStatus = 'N';
                    fileType = "PDD";

                    FileUploadID = ClsUploadData.GetFileUpload(objfileupload);
                    ClsUploadData.UpdateFlagForFileUploaded(FileUploadID, uploadStatus, readStatus, fileType, BrID);

                    DataTable UnreadFileId = new DataTable();

                    int id = 0;
                    string fileName = "";

                    UnreadFileId = ClsUploadData.GetCurrentFile(FileUploadID);

                    if (UnreadFileId.Rows.Count > 0)
                    {
                        for (int k = 0; k < UnreadFileId.Rows.Count; k++)
                        {
                            id = Convert.ToInt32(UnreadFileId.Rows[k][0].ToString());
                            fileName = UnreadFileId.Rows[k][2].ToString();

                            // Connection String to Excel Workbook
                            //string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;IMEX=1;';", path);
                            string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0;IMEX=1;';");
                            //string excelConnectionString  =  string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;IMEX=1;';", path);
                            connection = new OleDbConnection();
                            connection.ConnectionString = excelConnectionString;

                            try
                            {
                                connection.Open();
                            }
                            catch
                            {
                                //string con = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + path + ";" + "Extended Properties=Excel 8.0;HDR=Yes;IMEX=1";
                                //connection = new OleDbConnection(con);

                                //excelConnectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=Yes';");
                                //excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=YES';";
                                excelConnectionString = "Provider=Microsoft.Jet.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 15.0;HDR=YES';";
                                //excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=YES';";

                                connection = new OleDbConnection();
                                connection.ConnectionString = excelConnectionString;

                                connection.Open();
                            }

                            DataTable dtSheet = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sht = string.Empty;
                            string strLAN = "";
                            string strBARCODE = "";

                            sht = dtSheet.Rows[0][2].ToString().Replace("'", "");

                            if (sht != "Data$")
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Sheet name should be Data on excel file,Download the sample format.' );", true);
                                return;
                            }

                            //OleDbCommand command = new OleDbCommand("select * from [" + sht + "]", connection);
                            //OleDbCommand command1 = new OleDbCommand("select * from [" + sht + "]", connection);

                            OleDbCommand command = new OleDbCommand("select * from [Data$]", connection);
                            OleDbCommand command1 = new OleDbCommand("select * from [Data$]", connection);

                            UploadShtName = sht + "_" + Convert.ToString(LoginID) + "_" + DateTime.Now.ToString("dd-MMM-yyyy_HHmmss");


                            DataTable dtPDD = new DataTable("PDDLOAN");

                            DbDataReader dr = command.ExecuteReader();
                            DbDataReader dr1 = command1.ExecuteReader();

                            dtPDD.Load(dr1);
                            string[] splitarr = ExcelFormat.Split('|');
                            int l = 0;
                            foreach (DataColumn dtcol in dtPDD.Columns)
                            {
                                if (splitarr[l].ToString().ToUpper() != dtcol.ColumnName.ToString().ToUpper())
                                {
                                    //ScriptManager.RegisterStartupScript(this, GetType(), "TestAlert", "alert('Invalid excel format.');", true);
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('Invalid column name " + dtcol.ColumnName.ToString().ToUpper() + " in excel format.');", true);
                                    return;
                                }
                                l++;
                            }

                            CommonDB.ClassPDDError("ClsUploadData", "GetLAN", UploadShtName, "Barcode", "Error", CurrentFileName);

                            while (dr.Read())
                            {
                                ClsUploadPDDData objData = new ClsUploadPDDData();
                                ClsUploadFile objUploadFile = new ClsUploadFile();
                                string[] strPickupDt;
                                string strTemp = "";

                                alldata = "";

                                objData.BranchId = Convert.ToInt32(ddlBranch.SelectedValue);
                                objData.Inward_Date = txtInwardDate.Text.Trim();

                                if (dr[0] != DBNull.Value && Convert.ToString(dr[0]).Trim() != "")
                                {
                                    objData.LAN = Convert.ToString(dr[0]);
                                    strLAN = objData.LAN;
                                }
                                else
                                {
                                    //strLAN = Convert.ToString(dr[1]);
                                    alldata = " LAN Number can not be blank";
                                }

                                if (dr[1] != DBNull.Value && Convert.ToString(dr[1]).Trim() != "")
                                {
                                    objData.Barcode = Convert.ToString(dr[1]);
                                    strBARCODE = objData.Barcode;
                                }
                                else
                                {
                                    //strLAN = Convert.ToString(dr[1]);
                                    alldata = " Barcode can not be blank";
                                }


                                //objData.Barcode = Convert.ToString(dr[1]);
                                objData.State = Convert.ToString(dr[2]);
                                objData.BC_Name = Convert.ToString(dr[3]);
                                objData.Customer_Name = Convert.ToString(dr[4]);
                                objData.Exaternalcustno = Convert.ToString(dr[5]);
                                objData.BC_Branch = Convert.ToString(dr[6]);
                                objData.Center_Name = Convert.ToString(dr[7]);
                                objData.GroupID = Convert.ToString(dr[8]);
                                objData.Group_Name = Convert.ToString(dr[9]);
                                objData.Disbursed_Amount = Convert.ToString(dr[10]);
                                objData.Disbursement_Mode = Convert.ToString(dr[11]);

                                if (dr[11] != DBNull.Value)
                                {
                                    //objData.Disbursement_Date = Convert.ToString(dr[8]);
                                    strTemp = Check_GetCorrectDate(Convert.ToString(dr[12]));
                                    if (strTemp.Trim().ToUpper() == "ERROR")
                                    {
                                        alldata = alldata + "," + "\t" + " Disbursement Date is not in correct format";
                                    }
                                    else
                                    {
                                        objData.Disbursement_Date = strTemp;
                                    }
                                }

                                objData.UploadFileID = id;

                                string i = "";

                                if (alldata.ToString() != "")
                                {
                                    rCount = rCount + 1;
                                    isValid = false; strTemp = "";
                                    alldata = "Uploaded File Name :-" + CurrentFileName + "\t" + alldata;

                                    strTemp = alldata.Replace(",", Environment.NewLine);
                                    CommonDB.ClassPDDError("ClsUploadData", "GetLAN", UploadShtName, strBARCODE, Convert.ToString(strLAN + strTemp), CurrentFileName);
                                }
                                else
                                {
                                    i = "uploaded successfully.";
                                    setTag++;
                                }

                                if (i.ToLower().Trim() != "uploaded successfully.")
                                {
                                    rCount = rCount + 1;
                                    isUpload = false;
                                    alldata = "LAN ID : '" + strLAN + "' is not uploaded. Error is: " + i;
                                    CommonDB.ClassPDDError("ClsUploadData", "GetLAN", UploadShtName, strBARCODE, Convert.ToString(alldata), CurrentFileName);
                                }
                                else
                                {
                                    uCount = uCount + 1;
                                }
                            }

                            
                            //ClsUploadData.UpdateFileReadCompleteDateTime(id);

                            if (k < (UnreadFileId.Rows.Count - 1))
                            {
                                //readStatus = 'Y';
                                //ClsUploadData.UpdateFlagForFileUploaded(id, 'Y', readStatus, "PDD");
                                k = k++;
                            }
                            else
                            {
                                uploadStatus = 'Y';

                                //ClsUploadData.UpdateFlagForFileUploaded(id, uploadStatus, fileType, BrID);
                                ClsUploadData.UpdateFileUploadTime(id);
                                //BindUploadedFileGrid(LoginID);

                                //ClsUploadData.UpdateFlagForFileUploaded(id, 'Y', uploadStatus, "PDD", BrID);
                            }
                        }
                        
                        
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('"+uCount+"');", true);
                        BindGridData();



                        if (isUpload == true && isValid == true)
                        {
                            lnkbtnLogFile.Text = "";
                            lnkbtnLogFile.Visible = false;

                            ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('PDD Data Uploaded successfully.');", true);

                            
                            //ErrPanel.Controls.Add(CommonDB.MsgHolder("Total Uploaded Record - " + Convert.ToString(uCount) + " Total Rejected Record - " + Convert.ToString(rCount)));
                            //ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('Total Uploaded Record - " + Convert.ToString(uCount) + " Total Rejected Record - " + Convert.ToString(rCount) + "');", true);
                        }
                        else if (isUpload == true && isValid == false)
                        {
                            hflogFileName.Value = UploadShtName + ".txt";
                            hfLogFilePath.Value = strLogFilePath;

                            lnkbtnLogFile.Visible = true;
                            //lnkbtnLogFile.Text = "Download log file for failed Lead ID" ;

                            ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('PDD Data Uploaded. Check log file for missing PDD data');", true);
                            // ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('Total Uploaded Record - " + Convert.ToString(uCount) + "Total Rejected Record - " + Convert.ToString(rCount) + "');", true);
                            //ErrPanel.Controls.Add(CommonDB.MsgHolder("Total Uploaded Record - " + Convert.ToString(uCount) + " Total Rejected Record - " + Convert.ToString(rCount)));
                        }
                        else
                        {
                            hflogFileName.Value = UploadShtName + ".txt";
                            hfLogFilePath.Value = strLogFilePath;

                            lnkbtnLogFile.Visible = true;
                            //lnkbtnLogFile.Text = "Download log file for failed Lead ID";
                            ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('PDD Data Uploaded. Check Log file for missing LAN');", true);
                            // ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('Total Uploaded Record - " + Convert.ToString(uCount) + "Total Rejected Record - " + Convert.ToString(rCount) + "');", true);
                            //ErrPanel.Controls.Add(CommonDB.MsgHolder("Total Uploaded Record - " + Convert.ToString(uCount) + " Total Rejected Record - " + Convert.ToString(rCount)));
                        }

                    }



                    connection.Close();

                    

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please select the file');", true);
                    return;
                }

                


                #region | Commented Portion

                //else if (UnreadFileId.Rows.Count == 1)
                //{
                //    for(int j = 0; j < UnreadFileId.Rows.Count; j++)
                //    {
                //        id = Convert.ToInt32(UnreadFileId.Rows[j][0].ToString());

                //        // Connection String to Excel Workbook
                //        //string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;IMEX=1;';", path);
                //        string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0;IMEX=1;';");
                //        //string excelConnectionString  =  string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;IMEX=1;';", path);
                //        OleDbConnection connection = new OleDbConnection();
                //        connection.ConnectionString = excelConnectionString;

                //        try
                //        {
                //            connection.Open();
                //        }
                //        catch
                //        {
                //            //string con = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + path + ";" + "Extended Properties=Excel 8.0;HDR=Yes;IMEX=1";
                //            //connection = new OleDbConnection(con);

                //            //excelConnectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=Yes';");
                //            //excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=YES';";
                //            excelConnectionString = "Provider=Microsoft.Jet.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 15.0;HDR=YES';";
                //            //excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=YES';";

                //            connection = new OleDbConnection();
                //            connection.ConnectionString = excelConnectionString;

                //            connection.Open();
                //        }

                //        DataTable dtSheet = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                //        string sht = string.Empty;
                //        string strLAN = "";
                //        string strBARCODE = "";

                //        sht = dtSheet.Rows[0][2].ToString().Replace("'", "");

                //        if (sht != "Data$")
                //        {
                //            ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Sheet name should be Data on excel file,Download the sample format.' );", true);
                //            return;
                //        }

                //        //OleDbCommand command = new OleDbCommand("select * from [" + sht + "]", connection);
                //        //OleDbCommand command1 = new OleDbCommand("select * from [" + sht + "]", connection);

                //        OleDbCommand command = new OleDbCommand("select * from [Data$]", connection);
                //        OleDbCommand command1 = new OleDbCommand("select * from [Data$]", connection);

                //        UploadShtName = sht + "_" + Convert.ToString(LoginID) + "_" + DateTime.Now.ToString("dd-MMM-yyyy_HHmmss");


                //        DataTable dtPDD = new DataTable("PDDLOAN");

                //        DbDataReader dr = command.ExecuteReader();
                //        DbDataReader dr1 = command1.ExecuteReader();

                //        dtPDD.Load(dr1);
                //        string[] splitarr = ExcelFormat.Split('|');
                //        int l = 0;
                //        foreach (DataColumn dtcol in dtPDD.Columns)
                //        {
                //            if (splitarr[l].ToString().ToUpper() != dtcol.ColumnName.ToString().ToUpper())
                //            {
                //                //ScriptManager.RegisterStartupScript(this, GetType(), "TestAlert", "alert('Invalid excel format.');", true);
                //                ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('Invalid column name " + dtcol.ColumnName.ToString().ToUpper() + " in excel format.');", true);
                //                return;
                //            }
                //            l++;
                //        }

                //        CommonDB.ClassPDDError("ClsUploadData", "GetLAN", UploadShtName, "Barcode", "Error", CurrentFileName);

                //        while (dr.Read())
                //        {
                //            ClsUploadPDDData objData = new ClsUploadPDDData();
                //            ClsUploadFile objUploadFile = new ClsUploadFile();
                //            string[] strPickupDt;
                //            string strTemp = "";

                //            alldata = "";

                //            objData.BranchId = Convert.ToInt32(ddlBranch.SelectedValue);
                //            objData.Inward_Date = txtInwardDate.Text.Trim();

                //            if (dr[0] != DBNull.Value && Convert.ToString(dr[0]).Trim() != "")
                //            {
                //                objData.LAN = Convert.ToString(dr[0]);
                //                strLAN = objData.LAN;
                //            }
                //            else
                //            {
                //                //strLAN = Convert.ToString(dr[1]);
                //                alldata = " LAN Number can not be blank";
                //            }

                //            if (dr[1] != DBNull.Value && Convert.ToString(dr[1]).Trim() != "")
                //            {
                //                objData.Barcode = Convert.ToString(dr[1]);
                //                strBARCODE = objData.Barcode;
                //            }
                //            else
                //            {
                //                //strLAN = Convert.ToString(dr[1]);
                //                alldata = " Barcode can not be blank";
                //            }


                //            //objData.Barcode = Convert.ToString(dr[1]);
                //            objData.State = Convert.ToString(dr[2]);
                //            objData.BC_Name = Convert.ToString(dr[3]);
                //            objData.Customer_Name = Convert.ToString(dr[4]);
                //            objData.Exaternalcustno = Convert.ToString(dr[5]);
                //            objData.BC_Branch = Convert.ToString(dr[6]);
                //            objData.Center_Name = Convert.ToString(dr[7]);
                //            objData.Group_Name = Convert.ToString(dr[8]);
                //            objData.Disbursed_Amount = Convert.ToString(dr[9]);
                //            objData.Disbursement_Mode = Convert.ToString(dr[10]);

                //            if (dr[11] != DBNull.Value)
                //            {
                //                //objData.Disbursement_Date = Convert.ToString(dr[8]);
                //                strTemp = Check_GetCorrectDate(Convert.ToString(dr[11]));
                //                if (strTemp.Trim().ToUpper() == "ERROR")
                //                {
                //                    alldata = alldata + "," + "\t" + " Disbursement Date is not in correct format";
                //                }
                //                else
                //                {
                //                    objData.Disbursement_Date = strTemp;
                //                }
                //            }

                //            objData.UploadFileID = id;

                //            if (alldata.ToString() != "")
                //            {
                //                rCount = rCount + 1;
                //                isValid = false; strTemp = "";
                //                alldata = "Uploaded File Name :-" + CurrentFileName + "\t" + alldata;

                //                strTemp = alldata.Replace(",", Environment.NewLine);
                //                CommonDB.ClassPDDError("ClsUploadData", "GetLAN", UploadShtName, strBARCODE, Convert.ToString(strLAN + strTemp), CurrentFileName);
                //            }
                //            else
                //            {
                //                string i = "";

                //                i = ClsUploadData.UploadPDDData(objData, LoginID, UploadShtName + ".txt");
                //                if (i.ToLower().Trim() != "uploaded successfully.")
                //                {
                //                    rCount = rCount + 1;
                //                    isUpload = false;
                //                    alldata = "LAN ID : '" + strLAN + "' is not uploaded. Error is: " + i;
                //                    CommonDB.ClassPDDError("ClsUploadData", "GetLAN", UploadShtName, strBARCODE, Convert.ToString(alldata), CurrentFileName);
                //                }
                //                else
                //                {
                //                    uCount = uCount + 1;
                //                }
                //                setTag++;
                //            }





                //            BindGridData();

                //            if (isUpload == true && isValid == true)
                //            {
                //                lnkbtnLogFile.Text = "";
                //                lnkbtnLogFile.Visible = false;

                //                ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('PDD Data Uploaded successfully.');", true);
                //                ErrPanel.Controls.Add(CommonDB.MsgHolder("Total Uploaded Record - " + Convert.ToString(uCount) + " Total Rejected Record - " + Convert.ToString(rCount)));
                //                //ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('Total Uploaded Record - " + Convert.ToString(uCount) + " Total Rejected Record - " + Convert.ToString(rCount) + "');", true);
                //            }
                //            else if (isUpload == true && isValid == false)
                //            {
                //                hflogFileName.Value = UploadShtName + ".txt";
                //                hfLogFilePath.Value = strLogFilePath;

                //                lnkbtnLogFile.Visible = true;
                //                //lnkbtnLogFile.Text = "Download log file for failed Lead ID" ;

                //                ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('PDD Data Uploaded. Check log file for missing PDD data');", true);
                //                // ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('Total Uploaded Record - " + Convert.ToString(uCount) + "Total Rejected Record - " + Convert.ToString(rCount) + "');", true);
                //                ErrPanel.Controls.Add(CommonDB.MsgHolder("Total Uploaded Record - " + Convert.ToString(uCount) + " Total Rejected Record - " + Convert.ToString(rCount)));
                //            }
                //            else
                //            {
                //                hflogFileName.Value = UploadShtName + ".txt";
                //                hfLogFilePath.Value = strLogFilePath;

                //                lnkbtnLogFile.Visible = true;
                //                //lnkbtnLogFile.Text = "Download log file for failed Lead ID";
                //                ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('PDD Data Uploaded. Check Log file for missing LAN');", true);
                //                // ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('Total Uploaded Record - " + Convert.ToString(uCount) + "Total Rejected Record - " + Convert.ToString(rCount) + "');", true);
                //                ErrPanel.Controls.Add(CommonDB.MsgHolder("Total Uploaded Record - " + Convert.ToString(uCount) + " Total Rejected Record - " + Convert.ToString(rCount)));
                //            }

                //            //}
                //        }                       

                //    }
                //}


                //}
                #endregion
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
                CommonDB.LogError(this.Page, "BindGridData", "Error :" + Convert.ToString(ex.Message));
            }
        }

        // Check Date format......
        private string Check_GetCorrectDate(string sdate)
        {
            string strDate = "";
            try
            {
                if (sdate != "")
                {
                    string[] arrdate;

                    if (sdate.Contains("-"))
                    {
                        arrdate = sdate.Split('-');

                        if (arrdate[1].Length != 3)
                        {
                            if (arrdate.Length == 3)
                            {
                                int month = Convert.ToInt32(arrdate[1].ToString());
                                if (month <= 12)
                                {
                                    strDate = arrdate[0] + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(arrdate[1])).Substring(0, 3) + "-" + arrdate[2].Replace("12:00:00 AM", "").Replace("12:00:00 PM", "").Replace("00:00:00", "").Trim();
                                }
                                else
                                {
                                    ErrPanel.Controls.Add(CommonDB.ErrorMsgHolder("Month is invalid " + sdate.Trim()));
                                    strDate = "ERROR";
                                }
                            }
                        }
                        else
                        {
                            strDate = sdate.Trim().Replace("12:00:00 AM", "").Replace("12:00:00 PM", "").Replace("00:00:00", "").Trim();
                        }

                    }
                    else if (sdate.Contains("/"))
                    {
                        arrdate = sdate.Split('/');

                        if (arrdate[1].Length != 3)
                        {
                            if (arrdate.Length == 3)
                            {
                                int month = Convert.ToInt32(arrdate[1].ToString());
                                //ErrPanel.Controls.Add(CommonDB.ErrorMsgHolder(month.ToString()));
                                if (month <= 12)
                                {
                                    strDate = arrdate[0] + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(arrdate[1])).Substring(0, 3) + "-" + arrdate[2].Replace("12:00:00 AM", "").Replace("12:00:00 PM", "").Replace("00:00:00", "").Trim();
                                }
                                else
                                {
                                    ErrPanel.Controls.Add(CommonDB.ErrorMsgHolder("Month is invalid " + sdate.Trim()));
                                    strDate = "ERROR";
                                }
                            }
                        }
                        else
                        {
                            strDate = sdate.Replace("/", "-").Replace("12:00:00 AM", "").Replace("12:00:00 PM", "").Replace("00:00:00", "").Trim();
                        }
                    }
                    else
                    {
                        ErrPanel.Controls.Add(CommonDB.ErrorMsgHolder("Date format is invalid (Date Format : dd-mm-yyyy OR dd/mm/yyyy"));
                        strDate = "ERROR";
                    }

                }
                else
                {
                    strDate = sdate.Trim();
                }
            }
            catch (Exception ex)
            {
                ErrPanel.Controls.Add(CommonDB.ErrorMsgHolder(ex.Message));
            }

            return strDate;
        }

        protected void lnkbtnLogFile_Click(object sender, EventArgs e)
        {
            if (hflogFileName.Value != null && hflogFileName.Value.Trim() != "")
            {
                DownloadFile(hflogFileName.Value.ToString(), hfLogFilePath.Value.ToString());
            }
        }

        private Boolean DownloadFile(string FileName, string FilePath)
        {
            try
            {
                string filePullPath = FilePath + FileName;

                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + FileName);
                Response.TransmitFile(filePullPath);
                Response.End();
                return true;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
                CommonDB.LogError(this.Page, "DownloadFile", "Error :" + Convert.ToString(ex.Message));
                return false;
            }
        }

        protected void btnReuploadFile_Click(object sender, EventArgs e)
        {
            try
            {
                int setTag = 0;
                Boolean isUpload = true;
                Boolean isValid = true;
                string alldata = "";

                int LoginID = Convert.ToInt32(Session["LoginID"].ToString());
                string path = hdnRpath.Value;

                string UploadShtName = "";
                string LogFileFullPath = "";
                string CurrentFileName = "";


                ClsUploadFile objfileupload = new ClsUploadFile();
                objfileupload.FileName = Path.GetFileName(hdnRfile.Value);
                //objfileupload.FilePath = Path.GetFullPath(FileUpload1.FileName);
                objfileupload.FilePath = path;
                objfileupload.UploadedBy = LoginID;
                CurrentFileName = objfileupload.FileName;


                int FileUploadID = 0;

                FileUploadID = ClsUploadData.GetFileUpload(objfileupload);

                // Connection String to Excel Workbook
                //string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;IMEX=1;';", path);
                string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0;IMEX=1;';");
                //string excelConnectionString  =  string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;IMEX=1;';", path);
                OleDbConnection connection = new OleDbConnection();
                connection.ConnectionString = excelConnectionString;

                try
                {
                    connection.Open();
                }
                catch
                {
                    //string con = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + path + ";" + "Extended Properties=Excel 8.0;HDR=Yes;IMEX=1";
                    //connection = new OleDbConnection(con);

                    //excelConnectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=Yes';");
                    //excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=YES';";
                    excelConnectionString = "Provider=Microsoft.Jet.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 15.0;HDR=YES';";
                    //excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=YES';";

                    connection = new OleDbConnection();
                    connection.ConnectionString = excelConnectionString;

                    connection.Open();
                }

                DataTable dtSheet = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sht = string.Empty;
                string strLAN = "";

                sht = dtSheet.Rows[0][2].ToString().Replace("'", "");
                OleDbCommand command = new OleDbCommand("select * from [" + sht + "]", connection);
                UploadShtName = sht + "_" + DateTime.Now.ToString("dd-MMM-yyyy_HHmmss");

                DbDataReader dr = command.ExecuteReader();



                while (dr.Read())
                {
                    ClsUploadPDDData objData = new ClsUploadPDDData();
                    ClsUploadFile objUploadFile = new ClsUploadFile();
                    string[] strPickupDt;
                    string strTemp = "";

                    alldata = "";

                    objData.BranchId = Convert.ToInt32(ddlBranch.SelectedValue);
                    objData.Inward_Date = txtInwardDate.Text.Trim();

                    if (dr[0] != DBNull.Value && Convert.ToString(dr[1]).Trim() != "")
                    {
                        objData.LAN = Convert.ToString(dr[0]);
                        strLAN = objData.LAN;
                    }
                    else
                    {
                        //strLAN = Convert.ToString(dr[1]);
                        alldata = " LAN Number can not be blank";
                    }

                    objData.Barcode = Convert.ToString(dr[1]);
                    objData.State = Convert.ToString(dr[2]);
                    objData.BC_Name = Convert.ToString(dr[3]);
                    objData.Customer_Name = Convert.ToString(dr[4]);
                    objData.Exaternalcustno = Convert.ToString(dr[5]);
                    objData.BC_Branch = Convert.ToString(dr[6]);
                    objData.Center_Name = Convert.ToString(dr[7]);
                    objData.Group_Name = Convert.ToString(dr[8]);

                    if (dr[9] != DBNull.Value)
                    {
                        //objData.Disbursement_Date = Convert.ToString(dr[8]);
                        strTemp = Check_GetCorrectDate(Convert.ToString(dr[9]));
                        if (strTemp.Trim().ToUpper() == "ERROR")
                        {
                            alldata = alldata + "," + "\t" + " Disbursement Date is not in correct format";
                        }
                        else
                        {
                            objData.Disbursement_Date = strTemp;
                        }
                    }

                    objData.UploadFileID = FileUploadID;

                    if (alldata.ToString() != "")
                    {
                        isValid = false; strTemp = "";
                        alldata = "Uploaded File Name :-" + CurrentFileName + "\t" + alldata;

                        strTemp = alldata.Replace(",", Environment.NewLine);
                        CommonDB.ClassPDDError("ClsUploadData", "GetLAN", UploadShtName, strLAN, Convert.ToString(strLAN + strTemp), CurrentFileName);
                    }
                    else
                    {
                        string i = "";
                        i = "uploaded successfully.";
                        //i = ClsUploadData.UploadPDDData(objData, LoginID, UploadShtName + ".txt");
                        if (i.ToLower().Trim() != "uploaded successfully.")
                        {
                            isUpload = false;
                            alldata = "\n LAN ID : '" + strLAN + "' is not uploaded. Error is: " + i;
                            CommonDB.ClassPDDError("ClsUploadData", "GetLAN", UploadShtName, strLAN, Convert.ToString(alldata), CurrentFileName);
                        }
                        setTag++;
                    }


                }


                BindGridData();
                if (isUpload == true && isValid == true)
                {
                    lnkbtnLogFile.Text = "";
                    lnkbtnLogFile.Visible = false;

                    ScriptManager.RegisterStartupScript(this, GetType(), "TestAlert", "alert('PDD Data Uploaded successfully.');", true);

                }
                else if (isUpload == true && isValid == false)
                {
                    hflogFileName.Value = UploadShtName + ".txt";
                    hfLogFilePath.Value = strLogFilePath;

                    lnkbtnLogFile.Visible = true;
                    //lnkbtnLogFile.Text = "Download log file for failed Lead ID" ;

                    ScriptManager.RegisterStartupScript(this, GetType(), "TestAlert", "alert('PDD Data Uploaded. Check log file for missing PDD data');", true);
                }
                else
                {
                    hflogFileName.Value = UploadShtName + ".txt";
                    hfLogFilePath.Value = strLogFilePath;

                    lnkbtnLogFile.Visible = true;
                    //lnkbtnLogFile.Text = "Download log file for failed Lead ID";
                    ScriptManager.RegisterStartupScript(this, GetType(), "TestAlert", "alert('PDD Data Uploaded. Check Log file for missing LAN');", true);
                }



            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
                CommonDB.LogError(this.Page, "DownloadFile", "Error :" + Convert.ToString(ex.Message));
               
            }
        }

        protected void lnkExcelFormat_Click(object sender, EventArgs e)
        {

            try
            {
                string strExcelFilePath = ConfigurationManager.AppSettings["PDDExcelFilePath"].ToString();

                DownloadFile("PDDTrackerFormat.xls", strExcelFilePath);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
                CommonDB.LogError(this.Page, "DownloadFile", "Error :" + Convert.ToString(ex.Message));
              
            }

        }

        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvData.PageIndex = e.NewPageIndex;
                BindGridData();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            DataTable dtDataExport = new DataTable();
            int LoginID = Convert.ToInt32(Session["LoginID"].ToString());

            dtDataExport = ClsUploadData.GetGridData(LoginID);
            if(dtDataExport.Rows.Count > 0)
            {
                string fname = "UploadedRecords.xls";
                ExportToExcel(dtDataExport, fname);
            }
        }

        //protected void gvUpdloadDataDiv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    int LoginID = Convert.ToInt32(Session["LoginID"].ToString());

        //    gvData.PageIndex = e.NewPageIndex;
        //    this.BindUploadedFileGrid(LoginID);
        //}

        //protected void gvUpdloadDataDiv_SelectedIndexChanged(object sender, EventArgs e)
        //    {
        //    string FileName = "";
        //    FileName = (gvUpdloadDataDiv.SelectedRow.FindControl("lblFileName") as Label).Text;

        //    DataTable dtErrorLogFile = new DataTable();
        //    dtErrorLogFile = ClsUploadData.GetErrorLog(FileName);
        //    if (dtErrorLogFile.Rows.Count > 0)
        //    {
        //        string fname = "ErrorLog.xls";

        //        ExportToExcel(dtErrorLogFile, fname);
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('No Error in File');", true);
        //    }
        //}

        //protected void lnkDownloadErrorLog_Click(object sender, EventArgs e)
        //{
        //    int fileID = 0;
        //    string fileName = "";
        //    GridViewRow gvr = (GridViewRow)gvUpdloadDataDiv.SelectedRow;
        //    fileID = Convert.ToInt32(gvr.Cells[0].Text.ToString());
        //    fileName = gvr.Cells[1].Text;


        //}

        private void ExportToExcel(DataTable dt, string fname)
        {
            DataRow row;
            //searchResult.Columns.Remove("FilePath");
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=" + fname);
            Response.Write("<table border=1>");
            Response.Write("<thead>");
            Response.Write("<tr style='font-weight:bold'>");
            foreach (DataColumn clmn in dt.Columns)
            {
                Response.Write("<td align=center bgcolor=darkslateblue><font color=#FFFFFF>" + clmn.ColumnName.ToString() + "</font></td>");
            }
            Response.Write("</tr>");
            Response.Write("</thead>");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = dt.Rows[i];

                Response.Write("<tr>");
                foreach (DataColumn clmn in dt.Columns)
                {
                    Response.Write("<td style='text-align:center;'>" + row[clmn.ColumnName.ToString()].ToString() + "</td>");
                }
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.End();
        }

        //protected void lnkErrorCount_Click(object sender, EventArgs e)
        //{
        //    GridViewRow gvr = (GridViewRow)(sender as LinkButton).NamingContainer;
        //    string fileName = "";
        //    string fileName2 = "";
        //    fileName = gvUpdloadDataDiv.DataKeys[gvr.RowIndex].Value.ToString();
        //    //fileName2 = gvUpdloadDataDiv
        //    //string abc = "";
        //}



        //protected void btnYes_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        hdnConfirmStatus.Value = "YES";
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonDB.LogError(this.Page, "DownloadFile", "Error :" + Convert.ToString(ex.Message));

        //    }
        //}

        //protected void btnNo_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        hdnConfirmStatus.Value = "NO";
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonDB.LogError(this.Page, "DownloadFile", "Error :" + Convert.ToString(ex.Message));

        //    }
        //}
    }
}