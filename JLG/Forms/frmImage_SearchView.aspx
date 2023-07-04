<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/JLG_Master.Master" AutoEventWireup="true" CodeBehind="frmImage_SearchView.aspx.cs" Inherits="JLG.Forms.frmImage_SearchView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../Scripts/jquery-1.12.4.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>

    <script type="text/javascript">
        var _css = 0;

        function ChangePg(Pg) {

            Src = 'ViewImg.aspx?View=1&FilePath=' + GetBigSrc("FilePath") + "&Pg=" + Pg + "&Height=" + GetBigSrc("Height") + "&Width=" + GetBigSrc("Width");
            // alert(Src);
            SrcBig = 'ViewImg.aspx?View=1&FilePath=' + GetBigSrc("FilePath") + "&Pg=" + Pg + "&Height=" + 1000 + "&Width=" + 1000;
            SrcRevert = 'ViewImg.aspx?View=1&FilePath=' + GetBigSrc("FilePath") + "&Pg=" + Pg + "&Height=" + 600 + "&Width=" + 600;
            //alert(Src);
            document.getElementById('MainContent_txtCurrentPage').value = Pg;
            document.getElementById('MainContent__imgBig').src = Src;
            
            var totalpg = document.getElementById('MainContent_hdnTotalTIFPgs');
            //alert(totalpg.value);
            if (totalpg.value <= 8) {
                for (let i = 0; i < totalpg.value; i++) {

                    var imgname = 'MainContent_TumbImg_' + (i + 1);
                    //alert(imgname);
                    var pgId = document.getElementById(imgname);
                    //alert(pgId);
                    pgId.classList.remove('selected');
                }
            }
            else {
                var ddlpages = document.getElementById('MainContent_ddlPages');
                var x = ddlpages.value;
                //alert(x);
                var t = 0;
                if (x == 1) {
                    t = 9;
                    for (let i = x; i < t; i++) {

                        var imgname = 'MainContent_TumbImg_' + (i);
                        //alert(imgname);
                        var pgId = document.getElementById(imgname);
                        //alert(pgId);
                        pgId.classList.remove('selected');
                    }
                } else {
                    t = parseInt(totalpg.value) - parseInt(ddlpages.value) + 1;
                    for (let i = 0; i < t; i++) {

                        var imgname = 'MainContent_TumbImg_' + (i + parseInt(x));
                        //alert(imgname);
                        var pgId = document.getElementById(imgname);
                        //alert(pgId);
                        pgId.classList.remove('selected');
                    }
                }
                //alert(t);


            }
            var Cimgname = 'MainContent_TumbImg_' + Pg;
            var CurrentpgId = document.getElementById(Cimgname);
            //alert(CurrentpgId);
            CurrentpgId.classList.add('selected');

            //document.getElementById('_hlRot270').onclick = function () { ChangePg(Pg); document.getElementById('MainContent__imgBig').src = Src + "&Rotate=270"; };
            //document.getElementById('_hlRot180').onclick = function () { ChangePg(Pg); document.getElementById('MainContent__imgBig').src = Src + "&Rotate=180"; };
            //document.getElementById('_hlRot90').onclick = function () { ChangePg(Pg); document.getElementById('MainContent__imgBig').src = Src + "&Rotate=90"; };
            //document.getElementById('_hlBig').onclick = function () { ChangePg(Pg); document.getElementById('MainContent__imgBig').src = SrcBig + "&Rotate" + GetBigSrc("Rotate"); };
            //document.getElementById('_hlSmall').onclick = function () { ChangePg(Pg); document.getElementById('MainContent__imgBig').src = SrcRevert; };
            //document.getElementById('_hlRevert').onclick = function () { ChangePg(Pg); document.getElementById('MainContent__imgBig').src = SrcRevert; };

            //document.getElementById('_hlBig').onclick = function () {ZoomIn();};
            //document.getElementById('_hlSmall').onclick = function () {ZoomOut();};
        }

        function GetBigSrc(Qrystr) {
            var Qry = document.getElementById('MainContent__imgBig').src;
            //alert(Qry);
            gy = Qry.split("&");
            for (i = 0; i < gy.length; i++) {
                ft = gy[i].split("=");
                if (ft[0] == Qrystr)
                    return ft[1];
            }
        }

        function ZoomIn() {

            //alert("OK");
            var zoomimg = document.getElementById('MainContent__imgBig');

            //var currWidth = zoomimg.width();
            //var currHeight = zoomimg.height();

            //zoomimg.width(currWidth + 50);
            //zoomimg.height(currHeight + 50);


            var currWidth = parseInt(GetBigSrc("Width")) + 50;
            var currHeight = parseInt(GetBigSrc("Height")) + 50;

            var Src = 'ViewImg.aspx?View=1&FilePath=' + GetBigSrc("FilePath") + "&Pg=" + GetBigSrc("Pg") + "&Height=" + currHeight + "&Width=" + currWidth;
            zoomimg.src = Src;


        }

        function ZoomOut() {
            // alert("OK");
            var zoomimg = document.getElementById('MainContent__imgBig');

            //var currWidth = zoomimg.width();
            //var currHeight = zoomimg.height();

            //zoomimg.width(currWidth - 50);
            //zoomimg.height(currHeight - 50);

            var currWidth = parseInt(GetBigSrc("Width")) - 50;
            var currHeight = parseInt(GetBigSrc("Height")) - 50;

            var Src = 'ViewImg.aspx?View=1&FilePath=' + GetBigSrc("FilePath") + "&Pg=" + GetBigSrc("Pg") + "&Height=" + currHeight + "&Width=" + currWidth;
            zoomimg.src = Src;


        }

        function FitToPage() {

            var zoomimg = document.getElementById('MainContent__imgBig');

            //var currWidth = zoomimg.width();
            //var currHeight = zoomimg.height();

            var currWidth = 700;
            var currHeight = 750;

            var Src = 'ViewImg.aspx?View=1&FilePath=' + GetBigSrc("FilePath") + "&Pg=" + GetBigSrc("Pg") + "&Height=" + currHeight + "&Width=" + currWidth;
            zoomimg.src = Src;


        }

        function rotateImage(rotateID) {

            //if (rotateID == 1) {
            //    _css = { rotate: '-=90' };
            //}
            //else if (rotateID == 2) {
            //    _css = { rotate: '+=90' };
            //}

            if (rotateID == 1) {
                _css -= 90;
            }
            else if (rotateID == 2) {
                _css += 90;
            }

            var rotateimg = document.getElementById('MainContent__imgBig');
            rotateimg.style.transform = 'rotate(' + _css + 'deg)';

        }

    </script>
    <script type="text/javascript">

        $(document).ready(function () {

            // wireUpEvents();

            //Disable cut copy paste

            //Disable mouse right click
            $(window).on("contextmenu", function (e) {
                //return false;
                e.preventDefault();
            });

            $(window).keyup(function (e) {
                if (e.keyCode == 44) {
                    //alert("Preint Screen")
                    //$("body").hide();
                    //return false;
                    copyToClipboard();
                }
                //change clipboard value code

            });

            $(window).focus(function () {
                $("body").show();
            }).blur(function () {
                $("body").hide();
            });

            $('a').click(function (e) {
                if (e.ctrlKey) {
                    return false;
                }
                if (e.shiftKey) {
                    return false;
                }

            });

            $('body').mousedown(function (e) {
                debugger;
                if (e.button == 1) {
                    alert('not allowed');
                    return false
                }
            });

            var mnu = document.getElementById("mnuMain");
            mnu.ondragstart = function () {
                alert('not allowed');
                return false
            }


        });


        function copyToClipboard() {
            // Create a "hidden" input
            var aux = document.createElement("input");
            // Assign it the value of the specified element
            aux.setAttribute("value", "Print screen disable");
            // Append it to the body
            document.body.appendChild(aux);
            // Highlight its content
            aux.select();
            // Copy the highlighted text
            document.execCommand("copy");
            // Remove it from the body
            document.body.removeChild(aux);
            //alert("Print screen disabled");
        }
    </script>
    <style>
        .imgbutton {
            cursor: pointer;
            height: 30px;
            width: 30px;
        }

        .selected {
            box-shadow: 0px 12px 22px 1px #333;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="head-r1x">
            <span class="headx">PDD Image Search & View</span>
        </div>
        <div class="block1x form-horizontal">
            <div class="row">

                <div class="form-group col-sm-5">
                    <label class="control-label col-sm-5">Search By Barcode:</label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtSearchBarcode" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtSearchBarcode_TextChanged"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group col-sm-4">
                    <label class="control-label col-sm-5">Or LAN Number:</label>
                    <div class="col-sm-7">
                        <asp:TextBox ID="txtSearchLAN" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtSearchLAN_TextChanged"></asp:TextBox>
                    </div>
                </div>

            </div>
            <div class="row Separator">
            </div>
            <div class="row">
                <div class="col-sm-5">
                    <div class="row">
                        <div class="form-group col-sm-6">
                            <label class="control-label" for="txtBarcode">Barcode</label>
                            <asp:TextBox ID="txtBarcode" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-sm-1"></div>
                        <div class="form-group col-sm-6">
                            <label class="control-label" for="txtLAN">LAN Number</label>
                            <asp:TextBox ID="txtLAN" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-sm-6">
                            <label class="control-label" for="txtDisDate">Customer Name</label>
                            <asp:TextBox ID="txtCustName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-sm-1"></div>
                        <div class="form-group col-sm-6">
                            <label class="control-label" for="txtCustName">Exaternal Cust No.</label>
                            <asp:TextBox ID="txtExaternalcustno" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-sm-6">
                            <label class="control-label" for="txtDisDate">BC Name</label>
                            <asp:TextBox ID="txtBCName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-sm-1"></div>
                        <div class="form-group col-sm-6">
                            <label class="control-label" for="txtCustName">BC Branch</label>
                            <asp:TextBox ID="txtBCBranch" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-sm-6">
                            <label class="control-label" for="txtDisDate">Disbursement Date</label>
                            <asp:TextBox ID="txtDisDate" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-sm-1"></div>
                        <div class="form-group col-sm-6">
                            <label class="control-label" for="txtCustName">Inward Date</label>
                            <asp:TextBox ID="txtInwardDate" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-10" style="text-align: center">
                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="buttonx" OnClick="btnClear_Click" />
                            <asp:HiddenField ID="hdnFileName" runat="server" />
                            <asp:HiddenField ID="hdnFilePath" runat="server" />
                            <asp:HiddenField ID="hdnTempFilePath" runat="server" />
                            <asp:HiddenField ID="hdnTotalTIFPgs" runat="server" />
                        </div>
                    </div>
                </div>
                <%--  background-color: #FDB813;--%>
                <div class="col-sm-7" style="border: dotted 2px #FDB813">
                    <%--<iframe id="pdfFrame1" runat="server" class="embed-responsive-item" height="700" name="pdfFrame1" scrollbar="auto" src="" style="pointer-events: visible" width="100%" toolbar="1"></iframe>--%>
                    <div class="col-md-12" style="text-align: center;">
                        <asp:HyperLink runat="server" ID="_hlRot90" Text="[Rotate Left]" Font-Underline="true" Visible="false" />&nbsp;
                        <asp:HyperLink runat="server" ID="_hlRot270" Text="[Rotate Right]" Font-Underline="true" Visible="false" />&nbsp;
                        <asp:HyperLink runat="server" ID="_hlRot180" Text="[Rotate 180]" Font-Underline="true" Visible="false" />&nbsp;
                        <asp:HyperLink runat="server" ID="_hlBig" Text="[Zoom In]" Font-Underline="true" Visible="false" />&nbsp;
                        <asp:HyperLink runat="server" ID="_hlSmall" Text="[Zoom Out]" Font-Underline="true" Visible="false" />&nbsp;
                        <asp:HyperLink runat="server" ID="_hlRevert" Text="[Reset]" Font-Underline="true" Visible="false" />&nbsp;

                           <asp:DropDownList ID="ddlPages" runat="server" AutoPostBack="true" Style="width: 70px; height: 25px; border: solid 1px #808080; margin-top: 5px; display: inline-block"
                               OnSelectedIndexChanged="ddlPages_SelectedIndexChanged" ToolTip="Pages">
                           </asp:DropDownList>&nbsp;
                           <input id="txtCurrentPage" runat="server" style="width: 25px" title="Current Page" />&nbsp;
                           <img title="Zoom In" src="../Images/ZoomIn.png" onclick="javascript:ZoomIn();" class="imgbutton" />&nbsp;
                           <img title="Zoom Out" src="../Images/ZoomOut.png" onclick="javascript:ZoomOut();" class="imgbutton" />&nbsp;
                           <img title="Reset" src="../Images/Refresh.png" onclick="javascript:FitToPage();" class="imgbutton" />&nbsp;
                           <img title="Rotate Left" src="../Images/LeftRotate.png" onclick="javascript:rotateImage(1);" class="imgbutton" />&nbsp;
                           <img title="Rotate Right" src="../Images/RightRotate.png" onclick="javascript:rotateImage(2);" class="imgbutton" />

                    </div>
                    <div class=" col-md-12" style="text-align: center;">
                        <div class="col-md-2" style="float: left; border: solid 2px #FDB813; padding-top: 5px; height: 730px; overflow: auto">
                            <asp:PlaceHolder runat="server" ID="_plcImgsThumbs" />
                            <br />
                            <asp:PlaceHolder runat="server" ID="_plcImgsThumbsPager" />
                        </div>
                        <div class="col-md-10" style="float: right; border: solid 2px #FDB813; padding-top: 5px;">
                            <div style="overflow: auto; height: 720px; width: 100%">
                                <asp:PlaceHolder runat="server" ID="_plcBigImg" />
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
