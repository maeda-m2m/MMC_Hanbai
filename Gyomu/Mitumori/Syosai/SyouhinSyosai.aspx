<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SyouhinSyosai.aspx.cs" Inherits="Gyomu.Mitumori.Syosai.SyouhinSyosaiaspx" %>

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
			<th>商品コード</th>
			<td colspan="3">
                <asp:Label ID="lblSyouhinCode" runat="server" Text=""></asp:Label>
            </td>
		</tr>
		<tr>
			<th>商品名</th>
			<td colspan="3">
                <asp:Label ID="lblSyouhinMei" runat="server" Text=""></asp:Label>
            </td>
		</tr>
		<tr>
<%--			<th>明細区分</th>
			<td>
                <asp:Label ID="lblKubun" runat="server" Text=""></asp:Label>
            </td>--%>
			<th>メーカー品番</th>
			<td>
                <asp:Label ID="lblHinban" runat="server" Text=""></asp:Label>
            </td>
		</tr>
		<tr>
			<th>媒体名</th>
			<td colspan="3">
                <asp:Label ID="lblKeitai" runat="server" Text=""></asp:Label>
            </td>
		</tr>
		<tr>
			<th>仕入先コード</th>
			<td>
                <asp:Label ID="lblShiireCode" runat="server" Text=""></asp:Label>
            </td>
			<th>仕入先</th>
			<td>
                <asp:Label ID="lblShiire" runat="server" Text=""></asp:Label>
            </td>
		</tr>
<%--		<tr>
			<th>図書館コード</th>
			<td colspan="3">
                <asp:Label ID="lblTosyo" runat="server" Text=""></asp:Label>
            </td>
		</tr>--%>
		<tr>
			<th>倉庫</th>
			<td colspan="3">
                <asp:Label ID="lblSoko" runat="server" Text=""></asp:Label>
            </td>
		</tr>
		<tr>
			<th>利用状態</th>
			<td colspan="3">
                <asp:Label ID="lblRiyou" runat="server" Text=""></asp:Label>
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
