<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TokushuMenu.aspx.cs" Inherits="Gyomu.Tokuisaki.TokushuMenu" %>


<%@ Register Src="~/CtrlMitsuSyousai.ascx" TagName="Syosai" TagPrefix="uc2" %>
<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>特集マスター設定ページ</title>
    <link href="css/TokushuMenu.css" rel="stylesheet" type="text/css" />
</head>
<body>


    <form id="form1" runat="server">

        <div>


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
                <p id="TokushuTitle">特集マスター設定ページ</p>

            </div>


            <asp:Panel runat="server" ID="MainPanel">

                <div id="Drop">
                    カテゴリ:<asp:DropDownList runat="server" ID="CategoryDrop" OnSelectedIndexChanged="CategoryDrop_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="上映会" Value="205"></asp:ListItem>
                        <asp:ListItem Text="キッズ・BGV" Value="209"></asp:ListItem>
                        <asp:ListItem Text="バス" Value="203"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button runat="server" ID="SubButton" Text="画像の確認" OnClick="SubButton_Click" CssClass="Button" />
                </div>







                <asp:ListView runat="server" ID="MainListView" OnItemDataBound="MainListView_ItemDataBound">




                    <ItemTemplate>

                        <div id="MainDiv">

                            <table id="TokushuTitleDiv">
                                <tr>
                                    <th>特集名</th>
                                    <td>
                                        <asp:TextBox runat="server" Text='<%# Bind("tokusyu_name") %>' ID="TokushuNameTxt"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <th>タイプ</th>
                                    <td>
                                        <asp:DropDownList runat="server" ID="TypeDrop">
                                            <asp:ListItem Text="一覧" Value="一覧"></asp:ListItem>
                                            <asp:ListItem Text="ランキング" Value="ランキング"></asp:ListItem>

                                        </asp:DropDownList>
                                    </td>
                                    <%-- <td>

                                        <asp:TextBox runat="server" ID="TypeText" Text='<%# Bind("TokushuType") %>'></asp:TextBox></td>--%>
                                </tr>

                                <tr>
                                    <th>ランキングタイトル</th>
                                    <td>
                                        <asp:TextBox runat="server" Text='<%# Bind("RankingTitle") %>' ID="RankingTxt"></asp:TextBox></td>
                                </tr>
                                <tr></tr>

                                <tr>

                                    <tr>
                                        <th>特集紹介文</th>
                                        <td>
                                            <asp:TextBox runat="server" Text='<%# Bind("tokusyu_naiyou") %>' ID="ShousaiTxt" TextMode="MultiLine"></asp:TextBox></td>
                                    </tr>



                                </tr>

                            </table>


                        </div>



                        <asp:HiddenField runat="server" ID="Hidden" Value='<%# Bind("tokusyu_code") %>' />

                    </ItemTemplate>

                </asp:ListView>


                <div id="ButtonDiv">
                    <asp:Button runat="server" ID="ConfirmButton" Text="確定" OnClick="ConfirmButton_Click" />
                </div>

            </asp:Panel>


            <asp:Panel runat="server" ID="SubPanel">
                <div id="UploadDiv">
                    <asp:Button runat="server" ID="BackButton" Text="戻る" OnClick="BackButton_Click" CssClass="Button" />
                    <asp:Button runat="server" ID="ImageUploadButton" Text="アップロード" OnClick="ImageUploadButton_Click" CssClass="Button" />
                    <asp:FileUpload runat="server" ID="FileUpload" AllowMultiple="true" />
                </div>
                <main>
                    <telerik:RadGrid ID="MainRadGrid" runat="server" AutoGenerateColumns="False" OnItemCommand="MainRadGrid_ItemCommand" OnItemDataBound="MainRadGrid_ItemDataBound">

                        <AlternatingItemStyle BackColor="#93bbff" />

                        <MasterTableView>

                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="画像">

                                    <ItemTemplate>
                                        <asp:Image runat="server" ID="ShouhinImage" Width="100px" Height="150px" />
                                    </ItemTemplate>

                                </telerik:GridTemplateColumn>

                                <telerik:GridBoundColumn DataField="tokusyu_code" HeaderText="特集コード"></telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="tokusyu_name" HeaderText="特集名"></telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="CategoryCode" HeaderText="カテゴリコード"></telerik:GridBoundColumn>

                                <telerik:GridButtonColumn ButtonType="PushButton" CommandName="Delete" Text="画像削除" HeaderText="削除" ButtonCssClass="Button"></telerik:GridButtonColumn>
                            </Columns>

                        </MasterTableView>


                    </telerik:RadGrid>
                </main>
            </asp:Panel>





            <footer>
                <p>@ 2022 movies management company Co., Ltd.</p>
            </footer>
        </div>
        <telerik:RadAjaxManager runat="server" ID="Ram" OnAjaxRequest="Ram_AjaxRequest"></telerik:RadAjaxManager>
    </form>

    <script>
        'use strict';

        //const target1 = document.getElementById('MainListView_ctrl0_TextBox4');
        //target1.readOnly = true;

        //const target2 = document.getElementById('MainListView_ctrl2_TextBox4');
        //target2.readOnly = true;

    </script>
</body>
</html>
