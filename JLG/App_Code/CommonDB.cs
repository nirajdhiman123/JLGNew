using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using System.Text;
using System.IO;


namespace JLG
{
    public class CommonDB
    {
        #region DataBase Connection
        public static SqlConnection OpenConnection()
        {
            SqlConnection dbCon = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString);
            dbCon.Open();
            return dbCon;
        }
        //To Close the Connection
        public static void CloseConnection(SqlConnection dbCon)
        {
            if (dbCon != null)
            {
                if (dbCon.State == ConnectionState.Open)
                {
                    dbCon.Close();
                }
            }
        }
        #endregion
        #region Log Files ---------------------------------        
        public static void LogError(object objPage, string Module, string ErrorMsg)
        {
            try
            {
                System.Web.HttpContext context = System.Web.HttpContext.Current;
                ClsUser obj = (ClsUser)context.Session["objUser"];


                string FullMessage = "UserID : " + obj.UserId.ToString() + Environment.NewLine +
                                     "DateTime  : " + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss") + Environment.NewLine +
                                     "Page      : " + objPage.ToString() + Environment.NewLine +
                                     "Module    : " + Module.ToString() + Environment.NewLine +
                                     "Error     : " + Environment.NewLine + Environment.NewLine +
                                     ErrorMsg + Environment.NewLine + Environment.NewLine +
                                     "****************************************************************************************"
                                     + Environment.NewLine;

                try
                {
                    //string ErrorLogFolder = "~/LogFolder";
                    //  string ErrorLogFolder = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "LogFolder\\";
                    string ErrorLogFolder = ConfigurationManager.AppSettings["LogFilePath"].ToString() + "LogFolder\\";
                    string ProjName = ConfigurationManager.AppSettings["ProjName"].ToString();

                    if (Directory.Exists(ErrorLogFolder) == false)
                    {
                        Directory.CreateDirectory(ErrorLogFolder);
                    }
                    string ErrorFilename = ProjName + "_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt";
                    string FullFileName = Path.Combine(ErrorLogFolder, ErrorFilename);
                    File.AppendAllText(FullFileName, FullMessage);
                }
                catch { }

            }
            catch { }
        }

        public static void ClassLogError(string Class, string Module, string ErrorMsg)
        {
            try
            {
                System.Web.HttpContext context = System.Web.HttpContext.Current;
                ClsUser obj = (ClsUser)context.Session["objUser"];

                string FullMessage = "UserID : " + obj.UserId.ToString() + Environment.NewLine +
                                     "DateTime  : " + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss") + Environment.NewLine +
                                     "Class      : " + Class + Environment.NewLine +
                                     "Module    : " + Module.ToString() + Environment.NewLine +
                                     "Error     : " + Environment.NewLine + Environment.NewLine +
                                     ErrorMsg + Environment.NewLine + Environment.NewLine +
                                     "****************************************************************************************"
                                     + Environment.NewLine;

                try
                {
                    // string ErrorLogFolder = "~/LogFolder";
                    //string ErrorLogFolder = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "LogFolder\\";
                    string ErrorLogFolder = ConfigurationManager.AppSettings["LogFilePath"].ToString() + "LogFolder\\";
                    string ProjName = ConfigurationManager.AppSettings["ProjName"].ToString();

                    if (Directory.Exists(ErrorLogFolder) == false)
                    {
                        Directory.CreateDirectory(ErrorLogFolder);
                    }
                    string ErrorFilename = ProjName + "_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt";
                    string FullFileName = Path.Combine(ErrorLogFolder, ErrorFilename);
                    File.AppendAllText(FullFileName, FullMessage);
                }
                catch { }

            }
            catch { }
        }

