<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/JLG_Master.Master" AutoEventWireup="true" CodeBehind="frmChangePassword.aspx.cs" Inherits="JLG.Forms.frmChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function CheckPassword(char) {

            //var decimal = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,15}$/;
            var decimal = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=(.*[^a-zA-Z0-9])(?=.*[!@#$*()+=\;,./{}|\:\[\]\\\_]){2})(?!.*\s).{8,15}$/;
            //var decimal = 'a';
            if (char.value.match(decimal)) {
                //alert('Correct')
                RetVal = true;

            }
            else {
                alert('A minimum 8 characters password contains a combination of uppercase and lowercase letter,number and two special character are required.')
                RetVal = false;
            }
            return RetVal;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="head-r1x">
            <span class="headx">Change Password</span>
        </div>
        <div class="block1x form-horizontal">
            <div class="row">
               <h5 style="color:#ff6a00;"><b>Note - </b>A minimum 8 characters password contains a combination of uppercase and lowercase letter,number and two special character are required.</h5>
            </div>
            <div class="row">
                <div class="col-sm-8">
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="txtOldPWD">Old Password<span class="Star">*</span> :</label>
                        <div class="col-sm-5">
                            <asp:TextBox ID="txtOldPWD" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="txtNewPWD">New Password<span class="Star">*</span> :</label>
                        <div class="col-sm-5">
                            <asp:TextBox ID="txtNewPWD" runat="server" CssClass="form-control"></asp:TextBox>
                            <%--onblur="return CheckPassword(this)"--%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="txtConNewPWD">Confirm New Password<span class="Star">*</span> :</label>
                        <div class="col-sm-5">
                            <asp:TextBox ID="txtConNewPWD" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6" style="text-align: center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="buttonx" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonx" OnClick="btnCancel_Click" />
                </div>
            </div>

        </div>
    </div>
</asp:Content>
