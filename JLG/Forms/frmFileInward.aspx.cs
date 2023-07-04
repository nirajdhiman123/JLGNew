using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JLG.Forms
{
    public partial class frmFileInward : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ClsUser objuser = new ClsUser();
                objuser = (ClsUser)Session["objUser"];

                if (!Page.IsPostBack)
                {
                    if (Session["LoginID"] == null)
                    {
                        Response.Redirect("../frmLogin.aspx?msg=Session time out.", false);
                        return;
                    }

                    BindBranch();

                    DataTable dt = CommonData.GetKeyValues("Courier_Name", "");
                    CommonDB.FillDropDown(ddlCourierName, dt, "key_value", "key_name");

                    DataTable dtData = ClsUploadData.GetInwardData(objuser.UserId);
                    if (dtData != null && dtData.Rows.Count > 0)
                    {
                        gvInward.DataSource = dtData;
                        gvInward.DataBind();
                    }
                }
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

        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("../Forms/frmHome.aspx", false);
                return;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        protected void txtFileBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string[] Data = new string[5];

                ClsUser objuser = new ClsUser();
                objuser = (ClsUser)Session["objUser"];

                if (ddlBranch.SelectedValue == "")
                //if (txtLocation.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Enter location');", true);
                    return;
                }
                if (ddlCourierName.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Select courier name');", true);
                    return;
                }

                if (txtPODNumber.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Enter POD number');", true);
                    return;
                }
                if (txtReceivedDate.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Enter recived date ');", true);
                    return;
                }

                if (txtFileBarcode.Text.Trim() != "")
                {
                    if (txtFileBarcode.Text.Length == 10)
                    {
                        DataTable dt = new DataTable();
                        dt = ClsUploadData.GetSearchAllData(txtFileBarcode.Text.Trim());

                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                string IWDate = dt.Rows[0]["Inward_Date"].ToString();

                                if (IWDate == "")
                                {

                                    Data[0] = ddlBranch.SelectedValue;
                                    //Data[0] = txtLocation.Text.Trim();
                                    Data[1] = txtPODNumber.Text.Trim();
                                    Data[2] = ddlCourierName.SelectedValue;
                                    Data[3] = txtReceivedDate.Text.Trim();
                                    Data[4] = txtRemark.Text.Trim();

                                    string res = ClsUploadData.UpdateInwardDate(txtFileBarcode.Text.Trim(), objuser.UserId, Data);

                                    if (res != "")
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + res + "');", true);
                                        txtFileBarcode.Text = string.Empty;
                                        txtFileBarcode.Focus();

                                        DataTable dtData = ClsUploadData.GetInwardData(objuser.UserId);
                                        if (dtData != null && dtData.Rows.Count > 0)
                                        {
                                            gvInward.DataSource = dtData;
                                            gvInward.DataBind();
                                        }
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('File already inwared');", true);
                                }
                            }
                            else
                            {
                                Data[0] = ddlBranch.SelectedValue;
                                //Data[0] = txtLocation.Text.Trim();
                                Data[1] = txtPODNumber.Text.Trim();
                                Data[2] = ddlCourierName.SelectedValue;
                                Data[3] = txtReceivedDate.Text.Trim();
                                Data[4] = txtRemark.Text.Trim();

                                string res = ClsUploadData.InsertInwardFile(txtFileBarcode.Text.Trim(), objuser.UserId, Data);
                                if (res != "")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('No PDD data Found, " + res + "');", true);
                                }
                                txtFileBarcode.Text = string.Empty;
                                txtFileBarcode.Focus();

                            }
                        }
                        else
                        {
                            Data[0] = ddlBranch.SelectedValue;
                            //Data[0] = txtLocation.Text.Trim();
                            Data[1] = txtPODNumber.Text.Trim();
                            Data[2] = ddlCourierName.SelectedValue;
                            Data[3] = txtReceivedDate.Text.Trim();
                            Data[4] = txtRemark.Text.Trim();

                            string res = ClsUploadData.InsertInwardFile(txtFileBarcode.Text.Trim(), objuser.UserId, Data);
                            if (res != "")
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('No PDD data Found, " + res + "');", true);
                            }
                            txtFileBarcode.Text = string.Empty;
                            txtFileBarcode.Focus();

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }
    }
}