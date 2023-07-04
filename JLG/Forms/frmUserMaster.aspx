<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/JLG_Master.Master" AutoEventWireup="true" CodeBehind="frmUserMaster.aspx.cs" Inherits="JLG.Forms.frmUserMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script>
         function NumberOnly() {
             var AsciiValue = event.keyCode
             if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                 event.returnValue = true;
             else
                 event.returnValue = false;
         }

     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="head-r1x">
            <span class="headx">Create User</span>
        </div>
        <div class="block1x form-horizontal">
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="rdnUserType">User Type<span class="Star">*</span> :</label>
                        <div class="col-sm-7" style="margin: 5px 0 0 5px">
                            <asp:RadioButtonList ID="rdnUserType" runat="server" RepeatDirection="Horizontal" Width="150px">
                                <asp:ListItem Value="A">&nbsp;Admin</asp:ListItem>
                                <asp:ListItem Value="U">&nbsp;User</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="txtUserName">User Name<span class="Star">*</span> :</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" OnTextChanged="txtUserName_TextChanged" AutoPostBack="false"></asp:TextBox>
                        </div>
                    </div>

                    <%--  <div class="form-group">
                        <label class="control-label col-sm-3" for="Name">Default Pwd:</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>--%>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="ddlGroup">Group <span class="Star">*</span>:</label>
                        <div class="col-sm-7">
                            <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="ddlBranch">Branch<span class="Star">*</span> :</label>
                        <div class="col-sm-7">
                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="chkIsActive">Is Active <span class="Star">*</span> :</label>
                        <div class="col-sm-5" style="margin: 5px 0 0 5px">
                            <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" />
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="txtFirstName">First Name<span class="Star">*</span> :</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="txtLastName">Last Name<span class="Star">*</span> :</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="txtEmpCode">Emp Code<span class="Star">*</span> :</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="txtEmailId">Email ID<span class="Star">*</span> :</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtEmailId" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="txtMobileNo">Mobile No<span class="Star">*</span> :</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" onkeypress="return NumberOnly()"></asp:TextBox>
                        </div>
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
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group"> 
                        <label class="control-label col-sm-3" for="Name">Login Text:</label>
                        <div class="col-sm-5">
                            <asp:TextBox ID="txtLoginText" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-2">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="buttonx" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                </div>

                <%--   -------------------------    Added By Kapil Singhal on 12th Dec. 2022    ------------------------------------------   --%>

                <div class="col-sm-2 col-sm-offset-4">
                    <asp:Button ID="btnDownloadUserMaster" runat="server" Text="Download User Master" CssClass="buttonx" OnClick="btnDownloadUserMaster_Click" />
                </div>


                 <%--   -------------------------    ------------------------------------------    ------------------------------------------   --%>

            </div>
            <div class="row" style="overflow: auto;">
                <div class="col-sm-12">
                    <asp:GridView ID="gvUserDetails" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                        BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" AllowPaging="True" PageSize="10"
                        CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnPageIndexChanging="gvUserDetails_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HiddenField ID="HFUser_Id" runat="server" Value='<%# Bind("UserId") %>' />
                                     <asp:HiddenField ID="hdnEmailId" runat="server" Value='<%# Bind("EmailId") %>' />
                                     <asp:HiddenField ID="hdnLoginText" runat="server" Value='<%# Bind("Login_Text") %>' />
                                    <asp:HiddenField ID="hdnEmpName" runat="server" Value='<%# Bind("First_Name") %>' />
                                    <%#Container.DataItemIndex+1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="UserId" HeaderText="User ID">
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="EmpCode" HeaderText="EmpCode">
                                <ItemStyle Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Login_Text" HeaderText="UserName">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IsActive" HeaderText="IsActive">
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UserType" HeaderText="UserType">
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Branch_Name" HeaderText="Branch">
                                <ItemStyle Width="150px" HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField DataField="Branch_Name" HeaderText="Branch">
                                <ItemStyle Width="150px" HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField DataField="DefaultPWD" HeaderText="Default PWD">
                                <ItemStyle Width="150px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEdit" runat="server" ImageAlign="Middle" OnClick="imgEdit_Click"
                                        ImageUrl="~/Images/UI_dataentry.png" ToolTip='<%# Bind("UserId") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reset Password">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkrest" runat="server" ToolTip='<%# Bind("UserId") %>' OnClick="lnkrest_Click">Reset</asp:LinkButton>
                                    <%-- <asp:ImageButton ID="imgEdit" runat="server" ImageAlign="Middle"
                                        ImageUrl="~/Images/UI_dataentry.png" CssClass='<%# Bind("UserId") %>'
                                        OnClick="imgEdit_Click" />--%>
                                </ItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <%--  <FooterStyle BackColor="White" ForeColor="#000000" />
                        <HeaderStyle BackColor="#FFB300" Font-Bold="True" ForeColor="#000000" />
                        <PagerStyle BackColor="White" ForeColor="#000000" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" ForeColor="#000000" />
                        <SelectedRowStyle BackColor="#FFB300" Font-Bold="True" ForeColor="#663399" />
                        <SortedAscendingCellStyle BackColor="#FEFCEB" />
                        <SortedAscendingHeaderStyle BackColor="#AF0101" />
                        <SortedDescendingCellStyle BackColor="#F6F0C0" />
                        <SortedDescendingHeaderStyle BackColor="#7E0000" />--%>
                    </asp:GridView>
                </div>
            </div>
        </div>

    </div>

</asp:Content>
