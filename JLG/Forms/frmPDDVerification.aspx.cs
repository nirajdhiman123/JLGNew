using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Configuration;

namespace JLG.Forms
{
    public partial class frmPDDVerification : System.Web.UI.Page
    {
        //int pageCount = 0;
        //System.Drawing.Image _image;

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
                    BindRejectReason();
                    divReason.Visible = false;
                    btnSubmit.Enabled = false;
                    ddlStatus.Enabled = false;
                    btnGetAutoBarcode.Enabled = true; // Added By Kapil Singhal on 12th Dec. 2022 
                    txtSearchBarcode.Enabled = false;
                    txtSearchLAN.Enabled = false;
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

        private void BindRejectReason()
        {
            try
            {
                DataTable dt = CommonData.GetKeyValues("Reject_Reason", "");
                //ddlRejectReason.DataSource = dt;
                //ddlRejectReason.DataValueField = "key_name";
                //ddlRejectReason.DataTextField = "key_name";
                //ddlRejectReason.DataBind();
                //CommonDB.FillDropDown(ddlRejectReason, dt, "key_name", "key_name");
                //CommonDB.FillListBox(lstRejectReason, dt, "key_id", "key_value");

                lstRejectReason.Items.Clear();

                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        lstRejectReason.Items.Add(new ListItem(row["key_value"].ToString(), row["key_id"].ToString()));
                    }
                }
                else
                {
                    lstRejectReason.DataSource = null;
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlStatus.SelectedValue != "OK")
                {
                    divReason.Visible = true;
                }
                else
                {
                    divReason.Visible = false;
                    foreach (ListItem item in lstRejectReason.Items)
                    {
                        item.Selected = false;
                    }
                    //ddlRejectReason.SelectedIndex = 0;
                }

                ddlPages_SelectedIndexChanged(null, null);
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

                txtCurrentPage.Value = "";

                if (txtSearchBarcode.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('barcode can not be blank');", true);
                    return;
                }

