<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="JLG.frmLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="stylesheet" href="Styles/StyleSheet.css" type="text/css" />
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css" type="text/css" />
    <style type="text/css">
        .style1 {
            margin: 10% 25%;
            padding: 20px 20px;
            background: #fff;
            text-align: left;
            width: 50%;
            /*                margin: 55px auto;*/
            font-family: lucida grande, verdana;
            font-size: 12px;
            -webkit-border-radius: 10px;
            -moz-border-radius: 10px;
            -moz-box-shadow: 0 0 6px #999;
            -webkit-box-shadow: 0 0 6px #999;
            border: 1px solid #ccc;
            background-image: linear-gradient(to right,rgba(255,255,255,0),rgba(253,184,19,1));
            opacity:0.8;
        }

        .style7 {
            font-size: 10pt;
            font-weight: bold;
        }

        .style8 {
            font-size: 15pt;
            font-weight: bold;
            text-align: center;
            color: #009999;
        }

        .buttonx {
            background: #EEF7EA none 0 0 repeat-x;
            font-family: 'Helvetica Neue', Helvetica, sans-serif;
            border: solid 1px #adc0ed;
            color: #2e4987;
            cursor: pointer;
            display: block;
            width: 100%;
            font-weight: bold;
            font-size: 12px;
            line-height: 100%;
            margin: 1px;
            padding: 0.83em 1em;
            position: relative;
            text-decoration: none;
            text-shadow: #fff 0 1px 0;
            vertical-align: middle;
            white-space: nowrap;
            /* gecko */
            -moz-user-select: none;
            -moz-border-radius: 6px;
            background-image: -moz-linear-gradient(-90deg, #eeeeee 1%, #f3f5fa 5%, #e4eafa 70%, #c9d5f6 100%);
            /* webkit */
            -webkit-user-select: none;
            -webkit-border-radius: 6px;
            background-image: -webkit-gradient(linear,left top,left bottom, color-stop(0.01, #eee), color-stop(0.05, #f3f5fa), color-stop(0.70, #e4eafa), color-stop(1, #c9d5f6));
        }


        .form__input {
            width: 100%;
            border: 0px solid transparent;
            border-radius: 0;
            border-bottom: 1px solid #aaa;
            padding: 1em .5em .5em;
            padding-left: 2em;
            outline: none;
            margin: 1.5em auto;
            transition: all .5s ease;
        }

            .form__input:focus {
                border-bottom-color: #008080;
                box-shadow: 0 0 5px rgba(0,80,80,.4);
                border-radius: 4px;
            }

        .style9 {
            font-size: 12pt;
            font-weight: bold;
            text-align: center;
            color: #008080;
        }

        .input-group-addon {
            background-color: #FDB813;
            color: black;
            border: 0 !important;
        }

        .bg {
            /* The image used */
            background-image: url("Images/loginbg.jpg");
            /*background-image: repeating-linear-gradient(to left,rgba(255,255,255,0),rgba(200,185,148,1));*/
            /* Full height */
            height: 100%;
            /* Center and scale the image nicely */
            background-position: center;
            background-repeat: no-repeat;
            background-size: cover;
            opacity:0.5;
        }
    </style>
</head>
<body>
    <div>
        <img src="Images/loginbg.jpg" style="position:absolute;opacity:0.7;height:100%;width:100%"/>
        <form id="form1" runat="server" style="position:relative">
            <div class="container-fluid">
                <div>
                    <div class="navbar-header" style="float: left;">
                        <a class="navbar-brand" href="frmLogin.aspx">
                            <img src="Images/logo_writer_new.png" class="img-responsive" alt="Writer" width="180px" />
                        </a>
                    </div>
                </div>
                <div class="row style1">
                    <div class="col-md-5">
                        <div class="navbar-header" style="float: left; margin: 10% 0 0 0">
                            <img src="Images/Img1.png" class="img-responsive" alt="Writer" width="200px" />
                        </div>
                    </div>

                    <div class="col-md-7">
                        <div class="style8"
                            style="border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #808080">
                            <strong>JLG - Process</strong>
                        </div>
                        <div class="col-md-12 col-centered">
                            <div class="modal-body">
                                <div class="row">
                                    <div class="style9">
                                        <strong>Member Login</strong>
                                    </div>
                                </div>
                                <div class="row input-group modal-content" style="margin: 10% 0 5% 0">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:TextBox runat="server" ID="txtUserName" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="row input-group modal-content" style="margin: 0 0 5% 0">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                                    <asp:TextBox runat="server" TextMode="Password" ID="txtPassword" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="row input-group col-md-12" style="margin: 15% 0 2% 0">
                                    <asp:Button CssClass="buttonx" ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
