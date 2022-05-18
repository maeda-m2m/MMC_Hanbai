<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="MonthClosing.aspx.cs" Inherits="Gyomu.Closing.MonthClosing" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="MonthClosing.css" rel="stylesheet" type="text/css" />
    <link href="../Style/Grid.ykuri.css" rel="STYLESHEET" />

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>月次処理</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc:Menu ID="Menu" runat="server" />
        </div>
        <div>
            <telerik:RadTabStrip ID="RT" runat="server" AutoPostBack="True" SelectedIndex="1" BackColor="#d2eaf6">
                <Tabs>
                    <telerik:RadTab Selected="false" Text="元帳" Font-Size="12pt" NavigateUrl="Ledger.aspx"></telerik:RadTab>
                    <telerik:RadTab Text="月次処理" Font-Size="12pt" NavigateUrl="MonthClosing.aspx" Selected="true">
                    </telerik:RadTab>
                    <telerik:RadTab Text="年次処理" Font-Size="12pt" NavigateUrl="YearClosing.aspx" Selected="false">
                    </telerik:RadTab>
                    <telerik:RadTab Text="消込" Font-Size="12pt" NavigateUrl="Reconciliation.aspx" Selected="false"></telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
        </div>
        <div>
            <br />
            <asp:Label runat="server" ID="LblErr" ForeColor="Red"></asp:Label>
            <table runat="server" id="table1">
                <tr>
                    <td runat="server" class="tabletd">
                        <p>データ選択</p>
                    </td>
                    <td runat="server" style="background-color: white">
                        <telerik:RadComboBox runat="server" ID="RcbFormatSelsect">
                            <Items>
                                <telerik:RadComboBoxItem Text="売掛金入金データ" Value="1" />
                                <telerik:RadComboBoxItem Text="買掛金支払データ" Value="2" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td runat="server" class="tabletd">
                        <p>ファイル選択</p>
                    </td>
                    <td runat="server" style="background-color: white"></td>
                    <td runat="server" class="tabletd">
                        <asp:FileUpload runat="server" ID="FUData" />
                        <asp:Button runat="server" ID="BtnUpload" Text="アップロード" CssClass="Btn" OnClick="BtnUpload_Click" />
                    </td>
                </tr>
            </table>
            <table runat="server" id="table2">
                <tr>
                    <td runat="server" class="tabletd">
                        <p>データ作成日次</p>
                    </td>
                    <td runat="server" style="background-color: white">
                        <telerik:RadDatePicker runat="server" ID="RdpCreateDate"></telerik:RadDatePicker>
                    </td>
                    <td runat="server" class="tabletd">
                        <p>照会番号</p>
                    </td>
                    <td runat="server" style="background-color: white">
                        <asp:TextBox runat="server" ID="TbxSyoukaiNo"></asp:TextBox>
                    </td>
                    <td runat="server" class="tabletd">
                        <p>取引区分</p>
                    </td>
                    <td runat="server" style="background-color: white">
                        <telerik:RadComboBox runat="server" ID="RcbTorihikiKBN">
                            <Items>
                                <telerik:RadComboBoxItem Text="振込" Value="振込" />
                                <telerik:RadComboBoxItem Text="支払" Value="支払" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td runat="server" class="tabletd">
                        <p>入金日</p>
                    </td>
                    <td runat="server" style="background-color: white">
                        <p style="font-size: 10px; margin: 0; padding: 0">(例)20210401</p>
                        <asp:TextBox runat="server" ID="TbxNyukinDate"></asp:TextBox>
                    </td>
                    <td runat="server" class="tabletd">
                        <p>口座番号</p>
                    </td>
                    <td runat="server" style="background-color: white">
                        <asp:TextBox runat="server" ID="TbxKouzaNo"></asp:TextBox>
                    </td>
                    <td runat="server" class="tabletd">
                        <p>振込依頼人名</p>
                    </td>
                    <td runat="server" style="background-color: white">
                        <asp:TextBox runat="server" ID="TbxFurikomiIrai"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td runat="server" class="tabletd">
                        <p>得意先名</p>
                    </td>
                    <td runat="server" style="background-color: white">
                        <asp:TextBox runat="server" ID="TbxTokuisakiName"></asp:TextBox>
                    </td>
                    <td runat="server" class="tabletd">
                        <p>得意先コード</p>
                    </td>
                    <td runat="server" style="background-color: white">
                        <asp:TextBox runat="server" ID="TbxTokuisakiCode"></asp:TextBox>
                    </td>
                    <td runat="server" class="tabletd">
                        <p>振込銀行</p>
                    </td>
                    <td runat="server" style="background-color: white">
                        <asp:TextBox runat="server" ID="TbxFurikomiBank"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td runat="server" class="tabletd">
                        <p>振込支店</p>
                    </td>
                    <td runat="server" style="background-color: white">
                        <asp:TextBox runat="server" ID="TbxFurikomiShiten"></asp:TextBox>
                    </td>
                    <td runat="server" class="tabletd">
                        <p>入金</p>
                    </td>
                    <td runat="server" style="background-color: white">
                        <asp:TextBox runat="server" ID="TbxNyukinKingaku"></asp:TextBox>
                    </td>
                    <td colspan="2" runat="server" style="background-color: white">
                        <asp:Button runat="server" ID="BtnSerch" OnClick="BtnSerch_Click" Text="検索" CssClass="Btn" />
                    </td>
                </tr>
            </table>
            <br />
            <telerik:RadGrid ID="RadG" runat="server" CssClass="def" PageSize="1000" AllowPaging="True" EnableAJAX="True" EnableAJAXLoadingTemplate="True" Skin="ykuri" AllowCustomPaging="True" EnableEmbeddedSkins="False" GridLines="None" CellPadding="0" EnableEmbeddedBaseStylesheet="False" AutoGenerateColumns="False" OnItemDataBound="RadG_ItemDataBound" OnPageIndexChanged="RadG_PageIndexChanged" OnItemCreated="RadG_ItemCreated" Width="1800px">
                <PagerStyle Position="Top" AlwaysVisible="true" BackColor="#dfecfe" PagerTextFormat="ページ移動: {4} &amp;nbsp;ページ : &lt;strong&gt;{0:N0}&lt;/strong&gt; / &lt;strong&gt;{1:N0}&lt;/strong&gt; | 件数: &lt;strong&gt;{2:N0}&lt;/strong&gt; - &lt;strong&gt;{3:N0}件&lt;/strong&gt; / &lt;strong&gt;{5:N0}&lt;/strong&gt;件中" PageSizeLabelText="ページサイズ:" FirstPageToolTip="最初のページに移動" LastPageToolTip="最後のページに移動" NextPageToolTip="次のページに移動" PrevPageToolTip="前のページに移動" />
                <HeaderStyle Font-Size="8" HorizontalAlign="Center" BackColor="#0070C0" ForeColor="white" BorderColor="#0070C0" />
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

                        <telerik:GridTemplateColumn UniqueName="ColCreateDate" HeaderText="データ作成日次">
                            <HeaderStyle Width="150" Font-Size="12" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="CreateDate"></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColSyoukai" HeaderText="照会番号">
                            <HeaderStyle Width="80" Font-Size="12" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                            <ItemTemplate>

                                <asp:Label runat="server" ID="Syoukai"></asp:Label>
                            </ItemTemplate>

                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColTorihikiKBN" HeaderText="取引区分">
                            <HeaderStyle Width="60" Font-Size="12" HorizontalAlign="Center" />
                            <ItemStyle Wrap="false" Font-Size="15" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="TorihikiKBN"></asp:Label>
                            </ItemTemplate>

                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColNyukinDate" HeaderText="入金日">
                            <HeaderStyle Width="80" Font-Size="12" HorizontalAlign="Center" />
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="NyukinDate"></asp:Label>
                            </ItemTemplate>

                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColKouzaNo" HeaderText="口座番号">
                            <HeaderStyle Width="100" Font-Size="12" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="KouzaNo"></asp:Label>
                            </ItemTemplate>

                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColFurikkomiIrai" HeaderText="振込依頼人名">
                            <HeaderStyle Width="200" Font-Size="12" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="FurikkomiIrai"></asp:Label>
                            </ItemTemplate>

                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColTokuisakiName" HeaderText="得意先名">
                            <HeaderStyle Font-Size="12" HorizontalAlign="Center" Width="200" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="TokuisakiName"></asp:Label>
                            </ItemTemplate>

                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColTokuisakiCode" HeaderText="得意先コード">
                            <HeaderStyle Width="80" Font-Size="12" HorizontalAlign="Center" />
                            <HeaderStyle />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" Font-Size="15" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="TokuisakiCode"></asp:Label>
                            </ItemTemplate>

                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColBank" HeaderText="振込銀行">
                            <HeaderStyle Width="80" Font-Size="12" HorizontalAlign="Center" />
                            <ItemStyle Wrap="false" Font-Size="15" HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="Bank"></asp:Label>
                            </ItemTemplate>

                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColFurikomiShiten" HeaderText="振込支店">
                            <HeaderStyle Width="80" Font-Size="12" HorizontalAlign="Center" />
                            <ItemStyle Wrap="false" Font-Size="15" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="FurikomiShiten"></asp:Label>
                            </ItemTemplate>

                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColNyukingaku" HeaderText="入金金額">
                            <HeaderStyle Width="80" Font-Size="12" HorizontalAlign="Center" />
                            <ItemStyle Wrap="false" Font-Size="15" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="Nyukingaku"></asp:Label>
                            </ItemTemplate>

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

        </div>

    </form>
</body>
</html>
