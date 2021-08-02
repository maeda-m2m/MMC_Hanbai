<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterSyohin.aspx.cs" Inherits="Gyomu.Master.MasterSyohin" %>

<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Src="~/Master/CTLSyohin.ascx" TagName="M_Syohin" TagPrefix="uc2" %>
<%@ Register Src="~/Master/CtlSyouhin2.ascx" TagName="M_Syohin2" TagPrefix="uc4" %>
<%@ Register Src="~/Common/CtrlFilter.ascx" TagName="CtlFilter" TagPrefix="uc3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .sl {
            width: 35px;
        }
    </style>
    <link href="../../MainStyle.css" type="text/css" rel="STYLESHEET" />
    <link href="../../Style/Grid.ykuri.css" rel="STYLESHEET" />
    <link href="../../Style/ComboBox.ykuri.css" type="text/css" rel="STYLESHEET" />
    <telerik:RadScriptBlock ID="RSM" runat="server">
        <script type="text/javascript" src="../../Core.js"></script>
        <script src="../../Common/CommonJs.js" type="text/javascript"></script>
        <script type="text/jscript">

            function CntRow(cnt) {
                document.forms[0].count.value = cnt;
                return;
            }
        </script>
    </telerik:RadScriptBlock>
</head>
<body>
    <form id="form1" runat="server">
        <div id="MainMenu" runat="server">
            <uc:Menu ID="Menu" runat="server" />
        </div>
        <telerik:RadTabStrip ID="RT" runat="server" Skin="Office2007" AutoPostBack="True" SelectedIndex="0">
            <Tabs>
                <telerik:RadTab Text="商品マスタ" Font-Size="12pt" NavigateUrl="MasterSyohin.aspx" Selected="True">
                </telerik:RadTab>
                <telerik:RadTab Text="価格マスタ" Font-Size="12pt" NavigateUrl="MasterKakaku.aspx" Selected="True">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>

        <div id="Master" runat="server">
            <br />
            <table border="0" id="Serch" runat="server">
                <tbody>
                    <tr>
                        <td>
                            <table border="1">
                                <tbody>
                                    <tr>
                                        <td>
                                            <uc3:CtlFilter ID="F" runat="server" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                        <td>
                            <asp:Button ID="BtnSerch" runat="server" Text="検索" OnClick="BtnSerch_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <br />
            <asp:Button ID="BtnSinki" runat="server" Text="新規登録" Width="100" OnClick="BtnSinki_Click" />
<%--            <asp:Button ID="BtnIkkatu" runat="server" Text="一括修正" Width="100" OnClick="BtnIkkatu_Click" />--%>
            <asp:Button runat="server" ID="BtnCsvDownload" Text="CSVダウンロード" OnClick="BtnCsvDownload_Click" />

            <asp:Button ID="Button1" runat="server" Text="アップロード(工事中)" OnClick="Button1_Click1" />
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <%--            <asp:Button ID="BtnAllSelect" runat="server" Text="全てを選択" OnClick="BtnAllSelect_Click" />--%>
<%--            <asp:CheckBox ID="CheckBox2" runat="server" Text="全てを選択" AutoPostBack="true" OnCheckedChanged="CheckBox2_CheckedChanged" />--%>
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            <br />
            <div id="L" runat="server">
                <telerik:RadGrid ID="D" runat="server" CssClass="def" PageSize="15" AllowPaging="True" EnableAJAX="True" EnableAJAXLoadingTemplate="True" Skin="ykuri" AllowCustomPaging="True" EnableEmbeddedSkins="False" GridLines="None" CellPadding="0" EnableEmbeddedBaseStylesheet="False" OnItemDataBound="D_ItemDataBound" OnItemCreated="D_ItemCreated" OnPageIndexChanged="D_PageIndexChanged" OnPageSizeChanged="D_PageSizeChanged" OnPreRender="D_PreRender" Width="200" OnItemCommand="D_ItemCommand">
                    <HeaderContextMenu EnableAutoScroll="True">
                    </HeaderContextMenu>
                    <AlternatingItemStyle CssClass="alt"></AlternatingItemStyle>
                    <HeaderStyle CssClass="hd" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False"></HeaderStyle>
                    <MasterTableView CellPadding="2" GridLines="Both" Border="0" CellSpacing="0" AutoGenerateColumns="False">
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


                            <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="SelectButton">
                                <ItemTemplate>
                                    <%--<asp:Button ID="E" runat="server" Text="選択"></asp:Button>--%>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="E" runat="server" Text="詳細修正" OnClick="E_Click" Width="65px"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <EditFormSettings>
                            <EditColumn InsertImageUrl="Update.gif" UpdateImageUrl="Update.gif" EditImageUrl="Edit.gif" CancelImageUrl="Cancel.gif">
                            </EditColumn>
                        </EditFormSettings>
                        <ItemStyle Wrap="False"></ItemStyle>
                        <HeaderStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" CssClass="radgrid_header_def"></HeaderStyle>
                        <AlternatingItemStyle Wrap="False" CssClass="alt"></AlternatingItemStyle>
                        <PagerStyle Position="Top" PagerTextFormat="ページ移動: {4} &amp;nbsp;ページ : &lt;strong&gt;{0:N0}&lt;/strong&gt; / &lt;strong&gt;{1:N0}&lt;/strong&gt; | 件数: &lt;strong&gt;{2:N0}&lt;/strong&gt; - &lt;strong&gt;{3:N0}件&lt;/strong&gt; / &lt;strong&gt;{5:N0}&lt;/strong&gt;件中" PageSizeLabelText="ページサイズ:" FirstPageToolTip="最初のページに移動" LastPageToolTip="最後のページに移動" NextPageToolTip="次のページに移動" PrevPageToolTip="前のページに移動" AlwaysVisible="True" />
                    </MasterTableView>
                    <ClientSettings>
                        <ClientEvents OnGridCreated="Core.ResizeRadGrid" />
                        <Scrolling AllowScroll="false" UseStaticHeaders="True" />
                    </ClientSettings>
                    <FilterMenu EnableEmbeddedSkins="False">
                    </FilterMenu>
                </telerik:RadGrid>
            </div>
        </div>
        <input type="hidden" id="count" runat="server" />
        <div id="Touroku" runat="server">
            <br />
            <asp:Button ID="BtnBack" runat="server" Text="戻る" Width="75" OnClick="BtnBack_Click" />
            <br />
            <br />
            <uc2:M_Syohin ID="Syohin" runat="server" />

        </div>
        <div id="Touroku2" runat="server">
            <uc4:M_Syohin2 ID="Syohin2" runat="server"></uc4:M_Syohin2>
        </div>
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

    </form>
</body>
</html>
