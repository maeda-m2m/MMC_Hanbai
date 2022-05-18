<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TantoBumon.ascx.cs" Inherits="Gyomu.Master.TantoBumon" %>
<style type="text/css">
</style>

<table>
    <tr>
        <td>
            <p>部門</p>
        </td>
        <td>
            <telerik:RadComboBox ID="RadBumon" runat="server"></telerik:RadComboBox>
            <input type="hidden" runat="server" id="HidRowNo" />
            <input type="hidden" runat="server" id="HidKeyNo" />
        </td>
        <td>
            <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                <asp:ListItem Text="表示" Value="True"></asp:ListItem>
                <asp:ListItem Text="非表示" Value="False"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
</table>

