<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/JLG_Master.Master" AutoEventWireup="true" CodeBehind="frmDeleteDataByBarcode.aspx.cs" Inherits="JLG.Forms.frmDeleteDataByBarcode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="head-r1x">
            <span class="headx">Data Delete By Barcode</span>
        </div>
        <div class="block1x form-horizontal">
            <asp:Panel ID="ErrPanel" runat="server">
            </asp:Panel>
            <div class="row">
                <div class="col-sm-5">

                    <%--<div class="form-group">
                        <label class="control-label col-sm-3" for="txtFileBarcode">File Barcode:</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtFileBarcode" runat="server" CssClass="form-control" AutoPostBack="true"
                                OnTextChanged="txtFileBarcode_TextChanged" TabIndex="7"></asp:TextBox>
                        </div>
                    </div>--%>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="FileUpload1">Select File:</label>
                        <div class="col-sm-9">
                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-3">
                        </div>
                        <div class="col-sm-7">
                            <asp:LinkButton ID="lnkExcelFormat" runat="server" Text="Download excel format" OnClick="lnkExcelFormat_Click"></asp:LinkButton>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-3">
                            <asp:HiddenField ID="hflogFileName" runat="server" />
                            <asp:HiddenField ID="hfLogFilePath" runat="server" />
                            <asp:HiddenField ID="hdnRpath" runat="server" />
                            <asp:HiddenField ID="hdnRfile" runat="server" />
                        </div>
                        <%--<div class="col-sm-7">
                            <asp:LinkButton ID="lnkbtnLogFile" runat="server" Text="Download log file for failed LAN" Visible="false" OnClick="lnkbtnLogFile_Click"></asp:LinkButton>
                        </div>--%>
                    </div>
                </div>

                <div class="col-sm-1">
                    <asp:Button ID="btnUploadFile" runat="server" Text="Upload File" CssClass="buttonx" OnClick="btnUploadFile_Click" />
                </div>
                <div class="col-sm-1">
                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="buttonx" OnClick="btnClose_Click" TabIndex="8" />
                </div>
            </div>
            <div class="row Separator">
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" BackColor="White"
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
                            <asp:BoundField DataField="FileName" HeaderText="File Name">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Customer_Name" HeaderText="Customer Name">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="Exaternalcustno" HeaderText="Exaternalcustno">
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
                            </asp:BoundField>--%>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
