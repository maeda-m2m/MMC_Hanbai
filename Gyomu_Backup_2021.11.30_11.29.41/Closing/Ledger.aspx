<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ledger.aspx.cs" Inherits="Gyomu.Closing.Ledger" %>

<%@ Register TagPrefix="uc2" TagName="CtlNengappiFromTo" Src="~/Common/CtlNengappiForm.ascx" %>
<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>元帳</title>
    <link href="Ledger.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .Btn10 {
            font-size: 12px;
            margin-left: 5px;
            text-align: center;
            width: 95px;
            display: inline-block;
            text-decoration: none;
            color: white;
            border: solid 2px #619fed;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            border-radius: 3px;
            background-color: #92baec;
        }

            .Btn10:hover {
                background: #619fed;
                color: white;
                border: solid 2px #2c75d0;
            }

        .KensakuTD {
            background-color: #0070C0;
            color: white;
        }
    </style>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <uc:Menu ID="Menu" runat="server" />
            </div>
            <div>
                <telerik:RadTabStrip ID="RT" runat="server" AutoPostBack="True" SelectedIndex="0" BackColor="#d2eaf6">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="元帳" Font-Size="12pt" NavigateUrl="~/Closing/Ledger.aspx" Selected="true">
                        </telerik:RadTab>
                        <telerik:RadTab Text="月次処理" Font-Size="12pt" NavigateUrl="MonthClosing.aspx">
                        </telerik:RadTab>
                        <telerik:RadTab Text="年次処理" Font-Size="12pt" NavigateUrl="YearClosing.aspx">
                        </telerik:RadTab>
                        <telerik:RadTab Text="消込" Font-Size="12pt" NavigateUrl="Reconciliation.aspx"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
            </div>
            <br />
            <div>
                <table>
                    <tr>
                        <td class="KensakuTD">
                            <p>元帳</p>
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="RadLedger" OnSelectedIndexChanged="RadLedger_SelectedIndexChanged" AutoPostBack="true">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Text="" />
                                    <telerik:RadComboBoxItem runat="server" Text="得意先" />
                                    <telerik:RadComboBoxItem runat="server" Text="仕入先" />
                                    <telerik:RadComboBoxItem runat="server" Text="商品" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
                <table runat="server" id="TokuisakiKensaku">
                    <tr>
                        <td class="KensakuTD">
                            <p>使用施設</p>
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="RcbFacility" OnItemsRequested="RcbFacility_ItemsRequested" AllowCustomText="true" EnableVirtualScrolling="true" ShowMoreResultsBox="true" EnableLoadOnDemand="true"></telerik:RadComboBox>
                        </td>
                        <td class="KensakuTD">
                            <p>得意先名</p>
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="RcbTokuiShiire" OnItemsRequested="RcbTokuiShiire_ItemsRequested" OnSelectedIndexChanged="RcbTokuiShiire_SelectedIndexChanged" AllowCustomText="true" AutoPostBack="true" EnableVirtualScrolling="true" ShowMoreResultsBox="true" EnableLoadOnDemand="true"></telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="KensakuTD">
                            <p>市町村</p>
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="RcbCity" OnItemsRequested="RcbCity_ItemsRequested" AllowCustomText="true" EnableVirtualScrolling="true" ShowMoreResultsBox="true" EnableLoadOnDemand="true"></telerik:RadComboBox>
                        </td>
                        <td class="KensakuTD">
                            <p>期間</p>
                        </td>
                        <td>
                            <uc2:CtlNengappiFromTo ID="CtlKikan" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="KensakuTD">
                            <p>部門</p>
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="RcbBumon" OnItemsRequested="RcbBumon_ItemsRequested" AllowCustomText="true" EnableVirtualScrolling="true" ShowMoreResultsBox="true" EnableLoadOnDemand="true"></telerik:RadComboBox>
                        </td>
                        <td class="KensakuTD">
                            <p>担当者</p>
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="RcbStaff" OnItemsRequested="RcbStaff_ItemsRequested" AllowCustomText="true" EnableVirtualScrolling="true" ShowMoreResultsBox="true" EnableLoadOnDemand="true"></telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
                <table runat="server" id="ShiiresakiKensaku">
                    <tr>
                        <td class="KensakuTD">
                            <p>使用施設</p>
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="RcbFacility2" OnItemsRequested="RcbFacility_ItemsRequested" AllowCustomText="true" EnableVirtualScrolling="true" ShowMoreResultsBox="true" EnableLoadOnDemand="true"></telerik:RadComboBox>
                        </td>
                        <td class="KensakuTD">
                            <p>仕入先</p>
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="RcbShiiresaki" OnItemsRequested="RcbShiiresaki_ItemsRequested" AllowCustomText="true" EnableVirtualScrolling="true" ShowMoreResultsBox="true" EnableLoadOnDemand="true"></telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="KensakuTD">
                            <p>市町村</p>
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="RcbCity2" OnItemsRequested="RcbCity_ItemsRequested" AllowCustomText="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true" EnableLoadOnDemand="true"></telerik:RadComboBox>
                        </td>
                        <td class="KensakuTD">
                            <p>期間</p>
                        </td>
                        <td>
                            <uc2:CtlNengappiFromTo runat="server" ID="CtlKikan2" />
                        </td>
                    </tr>
                </table>
                <table runat="server" id="SyouhinKensaku">
                    <tr>
                        <td class="KensakuTD">
                            <p>メーカー品番</p>
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="RcbMaker" OnItemsRequested="RcbMaker_ItemsRequested" AllowCustomText="true" ShowMoreResultsBox="true" EnableLoadOnDemand="true" EnableVirtualScrolling="true"></telerik:RadComboBox>
                        </td>
                        <td class="KensakuTD">
                            <p>期間</p>
                        </td>
                        <td>
                            <uc2:CtlNengappiFromTo runat="server" ID="CtlKikan3" />
                        </td>
                    </tr>
                </table>
                <asp:Button runat="server" ID="BtnSerch" OnClick="BtnSerch_Click" Text="検索" CssClass="Btn10" />
                <br />
                <br />
                <table>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="BtnCSV" OnClick="BtnCSV_Click" Text="CSVダウンロード" CssClass="Btn10" Width="110px" />
                        </td>
                        <td>
                            <asp:Button runat="server" ID="BtnPrintOut" OnClick="BtnPrintOut_Click" Text="プリントアウト" CssClass="Btn10" Width="110px" />
                        </td>
                    </tr>
                </table>
                <br />
                <table>
                    <tr>
                        <td>
                            <asp:DataGrid runat="server" ID="DGLedger" AutoGenerateColumns="false" OnItemCommand="DGLedger_ItemCommand" OnItemDataBound="DGLedger_ItemDataBound">
                                <Columns>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" BackColor="#0070C0" ForeColor="White" />
                                        <HeaderTemplate>
                                            <p>年</p>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="LblYear"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn>
                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" BackColor="#0070C0" ForeColor="White" />
                                        <HeaderTemplate>
                                            <p>月</p>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="LblMonth"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn>
                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" BackColor="#0070C0" ForeColor="White" />
                                        <HeaderTemplate>
                                            <p>日</p>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="LblDay"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>


                                    <asp:TemplateColumn>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Center" BackColor="#0070C0" ForeColor="White" />
                                        <HeaderTemplate>
                                            <p>No.</p>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="LblNo"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" BackColor="#0070C0" ForeColor="White" />
                                        <HeaderTemplate>
                                            <p>使用施設</p>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="LblFacility"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="#0070C0" ForeColor="White" />
                                        <HeaderTemplate>
                                            <p>作品</p>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="LblProduct"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Center" BackColor="#0070C0" ForeColor="White" />
                                        <HeaderTemplate>
                                            <p>
                                                借方<br />
                                                売上金額
                                            </p>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="LblKarikata"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Center" BackColor="#0070C0" ForeColor="White" />
                                        <HeaderTemplate>
                                            <p>
                                                貸方<br />
                                                入金金額
                                            </p>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="LblKashikata"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Center" BackColor="#0070C0" ForeColor="White" />
                                        <HeaderTemplate>
                                            <p>残高</p>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="LblZandaka"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                    <tr>
                        <td>
                                                        <asp:DataGrid runat="server" ID="DataGrid1" AutoGenerateColumns="false" OnItemCommand="DGLedger_ItemCommand" OnItemDataBound="DGLedger_ItemDataBound">
                                <Columns>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" BackColor="#0070C0" ForeColor="White" />
                                        <HeaderTemplate>
                                            <p>年</p>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="LblYear"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn>
                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" BackColor="#0070C0" ForeColor="White" />
                                        <HeaderTemplate>
                                            <p>月</p>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="LblMonth"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn>
                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" BackColor="#0070C0" ForeColor="White" />
                                        <HeaderTemplate>
                                            <p>日</p>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="LblDay"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>


                                    <asp:TemplateColumn>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Center" BackColor="#0070C0" ForeColor="White" />
                                        <HeaderTemplate>
                                            <p>No.</p>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="LblNo"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" BackColor="#0070C0" ForeColor="White" />
                                        <HeaderTemplate>
                                            <p>使用施設</p>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="LblFacility"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="#0070C0" ForeColor="White" />
                                        <HeaderTemplate>
                                            <p>作品</p>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="LblProduct"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Center" BackColor="#0070C0" ForeColor="White" />
                                        <HeaderTemplate>
                                            <p>
                                                借方<br />
                                                支払金額
                                            </p>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="LblKarikata"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Center" BackColor="#0070C0" ForeColor="White" />
                                        <HeaderTemplate>
                                            <p>
                                                貸方<br />
                                                仕入額
                                            </p>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="LblKashikata"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Center" BackColor="#0070C0" ForeColor="White" />
                                        <HeaderTemplate>
                                            <p>残高</p>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="LblZandaka"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>

                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
