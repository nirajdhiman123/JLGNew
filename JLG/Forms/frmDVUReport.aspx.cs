using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JLG.Forms
{
    public partial class frmDVUReport : System.Web.UI.Page
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
                    txtFormDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    txtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

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

                if (rdnReportType.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please select report type');", true);
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

                BindGridData();
                
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
                rdnReportType.SelectedIndex = -1;
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

                if (rdnReportType.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please select report type');", true);
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

                dt = ClsUploadData.GetReportData(txtFormDate.Text.Trim(), txtToDate.Text.Trim(), "DVU", rdnReportType.SelectedValue.ToString());

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string fname = string.Empty;

                        if (rdnReportType.SelectedValue == "S")
                        {
                            fname = "PDD DVU Success Report.xls";
                        }
                        else
                        {
                            fname = "PDD DVU Reject Report.xls";
                        }

                        ExportToExcel(dt, fname);
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

        void ExportToExcel(DataTable searchResult, string filename)
        {
            DataRow row;
            //searchResult.Columns.Remove("FilePath");
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=" + filename);
            Response.Write("<table border=1>");
            Response.Write("<thead>");
            Response.Write("<tr style='font-weight:bold'>");
            foreach (DataColumn clmn in searchResult.Columns)
            {
                Response.Write("<td align=center bgcolor=#FF0000><font color=#FFFFFF>" + clmn.ColumnName.ToString() + "</font></td>");
            }
            Response.Write("</tr>");
            Response.Write("</thead>");

            for (int i = 0; i < searchResult.Rows.Count; i++)
            {
                row = searchResult.Rows[i];

                Response.Write("<tr>");
                foreach (DataColumn clmn in searchResult.Columns)
                {
                    Response.Write("<td>" + row[clmn.ColumnName.ToString()].ToString() + "</td>");
                }
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.End();
        }

        private void BindGridData()
        {
            try
            {
                DataTable dt = new DataTable();

                dt = ClsUploadData.GetReportData(txtFormDate.Text.Trim(), txtToDate.Text.Trim(), "DVU", rdnReportType.SelectedValue.ToString());

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
    }
}