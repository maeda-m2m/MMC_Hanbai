<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewOrderedInput.aspx.cs" Inherits="Gyomu.Order.NewOrderedInput" %>

<%@ Register Src="~/Order/CtrlNewOrderedMeisai.ascx" TagName="Syosai" TagPrefix="uc2" %>
<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="NewOrderedInput.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .Btn {
            text-align: center;
            width: 100px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: #2e2e2e;
            border: solid 2px #989ae1;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            background-color: #bbbcde;
        }

            .Btn:hover {
                background: #989ae1;
                color: #2e2e2e;
            }

        .TitleTD {
            background-color: #c1c2ff;
            width: 100px;
            text-align: center;
        }

        .MeisaiTD {
            background-color: #e5e5e5;
        }

        .RowNoTD {
            background-color: #c1c2ff;
        }

        .ProductTD {
            background-color: #e5e5e5;
            width: 200px;
        }

        #TbxShiireTanka {
            text-align: right;
        }
    </style>

    <title>発注新規入力</title>
</head>
<body>
    <form id="form1" runat="server">
        <uc:Menu ID="Menu" runat="server" />

        <telerik:RadTabStrip ID="RT" runat="server" AutoPostBack="True" SelectedIndex="1" BorderColor="#c1c2ff" BackColor="#c1c2ff" ForeColor="#c1c2ff">
            <Tabs>
                <telerik:RadTab Text="発注一覧" Font-Size="12pt" NavigateUrl="OrderedList.aspx">
                </telerik:RadTab>
                <telerik:RadTab Text="発注入力" Font-Size="12pt" NavigateUrl="NewOrderedInput.aspx" Selected="True">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <br />
        <table>
            <tr>
                <td>
                    <asp:Button runat="server" ID="BtnKeisan" Text="計算" CssClass="Btn" OnClick="BtnKeisan_Click" />
                </td>
                <td>
                    <asp:Button runat="server" ID="BtnRegister" Text="仕入" CssClass="Btn" OnClick="BtnRegister_Click" />
                </td>
                <td>
                    <asp:Button runat="server" ID="BtnDelete" Text="削除" CssClass="Btn" OnClick="BtnDelete_Click" OnClientClick="A()" />
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
                <td class="TitleTD">
                    <p>仕入先</p>
                </td>
                <td class="MeisaiTD" style="width: 120px">
                    <telerik:RadComboBox ID="ShiireRad" runat="server" AllowCustomText="true" EnableLoadOnDemand="true" MarkFirstMatch="true" ShowMoreResultsBox="true" ShowToggleImage="false" EnableVirtualScrolling="true" OnSelectedIndexChanged="ShiireRad_SelectedIndexChanged" OnItemsRequested="ShiireRad_ItemsRequested" Width="150px" AutoPostBack="true"></telerik:RadComboBox>
                    <input type="hidden" id="HidShiCode" runat="server" />
                    <input type="hidden" id="HidShiName" runat="server" />
                </td>
                <td class="TitleTD">
                    <p>カテゴリ</p>
                </td>
                <td class="MeisaiTD" style="width: 120px">
                    <telerik:RadComboBox runat="server" ID="CategoryRad" AllowCustomText="true" EnableLoadOnDemand="true" MarkFirstMatch="true" ShowMoreResultsBox="true" ShowToggleImage="false" EnableVirtualScrolling="true " OnSelectedIndexChanged="CategoryRad_SelectedIndexChanged" OnItemsRequested="CategoryRad_ItemsRequested" Width="150px" AutoPostBack="true"></telerik:RadComboBox>
                    <input type="hidden" runat="server" id="HidCateCode" />
                    <input type="hidden" runat="server" id="HidCateName" />
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
            <tr>
                <td class="TitleTD">
                    <p>開始</p>
                </td>
                <td class="MeisaiTD">
                    <telerik:RadDatePicker runat="server" ID="StartDate"></telerik:RadDatePicker>
                </td>
                <td class="TitleTD">
                    <p>使用期間</p>
                </td>
                <td class="MeisaiTD">
                    <telerik:RadComboBox runat="server" ID="Kikan" OnSelectedIndexChanged="Kikan_SelectedIndexChanged" AutoPostBack="true">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="使用期間を選択" />
                            <telerik:RadComboBoxItem runat="server" Text="1日" />
                            <telerik:RadComboBoxItem runat="server" Text="2日" />
                            <telerik:RadComboBoxItem runat="server" Text="3日" />
                            <telerik:RadComboBoxItem runat="server" Text="4日" />
                            <telerik:RadComboBoxItem runat="server" Text="5日" />
                            <telerik:RadComboBoxItem runat="server" Text="1ヵ月" />
                            <telerik:RadComboBoxItem runat="server" Text="2ヵ月" />
                            <telerik:RadComboBoxItem runat="server" Text="3ヵ月" />
                            <telerik:RadComboBoxItem runat="server" Text="4ヵ月" />
                            <telerik:RadComboBoxItem runat="server" Text="5ヵ月" />
                            <telerik:RadComboBoxItem runat="server" Text="6ヵ月" />
                            <telerik:RadComboBoxItem runat="server" Text="1年" />
                            <telerik:RadComboBoxItem runat="server" Text="99年" />
                        </Items>
                    </telerik:RadComboBox>
                    <asp:CheckBox runat="server" ID="ChkFuku" Text="複数" OnCheckedChanged="ChkFuku_CheckedChanged" AutoPostBack="true" />
                </td>
                <td class="TitleTD">
                    <p>終了</p>
                </td>
                <td class="MeisaiTD">
                    <telerik:RadDatePicker runat="server" ID="EndDate"></telerik:RadDatePicker>
                </td>
            </tr>
        </table>
        <img src="../Img/新規発注ヘッダ2.png" runat="server" width="1400" />

        <asp:DataGrid runat="server" ID="CtrlSyousai" AutoGenerateColumns="False" OnItemDataBound="CtrlSyousai_ItemDataBound" OnItemCommand="CtrlSyousai_ItemCommand">
            <Columns>

                <asp:TemplateColumn HeaderStyle-BorderStyle="None" ItemStyle-BorderStyle="None">
                    <ItemTemplate>
                        <asp:Button runat="server" ID="BtnDelete" class="Btn" CommandName="Del" Text="削除" />
                    </ItemTemplate>
                </asp:TemplateColumn>

                <asp:TemplateColumn HeaderStyle-BorderStyle="None" ItemStyle-BorderStyle="None">
                    <ItemTemplate>
                        <uc2:Syosai ID="Syosai" runat="server" />
                    </ItemTemplate>
                </asp:TemplateColumn>

            </Columns>
        </asp:DataGrid>
        <table>
            <tr>
                <td>
                    <asp:Button runat="server" ID="BtnAddRow" Text="明細追加" OnClick="BtnAddRow_Click" CssClass="Btn" />
                </td>
                <td>
                    <asp:Button runat="server" ID="BtnKeisan2" Text="計算" OnClick="BtnKeisan_Click" CssClass="Btn" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
