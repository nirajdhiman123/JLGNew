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
using System.Text.RegularExpressions;

namespace JLG.Forms
{
    public partial class frmArchivalDataUpload : System.Web.UI.Page
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
                    BindGridData();
                }
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

                dtData = ClsUploadData.GetArchivalGridData(LoginID);
                if (dtData != null && dtData.Rows.Count > 0)
                {
                    gvData.DataSource = dtData;
                    gvData.DataBind();

                    hflogFileName.Value = dtData.Rows[0]["Archival_ErrorFileName"].ToString();
                    if (hflogFileName.Value.Contains(".txt") == false) hflogFileName.Value = hflogFileName.Value + ".txt";

                    hfLogFilePath.Value = strLogFilePath;
                    if (File.Exists(strLogFilePath + hflogFileName.Value.ToString()))
                    {
                        lnkbtnLogFile.Visible = true;
                    }
                    else
                    {
                        lnkbtnLogFile.Visible = false;
                    }
                }
                else
                {
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
            OleDbConnection connection = new OleDbConnection();
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

                string ExcelFormat = ConfigurationManager.AppSettings["ArchivalExcelFormat"].ToString();


                int LoginID = Convert.ToInt32(Session["LoginID"].ToString());
                string path = ConfigurationManager.AppSettings["UploadLocation"].ToString() + Path.GetFileNameWithoutExtension(FileUpload1.FileName) + DateTime.Now.ToString("ddMMyyyyssmm") + ".xls";
                hdnRpath.Value = path;

                int count = 0;
                //if (File.Exists(path))
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "TestAlert", "alert('PDD file already uploaded');", true);
                //}
                //else
                //{
                if (FileUpload1.HasFile)
                {
                    FileUpload1.SaveAs(path);

                    ClsUploadFile objfileupload = new ClsUploadFile();
                    objfileupload.FileName = Path.GetFileName(FileUpload1.FileName);
                    objfileupload.FilePath = path;
                    objfileupload.UploadedBy = LoginID;
                    CurrentFileName = objfileupload.FileName;

                    int FileUploadID = 0;

                    FileUploadID = ClsUploadData.GetFileUpload(objfileupload);

                    uploadStatus = 'N';
                    readStatus = 'N';
                    fileType = "ARCH";

                    ClsUploadData.UpdateFlagForFileUploaded(FileUploadID, uploadStatus, readStatus, fileType,0);
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please select the file');", true);
                }

                DataTable UnreadArchFileId = new DataTable();

                int ArchFileId = 0;

                UnreadArchFileId = ClsUploadData.GetUnreadFileID("ARCH");

                if (UnreadArchFileId.Rows.Count > 0)
                {
                    for (int a = 0; a < UnreadArchFileId.Rows.Count; a++)
                    {
                        ArchFileId = Convert.ToInt32(UnreadArchFileId.Rows[a][0].ToString());

                        string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0;IMEX=1;';");
                        
                        connection.ConnectionString = excelConnectionString;

                        try
                        {
                            connection.Open();
                        }
                        catch
                        {

                            excelConnectionString = "Provider=Microsoft.Jet.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 15.0;HDR=YES';";
                            connection = new OleDbConnection();
                            connection.ConnectionString = excelConnectionString;

                            connection.Open();
                        }

                        DataTable dtSheet = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        string sht = string.Empty;
                        string strLAN = "";
                        string strBARCODE = "";

                        sht = dtSheet.Rows[0][2].ToString().Replace("'", "");

                        //if (sht != "Data$")
                        //{
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Sheet name should be Data on excel file,Download the sample format.' );", true);
                        //    return;
                        //}

                        OleDbCommand command = new OleDbCommand("select * from [" + sht + "]", connection);
                        OleDbCommand command1 = new OleDbCommand("select * from [" + sht + "]", connection);


                        //OleDbCommand command = new OleDbCommand("select * from [Data$]", connection);
                        //OleDbCommand command1 = new OleDbCommand("select * from [Data$]", connection);

                        UploadShtName = sht + "_" + Convert.ToString(LoginID) + "_" + DateTime.Now.ToString("dd-MMM-yyyy_HHmmss");


                        DataTable dtPDD = new DataTable("Archival");

                        DbDataReader dr = command.ExecuteReader();
                        DbDataReader dr1 = command1.ExecuteReader();

                        dtPDD.Load(dr1);
                        string[] splitarr = ExcelFormat.Split('|');
                        int l = 0;
                        foreach (DataColumn dtcol in dtPDD.Columns)
                        {
                            if (splitarr[l].ToString().ToUpper() != dtcol.ColumnName.ToString().ToUpper())
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('Invalid column name " + dtcol.ColumnName.ToString().ToUpper() + " in excel format.');", true);
                                return;
                            }
                            l++;
                        }

                        CommonDB.ClassPDDError("ClsUploadArchivalData", "GetBarcode", UploadShtName, "Barcode", "Error", CurrentFileName);

                        while (dr.Read())
                        {
                            ClsUploadArchivalData objData = new ClsUploadArchivalData();
                            // ClsUploadFile objUploadFile = new ClsUploadFile();
                            string[] strPickupDt;
                            string strTemp = "";

                            alldata = "";


                            if (dr[0] != DBNull.Value && Convert.ToString(dr[0]).Trim() != "")
                            {
                                objData.Barcode = Convert.ToString(dr[0]);
                                strBARCODE = objData.Barcode;
                            }
                            else
                            {
                                //strLAN = Convert.ToString(dr[1]);
                                alldata = " Barcode can not be blank";
                            }


                            //objData.Barcode = Convert.ToString(dr[1]);
                            //objData.ArchivalDate = Convert.ToString(dr[2]);


                            if (dr[1] != DBNull.Value)
                            {
                                strTemp = Check_GetCorrectDate(Convert.ToString(dr[1]));
                                if (strTemp.Trim().ToUpper() == "ERROR")
                                {
                                    alldata = alldata + "," + "\t" + " Archival Date is not in correct format";
                                }
                                else
                                {
                                    objData.ArchivalDate = strTemp;
                                }
                            }

                            if (dr[2] != DBNull.Value)
                            {
                                if (IsAlphaNumeric(Convert.ToString(dr[2])) == false)
                                {
                                    alldata = alldata + "," + "\t" + " Lot No should be alphanumeric";
                                }
                                else
                                {
                                    objData.LotNo = Convert.ToString(dr[2]);
                                }
                            }

                            if (dr[3] != DBNull.Value)
                            {
                                if (IsAlphaNumeric(Convert.ToString(dr[3])) == false)
                                {
                                    alldata = alldata + "," + "\t" + " Carton No should be alphanumeric";
                                }
                                else
                                {
                                    objData.CartonNo = Convert.ToString(dr[3]);
                                }
                            }

                            if (dr[4] != DBNull.Value)
                            {
                                if (IsAlphaNumeric(Convert.ToString(dr[4])) == false)
                                {
                                    alldata = alldata + "," + "\t" + " File Code should be alphanumeric";
                                }
                                else
                                {
                                    objData.FileNo = Convert.ToString(dr[4]);
                                }
                            }


                            objData.UploadFileID = ArchFileId;
                            string i = "";

                            if (alldata.ToString() != "")
                            {
                                rCount = rCount + 1;
                                isValid = false; strTemp = "";
                                alldata = "Uploaded File Name :-" + CurrentFileName + "\t" + alldata;

                                strTemp = alldata.Replace(",", Environment.NewLine);
                                CommonDB.ClassPDDError("ClsUploadArchivalData", "GetLAN", UploadShtName, strBARCODE, Convert.ToString(strLAN + strTemp), CurrentFileName);
                            }
                            else
                            {
                                
                                i = "uploaded successfully.";
                                //i = ClsUploadData.UploadArchivalData(objData, LoginID, UploadShtName + ".txt"); 
                                setTag++;
                            }

                            if (i.ToLower().Trim() != "uploaded successfully.")
                            {
                                rCount = rCount + 1;
                                isUpload = false;
                                alldata = "LAN ID : '" + strLAN + "' is not uploaded. Error is: " + i;
                                CommonDB.ClassPDDError("ClsUploadArchivalData", "GetLAN", UploadShtName, strBARCODE, Convert.ToString(alldata), CurrentFileName);
                            }
                            else
                            {
                                uCount = uCount + 1;
                            }
                            

                            if (a < (UnreadArchFileId.Rows.Count - 1))
                            {
                                //readStatus = 'Y';
                                //ClsUploadData.UpdateFlagForFileUploaded(ArchFileId, 'Y', readStatus, "ARCH");
                                a = a++;
                            }
                            else
                            {
                                uploadStatus = 'Y';
                                readStatus = 'N';
                                ClsUploadData.UpdateFlagForFileUploaded(ArchFileId,uploadStatus, readStatus, fileType, 0);
                                ClsUploadData.UpdateFileUploadTime(ArchFileId);
  
                            }
                        }
                        //dr.Close();
                        BindGridData();

                        if (isUpload == true && isValid == true)
                        {
                            lnkbtnLogFile.Text = "";
                            lnkbtnLogFile.Visible = false;

                            ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('Archival Data Uploaded successfully.');", true);
                            //ErrPanel.Controls.Add(CommonDB.MsgHolder("Total Uploaded Record - " + Convert.ToString(uCount) + " Total Rejected Record - " + Convert.ToString(rCount)));
                            //ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('Total Uploaded Record - " + Convert.ToString(uCount) + " Total Rejected Record - " + Convert.ToString(rCount) + "');", true);
                        }
                        else if (isUpload == true && isValid == false)
                        {
                            hflogFileName.Value = UploadShtName + ".txt";
                            hfLogFilePath.Value = strLogFilePath;

                            lnkbtnLogFile.Visible = true;
                            //lnkbtnLogFile.Text = "Download log file for failed Lead ID" ;

                            ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('Archival Data Uploaded. Check log file for missing PDD data');", true);
                            // ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('Total Uploaded Record - " + Convert.ToString(uCount) + "Total Rejected Record - " + Convert.ToString(rCount) + "');", true);
                            //ErrPanel.Controls.Add(CommonDB.MsgHolder("Total Uploaded Record - " + Convert.ToString(uCount) + " Total Rejected Record - " + Convert.ToString(rCount)));
                        }
                        else
                        {
                            hflogFileName.Value = UploadShtName + ".txt";
                            hfLogFilePath.Value = strLogFilePath;

                            lnkbtnLogFile.Visible = true;
                            //lnkbtnLogFile.Text = "Download log file for failed Lead ID";
                            ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('Archival Data Uploaded. Check Log file for missing LAN');", true);
                            // ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('Total Uploaded Record - " + Convert.ToString(uCount) + "Total Rejected Record - " + Convert.ToString(rCount) + "');", true);
                            //ErrPanel.Controls.Add(CommonDB.MsgHolder("Total Uploaded Record - " + Convert.ToString(uCount) + " Total Rejected Record - " + Convert.ToString(rCount)));
                        }

                    }
                }

                // Connection String to Excel Workbook

                connection.Close();



                //}


                //}

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
                CommonDB.LogError(this.Page, "BindGridData", "Error :" + Convert.ToString(ex.Message));
            }


        }

        public static bool IsAlphaNumeric(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            Regex r = new Regex("^(?=.*[a-zA-Z])(?=.*[0-9])[A-Za-z0-9]+$");
            return r.IsMatch(str);

        }
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

        protected void lnkExcelFormat_Click(object sender, EventArgs e)
        {
            try
            {
                string strExcelFilePath = ConfigurationManager.AppSettings["ArchivalExcelFilePath"].ToString();

                DownloadFile("ArchivalDataFormat.xls", strExcelFilePath);

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
    }
}