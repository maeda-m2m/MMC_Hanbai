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



            <div id="">
                <asp:ListView runat="server" ID="MainListView">




                    <ItemTemplate>

                        <div id="MainDiv">
                            <table id="TokushuTitleDiv">
                                <tr>

                                    <td>
                                        <asp:Label runat="server" Text='<%# Bind("TokushuType") %>' ID="Label1"></asp:Label></td>
                                    <th>特集名</th>
                                    <td>
                                        <asp:TextBox runat="server" Text='<%# Bind("tokusyu_name") %>' ID="TextBox2"></asp:TextBox></td>
                                    <th>ランキングタイトル</th>
                                    <td>
                                        <asp:TextBox runat="server" Text='<%# Bind("RankingTitle") %>' ID="TextBox4"></asp:TextBox></td>
                                </tr>
                            </table>
                            <%--   <asp:Label runat="server" Text='<%# Bind("tokusyu_code") %>' ID="Label5"></asp:Label>--%>



                            <table>
                                <tr>
                                    <th>特集紹介文</th>
                                    <td>
                                        <asp:TextBox runat="server" Text='<%# Bind("tokusyu_naiyou") %>' ID="TextBox3" TextMode="MultiLine"></asp:TextBox></td>
                                </tr>
                            </table>




                        </div>
                        <asp:HiddenField runat="server" ID="Hidden" Value='<%# Bind("tokusyu_code") %>' />
                    </ItemTemplate>
                </asp:ListView>
                <div id="ButtonDiv">
                    <asp:Button runat="server" ID="ConfirmButton" Text="確定" OnClick="ConfirmButton_Click" />
                </div>
            </div>
            <footer>
                <p>@ 2022 movies management company Co., Ltd.</p>
            </footer>
        </div>

    </form>

    <script>
        'use strict';

        const target1 = document.getElementById('MainListView_ctrl0_TextBox4');
        target1.readOnly = true;

        const target2 = document.getElementById('MainListView_ctrl2_TextBox4');
        target2.readOnly = true;

    </script>
</body>
</html>
