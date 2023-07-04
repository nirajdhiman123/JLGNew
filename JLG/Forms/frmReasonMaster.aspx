<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/JLG_Master.Master" AutoEventWireup="true" CodeBehind="frmReasonMaster.aspx.cs" Inherits="JLG.Forms.frmReasonMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="head-r1x">
            <span class="headx">Create Reason Master</span>
        </div>
        <div class="block1x form-horizontal">
            <div class="row">
                <div class="col-sm-6">
                    <div class="row">
                        <div class="form-group">
                            <label class="control-label col-sm-3">Type :</label>
                            <div class="col-sm-7">
                                <asp:RadioButtonList ID="rdnReportType" runat="server" RepeatDirection="Horizontal" Width="90%"
                                    OnSelectedIndexChanged="rdnReportType_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="D" Selected="True">Document </asp:ListItem>
                                    <asp:ListItem Value="R">Rejection Remarks</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-sm-3">Document Name :</label>
                            <div class="col-sm-7">
                                <asp:DropDownList ID="ddlDocumentRelated" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtDocumentRelated" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label  class="control-label col-sm-3" >Reason Remark :</label>
                            <div class="col-sm-7">
                                <asp:TextBox ID="txtReason" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-sm-3" for="Name">Is Active :</label>
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
                    <div class="row col-sm-12">
                        <asp:GridView ID="gvRejectReason" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%"
                            CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                            AllowPaging="True" PageSize="20" OnPageIndexChanging="gvRejectReason_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="key_id" runat="server" Value='<%# Bind("key_id") %>' />
                                        <%#Container.DataItemIndex+1%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                </asp:TemplateField>
                                <%-- <asp:BoundField DataField="GroupId" HeaderText="Group ID">
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:BoundField>--%>
                                <asp:BoundField DataField="key_name" HeaderText="Document Name">
                                    <ItemStyle Width="200px"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="key_value" HeaderText="Reason Remark">
                                    <ItemStyle  Width="500px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="key_active" HeaderText="IsActive">
                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEdit" runat="server" ImageAlign="Middle"
                                            ImageUrl="~/Images/UI_dataentry.png" ToolTip='<%# Bind("key_id") %>' OnClick="imgEdit_Click" />
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-sm-6">
                </div>
            </div>

        </div>
    </div>
</asp:Content>
