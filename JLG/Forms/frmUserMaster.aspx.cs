using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JLG.Forms
{
    public partial class frmUserMaster : System.Web.UI.Page
    {
        static string EditID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ClsUser objuser = new ClsUser();
                objuser = (ClsUser)Session["objUser"];

                if (objuser != null)
                {
                    if (!IsPostBack)
                    {
                        BindGroup();
                        BindBranch();

                        DataTable dt = new DataTable();
                        dt = CommonData.GetUserDetails("");
                        gvUserDetails.DataSource = dt;
                        gvUserDetails.DataBind();
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }

        }


        private void BindGroup()
        {
            try
            {
                DataTable dt = CommonData.GetGroup();
                ddlGroup.DataSource = dt;
                ddlGroup.DataValueField = "GroupId";
                ddlGroup.DataTextField = "GroupName";
                ddlGroup.DataBind();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

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

        public static bool IsAlphaNumeric(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            Regex r = new Regex("^(?=.*[a-zA-Z])(?=.*[0-9])[A-Za-z0-9]+$");
            return r.IsMatch(str);

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            /////////////////////////////////////////// Changes Done By Kapil Singhal on 9th Dec. 2022 //////////////////////////////////////////////////////////////

            DateTime inActivationDate;
            int inActivationDone_By = 0;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///

            try
            {
                string RandomPassword = string.Empty;

                if (rdnUserType.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please select the User type first');", true);
                    return;
                }

                if (txtUserName.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('User Name can not be blank' );", true);
                    return;
                }
                else
                {

                    if (IsAlphaNumeric(txtUserName.Text.Trim()) == false)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('User Name should contains a combination of letter & number' );", true);
                        return;
                    }
                }

                if (hdnEditId.Value == "")
                {
                    //if (txtPassword.Text.Trim() == "")
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Password can not be blank' );", true);
                    //    return;
                    //}
                }

                if (ddlGroup.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please select the group first' );", true);
                    return;
                }
                if (ddlBranch.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please select the branch first' );", true);
                    return;
                }

                if (txtFirstName.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('First Name can not be blank' );", true);
                    return;
                }
                if (txtLastName.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Last Name can not be blank' );", true);
                    return;
                }

                if (txtEmpCode.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Employee Code can not be blank' );", true);
                    return;
                }

                if (txtEmailId.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Email Id can not be blank' );", true);
                    return;
                }

                if (txtMobileNo.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Mobile No. can not be blank' );", true);
                    return;
                }



                ClsUser objuser = new ClsUser();


                if (hdnEditId.Value.ToString() == "")
                {
                    RandomPassword = CreateRandomPassword();
                }
                else
                {
                    objuser.UserId = Convert.ToInt32(hdnEditId.Value);
                    RandomPassword = "";
                }

                objuser.FirstName = txtFirstName.Text.Trim();
                objuser.LastName = txtLastName.Text.Trim();
                objuser.EmpName = txtFirstName.Text.Trim() + " " + txtLastName.Text.Trim();
                objuser.LoginText = txtUserName.Text.Trim();
                objuser.EmpCode = txtEmpCode.Text.Trim();
                objuser.Login_Password = RandomPassword;//txtPassword.Text.Trim();
                objuser.EmailID = txtEmailId.Text.Trim();
                objuser.MobileNo = txtMobileNo.Text.Trim();
                objuser.GroupId = Convert.ToInt32(ddlGroup.SelectedItem.Value);
                objuser.BranchId = Convert.ToInt32(ddlBranch.SelectedItem.Value);
                //objuser.dashboard = rdodashboard.SelectedValue;
                //if (hdnEditId.Value.ToString() != "")
                //{
                //    objuser.UserId = Convert.ToInt32(hdnEditId.Value);
                //    //if (txtPassword.Enabled == true)
                //    //{
                //    //    objuser.reset = "pass";
                //    //}
                //    //else
                //    //{
                //    //    objuser.reset = null;

                //    //}

                //}

                if (chkIsActive.Checked == true)
                {
                    objuser.IsActive = "Y";
                }
                else
                {
                    objuser.IsActive = "N";


                    ////////////////////////////////////////////////////////// Changes Done By Kapil Singhal on 9th Dec. 2022 //////////////////////////////////////////////

                    inActivationDate = DateTime.Now;
                    objuser.inActivationDate = inActivationDate;
                    string loginID = Session["LoginID"].ToString();
                    objuser.inActivationDone_By = Convert.ToInt32(loginID);

                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///
                }



                objuser.UserType = rdnUserType.SelectedValue;

                string loginText = "";

                loginText = CommonData.CheckExistingUser(objuser.LoginText);

                if (loginText != "")
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This Username is Already Exist..');", true);
                    if (btnSubmit.Text == "Submit")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This Username is Already Exist..');", true);
                    }
                    else
                    {
                        string res = CommonData.InsertUser(objuser);
                        if (res != "")
                        {

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + res + "');", true);

                            if (hdnEditId.Value.ToString() == "")
                            {
                                bool mailStatus = SendMailProcess(txtEmailId.Text, txtFirstName.Text, txtUserName.Text.Trim(), RandomPassword);
                            }

                            btnCancel_Click(null, null);
                            //txtPassword.Enabled = true;
                        }
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", res, true);
                        DataTable dt = new DataTable();
                        dt = CommonData.GetUserDetails("");
                        gvUserDetails.DataSource = dt;
                        gvUserDetails.DataBind();

                        btnSubmit.Text = "Submit";
                    }
                }
                else
                {
                    
                    string res = CommonData.InsertUser(objuser);
                    if (res != "")
                    {

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + res + "');", true);

                        if (hdnEditId.Value.ToString() == "")
                        {
                            bool mailStatus = SendMailProcess(txtEmailId.Text, txtFirstName.Text, txtUserName.Text.Trim(), RandomPassword);
                        }

                        btnCancel_Click(null, null);
                        //txtPassword.Enabled = true;
                    }
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", res, true);
                    DataTable dt = new DataTable();
                    dt = CommonData.GetUserDetails("");
                    gvUserDetails.DataSource = dt;
                    gvUserDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {

                txtFirstName.Text = string.Empty;
                txtLastName.Text = string.Empty;
                txtUserName.Text = string.Empty;
                txtEmailId.Text = string.Empty;
                txtEmpCode.Text = string.Empty;
                txtMobileNo.Text = string.Empty;
                //txtPassword.Text = string.Empty;
                rdnUserType.SelectedValue = null;
                hdnEditId.Value = string.Empty;
                ddlGroup.SelectedIndex = 0;
                ddlBranch.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        protected void imgEdit_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //txtPassword.Enabled = false;
                ImageButton btnEdit = (ImageButton)sender;
                GridViewRow Grow = (GridViewRow)btnEdit.NamingContainer;
                ImageButton imgEdit = (ImageButton)Grow.FindControl("imgEdit");
                //EditID = imgEdit.ToolTip;
                hdnEditId.Value = imgEdit.ToolTip;
                DisplayData();
                btnSubmit.Text = "Update";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        protected void lnkrest_Click(object sender, EventArgs e)
        {
            try
            {
                txtUserName.Enabled = false;
                txtEmpCode.Enabled = false;
                chkIsActive.Enabled = false;
                ddlGroup.Enabled = false;
                rdnUserType.Enabled = false;
                ddlBranch.Enabled = false;
                //txtPassword.Enabled = true;

                ClsUser objuser = new ClsUser();
                objuser = (ClsUser)Session["objUser"];
                string UserId = objuser.UserId.ToString();


                string ResetPassword = CreateRandomPassword();

                LinkButton btnEdit = (LinkButton)sender;
                GridViewRow Grow = (GridViewRow)btnEdit.NamingContainer;
                LinkButton lnkrest = (LinkButton)Grow.FindControl("lnkrest");

                HiddenField EmailId = (HiddenField)Grow.FindControl("hdnEmailId");
                HiddenField LoginText = (HiddenField)Grow.FindControl("hdnLoginText");
                HiddenField EmpName = (HiddenField)Grow.FindControl("hdnEmpName");

                EditID = lnkrest.ToolTip;
                //DisplayData();
                string result = CommonData.ResetPassword(EditID, UserId, ResetPassword);
                if (result != "")
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + result + "')", true);

                    bool mailStatus = SendMailProcess(EmailId.Value, EmpName.Value, LoginText.Value, ResetPassword);

                    btnCancel_Click(null, null);

                    DataTable dt = new DataTable();
                    dt = CommonData.GetUserDetails("");
                    gvUserDetails.DataSource = dt;
                    gvUserDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtLoginText.Text.Trim() == "")
                {

                    DataTable dt = new DataTable();
                    dt = CommonData.GetUserDetails("");
                    gvUserDetails.DataSource = dt;
                    gvUserDetails.DataBind();
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt = CommonData.GetUserDetails(txtLoginText.Text.Trim());
                    gvUserDetails.DataSource = dt;
                    gvUserDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        private void DisplayData()
        {
            try
            {
                ClsUser objuser = new ClsUser();
                DataTable dtTable = CommonData.UserDisplayData(Convert.ToInt32(hdnEditId.Value));

                if (dtTable != null)
                {
                    if (dtTable.Rows.Count > 0)
                    {

                        txtUserName.Text = dtTable.Rows[0]["Login_Text"].ToString();
                        if (dtTable.Rows[0]["IsActive"].ToString() == "Y")
                        {
                            chkIsActive.Checked = true;
                        }
                        else
                        {
                            chkIsActive.Checked = false;
                        }
                        txtFirstName.Text = dtTable.Rows[0]["First_Name"].ToString();
                        txtLastName.Text = dtTable.Rows[0]["Last_Name"].ToString();
                        txtEmpCode.Text = dtTable.Rows[0]["EmpCode"].ToString();
                        ddlGroup.SelectedValue = dtTable.Rows[0]["GroupID"].ToString();
                        //txtPassword.Text = dtTable.Rows[0]["Login_Password"].ToString();
                        rdnUserType.SelectedValue = dtTable.Rows[0]["UserType"].ToString();
                        ddlBranch.SelectedValue = dtTable.Rows[0]["BranchID"].ToString();
                        //rdodashboard.SelectedValue = dtTable.Rows[0]["dashboard"].ToString();
                        txtEmailId.Text = dtTable.Rows[0]["EmailId"].ToString();
                        txtMobileNo.Text = dtTable.Rows[0]["MobileNo"].ToString();
                        //txtPassword.Enabled = false;
                    }
                }


            }
            catch (System.Threading.ThreadAbortException) { }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        // Default size of random password is 15  
        private static string CreateRandomPassword(int length = 8)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        private static string CreateRandomPasswordWithRandomLength()
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();

            // Minimum size 8. Max size is number of all allowed chars.  
            int size = random.Next(8, validChars.Length);

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[size];
            for (int i = 0; i < size; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        private Boolean SendMailProcess(string TO_EMAIL_ID, string EmpName, string LoginText, string pwd)
        {
            ClsUser objuser = new ClsUser();
            Boolean mailstatus = false;
            objuser = (ClsUser)Session["objUser"];

            try
            {
                SmtpClient mailClient = new SmtpClient();

                string MailUserName = ConfigurationSettings.AppSettings["MailUserID"].ToString();
                string MailPassword = ConfigurationSettings.AppSettings["MailPassword"].ToString();
                string MailHost = ConfigurationSettings.AppSettings["MailHost"].ToString();
                string MailPort = ConfigurationSettings.AppSettings["MailPort"].ToString();
                string FromMailID = ConfigurationSettings.AppSettings["FromMailID"].ToString();
                //string EmailTemplatepath = Server.MapPath("~/Templates/");  //ConfigurationSettings.AppSettings["EmailTemplatepath"].ToString();
                string EmailTemplatepath = ConfigurationSettings.AppSettings["EmailTemplatepath"].ToString();

                //---------EMail Sending Coade -----------------  //

                MailMessage MailMessage = new MailMessage();
                System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential(MailUserName, MailPassword);
                mailClient.Credentials = basicAuthenticationInfo;
                MailAddress MailAddress = new MailAddress(FromMailID);

                string strSubj = null;
                string strMailBody = null;

                mailClient.Host = MailHost;
                mailClient.Port = Convert.ToInt32(MailPort);
                MailMessage.From = MailAddress;

                //---------To Mail ID -----------------------------------//
                MailMessage.To.Add(TO_EMAIL_ID);
                //--------------Mail Subject----------------------------//

                strSubj = "";

                strSubj = "Login Credential";
                MailMessage.Subject = strSubj;
                //--------------Mail Body----------------------------//
                MailMessage.IsBodyHtml = true;

                //System.IO.Path.GetFullPath("~/MailFormat.htm");
                StreamReader reader = null;
                string appPath = EmailTemplatepath;
                reader = new StreamReader(EmailTemplatepath + "\\LoginCredential.htm");
                string readFile = reader.ReadToEnd();

                strMailBody = readFile;
                strMailBody = strMailBody.Replace("$$empname$$", EmpName);
                strMailBody = strMailBody.Replace("$$loginid$$", LoginText);
                strMailBody = strMailBody.Replace("$$pwd$$", pwd);



                MailMessage.Body = strMailBody;
                MailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                mailClient.Send(MailMessage);

                //-----End Mail Sending -----------------
                string mailmsg = MailMessage.DeliveryNotificationOptions.ToString();

                MailMessage = null;
                if (mailmsg == "OnSuccess")
                {
                    mailstatus = true;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex + "')", true);
            }
            return mailstatus;
        }

        protected void gvUserDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvUserDetails.PageIndex = e.NewPageIndex;

                DataTable dt = new DataTable();
                dt = CommonData.GetUserDetails("");
                gvUserDetails.DataSource = dt;
                gvUserDetails.DataBind();


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        
        protected void txtUserName_TextChanged(object sender, EventArgs e)
        {
            string loginText = "";

            //loginText = CommonData.CheckExistingUser(txtUserName.Text);

            //if (loginText != "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This Username is Already Exist..');", true);
               
            //}
        }


        /// <summary>
        /// /////////////////////////////////////////////// Added By Kapil Singhal on 12th Dec. 2022 ////////////////////////////////////////////////////////
        /// 


        protected void btnDownloadUserMaster_Click(object sender, EventArgs e)
        {
            DataTable dtUserMaster = new DataTable();
            dtUserMaster = CommonData.GetUserMaster();
            if(dtUserMaster.Rows.Count > 0)
            {
                string fname = "UserMaster.xls";
                ExportToExcel(dtUserMaster, fname);
            }
        }

        private void ExportToExcel(DataTable dtDataExport, string fname)
        {
            DataRow row;
            //searchResult.Columns.Remove("FilePath");
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=" + fname);
            Response.Write("<table border=1 cellpadding=10>");
            Response.Write("<thead>");
            Response.Write("<tr width=30 style='font-weight:bold'>");
            foreach (DataColumn clmn in dtDataExport.Columns)
            {
                Response.Write("<td align=center bgcolor=darkslateblue><font color=#FFFFFF>" + clmn.ColumnName.ToString() + "</font></td>");
            }
            Response.Write("</tr>");
            Response.Write("</thead>");

            for (int i = 0; i < dtDataExport.Rows.Count; i++)
            {
                row = dtDataExport.Rows[i];
                Response.Write("<tr>");
                foreach (DataColumn clmn in dtDataExport.Columns)
                {
                    if(row[clmn.ColumnName].ToString() == "INACTIVE")
                    {
                        Response.Write("<td style='background-color:Red;'>" + row[clmn.ColumnName.ToString()].ToString() + "</td>");                        
                    }
                    else
                    {
                        Response.Write("<td>" + row[clmn.ColumnName.ToString()].ToString() + "</td>");
                    }                    
                }
                Response.Write("</tr>");
            }

            Response.Write("</table>");
            Response.End();           

        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
    }
}