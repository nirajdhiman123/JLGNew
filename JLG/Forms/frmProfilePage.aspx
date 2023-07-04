<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/JLG_Master.Master" AutoEventWireup="true" CodeBehind="frmProfilePage.aspx.cs" Inherits="JLG.Forms.frmProfilePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="head-r1x">
            <span class="headx">User Profile</span>
        </div>
        <div class="block1x form-horizontal">
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="rdnUserType">User Type :</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtUserType" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="txtUserName">User Name :</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="ddlGroup">Group :</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtGroup" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="ddlBranch">Branch :</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="txtFirstName">First Name :</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="txtLastName">Last Name :</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="txtEmpCode">Emp Code :</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="txtEmailId">Email ID :</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtEmailId" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="txtMobileNo">Mobile No :</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="col-md-3" style="border:solid 1px #808080;">
                    <div class="row head-r1x col-sm-12">
                        <label class="control-label" for="Name">Menu Rights:</label>
                    </div>
                    <div class="row col-sm-12" style="height: 400px; overflow: auto; margin: 1% 3%;">
                        <asp:TreeView ID="tvMenu" runat="server" Height="165px" NodeWrap="True">
                        </asp:TreeView>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 text-center">
                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="buttonx" OnClick="btnClose_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
