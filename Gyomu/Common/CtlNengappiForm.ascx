<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlNengappiForm.ascx.cs" Inherits="Gyomu.Common.CtlNengappiForm" %>
<table id="T" style="border-collapse:collapse">
    <tr>
        <td>
            <table id="TblFrom" cellspacing="0" cellpadding="0" border="0" runat="server">
                <tr>
                    <td>
                        <%--						<telerik:RadDatePicker id="RdpFrom" ToolTip="" MinDate="1950-01-01" runat="server" Width="120px"></telerik:RadDatePicker></td>--%>
                        <telerik:RadDatePicker runat="server" ID="RdpFrom" MinDate="1950-01-01" ></telerik:RadDatePicker>
                </tr>
            </table>
        </td>
        <td>
            <asp:DropDownList ID="DdlKikan" runat="server">
                <asp:ListItem Value="0">指定しない</asp:ListItem>
                <asp:ListItem Value="1">指定日</asp:ListItem>
                <asp:ListItem Value="2">以前</asp:ListItem>
                <asp:ListItem Value="3">以降</asp:ListItem>
                <asp:ListItem Value="4">から</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td>
            <table id="TblTo" cellspacing="0" cellpadding="0" border="0" runat="server">
                <tr>
                    <td>
                        <%--						<telerik:RadDatePicker id="RdpTo" Runat="server" MinDate="1950-01-01" Width="120px">
						</telerik:RadDatePicker></td>--%>
                        <telerik:RadDatePicker runat="server" ID="RdpTo" MinDate="1950-01-01" ></telerik:RadDatePicker>
                    </td>
                </tr>
            </table>
        </td>
        <td></td>
    </tr>
</table>


