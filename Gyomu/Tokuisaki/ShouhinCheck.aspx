<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShouhinCheck.aspx.cs" Inherits="Gyomu.Tokuisaki.ShouhinCheck" %>

<%@ Register Src="~/CtrlMitsuSyousai.ascx" TagName="Syosai" TagPrefix="uc2" %>
<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="css/ShouhinCheck.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <header style="background-color: white;">
            <uc:Menu ID="Menu" runat="server" />
            <telerik:RadTabStrip ID="RT" runat="server" AutoPostBack="True" SelectedIndex="0" BackColor="#D2EAF6">
                <Tabs>

                    <telerik:RadTab Text="特集マスター" NavigateUrl="TokushuMenu.aspx">
                    </telerik:RadTab>
                    <telerik:RadTab Text="特集商品検索" NavigateUrl="ShouhinSearch.aspx">
                    </telerik:RadTab>
                    <telerik:RadTab Text="特集商品編集" NavigateUrl="ShouhinCheck.aspx">
                    </telerik:RadTab>
                    <telerik:RadTab Text="お知らせ" NavigateUrl="Oshirase.aspx">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
        </header>

        <div id="MainDiv">
            <div id="TitleDiv">
                <p>特集商品編集ページ</p>

            </div>
            <div id="CategoryDiv">
                特集名:<asp:DropDownList runat="server" ID="TokushuNameDrop" DataSourceID="SqlDataSource1" DataTextField="tokusyu_name" DataValueField="tokusyu_code" AutoPostBack="true" OnSelectedIndexChanged="TokushuNameDrop_SelectedIndexChanged"></asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:GyomuConnectionString %>' SelectCommand="SELECT [tokusyu_code], [tokusyu_name] FROM [M_tokusyu] WHERE tokusyu_code != '1' AND [CategoryCode] = @CategoryCode">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="TokushuCategoryDrop" PropertyName="SelectedValue" Name="CategoryCode" Type="String" />
                    </SelectParameters>

                </asp:SqlDataSource>

                カテゴリ:<asp:DropDownList runat="server" ID="TokushuCategoryDrop" DataSourceID="SqlDataSource2" DataTextField="Categoryname" DataValueField="CategoryCode" AutoPostBack="true" OnSelectedIndexChanged="TokushuCategoryDrop_SelectedIndexChanged"></asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="SqlDataSource2" ConnectionString='<%$ ConnectionStrings:GyomuConnectionString %>' SelectCommand="SELECT DISTINCT [CategoryCode], [Categoryname] FROM [M_Kakaku_2] WHERE CategoryCode = '203' or CategoryCode = '205' or CategoryCode = '209'"></asp:SqlDataSource>
            </div>

            <asp:ListView runat="server" ID="MainListView">
                <EmptyDataTemplate>
                    <div id="EmptyDiv">
                        <p>このカテゴリ、特集には商品が登録されていません。</p>

                    </div>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <div id="MainListDiv">
                        <table id="RankingTable">
                            <tr>

                                <td>
                                    <asp:TextBox runat="server" ID="Rankingtext" Text='<%# Bind("Ranking") %>' TextMode="SingleLine" MaxLength="3" ToolTip="ランキングの順位を変更することができます。"></asp:TextBox>

                                </td>
                            </tr>
                        </table>
                        <table>

                            <tr>
                                <th>商品名:</th>
                                <td>
                                    <asp:Label runat="server" Text='<%# Bind("SyouhinMei") %>' ID="Label1"></asp:Label></td>

                            </tr>
                            <tr>
                                <th>メディア:</th>
                                <td>
                                    <asp:Label runat="server" Text='<%# Bind("Media") %>' ID="Label3"></asp:Label></td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <th>紹介文</th>
                                <td>
                                    <asp:TextBox runat="server" ID="ShoukaiText" Text='<%# Bind("tokusyu_shouhin_shoukai") %>' TextMode="MultiLine"></asp:TextBox></td>
                            </tr>
                        </table>
                        <%-- <div>
                        </div>
                        <div id="shouhinDiv">
                        </div>
                        <div id="ShoukaiDiv">--%>
                        <%-- </div>--%>
                    </div>
                    <asp:HiddenField runat="server" ID="ShouhinCodeHidden" Value='<%# Bind("SyouhinCode") %>' />
                </ItemTemplate>
            </asp:ListView>
            <div id="ButtonDiv">
                <asp:Button runat="server" ID="ConfirmButton" Text="確定" OnClick="ConfirmButton_Click" />
                <%-- <asp:Button runat="server" ID="BackButton" Text="戻る" OnClick="BackButton_Click" />--%>
            </div>

        </div>
        <footer>
            <p>@ 2022 movies management company Co., Ltd.</p>
        </footer>
    </form>
</body>
</html>
