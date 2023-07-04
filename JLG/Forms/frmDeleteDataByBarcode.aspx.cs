using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JLG.Forms
{
    public partial class frmDeleteDataByBarcode : System.Web.UI.Page
    {
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

                    //BindGridData();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        //protected void txtFileBarcode_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ClsUser objuser = new ClsUser();
        //        objuser = (ClsUser)Session["objUser"];

        //        if (txtFileBarcode.Text.Trim() != "")
        //        {
        //            if (txtFileBarcode.Text.Length == 10)
        //            {
        //                DataTable dt = new DataTable();
        //                dt = ClsUploadData.GetSearchAllData(txtFileBarcode.Text.Trim());

        //                if (dt != null)
        //                {
        //                    if (dt.Rows.Count > 0)
        //                    {
        //                        string IWDate = dt.Rows[0]["Inward_Date"].ToString();

        //                        if (IWDate == "")
        //                        {
        //                            gvInward.DataSource = dt;
        //                            gvInward.DataBind();

        //                        }
        //                        else
        //                        {
        //                            ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('File already inwared');", true);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('No PDD data Found, " + res + "');", true);
        //                        txtFileBarcode.Text = string.Empty;
        //                        txtFileBarcode.Focus();

        //                    }
        //                }
        //                else
        //                {
        //                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('No PDD data Found, " + res + "');", true);
        //                    txtFileBarcode.Text = string.Empty;
        //                    txtFileBarcode.Focus();

        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
        //    }
        //}

        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Forms/frmHome.aspx");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            try
            {
                int setTag = 0;
                Boolean isUpload = true;
                Boolean isValid = true;
                string alldata = "";
                int uCount = 0;
                int rCount = 0;

                //string ExcelFormat = ConfigurationManager.AppSettings["ArchivalExcelFormat"].ToString();
                string ExcelFormat = "Barcode|File Name";


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

                    string UploadShtName = "";
                    string LogFileFullPath = "";
                    string CurrentFileName = "";


                    ClsUploadFile objfileupload = new ClsUploadFile();
                    objfileupload.FileName = Path.GetFileName(FileUpload1.FileName);
                    objfileupload.FilePath = path;
                    objfileupload.UploadedBy = LoginID;
                    CurrentFileName = objfileupload.FileName;

                    int FileUploadID = 0;

                    FileUploadID = ClsUploadData.GetFileUpload(objfileupload);

                    // Connection String to Excel Workbook
                    string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0;IMEX=1;';");
                    OleDbConnection connection = new OleDbConnection();
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


                    DataTable dtPDD = new DataTable("Data");

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

                    CommonDB.ClassPDDError("ClsDeleteDataByBarcode", "GetColumn", UploadShtName, "Column", "Error", CurrentFileName);

                    while (dr.Read())
                    {
                        //ClsUploadArchivalData objData = new ClsUploadArchivalData();
                        // ClsUploadFile objUploadFile = new ClsUploadFile();
                        string[] strPickupDt;
                        string strTemp = "";

                        alldata = "";


                        //if (dr[0] != DBNull.Value && Convert.ToString(dr[0]).Trim() != "")
                        //{
                        //    objData.Barcode = Convert.ToString(dr[0]);
                        //    strBARCODE = objData.Barcode;
                        //}
                        //else
                        //{
                        //    //strLAN = Convert.ToString(dr[1]);
                        //    alldata = " Barcode can not be blank";
                        //}


                        //objData.UploadFileID = FileUploadID;

                        //if (alldata.ToString() != "")
                        //{
                        //    rCount = rCount + 1;
                        //    isValid = false; strTemp = "";
                        //    alldata = "Uploaded File Name :-" + CurrentFileName + "\t" + alldata;

                        //    strTemp = alldata.Replace(",", Environment.NewLine);
                        //    CommonDB.ClassPDDError("ClsUploadArchivalData", "GetLAN", UploadShtName, strBARCODE, Convert.ToString(strLAN + strTemp), CurrentFileName);
                        //}
                        //else
                        //{
                        //    string i = "";

                        //    i = ClsUploadData.UploadArchivalData(objData, LoginID, UploadShtName + ".txt");
                        //    if (i.ToLower().Trim() != "uploaded successfully.")
                        //    {
                        //        rCount = rCount + 1;
                        //        isUpload = false;
                        //        alldata = "LAN ID : '" + strLAN + "' is not uploaded. Error is: " + i;
                        //        CommonDB.ClassPDDError("ClsUploadArchivalData", "GetLAN", UploadShtName, strBARCODE, Convert.ToString(alldata), CurrentFileName);
                        //    }
                        //    else
                        //    {
                        //        uCount = uCount + 1;
                        //    }
                        //    setTag++;
                        //}

                        string i = "";

                        i = ClsUploadData.DeleteDataByBarcode(Convert.ToString(dr[0]), Convert.ToString(dr[1]), LoginID);
                        if (i.Trim() == "Deleted successfully.")
                        {
                            rCount = rCount + 1;
                        }

                        uCount = uCount + 1;

                    }


                    BindGridData();

                    ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('Data Deleted successfully.');", true);
                    ErrPanel.Controls.Add(CommonDB.MsgHolder("Total Uploaded Record - " + Convert.ToString(uCount) + " Total Deleted Record - " + Convert.ToString(rCount)));

                    //if (isUpload == true && isValid == true)
                    //{
                    //    lnkbtnLogFile.Text = "";
                    //    lnkbtnLogFile.Visible = false;

                    //    ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('Archival Data Uploaded successfully.');", true);
                    //    ErrPanel.Controls.Add(CommonDB.MsgHolder("Total Uploaded Record - " + Convert.ToString(uCount) + " Total Rejected Record - " + Convert.ToString(rCount)));
                    //    //ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('Total Uploaded Record - " + Convert.ToString(uCount) + " Total Rejected Record - " + Convert.ToString(rCount) + "');", true);
                    //}
                    //else if (isUpload == true && isValid == false)
                    //{
                    //    hflogFileName.Value = UploadShtName + ".txt";
                    //    hfLogFilePath.Value = strLogFilePath;

                    //    lnkbtnLogFile.Visible = true;
                    //    //lnkbtnLogFile.Text = "Download log file for failed Lead ID" ;

                    //    ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('Archival Data Uploaded. Check log file for missing PDD data');", true);
                    //    // ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('Total Uploaded Record - " + Convert.ToString(uCount) + "Total Rejected Record - " + Convert.ToString(rCount) + "');", true);
                    //    ErrPanel.Controls.Add(CommonDB.MsgHolder("Total Uploaded Record - " + Convert.ToString(uCount) + " Total Rejected Record - " + Convert.ToString(rCount)));
                    //}
                    //else
                    //{
                    //    hflogFileName.Value = UploadShtName + ".txt";
                    //    hfLogFilePath.Value = strLogFilePath;

                    //    lnkbtnLogFile.Visible = true;
                    //    //lnkbtnLogFile.Text = "Download log file for failed Lead ID";
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('Archival Data Uploaded. Check Log file for missing LAN');", true);
                    //    // ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('Total Uploaded Record - " + Convert.ToString(uCount) + "Total Rejected Record - " + Convert.ToString(rCount) + "');", true);
                    //    ErrPanel.Controls.Add(CommonDB.MsgHolder("Total Uploaded Record - " + Convert.ToString(uCount) + " Total Rejected Record - " + Convert.ToString(rCount)));
                    //}

                    //}
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please select the file');", true);
                }
                //}

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
                CommonDB.LogError(this.Page, "BindGridData", "Error :" + Convert.ToString(ex.Message));
            }
        }

        protected void lnkExcelFormat_Click(object sender, EventArgs e)
        {
            try
            {
                string strExcelFilePath = ConfigurationManager.AppSettings["PDDDeleteExcelFilePath"].ToString();

                DownloadFile("PDDDeleteFormat.xls", strExcelFilePath);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
                CommonDB.LogError(this.Page, "DownloadFile", "Error :" + Convert.ToString(ex.Message));

            }
        }

        private void BindGridData()
        {
            try
            {
                DataTable dtData = new DataTable();
                int LoginID = Convert.ToInt32(Session["LoginID"].ToString());

                dtData = ClsUploadData.GetPDDDeletedData(LoginID);
                if (dtData != null && dtData.Rows.Count > 0)
                {
                    gvData.DataSource = dtData;
                    gvData.DataBind();

                    //hflogFileName.Value = dtData.Rows[0]["Archival_ErrorFileName"].ToString();
                    //if (hflogFileName.Value.Contains(".txt") == false) hflogFileName.Value = hflogFileName.Value + ".txt";

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
    }
}