<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/JLG_Master.Master" AutoEventWireup="true" CodeBehind="frmArchivalDataUpload.aspx.cs" Inherits="JLG.Forms.frmArchivalDataUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="head-r1x">
            <span class="headx">Archival Data Upload</span>
        </div>
        <div class="block1x form-horizontal">
            <asp:Panel ID="ErrPanel" runat="server">
            </asp:Panel>
            <div class="row">
                <div class="col-sm-6">            
                   <%-- <div class="form-group">
                        <label class="control-label col-sm-3" for="FileUpload1">Date:</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtInwardDate" runat="server" CssClass="youpi" Width="150px"></asp:TextBox>
                        </div>
                    </div>--%>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="FileUpload1">Select File:</label>
                        <div class="col-sm-7">
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
                        <div class="col-sm-3"></div>
                        <div class="col-sm-7">
                            <asp:Button ID="btnUploadFile" runat="server" Text="Upload File" CssClass="buttonx" OnClick="btnUploadFile_Click" />               
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-3">
                            <asp:HiddenField ID="hflogFileName" runat="server" />
                            <asp:HiddenField ID="hfLogFilePath" runat="server" />
                            <asp:HiddenField ID="hdnRpath" runat="server" />
                            <asp:HiddenField ID="hdnRfile" runat="server" />
                        </div>
                        <div class="col-sm-7">
                            <asp:LinkButton ID="lnkbtnLogFile" runat="server" Text="Download log file for failed LAN" Visible="false" OnClick="lnkbtnLogFile_Click"></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <asp:GridView ID="gvData" DataKeyNames="SrNo" CssClass="Grid" AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr" runat="server" Style="overflow: auto;" AutoGenerateColumns="False" Width="100%"
                        AllowPaging="true" PageSize="15" OnPageIndexChanging="gvData_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="SrNo" HeaderText="Sr No" />
                            <asp:BoundField DataField="LAN" HeaderText="LAN" />
                            <asp:BoundField DataField="Barcode" HeaderText="Barcode" />
                            <asp:BoundField DataField="Customer_Name" HeaderText="Customer Name" />
                            <asp:BoundField DataField="Archival_Status" HeaderText="Archival Status" />
                            <asp:BoundField DataField="Archival_Date" HeaderText="Archival Date" DataFormatString="{0:dd/MMM/yyyy}"/>
                            <asp:BoundField DataField="Lot_No" HeaderText="Lot No" />
                            <asp:BoundField DataField="Carton_No" HeaderText="Carton No" />
                            <asp:BoundField DataField="File_Code" HeaderText="File Code" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
