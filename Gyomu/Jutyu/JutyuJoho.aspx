<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JutyuJoho.aspx.cs" Inherits="Gyomu.Jutyu.Jutyujoho" %>

<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>

<%@ Register Src="../Common/CtlNengappiForm.ascx" TagName="CtlNengappiFromTo" TagPrefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>受注情報</title>
    <%--    <link href="../../Style/Grid.ykuri.css" rel="STYLESHEET" />
    <link href="../../Style/ComboBox.ykuri.css" type="text/css" rel="STYLESHEET" />
    <link href="../../MainStyle.css" type="text/css" rel="Stylesheet" />
    <link href="../sheet/MainStyles.css" rel="stylesheet" type="text/css" />--%>
    <style type="text/css">
        body {
            font-family: 'Meiryo UI';
            padding: 0;
            margin: 0;
        }

        .column {
            text-align: center;
            color: black;
            padding: 0.5em;
            font-size: 12px;
            width: 60px;
            background-color: #ffd900;
        }

        .row {
            background-color: #f3f3f3;
        }

        .auto-style2 {
            border-width: 0;
            padding-left: 5px;
            padding-right: 4px;
            padding-top: 0;
            padding-bottom: 0;
        }

        .auto-style3 {
            border-width: 0;
            padding: 0;
        }

        .Btn2 {
            font-size: 17px;
            margin-left: 5px;
            text-align: center;
            width: 95px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: white;
            border: solid 2px #ea0000;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            border-radius: 3px;
            background-color: #ea0000;
        }

            .Btn2:hover {
                background: white;
                color: #ea0000;
            }

        .Btn3 {
            font-size: 17px;
            margin-left: 5px;
            text-align: center;
            width: 95px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: white;
            border: solid 2px #ea0088;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            border-radius: 3px;
            background-color: #ea0088;
        }

            .Btn3:hover {
                background: white;
                color: #ea0088;
            }


        .Btn4 {
            font-size: 17px;
            margin-left: 5px;
            text-align: center;
            width: 95px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: white;
            border: solid 2px #7900ea;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            border-radius: 3px;
            background-color: #7900ea;
        }

            .Btn4:hover {
                background: white;
                color: #7900ea;
            }

        .Btn5 {
            font-size: 17px;
            margin-left: 5px;
            text-align: center;
            width: 95px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: white;
            border: solid 2px #00afea;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            border-radius: 3px;
            background-color: #00afea;
        }

            .Btn5:hover {
                background: white;
                color: #00afea;
            }

        .Btn6 {
            font-size: 17px;
            margin-left: 5px;
            text-align: center;
            width: 95px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: white;
            border: solid 2px #00d407;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            border-radius: 3px;
            background-color: #00d407;
        }

            .Btn6:hover {
                background: white;
                color: #00d407;
            }

        .Btn7 {
            font-size: 17px;
            margin-left: 5px;
            text-align: center;
            width: 95px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: white;
            border: solid 2px #eb6600;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            border-radius: 3px;
            background-color: #eb6600;
        }

            .Btn7:hover {
                background: white;
                color: #eb6600;
            }

        .Btn10 {
            text-align: center;
            width: 95px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: #5d5d5d;
            border: solid 2px #ffd900;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            background-color: #ffef93;
        }

            .Btn10:hover {
                background: #ffd900;
                color: black;
            }

        .gv a {
            background-color: white;
            text-decoration: none;
            color: #c0a300;
            font-size: 15px;
        }

            .gv a:link {
                background-color: white;
                text-decoration: none;
                color: #c0a300;
                font-weight: bold;
                font-size: 15px;
            }

            .gv a:visited {
                background-color: darkgreen;
                text-decoration: none;
                color: #c0a300;
                font-weight: bold;
                font-size: 15px;
            }
    </style>

    <script type="text/javascript">

        function CntRow(cnt) {
            document.forms[0].count.value = cnt;
            return;
        }

        function JtuRow(cnt) {
            document.forms[0].count.value = cnt;
            return;
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="MainMenu" runat="server">
            <uc:Menu ID="Menu" runat="server" />
        </div>
        <br />
        <div id="Kensaku" runat="server">
            <telerik:RadTabStrip ID="RT" runat="server" AutoPostBack="True" SelectedIndex="0" BackColor="#ffef93">
                <Tabs>
                    <telerik:RadTab Text="受注情報" Font-Size="12pt" NavigateUrl="~/Jutyu/JutyuJoho.aspx" Selected="True">
                    </telerik:RadTab>
                    <telerik:RadTab Text="受注入力" Font-Size="12pt" NavigateUrl="../OrderInput.aspx">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <br />
            <table style="width: 100%; border-collapse: collapse; border: 1px solid #9d9d9d">
                <tr>
                    <td>
                        <table style="width: 100%; border-collapse: collapse">
                            <tr>
                                <td class="column">
                                    <p>発注</p>
                                </td>
                                <td class="row">
                                    <asp:DropDownList ID="DrpFlg" runat="server" Width="100">
                                        <asp:ListItem Selected="True" Value="False">未発注</asp:ListItem>
                                        <asp:ListItem Value="True">発注済み</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="column">
                                    <p>受注No</p>
                                </td>
                                <td class="row">
                                    <asp:TextBox ID="TbxJutyuNo" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td class="column">
                                    <p>担当者</p>
                                </td>
                                <td class="row">
                                    <telerik:RadComboBox ID="RadTanto" runat="server" Width="110" EmptyMessage="-------" Height="180px"></telerik:RadComboBox>
                                </td>
                                <td class="column">
                                    <p>部門</p>
                                </td>
                                <td class="row">
                                    <telerik:RadComboBox ID="RadBumon" runat="server" Width="100" Height="180px"></telerik:RadComboBox>
                                </td>
                                <td class="column">
                                    <p>見積日</p>
                                </td>
                                <td class="row">
                                    <uc2:CtlNengappiFromTo ID="CtlJucyuBi" runat="server" />
                                </td>
                                <td class="column">
                                    <p>カテゴリ</p>
                                </td>
                                <td class="row">
                                    <telerik:RadComboBox ID="RadCate" runat="server" Width="120" AutoPostBack="true"></telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td rowspan="3" class="row">
                        <asp:Button ID="BtnKensaku" runat="server" Text="検索" Width="100px" OnClick="BtnKensaku_Click" CssClass="Btn10" Height="56px" />
                    </td>

                </tr>
                <tr>
                    <td>
                        <table style="width: 100%; border-collapse: collapse">
                            <tr>
                                <td class="column">
                                    <p>得意先</p>
                                </td>
                                <td colspan="2" class="row">
                                    <telerik:RadComboBox ID="RadTokuiMeisyo" runat="server" Width="200" Height="180px" AutoPostBack="true" AllowCustomText="True" EnableLoadOnDemand="True" OnItemsRequested="RadTokuiMeisyo_ItemsRequested"></telerik:RadComboBox>
                                </td>
                                <td class="column">
                                    <p>請求先</p>
                                </td>
                                <td colspan="2" class="row">
                                    <telerik:RadComboBox ID="RadSekyuMeisyo" runat="server" Width="200" Height="180px" AllowCustomText="True" AutoPostBack="true" EnableLoadOnDemand="True" OnItemsRequested="RadTokuiMeisyo_ItemsRequested"></telerik:RadComboBox>
                                </td>
                                <td class="column">
                                    <p>直送先</p>
                                </td>
                                <td colspan="2" class="row">
                                    <telerik:RadComboBox ID="RadTyokusoMeisyo" runat="server" Width="250" Height="180px" AutoPostBack="true" EnableLoadOnDemand="True" OnItemsRequested="RadTyokusoMeisyo_ItemsRequested"></telerik:RadComboBox>
                                </td>
                                <td class="column">
                                    <p>施設</p>
                                </td>
                                <td colspan="2" class="row">
                                    <telerik:RadComboBox ID="RadSisetMeisyo" runat="server" Width="250" Height="180px" AutoPostBack="true" EnableLoadOnDemand="True" OnItemsRequested="RadSisetMeisyo_ItemsRequested"></telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%; border-collapse: collapse">
                            <tr>
                                <td class="column">
                                    <p>品番</p>
                                </td>
                                <td class="row">
                                    <asp:TextBox ID="TbxHinban" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td class="column">
                                    <p>品名</p>
                                </td>
                                <td colspan="9" class="row">
                                    <telerik:RadComboBox ID="RadSyohinmeisyou" runat="server" Width="400px" Height="100px" AutoPostBack="true" EnableLoadOnDemand="True" OnItemsRequested="RadSyohinmeisyou_ItemsRequested"></telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        <br />
        <asp:Button ID="BtnDownlod" runat="server" Text="CSVダウンロード" Width="180px" CssClass="Btn10" OnClick="BtnDownlod_Click" />
        &nbsp;
        <asp:Button runat="server" Text="明細CSVダウンロード" Width="180px" CssClass="Btn10" ID="BtnMeisaiCSVdownload" OnClick="BtnMeisaiCSVdownload_Click" />
        &nbsp;
        <asp:Button ID="BtnEdit" runat="server" Text="修正" CssClass="Btn10" Width="100px" OnClick="BtnEdit_Click" />
        &nbsp;
        <asp:Button ID="BtnOrdered" runat="server" Text="発注" CssClass="Btn10" Width="100px" OnClick="BtnOrdered_Click" />
        &nbsp;
        <asp:Button ID="BtnDel" runat="server" Text="削除" CssClass="Btn10" Width="100px" OnClick="BtnDel_Click" OnClientClick="A()" />
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

        <br />
        <br />

        <%-- <asp:Button ID="BtnBitumori" runat="server" Text="見積書" Width="100px" style="height: 21px" />
        <asp:Button ID="BtnNouhin" runat="server" Text="納品書" Width="100px" style="height: 21px" />
        <asp:Button ID="BtnSeikyu" runat="server" Text="請求書" Width="100px" style="height: 21px" />--%>
        <div id="Itiran" runat="server">
            <telerik:RadGrid ID="RadG" Width="100%" EnableEmbeddedBaseStylesheet="true" EnableEmbeddedSkins="false" AllowCustomPaging="false" runat="server" PageSize="200" AllowPaging="True" AutoGenerateColumns="False" OnItemDataBound="RadG_ItemDataBound" OnPageIndexChanged="RadG_PageIndexChanged" OnItemCreated="RadG_ItemCreated">
                <PagerStyle AlwaysVisible="true" Position="Top" PageSizeControlType="None" Mode="NumericPages" CssClass="gv" />
                <HeaderStyle BackColor="#ffd900" Font-Size="13" />
                <AlternatingItemStyle BackColor="#fff2ab" />
                <MasterTableView>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="ColChk_Row">
                            <HeaderStyle Width="50" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false"></ItemStyle>
                            <ItemTemplate>
                                <input id="ChkRow" runat="server" type="checkbox" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="Coljutyu" HeaderText="受注No">
                            <HeaderStyle Width="50" HorizontalAlign="Center" Font-Size="10" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="10"></ItemStyle>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColCategori" HeaderText="カテゴリ">
                            <HeaderStyle Width="65" HorizontalAlign="Center" Font-Size="10" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="10" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColBumon" HeaderText="部門">
                            <HeaderStyle Width="100" HorizontalAlign="Center" Font-Size="10" />
                            <ItemStyle Wrap="false" Font-Size="10" HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColTantousya" HeaderText="担当者">
                            <HeaderStyle Width="65" HorizontalAlign="Center" Font-Size="10" />
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="10" />
                        </telerik:GridTemplateColumn>


                        <telerik:GridTemplateColumn UniqueName="ColTokuisakiCode" HeaderText="得意先コード">
                            <HeaderStyle Width="130" HorizontalAlign="Center" Font-Size="10" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="10" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColTokuisakiName" HeaderText="得意先名">
                            <HeaderStyle Width="350" HorizontalAlign="Center" Font-Size="10" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="10" />
                        </telerik:GridTemplateColumn>


                        <telerik:GridTemplateColumn UniqueName="ColSisetu" HeaderText="施設">
                            <HeaderStyle HorizontalAlign="Center" Font-Size="10" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="10" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColSuryo" HeaderText="数量">
                            <HeaderStyle Width="50" HorizontalAlign="Center" Font-Size="10" />
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" Font-Size="10" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColKingaku" HeaderText="金額">
                            <HeaderStyle Width="100" Font-Size="10" />
                            <ItemStyle Wrap="false" Font-Size="10" HorizontalAlign="Right" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColMitumoriDay" HeaderText="受注日">
                            <HeaderStyle Width="100" HorizontalAlign="Center" Font-Size="10" />
                            <ItemStyle Wrap="false" Font-Size="10" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <input type="hidden" id="count" runat="server" />
        </div>
        <telerik:RadAjaxManager ID="Ram" runat="server" OnAjaxRequest="Ram_AjaxRequest">
            <ClientEvents OnResponseEnd="OnResponseEnd" OnRequestStart="OnRequestStart" />
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="BtnL">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="TblK" />
                        <telerik:AjaxUpdatedControl ControlID="TblList" LoadingPanelID="LP"
                            UpdatePanelHeight="200px" UpdatePanelRenderMode="Inline" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
    </form>
</body>
</html>

