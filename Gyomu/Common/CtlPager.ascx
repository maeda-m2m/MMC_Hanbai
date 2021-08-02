<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlPager.ascx.cs" Inherits="Yodokou_HanbaiKanri.Common.CtlPager" %>
<table id="T"  border="0" cellpadding="2" cellspacing="0" runat="server">
    <tr>
        <td nowrap="nowrap">
            <asp:Button ID="BtnPrev" runat="server" Text="前の頁" OnClick="BtnPrev_Click"/>
        </td>
        <td nowrap="nowrap">
            <asp:DropDownList ID="DdlPage" runat="server" AutoPostBack="true" 
            OnSelectedIndexChanged="DdlPage_SelectedIndexChanged">
            </asp:DropDownList></td>
        <td nowrap="nowrap" >
            /</td>
        <td nowrap="nowrap" >
            <asp:Literal ID="LitPageCount" runat="server"></asp:Literal></td>
        <td nowrap="nowrap" >
            <asp:Button ID="BtnNext" runat="server" Text="次の頁" OnClick="BtnNext_Click"/></td>
        <td nowrap="nowrap" >
            <asp:Literal ID="LitCounter" runat="server"></asp:Literal></td>
    </tr>
</table>