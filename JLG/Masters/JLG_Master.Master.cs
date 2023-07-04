using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JLG.Masters
{
    public partial class JLG_Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GetMenu();
                    lblUserName.Text = Convert.ToString(Session["EmpName"]);
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
                dtMenu = CommonData.GetMenuRights(Convert.ToInt32(Session["GroupID"]));

                if (dtMenu.Rows.Count > 0)
                {
                    DataView view = new DataView(dtMenu);
                    view.RowFilter = "Parent_Id='0'";

                    foreach (DataRowView row in view)
                    {
                        MenuItem menuItem = new MenuItem
                        {
                            Value = row["Menu_Id"].ToString(),
                            Text = row["Menu_Name"].ToString(),
                            ImageUrl = row["Img_Url"].ToString(),
                            NavigateUrl = row["Menu_Url"].ToString(),
                            Target = row["Target"].ToString()

                            //Selected = row["Url"].ToString().EndsWith(currentPage, StringComparison.CurrentCultureIgnoreCase)
                        };
                        Menu1.Items.Add(menuItem);
                        GetSubMenu(dtMenu, menuItem);
                    }
                }

            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        private void GetSubMenu(DataTable dtSubMenu, MenuItem menuItem)
        {
            try
            {
                DataView viewItem = new DataView(dtSubMenu);
                viewItem.RowFilter = "Parent_Id='" + menuItem.Value + "'";

                foreach (DataRowView childrow in viewItem)
                {
                    MenuItem childmenuItem = new MenuItem
                    {
                        Value = childrow["Menu_Id"].ToString(),
                        Text = childrow["Menu_Name"].ToString(),
                        ImageUrl = childrow["Img_Url"].ToString(),
                        NavigateUrl = childrow["Menu_Url"].ToString(),
                        Target = childrow["Target"].ToString()
                    };
                    menuItem.ChildItems.Add(childmenuItem);
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