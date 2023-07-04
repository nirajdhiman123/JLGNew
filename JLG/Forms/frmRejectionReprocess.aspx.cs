using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JLG.Forms
{
    public partial class frmRejectionReprocess : System.Web.UI.Page
    {
        int DefaultThumbHieght = 100;
        int DefaultThumbWidth = 100;
        int DefaultBigHieght = 750;
        int DefaultBigWidth = 700;
        int PagerSize = 8;

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
                    btnSubmit.Enabled = false;
                }

                //if (hdnFilePath.Value != "")
                //{
                //    LoadFile(hdnFilePath.Value, hdnFileName.Value);
                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        protected void txtSearchBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                ClsUser objuser = new ClsUser();
                objuser = (ClsUser)Session["objUser"];

                if (txtSearchBarcode.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('barcode can not be blank');", true);
                    return;
                }

                dt = ClsUploadData.GetDVURejectedData(txtSearchBarcode.Text.Trim());

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            //if (dr["Status"].ToString().Trim() != "OK")
                            //{
                            //    if (dr["IsRescan"].ToString().Trim() == "N")
                            //    {
                            if (dr["IsLocked"].ToString().Trim() == "N")
                            {
                                txtBarcode.Text = dr["Barcode"].ToString();
                                txtLAN.Text = dr["LAN"].ToString();
                                //txtDisDate.Text = dr["Disbursement_Date"].ToString();
                                hdnProcessId.Value = dr["ProcessId"].ToString();
                                txtMemberName.Text = dr["Customer_Name"].ToString();
                                txtDisbursalAmt.Text = dr["Disbursed_Amount"].ToString();
                                txtDisbursementMode.Text = dr["Disbursement_Mode"].ToString();
                                hdnFileName.Value = dr["FileName"].ToString();
                                hdnFilePath.Value = dr["FilePath"].ToString();
                                txtGroupName.Text = dr["Group_Name"].ToString();
                                txtCenterName.Text = dr["Center_Name"].ToString();
                                txtRejectReason.Text = dr["RejectReason"].ToString();
                                txtRejectedDate.Text = dr["RejectDate"].ToString();
                                txtGroupCount.Text = dr["GroupCount"].ToString();

                                LoadFile(hdnFilePath.Value, hdnFilePath.Value);

                                string msg = ClsUploadData.UpdateFile_LockUnlock(Convert.ToInt32(hdnProcessId.Value), objuser.UserId, "Y");

                                btnSubmit.Enabled = true;
                                txtSearchLAN.Enabled = false;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Validation", "alert('Barcode : " + dr["Barcode"].ToString() + " is locked,used by another user.');", true);
                            }
                            //    }
                            //    else
                            //    {
                            //        if (dr["IsRescan"].ToString().Trim() == "Y")
                            //        {
                            //            ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Barcode : " + dr["Barcode"].ToString() + " is Rejected, New file not uploaded.');", true);
                            //        }
                            //        else
                            //        {
                            //            ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Barcode : " + dr["Barcode"].ToString() + " file not uploaded.');", true);
                            //        }
                            //    }

                            //}
                            //else
                            //{
                            //    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Data already verified');", true);
                            //}
                        }
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

        protected void txtSearchLAN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                ClsUser objuser = new ClsUser();
                objuser = (ClsUser)Session["objUser"];

                if (txtSearchLAN.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('LAN can not be blank');", true);
                    return;
                }

                dt = ClsUploadData.GetDVURejectedData(txtSearchLAN.Text.Trim());

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            //if (dr["Status"].ToString().Trim() != "OK")
                            //{
                            //    if (dr["IsRescan"].ToString().Trim() == "N")
                            //    {
                            if (dr["IsLocked"].ToString().Trim() == "N")
                            {
                                txtBarcode.Text = dr["Barcode"].ToString();
                                txtLAN.Text = dr["LAN"].ToString();
                                //txtDisDate.Text = dr["Disbursement_Date"].ToString();
                                hdnProcessId.Value = dr["ProcessId"].ToString();
                                txtMemberName.Text = dr["Customer_Name"].ToString();
                                txtDisbursalAmt.Text = dr["Disbursed_Amount"].ToString();
                                txtDisbursementMode.Text = dr["Disbursement_Mode"].ToString();
                                hdnFileName.Value = dr["FileName"].ToString();
                                hdnFilePath.Value = dr["FilePath"].ToString();
                                txtGroupName.Text = dr["Group_Name"].ToString();
                                txtCenterName.Text = dr["Center_Name"].ToString();
                                txtRejectReason.Text = dr["RejectReason"].ToString();
                                txtRejectedDate.Text = dr["RejectDate"].ToString();
                                txtGroupCount.Text = dr["GroupCount"].ToString();

                                LoadFile(hdnFilePath.Value, hdnFileName.Value);

                                string msg = ClsUploadData.UpdateFile_LockUnlock(Convert.ToInt32(hdnProcessId.Value), objuser.UserId, "Y");

                                btnSubmit.Enabled = true;
                                txtSearchBarcode.Enabled = false;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Validation", "alert('LAN : " + dr["LAN"].ToString() + " is locked,used by another user.');", true);
                            }

                            //    }
                            //    else
                            //    {
                            //        if (dr["IsRescan"].ToString().Trim() == "Y")
                            //        {
                            //            ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('LAN : " + dr["LAN"].ToString() + " is Rejected, New file not uploaded.');", true);
                            //        }
                            //        else
                            //        {
                            //            ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('LAN : " + dr["LAN"].ToString() + " file not uploaded.');", true);
                            //        }
                            //    }
                            //}
                            //else
                            //{
                            //    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Data already verified');", true);
                            //}
                        }
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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(700);

            try
            {
                string RejectReason = string.Empty;
                ClsUser objuser = new ClsUser();
                objuser = (ClsUser)Session["objUser"];

                if (ddlStatus.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please select status');", true);
                    return;
                }

                if (txtRemark.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please enter remark');", true);
                    return;
                }


                string[] objData = new string[4];

                string repro_rem = "";

                objData[0] = hdnProcessId.Value;
                objData[1] = ddlStatus.SelectedValue;

                repro_rem = txtRemark.Text.Trim();



                objData[2] = txtRemark.Text.Trim();

                string res = ClsUploadData.UpdateReprocessStatus(objData, objuser.UserId);

                if (res != "")
                {
                    string msg = ClsUploadData.UpdateFile_LockUnlock(0, objuser.UserId, "N");

                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + res + "');", true);
                    btnCancel_Click(null, null);
                    btnSubmit.Enabled = false;
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
                ClsUser objuser = new ClsUser();
                objuser = (ClsUser)Session["objUser"];

                hdnProcessId.Value = "";
                txtSearchBarcode.Enabled = true;
                txtSearchLAN.Enabled = true;
                txtSearchBarcode.Text = "";
                txtSearchLAN.Text = "";
                txtBarcode.Text = "";
                txtLAN.Text = "";
                txtDisbursalAmt.Text = "";
                txtDisbursementMode.Text = "";
                txtMemberName.Text = "";
                txtGroupName.Text = "";
                txtCenterName.Text = "";
                txtGroupCount.Text = "";
                txtRejectedDate.Text = "";
                txtRejectReason.Text = "";
                ddlStatus.SelectedIndex = 0;
                txtRemark.Text = "";
                hdnFileName.Value = "";
                hdnFilePath.Value = "";
                btnSubmit.Enabled = false;
                _plcBigImg.Controls.Clear();
                _plcImgsThumbs.Controls.Clear();
                _plcImgsThumbsPager.Controls.Clear();

                string msg = ClsUploadData.UpdateFile_LockUnlock(0, objuser.UserId, "N");

                ddlPages.Items.Clear();
                txtCurrentPage.Value = "";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }


        }

        private void LoadFile(string fPath, string filename)
        {
            try
            {
                ddlPages.Items.Clear();

                FilePath = fPath;

                int StartPg = 1;
                //if (Request.QueryString["StartPage"] != null)
                //    StartPg = System.Convert.ToInt16(Request.QueryString["StartPage"]);

                if (txtCurrentPage.Value != "")
                    StartPg = System.Convert.ToInt16(txtCurrentPage.Value);

                int BigImgPg = StartPg;
                int EndPg = StartPg + (PagerSize - 1);
                if (EndPg > TotalTIFPgs)
                    EndPg = TotalTIFPgs;

                //Add/configure the thumbnails
                while (StartPg <= EndPg)
                {
                    Label lbl = new Label();
                    if (StartPg % 4 == 0 && StartPg != 0) lbl.Text = StartPg.ToString() + "&nbsp;";
                    else lbl.Text = StartPg.ToString() + "&nbsp;";
                    Image Img = new Image();
                    Img.BorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), "Solid");
                    Img.BorderWidth = Unit.Parse("1");
                    //Img.Style["padding"] = "5px 5px 5px 0px";
                    Img.Attributes.Add("onclick", "ChangePg('" + StartPg.ToString() + "');");
                    Img.Attributes.Add("onmouseover", "this.style.cursor='pointer';");
                    Img.ImageUrl = "ViewImg.aspx?FilePath=" + FilePath + "&Pg=" + (StartPg).ToString() + "&Height=" + DefaultThumbHieght.ToString() + "&Width=" + DefaultThumbWidth;
                    _plcImgsThumbs.Controls.Add(Img);
                    _plcImgsThumbs.Controls.Add(lbl);
                    StartPg++;
                }

                //Bind big img
                Image BigImg = new Image();
                BigImg.BorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), "Solid");
                BigImg.BorderWidth = Unit.Parse("1");
                BigImg.ID = "_imgBig";
                BigImg.ImageUrl = "ViewImg.aspx?View=1&FilePath=" + FilePath + "&Pg=" + BigImgPg.ToString() + "&Height=" + DefaultBigHieght.ToString() + "&Width=" + DefaultBigWidth.ToString();
                _plcBigImg.Controls.Add(BigImg);

                if (txtCurrentPage.Value == "")
                {
                    txtCurrentPage.Value = "1";
                }

                //ConfigPagers
                //Config page 1 - whatever

                if ((TotalTIFPgs / PagerSize) >= 1)
                {
                    string _page = "";
                    if ((1 + PagerSize) > TotalTIFPgs)
                        _page = "1-" + TotalTIFPgs;
                    else
                        _page = "1-" + PagerSize;

                    ddlPages.Items.Add(new ListItem(_page, "1"));
                }

                for (int i = 1; i <= (TotalTIFPgs / PagerSize); i++)
                {
                    string _page = "";

                    if (i == (TotalTIFPgs / PagerSize))
                        _page = ((i * PagerSize) + 1).ToString() + "-" + TotalTIFPgs;
                    else
                        _page = ((i * PagerSize) + 1).ToString() + "-" + (((i + 1) * PagerSize)).ToString();

                    ddlPages.Items.Add(new ListItem(_page, ((i * PagerSize) + 1).ToString()));
                }

                //if ((TotalTIFPgs / PagerSize) >= 1)
                //{
                //    HyperLink _hl = new HyperLink();
                //    Label lbl = new Label(); lbl.Text = "&nbsp;";
                //    if (Request.Url.ToString().IndexOf("&StartPage=") >= 0)
                //        _hl.NavigateUrl = Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("&StartPage=")) + "&StartPage=1";
                //    else
                //        _hl.NavigateUrl = Request.Url.ToString() + "&StartPage=1";
                //    if ((1 + PagerSize) > TotalTIFPgs)
                //        _hl.Text = "1-" + TotalTIFPgs;
                //    else
                //        _hl.Text = "1-" + PagerSize;
                //    _plcImgsThumbsPager.Controls.Add(_hl);
                //    _plcImgsThumbsPager.Controls.Add(lbl);
                //}
                ////Config the rest of the page pagers
                //for (int i = 1; i <= (TotalTIFPgs / PagerSize); i++)
                //{
                //    HyperLink _hl = new HyperLink();
                //    Label lbl1 = new Label(); lbl1.Text = "&nbsp;";
                //    if (Request.Url.ToString().IndexOf("&StartPage=") >= 0)
                //        _hl.NavigateUrl = Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("&StartPage=")) + "&StartPage=" + ((i * PagerSize) + 1).ToString();
                //    else
                //        _hl.NavigateUrl = Request.Url.ToString() + "&StartPage=" + ((i * PagerSize) + 1).ToString();
                //    if (i == (TotalTIFPgs / PagerSize))
                //        _hl.Text = ((i * PagerSize) + 1).ToString() + "-" + TotalTIFPgs;
                //    else
                //        _hl.Text = ((i * PagerSize) + 1).ToString() + "-" + (((i + 1) * PagerSize)).ToString();
                //    _plcImgsThumbsPager.Controls.Add(_hl);
                //    _plcImgsThumbsPager.Controls.Add(lbl1);
                //}


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        public int TotalTIFPgs
        {
            get
            {
                if (ViewState["TotalTIFPgs"] == null)
                {
                    TIF TheFile = new TIF(FilePath);
                    ViewState["TotalTIFPgs"] = TheFile.PageCount;
                    TheFile.Dispose();
                }
                return System.Convert.ToInt16(ViewState["TotalTIFPgs"]);
            }
            set
            {
                ViewState["TotalTIFPgs"] = value;
            }
        }

        public String FilePath
        {
            get
            {
                if (ViewState["FilePath"] == null)
                {
                    return "";
                }
                return ViewState["FilePath"].ToString();
            }
            set
            {
                ViewState["FilePath"] = value;
            }
        }

        protected void ddlPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _plcImgsThumbs.Controls.Clear();
                _plcImgsThumbsPager.Controls.Clear();
                _plcBigImg.Controls.Clear();

                FilePath = hdnFilePath.Value;

                int StartPg = 1;
                //if (Request.QueryString["StartPage"] != null)
                //    StartPg = System.Convert.ToInt16(Request.QueryString["StartPage"]);


                StartPg = System.Convert.ToInt16(ddlPages.SelectedValue);
                txtCurrentPage.Value = ddlPages.SelectedValue;

                int BigImgPg = StartPg;
                int EndPg = StartPg + (PagerSize - 1);
                if (EndPg > TotalTIFPgs)
                    EndPg = TotalTIFPgs;

                //Add/configure the thumbnails
                while (StartPg <= EndPg)
                {
                    Label lbl = new Label();
                    if (StartPg % 4 == 0 && StartPg != 0) lbl.Text = StartPg.ToString() + "&nbsp;";
                    else lbl.Text = StartPg.ToString() + "&nbsp;";
                    Image Img = new Image();
                    Img.BorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), "Solid");
                    Img.BorderWidth = Unit.Parse("1");
                    // Img.Style["padding"] = "5px 5px 5px 0px";
                    Img.Attributes.Add("onclick", "ChangePg('" + StartPg.ToString() + "');");
                    Img.Attributes.Add("onmouseover", "this.style.cursor='pointer';");
                    Img.ImageUrl = "ViewImg.aspx?FilePath=" + FilePath + "&Pg=" + (StartPg).ToString() + "&Height=" + DefaultThumbHieght.ToString() + "&Width=" + DefaultThumbWidth;
                    _plcImgsThumbs.Controls.Add(Img);
                    _plcImgsThumbs.Controls.Add(lbl);
                    StartPg++;
                }

                //Bind big img
                Image BigImg = new Image();
                BigImg.BorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), "Solid");
                BigImg.BorderWidth = Unit.Parse("1");
                BigImg.ID = "_imgBig";
                BigImg.ImageUrl = "ViewImg.aspx?View=1&FilePath=" + FilePath + "&Pg=" + BigImgPg.ToString() + "&Height=" + DefaultBigHieght.ToString() + "&Width=" + DefaultBigWidth.ToString();
                _plcBigImg.Controls.Add(BigImg);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

    }
}