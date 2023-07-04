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
    public partial class frmLocationMaster : System.Web.UI.Page
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

                    DataTable dt = new DataTable();

                    dt = CommonData.GetLocationData(0);
                    gvLocation.DataSource = dt;
                    gvLocation.DataBind();
                    //DisplayData();

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
                ClsUser objuser = new ClsUser();
                objuser = (ClsUser)Session["objUser"];

                if (txtLocation.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Lacation can not be blank');", true);
                    return;
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

                string res = CommonData.InsertLocation(txtLocation.Text.Trim(), isactive, Convert.ToInt32(hdnEditId.Value == "" ? "0" : hdnEditId.Value), objuser.UserId);

                if (res != "")
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Message", "alert('" + res + "');", true);
                    btnCancel_Click(null, null);

                }

                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", res, true);
                DataTable dt = new DataTable();
                dt = CommonData.GetLocationData(0);
                gvLocation.DataSource = dt;
                gvLocation.DataBind();

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

                txtLocation.Text = string.Empty;
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
                DataTable dtTable = CommonData.GetLocationData(Convert.ToInt32(hdnEditId.Value));

                if (dtTable != null)
                {
                    if (dtTable.Rows.Count > 0)
                    {

                        txtLocation.Text = dtTable.Rows[0]["LocationName"].ToString();
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