<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderedInput2.aspx.cs" Inherits="Gyomu.Order.OrderedInput2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>入荷履歴</title>
    <link href="Ordered.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="TbLog">
                <tr class="LogTR">
                    <td colspan="4" class="LogTD">
                        <asp:Label runat="server" ID="Err" ForeColor="Red"></asp:Label>
                    </td>
                    <td class="LogTD">
                        <p>発注No</p>
                    </td>
                    <td style="text-align: right" class="LogTD">
                        <asp:Label runat="server" ID="OrderedNo"></asp:Label>
                    </td>
                </tr>
                <tr class="LogTR">
                    <td class="LogTD">
                        <p>メーカー品番</p>
                    </td>
                    <td style="text-align: left" class="LogTD">
                        <asp:Label runat="server" ID="MakerNo"></asp:Label>
                    </td>
                    <td class="LogTD"></td>
                    <td class="LogTD"></td>
                    <td class="LogTD"></td>
                    <td class="LogTD"></td>
                </tr>
                <tr class="LogTR">
                    <td class="LogTD">
                        <p>タイトル</p>
                    </td>
                    <td class="LogTD">
                        <asp:Label runat="server" ID="LblProductName"></asp:Label>
                    </td>
                    <td class="LogTD"></td>
                    <td class="LogTD"></td>
                    <td class="LogTD"></td>
                    <td class="LogTD"></td>
                </tr>
                <tr class="LogTR">
                    <td class="LogTD">
                        <p>発注数量</p>
                    </td>
                    <td style="text-align: left" class="LogTD">
                        <asp:Label runat="server" ID="LblSuryo"></asp:Label>
                    </td>
                    <td class="LogTD"></td>
                    <td class="LogTD"></td>
                    <td class="LogTD"></td>
                    <td class="LogTD"></td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:DataGrid runat="server" ID="DGlog" AutoGenerateColumns="false" OnItemDataBound="DGlog_ItemDataBound" OnItemCommand="DGlog_ItemCommand">
                            <Columns>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Button runat="server" ID="BtnDel" Text="削除" CommandName="Delete" CssClass="Btn7" />
                                        <asp:Button runat="server" ID="BtnEdit" Text="編集" CommandName="Edit" CssClass="Btn7" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn ItemStyle-Width="50px" HeaderStyle-Font-Size="12px" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="Suryo" Text="発注残数"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="LblZanSuryo">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn ItemStyle-Width="50px" HeaderStyle-Font-Size="12px" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="Nyuko" Text="入庫個数"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="LblNyuko">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn ItemStyle-Width="100px" HeaderStyle-Font-Size="12px" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="Tanto" Text="入力者"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="LblStaff">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn ItemStyle-Width="100px" HeaderStyle-Font-Size="12px" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="Date" Text="入荷日"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="LblNyukaDate">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn ItemStyle-Width="100px" HeaderStyle-Font-Size="12px" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="Date" Text="発注日"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="LblHatyuDate">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                            </Columns>
                            <HeaderStyle BackColor="#b3c6e7" BorderStyle="None" />
                            <ItemStyle BorderStyle="Solid" BorderColor="#2e75b6" BorderWidth="2px" />
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
