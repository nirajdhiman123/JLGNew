<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/JLG_Master.Master" AutoEventWireup="true" CodeBehind="frmHome.aspx.cs" Inherits="JLG.Forms.frmHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../Styles/jquery-ui.css" />

    <script src="../Scripts/jquery-1.12.4.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.min_1.8.3.js"></script>
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
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 3px solid #FDB813;
            width: 200px;
            height: 120px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <img src="../Images/loading.gif" alt=""/>
        </div>
        <div class="head-r1x">
            <span class="headx">Dashboard</span>
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
                    <asp:Button ID="btnSubmit" runat="server" Text="Search" CssClass="buttonx" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonx" OnClick="btnRefresh_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonx" OnClick="btnCancel_Click" />
                </div>
            </div>
            <div class="row Separator">
            </div>
            <div class="row" style="overflow: auto;">
                <div class="col-sm-12">
                    <asp:GridView ID="gvData" CssClass="Grid" AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr" runat="server" Style="overflow: auto;" AutoGenerateColumns="false" Width="100%"
                        BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                        <Columns>
                            <asp:BoundField DataField="ItemDesc" HeaderText="Item Desc" HtmlEncode="false">
                                <ItemStyle Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SuccessCount" HeaderText="Success Count" HtmlEncode="false">
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RejectCount" HeaderText="Reject Count" HtmlEncode="false">
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PendingCount" HeaderText="Pending Count" HtmlEncode="false">
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalCount" HeaderText="Total Count" HtmlEncode="false">
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>

                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
