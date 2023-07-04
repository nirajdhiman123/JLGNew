﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/JLG_Master.Master" AutoEventWireup="true" CodeBehind="frmMISReport.aspx.cs" Inherits="JLG.Forms.frmMISReport" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="head-r1x">
            <span class="headx">MIS Report</span>
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
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonx" OnClick="btnCancel_Click" />
                    <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="buttonx" OnClick="btnExportToExcel_Click" />
                </div>
            </div>
            <div class="row Separator">
            </div>
            <div class="row" style="overflow: auto;">
                <div class="col-sm-12">
                    <asp:GridView ID="gvData" CssClass="Grid" AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr" runat="server" Style="overflow: auto;" AutoGenerateColumns="true" Width="100%"
                        BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                        AllowPaging="True" PageSize="15" OnPageIndexChanging="gvData_PageIndexChanging">

                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
