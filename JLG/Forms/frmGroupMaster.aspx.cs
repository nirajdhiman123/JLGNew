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
    public partial class frmGroupMaster : System.Web.UI.Page
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

                    //GetID();
                    string status;
                    DataTable dt = new DataTable();
                    if (chkIsActive.Checked == true)
                    {
                        status = "Y";
                    }
                    else
                    {
                        status = "N";
                    }
                    dt = CommonData.GetGroupUser();
                    gvUserDetails.DataSource = dt;
                    gvUserDetails.DataBind();
                    //DisplayData();
                    GetMenu();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        private void GetMenu()
        {
            try
            {
                //ClsUser objuser = new ClsUser();

                DataTable dtMenu = new DataTable();
                dtMenu = CommonData.GetMenu();

                if (dtMenu.Rows.Count > 0)
                {
                    DataView view = new DataView(dtMenu);
                    view.RowFilter = "Parent_Id='0'";

                    foreach (DataRowView row in view)
                    {
                        TreeNode menuItem = new TreeNode
                        {

                            Value = row["Menu_Id"].ToString(),
                            Text = row["Menu_Name"].ToString(),
                            //ImageUrl = row["Img_Url"].ToString(),
                            //NavigateUrl = row["Menu_Url"].ToString()
                            //Selected = row["Url"].ToString().EndsWith(currentPage, StringComparison.CurrentCultureIgnoreCase)
                        };
                        tvMenu.Nodes.Add(menuItem);
                        GetSubMenu(dtMenu, menuItem);
                    }
                }

            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        private void GetSubMenu(DataTable dtSubMenu, TreeNode menuItem)
        {
            try
            {
                DataView viewItem = new DataView(dtSubMenu);
                viewItem.RowFilter = "Parent_Id='" + menuItem.Value + "'";

                foreach (DataRowView childrow in viewItem)
                {
                    TreeNode childmenuItem = new TreeNode
                    {
                        Value = childrow["Menu_Id"].ToString(),
                        Text = childrow["Menu_Name"].ToString(),
                        //ImageUrl = childrow["Img_Url"].ToString(),
                        //NavigateUrl = childrow["Menu_Url"].ToString()
                    };
                    menuItem.ChildNodes.Add(childmenuItem);
                    GetSubMenu(dtSubMenu, childmenuItem);
                }


            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        string UserIDNew, GroupID, MenuID, GroupName, IsActive;
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtGroupName.Text != "")
                {
                    bool chkmenu = false;
                    foreach (TreeNode tnode in tvMenu.Nodes)
                    {
                        if (tnode.Checked == true)
                        {
                            chkmenu = true;
                        }
                        foreach (TreeNode childnode in tnode.ChildNodes)
                        {
                            if (childnode.Checked == true)
                            {
                                chkmenu = true;
                            }
                        }
                    }
                    if (chkmenu == true)
                    {
                        if (hdnEditId.Value == null)
                        {
                            GroupName = txtGroupName.Text;
                            if (chkIsActive.Checked == true)
                            {
                                IsActive = "Y";
                            }
                            else
                            {
                                IsActive = "N";
                            }
                            string res = CommonData.InsertGroup(GroupName, IsActive);
                            hdnEditId.Value = res;

                        }

                        string res1 = CommonData.DeleteGroupUser(hdnEditId.Value);
                        foreach (TreeNode tnode in tvMenu.Nodes)
                        {
                            if (tnode.Checked)
                            {
                                MenuID = tnode.Value;
                                string res = CommonData.InsertGroupUser(MenuID, hdnEditId.Value);
                            }
                            foreach (TreeNode childnode in tnode.ChildNodes)
                            {
                                if (childnode.Checked)
                                {
                                    MenuID = childnode.Value;
                                    string res = CommonData.InsertGroupUser(MenuID, hdnEditId.Value);
                                }
                            }
                        }
                    }
                    else
                    {
                        string res1 = CommonData.DeleteGroupUser(hdnEditId.Value);
                    }

                    btnCancel_Click(null, null);
                    DataTable dt = new DataTable();
                    dt = CommonData.GetGroupUser();
                    gvUserDetails.DataSource = dt;
                    gvUserDetails.DataBind();
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
                txtGroupName.Text = "";
                chkIsActive.Checked = false;
                EditID = string.Empty;
                hdnEditId.Value = string.Empty;
                ClsUser objuser = new ClsUser();
                foreach (TreeNode tnode in tvMenu.Nodes)
                {
                    tnode.Checked = false;

                    foreach (TreeNode childnode in tnode.ChildNodes)
                    {
                        childnode.Checked = false;
                    }
                }
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
                foreach (TreeNode tnode in tvMenu.Nodes)
                {
                    tnode.Checked = false;

                    foreach (TreeNode childnode in tnode.ChildNodes)
                    {
                        childnode.Checked = false;
                    }
                }


                DataTable dtTable = CommonData.DisplayGroupUser(hdnEditId.Value);
                txtGroupName.Text = dtTable.Rows[0]["GroupName"].ToString();
                if (dtTable.Rows[0]["IsActive"].ToString() == "Y")
                {
                    chkIsActive.Checked = true;
                }
                else
                {
                    chkIsActive.Checked = false;
                }
                if (dtTable != null)
                {
                    if (dtTable.Rows.Count > 0)
                    {


                        foreach (DataRow row in dtTable.Rows)
                        {
                            foreach (TreeNode tnode in tvMenu.Nodes)
                            {
                                if (row["MenuId"].ToString() == tnode.Value)
                                {
                                    tnode.Checked = true;
                                }

                                foreach (TreeNode childnode in tnode.ChildNodes)
                                {
                                    if (row["MenuId"].ToString() == childnode.Value)
                                    {
                                        childnode.Checked = true;
                                    }

                                }
                            }
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