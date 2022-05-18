<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Result.aspx.cs" Inherits="Gyomu.Master.Result" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>結果</title>
    <style type="text/css">
        #DivBase {
            width: 100%;
        }

        #DivMain {
            width: 1000px;
            margin: 0px auto;
        }

        #TBresult {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div runat="server" id="DivBase">
            <div runat="server" id="DivMain">
                <table runat="server" id="TBresult">
                    <tr>
                        <td>
                            <p>データ取込数：</p>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="LblCounts"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>成功：</p>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="LblSuccessCount" ForeColor="Green"></asp:Label>
                        </td>
                        <td>
                            <p>件</p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>失敗：</p>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="LblFailedCount" ForeColor="Red"></asp:Label>
                        </td>
                        <td>
                            <p>件</p>
                        </td>
                    </tr>
                </table>
                <asp:DataGrid runat="server" ID="DGdetail" AutoGenerateColumns="false" BorderColor="lightgray" OnItemDataBound="DGdetail_ItemDataBound" Width="100%">
                    <Columns>
                        <asp:TemplateColumn>
                            <HeaderStyle Width="50px" />
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderTemplate>
                                <p>番号</p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="LblErrorNo"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn>
                            <ItemStyle Wrap="true" />
                            <HeaderTemplate>
                                <p>エラー内容</p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="LblErrorDatail"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>

                <asp:DataGrid runat="server" ID="DGFaliedData" AutoGenerateColumns="false" OnItemDataBound="DGFaliedData_ItemDataBound" Width="100%">
                </asp:DataGrid>
                <asp:Button runat="server" ID="BtnBack" OnClick="BtnBack_Click" Text="戻る" />
            </div>
        </div>
    </form>
</body>
</html>
