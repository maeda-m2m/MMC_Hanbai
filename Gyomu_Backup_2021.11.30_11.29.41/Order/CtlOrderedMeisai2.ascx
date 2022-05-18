<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlOrderedMeisai2.ascx.cs" Inherits="Gyomu.Order.CtlOrderedMeisai2" %>
<link href="Ordered.css" type="text/css" rel="stylesheet" />
<asp:Label ID="Err" runat="server" Text="" ForeColor="Red"></asp:Label>
<table class="MeisaiTB">
    <tr>
        <td rowspan="2" class="RowNoTD">
            <asp:Label ID="RowNo" CssClass="RowNoClass" runat="server"></asp:Label>
            <br />
            <asp:CheckBox runat="server" ID="ChkCansel" Text="キャンセル" />
            <input type="hidden" runat="server" id="OrderedNo" />
            <input type="hidden" runat="server" id="HidTokuisakiCode" />
            <input type="hidden" runat="server" id="HidTokuisakiName" />
            
        </td>
        <td class="MeisaiTD">
            <asp:TextBox runat="server" ID="TbxMaker" AutoPostBack="true" Width="100px"></asp:TextBox>
        </td>
        <td class="MeisaiTD" style="width:500px">
            <asp:Label runat="server" ID="LblProductName"></asp:Label>
        </td>

        <td class="MeisaiTD" style="width:30px">
            <asp:Label runat="server" ID="LblSuryo"></asp:Label>
        </td>
        <td class="MeisaiTD" style="width:80px">
            <asp:Label runat="server" ID="LblHanni"></asp:Label>
        </td>
        <td class="MeisaiTD" style="width:80px">
            <asp:Label runat="server" ID="LblMedia"></asp:Label>
        </td>
        <td class="MeisaiTD" style="text-align:right;">
            <asp:TextBox runat="server" ID="TbxShiireTanka" Width="100px" CssClass="tbx"></asp:TextBox>
        </td>
        <td class="MeisaiTD" style="text-align:right;">
            <asp:TextBox runat="server" ID="TbxShiireKingaku" Width="100px" CssClass="tbx"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="3" class="MeisaiTD">
            <asp:Label runat="server" ID="LblFacility"></asp:Label>
        </td>
        <td class="MeisaiTD">
            <asp:Label runat="server" ID="LblSekisu"></asp:Label>
        </td>
        <td class="MeisaiTD">
            <asp:Label ID="LblSiyouKaishi" runat="server"></asp:Label>
        </td>
        <td class="MeisaiTD">
            <asp:Label runat="server" ID="LblSiyouOwari"></asp:Label>
        </td>
        <td class="MeisaiTD">
            <asp:DropDownList runat="server" ID="DdlWarehouse">
                <asp:ListItem Text="発注" Value="発注"></asp:ListItem>
                <asp:ListItem Text="在庫" Value="在庫"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>
