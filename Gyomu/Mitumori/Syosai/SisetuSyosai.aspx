<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SisetuSyosai.aspx.cs" Inherits="Gyomu.Mitumori.Syosai.SisetuSyosai" %>

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
			<th>施設コード</th>
			<td>
                <asp:Label ID="lblCode" runat="server" Text=""></asp:Label>
            </td>
		</tr>
		<tr>
			<th>施設名</th>
			<td>
                <asp:Label ID="lblSisetuName" runat="server" Text=""></asp:Label>
            </td>
		</tr>
		<tr>
			<th>施設担当者</th>
			<td>
                <asp:Label ID="lblTanto" runat="server" Text=""></asp:Label>
            </td>
		</tr>
		<tr>
			<th>住所</th>
			<td>
                <asp:Label ID="lblJusyo" runat="server" Text=""></asp:Label>
            </td>
		</tr>
		<tr>
			<th>電話番号</th>
			<td>
                <asp:Label ID="lblTell" runat="server" Text=""></asp:Label>
            </td>
		</tr>
<%--		<tr>
			<th>スポット得意先
</th>
			<td>
                <asp:Label ID="lblSpotto" runat="server" Text=""></asp:Label>
            </td>
		</tr>--%>
	</tbody>
</table>
             <br />
            <asp:Button ID="BtnMaster" runat="server" Text="新規登録" OnClick="BtnMaster_Click" />
            <input id="BtnClose" runat="server" onclick="Close();" style="width:100;" type="button" value="閉じる" /><br />
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblMsg2" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
