<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/JLG_Master.Master" AutoEventWireup="true" CodeBehind="frmRejectionReprocess.aspx.cs" Inherits="JLG.Forms.frmRejectionReprocess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../Styles/jquery-ui.css" />

    <script src="../Scripts/jquery-1.12.4.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".youpi").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: '../Images/calender.jpg',
                dateFormat: 'dd-M-yy',
                //yearRange: "c-100:c+100",
                showAnim: 'slide',
                changeMonth: true,
                maxDate: new Date(),
                //  minDate: "-2D",
                changeYear: true
            });
        });
    </script>

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
            //document.getElementById('_hlRot270').onclick = function () { ChangePg(Pg); document.getElementById('MainContent__imgBig').src = Src + "&Rotate=270"; };
            //document.getElementById('_hlRot180').onclick = function () { ChangePg(Pg); document.getElementById('MainContent__imgBig').src = Src + "&Rotate=180"; };
            //document.getElementById('_hlRot90').onclick = function () { ChangePg(Pg); document.getElementById('MainContent__imgBig').src = Src + "&Rotate=90"; };
            //document.getElementById('_hlBig').onclick = function () { ChangePg(Pg); document.getElementById('MainContent__imgBig').src = SrcBig + "&Rotate" + GetBigSrc("Rotate"); };
            //document.getElementById('_hlSmall').onclick = function () { ChangePg(Pg); document.getElementById('MainContent__imgBig').src = SrcRevert; };
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

            var currWidth = parseInt(GetBigSrc("Width")) + 50;
            var currHeight = parseInt(GetBigSrc("Height")) + 50;



            var Src = 'ViewImg.aspx?View=1&FilePath=' + GetBigSrc("FilePath") + "&Pg=" + GetBigSrc("Pg") + "&Height=" + currHeight + "&Width=" + currWidth;
            zoomimg.src = Src;
            //zoomimg.width(currWidth + 50);
            //zoomimg.height(currHeight + 50);

        }

        function ZoomOut() {
            // alert("OK");
            var zoomimg = document.getElementById('MainContent__imgBig');
            //var currWidth = zoomimg.width();
            //var currHeight = zoomimg.height();

            var currWidth = parseInt(GetBigSrc("Width")) - 50;
            var currHeight = parseInt(GetBigSrc("Height")) - 50;


            var Src = 'ViewImg.aspx?View=1&FilePath=' + GetBigSrc("FilePath") + "&Pg=" + GetBigSrc("Pg") + "&Height=" + currHeight + "&Width=" + currWidth;
            zoomimg.src = Src;

            //zoomimg.width(currWidth - 50);
            //zoomimg.height(currHeight - 50);
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


          function ShowProgress() {
            setTimeout(function () {
                var modal = $('#mdlLoader');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }


      </script>
    <style>
        .imgbutton {
            cursor: pointer;
            height: 30px;
            width: 30px;
        }

        .modal
        {
            position: fixed;
            top: 20px;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.9;
            filter: alpha(opacity=100);
            min-height: 100%;
            width: 100%;
        }
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            max-width: 100%;
            height: 600px;
            display: none;
            position: fixed;
            background-color: darkgray;
            z-index: 999;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="head-r1x">
            <span class="headx">DVU Rejection Re-process</span>
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
            <div class="row" style="overflow: auto;">
                <div class="col-sm-5">
                    <div class="row">
                        <div class="form-group col-sm-6">
                            <label class="control-label" for="txtBarcode">Barcode</label>
                            <asp:TextBox ID="txtBarcode" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="form-group col-sm-1"></div>
                        <div class="form-group col-sm-6">
                            <label class="control-label" for="txtLAN">LAN Number</label>
                            <asp:TextBox ID="txtLAN" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-sm-8">
                            <label class="control-label" for="txtBarcode">Member Name</label>
                            <asp:TextBox ID="txtMemberName" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-sm-6">
                            <label class="control-label" for="txtBarcode">Disbursal Amount</label>
                            <asp:TextBox ID="txtDisbursalAmt" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="form-group col-sm-1"></div>
                        <div class="form-group col-sm-6">
                            <label class="control-label" for="txtLAN">Disbursement Mode</label>
                            <asp:TextBox ID="txtDisbursementMode" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-sm-6">
                            <label class="control-label" for="txtBarcode">Group Name</label>
                            <asp:TextBox ID="txtGroupName" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="form-group col-sm-1"></div>
                        <div class="form-group col-sm-6">
                            <label class="control-label" for="txtLAN">Center Name</label>
                            <asp:TextBox ID="txtCenterName" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-sm-6">
                            <label class="control-label" for="txtDisDate">Max Group Count</label>
                            <%--<div class="form-control">
                                <asp:TextBox ID="txtDisDate" runat="server" CssClass="youpi" BorderStyle="None"></asp:TextBox>
                                 <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>--%>
                            <asp:TextBox ID="txtGroupCount" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="form-group col-sm-1"></div>
                       <div class="form-group col-sm-6">
                            <label class="control-label" for="txtLAN">Rejection Date</label>
                            <asp:TextBox ID="txtRejectedDate" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                         <div class="form-group col-sm-12">
                            <label class="control-label" for="ddlStatus">Reject Reason</label>
                            <asp:TextBox ID="txtRejectReason" runat="server" CssClass="form-control" ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" runat="server">
                        <div class="form-group col-sm-3">
                            <label class="control-label" for="ddlStatus">Status</label>
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                <asp:ListItem Value="">--SELECT--</asp:ListItem>
                                <asp:ListItem Value="OK">OK</asp:ListItem>
                                <asp:ListItem Value="NOT OK">Reject</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group col-sm-1"></div>
                        <div class="form-group col-sm-9">
                            <label class="control-label" for="txtDisDate">Remark</label>
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-10" style="text-align: center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="buttonx" OnClick="btnSubmit_Click" OnClientClick="ShowProgress();" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonx" OnClick="btnCancel_Click" />
                            <asp:HiddenField ID="hdnFileName" runat="server" />
                            <asp:HiddenField ID="hdnFilePath" runat="server" />
                            <asp:HiddenField ID="hdnProcessId" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="col-sm-7" style="border: dotted 2px #FDB813">
                    <div class="col-md-12" style="text-align: center;">
                         <asp:DropDownList ID="ddlPages" runat="server" AutoPostBack="true" Style="width: 70px;height:25px; border: solid 1px #808080; margin-top: 5px; display: inline-block"
                               OnSelectedIndexChanged="ddlPages_SelectedIndexChanged" ToolTip="Pages">
                           </asp:DropDownList>&nbsp;
                           <input id="txtCurrentPage" runat="server" style="width:25px" title="Current Page" />&nbsp;
                        <img title="Zoom In" src="../Images/ZoomIn.png" onclick="javascript:ZoomIn();" class="imgbutton" />&nbsp;
                         <img title="Zoom Out" src="../Images/ZoomOut.png" onclick="javascript:ZoomOut();" class="imgbutton" />&nbsp;
                         <img title="Reset" src="../Images/Refresh.png" onclick="javascript:FitToPage();" class="imgbutton" />&nbsp;
                         <img title="Rotate Left" src="../Images/LeftRotate.png" onclick="javascript:rotateImage(1);" class="imgbutton" />&nbsp;
                         <img title="Rotate Right" src="../Images/RightRotate.png" onclick="javascript:rotateImage(2);" class="imgbutton" />
                    </div>
                    <div class=" col-md-12" style="text-align: center;">
                        <div class="col-md-2" style="float: left; border: solid 2px #808080; padding-top: 5px; height: 730px;overflow: auto;">
                            <asp:PlaceHolder runat="server" ID="_plcImgsThumbs" />
                            <br />
                            <asp:PlaceHolder runat="server" ID="_plcImgsThumbsPager" />
                        </div>
                        <div class="col-md-10" style="float: right; border: solid 2px #808080; padding-top: 5px;">
                            <div style="overflow: auto; height: 720px; width: 100%">
                                <asp:PlaceHolder runat="server" ID="_plcBigImg" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="mdlLoader" class="loading" align="center">
           <h2><b>Please wait.</b></h2> <br />
            <br />
            <img src="../Images/loading.gif" alt="" />
        </div>

    
</asp:Content>
