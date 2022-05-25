<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Oshirase.aspx.cs" Inherits="Gyomu.Tokuisaki.Oshirase" %>



<%@ Register Src="~/CtrlMitsuSyousai.ascx" TagName="Syosai" TagPrefix="uc2" %>
<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="css/Oshirase.css" rel="stylesheet" type="text/css" />
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
        <div>
            <p id="TokushuTitle">お知らせ編集ページ</p>
        </div>




        <div>

            <asp:ListView runat="server" ID="MainListView" OnItemCommand="MainListView_ItemCommand" OnItemDataBound="MainListView_ItemDataBound">


                <ItemTemplate>

                    <div id="MainDiv">

                        <div id="TitleDiv">

                            <div>
                                <asp:Button runat="server" ID="DeleteButton" Text="削除" CssClass="DeleteButton" CommandName="sakujo" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('削除を行います。よろしいですか？')" />
                                タイトル:<asp:TextBox runat="server" ID="TitleText" Text='<%# Bind("Title") %>'></asp:TextBox>
                            </div>
                            <div>
                                日付:<asp:TextBox runat="server" ID="TextBox1" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>

                        <div id="ShousaiTextDiv">
                            <asp:TextBox runat="server" ID="TextBox2" TextMode="MultiLine" Text='<%# Bind("Shousai") %>'></asp:TextBox>
                        </div>



                        <asp:HiddenField runat="server" ID="OshiraseIDHidden" Value='<%# Bind("OshiraseID") %>' />
                    </div>

                </ItemTemplate>

            </asp:ListView>

            <div id="ButtonDiv">
                <asp:Button runat="server" ID="RowAddButton" Text="行追加" OnClick="RowAddButton_Click" CssClass="DeleteButton" />
                <asp:Button runat="server" ID="ConfirmButton" Text="確定" OnClick="ConfirmButton_Click" CssClass="DeleteButton" />

            </div>

        </div>

        <footer>
            <p>@ 2022 movies management company Co., Ltd.</p>
        </footer>
    </form>
</body>
</html>
