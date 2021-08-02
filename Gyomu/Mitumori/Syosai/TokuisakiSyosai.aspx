<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TokuisakiSyosai.aspx.cs" Inherits="Gyomu.Mitumori.Syosai.TokuisakiSyosai" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        th {
	background: #00008B;
    color:#FFFFFF;
}
    </style>
        <script type="text/javascript">

     function Close() {
        window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="1">
	<tbody>
		<tr>
			<th>
                <asp:Literal ID="Literal1" runat="server">得意先コード</asp:Literal></th>
			<td colspan="3">
                <asp:Label ID="lblCode" runat="server" Text=""></asp:Label>
            </td>
		</tr>
		<tr>
			<th>
                <asp:Literal ID="Literal2" runat="server">得意先名</asp:Literal></th>
			<td colspan="3">
                <asp:Label ID="lblTokuisakiName" runat="server" Text=""></asp:Label>
            </td>
		</tr>
		<tr>
			<th>
                <asp:Literal ID="Literal3" runat="server">住所</asp:Literal></th>
			<td colspan="3">
                <asp:Label ID="lblJusyo" runat="server" Text=""></asp:Label>
            </td>
		</tr>
        <tr>
			<th>
                <asp:Literal ID="Literal4" runat="server">電話番号</asp:Literal></th>
			<td>
                <asp:Label ID="lblTel" runat="server" Text=""></asp:Label>
            </td>
			<th>
                <asp:Literal ID="Literal5" runat="server">Fax番号</asp:Literal></th>
			<td>
                <asp:Label ID="lblFax" runat="server" Text=""></asp:Label>
            </td>
		</tr>
		<tr>
			<th>
                <asp:Literal ID="Literal6" runat="server">得意先<br />担当者名</asp:Literal></th>
			<td colspan="3">
                <asp:Label ID="lblTokuisakiTanotoName" runat="server" Text=""></asp:Label>
            </td>
		</tr>
		<tr>
			<th>
                <asp:Literal ID="Literal7" runat="server">得意先部署</asp:Literal></th>
			<td>
                <asp:Label ID="lblBusyo" runat="server" Text=""></asp:Label>
            </td>
<%--			<th>
                <asp:Literal ID="Literal8" runat="server">得意先<Br />担当者役職名</asp:Literal></th>
			<td>
                <asp:Label ID="lblYakuwari" runat="server" Text=""></asp:Label>
            </td>--%>
		</tr>
		<tr>
			<th>
                <asp:Literal ID="Literal9" runat="server">敬称</asp:Literal></th>
			<td colspan="3">
                <asp:Label ID="lblKeisyou" runat="server" Text=""></asp:Label>
            </td>
		</tr>
		<tr>
			<th>
                <asp:Literal ID="Literal10" runat="server">主担当者<Br />コード</asp:Literal></th>
			<td colspan="3">
                <asp:Label ID="lblTanto" runat="server" Text=""></asp:Label>
            </td>
		</tr>
		<tr>
			<th>
                <asp:Literal ID="Literal11" runat="server">主担当者名</asp:Literal></th>
			<td colspan="3">
                <asp:Label ID="lblTantouName" runat="server" Text=""></asp:Label>
            </td>
		</tr>
	</tbody>
            </table>
                        <br />
            <asp:Button ID="BtnMaster" runat="server" Text="新規登録" OnClick="BtnMaster_Click" />
            <input id="BtnClose" runat="server" onclick="Close();" style="width:100;" type="button" value="閉じる" /><br />
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
