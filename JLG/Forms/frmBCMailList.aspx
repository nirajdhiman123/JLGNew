<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/JLG_Master.Master" AutoEventWireup="true" CodeBehind="frmBCMailList.aspx.cs" Inherits="JLG.Forms.frmBCMailList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../Styles/jquery-ui.css" />
    <script src="../Scripts/jquery-1.12.4.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="head-r1x">
            <span class="headx">Create BC Mail List</span>
        </div>
        <div class="block1x form-horizontal">
            <div class="row">
                <div class="col-sm-6">
                    <div class="row">
                        <div class="form-group">
                            <label class="control-label col-sm-3" for="Name">BC Name:</label>
                            <div class="col-sm-7">
                                <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"></asp:DropDownList>
                            </div>

                        </div>

                        <div class="form-group">
                            <label class="control-label col-sm-3" for="Name">To Mail Id:</label>
                            <div class="col-sm-7">

                                <asp:TextBox ID="txtToMail" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Button ID="btnToAdd" runat="server" Text="Add" CssClass="buttonx" OnClick="btnToAdd_Click" />
                            </div>

                        </div>


                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="row">
                        <div class="form-group">
                            <label class="control-label col-sm-3" for="Name">BC Code:</label>
                            <div class="col-sm-7">

                                <asp:TextBox ID="txtBCCode" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                        </div>

                        <div class="form-group">
                            <label class="control-label col-sm-3" for="Name">BCC Mail ID:</label>
                            <div class="col-sm-7">
                                <asp:TextBox ID="txtBCCMail" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Button ID="btnBCCAdd" runat="server" Text="Add" CssClass="buttonx" OnClick="btnBCCAdd_Click" />
                            </div>

                        </div>

                    </div>
                </div>
            </div>
            <div class="row Separator">
            </div>
            <div class="row">
                <div class="col-sm-12" style="text-align: center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="buttonx" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonx" OnClick="btnCancel_Click" />
                    <asp:HiddenField ID="hdnEditId" runat="server" />
                </div>
            </div>
            <div class="row Separator">
            </div>

            <div class="row col-sm-6">
                <asp:GridView ID="gvUserDetails" runat="server" AutoGenerateColumns="false" DataKeyNames="ID"
                    OnRowDataBound="OnRowDataBound" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit"
                    OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting" EmptyDataText="No records has been added."
                    BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%"
                    CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="30">
                            <ItemTemplate>
                                <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                            </ItemTemplate>
                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="To Mail" ItemStyle-Width="250">
                            <ItemTemplate>
                                <asp:Label ID="lblToMail" runat="server" Text='<%# Eval("ToMail") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtToMail" runat="server" Text='<%# Eval("ToMail") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" ItemStyle-Width="150" />
                    </Columns>
                </asp:GridView>
            </div>

            <div class="row col-sm-6">
                <asp:GridView ID="gvBCC" runat="server" AutoGenerateColumns="false" DataKeyNames="BCCID"
                    OnRowDataBound="OnBCCRowDataBound" OnRowEditing="OnBCCRowEditing" OnRowCancelingEdit="OnBCCRowCancelingEdit"
                    OnRowUpdating="OnBCCRowUpdating" OnRowDeleting="OnBCCRowDeleting" EmptyDataText="No records has been added."
                    BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%"
                    CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="30">
                            <ItemTemplate>
                                <asp:Label ID="lblBCCId" runat="server" Text='<%# Eval("BCCId") %>'></asp:Label>
                            </ItemTemplate>
                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BCC Mail" ItemStyle-Width="250">
                            <ItemTemplate>
                                <asp:Label ID="lblBCCMail" runat="server" Text='<%# Eval("BCCMail") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtBCCMail" runat="server" Text='<%# Eval("BCCMail") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" ItemStyle-Width="150" />
                    </Columns>
                </asp:GridView>
            </div>




        </div>
    </div>
</asp:Content>
