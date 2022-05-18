<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print.aspx.cs" Inherits="Gyomu.Mitumori.Print" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="https://code.jquery.com/jquery-3.1.0.min.js"></script>

    <title>印刷ページ</title>
    <style type="text/css">
        body {
            font-family: 'Yu Gothic UI';
        }

        * {
            margin: 0;
            padding: 0;
        }

        #overlay {
            position: fixed;
            top: 0;
            z-index: 100;
            width: 100%;
            height: 100%;
            display: none;
            background: rgba(0,0,0,0.6);
        }

        .cv-spinner {
            height: 100%;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .spinner {
            width: 40px;
            height: 40px;
            border: 4px #ddd solid;
            border-top: 4px #2e93e6 solid;
            border-radius: 50%;
            animation: sp-anime 0.8s infinite linear;
        }

        @keyframes sp-anime {
            100% {
                transform: rotate(360deg);
            }
        }

        .is-hide {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="overlay">
            <div class="cv-spinner">
                <span class="spinner"></span>
                <script type="text/javascript">
                    jQuery(function ($) {
                        $(document).ajaxSend(function () {
                            $("#overlay").fadeIn(300);
                        });

                        $('#BtnPrint').click(function () {
                            $.ajax({
                                type: 'GET',
                                success: function (data) {
                                    console.log(data);
                                }
                            }).done(function () {
                                setTimeout(function () {
                                    $("#overlay").fadeOut(300);
                                }, 100000);
                            });
                        });
                    });
                </script>
            </div>
        </div>

        <div>
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
            <table runat="server" style="border: solid 1px #2e75b6;">
                <tr>
                    <td runat="server" style="background-color: #b3c6e7;">
                        <p>見積No.</p>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="LblMitumoriNo"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td runat="server" style="background-color: #b3c6e7;">
                        <p>出力フォーマット</p>
                    </td>
                    <td>
                        <%--                        <asp:RadioButton ID="RbMitumori" runat="server" Text="見積書" /><br />
                        <asp:RadioButton ID="RbNouhin" runat="server" Text="納品書" /><br />
                        <asp:RadioButton ID="RbSeikyu" runat="server" Text="請求書" /><br />--%>
                        <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                            <asp:ListItem Text="見積書" Value="Mitumori"></asp:ListItem>
                            <asp:ListItem Text="納品書" Value="Nouhin"></asp:ListItem>
                            <asp:ListItem Text="請求書" Value="Sekyu"></asp:ListItem>
                            <asp:ListItem Text="内訳書" Value="Uchiwake"></asp:ListItem>
                            <asp:ListItem Text="出荷伝票" Value="Denpyou"></asp:ListItem>
                        </asp:CheckBoxList>
                        <asp:CheckBox runat="server" ID="ChkName" Text="代表者名" BackColor="#e5eeff" />
                        <br />
                        <asp:CheckBox runat="server" ID="ChkDate" Text="日付無し" BackColor="#e5eeff" OnCheckedChanged="ChkDate_CheckedChanged" />
                        <script type="text/javascript">
</script>
                        <br />
                        <div runat="server" id="DivDate">
                            <p>日付指定</p>
                            <telerik:RadDatePicker runat="server" ID="RdpDate"></telerik:RadDatePicker>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; background-color: #b3c6e7;">
                        <asp:Button runat="server" Text="印刷" ID="BtnPrint" OnClick="BtnPrint_Click" OnClientClick="Status()" />
                        <script type="text/javascript">
                            function Status() {
                                debugger;
                                var end = document.getElementById('LblEnd');
                                end.innerText = "印刷中...";
                            }
                        </script>
                    </td>
                </tr>
            </table>
        </div>
        <asp:Label runat="server" ID="err" ForeColor="Red"></asp:Label>
        <asp:Label runat="server" ID="LblEnd" ForeColor="Green"></asp:Label>
        <telerik:RadAjaxManager ID="Ram" runat="server" OnAjaxRequest="Ram_AjaxRequest">
            <%--<ClientEvents OnResponseEnd="OnResponseEnd" OnRequestStart="OnRequestStart" /> --%>
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
