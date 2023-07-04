using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JLG.Forms
{
    public partial class frmChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if (Request.QueryString["msg"] != null)
                //{
                //    string strRes = Request.QueryString["msg"].ToString();
                //    if (strRes != "")
                //    {
                //        Page.ClientScript.RegisterClientScriptBlock(GetType(), "msgbox", "alert('Session time out.');", true);
                //    }
                //}

                if (Session["LoginID"] == null)
                {
                    Response.Redirect("../frmLogin.aspx?msg=Session time out.", false);
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonDB.LogError(this.Page, "Page_Load", "Error :" + Convert.ToString(ex.Message));
            }
        }

        public static bool CheckPassword(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            Regex r = new Regex("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=(.*[^a-zA-Z0-9])(?=.*[!@#$*()+=\\;,./{}|\\:\\[\\]\\\\_]){2})(?!.*\\s).{8,15}$");
            return r.IsMatch(str);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                ClsUser objuser = new ClsUser();
                objuser = (ClsUser)Session["objUser"];
                string UserID = objuser.UserId.ToString();
                List<string> strData = new List<string>();


                if (txtOldPWD.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Old Password is mandatory.' );", true);
                    return;
                }
                if (txtNewPWD.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('New Password is mandatory.' );", true);
                    return;
                }
                else 
                {
                    if (CheckPassword(txtNewPWD.Text.Trim()) == false)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('A minimum 8 characters password contains a combination of uppercase and lowercase letter,number and one special character are required.' );", true);
                        return;
                    }
                }
                if (txtConNewPWD.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Confirm New Password is mandatory.' );", true);
                    return;
                }
                if (txtConNewPWD.Text.Trim() != txtNewPWD.Text.Trim())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('New and Confirm password does not match.' );", true);
                    return;
                }


                strData.Add(UserID);
                strData.Add(txtNewPWD.Text.ToString().Trim());
                strData.Add(txtOldPWD.Text.ToString().Trim());

                string results = CommonData.Changepassword(strData);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + results + "');", true);

                txtOldPWD.Text = string.Empty;
                txtNewPWD.Text = string.Empty;
                txtConNewPWD.Text = string.Empty;


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/frmHome.aspx");
        }
    }
}