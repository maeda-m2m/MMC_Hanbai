<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderedInput.aspx.cs" Inherits="Gyomu.Order.OrderedInput" %>

<%@ Register Src="~/Order/CtlOrderedMeisai2.ascx" TagName="Syosai" TagPrefix="uc2" %>
<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="Ordered.css" type="text/css" rel="stylesheet" />
    <script src="http://code.jquery.com/jquery-1.11.3.min.js"></script>
    <script src="http://code.jquery.com/jquery-migrate-1.2.1.min.js"></script>
    <script src="Ordered.js"></script>

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <uc:Menu ID="Menu" runat="server" />

        <telerik:RadTabStrip ID="RT" runat="server" AutoPostBack="True" SelectedIndex="1" BorderColor="#c1c2ff" BackColor="#c1c2ff" ForeColor="#c1c2ff">
            <Tabs>
                <telerik:RadTab Text="発注一覧" Font-Size="12pt" NavigateUrl="OrderedList.aspx">
                </telerik:RadTab>
                <telerik:RadTab Text="発注入力" Font-Size="12pt" NavigateUrl="OrderedInput.aspx" Selected="True">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <br />


        <table>
            <tr>
                <td>
                    <asp:Button runat="server" ID="BtnKeisan" Text="計算" CssClass="Btn" OnClick="BtnKeisan_Click" />
                    <script type="text/javascript">
                        function Keisan(tbx) {
                            debugger;
                            var ary = tbx.split('-');
                            debugger;
                            var tan = document.getElementById(ary[0]).value;
                            var su = document.getElementById(ary[2]).value;
                            debugger;
                            document.getElementById(ary[1]).value = tan * su;
                        }
                    </script>
                </td>
                <td>
                    <asp:Button runat="server" ID="BtnRegister" Text="仕入" CssClass="Btn" OnClick="BtnRegister_Click" />
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


        <asp:Label runat="server" ID="Err" ForeColor="Red"></asp:Label>
        <asp:Label runat="server" ID="End" ForeColor="Green"></asp:Label>

        <table class="MeisaiTB">
            <tr>
                <td class="TitleTD">
                    <p>仕入先</p>
                </td>
                <td class="MeisaiTD" style="width: 120px">
                    <asp:Label runat="server" ID="LblShiiresaki"></asp:Label>
                </td>
                <td class="TitleTD">
                    <p>カテゴリ</p>
                </td>
                <td class="MeisaiTD" style="width: 120px">
                    <asp:Label runat="server" ID="LblCategory"></asp:Label>
                </td>
                <td class="TitleTD">
                    <p>発注No</p>
                </td>
                <td class="MeisaiTD" style="width: 120px">
                    <asp:Label runat="server" ID="LblOrdered"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="TitleTD">
                    <p>数量合計</p>
                </td>
                <td class="MeisaiTD" style="width: 120px">
                    <asp:Label runat="server" ID="LblSu"></asp:Label>
                </td>
                <td class="TitleTD">
                    <p>仕入合計金額</p>
                </td>
                <td class="MeisaiTD" style="text-align: right; width: 120px">
                    <asp:Label runat="server" ID="LblShiirekei"></asp:Label>
                </td>
                <td class="TitleTD">
                    <p>発注日付</p>
                </td>
                <td class="MeisaiTD" style="width: 120px">
                    <asp:Label runat="server" ID="LblOrderedDate"></asp:Label>
                </td>
            </tr>
        </table>
        <input type="hidden" runat="server" id="HidTokusakiCode" />
        <input type="hidden" runat="server" id="HidTokuisakiName" />

        <asp:DataGrid runat="server" ID="CtrlSyousai" AutoGenerateColumns="False" OnItemDataBound="CtrlSyousai_ItemDataBound" OnItemCommand="CtrlSyousai_ItemCommand">
            <Columns>

                <asp:TemplateColumn HeaderStyle-BorderStyle="None" ItemStyle-BorderStyle="None">
                    <ItemTemplate>
                        <uc2:Syosai ID="Syosai" runat="server" />
                    </ItemTemplate>
                </asp:TemplateColumn>

            </Columns>
        </asp:DataGrid>
    </form>
</body>
</html>
