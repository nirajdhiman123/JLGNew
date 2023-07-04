using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace JLG.Forms
{
    public partial class frmGetMailList : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dt = CommonData.GetMailerInfo(string.Empty);
                ExportToExcel(dt, "MailList.xls");
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
    }
}