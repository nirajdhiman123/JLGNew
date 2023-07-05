using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JLG
{
    public partial class frmLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //niraj
                ClsUser objuser = new ClsUser();
                objuser = (ClsUser)Session["objUser"];

                if (Request.QueryString["msg"] != null)
                {
                    string strRes = Request.QueryString["msg"].ToString();
                    if (strRes != "")
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "msgbox", "alert('Session time out.');", true);
                    }
                }

                if (!IsPostBack)
                {
                    if (objuser != null)
                    {
                        string msg = ClsUploadData.UpdateFile_LockUnlock(0, objuser.UserId, "N");
                    }
                }

                if (Session["LoginID"] != null)
                {

                }
            }
            catch (Exception ex)
            {
                CommonDB.LogError(this.Page, "Page_Load", "Error :" + Convert.ToString(ex.Message));
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtlogin = new DataTable();

                ClsUser objuser = new ClsUser();

                string EmpCode = txtUserName.Text.Trim();
                string Login_Password = txtPassword.Text.Trim();
                List<string> UserDetails = new List<string>();
                UserDetails.Add(EmpCode);
                UserDetails.Add(Login_Password);

                dtlogin = CommonData.CheckUserLogin(UserDetails);
                if (dtlogin.Rows.Count > 0)
                {
                    if (dtlogin.Rows[0][0].ToString() == "-1")
                    {
                        //ErrPanel.Controls.Add(CommonDB.ErrorMsgHolder(dtlogin.Rows[0][1].ToString()));
                        ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + dtlogin.Rows[0][1].ToString() + "');", true);
                    }
                    else
                    {
                        foreach (DataRow dr in dtlogin.Rows)
                        {
                            objuser.UserId = Convert.ToInt32(dr["UserId"]);
                            // objuser.User_Type_Code = Convert.ToString(dr["User_Type_Code"]);
                            objuser.EmpCode = Convert.ToString(dr["EmpCode"]);
                            objuser.EmpName = Convert.ToString(dr["EmpName"]);
                            objuser.GroupId = Convert.ToInt32(dr["GroupID"]);
                            //objuser.IsActive = Convert.ToString(dr["IsActive"]);
                            //objuser.Login_Password = Convert.ToString(dr["Login_Password"]);
                            objuser.BranchId = Convert.ToInt32(dr["BranchID"]);
                            objuser.Branch = (dr["BranchList"].ToString());
                            objuser.UserType = Convert.ToString(dr["UserType"]);
                            objuser.dashboard = Convert.ToString(dr["dashboard"]);
                            objuser.Last_LogIn_DateTime = Convert.ToString(dr["Last_LogIn_DateTime"]);


                            Session["objUser"] = objuser;
                            Session["LoginID"] = Convert.ToString(dr["UserId"]);
                            Session["EmpName"] = Convert.ToString(dr["EmpName"]);
                            Session["GroupID"] = Convert.ToInt32(dr["GroupID"]);
                            Session["EmpCode"] = Convert.ToString(dr["EmpCode"]);
                            Session["Login_Text"] = Convert.ToString(dr["Login_Text"]);
                            Session["UserType"] = Convert.ToString(dr["UserType"]);
                            Session["UserBranch"] = Convert.ToString(dr["UserBranch"]);
                            Session["UserCity"] = Convert.ToString(dr["UserCity"]);
                            Session["UserState"] = Convert.ToString(dr["UserState"]);
                            Session["UserPincode"] = Convert.ToString(dr["UserPincode"]);
                            Session["UserZonalType"] = Convert.ToString(dr["ZoneType"]);


                            //  string Dashboard = objuser.dashboard
                            if (Convert.ToInt32(dr["UserId"]) != null)
                            {
                                //int loguserid = Convert.ToInt32(dr["UserId"]);
                                //string Resultlogindate = CommonData.UpdateLoginDate(loguserid);

                                if (dr["Force_Password_Change"].ToString() == "Y")
                                {
                                    Response.Redirect("~/Forms/frmChangePassword.aspx");
                                }
                                else
                                {
                                    Response.Redirect("~/Forms/frmHome.aspx");
                                }


                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "TestAlert", "alert('Kindly check the credentials');", true);
                            }
                        }

                    }

                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "TestAlert", "alert('User Does not exists');", true);
                }
            }
            catch (Exception ex)
            {
                CommonDB.LogError(this.Page, "btnLogin_Click", "Error :" + Convert.ToString(ex.Message));
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }


    }
}