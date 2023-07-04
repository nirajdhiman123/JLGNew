using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
namespace JLG.Forms
{
    public partial class frmParentChildSheet : System.Web.UI.Page
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
                    //txtFormDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    //txtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    //BindGridData();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }

        }

        private void BindGridData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ClsUploadData.GetParentChildSheetData(txtFormDate.Text.Trim(), txtToDate.Text.Trim());

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        gvData.DataSource = dt;
                        gvData.DataBind();

                    }
                    else
                    {
                        gvData.DataSource = null;
                        gvData.DataBind();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('No Record Found');", true);
                    }
                }
                else
                {
                    gvData.DataSource = null;
                    gvData.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('No Record Found');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                if (ddlType.SelectedValue == "0")
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please select report type.');", true);
                    return;
                }
                    if (ddlType.SelectedValue == "2")
                {

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

                }
                dt = ClsUploadData.GetParentChildSheetData(txtFormDate.Text.Trim(), txtToDate.Text.Trim());

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        gvData.DataSource = dt;
                        gvData.DataBind();

                    }
                    else
                    {
                        gvData.DataSource = null;
                        gvData.DataBind();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('No Record Found');", true);
                    }
                }
                else
                {
                    gvData.DataSource = null;
                    gvData.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('No Record Found');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        private void clearAll()
        {
            txtFormDate.Text = "";
            txtToDate.Text = "";
            
            gvData.DataSource = null;
            gvData.DataBind();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {

                clearAll();
                ddlType.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                if (ddlType.SelectedValue == "2")
                {

                
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
                }

                dt = ClsUploadData.GetParentChildSheetData(txtFormDate.Text.Trim(), txtToDate.Text.Trim());

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string fname = "ParentchildSheet.xls";

                        //ExportToExcel(dt, fname);
                        ExportToCSV(dt, fname);
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


        void ExportToCSV(DataTable searchResult, string filename)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            Response.Charset = "";
            Response.ContentType = "application/text";

            StringBuilder sb = new StringBuilder();

            for (int k = 0; k < searchResult.Columns.Count; k++)
            {
                //add separator
                sb.Append(searchResult.Columns[k].ColumnName + '|');
            }

            //append new line

            sb.Append("\r\n");

            for (int i = 0; i < searchResult.Rows.Count; i++)
            {
                for (int k = 0; k < searchResult.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(searchResult.Rows[i][k].ToString().Replace(",", ";") + '|');
                }

                //append new line
                sb.Append("\r\n");
            }

            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearAll();

            if (ddlType.SelectedValue == "1") //All
            {
                txtFormDate.Text = string.Empty;
                txtToDate.Text = string.Empty;
                txtFormDate.Enabled = false;
                txtToDate.Enabled = false;
            }
            else if (ddlType.SelectedValue == "2") //Selected
            {
                
                txtFormDate.Enabled = true;
                txtToDate.Enabled = true;
            }
            else  // --select--
            {
            }
        }
    }
}