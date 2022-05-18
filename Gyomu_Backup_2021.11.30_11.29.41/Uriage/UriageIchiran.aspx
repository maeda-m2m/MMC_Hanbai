<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UriageIchiran.aspx.cs" Inherits="Gyomu.Uriage.UriageIchiran" %>

<%@ Register TagPrefix="uc2" TagName="CtlNengappiFromTo" Src="~/Common/CtlNengappiForm.ascx" %>

<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../MainStyle.css" type="text/css" rel="Stylesheet" />
    <link href="../sheet/MainStyles.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .Btn10 {
            font-size: 17px;
            margin-left: 5px;
            text-align: center;
            width: 95px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: white;
            border: solid 2px #619fed;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            border-radius: 3px;
            background-color: #92baec;
        }

            .Btn10:hover {
                background: #619fed;
                color: white;
                border: solid 2px #2c75d0;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="MainMenu" runat="server">
            <uc:Menu ID="Menu" runat="server" />
        </div>
        <br />
        <div id="Kensaku" runat="server">
            <telerik:RadTabStrip ID="RT" runat="server" AutoPostBack="True" SelectedIndex="0" BackColor="#d2eaf6">
                <Tabs>
                    <telerik:RadTab Text="売上一覧" Font-Size="12pt" NavigateUrl="UriageIchiran.aspx" Selected="True">
                    </telerik:RadTab>
                    <telerik:RadTab Text="売上入力" Font-Size="12pt" NavigateUrl="UriageMeisai.aspx">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <br />
            <table border="1" style="background-color: #c4ddff;">
                <tbody>
                    <tr>
                        <td class="column" style="background-color: #006bff; color: white;">
                            <asp:Literal ID="Literal12" runat="server">計上</asp:Literal></td>
                        <td class="row">
                            <asp:DropDownList ID="DrpFlg" runat="server" Width="100">
                                <asp:ListItem Value="False">未登録</asp:ListItem>
                                <asp:ListItem Value="True">登録済</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="column" style="background-color: #006bff; color: white;">
                            <asp:Literal ID="Literal1" runat="server">受注No</asp:Literal></td>
                        <td class="row">
                            <asp:TextBox ID="TbxUriageNo" runat="server"></asp:TextBox>
                        </td>
                        <td class="column" style="background-color: #006bff; color: white;">
                            <asp:Literal ID="Literal3" runat="server">得意先名</asp:Literal></td>
                        <td class="row">
                            <telerik:RadComboBox ID="RadTokuiMeisyo" runat="server" Width="200" Height="180px" AutoPostBack="true" AllowCustomText="True" EnableLoadOnDemand="True" OnItemsRequested="RadTokuiMeisyo_ItemsRequested"></telerik:RadComboBox>
                        </td>
                        <td class="column" style="background-color: #006bff; color: white;">
                            <asp:Literal ID="Literal4" runat="server">請求先</asp:Literal></td>
                        <td class="row">
                            <telerik:RadComboBox ID="RadSekyuMeisyo" runat="server" Width="200" Height="180px" AllowCustomText="True" AutoPostBack="true" EnableLoadOnDemand="True" OnItemsRequested="RadSekyuMeisyo_ItemsRequested"></telerik:RadComboBox>
                        </td>
                        <td class="column" style="background-color: #006bff; color: white;">
                            <asp:Literal ID="Literal14" runat="server">直送先</asp:Literal></td>
                        <td>
                            <telerik:RadComboBox ID="RadTyokusoMeisyo" runat="server" Width="350" Height="180px" AutoPostBack="true" EnableLoadOnDemand="True" OnItemsRequested="RadTyokusoMeisyo_ItemsRequested"></telerik:RadComboBox>
                        </td>
                        <td rowspan="3">
                            <asp:Button ID="BtnKensaku" runat="server" Text="検索" Width="100px" Style="height: 50px" OnClick="BtnKensaku_Click" CssClass="Btn10" />
                        </td>
                    </tr>
                    <tr>
                        <td class="column" style="background-color: #006bff; color: white;">
                            <asp:Literal ID="Literal5" runat="server">施設</asp:Literal></td>
                        <td class="row" colspan="3">
                            <telerik:RadComboBox ID="RadSisetMeisyo" runat="server" Width="350" Height="180px" AutoPostBack="true" EnableLoadOnDemand="True" OnItemsRequested="RadSisetMeisyo_ItemsRequested"></telerik:RadComboBox>
                        </td>
                        <td class="column" style="background-color: #006bff; color: white;">
                            <asp:Literal ID="Literal6" runat="server">カテゴリ</asp:Literal></td>
                        <td class="row">
                            <telerik:RadComboBox ID="RadCate" runat="server" Width="120" AutoPostBack="true"></telerik:RadComboBox>
                        </td>
                        <td class="column" style="background-color: #006bff; color: white;">
                            <asp:Literal ID="Literal7" runat="server">部門</asp:Literal></td>
                        <td class="row">
                            <telerik:RadComboBox ID="RadBumon" runat="server" Width="100" Height="180px"></telerik:RadComboBox>
                        </td>
                        <td class="column" style="background-color: #006bff; color: white;">
                            <asp:Literal ID="Literal8" runat="server">品番</asp:Literal></td>
                        <td class="row">
                            <asp:TextBox ID="TbxHinban" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="column" style="background-color: #006bff; color: white;">
                            <asp:Literal ID="Literal15" runat="server">品名</asp:Literal>
                        </td>
                        <td colspan="3">
                            <telerik:RadComboBox ID="RadSyohinmeisyou" runat="server" Width="400" Height="180px" AutoPostBack="true" EnableLoadOnDemand="True" OnItemsRequested="RadSyohinmeisyou_ItemsRequested"></telerik:RadComboBox>
                        </td>
                        <td class="column" style="background-color: #006bff; color: white;">
                            <asp:Literal ID="Literal9" runat="server">担当者</asp:Literal></td>
                        <td class="row">
                            <telerik:RadComboBox ID="RadTanto" runat="server" Width="110" EmptyMessage="-------" Height="180px">
                            </telerik:RadComboBox>
                        </td>
                        <td class="column" style="background-color: #006bff; color: white;">
                            <asp:Literal ID="Literal2" runat="server">入力者</asp:Literal></td>
                        <td class="row">
                            <telerik:RadComboBox ID="RadNyuryoku" runat="server" Width="110" EmptyMessage="-------" Height="180px">
                            </telerik:RadComboBox>
                        </td>
                        <td class="column" style="background-color: #006bff; color: white;">
                            <asp:Literal ID="Literal10" runat="server">受注日</asp:Literal></td>
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
        <asp:Button ID="BtnDownlod" runat="server" Text="CSVダウンロード" Width="180px" CssClass="Btn10" OnClick="BtnDownlod_Click" />
        &nbsp;
        <asp:Button ID="BtnEdit" runat="server" Text="修正" CssClass="Btn10" Width="100px" OnClick="BtnEdit_Click" />
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

        &nbsp;
        <asp:Button ID="BtnPrint" runat="server" Text="印刷" CssClass="Btn10" Width="100px" OnClick="BtnPrint_Click" />
        <br />
        <br />

        <%-- <asp:Button ID="BtnBitumori" runat="server" Text="見積書" Width="100px" style="height: 21px" />
        <asp:Button ID="BtnNouhin" runat="server" Text="納品書" Width="100px" style="height: 21px" />
        <asp:Button ID="BtnSeikyu" runat="server" Text="請求書" Width="100px" style="height: 21px" />--%>
        <div id="Itiran" runat="server">
            <telerik:RadGrid ID="RadG" runat="server" CssClass="def" PageSize="1000" AllowPaging="True" EnableAJAX="True" EnableAJAXLoadingTemplate="True" Skin="ykuri" AllowCustomPaging="True" EnableEmbeddedSkins="False" GridLines="None" CellPadding="0" EnableEmbeddedBaseStylesheet="False" AutoGenerateColumns="False" OnItemDataBound="RadG_ItemDataBound" OnPageIndexChanged="RadG_PageIndexChanged" OnItemCommand="RadG_ItemCommand" OnItemCreated="RadG_ItemCreated">
                <PagerStyle Position="Top" AlwaysVisible="true" BackColor="#dfecfe" PagerTextFormat="ページ移動: {4} &amp;nbsp;ページ : &lt;strong&gt;{0:N0}&lt;/strong&gt; / &lt;strong&gt;{1:N0}&lt;/strong&gt; | 件数: &lt;strong&gt;{2:N0}&lt;/strong&gt; - &lt;strong&gt;{3:N0}件&lt;/strong&gt; / &lt;strong&gt;{5:N0}&lt;/strong&gt;件中" PageSizeLabelText="ページサイズ:" FirstPageToolTip="最初のページに移動" LastPageToolTip="最後のページに移動" NextPageToolTip="次のページに移動" PrevPageToolTip="前のページに移動" />
                <HeaderStyle Font-Size="8" HorizontalAlign="Center" CssClass="hd yt st" />
                <ItemStyle Wrap="true" VerticalAlign="Middle" Font-Size="9" />
                <HeaderContextMenu EnableEmbeddedBaseStylesheet="False" CssClass="GridContextMenu GridContextMenu_Outlook">
                </HeaderContextMenu>
                <AlternatingItemStyle Font-Size="9" CssClass="alterRow" />
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
                                <input id="ChkRow" runat="server" type="checkbox" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColUriage" HeaderText="売上No">
                            <HeaderStyle Width="90" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15"></ItemStyle>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColCategori" HeaderText="カテゴリ">
                            <HeaderStyle Width="70" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColBumon" HeaderText="部門">
                            <HeaderStyle Width="80" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle Wrap="false" Font-Size="15" HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColTantousya" HeaderText="担当者">
                            <HeaderStyle Width="80" Font-Size="15" HorizontalAlign="Center" />
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>


                        <telerik:GridTemplateColumn UniqueName="ColTokuisakiCode" HeaderText="得意先コード">
                            <HeaderStyle Width="80" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColTokuisakiName" HeaderText="得意先名">
                            <HeaderStyle Width="180" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>


                        <telerik:GridTemplateColumn UniqueName="ColSisetu" HeaderText="施設">
                            <HeaderStyle Font-Size="15" HorizontalAlign="Center" Width="150" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColSuryo" HeaderText="数量">
                            <HeaderStyle Width="50" Font-Size="15" HorizontalAlign="Center" />
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColKingaku" HeaderText="金額">
                            <HeaderStyle Width="100" Font-Size="15" />
                            <ItemStyle Wrap="false" Font-Size="15" HorizontalAlign="Right" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="ColMitumoriDay" HeaderText="受注日">
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
                    <Scrolling AllowScroll="True" FrozenColumnsCount="0" ScrollHeight="600px" EnableColumnClientFreeze="true" UseStaticHeaders="True" />
                </ClientSettings>
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
