using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JLG.Forms
{
    public partial class frmProfilePage : System.Web.UI.Page
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

                    DisplayData();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Forms/frmHome.aspx");
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
                objuser = (ClsUser)Session["objUser"];

                DataTable dtTable = CommonData.UserDisplayProfileData(objuser.UserId);

                if (dtTable != null)
                {
                    if (dtTable.Rows.Count > 0)
                    {
                        txtUserType.Text = dtTable.Rows[0]["UserType"].ToString();
                        txtUserName.Text = dtTable.Rows[0]["Login_Text"].ToString();
                        txtGroup.Text = dtTable.Rows[0]["GroupName"].ToString();
                        txtBranch.Text = dtTable.Rows[0]["Branch_Name"].ToString();
                        txtFirstName.Text = dtTable.Rows[0]["First_Name"].ToString();
                        txtLastName.Text = dtTable.Rows[0]["Last_Name"].ToString();
                        txtEmpCode.Text = dtTable.Rows[0]["EmpCode"].ToString();
                        txtEmailId.Text = dtTable.Rows[0]["EmailId"].ToString();
                        txtMobileNo.Text = dtTable.Rows[0]["MobileNo"].ToString();

                        GetMenu(Convert.ToInt32(dtTable.Rows[0]["GroupID"].ToString()));

                    }
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }
        private void GetMenu(int groupId)
        {
            try
            {
                //ClsUser objuser = new ClsUser();

                DataTable dtMenu = new DataTable();
                dtMenu = CommonData.GetProfileMenu(groupId);

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

    }
}