<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/JLG_Master.Master" AutoEventWireup="true" CodeBehind="frmBranchMaster.aspx.cs" Inherits="JLG.Forms.frmBranchMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function CheckNumericKey(char1) {
             if (char1 >= 48 && char1 <= 57) {
                 RetVal = true;
             }
             else {
                 RetVal = false;
             }
             return RetVal;
         }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="head-r1x">
            <span class="headx">Create Branch</span>
        </div>
        <div class="block1x form-horizontal">
            <div class="row">
                <div class="col-sm-6">
                    <div class="row">
                        <div class="form-group">
                            <label class="control-label col-sm-3" for="Name">Location Name:</label>
                            <div class="col-sm-7">
                                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-sm-3" for="Name">Branch Name:</label>
                            <div class="col-sm-7">
                                <asp:TextBox ID="txtBranchName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-sm-3" for="Name">Email ID:</label>
                            <div class="col-sm-7">
                                <asp:TextBox ID="txtEmailId" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-sm-3" for="Name">Phone No:</label>
                            <div class="col-sm-7">
                                <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="form-control" MaxLength="10" OnKeyPress="return CheckNumericKey(event.keyCode)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-sm-3" for="Name">Is Active:</label>
                            <div class="col-sm-5" style="margin: 5px 0 0 5px">
                                <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="Name">Address 1:</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="Name">Address 2:</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="Name">Address 3:</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtAddress3" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="Name">Address 4:</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtAddress4" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="Name">Pin Code:</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtPincode" runat="server" CssClass="form-control" MaxLength="6" OnKeyPress="return CheckNumericKey(event.keyCode)"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="Name">City:</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="Name">State:</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtState" runat="server" CssClass="form-control"></asp:TextBox>
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
                <div class="row col-sm-10">
                    <asp:GridView ID="gvBranch" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%"
                        CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HiddenField ID="Branch_Id" runat="server" Value='<%# Bind("Branch_Id") %>' />
                                    <%#Container.DataItemIndex+1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Branch_Name" HeaderText="Branch Name">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Loc_Name" HeaderText="Location Name">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Address" HeaderText="Address">
                                <ItemStyle Width="250px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PinCode" HeaderText="Pincode">
                                <ItemStyle Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EmailID" HeaderText="Email ID">
                                <ItemStyle Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PhoneNo" HeaderText="Phone No">
                                <ItemStyle Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IsActive" HeaderText="IsActive">
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEdit" runat="server" ImageAlign="Middle"
                                        ImageUrl="~/Images/UI_dataentry.png" ToolTip='<%# Bind("Branch_Id") %>' OnClick="imgEdit_Click" />
                                </ItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
