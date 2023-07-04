using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JLG.Forms
{
    public partial class frmImage_SearchView : System.Web.UI.Page
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearchBarcode.Text = "";
                txtSearchLAN.Text = "";
                txtBarcode.Text = "";
                txtLAN.Text = "";
                txtCustName.Text = "";
                txtExaternalcustno.Text = "";
                txtBCName.Text = "";
                txtBCBranch.Text = "";
                txtDisDate.Text = "";
                txtInwardDate.Text = "";
                hdnFileName.Value = "";
                hdnFilePath.Value = "";
                // pdfFrame1.Attributes.Add("src", "");

                _plcBigImg.Controls.Clear();
                _plcImgsThumbs.Controls.Clear();
                _plcImgsThumbsPager.Controls.Clear();
                ddlPages.Items.Clear();
                txtCurrentPage.Value = "";
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
                ClsUser objuser = new ClsUser();
                objuser = (ClsUser)Session["objUser"];

                txtCurrentPage.Value = "";

                DataTable dt = new DataTable();
                if (txtSearchBarcode.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('barcode can not be blank');", true);
                    return;
                }

                dt = ClsUploadData.GetSearchAllData(txtSearchBarcode.Text.Trim());

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            txtBarcode.Text = dr["Barcode"].ToString();
                            txtLAN.Text = dr["LAN"].ToString();
                            txtCustName.Text = dr["Customer_Name"].ToString();
                            txtExaternalcustno.Text = dr["Exaternalcustno"].ToString();
                            txtBCName.Text = dr["BC_Name"].ToString();
                            txtBCBranch.Text = dr["BC_Branch"].ToString();
                            txtDisDate.Text = dr["Disbursement_Date"].ToString();
                            txtInwardDate.Text = dr["Inward_Date"].ToString();

                            hdnFileName.Value = dr["FileName"].ToString();
                            hdnFilePath.Value = dr["FilePath"].ToString();


                            //string src = CommonDB.GetSrc("Handler") + hdnFilePath.Value;
                            // pdfFrame1.Attributes.Add("src", src);
                            if (dr["IsPurg"].ToString() != "Y")
                            {
                                LoadFile(hdnFilePath.Value, hdnFilePath.Value, Convert.ToString(objuser.UserId));
                            }
                            else 
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('Data already purged, image can not be view.');", true);
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

        protected void txtSearchLAN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ClsUser objuser = new ClsUser();
                objuser = (ClsUser)Session["objUser"];

                txtCurrentPage.Value = "";

                DataTable dt = new DataTable();
                if (txtSearchLAN.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('LAN can not be blank');", true);
                    return;
                }

                dt = ClsUploadData.GetSearchAllData(txtSearchLAN.Text.Trim());

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            txtBarcode.Text = dr["Barcode"].ToString();
                            txtLAN.Text = dr["LAN"].ToString();
                            txtCustName.Text = dr["Customer_Name"].ToString();
                            txtExaternalcustno.Text = dr["Exaternalcustno"].ToString();
                            txtBCName.Text = dr["BC_Name"].ToString();
                            txtBCBranch.Text = dr["BC_Branch"].ToString();
                            txtDisDate.Text = dr["Disbursement_Date"].ToString();
                            txtInwardDate.Text = dr["Inward_Date"].ToString();
                            hdnFileName.Value = dr["FileName"].ToString();
                            hdnFilePath.Value = dr["FilePath"].ToString();

                            //string src = CommonDB.GetSrc("Handler") + hdnFilePath.Value;
                            // pdfFrame1.Attributes.Add("src", src);

                            LoadFile(hdnFilePath.Value, hdnFilePath.Value, Convert.ToString(objuser.UserId));
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


        private void LoadFile(string fPath, string filename, string userid)
        {
            try
            {
                ViewState["TotalTIFPgs"] = null;

                string UploadFilePath = ConfigurationManager.AppSettings["UploadLocation"].ToString();
                UploadFilePath = UploadFilePath + Path.GetFileNameWithoutExtension(fPath) + "_" + userid + Path.GetExtension(filename);

                File.Copy(fPath, UploadFilePath, true);

                hdnTempFilePath.Value = UploadFilePath;

                ddlPages.Items.Clear();

                //FilePath = fPath;
                FilePath = UploadFilePath;

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
                //_hlBig.Attributes.Add("OnClick", "document.getElementById('MainContent__imgBig').src = 'ViewImg.aspx?View=1&FilePath=" + FilePath.ToString().Replace(@"\", "%5C") + "&Pg=" + BigImgPg.ToString() + "&Height=" + (DefaultBigHieght + 50).ToString() + "&Width=" + (DefaultBigWidth + 50).ToString() + "';");
                _hlBig.Attributes.Add("OnClick", "ZoomIn();");
                _hlBig.Attributes.Add("onmouseover", "this.style.cursor='pointer';");

                //_hlSmall.Attributes.Add("OnClick", "document.getElementById('MainContent__imgBig').src = 'ViewImg.aspx?View=1&FilePath=" + FilePath.ToString().Replace(@"\", "%5C") + "&Pg=" + BigImgPg.ToString() + "&Height=" + (DefaultBigHieght - 50).ToString() + "&Width=" + (DefaultBigWidth - 50).ToString() + "';");
                _hlSmall.Attributes.Add("OnClick", "ZoomOut();");
                _hlSmall.Attributes.Add("onmouseover", "this.style.cursor='pointer';");

                //_hlRevert.Attributes.Add("OnClick", "document.getElementById('MainContent__imgBig').src = 'ViewImg.aspx?View=1&FilePath=" + FilePath.ToString().Replace(@"\", "%5C") + "&Pg=" + BigImgPg.ToString() + "&Height=" + (DefaultBigHieght).ToString() + "&Width=" + (DefaultBigWidth).ToString() + "';");
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
                //    if (Request.Url.ToString().IndexOf("?StartPage=") >= 0)
                //        _hl.NavigateUrl = Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("?StartPage=")) + "?StartPage=1";
                //    else
                //        _hl.NavigateUrl = Request.Url.ToString() + "?StartPage=1";

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
                //    if (Request.Url.ToString().IndexOf("?StartPage=") >= 0)
                //        _hl.NavigateUrl = Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("?StartPage=")) + "?StartPage=" + ((i * PagerSize) + 1).ToString();
                //    else
                //        _hl.NavigateUrl = Request.Url.ToString() + "?StartPage=" + ((i * PagerSize) + 1).ToString();

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
                FilePath = hdnTempFilePath.Value;

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
    }
}