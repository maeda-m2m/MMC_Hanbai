<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlTanto.ascx.cs" Inherits="Gyomu.Master.CtlTanto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Master/TantoBumon.ascx" TagName="Bumon" TagPrefix="uc" %>

<style type="text/css">
    th {
        background: #00008B;
        color: white;
    }

    p {
        font-family: Meiryo;
    }

    #CtlBumon {
        border: 1px solid white;
    }
</style>

<table border="1">
    <tbody>
        <asp:Label ID="Err" runat="server" Text="" ForeColor="Red"></asp:Label>
        <tr>
            <th>
                <asp:Label ID="Label1" runat="server" Text="UserID"></asp:Label>
            </th>
            <td>
                <asp:TextBox ID="TbxId" runat="server"></asp:TextBox>
        </tr>
        <tr>
            <th>
                <asp:Label ID="Label2" runat="server" Text="読み方"></asp:Label>
            </th>
            <td>
                <asp:TextBox ID="TbxYomi" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                <asp:Label ID="Label3" runat="server" Text="名前"></asp:Label>
            </th>
            <td>
                <asp:TextBox ID="TbxName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
        </tr>
        <tr>
            <th>
                <asp:Label ID="Label7" runat="server" Text="パスワード"></asp:Label>
            </th>
            <td>

                <asp:TextBox ID="TbxPass" runat="server"></asp:TextBox>

            </td>
        </tr>
    </tbody>
</table>
<asp:DataGrid runat="server" ID="CtlBumon" AutoGenerateColumns="False" OnItemDataBound="CtlBumon_ItemDataBound" OnItemCommand="CtlBumon_ItemCommand">
    <Columns>
        <asp:TemplateColumn HeaderStyle-BorderStyle="None" ItemStyle-BorderStyle="None">
            <HeaderStyle BorderColor="White" BorderStyle="None" />
            <ItemStyle BorderColor="white" BorderStyle="None" />
            <ItemTemplate>
                <uc:Bumon ID="Syosai" runat="server" />
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>

    <Columns>
        <asp:TemplateColumn HeaderStyle-BorderStyle="None" ItemStyle-BorderStyle="None">
            <HeaderStyle BorderColor="White" BorderStyle="None" />
            <ItemStyle BorderColor="White" BorderStyle="None" />
            <ItemTemplate>
                <asp:Button ID="BtnDel" runat="server" Text="削除" />
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>

</asp:DataGrid>
<p>※表示できる部署は1つだけです</p>
<asp:Button ID="AddBtn" runat="server" Text="部署追加" OnClick="AddBtn_Click" />
