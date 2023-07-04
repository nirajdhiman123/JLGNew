<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/JLG_Master.Master" AutoEventWireup="true" CodeBehind="frmPDDVerification.aspx.cs" Inherits="JLG.Forms.frmPDDVerification" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="../Styles/jquery-ui.css" />
    <link rel="stylesheet" href="../bootstrap/css/bootstrap-multiselect.css" />

    <script src="../Scripts/jquery-1.12.4.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../bootstrap/js/bootstrap.min.js"></script>
    <script src="../bootstrap/js/bootstrap-multiselect.js"></script>



    <script type="text/javascript">

        //function div_VF(x) {
        //    //alert(x.value);
        //    var eleDiv = document.getElementById("divReason");
        //    if (x.value != "OK") {
        //        eleDiv.style.display = "block";
        //    }
        //    else {

        //        eleDiv.style.display = "none";
        //    }
        //}

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

            $("#reset_client").click(function () {
                $('[id*=lstRejectReason] option').each(function (element) {
                    $(this).removeAttr('selected').prop('selected', false);
                });
                $('[id*=lstRejectReason]').multiselect('refresh');
            });
        });

        $(function () {
            $('[id*=lstRejectReason]').multiselect({
                includeSelectAllOption: false,
                enableCaseInsensitiveFiltering: true,
                maxHeight: 250,
                buttonWidth: 230,
                enableFiltering: true,
                numberDisplayed: 2,
                onChange: function (option, checked) {
                    // Get selected options.
                    var selectedOptions = $('[id*=lstRejectReason] option:selected');
                    //alert(selectedOptions);
                    //alert(selectedOptions.length);
                    if (selectedOptions.length >= 5) {

                        // Disable all other checkboxes.
                        var nonSelectedOptions = $('[id*=lstRejectReason] option').filter(function () {
                            return !$(this).is(':selected');
                        });

                        nonSelectedOptions.each(function () {
                            var input = jQuery('[id*=lstRejectReason] + .btn-group input[value="' + $(this).val() + '"]');
                            input.prop('disabled', true);
                            input.parent('li').addClass('disabled');
                        });
                    }
                    else {
                        // Enable all checkboxes.
                        $('[id*=lstRejectReason] option').each(function () {
                            var input = $('[id*=lstRejectReason] + .btn-group  input[value="' + $(this).val() + '"]');
                            input.prop('disabled', false);
                            input.parent('li').addClass('disabled');
                        });
                    }
                }
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

         
         
         

     </script>
    <style type="text/css">
        .imgbutton {
            cursor: pointer;
            height: 30px;
            width: 30px;
        }

        .selected {
            box-shadow: 0px 12px 22px 1px #333;
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


        /* ------------------------------------------------------------     Added By Kapil Singhal on 12th Dec. 2022   ------------------------------------------*/ 

        .paddingStyle{
            padding-top:6px;
        }

        .padStyle{
            padding-top:2px;
        }

        .btnStyle{
            background-color:aliceblue;
            border-radius:3px;
            border:1px solid darkblue;
            color:darkslateblue;
            font-family:'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
            font-size:small;
            padding-top:2px;
            padding-bottom:3px;
        }

        .btnStyle:hover{
            background-color:darkslateblue;
            color:aliceblue;
            box-shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px;
            transition:ease-in-out 0.3s;
        }


        /*  --------------------------------------------------------------------------------------------  */

    </style>
            <script type="text/javascript">
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
        //$('#btnSubmit').on("click", function () {
        //    ShowProgress();
        //});
            </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <%--<div id="loaderDiv" style="display:none;background-color:gray;height:600px;width:600px;">
                <img src="../Images/loading.gif"  />
        </div>--%>
        <div class="head-r1x">
            <span class="headx">PDD Verification</span>
        </div>
        <div class="block1x form-horizontal">

            <%--       Added By Kapil Singhal on 12th Dec. 2022       --%>

            <div class="row">
                <div class="form-group col-sm-4">
                    <label class="control-label col-sm-5">Auto Barcode:</label>
                    <div class="col-sm-1 paddingStyle">
                        <asp:CheckBox ID="chkAutoBarcode" runat="server" Checked="true" OnCheckedChanged="chkAutoBarcode_CheckedChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-sm-5 padStyle">
                        <asp:Button ID="btnGetAutoBarcode" runat="server" CssClass="btnStyle" Text="Get Auto Barcode" OnClick="btnGetAutoBarcode_Click" />
                    </div>
                </div>
            </div>

             <%-- -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------%>

            <div class="row">
                <div class="form-group col-sm-4">
                    <label class="control-label col-sm-6">Search By Barcode:</label>
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
                            <label class="control-label" for="txtDisDate">Disbursement/Receipt Date</label>
                            <div class="form-control">
                                <asp:TextBox ID="txtDisDate" runat="server" CssClass="youpi" BorderStyle="None"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group col-sm-1"></div>
                        <div class="form-group col-sm-6">
                            <label class="control-label" for="ddlStatus">Status</label>
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                <asp:ListItem Value="">--SELECT--</asp:ListItem>
                                <asp:ListItem Value="OK">OK</asp:ListItem>
                                <asp:ListItem Value="NOT OK">NOT OK</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div id="divReason" class="row" runat="server">
                        <div class="form-group col-sm-8">
                            <label class="control-label" for="ddlStatus">Reject Reason</label>
                            <%-- <asp:DropDownList ID="ddlRejectReason" runat="server" CssClass="form-control">
                            </asp:DropDownList>--%>
                            <asp:ListBox ID="lstRejectReason" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                            <input type="button" value="Reset" name="reset_clients" id="reset_client" class="buttonx" title="Clear Selection">
                        </div>
                        <div class="form-group col-sm-1"></div>
                        <div class="form-group col-sm-11">
                            <label class="control-label" for="txtDisDate">Remark</label>
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            <%--   <asp:DropDownList ID="ddlRejectRemark" runat="server" CssClass="form-control">
                            </asp:DropDownList>--%>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-10" style="text-align: center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="buttonx" OnClick="btnSubmit_Click" OnClientClick="ShowProgress();" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonx" OnClick="btnCancel_Click" />
                            <asp:HiddenField ID="hdnFileName" runat="server" />
                            <asp:HiddenField ID="hdnFilePath" runat="server" />
                            <asp:HiddenField ID="hdnProcessId" runat="server" />
                            <asp:HiddenField ID="hdnTotalTIFPgs" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="col-sm-7" style="border: dotted 2px #FDB813">
                    <%--<iframe id="pdfFrame1" runat="server"  class="embed-responsive-item" height="700" name="pdfFrame1" scrollbar="auto" src="" style="pointer-events: visible" width="100%"></iframe>--%>
                    <%-- <img id="pdfFrame1" runat="server" height="700" />--%>
                    <div class="col-md-12" style="text-align: center;">
                        <asp:HyperLink runat="server" ID="_hlRot90" Text="[Rotate Left]" Font-Underline="true" Visible="false" />&nbsp;
                        <asp:HyperLink runat="server" ID="_hlRot270" Text="[Rotate Right]" Font-Underline="true" Visible="false" />&nbsp;
                        <asp:HyperLink runat="server" ID="_hlRot180" Text="[Rotate 180]" Font-Underline="true" Visible="false" />&nbsp;
                      <%--  <asp:HyperLink runat="server" ID="_hlBig" Text="[Bigger]" Font-Underline="true" />&nbsp;
                        <asp:HyperLink runat="server" ID="_hlSmall" Text="[Revert]" Font-Underline="true" />&nbsp;<br />--%>
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
                        <div class="col-md-2" style="float: left; border: solid 2px #808080; padding-top: 5px; height: 730px; overflow: auto;">
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

        <div id="mdlLoader" class="loading" align="center">
           <h2><b>Please wait.</b></h2> <br />
            <br />
            <img src="../Images/loading.gif" alt="" />
        </div>


    </div>
</asp:Content>
