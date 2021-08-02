<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NyukinRireki.aspx.cs" Inherits="Gyomu.Uriage.NyukinRireki" %>
<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register TagPrefix="uc2" TagName="CtlNengappiFromTo" Src="~/Common/CtlNengappiForm.ascx" %>
<%@ Register TagPrefix="uc3" TagName="CtlPager" Src="~/Common/CtlPager.ascx" %>

<%@ Register TagPrefix="cc1" Assembly="Core" Namespace="Core.Web" %>
<%@ Register TagPrefix="telerik" Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>入金履歴</title>
    <link href="../../Style/Grid.ykuri.css" rel="STYLESHEET" />
    <link href="../../Style/ComboBox.ykuri.css" type="text/css" rel="STYLESHEET" />
    <link href="../../MainStyle.css" type="text/css" rel="Stylesheet" />
    <style type="text/css">
        body
        {
        	font-family:Tahoma;
        	font-size:9pt;
        }
        .sl
        {
        	border-bottom:solid 1px silver;
        }
    </style>
</head>

<script type="text/javascript" src="../../Core.js"></script>

<script type="text/javascript">
    function Syusei(DenBan) {
        location.href = "NyukinDenpyo.aspx?DenBan=" + DenBan;
    }
    window.onload = function() { Core.ResizeRadGrid($find('<%= RadG.ClientID%>')); }
    window.onresize = function() { Core.ResizeRadGrid($find('<%= RadG.ClientID%>')); }
</script>

