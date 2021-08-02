<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppropriateInput.aspx.cs" Inherits="Gyomu.Appropriate.AppropriateInput" %>

<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Src="~/Order/CtlOrderedMeisai.ascx" TagName="Syosai" TagPrefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../Order/Ordered.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .Btn {
            text-align: center;
            width: 100px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: #2e2e2e;
            border: solid 2px #57c7ff;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            background-color: #aae3ff;
        }

            .Btn:hover {
                background: #98c7de;
                color: #000000;
                border solid 3px #49a8d7;
            }
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <%--        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
        <div id="MainMenu" runat="server">
            <uc:Menu ID="Menu" runat="server" />
        </div>

        <div>
            <telerik:RadTabStrip ID="RT" runat="server" AutoPostBack="True" BackColor="#d2eaf6">
                <Tabs>
                    <telerik:RadTab Text="仕入一覧" Font-Size="12pt" NavigateUrl="ApproptiateList.aspx"></telerik:RadTab>
                </Tabs>
                <Tabs>
                    <telerik:RadTab Text="仕入計上" Font-Size="12pt" NavigateUrl="AppropriateInput.aspx" Selected="true">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
        </div>
        <br />
        <table>
            <tr>
                <td>
                    <asp:Button runat="server" ID="BtnKeisan" Text="計算" CssClass="Btn" />
                </td>
                <td>
                    <asp:Button runat="server" ID="BtnRegister" Text="登録" CssClass="Btn" OnClick="BtnRegister_Click" />
                </td>
                <td>
                    <asp:Button runat="server" ID="BtnDelete" Text="削除" CssClass="Btn" OnClientClick="A()" />
                    <script type="text/javascript">
                        function A() {
                            var a1 = window.confirm('本当に削除しますか？');
                            if (a1 === true) {
                                return (true)
                            }
                            else if (a1 === false) {
                                return (false
                                )
                            }
                        }
                    </script>

                </td>
            </tr>
        </table>
        <br />
        <asp:Label runat="server" ID="Err" ForeColor="Red"></asp:Label>
        <asp:Label runat="server" ID="End" ForeColor="Green"></asp:Label>

        <table class="MeisaiTB">
            <tr>
                <td style="background-color: #80c9ed; text-align: center;">
                    <p>仕入先</p>
                </td>
                <td class="MeisaiTD" style="width: 120px">
                    <asp:Label runat="server" ID="LblShiiresaki"></asp:Label>
                </td>
                <td style="background-color: #80c9ed; text-align: center;">
                    <p>カテゴリ</p>
                </td>
                <td class="MeisaiTD" style="width: 120px">
                    <asp:Label runat="server" ID="LblCategory"></asp:Label>
                </td>
                <td style="background-color: #80c9ed; text-align: center;">
                    <p>発注No</p>
                </td>
                <td class="MeisaiTD" style="text-align: right; width: 120px">
                    <asp:Label runat="server" ID="LblOrdered"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="background-color: #80c9ed; text-align: center;">
                    <p>数量合計</p>
                </td>
                <td class="MeisaiTD" style="width: 120px">
                    <asp:Label runat="server" ID="LblSu"></asp:Label>
                </td>
                <td style="background-color: #80c9ed; text-align: center;">
                    <p>仕入合計金額</p>
                </td>
                <td class="MeisaiTD" style="text-align: right; width: 120px">
                    <asp:Label runat="server" ID="LblShiirekei"></asp:Label>
                </td>
                <td style="background-color: #80c9ed; text-align: center;">
                    <p>発注日付</p>
                </td>
                <td class="MeisaiTD" style="width: 120px">
                    <asp:Label runat="server" ID="LblOrderedDate"></asp:Label>
                </td>
            </tr>
        </table>

        <asp:DataGrid runat="server" ID="CtrlSyousai" AutoGenerateColumns="False" OnItemDataBound="CtrlSyousai_ItemDataBound" OnItemCommand="CtrlSyousai_ItemCommand">
            <Columns>

                <asp:TemplateColumn HeaderStyle-BorderStyle="None" ItemStyle-BorderStyle="None">
                    <ItemTemplate>
                        <uc2:Syosai ID="Syosai" runat="server" />
                    </ItemTemplate>
                </asp:TemplateColumn>

                <asp:TemplateColumn HeaderStyle-BorderStyle="None" ItemStyle-BorderStyle="None">
                    <ItemTemplate>
                        <table runat="server">
                            <tr>
                                <td class="Nyuka" style="width: 30px;">
                                    <asp:Label runat="server" ID="LblNokori" ForeColor="Red" Text=""></asp:Label>
                                </td>
                                <td class="Nyuka">
                                    <asp:TextBox runat="server" ID="TbxNyukasu" Width="30px"></asp:TextBox>
                                </td>
                                <td class="Nyuka">
                                    <asp:Button runat="server" ID="BtnLogList" Text="ログ" CssClass="Btn2" CommandName="Log" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" class="Nyuka">
                                    <asp:Button runat="server" ID="BtnUpdate" Text="✔" CssClass="Btn" CommandName="Update" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
        <asp:Button runat="server" ID="BtnAddLine" Text="明細追加" CssClass="Btn" OnClick="BtnAddLine_Click" />
    </form>
</body>
</html>