        public static void ClassPDDError(string Class, string Module, string ErrorFileName, string Barcode, string ErrorMsg, string ExcelFileName)
        {
            try
            {
                System.Web.HttpContext context = System.Web.HttpContext.Current;
                ClsUser obj = (ClsUser)context.Session["objUser"];

                //string FullMessage = "Upload PDD File : " + ExcelFileName + Environment.NewLine +
                //                     "EmpCode   : " + obj.EmpCode.ToString() + Environment.NewLine +
                //                     "DateTime : " + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss") + Environment.NewLine +
                //                     "Class    : " + Class + Environment.NewLine +
                //                     "Module   : " + Module.ToString() + Environment.NewLine +
                //                     "Error    : " + ErrorMsg + " , " + Environment.NewLine +
                string FullMessage = Barcode + "|" + ErrorMsg + Environment.NewLine;
                //"****************************************************************************************"
                //+ Environment.NewLine;

                try
                {
                    // string ErrorLogFolder = "~/LogFolder";
                    //  string ErrorLogFolder = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "LogFolder\\";
                    string ErrorLogFolder = ConfigurationManager.AppSettings["LogFilePath"].ToString() + "LogFolder\\";


                    if (Directory.Exists(ErrorLogFolder) == false)
                    {
                        Directory.CreateDirectory(ErrorLogFolder);
                    }
                    //  string ErrorFilename = "IndiaBullsLeadData_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt";
                    ErrorFileName = ErrorFileName + ".txt";

                    string FullFileName = Path.Combine(ErrorLogFolder, ErrorFileName);
                    File.AppendAllText(FullFileName, FullMessage);
                }
                catch { }

            }
            catch { }
        }
        #endregion Log Files ---------------------------------

        public static Control ErrorMsgHolder(string Error)
        {
            //Object value = null;
            System.Web.UI.HtmlControls.HtmlGenericControl dynDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            try
            {
                Random rand = new Random();
                dynDiv.ID = "dynDivCode" + rand.Next(0, 10);
                dynDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FF6633");
                dynDiv.Style.Add(HtmlTextWriterStyle.Color, "Black");
                dynDiv.Style.Add(HtmlTextWriterStyle.Height, "20px");
                dynDiv.Style.Add(HtmlTextWriterStyle.Width, "100%");
                dynDiv.InnerHtml = Error;
                //value = dynDiv;
            }
            catch (Exception EX)
            {
                //LogError(this.Page, "ErrorMsgHolder", EX.ToString());
            }
            return dynDiv;
        }

        public static Control MsgHolder(string Msg)
        {
            //Object value = null;
            System.Web.UI.HtmlControls.HtmlGenericControl dynDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            try
            {
                Random rand = new Random();
                dynDiv.ID = "dynDivCode" + rand.Next(0, 10);
                dynDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#33CC33");
                dynDiv.Style.Add(HtmlTextWriterStyle.Color, "Black");
                dynDiv.Style.Add(HtmlTextWriterStyle.Height, "20px");
                dynDiv.Style.Add(HtmlTextWriterStyle.Width, "100%");
                dynDiv.InnerHtml = Msg;
                //value = dynDiv;
            }
            catch (Exception EX)
            {
                //LogError(this.Page, "MsgHolder", EX.ToString());
            }
            return dynDiv;
        }