<body>
    <form id="form1" runat="server">
        <div id="MainMenu" runat="server">
            <uc:menu ID="Menu" runat="server" />
        </div>
        <br />
        <div id="Nyuryoku" runat="server">
            <telerik:RadTabStrip ID="RT" runat="server" Skin="Office2007" AutoPostBack="True" SelectedIndex="2">
        <Tabs>
            <telerik:RadTab Text="売上管理" Font-Size="12pt" NavigateUrl="~/Uriage/UriageJoho.aspx">
            </telerik:RadTab>
            <telerik:RadTab Text="入金伝票" Font-Size="12pt" NavigateUrl="~/Uriage/NyukinDenpyo.aspx">
            </telerik:RadTab>
             <telerik:RadTab Text="入金履歴" Font-Size="12pt" NavigateUrl="~/Uriage/NyukinRireki.aspx" Selected="True">
            </telerik:RadTab>
        </Tabs>
            </telerik:RadTabStrip>
        </div>
         <table id="TblSearch" runat="server">
        <tr>
            <td class="column">
                伝票番号
            </td>
            <td class="row">
                <asp:TextBox ID="TbxDenBan" runat="server" MaxLength="20" Width="80"></asp:TextBox>
            </td>
            <td class="column">
                得意先
            </td>
            <td class="row">
                <telerik:RadComboBox ID="RcbTokuisakiMei" runat="server" AllowCustomText="true" OnItemsRequested="RcbTokuisakiMei_ItemsRequested" EnableLoadOnDemand="True" Height="180px" DropDownWidth="350px" Skin="Web20">
                </telerik:RadComboBox>
            </td>
            <td class="column">
                <asp:Label ID="Label1" runat="server" Text="入金日"></asp:Label>
            </td>
            <td class="row">
                <uc2:CtlNengappiFromTo ID="CtlJucyuBi" runat="server" />
            </td>
            <td class="column">
                担当者
            </td>
            <td class="row">
                <telerik:RadComboBox ID="RcbUserName" runat="server" AllowCustomText="true" EnableLoadOnDemand="True" Height="180px" Skin="Web20">
                </telerik:RadComboBox>
            </td>
            <td>
                <asp:Button ID="BtnSearch" runat="server" Width="120" CssClass="buttonY" Text="検索" OnClick="BtnSearch_Click"/>
            </td>
        </tr>
    </table>
        <br />
        <telerik:RadGrid ID="RadG" runat="server" CssClass="def" PageSize="15" AllowPaging="True" EnableAJAX="True" EnableAJAXLoadingTemplate="True" Skin="ykuri" AllowCustomPaging="True" EnableEmbeddedSkins="False" GridLines="None" CellPadding="0" EnableEmbeddedBaseStylesheet="False" AutoGenerateColumns="False" Width="200" OnItemDataBound="RadG_ItemDataBound" OnItemCreated="RadG_ItemCreated">
    <PagerStyle Position="Top" AlwaysVisible="true" BackColor="#dfecfe" PagerTextFormat="ページ移動: {4} &amp;nbsp;ページ : &lt;strong&gt;{0:N0}&lt;/strong&gt; / &lt;strong&gt;{1:N0}&lt;/strong&gt; | 件数: &lt;strong&gt;{2:N0}&lt;/strong&gt; - &lt;strong&gt;{3:N0}件&lt;/strong&gt; / &lt;strong&gt;{5:N0}&lt;/strong&gt;件中" PageSizeLabelText="ページサイズ:" FirstPageToolTip="最初のページに移動" LastPageToolTip="最後のページに移動" NextPageToolTip="次のページに移動" PrevPageToolTip="前のページに移動" />
        <HeaderStyle Font-Size="10" HorizontalAlign="Center" CssClass="hd yt st" />
        <ItemStyle Wrap="true" VerticalAlign="Middle" Font-Size="10" />
        <HeaderContextMenu EnableEmbeddedBaseStylesheet="False" CssClass="GridContextMenu GridContextMenu_Outlook">
        </HeaderContextMenu>
        <AlternatingItemStyle Font-Size="10" BackColor="#ADD8E6" />        
                <MasterTableView CellPadding="2" GridLines="Both" BorderWidth="1" BorderColor="#000000" CellSpacing="0" AutoGenerateColumns="False" AllowMultiColumnSorting="false" AllowNaturalSort="false">
            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
            </RowIndicatorColumn>
            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
            </ExpandCollapseColumn>
            <Columns>
                <telerik:GridTemplateColumn UniqueName="Syusei">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <input id="SyuseiBtn" runat="server" type="button" value="修正" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ColNo" ItemStyle-HorizontalAlign="Left" HeaderText="伝番">
                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="NyuukinBi" HeaderText="入金日">
                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ColTokuisakiMei" HeaderText="得意先コード<br/>得意先名">
                    <ItemStyle CssClass="st" Wrap="false" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ColNyukinGaku" HeaderText="入金額">
                    <ItemStyle HorizontalAlign="Right" Wrap="false"/>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ColGenkin" HeaderText="現金">
                    <ItemStyle HorizontalAlign="Right" Wrap="false" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ColTegata" HeaderText="手形">
                    <ItemStyle HorizontalAlign="Right" Wrap="false" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ColTegataKijitu" HeaderText="手形期日<br/>振込人">
                    <ItemStyle HorizontalAlign="Right" Wrap="false" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ColHurikomi" HeaderText="振込">
                    <ItemStyle HorizontalAlign="Right" Wrap="false" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ColKogitte" HeaderText="小切手">
                    <ItemStyle HorizontalAlign="Right" Wrap="false" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ColKogitteKijitu" HeaderText="小切手期日">
                    <ItemStyle HorizontalAlign="Right" Wrap="false" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ColCyousei" HeaderText="調整">
                    <ItemStyle HorizontalAlign="Right" Wrap="false" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ColSousai" HeaderText="相殺">
                    <ItemStyle HorizontalAlign="Right" Wrap="false" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ColTekiyou" HeaderText="摘要">
                    <ItemStyle HorizontalAlign="Left" CssClass="st" Wrap="false" />
                </telerik:GridTemplateColumn>
            </Columns>
            <EditFormSettings>
                <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu EnableImageSprites="False" EnableEmbeddedBaseStylesheet="False">
        </FilterMenu>
        <ClientSettings>
            <ClientEvents OnGridCreated="Core.ResizeRadGrid" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
    </telerik:RadGrid>
    </form>
</body>
</html>