                dt = ClsUploadData.GetSearchData(txtSearchBarcode.Text.Trim());

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["Status"].ToString().Trim() != "OK")
                            {
                                if (dr["IsRescan"].ToString().Trim() == "N")
                                {
                                    if (dr["IsLocked"].ToString().Trim() == "N")
                                    {

                                        txtBarcode.Text = dr["Barcode"].ToString();
                                        txtLAN.Text = dr["LAN"].ToString();
                                        txtMemberName.Text = dr["Customer_Name"].ToString();
                                        txtDisbursalAmt.Text = dr["Disbursed_Amount"].ToString();
                                        txtDisbursementMode.Text = dr["Disbursement_Mode"].ToString();
                                        txtDisDate.Text = dr["Disbursement_Date"].ToString();
                                        hdnProcessId.Value = dr["ProcessId"].ToString();
                                        hdnFileName.Value = dr["FileName"].ToString();
                                        hdnFilePath.Value = dr["FilePath"].ToString();

                                        //string src = CommonDB.GetSrc("Handler") + hdnFilePath.Value + "#toolbar=0";
                                        //pdfFrame1.Attributes.Add("src", src);

                                        LoadFile(hdnFilePath.Value, hdnFilePath.Value);

                                        string msg = ClsUploadData.UpdateFile_LockUnlock(Convert.ToInt32(hdnProcessId.Value), objuser.UserId, "Y");

                                        btnSubmit.Enabled = true;
                                        ddlStatus.Enabled = true;
                                        txtSearchLAN.Enabled = false;
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "Validation", "alert('Barcode : " + dr["Barcode"].ToString() + " is locked,used by another user.');", true);
                                    }
                                }
                                else
                                {
                                    if (dr["IsRescan"].ToString().Trim() == "Y")
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "Validation", "alert('Barcode : " + dr["Barcode"].ToString() + " is Rejected, New file not uploaded.');", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "Validation", "alert('Barcode : " + dr["Barcode"].ToString() + " file not uploaded.');", true);
                                    }
                                }

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Validation", "alert('Data already verified');", true);
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Validation", "alert('No Record Found');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Validation", "alert('No Record Found');", true);
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

                txtCurrentPage.Value = "";

                if (txtSearchLAN.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('LAN can not be blank');", true);
                    return;
                }

                dt = ClsUploadData.GetSearchData(txtSearchLAN.Text.Trim());

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["Status"].ToString().Trim() != "OK")
                            {
                                if (dr["IsRescan"].ToString().Trim() == "N")
                                {
                                    if (dr["IsLocked"].ToString().Trim() == "N")
                                    {
                                        txtBarcode.Text = dr["Barcode"].ToString();
                                        txtLAN.Text = dr["LAN"].ToString();
                                        txtMemberName.Text = dr["Customer_Name"].ToString();
                                        txtDisbursalAmt.Text = dr["Disbursed_Amount"].ToString();
                                        txtDisbursementMode.Text = dr["Disbursement_Mode"].ToString();
                                        txtDisDate.Text = dr["Disbursement_Date"].ToString();
                                        hdnProcessId.Value = dr["ProcessId"].ToString();
                                        hdnFileName.Value = dr["FileName"].ToString();
                                        hdnFilePath.Value = dr["FilePath"].ToString();

                                        ////pageCount = getPageCount(hdnFilePath.Value);
                                        //System.Drawing.Image tiffimg = System.Drawing.Image.FromFile(hdnFilePath.Value);

                                        //System.Drawing.Imaging.FrameDimension dimension;
                                        //dimension = System.Drawing.Imaging.FrameDimension.Page;
                                        //pageCount = tiffimg.GetFrameCount(dimension);
                                        //if (pageCount > 0)
                                        //{
                                        //    dimension = new System.Drawing.Imaging.FrameDimension(tiffimg.FrameDimensionsList[0]);
                                        //    _image.SelectActiveFrame(dimension, 0);
                                        //}

                                        //string src = CommonDB.GetSrc("Handler") + hdnFilePath.Value;
                                        //pdfFrame1.Attributes.Add("src", src);



                                        LoadFile(hdnFilePath.Value, hdnFileName.Value);

                                        string msg = ClsUploadData.UpdateFile_LockUnlock(Convert.ToInt32(hdnProcessId.Value), objuser.UserId, "Y");

                                        btnSubmit.Enabled = true;
                                        ddlStatus.Enabled = true;
                                        txtSearchBarcode.Enabled = false;
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "Validation", "alert('LAN : " + dr["LAN"].ToString() + " is locked,used by another user.');", true);
                                    }

                                }
                                else
                                {
                                    if (dr["IsRescan"].ToString().Trim() == "Y")
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('LAN : " + dr["LAN"].ToString() + " is Rejected, New file not uploaded.');", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('LAN : " + dr["LAN"].ToString() + " file not uploaded.');", true);
                                    }
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Data already verified');", true);
                            }
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
        //public int getPageCount(String fileName)
        //{
        //    int pageCount = -1;
        //    try
        //    {
        //        System.Drawing.Image img = System.Drawing.Bitmap.FromFile(fileName);
        //        pageCount = img.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page);
        //        if (pageCount == 0) //single page tiff
        //        {
        //            pageCount = 1;
        //        }
        //        else if (pageCount > 1) // tiffs having pages more than one
        //        {
        //            //pageCount = pageCount - 1;
        //        }
        //        img.Dispose();

        //    }
        //    catch (Exception ex)
        //    {
        //        pageCount = 1;
        //    }
        //    return pageCount;
        //}

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(700);

                string RejectReason = string.Empty;
                ClsUser objuser = new ClsUser();
                objuser = (ClsUser)Session["objUser"];

                if (ddlStatus.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please select status');", true);
                    return;
                }

                if (ddlStatus.SelectedValue != "OK")
                {
                    //if (ddlRejectReason.SelectedValue == "0")
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please select reject reason');", true);
                    //    return;
                    //}
                    //if (txtRemark.Text.Trim() == "")
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please enter remark');", true);
                    //    return;
                    //}

                    //if (ddlRejectRemark.SelectedValue == "0")
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please select reject remark');", true);
                    //    return;
                    //}

                    foreach (ListItem item in lstRejectReason.Items)
                    {
                        if (item.Selected)
                        {
                            string[] keyid = item.Value.Split('|');
                            //RejectReason += item.Text + "," + item.Value;
                            //RejectReason += item.Value + ",";

                            if (keyid[1] == "Y")
                            {
                                if (txtRemark.Text.Trim() == "")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please enter remark');", true);
                                    return;
                                }
                            }

                            RejectReason += keyid[0].ToString() + ",";
                        }
                    }

                    if (RejectReason == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Please select reject reason');", true);
                        return;
                    }

                }

                string[] objData = new string[5];

                objData[0] = hdnProcessId.Value;
                objData[1] = ddlStatus.SelectedValue;
                //objData[2] = ddlRejectReason.SelectedValue;
                objData[2] = RejectReason;
                objData[3] = txtRemark.Text.Trim().Replace("\n","");
                //objData[3] = ddlRejectRemark.SelectedValue;
                objData[4] = txtDisDate.Text.Trim();

                string res = ClsUploadData.UpdateVerificationStatus(objData, objuser.UserId);

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
                txtDisDate.Text = "";
                txtDisbursalAmt.Text = "";
                txtDisbursementMode.Text = "";
                txtMemberName.Text = "";
                ddlStatus.SelectedIndex = 0;
                //ddlRejectReason.SelectedIndex = 0;
                //ddlRejectRemark.Items.Clear();
                //ddlRejectRemark.SelectedIndex = 0;
                divReason.Visible = false;
                txtRemark.Text = "";
                hdnFileName.Value = "";
                hdnFilePath.Value = "";
                //pdfFrame1.Attributes.Add("src", "");
                btnSubmit.Enabled = false;
                ddlStatus.Enabled = false;
                _plcBigImg.Controls.Clear();
                _plcImgsThumbs.Controls.Clear();
                _plcImgsThumbsPager.Controls.Clear();

                string msg = ClsUploadData.UpdateFile_LockUnlock(0, objuser.UserId, "N");

                ddlPages.Items.Clear();
                txtCurrentPage.Value = "";

                foreach (ListItem item in lstRejectReason.Items)
                {
                    item.Selected = false;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        //protected void ddlRejectReason_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DataTable dt = CommonData.GetKeyValues("Reject_Reason", ddlRejectReason.SelectedValue);
        //        ddlRejectRemark.DataSource = dt;
        //        //ddlRejectRemark.DataValueField = "key_value";
        //        //ddlRejectRemark.DataTextField = "key_value";
        //        //ddlRejectRemark.DataBind();
        //        CommonDB.FillDropDown(ddlRejectRemark, dt, "key_id", "key_value");
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
        //    }
        //}

        private void LoadFile(string fPath, string filename)
        {
            try
            {

                //string UploadFilePath = ConfigurationManager.AppSettings["UploadLocation"].ToString();
                //UploadFilePath = UploadFilePath + Path.GetFileNameWithoutExtension(fPath) + "_" + userid + Path.GetExtension(filename);

                //File.Copy(fPath, UploadFilePath);

                ddlPages.Items.Clear();

                FilePath = fPath;
                //FilePath = UploadFilePath;

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
                    Img.ID = "TumbImg_" + StartPg.ToString();
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

                //Config actions
                _hlRot90.Attributes.Add("OnClick", "document.getElementById('MainContent__imgBig').src = 'ViewImg.aspx?View=1&FilePath=" + FilePath.ToString().Replace(@"\", "%5C") + "&Pg=" + BigImgPg.ToString() + "&Height=" + DefaultBigHieght.ToString() + "&Width=" + DefaultBigWidth.ToString() + "&Rotate=90';");
                _hlRot90.Attributes.Add("onmouseover", "this.style.cursor='pointer';");

                _hlRot180.Attributes.Add("OnClick", "document.getElementById('MainContent__imgBig').src = 'ViewImg.aspx?View=1&FilePath=" + FilePath.ToString().Replace(@"\", "%5C") + "&Pg=" + BigImgPg.ToString() + "&Height=" + DefaultBigHieght.ToString() + "&Width=" + DefaultBigWidth.ToString() + "&Rotate=180';");
                _hlRot180.Attributes.Add("onmouseover", "this.style.cursor='pointer';");

                _hlRot270.Attributes.Add("OnClick", "document.getElementById('MainContent__imgBig').src = 'ViewImg.aspx?View=1&FilePath=" + FilePath.ToString().Replace(@"\", "%5C") + "&Pg=" + BigImgPg.ToString() + "&Height=" + DefaultBigHieght.ToString() + "&Width=" + DefaultBigWidth.ToString() + "&Rotate=270';");
                _hlRot270.Attributes.Add("onmouseover", "this.style.cursor='pointer';");

                //_hlBig.Attributes.Add("OnClick", "document.getElementById('MainContent__imgBig').src = 'ViewImg.aspx?View=1&FilePath=" + FilePath.ToString().Replace(@"\", "%5C") + "&Pg=" + BigImgPg.ToString() + "&Height=" + (DefaultBigHieght + 400).ToString() + "&Width=" + (DefaultBigWidth + 400).ToString() + "';");
                _hlBig.Attributes.Add("OnClick", "ZoomIn();");
                _hlBig.Attributes.Add("onmouseover", "this.style.cursor='pointer';");

                //_hlSmall.Attributes.Add("OnClick", "document.getElementById('MainContent__imgBig').src = 'ViewImg.aspx?View=1&FilePath=" + FilePath.ToString().Replace(@"\", "%5C") + "&Pg=" + BigImgPg.ToString() + "&Height=" + (DefaultBigHieght).ToString() + "&Width=" + (DefaultBigWidth).ToString() + "';");
                _hlSmall.Attributes.Add("OnClick", "ZoomOut();");
                _hlSmall.Attributes.Add("onmouseover", "this.style.cursor='pointer';");

                _hlRevert.Attributes.Add("OnClick", "FitToPage();");
                _hlRevert.Attributes.Add("onmouseover", "this.style.cursor='pointer';");


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
                string exMsg = "Issue with the Image File to show in the Viewer. Please check whether the image file is available or not at the File Path.";
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + exMsg + "');", true);
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
                    hdnTotalTIFPgs.Value = Convert.ToString(ViewState["TotalTIFPgs"]);
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
                    Img.ID = "TumbImg_" + StartPg.ToString();
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

        protected void chkAutoBarcode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoBarcode.Checked == false)
            {
                txtSearchBarcode.Enabled = true;
                txtSearchLAN.Enabled = true;
                btnGetAutoBarcode.Enabled = false;
                
            }
            else
            {
                txtSearchBarcode.Enabled = false;
                txtSearchLAN.Enabled = false;
                btnGetAutoBarcode.Enabled = true;
                txtSearchBarcode.Text = "";
                txtSearchLAN.Text = "";
            }

            txtSearchBarcode.Text = "";
            txtSearchLAN.Text = "";
            txtBarcode.Text = "";
            txtLAN.Text = "";
            txtMemberName.Text = "";
            txtDisbursalAmt.Text = "";
            txtDisbursementMode.Text = "";
            txtDisDate.Text = "";
            ddlStatus.SelectedIndex = 0;
            ddlStatus.Enabled = false;
            btnSubmit.Enabled = false;
        }



        /// 
        /// //////////////////////////////////////////////////////// Added By Kapil Singhal on 12th Dec. 2022 //////////////////////////////////////////////////////
        /// 

        protected void btnGetAutoBarcode_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                ClsUser objuser = new ClsUser();
                objuser = (ClsUser)Session["objUser"];

                txtCurrentPage.Value = "";

                //if (txtSearchBarcode.Text.Trim() == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('barcode can not be blank');", true);
                //    return;
                //}

                dt = ClsUploadData.GetAutoBarcodeData();

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["Status"].ToString().Trim() != "OK")
                            {
                                if (dr["IsRescan"].ToString().Trim() == "N")
                                {
                                    if (dr["IsLocked"].ToString().Trim() == "N")
                                    {

                                        txtBarcode.Text = dr["Barcode"].ToString();
                                        txtLAN.Text = dr["LAN"].ToString();
                                        txtMemberName.Text = dr["Customer_Name"].ToString();
                                        txtDisbursalAmt.Text = dr["Disbursed_Amount"].ToString();
                                        txtDisbursementMode.Text = dr["Disbursement_Mode"].ToString();
                                        txtDisDate.Text = dr["Disbursement_Date"].ToString();
                                        hdnProcessId.Value = dr["ProcessId"].ToString();
                                        hdnFileName.Value = dr["FileName"].ToString();
                                        hdnFilePath.Value = dr["FilePath"].ToString();

                                        //string src = CommonDB.GetSrc("Handler") + hdnFilePath.Value + "#toolbar=0";
                                        //pdfFrame1.Attributes.Add("src", src);

                                        try
                                        {
                                            LoadFile(hdnFilePath.Value, hdnFilePath.Value);
                                        }
                                        catch(Exception exFileImage)
                                        {
                                            string msgFileImage = "File Image not found for this Barcode. ( Barcode : " + " " + txtBarcode.Text + " " + ").";
                                            return;
                                        }

                                        string msg = ClsUploadData.UpdateFile_LockUnlock(Convert.ToInt32(hdnProcessId.Value), objuser.UserId, "Y");

                                        btnSubmit.Enabled = true;
                                        ddlStatus.Enabled = true;
                                        txtSearchLAN.Enabled = false;
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "Validation", "alert('Barcode : " + dr["Barcode"].ToString() + " is locked,used by another user.');", true);
                                    }
                                }
                                else
                                {
                                    if (dr["IsRescan"].ToString().Trim() == "Y")
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "Validation", "alert('Barcode : " + dr["Barcode"].ToString() + " is Rejected, New file not uploaded.');", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "Validation", "alert('Barcode : " + dr["Barcode"].ToString() + " file not uploaded.');", true);
                                    }
                                }

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Validation", "alert('Data already verified');", true);
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Validation", "alert('No Record Found');", true);
                        ClearFields();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Validation", "alert('No Record Found');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + ex.Message + "');", true);
            }
        }

        private void ClearFields()
        {
            txtSearchBarcode.Text = "";
            txtSearchLAN.Text = "";
            txtBarcode.Text = "";
            txtLAN.Text = "";
            txtMemberName.Text = "";
            txtDisbursalAmt.Text = "";
            txtDisbursementMode.Text = "";
            txtDisDate.Text = "";
            ddlStatus.SelectedIndex = 0;
            ddlStatus.Enabled = false;
            btnSubmit.Enabled = false;
        }

        ///
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
    }
}