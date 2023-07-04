<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/JLG_Master.Master" AutoEventWireup="true" CodeBehind="frmCreateBC.aspx.cs" Inherits="JLG.Forms.frmCreateBC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <div class="head-r1x">
            <span class="headx">Create BC</span>
        </div>
        <div class="block1x form-horizontal">
            <div class="row">
                <div class="col-sm-6">
                    <div class="row">
                        <div class="form-group">
                            <label class="control-label col-sm-3" for="Name">Group Name:</label>
                            <div class="col-sm-7">
                                <asp:TextBox ID="txtGroupName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                        </div>
                        <div class="form-group">
                            <label class="control-label col-sm-3" for="Name">Is Active:</label>
                            <div class="col-sm-5" style="margin: 5px 0 0 5px">
                                <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" />
                            </div>
                        </div>
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
                    <div class="row col-sm-10">
                        <asp:GridView ID="gvUserDetails" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%"
                            CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="HFUser_Id" runat="server" Value='<%# Bind("BC_Id") %>' />
                                        <%#Container.DataItemIndex+1%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                </asp:TemplateField>
                                <%-- <asp:BoundField DataField="GroupId" HeaderText="Group ID">
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:BoundField>--%>
                                <asp:BoundField DataField="BC_Name" HeaderText="BC Name">
                                    <ItemStyle Width="200px" />
                                </asp:BoundField>
                                <%--   <asp:BoundField DataField="CreatedBy" HeaderText="Created By" Visible="false">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UpdatedBy" HeaderText="UpdatedBy" Visible="false">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>--%>
                                <asp:BoundField DataField="IsActive" HeaderText="IsActive">
                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEdit" runat="server" ImageAlign="Middle"
                                            ImageUrl="~/Images/UI_dataentry.png" ToolTip='<%# Bind("BC_Id") %>' OnClick="imgEdit_Click" />
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                
            </div>

        </div>
    </div>
</asp:Content>
