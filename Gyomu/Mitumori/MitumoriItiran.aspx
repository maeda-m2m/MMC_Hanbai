<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MitumoriItiran.aspx.cs" Inherits="Gyomu.Mitumori.MitumoriItiran" %>

<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register TagPrefix="uc2" TagName="CtlNengappiFromTo" Src="~/Common/CtlNengappiForm.ascx" %>
<%@ Register TagPrefix="uc3" TagName="CtlPager" Src="~/Common/CtlPager.ascx" %>

<%@ Register TagPrefix="cc1" Assembly="Core" Namespace="Core.Web" %>
<%@ Register TagPrefix="telerik" Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="Head1">
    <title>受注情報</title>
    <link href="../../Style/Grid.ykuri.css" rel="STYLESHEET" />
    <link href="../../Style/ComboBox.ykuri.css" type="text/css" rel="STYLESHEET" />
    <link href="../../MainStyle.css" type="text/css" rel="Stylesheet" />
    <link href="../sheet/MainStyles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body {
            font-family: Tahoma;
            font-size: 9pt;
        }

        .sl {
            border-bottom: solid 1px silver;
        }

        .g {
            BACKGROUND-COLOR: gray
        }

        .column {
            background-color: #53d153;
            color: black;
            padding: 0.5em;
            font-size: 12px;
        }

        .row {
        }

        .gridh {
            height: 550px;
            width: 95%
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

        .Btn8 {
            font-size: 17px;
            margin-left: 5px;
            text-align: center;
            width: 95px;
            height: 100px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: black;
            border: solid 2px #56e3ff;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            border-radius: 3px;
            background-color: #56e3ff;
        }

            .Btn8:hover {
                background: white;
                color: #eb6600;
            }

        .auto-style2 {
            font-size: 17px;
            margin-left: 5px;
            text-align: center;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: black;
            border: solid 2px #56e3ff;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            border-radius: 3px;
            background-color: #56e3ff;
        }

        #Chkbox input[type=checkbox] {
            width: 24px;
            height: 24px;
            -moz-transform: scale(1.4);
            -webkit-transform: scale(1.4);
            transform: scale(1.4);
        }

        .Btn10 {
            text-align: center;
            width: 95px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: #5d5d5d;
            border: solid 2px #8dea8d;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            background-color: #bae5ba;
        }

            .Btn10:hover {
                background: #53d153;
                color: black;
            }
    </style>

    <script type="text/javascript" src="../../Core.js"></script>

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
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

