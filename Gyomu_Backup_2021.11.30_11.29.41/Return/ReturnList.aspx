<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReturnList.aspx.cs" Inherits="Gyomu.Return.ReturnList" %>

<%@ Register Src="~/Common/CtlNengappiForm.ascx" TagPrefix="uc2" TagName="Date" %>
<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="Return.css" rel="stylesheet" type="text/css" />
    <title>返却一覧</title>

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

        .auto-style2 {
            width: 100px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="MainMenu" runat="server">
                <uc:Menu ID="Menu" runat="server" />
            </div>
            <telerik:RadTabStrip ID="RT" runat="server" AutoPostBack="True" SelectedIndex="1" BackColor="#d2eaf6">
                <Tabs>
                    <telerik:RadTab Text="返却一覧" Font-Size="12pt" NavigateUrl="ReturnList.aspx" Selected="true">
                    </telerik:RadTab>
                    <telerik:RadTab Text="返却処理" Font-Size="12pt" NavigateUrl="ReturnInput.aspx">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
        </div>
        <br />
        <div>
            <table>
                <tr>
                    <td class="SerchTD">
                        <p>返却</p>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="DDLStatus">
                            <asp:ListItem Text="未返却" Value="0"></asp:ListItem>
                            <asp:ListItem Text="返却済" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="SerchTD">
                        <p>返却No.</p>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="TbxReturnNo"></asp:TextBox>
                    </td>
                    <td class="SerchTD">
                        <p>得意先名</p>
                    </td>
                    <td>
                        <telerik:RadComboBox runat="server" AllowCustomText="true" ID="RcbTokuisaki" OnItemsRequested="RcbTokuisaki_ItemsRequested" OnSelectedIndexChanged="RcbTokuisaki_SelectedIndexChanged" EnableLoadOnDemand="true"></telerik:RadComboBox>
                    </td>
                    <td rowspan="3">
                        <asp:Button runat="server" ID="BtnSerch" Text="検索" OnClick="BtnSerch_Click" CssClass="Btn10" />
                    </td>
                </tr>
                <tr>
                    <td class="SerchTD">
                        <p>施設</p>
                    </td>
                    <td>
                        <telerik:RadComboBox runat="server" ID="RcbFacility" AllowCustomText="true" OnItemsRequested="RcbFacility_ItemsRequested" OnSelectedIndexChanged="RcbFacility_SelectedIndexChanged" EnableLoadOnDemand="true" AutoPostBack="true"></telerik:RadComboBox>
                    </td>
                    <td class="SerchTD">
                        <p>カテゴリ</p>
                    </td>
                    <td>
                        <telerik:RadComboBox runat="server" ID="RcbCategory"></telerik:RadComboBox>
                    </td>
                    <td class="SerchTD">
                        <p>部門</p>
                    </td>
                    <td>
                        <telerik:RadComboBox runat="server" ID="RcbBumon"></telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="SerchTD">
                        <p>担当者</p>
                    </td>
                    <td>
                        <telerik:RadComboBox runat="server" ID="RcbTanto" AllowCustomText="true" OnItemsRequested="RcbTanto_ItemsRequested" OnSelectedIndexChanged="RcbTanto_SelectedIndexChanged"></telerik:RadComboBox>
                    </td>
                    <td class="SerchTD">
                        <p>入力者</p>
                    </td>
                    <td>
                        <telerik:RadComboBox runat="server" AllowCustomText="true" OnItemsRequested="RcbTanto_ItemsRequested" OnSelectedIndexChanged="RcbTanto_SelectedIndexChanged"></telerik:RadComboBox>
                    </td>
                    <td class="SerchTD">
                        <p>返却日</p>
                    </td>
                    <td>
                        <uc2:Date runat="server" ID="CtlReturnDate" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div>
            <table>
                <tr>
                    <td>
                        <asp:Button runat="server" ID="BtnPrint" Text="印刷" OnClick="BtnPrint_Click" CssClass="Btn10" />
                    </td>
                    <td>
                        <asp:Button runat="server" ID="BtnReturn" Text="返却処理" OnClick="BtnReturn_Click" CssClass="Btn10" Width="130px" />
                    </td>
                    <td>
                        <asp:Button runat="server" ID="BtnDelete" Text="削除" OnClick="BtnDelete_Click" CssClass="Btn10" OnClientClick="A()" />
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
        </div>
        <asp:Label runat="server" ID="LblErr"></asp:Label>
        <br />
        <div>
            <telerik:RadGrid ID="RadG" runat="server" CssClass="def" PageSize="1000" AllowPaging="True" EnableAJAX="True" EnableAJAXLoadingTemplate="True" Skin="ykuri" AllowCustomPaging="True" EnableEmbeddedSkins="False" GridLines="None" CellPadding="0" EnableEmbeddedBaseStylesheet="False" AutoGenerateColumns="False" OnItemDataBound="RadG_ItemDataBound" OnPageIndexChanged="RadG_PageIndexChanged" OnItemCreated="RadG_ItemCreated">
                <PagerStyle Position="Top" AlwaysVisible="true" BackColor="#dfecfe" PagerTextFormat="ページ移動: {4} &amp;nbsp;ページ : &lt;strong&gt;{0:N0}&lt;/strong&gt; / &lt;strong&gt;{1:N0}&lt;/strong&gt; | 件数: &lt;strong&gt;{2:N0}&lt;/strong&gt; - &lt;strong&gt;{3:N0}件&lt;/strong&gt; / &lt;strong&gt;{5:N0}&lt;/strong&gt;件中" PageSizeLabelText="ページサイズ:" FirstPageToolTip="最初のページに移動" LastPageToolTip="最後のページに移動" NextPageToolTip="次のページに移動" PrevPageToolTip="前のページに移動" />
                <HeaderStyle Font-Size="8" HorizontalAlign="Center" BackColor="#0070C0" ForeColor="White" BorderColor="#0070C0" />
                <ItemStyle Wrap="true" VerticalAlign="Middle" Font-Size="8" />
                <HeaderContextMenu EnableEmbeddedBaseStylesheet="False" CssClass="GridContextMenu GridContextMenu_Outlook">
                </HeaderContextMenu>
                <AlternatingItemStyle Font-Size="9" BackColor="#c3e6ff" />
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

                        <telerik:GridTemplateColumn UniqueName="ColReturnNo" HeaderText="返却No">
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
                            <HeaderStyle Width="70" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColTokuisakiName" HeaderText="得意先名">
                            <HeaderStyle Width="215" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColSisetu" HeaderText="施設名">
                            <HeaderStyle Font-Size="15" HorizontalAlign="Center" Width="250" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColSuryo" HeaderText="数量">
                            <HeaderStyle Width="50" Font-Size="15" HorizontalAlign="Center" />
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColKingaku" HeaderText="金額">
                            <HeaderStyle Width="50" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle Wrap="false" Font-Size="15" HorizontalAlign="Right" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColEndDate" HeaderText="使用終了日">
                            <HeaderStyle Width="100" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColStatus" HeaderText="返却">
                            <HeaderStyle Width="80" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle Wrap="false" Font-Size="15" HorizontalAlign="Center" />
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
