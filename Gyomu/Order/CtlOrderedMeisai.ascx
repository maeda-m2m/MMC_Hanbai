<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlOrderedMeisai.ascx.cs" Inherits="Gyomu.Order.CtlOrderedMeisai" %>
<link href="Ordered.css" type="text/css" rel="stylesheet" />
<asp:Label ID="Err" runat="server" Text="" ForeColor="Red"></asp:Label>

<table class="MeisaiTB">
    <tr>
        <td rowspan="2" class="RowNoTD">
            <asp:Label ID="RowNo" CssClass="RowNoClass" runat="server"></asp:Label>
            <input type="hidden" runat="server" id="HidCate" />

            <br />
            <asp:CheckBox runat="server" ID="ChkCansel" Text="キャンセル" />
            <input type="hidden" runat="server" id="OrderedNo" />
        </td>
        <td class="MeisaiTD" style="width:100px">
            <asp:Label runat="server" ID="LblProductName"></asp:Label>

        </td>
        <td class="MeisaiTD" style="width: 500px">
            <telerik:RadComboBox ID="RcbMaker" runat="server" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" EmptyMessage="" AutoPostBack="True" Width="500px" OnItemsRequested="RcbMaker_ItemsRequested" OnSelectedIndexChanged="RcbMaker_SelectedIndexChanged"></telerik:RadComboBox>

        </td>

        <td class="MeisaiTD" style="width: 30px">
            <asp:Label runat="server" ID="LblSuryo"></asp:Label>
        </td>
        <td class="MeisaiTD" style="width: 80px">
            <asp:Label runat="server" ID="LblHanni"></asp:Label>
        </td>
        <td class="MeisaiTD" style="width: 80px">
            <asp:Label runat="server" ID="LblMedia"></asp:Label>
        </td>
        <td class="MeisaiTD" style="text-align: right;">
            <asp:TextBox runat="server" ID="TbxShiireTanka" Width="100px" CssClass="tbx"></asp:TextBox>
            <script>
                function GetData(data) {
                    debugger;
                    var ary = data.split('-');
                    var tanka = document.getElementById(ary[0]).value;
                    var suryo = document.getElementById(ary[1]).innerText;
                    var kin = tanka * suryo;
                    debugger;
                    document.getElementById(ary[2]).value = String(kin).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                    document.getElementById(ary[0]).value = String(tanka).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                }
            </script>
        </td>
        <td class="MeisaiTD" style="text-align: right;">
            <asp:TextBox runat="server" ID="TbxShiireKingaku" Width="100px" CssClass="tbx"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="3" class="MeisaiTD" style="height: 20px;">
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
            <asp:Label runat="server" ID="LblWareHouse"></asp:Label>
        </td>
    </tr>
</table>
