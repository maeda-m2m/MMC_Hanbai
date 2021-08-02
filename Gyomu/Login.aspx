<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Gyomu.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .logo {
            width: 110px;
            height: 65px;
        }

        .center {
            text-align: center;
        }

        .BtnLog {
            text-align: right;
        }

        .width {
            width: 100%;
        }

        .BackColor1 {
            background-color: #808080;
        }

        .BackColor2 {
            background-color: #000000;
            color: white
        }

        h3 {
            border-bottom: solid 3px #808080;
            position: relative;
        }

            h3:after {
                position: absolute;
                content: " ";
                display: block;
                border-bottom: solid 3px #000000;
                bottom: -3px;
                width: 50%;
            }

        .btn-flat-vertical-border {
            position: relative;
            display: inline-block;
            font-weight: bold;
            padding: 0.5em 1em;
            text-decoration: none;
            border-left: solid 4px #000000;
            border-right: solid 4px #000000;
            color: white;
            background: #808080;
            transition: .4s;
            text-align: right;
        }

            .btn-flat-vertical-border:hover {
                background: #000000;
                color: #FFF;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <table id="T001" cellspacing="0" border="0">
                <tr>
                    <td>
                        <img alt="" src="Img/ロゴ.gif" class="logo" id="rogo" runat="server" />
                    </td>
                    <td>
                        <strong>
                            <asp:Label ID="Syamei" runat="server" Text="株式会社ムービーマネジメントカンパニー" Font-Size="X-Large"></asp:Label>
                        </strong>
                    </td>
                </tr>
            </table>
            <table class="width">
                <tr>
                    <td class="width">
                        <h3></h3>
                    </td>
                </tr>
            </table>

            <table>
                <tbody>
                    <tr>
                        <td>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="4" border="1">
                                                <tbody>
                                                    <tr>
                                                        <td class="BackColor2">

                                                            <strong>

                                                                <asp:Label ID="Label1" runat="server" Text="ログインID"></asp:Label>

                                                            </strong>

                                                        </td>
                                                        <td class="BackColor1">

                                                            <asp:TextBox ID="TbxLogin" runat="server"></asp:TextBox>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="BackColor2">
                                                            <strong>
                                                                <asp:Label ID="Label2" runat="server" Text="パスワード"></asp:Label>

                                                            </strong>

                                                        </td>
                                                        <td class="BackColor1">
                                                            <asp:TextBox ID="TbxPass" runat="server" TextMode="Password"></asp:TextBox>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" class="BtnLog">
                                                            <strong>
                                                                <asp:Button ID="BtnRogin" runat="server" Text="ログイン" CssClass="btn-flat-vertical-border" OnClick="BtnRogin_Click" />
                                                            </strong>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                        <td>
                                            <table width="30">
                                                <tr>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="＜MMCからのお知らせ＞" ForeColor="Gray" Font-Size="Medium"></asp:Label>
                                            <br />
                                            <asp:TextBox ID="TbxInfomation" runat="server" Height="300px" Width="650px" ReadOnly="true"
                                                TextMode="MultiLine" Font-Size="11" BorderWidth="2" Wrap="true" Style="font-family: Tahoma;"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
            <table class="def" id="TblFooter" style="border-top: #808080 1px solid; margin-top: 4px; border-bottom: #000000 1px solid"
                cellspacing="0" cellpadding="4" width="100%" border="0">
                <tr>
                    <td style="white-space: nowrap">
                        <asp:Label ID="LblCopyright" runat="server" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>


        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <telerik:RadAjaxManager ID="Ram" runat="server" OnAjaxRequest="Ram_AjaxRequest">
            <ClientEvents OnResponseEnd="OnResponseEnd" OnRequestStart="OnRequestStart" />
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="BtnL">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="TblK" />
                        <telerik:AjaxUpdatedControl ControlID="TblList" LoadingPanelID="LP"
                            UpdatePanelHeight="200px" UpdatePanelRenderMode="Inline" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
    </form>
</body>
</html>
