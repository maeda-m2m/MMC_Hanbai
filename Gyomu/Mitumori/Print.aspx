<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print.aspx.cs" Inherits="Gyomu.Mitumori.Print" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
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
                        <asp:CheckBox runat="server" ID="ChkDate" Text="日付無し" BackColor="#e5eeff" />

                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; background-color: #b3c6e7;">
                        <asp:Button runat="server" Text="印刷" ID="BtnPrint" OnClick="BtnPrint_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:Label runat="server" ID="err" ForeColor="Red"></asp:Label>
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
