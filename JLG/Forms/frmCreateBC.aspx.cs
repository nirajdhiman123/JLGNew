using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data;

namespace JLG.Forms
{
    public partial class frmCreateBC : System.Web.UI.Page
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
                        dt = CommonData.GetBC(0,"False");
                        gvUserDetails.DataSource = dt;
                        gvUserDetails.DataBind();
                        
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
                       
                                GroupName = txtGroupName.Text;
                                if (chkIsActive.Checked == true)
                                {
                                    IsActive = "Y";
                                }
                                else
                                {
                                    IsActive = "N";
                                }
                                string res = CommonData.InsertBC(GroupName, IsActive, Convert.ToInt32(hdnEditId.Value == "" ? "0" : hdnEditId.Value));
                                hdnEditId.Value = res;

                        btnCancel_Click(null, null);
                        DataTable dt = new DataTable();
                        dt = CommonData.GetBC(0,"False");
                        gvUserDetails.DataSource = dt;
                        gvUserDetails.DataBind();
                    }
                    else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please fill Group Name.');", true);
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
                    //foreach (TreeNode tnode in tvMenu.Nodes)
                    //{
                    //    tnode.Checked = false;

                    //    foreach (TreeNode childnode in tnode.ChildNodes)
                    //    {
                    //        childnode.Checked = false;
                    //    }
                    //}
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
                   
                    DataTable dtTable = CommonData.GetBC(Convert.ToInt32(hdnEditId.Value),"False");
                    txtGroupName.Text = dtTable.Rows[0]["BC_Name"].ToString();
                    if (dtTable.Rows[0]["IsActive"].ToString() == "Y")
                    {
                        chkIsActive.Checked = true;
                    }
                    else
                    {
                        chkIsActive.Checked = false;
                    }
                    
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
                }
            }
        }
    }