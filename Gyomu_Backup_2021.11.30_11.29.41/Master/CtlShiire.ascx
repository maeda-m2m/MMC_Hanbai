<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlShiire.ascx.cs" Inherits="Gyomu.Master.CtlShiire" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<style type="text/css">
    th {
        background: #00008B;
        color:white;
    }

</style>

<table border="1">
	<tbody>
		<tr>
			<th>コード</th>
			<td><asp:TextBox ID="TbxCode" runat="server" Width="300"></asp:TextBox></td>
		</tr>
		<tr>
			<th>仕入先名</th>
			<td><asp:TextBox ID="TbxShiire" runat="server" Width="350"></asp:TextBox></td>
		</tr>
		<tr>
			<th>フリガナ</th>
			<td><asp:TextBox ID="TbxKana" runat="server" Width="350"></asp:TextBox></td>
		</tr>
		<tr>
			<th>略称</th>
			<td><asp:TextBox ID="TbxRyaku" runat="server" Width="350"></asp:TextBox></td>
		</tr>
		<tr>
			<th>郵便番号</th>
			<td><asp:TextBox ID="TbxPost" runat="server" Width="300"></asp:TextBox></td>
		</tr>
		<tr>
			<th>住所1</th>
			<td><asp:TextBox ID="TbxAdd1" runat="server" Width="350"></asp:TextBox></td>
		</tr>
		<tr>
			<th>住所2</th>
			<td><asp:TextBox ID="TbxAdd2" runat="server" Width="350"></asp:TextBox></td>
		</tr>
		<tr>
			<th>電話番号</th>
			<td> <asp:TextBox ID="TbxTell" runat="server" Width="300"></asp:TextBox></td>
		</tr>
		<tr>
			<th>Fax</th>
			<td><asp:TextBox ID="TbxFax" runat="server" Width="300"></asp:TextBox></td>
		</tr>
		<tr>
			<th>担当者</th>
			<td> <asp:TextBox ID="TbxPersonal" runat="server" Width="300"></asp:TextBox></td>
		</tr>
		<tr>
			<th>部署</th>
			<td> <asp:TextBox ID="TbxBusyo" runat="server" Width="300"></asp:TextBox></td>
		</tr>
		<tr>
			<th>締日</th>
			<td><asp:DropDownList ID="DrpOff" runat="server">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="05">5日締め</asp:ListItem>
                    <asp:ListItem Value="10">10日締め</asp:ListItem>
                    <asp:ListItem Value="15">15日締め</asp:ListItem>
                    <asp:ListItem Value="20">20日締め</asp:ListItem>
                    <asp:ListItem Value="25">25日締め</asp:ListItem>
                    <asp:ListItem Value="99">月末締め</asp:ListItem>
                    <asp:ListItem Value="00">随時締め</asp:ListItem>
			    </asp:DropDownList></td>
		</tr>
		<tr>
			<th>支払日</th>
			<td><asp:DropDownList ID="Drppay" runat="server">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="05">5日締め</asp:ListItem>
                    <asp:ListItem Value="10">10日締め</asp:ListItem>
                    <asp:ListItem Value="15">15日締め</asp:ListItem>
                    <asp:ListItem Value="20">20日締め</asp:ListItem>
                    <asp:ListItem Value="25">25日締め</asp:ListItem>
                    <asp:ListItem Value="99">月末締め</asp:ListItem>
                    <asp:ListItem Value="00">随時締め</asp:ListItem>
			    </asp:DropDownList></td>
		</tr>
		<tr>
			<th>備考</th>
			<td><asp:TextBox ID="TbxOther" runat="server"></asp:TextBox></td>
		</tr>
	</tbody>
</table>