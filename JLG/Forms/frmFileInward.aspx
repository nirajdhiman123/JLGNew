<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/JLG_Master.Master" AutoEventWireup="true" CodeBehind="frmFileInward.aspx.cs" Inherits="JLG.Forms.frmFileInward" %>

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
            <span class="headx">File Inward</span>
        </div>
        <div class="block1x form-horizontal">
            <div class="row">
                <div class="col-sm-5">
                    <%--<div class="row">--%>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="ddlBranch">Location:</label>
                        <div class="col-sm-7">
                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" TabIndex="1"></asp:DropDownList>
                           <%-- <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>--%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="ddlCourierName">Courier Name:</label>
                        <div class="col-sm-7">
                            <asp:DropDownList ID="ddlCourierName" runat="server" CssClass="form-control" TabIndex="4"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="txtFileBarcode">Remark:</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TabIndex="6"></asp:TextBox>
                        </div>
                    </div>
                    
                    <%-- </div>--%>
                </div>

                <div class="col-sm-5">
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="txtPODNumber">POD  Number:</label>
                        <div class="col-sm-5">
                            <asp:TextBox ID="txtPODNumber" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="txtInwardDate">Received Date:</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtReceivedDate" runat="server" CssClass="youpi" Width="150px" TabIndex="5"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="txtFileBarcode">File Barcode:</label>
                        <div class="col-sm-5">
                            <asp:TextBox ID="txtFileBarcode" runat="server" CssClass="form-control" AutoPostBack="true"
                                OnTextChanged="txtFileBarcode_TextChanged" TabIndex="7"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12" style="text-align: center">
                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="buttonx" OnClick="btnClose_Click" TabIndex="8" />
                </div>
            </div>
            <div class="row Separator">
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="gvInward" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%"
                        CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="LAN" HeaderText="LAN">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Barcode" HeaderText="Barcode">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Disbursement_Date" HeaderText="Disbursement Date">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Inward_Date" HeaderText="Inward Date">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                             <asp:BoundField DataField="POD_Number" HeaderText="POD Number">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                             <asp:BoundField DataField="Inward_Remark" HeaderText="Inward Remark">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Exaternalcustno" HeaderText="Exaternalcustno">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BC_Branch" HeaderText="BC Branch">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Center_Name" HeaderText="Center Name">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Group_Name" HeaderText="Group Name">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
