<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReturnInput.aspx.cs" Inherits="Gyomu.Return.ReturnInput" %>
<%@ Register Src="~/Common/CtlNengappiForm.ascx" TagPrefix="uc2" TagName="Date" %>
<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="Return.css" rel="stylesheet" type="text/css" />

    <title>返却処理</title>

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
        <div>
            <div id="MainMenu" runat="server">
                <uc:Menu ID="Menu" runat="server" />
            </div>
            <br />
            <telerik:RadTabStrip ID="RT" runat="server" AutoPostBack="True" SelectedIndex="1" BackColor="#d2eaf6">
                <Tabs>
                    <telerik:RadTab Text="返却一覧" Font-Size="12pt" NavigateUrl="ReturnList.aspx">
                    </telerik:RadTab>
                    <telerik:RadTab Text="返却処理" Font-Size="12pt" NavigateUrl="ReturnInput.aspx" Selected="true">
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
                        <p>返却管理No.</p>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="TbxReturnNo"></asp:TextBox>
                    </td>
                    <td class="SerchTD">
                        <p>カテゴリ</p>
                    </td>
                    <td>
                        <telerik:RadComboBox runat="server" ID="RcbCategory"></telerik:RadComboBox>
                    </td>
                    <td rowspan="3">
                        <asp:Button runat="server" ID="BtnSerch" OnClick="BtnSerch_Click" CssClass="Btn10" Text="検索" />
                    </td>
                </tr>
                <tr>
                    <td class="SerchTD">
                        <p>施設</p>
                    </td>
                    <td>
                        <telerik:RadComboBox runat="server" ID="RcbFacility" OnItemsRequested="RcbFacility_ItemsRequested"></telerik:RadComboBox>
                    </td>
                    <td class="SerchTD">
                        <p>商品名</p>
                    </td>
                    <td>
                        <telerik:RadComboBox runat="server" ID="RcbProduct" OnItemsRequested="RcbProduct_ItemsRequested" AllowCustomText="true"></telerik:RadComboBox>
                    </td>
                    <td class="SerchTD">
                        <p>媒体</p>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="DDLMedia">
                            <asp:ListItem Text="DVD" Value="DVD"></asp:ListItem>
                            <asp:ListItem Text="BD" Value="BD"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="SerchTD">
                        <p>担当者</p>
                    </td>
                    <td>
                        <telerik:RadComboBox runat="server" ID="RcbTanto" OnItemsRequested="RcbTanto_ItemsRequested" AllowCustomText="true"></telerik:RadComboBox>
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
                        <asp:Button runat="server" ID="BtnDelete" Text="削除" OnClick="BtnDelete_Click" CssClass="Btn10" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:Label runat="server" ID="LblErr"></asp:Label>

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

                        <telerik:GridTemplateColumn UniqueName="ColCategori" HeaderText="カテゴリ">
                            <HeaderStyle Width="80" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColTantousya" HeaderText="担当者">
                            <HeaderStyle Width="80" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle Wrap="false" Font-Size="15" HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColFacilityCode" HeaderText="施設コード">
                            <HeaderStyle Width="100" Font-Size="15" HorizontalAlign="Center" />
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColFacilityName" HeaderText="施設名">
                            <HeaderStyle Width="150" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColProductName" HeaderText="商品名">
                            <HeaderStyle Width="180" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColMedia" HeaderText="媒体">
                            <HeaderStyle Font-Size="15" HorizontalAlign="Center" Width="50" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColSuryo" HeaderText="数量">
                            <HeaderStyle Width="50" Font-Size="15" HorizontalAlign="Center" />
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColReturnNo" HeaderText="返却管理No">
                            <HeaderStyle Width="100" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColStatus" HeaderText="返却">
                            <HeaderStyle Width="50" Font-Size="15" HorizontalAlign="Center" />
                            <ItemStyle Wrap="false" Font-Size="15" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <input id="ChkReturn" name="ChkReturn" runat="server" type="checkbox" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColEndDate" HeaderText="返却処理日">
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