        public static void FillDropDown(DropDownList obj, DataTable dtcmd, string v_Member, string d_Member)
        {
            try
            {
                obj.Items.Clear();

                if (dtcmd.Rows.Count > 0)
                {
                    obj.Items.Add(new ListItem("--Select--", "0"));
                    foreach (DataRow row in dtcmd.Rows)
                    {
                        obj.Items.Add(new ListItem(row[d_Member].ToString(), row[v_Member].ToString()));
                    }
                }
                else
                {
                    obj.DataSource = null;
                    obj.Items.Add(new ListItem("--Select--", "0"));
                }

            }
            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public static void FillListBox(ListBox obj, DataTable dtcmd, string v_Member, string d_Member)
        {
            try
            {
                obj.Items.Clear();

                if (dtcmd.Rows.Count > 0)
                {
                    obj.Items.Add(new ListItem("All", "0"));
                    foreach (DataRow row in dtcmd.Rows)
                    {
                        obj.Items.Add(new ListItem(row[d_Member].ToString(), row[v_Member].ToString()));
                    }
                }
                else
                {
                    obj.DataSource = null;
                    obj.Items.Add(new ListItem("All", "0"));
                }

            }
            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public static void FillChkList(CheckBoxList obj, DataTable dtcmd, string v_Member, string d_Member)
        {
            try
            {
                obj.Items.Clear();

                if (dtcmd.Rows.Count > 0)
                {
                    obj.Items.Add(new ListItem("All", "All"));
                    foreach (DataRow row in dtcmd.Rows)
                    {
                        obj.Items.Add(new ListItem(row[d_Member].ToString(), row[v_Member].ToString()));
                    }
                }
                else
                {
                    obj.DataSource = null;
                    obj.Items.Add(new ListItem("All", "All"));
                }

            }
            catch (Exception ex)
            {
                throw ex;
                // MessageBox.Show("Error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public static string beforedot(string s)
        {
            int l = s.LastIndexOf(".");
            return s.Substring(0, l);
        }

        public static string afterdot(string s)
        {
            string[] array = s.Split('.');
            string Ext = "." + array[array.Length - 1];
            return Ext;
        }

        public static string Encryptdata(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new
            byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }

        public static DataTable GetDataTable(string prmQuery)
        {
            SqlDataAdapter dbRdr = new SqlDataAdapter();
            DataTable dtTable = null;
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            try
            {

                using (dbCon = OpenConnection())
                {
                    dbCmd = new SqlCommand();
                    dbCmd.Connection = dbCon;
                    dbCmd.CommandText = prmQuery;
                    dbRdr.Fill(dtTable);
                }
            }
            catch (Exception ex)
            {
                //LogError(this.Page, "GetDataSet", EX.ToString());
            }
            return dtTable;
        }

        public static Object GetValue(string prmQuery)
        {
            SqlDataAdapter dbRdr = new SqlDataAdapter();
            SqlCommand dbCmd = null;
            SqlConnection dbCon = null;
            Object value = null;
            try
            {
                using (dbCon = OpenConnection())
                {
                    dbCmd = new SqlCommand();
                    dbCmd.Connection = dbCon;
                    dbCmd.CommandText = prmQuery;
                    value = dbCmd.ExecuteScalar();
                }

            }
            catch (Exception EX)
            {
                //LogError(this.Page, "GetValue", EX.ToString());
            }
            return value;
        }

        public static string GetSrc(string link)
        {
            string src = string.Empty;
            string url = HttpContext.Current.Request.Url.ToString();
            string[] urlParts = url.Split('/');
            string port = HttpContext.Current.Request.ServerVariables["SERVER_PORT"] == "80" ? "" : HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            int count = urlParts[2].Split(':').Length - 1;
            if (count >= 1)
            {

                //src = "http://" + urlParts[2] + "/"; changed   
                src = urlParts[0] + "//" + urlParts[2] + "/";
            }
            else if (port.Length > 0)
            {
                //src = "http://" + urlParts[2] + ":" + port + "/"; changed   

                src = urlParts[0] + "//" + urlParts[2] + ":" + port + "/";
            }
            else
            {
                //src = "http://" + urlParts[2] + "/"; changed   
                src = urlParts[0] + "//" + urlParts[2] + "/";

            }

            src = ConfigurationManager.AppSettings["AppLocation"].ToString() == string.Empty ? src : src + ConfigurationManager.AppSettings["AppLocation"].ToString() + "/";

            if (link == "Handler")
            {
                src = src + "GenericHandler.ashx?f=";
            }
            else
            {
                //src = src + "Images/preview-not-available.pdf";
                src = src + "Images/imagenotfound.jpeg";
            }

            return src;

        }
    }
}