using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JLG.Forms
{
    public partial class frmBCMailList : System.Web.UI.Page
    {
        static string EditID;
        DataRow dr;
        DataTable DTToGrid;
        DataTable DTBCCGrid;
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

                    //GetID();
                    string status;
                    DataTable dt = new DataTable();

                    dt = CommonData.GetBC(0,"True");
                    dr = dt.NewRow();
                    dr[1] = "--Select --";
                    dt.Rows.InsertAt(dr, 0);
                    ddlGroup.DataValueField = "BC_Id";
                    ddlGroup.DataTextField = "BC_Name";
                    ddlGroup.DataSource = dt;
                    ddlGroup.DataBind();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }


        string UserIDNew, GroupID, MenuID, GroupName, IsActive;

        /// <summary>
        /// Create table to Temporary stored TO Mail List
        /// </summary>
        /// <returns></returns>
        private DataTable CreateToMailGridData()
        {
            DTToGrid = new DataTable();

            DTToGrid.Columns.Add("ID", typeof(int));
            DTToGrid.Columns.Add("ToMail", typeof(string));
            ViewState["dtTo"] = DTToGrid;
            return ViewState["dtTo"] as DataTable;
        }

        /// <summary>
        ///  Create table to Temporary stored BCC Mail List
        ///  /// </summary>
        /// <returns></returns>
        private DataTable CreateBCCMailGridData()
        {
            DTBCCGrid = new DataTable();

            DTBCCGrid.Columns.Add("BCCID", typeof(int));
            DTBCCGrid.Columns.Add("BCCMail", typeof(string));
            ViewState["dtBCC"] = DTBCCGrid;
            return ViewState["dtBCC"] as DataTable;
        }

        /// <summary>
        /// This event fetch data for selected group
        /// and bind Comma seperated mail list to grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strMails;
            DataTable dt = new DataTable();
            int srno;
            DTToGrid = CreateToMailGridData();
            DTBCCGrid = CreateBCCMailGridData();

            try
            {
                dt = CommonData.GetMailerInfo(ddlGroup.SelectedItem.Text);


                if (dt.Rows.Count > 0)
                {
                    txtBCCode.Text = (string)dt.Rows[0]["PDD_BC_Code"];

                    strMails = (string)dt.Rows[0]["PDD_BC_To_Email"];
                    srno = 1;

                    //Loop to Convert Comma sepeared List to Table
                    // Change "," to ";"
                    foreach (var item in strMails.Split(';'))
                    {
                        dr = DTToGrid.NewRow();
                        dr[0] = srno;
                        dr[1] = item;
                        DTToGrid.Rows.Add(dr);

                        srno = srno + 1;
                    }
                    ViewState["dtTo"] = DTToGrid;
                    BindGrid();

                    strMails = (string)dt.Rows[0]["PDD_BC_BCC_Email"];

                    srno = 1;
                    foreach (var item in strMails.Split(';'))
                    {
                        dr = DTBCCGrid.NewRow();
                        dr[0] = srno;
                        dr[1] = item;
                        DTBCCGrid.Rows.Add(dr);

                        srno = srno + 1;
                    }
                    ViewState["dtBCC"] = DTBCCGrid;
                    BindBCCGrid();

                }
                else
                {
                    txtBCCode.Text = "";
                    txtToMail.Text = "";
                    txtBCCMail.Text = "";
                    ViewState["dtTo"] = CreateToMailGridData();
                    this.DataBind();

                    ViewState["dtBCC"] = CreateBCCMailGridData();
                    this.BindBCCGrid();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);

            }


        }

        /// <summary>
        /// Validate Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ClsBCMailList objMail = new ClsBCMailList();

            try
            {

                //if (txtToMail.Text.Trim() == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('To Mail Id can not be blank');", true);
                //    return;
                //}

                //Validate To Mail ID
                //var query = from val in txtToMail.Text.Split(',')
                //            select (val);
                //foreach (string strMail in query)
                //{
                //    if (strMail != "")
                //    {
                //        if (IsValidEmail(strMail.Trim()) == false)
                //        {
                //            //string script = "Hi";
                //            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script", script, false);

                //            ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Invalid mail id " + strMail + " found in To Mail List');", true);
                //            txtToMail.Focus();
                //            txtToMail.BorderColor = System.Drawing.Color.Red;
                //            return;
                //        }
                //    }
                //}


                ////Validate BCC Mail Id
                //var query1 = from val in txtBCCMail.Text.Split(',')
                //             select (val);
                //foreach (string strMail in query1)
                //{
                //    if (strMail != "")
                //    {
                //        if (IsValidEmail(strMail.Trim()) == false)
                //        {
                //            ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Invalid mail id " + strMail + " found in BCC Mail List');", true);
                //            txtBCCMail.Focus();
                //            txtBCCMail.BorderColor = System.Drawing.Color.Red;
                //            return;
                //        }
                //    }
                //}

                //if (txtToMail.Text != "")
                //{

                //loop on grid to get comma seperated value
                String strToMail = "";
                String strBCCMail = "";

                DTToGrid = ViewState["dtTo"] as DataTable;
                foreach (DataRow item in DTToGrid.Rows)
                {
                    if (item["ToMail"].ToString().Trim() != "")
                    {
                        strToMail = strToMail + item["ToMail"].ToString() + ";";
                    }

                }

                DTBCCGrid = ViewState["dtBCC"] as DataTable;
                foreach (DataRow item in DTBCCGrid.Rows)
                {
                    if (item["BCCMail"].ToString().Trim() != "")
                    {
                        strBCCMail = strBCCMail + item["BCCMail"].ToString() + ";";
                    }

                }

                if (strToMail.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('To Mail Id can not be blank');", true);
                    return;
                }

                objMail.PDD_BC_Name = ddlGroup.SelectedItem.Text;
                objMail.PDD_BC_Code = txtBCCode.Text;

                if (strToMail.Length > 0)
                {
                    objMail.PDD_BC_To_Email = strToMail.Remove(strToMail.Length - 1);
                }
                else { objMail.PDD_BC_To_Email = string.Empty; }
                objMail.PDD_BC_To_Email = strToMail.Remove(strToMail.Length - 1);
                objMail.PDD_BC_CC_Email = string.Empty;

                if (strBCCMail.Length > 0)
                {
                    objMail.PDD_BC_BCC_Email = strBCCMail.Remove(strBCCMail.Length - 1);
                }
                else { objMail.PDD_BC_BCC_Email = string.Empty; }
                

                //if (chkIsActive.Checked)
                //{
                //    objMail.IsActive = "Y";
                //}
                //else { objMail.IsActive = "N"; }
                objMail.IsActive = "Y";
                objMail.Report_Type = "INV";

                string res = CommonData.InsertMailList(objMail);
                hdnEditId.Value = res;

                btnCancel_Click(null, null);

                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + res + "');", true);

                //}
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
                ddlGroup.SelectedIndex = 0;
                //chkIsActive.Checked = false;
                txtBCCode.Text = "";
                txtToMail.Text = "";
                txtBCCMail.Text = "";


                EditID = string.Empty;
                hdnEditId.Value = string.Empty;
                ClsBCMailList objMail = new ClsBCMailList();

                ViewState["dtTo"] = null;
                this.DataBind();

                ViewState["dtBCC"] = null;
                this.BindBCCGrid();

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

        protected void imgDelete_Click(object sender, ImageClickEventArgs e)
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


                DataTable dtTable = CommonData.GetBC(Convert.ToInt32(hdnEditId.Value),"True");
                //txtGroupName.Text = dtTable.Rows[0]["BC_Name"].ToString();


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }


        #region "To Mail Code"
        protected void BindGrid()
        {
            gvUserDetails.DataSource = ViewState["dtTo"] as DataTable;
            gvUserDetails.DataBind();
        }

        protected void btnToAdd_Click(object sender, EventArgs e)
        {
            String strToAdd;
            DTToGrid = ViewState["dtTo"] as DataTable;

            if (ddlGroup.SelectedItem.Text == "--Select --")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please select group.');", true);
                return;
            }



            strToAdd = txtToMail.Text;

            if (strToAdd != "")
            {
                if (DTToGrid != null)
                {


                    bool exists = DTToGrid.Select().ToList().Exists(row => row["ToMail"].ToString().ToUpper().Trim() == strToAdd.ToUpper().Trim());
                    if (exists)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Mail Id already Exists.');", true);
                        txtToMail.Text = string.Empty;
                        txtToMail.Focus();
                        return;
                    }
                }

                string max;
                if (DTToGrid != null)
                {
                    if (DTToGrid.Rows.Count == 0)
                    {
                        max = "0";
                    }
                    else { max = DTToGrid.AsEnumerable().Max(row => row["ID"]).ToString(); }
                }
                else { max = "0"; }

                if (IsValidEmail(strToAdd.Trim()) == false)
                {
                    //string script = "Hi";
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script", script, false);

                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Invalid mail Id.');", true);
                    txtToMail.Focus();
                    return;
                }
                else
                {
                    dr = DTToGrid.NewRow();

                    dr[0] = Convert.ToInt32(max) + 1;
                    dr[1] = strToAdd;

                    DTToGrid.Rows.Add(dr);
                    ViewState["DtTo"] = DTToGrid;
                    BindGrid();
                    txtToMail.Text = "";
                }

            }

        }

        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvUserDetails.Rows[e.RowIndex];
            //int customerId = Convert.ToInt32(gvUserDetails.DataKeys[e.RowIndex].Values[0]);
            //int customerId = Convert.ToInt32(gvUserDetails.DataKeys[e.RowIndex].Values[0]);
            int intID = Convert.ToInt32((row.FindControl("lblId") as Label).Text);
            string strToMail = (row.FindControl("txtToMail") as TextBox).Text;

            DTToGrid = ViewState["dtTo"] as DataTable;

            if (IsValidEmail(strToMail.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Invalid mail Id.');", true);
                return;
            }
            else
            {
                DTToGrid.Select(string.Format("[ID] = '{0}'", intID)).ToList<DataRow>().ForEach(r => r["ToMail"] = strToMail);

                DTToGrid.AcceptChanges();

                ViewState["dtTo"] = DTToGrid;

                gvUserDetails.EditIndex = -1;
                this.BindGrid();
            }
        }

        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            gvUserDetails.EditIndex = -1;
            this.BindGrid();
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //int customerId = Convert.ToInt32(gvUserDetails.DataKeys[e.RowIndex].Values[0]);
            GridViewRow row = gvUserDetails.Rows[e.RowIndex];
            int intID = Convert.ToInt32((row.FindControl("lblId") as Label).Text);

            DataTable dt = ViewState["dtTo"] as DataTable;
            //dt.Rows[intID].Delete();

            dt.Select("ID=" + intID).ToList().ForEach(x => x.Delete());
            dt.AcceptChanges();

            ViewState["dtTo"] = dt;
            BindGrid();



        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvUserDetails.EditIndex)
            {
                (e.Row.Cells[2].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
            }
        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUserDetails.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }

        #endregion

        #region "BCC Mail Code"

        protected void btnBCCAdd_Click(object sender, EventArgs e)
        {
            String strToAdd;
            DTBCCGrid = ViewState["dtBCC"] as DataTable;

            if (ddlGroup.SelectedItem.Text == "--Select --")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please select group.');", true);
                return;
            }

            strToAdd = txtBCCMail.Text;

            if (strToAdd != "")
            {
                bool exists = DTBCCGrid.Select().ToList().Exists(row => row["BCCMail"].ToString().ToUpper().Trim() == strToAdd.ToUpper().Trim());
                if (exists)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Mail Id already Exists.');", true);
                    txtBCCMail.Text = string.Empty;
                    txtBCCMail.Focus();
                    return;
                }

                string max;
                if (DTBCCGrid.Rows.Count == 0)
                {
                    max = "0";
                }
                else { max = DTBCCGrid.AsEnumerable().Max(row => row["BCCID"]).ToString(); }

                if (IsValidEmail(strToAdd.Trim()) == false)
                {
                    //string script = "Hi";
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script", script, false);

                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Invalid mail Id.');", true);
                    txtBCCMail.Focus();
                    return;
                }
                else
                {
                    dr = DTBCCGrid.NewRow();

                    dr[0] = Convert.ToInt32(max) + 1;
                    dr[1] = strToAdd;

                    DTBCCGrid.Rows.Add(dr);
                    ViewState["DtBCC"] = DTBCCGrid;
                    BindBCCGrid();
                    txtBCCMail.Text = "";
                }

            }

        }
        protected void BindBCCGrid()
        {
            gvBCC.DataSource = ViewState["dtBCC"] as DataTable;
            gvBCC.DataBind();
        }

        protected void OnBCCRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvBCC.Rows[e.RowIndex];
            int intID = Convert.ToInt32((row.FindControl("lblBCCId") as Label).Text);
            string strToMail = (row.FindControl("txtBCCMail") as TextBox).Text;

            DTBCCGrid = ViewState["dtBCC"] as DataTable;

            if (IsValidEmail(strToMail.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Invalid mail Id.');", true);
                return;
            }
            else
            {
                DTBCCGrid.Select(string.Format("[BCCID] = '{0}'", intID)).ToList<DataRow>().ForEach(r => r["BCCMail"] = strToMail);

                DTBCCGrid.AcceptChanges();

                ViewState["dtBCC"] = DTBCCGrid;

                gvBCC.EditIndex = -1;
                this.BindBCCGrid();
            }


        }

        protected void OnBCCRowCancelingEdit(object sender, EventArgs e)
        {
            gvBCC.EditIndex = -1;
            this.BindBCCGrid();
        }

        protected void OnBCCRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //int customerId = Convert.ToInt32(gvUserDetails.DataKeys[e.RowIndex].Values[0]);
            GridViewRow row = gvBCC.Rows[e.RowIndex];
            int intID = Convert.ToInt32((row.FindControl("lblBCCId") as Label).Text);

            DataTable dt = ViewState["dtBCC"] as DataTable;
            //dt.Rows[intID].Delete();

            dt.Select("BCCID=" + intID).ToList().ForEach(x => x.Delete());
            dt.AcceptChanges();

            ViewState["dtBCC"] = dt;


            this.BindBCCGrid();
        }

        protected void OnBCCRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvBCC.EditIndex)
            {
                (e.Row.Cells[2].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
            }
        }

        protected void OnBCCRowEditing(object sender, GridViewEditEventArgs e)
        {
            gvBCC.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }

        #endregion    
    }
}