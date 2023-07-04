using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;

namespace JLG.Forms
{
    public partial class frmPurgeMISReport : System.Web.UI.Page
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
                    BindGridData();
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
                
                DataTable dt = new DataTable();
                if (DateTime.Now.AddMonths(-6) > Convert.ToDateTime(txtFormDate.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('From date should not older than 6 months from current date.');", true);
                    return;

                }
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


                dt = ClsUploadData.GetMISReportPurgeData(txtFormDate.Text.Trim(), txtToDate.Text.Trim());

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

                dt = ClsUploadData.GetMISReportPurgeData(txtFormDate.Text.Trim(), txtToDate.Text.Trim());

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        //string fname = "MISReport.xls";

                        //ExportToExcel(dt, fname);
                        string fname = "PurgedMISReport.csv";

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

        void ExportToCSV(DataTable searchResult, string filename)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition","attachment;filename="+filename);
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

        private void BindGridData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ClsUploadData.GetMISReportPurgeData(txtFormDate.Text.Trim(), txtToDate.Text.Trim());

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

    }
}