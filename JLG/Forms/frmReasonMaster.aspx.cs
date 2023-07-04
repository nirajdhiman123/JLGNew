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
    public partial class frmReasonMaster : System.Web.UI.Page
    {
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

                    DataTable dt = new DataTable();
                    dt = CommonData.GetReasonData("");
                    gvRejectReason.DataSource = dt;
                    gvRejectReason.DataBind();
                    //DisplayData();

                    DataTable dt1 = CommonData.GetKeyValues("Reject_Reason", "");
                    ddlDocumentRelated.DataSource = dt1;
                    CommonDB.FillDropDown(ddlDocumentRelated, dt1, "key_name", "key_name");

                    txtDocumentRelated.Visible = true;
                    ddlDocumentRelated.Visible = false;

                }
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
                string docname = string.Empty;

                ClsUser objuser = new ClsUser();
                objuser = (ClsUser)Session["objUser"];

                if (rdnReportType.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please select the type');", true);
                    return;
                }

                if (rdnReportType.SelectedValue == "D")
                {
                    if (txtDocumentRelated.Text.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Document Name can not be blank');", true);
                        return;
                    }

                    if (txtReason.Text.Trim() == "" && hdnEditId.Value == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert(Reason remark can not be blank');", true);
                        return;
                    }
                }
                else
                {
                    if (ddlDocumentRelated.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please select the document type');", true);
                        return;
                    }

                    if (txtReason.Text.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Reason remark can not be blank');", true);
                        return;
                    }
                }

                if (rdnReportType.SelectedValue == "D")
                {
                    docname = txtDocumentRelated.Text.Trim();
                }
                else
                {
                    docname = ddlDocumentRelated.SelectedValue;
                }

                string isactive = "";
                if (chkIsActive.Checked == true)
                {
                    isactive = "Y";
                }
                else
                {
                    isactive = "N";
                }

                string res = CommonData.InsertReason(rdnReportType.SelectedValue, docname, txtReason.Text.Trim(), isactive, hdnEditId.Value, objuser.UserId);

                if (res != "")
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Message", "alert('" + res + "');", true);
                    btnCancel_Click(null, null);

                }

                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", res, true);
                DataTable dt = new DataTable();
                dt = CommonData.GetReasonData("");
                gvRejectReason.DataSource = dt;
                gvRejectReason.DataBind();

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

                txtReason.Text = string.Empty;
                txtDocumentRelated.Text = string.Empty;
                ddlDocumentRelated.SelectedIndex = 0;
                hdnEditId.Value = string.Empty;
                txtReason.Enabled = true;
                txtDocumentRelated.Enabled = true;
                ddlDocumentRelated.Enabled = true;

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
                if (rdnReportType.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please select the type');", true);
                    return;
                }

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
                DataTable dtTable = CommonData.GetReasonData(hdnEditId.Value);

                if (dtTable != null)
                {
                    if (dtTable.Rows.Count > 0)
                    {
                        if (rdnReportType.SelectedValue == "D")
                        {
                            txtDocumentRelated.Text = dtTable.Rows[0]["key_name"].ToString();
                            txtReason.Enabled = false;
                            //txtReason.Text = dtTable.Rows[0]["key_value"].ToString();
                        }
                        else
                        {
                            ddlDocumentRelated.SelectedValue = dtTable.Rows[0]["key_name"].ToString();
                            ddlDocumentRelated.Enabled = false;
                            txtReason.Text = dtTable.Rows[0]["key_value"].ToString();
                        }


                        if (dtTable.Rows[0]["key_active"].ToString() == "Y")
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

        protected void rdnReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdnReportType.SelectedValue == "D")
                {
                    txtDocumentRelated.Visible = true;
                    ddlDocumentRelated.Visible = false;     
                }
                else
                {
                    txtDocumentRelated.Visible = false;
                    ddlDocumentRelated.Visible = true;
                }

                btnCancel_Click(null, null);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        protected void gvRejectReason_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvRejectReason.PageIndex = e.NewPageIndex;

                DataTable dt = new DataTable();
                dt = CommonData.GetReasonData("");
                gvRejectReason.DataSource = dt;
                gvRejectReason.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
          
        }
    }
}