</head>
<body>
    <form id="form1" runat="server">
        <div id="MainMenu" runat="server">
            <uc:Menu ID="Menu" runat="server" />
        </div>
        <br />
        <div id="Kensaku" runat="server">
            <telerik:RadTabStrip ID="RT" runat="server" AutoPostBack="True" SelectedIndex="1" BackColor="#8dea8d">
                <Tabs>
                    <telerik:RadTab Text="見積一覧" Font-Size="12pt" NavigateUrl="~/Mitumori/MitumoriItiran.aspx" Selected="true">
                    </telerik:RadTab>
                    <telerik:RadTab Text="見積入力" Font-Size="12pt" NavigateUrl="../Kaihatsu.aspx">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <br />

            <table border="1">
                <tbody>
                    <tr>
                        <td class="column">
                            <asp:Literal ID="Literal14" runat="server">受注</asp:Literal>
                        </td>
                        <td>
                            <asp:DropDownList ID="DrpFlg" runat="server" Width="100">
                                <asp:ListItem Selected="True" Value="False">未受注</asp:ListItem>
                                <asp:ListItem Value="True">受注済</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="column">
                            <asp:Literal ID="Literal1" runat="server">見積No</asp:Literal></td>
                        <td class="row">
                            <asp:TextBox ID="TbxMitumoriNo" runat="server"></asp:TextBox>
                        </td>
                        <td class="column">
                            <asp:Literal ID="Literal2" runat="server">得意先</asp:Literal></td>
                        <td class="row">
                            <telerik:RadComboBox ID="RadTokuiMeisyo" runat="server" Width="200" Height="180px" AutoPostBack="true" AllowCustomText="True" EnableLoadOnDemand="True" OnItemsRequested="RadTokuiMeisyo_ItemsRequested"></telerik:RadComboBox>
                        </td>
                        <td class="column">
                            <asp:Literal ID="Literal3" runat="server">請求先</asp:Literal></td>
                        <td class="row">
                            <telerik:RadComboBox ID="RadSekyuMeisyo" runat="server" Width="200" Height="180px" AllowCustomText="True" AutoPostBack="true" EnableLoadOnDemand="True" OnItemsRequested="RadTokuiMeisyo_ItemsRequested"></telerik:RadComboBox>
                        </td>
                        <td class="column">
                            <asp:Literal ID="Literal4" runat="server">直送先</asp:Literal></td>
                        <td class="row">
                            <telerik:RadComboBox ID="RadTyokusoMeisyo" runat="server" Width="350" Height="180px" AutoPostBack="true" EnableLoadOnDemand="True" OnItemsRequested="RadTyokusoMeisyo_ItemsRequested"></telerik:RadComboBox>
                        </td>
                        <td rowspan="3">
                            <asp:Button ID="BtnKensaku" runat="server" Text="検索" Width="100px" OnClick="BtnKensaku_Click" CssClass="Btn10" Height="56px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="column">
                            <asp:Literal ID="Literal5" runat="server">施設</asp:Literal></td>
                        <td class="row" colspan="3">
                            <telerik:RadComboBox ID="RadSisetMeisyo" runat="server" Width="350" Height="180px" AutoPostBack="true" EnableLoadOnDemand="True" OnItemsRequested="RadSisetMeisyo_ItemsRequested"></telerik:RadComboBox>
                        </td>
                        <td class="column">
                            <asp:Literal ID="Literal6" runat="server">カテゴリ</asp:Literal></td>
                        <td class="row">
                            <telerik:RadComboBox ID="RadCate" runat="server" Width="120" AutoPostBack="true"></telerik:RadComboBox>
                        </td>
                        <td class="column">
                            <asp:Literal ID="Literal7" runat="server">部門</asp:Literal></td>
                        <td class="row">
                            <telerik:RadComboBox ID="RadBumon" runat="server" Width="100" Height="180px"></telerik:RadComboBox>
                        </td>
                        <td class="column">
                            <asp:Literal ID="Literal8" runat="server">品番</asp:Literal></td>
                        <td class="row">
                            <asp:TextBox ID="TbxHinban" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="column">
                            <asp:Literal ID="Literal9" runat="server">品名</asp:Literal></td>
                        <td class="row" colspan="3">
                            <telerik:RadComboBox ID="RadSyohinmeisyou" runat="server" Width="400" Height="180px" AutoPostBack="true" EnableLoadOnDemand="True" OnItemsRequested="RadSyohinmeisyou_ItemsRequested"></telerik:RadComboBox>
                        </td>
                        <td class="column">
                            <asp:Literal ID="Literal10" runat="server">担当者</asp:Literal></td>
                        <td class="row">
                            <telerik:RadComboBox ID="RadTanto" runat="server" Width="110" EmptyMessage="-------" Height="180px">
                            </telerik:RadComboBox>
                        </td>
                        <td class="column">
                            <td class="row"></td>
                            <td class="column">
                                <asp:Literal ID="Literal12" runat="server">見積日</asp:Literal></td>
                            <td class="row">
                                <uc2:CtlNengappiFromTo ID="CtlJucyuBi" runat="server" />
                            </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <br />
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        <br />
        <asp:Button ID="BtnDownlod" runat="server" Text="CSVダウンロード" Width="180px" CssClass="Btn10" OnClientClick="PostClick('DL')" OnClick="BtnDownlod_Click" />
        &nbsp;
        <asp:Literal ID="Literal13" runat="server">発行:</asp:Literal>
        <asp:Button ID="BtnPrint" runat="server" Text="印刷" CssClass="Btn10" OnClick="BtnPrint_Click" />
        &nbsp;
        <%--        <asp:Button ID="BtnBitumori" runat="server" Text="見積書" CssClass="Btn2" OnClick="BtnBitumori_Click" />
        <asp:Button ID="BtnNouhin" runat="server" Text="納品書" CssClass="Btn3" OnClick="BtnNouhin_Click" />
        <asp:Button ID="BtnSeikyu" runat="server" Text="請求書" CssClass="Btn4" OnClick="BtnSeikyu_Click" />--%>
        <asp:Button ID="BtnSyusei" runat="server" Text="修正" CssClass="Btn10" OnClick="BtnSyusei_Click1" />
        &nbsp;
        <asp:Button ID="BtnDelete" runat="server" Text="削除" CssClass="Btn10" OnClick="BtnDelete_Click" OnClientClick="A()" />
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
        <div id="Itiran" runat="server">
            <telerik:RadGrid ID="RadG" runat="server" CssClass="def" PageSize="1000" AllowPaging="True" EnableAJAX="True" EnableAJAXLoadingTemplate="True" Skin="ykuri" AllowCustomPaging="True" EnableEmbeddedSkins="False" GridLines="None" CellPadding="0" EnableEmbeddedBaseStylesheet="False" AutoGenerateColumns="False" OnItemDataBound="RadG_ItemDataBound" OnPageIndexChanged="RadG_PageIndexChanged" OnItemCreated="RadG_ItemCreated">
                <PagerStyle Position="Top" AlwaysVisible="true" BackColor="#dfecfe" PagerTextFormat="ページ移動: {4} &amp;nbsp;ページ : &lt;strong&gt;{0:N0}&lt;/strong&gt; / &lt;strong&gt;{1:N0}&lt;/strong&gt; | 件数: &lt;strong&gt;{2:N0}&lt;/strong&gt; - &lt;strong&gt;{3:N0}件&lt;/strong&gt; / &lt;strong&gt;{5:N0}&lt;/strong&gt;件中" PageSizeLabelText="ページサイズ:" FirstPageToolTip="最初のページに移動" LastPageToolTip="最後のページに移動" NextPageToolTip="次のページに移動" PrevPageToolTip="前のページに移動" />
                <HeaderStyle Font-Size="8" HorizontalAlign="Center" BackColor="#53d153" ForeColor="black" BorderColor="#53d153" />
                <ItemStyle Wrap="true" VerticalAlign="Middle" Font-Size="8" />
                <HeaderContextMenu EnableEmbeddedBaseStylesheet="False" CssClass="GridContextMenu GridContextMenu_Outlook">
                </HeaderContextMenu>
                <AlternatingItemStyle Font-Size="9" BackColor="#bae5ba" />
                <MasterTableView CellPadding="2" GridLines="Both" BorderWidth="1" BorderColor="#000000" CellSpacing="0" AutoGenerateColumns="False" AllowMultiColumnSorting="false" AllowNaturalSort="false">
                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="ColChk_Row">
                            <ItemStyle HorizontalAlign="Center" Wrap="false"></ItemStyle>
                            <HeaderStyle Width="24" />
                            <ItemTemplate>
                                <input id="ChkRow" name="ChkRow" runat="server" type="checkbox" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>


                        <telerik:GridTemplateColumn UniqueName="ColMitumori" HeaderText="見積No">
                            <HeaderStyle Width="80" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15"></ItemStyle>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColCategori" HeaderText="カテゴリ">
                            <HeaderStyle Width="80" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColBumon" HeaderText="部門">
                            <HeaderStyle Width="100" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle Wrap="false" Font-Size="15" HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColTantousya" HeaderText="担当者">
                            <HeaderStyle Width="65" Font-Size="15" HorizontalAlign="Center" />
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColTokuisakiCode" HeaderText="得意先コード">
                            <HeaderStyle Width="80" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColTokuisakiName" HeaderText="得意先名">
                            <HeaderStyle Width="215" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColSisetu" HeaderText="施設">
                            <HeaderStyle Font-Size="15" HorizontalAlign="Center" Width="250" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColSuryo" HeaderText="数量">
                            <HeaderStyle Width="30" Font-Size="15" HorizontalAlign="Center" />
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColKingaku" HeaderText="金額">
                            <HeaderStyle Width="50" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle Wrap="false" Font-Size="15" HorizontalAlign="Right" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColMitumoriDay" HeaderText="見積日">
                            <HeaderStyle Width="100" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>
                    </Columns>

                    <EditFormSettings>
                        <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                        </EditColumn>
                    </EditFormSettings>

                    <PagerStyle AlwaysVisible="True"></PagerStyle>
                </MasterTableView>
                <FilterMenu EnableImageSprites="False" EnableEmbeddedBaseStylesheet="False">
                </FilterMenu>
                <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>

                <ClientSettings>
                    <ClientEvents OnGridCreated="Core.ResizeRadGrid" />
                    <Scrolling AllowScroll="True" FrozenColumnsCount="0" ScrollHeight="600px" EnableColumnClientFreeze="True" UseStaticHeaders="True" />
                </ClientSettings>
            </telerik:RadGrid>
            <input type="hidden" id="count" runat="server" />
            <telerik:RadAjaxManager ID="Ram" runat="server" OnAjaxRequest="Ram_AjaxRequest">
                <%--<ClientEvents OnResponseEnd="OnResponseEnd" OnRequestStart="OnRequestStart" /> --%>
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

        </div>
    </form>

</body>
</html>

