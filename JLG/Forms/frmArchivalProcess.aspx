<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/JLG_Master.Master" AutoEventWireup="true" CodeBehind="frmArchivalProcess.aspx.cs" Inherits="JLG.Forms.frmArchivalProcess" %>

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

        function Deleteconfirm(fd, td) {
            //if (!confirm("Do you want to continue..?", "Confirm", "Yes", "No", null, this)) {
            //var r = confirm('Are you sure send to swap case?', "Confirm", "Yes", "No", null, this);
            //alert(code);

            if (confirm("This Loan Code already exists,Are you sure you want to send to swap case..?")) {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "../Services/CommonData.asmx/DeleteArchiveData",
                    data: '{ fromdate: "' + fd + '",todate: "' + td + '"}',
                    dataType: "json",
                    success: function (Result) {
                        Result = Result.d;
                         alert(Result);
                    },
                    error: function (Result) {
                        alert(Result.error);
                    }
                });
                //var x = $('#hdnswapconfirm');
                //x.value = 'YES';
            } else {
                //document.getElementById('hdnswapconfirm').innerHTML = "NO";

                //var x = $('#hdnswapconfirm');
                //x.value = 'NO';
            }

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="head-r1x">
            <span class="headx">Archival Process</span>
        </div>
        <div class="block1x form-horizontal">
            <div class="row no-gutters">
                <div class="form-group col-sm-3">
                    <label class="control-label col-sm-4" style="padding: 0;">From Date:</label>
                    <div class="col-sm-7" style="padding: 0;">
                        <asp:TextBox ID="txtFormDate" runat="server" CssClass="youpi" Width="130px"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group col-sm-3">
                    <label class="control-label col-sm-3" style="padding: 0;">To Date:</label>
                    <div class="col-sm-7" style="padding: 0;">
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="youpi" Width="130px"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12" style="text-align: left; margin-left: 10%">
                    <asp:Button ID="btnSubmit" runat="server" Text="Get Archive Data" CssClass="buttonx" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonx" OnClick="btnCancel_Click" />
                    <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="buttonx" OnClick="btnExportToExcel_Click" />
                </div>
            </div>
            <div class="row Separator">
            </div>
            <div class="row" style="overflow: auto;">
                <div class="col-sm-12">
                    <asp:GridView ID="gvData" DataKeyNames="SrNo" CssClass="Grid" AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr" runat="server" Style="overflow: auto;" AutoGenerateColumns="False" Width="100%"
                        AllowPaging="true" PageSize="15" OnPageIndexChanging="gvData_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="SrNo" HeaderText="Sr No" />
                            <asp:BoundField DataField="LAN" HeaderText="LAN Number" />
                            <asp:BoundField DataField="Barcode" HeaderText="Barcode" />
                            <asp:BoundField DataField="Customer_Name" HeaderText="Customer Name" />
                            <asp:BoundField DataField="BC_Name" HeaderText="BC Name" />
                            <asp:BoundField DataField="BC_Branch" HeaderText="BC Branch" />
                            <asp:BoundField DataField="Disbursement_Date" HeaderText="Disbursement Date" />
                            <asp:BoundField DataField="FileName" HeaderText="File Name" />
                           <%-- <asp:BoundField DataField="File Path" HeaderText="FilePath" />--%>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
