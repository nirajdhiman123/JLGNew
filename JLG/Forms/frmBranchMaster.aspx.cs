using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.IO;

namespace JLG.Forms
{
    public partial class frmBranchMaster : System.Web.UI.Page
    {
        static string EditID;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["LoginID"] == null)
                    {
                        Response.Redirect("../frmLogin.aspx?msg=Session time out.", false);
                        return;
                    }

                    BindLocation();

                    DataTable dt = new DataTable();

                    dt = CommonData.GetBranchData(0);
                    gvBranch.DataSource = dt;
                    gvBranch.DataBind();
                    //DisplayData();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        private void BindLocation()
        {
            try
            {
                DataTable dt = CommonData.GetLocationData(0);
                ddlLocation.DataSource = dt;
                ddlLocation.DataValueField = "LocationID";
                ddlLocation.DataTextField = "LocationName";
                ddlLocation.DataBind();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                ClsUser objuser = new ClsUser();
                objuser = (ClsUser)Session["objUser"];
              

                if (txtBranchName.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Branch Name can not be blank');", true);
                    return;
                }
                if (ddlLocation.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please select the Lacation');", true);
                    return;
                }


                ClsBranchData objBranch = new ClsBranchData();

                objBranch.BranchName = txtBranchName.Text.Trim();
                objBranch.LocId = Convert.ToInt32(ddlLocation.SelectedValue);
                objBranch.LocName = ddlLocation.SelectedItem.Text.Trim();
                objBranch.Address1 = txtAddress1.Text.Trim();
                objBranch.Address2 = txtAddress2.Text.Trim();
                objBranch.Address3 = txtAddress3.Text.Trim();
                objBranch.Address4 = txtAddress4.Text.Trim();
                objBranch.Pincode = txtPincode.Text.Trim();
                objBranch.City = txtCity.Text.Trim();
                objBranch.State = txtState.Text.Trim();
                objBranch.EmailId = txtEmailId.Text.Trim();
                objBranch.PhoneNo = txtPhoneNo.Text.Trim();

                if (chkIsActive.Checked == true)
                {
                    objBranch.IsActive = "Y";
                }
                else
                {
                    objBranch.IsActive = "N";
                }

                string res = CommonData.InsertBranch(objBranch, Convert.ToInt32(hdnEditId.Value == "" ? "0" : hdnEditId.Value), objuser.UserId);

                if (res != "")
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Message", "alert('" + res + "');", true);
                    btnCancel_Click(null, null);

                }

                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", res, true);
                DataTable dt = new DataTable();
                dt = CommonData.GetBranchData(0);
                gvBranch.DataSource = dt;
                gvBranch.DataBind();

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
                ddlLocation.SelectedIndex = 0;
                txtBranchName.Text = string.Empty;
                txtEmailId.Text = string.Empty;
                txtPhoneNo.Text = string.Empty;
                txtAddress1.Text = string.Empty;
                txtAddress2.Text = string.Empty;
                txtAddress3.Text = string.Empty;
                txtAddress4.Text = string.Empty;
                txtPincode.Text = string.Empty;
                txtCity.Text = string.Empty;
                txtState.Text = string.Empty;
                hdnEditId.Value = string.Empty;

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
                ImageButton btnEdit = (ImageButton)sender;
                GridViewRow Grow = (GridViewRow)btnEdit.NamingContainer;
                ImageButton imgEdit = (ImageButton)Grow.FindControl("imgEdit");
                //EditID = imgEdit.ToolTip;
                hdnEditId.Value = imgEdit.ToolTip;
                DisplayData();

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
                DataTable dtTable = CommonData.GetBranchData(Convert.ToInt32(hdnEditId.Value));

                if (dtTable != null)
                {
                    if (dtTable.Rows.Count > 0)
                    {
                        ddlLocation.SelectedValue = dtTable.Rows[0]["LocationID"].ToString();
                        txtBranchName.Text = dtTable.Rows[0]["Branch_Name"].ToString();
                        txtEmailId.Text = dtTable.Rows[0]["EmailID"].ToString();
                        txtPhoneNo.Text = dtTable.Rows[0]["PhoneNo"].ToString();
                        txtAddress1.Text = dtTable.Rows[0]["Address1"].ToString();
                        txtAddress2.Text = dtTable.Rows[0]["Address2"].ToString();
                        txtAddress3.Text = dtTable.Rows[0]["Address3"].ToString();
                        txtAddress4.Text = dtTable.Rows[0]["Address4"].ToString();
                        txtPincode.Text = dtTable.Rows[0]["PinCode"].ToString();
                        txtCity.Text = dtTable.Rows[0]["City"].ToString();
                        txtState.Text = dtTable.Rows[0]["State"].ToString();

                        if (dtTable.Rows[0]["IsActive"].ToString() == "Y")
                        {
                            chkIsActive.Checked = true;
                        }
                        else
                        {
                            chkIsActive.Checked = false;
                        }
                    }
                }


            }
            catch (System.Threading.ThreadAbortException) { }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }
    }
}