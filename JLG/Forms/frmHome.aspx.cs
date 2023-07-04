using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JLG.Forms
{
    public partial class frmHome : System.Web.UI.Page
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
                    //string script = "$(document).ready(function () { $('[id*=btnSubmit]').click(); });";
                    //ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);

                    //txtFormDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    //txtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    btnRefresh_Click(null, null);

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
                System.Threading.Thread.Sleep(5000);

                DataTable dt = new DataTable();
                if (txtFormDate.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('From date can not be blank');", true);
                    return;
                }

                if (txtToDate.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('To date can not be blank');", true);
                    return;
                }

                if (Convert.ToDateTime(txtFormDate.Text.Trim()) > Convert.ToDateTime(txtToDate.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('From date can not grater than To date ');", true);
                    return;
                }

                if (Convert.ToDateTime(txtToDate.Text.Trim()) > Convert.ToDateTime(DateTime.Now))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('To date can not grater than Current date ');", true);
                    return;
                }


                dt = ClsUploadData.GetDashboardData(txtFormDate.Text.Trim(), txtToDate.Text.Trim());

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        gvData.DataSource = dt;
                        gvData.DataBind();

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('No Record Found');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('No Record Found');", true);
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
                txtFormDate.Text = "";
                txtToDate.Text = "";
                gvData.DataSource = null;
                gvData.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(5000);

                DataTable dt = new DataTable();

                dt = ClsUploadData.GetDashboardData("", "");

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        gvData.DataSource = dt;
                        gvData.DataBind();

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('No Record Found');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('No Record Found');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }
    }
}