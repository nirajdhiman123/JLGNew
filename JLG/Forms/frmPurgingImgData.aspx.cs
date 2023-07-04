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
    public partial class frmPurgingImgData : System.Web.UI.Page
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

        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            string i = "";
            char uploadStatus;
            char readStatus;
            string fileType = "";
            int FileUploadID = 0;

            try
            {
                int LoginID = Convert.ToInt32(Session["LoginID"].ToString());
                string path = ConfigurationManager.AppSettings["UploadLocation"].ToString() + Path.GetFileNameWithoutExtension(FileUpload1.FileName) + DateTime.Now.ToString("ddMMyyyyssmm") + ".csv";
                hdnRpath.Value = path;

                if (FileUpload1.HasFile)
                {
                    FileUpload1.SaveAs(path);

                    if (Path.GetExtension(path) == ".csv")
                    {
                        ClsUploadFile objfileupload = new ClsUploadFile();
                        objfileupload.FileName = Path.GetFileName(FileUpload1.FileName);
                        objfileupload.FilePath = path;
                        objfileupload.UploadedBy = LoginID;

                        

                        FileUploadID = ClsUploadData.GetFileUpload(objfileupload);

                        uploadStatus = 'N';
                        readStatus = 'N';
                        fileType = "PURG";

                        ClsUploadData.UpdateFlagForFileUploaded(FileUploadID, uploadStatus, readStatus, fileType, 0); //var lines = File.ReadAllLines(path);
                    }
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please select the file');", true);
                }

                DataTable UnreadPurgFileId = new DataTable();

                int PurgFileId = 0;
                string PurgeFilePath = "";
                string PurgeFileName = "";

                UnreadPurgFileId = ClsUploadData.GetUnreadFileID("PURG");

                if (UnreadPurgFileId.Rows.Count > 0)
                {
                    for (int m = 0; m < UnreadPurgFileId.Rows.Count; m++)
                    {
                        PurgFileId = Convert.ToInt32(UnreadPurgFileId.Rows[m][0].ToString());
                        PurgeFilePath = UnreadPurgFileId.Rows[m]["FilePath"].ToString();
                        PurgeFileName = UnreadPurgFileId.Rows[m]["FileName"].ToString();

                        string[] Allline = File.ReadAllLines(PurgeFilePath);
                        var reader = new StreamReader(File.OpenRead(path));
                        int filelen = 0;
                        filelen = Allline.Length;
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            //i = ClsUploadData.UploadPurgingData(line.ToString(), LoginID, PurgeFileName, PurgeFilePath);
                        }

                        if (m < (UnreadPurgFileId.Rows.Count - 1))
                        {
                            //readStatus = 'Y';
                            //ClsUploadData.UpdateFlagForFileUploaded(PurgFileId, 'Y', readStatus, "PURG");
                            m = m++;
                        }
                        else
                        {
                            uploadStatus = 'Y';
                            readStatus = 'N';
                            fileType = "PURG";
                            ClsUploadData.UpdateFlagForFileUploaded(FileUploadID, uploadStatus, readStatus, fileType, 0); //var lines = File.ReadAllLines(path);
                            ClsUploadData.UpdateFileUploadTime(FileUploadID);
                            i = "uploaded successfully.";
                        }
                    }

                    if (i == "uploaded successfully.")
                    {
                        BindGridData();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Info", "alert('Purging Data Uploaded successfully.');", true);
                    }
                }
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
                CommonDB.LogError(this.Page, "btnUploadFile_Click", "Error :" + Convert.ToString(ex.Message));
            }
        }

        private void BindGridData()
        {
            try
            {
                DataTable dtData = new DataTable();
                int LoginID = Convert.ToInt32(Session["LoginID"].ToString());

                dtData = ClsUploadData.GetPurgingGridData(LoginID);
                if (dtData != null && dtData.Rows.Count > 0)
                {
                    gvData.DataSource = dtData;
                    gvData.DataBind();

                    //hflogFileName.Value = dtData.Rows[0]["Archival_ErrorFileName"].ToString();
                    //if (hflogFileName.Value.Contains(".txt") == false) hflogFileName.Value = hflogFileName.Value + ".txt";

                    //hfLogFilePath.Value = strLogFilePath;
                    //if (File.Exists(strLogFilePath + hflogFileName.Value.ToString()))
                    //{
                    //    lnkbtnLogFile.Visible = true;
                    //}
                    //else
                    //{
                    //    lnkbtnLogFile.Visible = false;
                    //}
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