<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/JLG_Master.Master" AutoEventWireup="true" CodeBehind="frmPDD_DataUpload.aspx.cs" Inherits="JLG.Forms.frmPDD_DataUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--<link rel="stylesheet" href='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css' media="screen" />--%>
    <link rel="stylesheet" href="../Styles/jquery-ui.css" />

    <script src="../Scripts/jquery-1.12.4.js"></script>
    <%--    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
    <script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js'></script>--%>

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


        function Reuploadconfirm() {
            if (confirm("This file already uploaded,Are you sure you want to upload again..?")) {
                //debugger;
                document.getElementById('<%=btnReuploadFile.ClientID %>').click();
            }

        }

        //function ShowPopup() {

        //    $("#myModal").modal("show");
        //}

    </script>
    <style>
        .hidebtn {
            display: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="head-r1x">
            <span class="headx">PDD Data Upload</span>
        </div>
        <div class="block1x form-horizontal">
            <asp:Panel ID="ErrPanel" runat="server">
            </asp:Panel>
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="FileUpload1">Branch:</label>
                        <div class="col-sm-7">
                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3" for="FileUpload1">Date:</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtInwardDate" runat="server" CssClass="youpi" Width="150px"></asp:TextBox>
                        </div>
                    </div>
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
                            <asp:Button ID="btnReuploadFile" runat="server" Text="ReUpload File" CssClass="buttonx hidebtn" OnClick="btnReuploadFile_Click" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="buttonx" OnClick="btnExportToExcel_Click" />
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
                            <br />
                            <br />
                        </div>
                    </div>
                </div>
            </div>
            <%--<div class="row">
                <div class="col-sm-12">
                    <asp:GridView ID="gvUpdloadDataDiv" runat="server" AutoGenerateColumns="false" GridLines="Both" CssClass="Grid"
                        AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" Style="overflow: auto;" AllowPaging="true" PageSize="15"
                        Width="100%" OnPageIndexChanging="gvUpdloadDataDiv_PageIndexChanging" OnSelectedIndexChanged="gvUpdloadDataDiv_SelectedIndexChanged"
                         >
                        <Columns>                            
                            <asp:TemplateField HeaderText="File ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblFileID" runat="server" Text='<%#Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>    
                            <asp:TemplateField HeaderText="File Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblFileName" runat="server" Text='<%#Bind("FileName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>    
                            <asp:TemplateField HeaderText="Total Rows Uploaded">
                                <ItemTemplate>                                    
                                    <asp:Label ID="lblRowsCount" runat="server" Text='<%#Bind("Success_Row_Count") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                           
                            <asp:TemplateField HeaderText="Error Rows Count">
                                <ItemTemplate>
                                    
                                    <asp:Label ID="lblErrorCount" runat="server" Text='<%# Bind("ErrorCount") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField Text="Select" CommandName="Select" HeaderText="Download Erro Log"/>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>--%>
            <div class="row">
                <div class="col-sm-12">
                    <asp:GridView ID="gvData" DataKeyNames="SrNo" CssClass="Grid" AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr" runat="server" Style="overflow: auto;" AutoGenerateColumns="False" Width="100%"
                        AllowPaging="true" PageSize="15" OnPageIndexChanging="gvData_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="SrNo" HeaderText="Sr No" />
                            <asp:BoundField DataField="LAN" HeaderText="LAN" />
                            <asp:BoundField DataField="Barcode" HeaderText="Barcode" />
                            <asp:BoundField DataField="State" HeaderText="State" />
                            <asp:BoundField DataField="BC_Name" HeaderText="BC Name" />
                            <asp:BoundField DataField="Customer_Name" HeaderText="Customer Name" />
                            <asp:BoundField DataField="Exaternalcustno" HeaderText="Exaternalcustno" />
                            <asp:BoundField DataField="BC_Branch" HeaderText="BC Branch" />
                            <asp:BoundField DataField="Center_Name" HeaderText="Center Name" />
                            <asp:BoundField DataField="Group_Name" HeaderText="Group Name" />
                            <asp:BoundField DataField="Disbursement_Date" HeaderText="Disbursement Date" DataFormatString="{0:dd/MMM/yyyy}" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <%--<div id="myModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            &times;</button>
                        <h4 class="modal-title">Confirmation</h4>
                    </div>
                    <div class="modal-body">
                        <p>
                            This file already uploaded,Are you sure you want to upload again..?
                        </p>

                    </div>
                    <div class="modal-footer">

                        <asp:Button runat="server" ID="btnYes" Text="Yes" class="btn btn-default"
                            OnClick="btnYes_Click" UseSubmitBehavior="false" data-dismiss="modal" />
                        <asp:Button runat="server" ID="btnNo" Text="No" class="btn btn-default"
                            OnClick="btnNo_Click" UseSubmitBehavior="false" data-dismiss="modal" />
                    </div>
                </div>
            </div>
        </div>--%>
    </div>
</asp:Content>
