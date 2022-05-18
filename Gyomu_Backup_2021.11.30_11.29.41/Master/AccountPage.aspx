<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountPage.aspx.cs" Inherits="Gyomu.Master.AccountPage" %>

<%@ Register Src="~/CtrlMitsuSyousai.ascx" TagName="Syosai" TagPrefix="uc2" %>
<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>会社情報管理</title>
    <style>
        @charset "UTF-8";

        html {
            font-size: 100%;
        }

        body {
            font-family: Meiryo;
        }
        /*-----------------------------------------------------------------------------------------*/
        /*全体に適用するCSS*/
        .btn {
            text-align: center;
            width: 95px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: white;
            border: solid 2px #0070C0;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            background-color: #9DC3E6;
        }
        /*-----------------------------------------------------------------------------------------*/
        /*headerのCSS*/
        /*-----------------------------------------------------------------------------------------*/
        #button1 {
            /*     border-width: thin;
            border-style: solid;
            border-color: black */
            text-align: center;
            width: 95px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: white;
            border: solid 2px #0070C0;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            background-color: #9DC3E6;
        }
        /*-----------------------------------------------------------------------------------------*/
        /*mainのCSS*/
        /*上の本社情報の欄*/
        /*-----------------------------------------------------------------------------------------*/

        #table1 {
            width: 1200px;
            height: 300px;
            border-color: #0070C0;
            border: 2px solid #0070C0;
        }

        main th {
            background-color: #9DC3E6;
            border-style: none;
            font-size: 12px;
            width: 100px;
            height: 20px;
            text-align: center;
            font-weight: normal;
        }

        main td {
            background-color: #e6e6e6;
            border-style: none;
        }


        /*-----------------------------------------------------------------------------------------*/
        /*sectionのCSS*/
        /*下の明細のCSS*/
        /*-----------------------------------------------------------------------------------------*/


        #DG {
            width: 1200px;
            height: 300px;
            background-color: white;
        }

        .DG_lbl1 {
            font-size: 30px;
        }

        section th {
            background-color: #DDEBF7;
            border-style: solid;
            border-color: white;
            border-width: 3px;
            font-size: 12px;
            text-align: center;
            font-weight: normal;
        }

        section td {
            border-style: none;
        }

        section table {
            width: 100%;
            background-color: #e6e6e6;
        }

        #table2 {
            border-style: solid;
            border-color: #0070C0;
            border-width: 2px;
        }
        /*-----------------------------------------------------------------------------------------*/
        .btn_siten {
            text-align: center;
            width: 120px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: white;
            border: solid 2px #0070C0;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            background-color: #9DC3E6;
        }
        /*-----------------------------------------------------------------------------------------*/
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <header>
            <uc:Menu ID="Menu" runat="server" />
            <telerik:RadTabStrip ID="RT" runat="server" AutoPostBack="True" SelectedIndex="0" BackColor="#D2EAF6">
                <Tabs>
                    <telerik:RadTab Text="会社情報管理" NavigateUrl="AccountPage.aspx">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <br />
            <asp:Button runat="server" ID="insert_btn" CssClass="btn" Text="登録" OnClick="insert_btn_Click" />
            <asp:Label runat="server" ID="error_lbl1"></asp:Label>
            <asp:Label runat="server" ID="editor_lbl" Text="最終更新者:"></asp:Label>
            <br />
        </header>
        <br />
        <main>
            <table id="table1" border="1">
                <tr>
                    <th>列番号</th>
                    <td>
                        <asp:Label runat="server" ID="lbl1"></asp:Label>
                    </td>
                    <th>市町村コード</th>
                    <td>
                        <asp:TextBox runat="server" ID="tb2" placeholder="123456"></asp:TextBox>
                    </td>
                    <th>電話番号</th>
                    <td>
                        <asp:TextBox runat="server" ID="tb3" placeholder="00-0000-0000"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>企業名</th>
                    <td>
                        <asp:TextBox runat="server" ID="tb4" placeholder="テスト株式会社"></asp:TextBox>
                    </td>
                    <th>住所1</th>
                    <td>
                        <asp:TextBox runat="server" ID="tb5" placeholder="東京都新宿区西新宿1-1-1"></asp:TextBox>
                    </td>
                    <th>FAX</th>
                    <td>
                        <asp:TextBox runat="server" ID="tb6" placeholder="00-0000-0000"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>企業名略称</th>
                    <td>
                        <asp:TextBox runat="server" ID="tb7" placeholder="テスト"></asp:TextBox>
                    </td>
                    <th>住所2（ビル名、階数）</th>
                    <td>
                        <asp:TextBox runat="server" ID="tb8" placeholder="テストビル6階"></asp:TextBox>
                    </td>
                    <th></th>
                    <td></td>
                </tr>
                <tr>
                    <th>代表者名</th>
                    <td>
                        <asp:TextBox runat="server" ID="tb9" placeholder="山田太郎"></asp:TextBox>
                    </td>
                    <th></th>
                    <td></td>
                    <th></th>
                    <td></td>
                </tr>
                <tr>
                    <th>銀行名</th>
                    <td>
                        <asp:TextBox runat="server" ID="tb10" placeholder="テスト銀行"></asp:TextBox>
                    </td>
                    <th>支店名（銀行）</th>
                    <td>
                        <asp:TextBox runat="server" ID="tb11" placeholder="テスト支店"></asp:TextBox>
                    </td>
                    <th>口座区分</th>
                    <td>
                        <asp:TextBox runat="server" ID="tb12" placeholder="普通"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>銀行コード</th>
                    <td>
                        <asp:TextBox runat="server" ID="tb13" placeholder="1234"></asp:TextBox>
                    </td>
                    <th>支店コード</th>
                    <td>
                        <asp:TextBox runat="server" ID="tb14" placeholder="123"></asp:TextBox>
                    </td>
                    <th>口座番号</th>
                    <td>
                        <asp:TextBox runat="server" ID="tb15" placeholder="1234567"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
        </main>
        <section>
            <asp:DataGrid runat="server" ID="DG" AutoGenerateColumns="False" OnSelectedIndexChanged="DG_SelectedIndexChanged" OnItemDataBound="DG_ItemDataBound" OnItemCommand="DG_ItemCommand">
                <HeaderStyle />
                <Columns>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="DG_lbl1" CssClass="DG_lbl1"></asp:Label>
                            <asp:Button runat="server" ID="DG_btn1" Text="削除" CommandName="Delete" CssClass="btn" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <table runat="server" id="table2">
                                <tr>
                                    <th>営業所名</th>
                                    <td>
                                        <asp:TextBox runat="server" ID="t_tb1" placeholder="テスト営業所"></asp:TextBox>
                                    </td>
                                    <th>住所1</th>
                                    <td>
                                        <asp:TextBox runat="server" ID="t_tb3" placeholder="東京都新宿区西新宿1-1-1"></asp:TextBox>
                                    </td>
                                    <th>電話番号</th>
                                    <td>
                                        <asp:TextBox runat="server" ID="t_tb5" placeholder="00-0000-0000"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>市町村コード</th>
                                    <td>
                                        <asp:TextBox runat="server" ID="t_tb2" placeholder="123456"></asp:TextBox>
                                    </td>
                                    <th>住所2（ビル名、階数）</th>
                                    <td>
                                        <asp:TextBox runat="server" ID="t_tb4" placeholder="テストビル6階"></asp:TextBox>
                                    </td>
                                    <th>FAX</th>
                                    <td>
                                        <asp:TextBox runat="server" ID="t_tb6" placeholder="00-0000-0000"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
            <br />
            <asp:Button runat="server" ID="btn2" CssClass="btn_siten" Text="支店情報追加" OnClick="btn2_Click" />
        </section>
    </form>
</body>
</html>
