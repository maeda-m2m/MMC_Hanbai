<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeikyuSyosai.aspx.cs" Inherits="Gyomu.Mitumori.Syosai.SeikyuSyousai" %>

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
			<th>請求先コード</th>
			<td colspan="3">
                <asp:Label ID="lblSeikyuCode" runat="server" Text=""></asp:Label>
            </td>
		</tr>
		<tr>
			<th>請求先略称</th>
			<td colspan="3">
                <asp:Label ID="lblSeikyuRyajuSyo" runat="server" Text=""></asp:Label>
            </td>
		</tr>
		<tr>
			<th>締日区分</th>
			<td>
                <asp:Label ID="lblSimebiKubun" runat="server" Text=""></asp:Label>
            </td>
			<th>税額通知</th>
			<td>
                <asp:Label ID="lblZeigakututi" runat="server" Text=""></asp:Label>
            </td>
		</tr>
		<tr>
			<th>税込区分</th>
			<td colspan="3">
                <asp:Label ID="lblZeikomikubun" runat="server" Text=""></asp:Label>
            </td>
		</tr>
		<tr>
			<th>会社銀行コード</th>
			<td>
                <asp:Label ID="lblGinkoCode" runat="server" Text=""></asp:Label>
            </td>
			<th>会社銀行名</th>
			<td>
                <asp:Label ID="lblGinkoName" runat="server" Text=""></asp:Label>
            </td>
		</tr>
		<tr>
			<th>会社口座番号</th>
			<td colspan="3">
                <asp:Label ID="lblKaisyaKoza" runat="server" Text=""></asp:Label>
            </td>
		</tr>
<%--		<tr>
			<th>振込依頼人</th>
			<td colspan="3">
                <asp:Label ID="lblIrainin" runat="server" Text=""></asp:Label>
            </td>--%>
		</tr>
	</tbody>
</table>
            <br />
            <asp:Button ID="BtnMaster" runat="server" Text="新規登録" OnClick="BtnMaster_Click" />
             <input id="BtnClose" type="button" runat="server" style="width:100;" value="閉じる" onclick="Close();" />
            <br />
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
