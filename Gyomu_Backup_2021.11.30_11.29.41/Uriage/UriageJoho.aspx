<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UriageJoho.aspx.cs" Inherits="Gyomu.Uriage.UriageJoho" %>
<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../../Style/Grid.ykuri.css" rel="STYLESHEET" />
    <link href="../../Style/ComboBox.ykuri.css" type="text/css" rel="STYLESHEET" />
    <link href="../../MainStyle.css" type="text/css" rel="Stylesheet" />
    <link href="../sheet/MainStyles.css" rel="stylesheet" type="text/css" />

<style type="text/css">
        .column{
            background-color:#00008B;
            color:#FFFFFF;
            padding: 0.5em;
            font-size:10px;
        }
        .row{

        }
    </style>

      <script type="text/javascript">

                    function CntRow(cnt) {
                        document.forms[0].count.value = cnt;
                return;
        }
</script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="MainMenu" runat="server">
            <uc:menu ID="Menu" runat="server" />
        </div>
        <br />
        <div id="Kensaku" runat="server">
            <telerik:RadTabStrip ID="RT" runat="server" Skin="Office2007" AutoPostBack="True" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab Text="売上管理" Font-Size="12pt" NavigateUrl="~/Uriage/UriageJoho.aspx" Selected="True">
            </telerik:RadTab>
            <telerik:RadTab Text="入金伝票" Font-Size="12pt" NavigateUrl="~/Uriage/NyukinDenpyo.aspx">
            </telerik:RadTab>
             <telerik:RadTab Text="入金履歴" Font-Size="12pt" NavigateUrl="~/Uriage/NyukinRireki.aspx">
            </telerik:RadTab>
        </Tabs>
            </telerik:RadTabStrip>
            <br />
            <table border="1">
	<tbody>
		<tr>
			<td class="column"><asp:Literal ID="Literal1" runat="server">伝票番号</asp:Literal></td>
			<td class="row">
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </td>
			<td class="column"><asp:Literal ID="Literal2" runat="server">受注番号</asp:Literal></td>
			<td class="row">
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            </td>
			<td class="column"><asp:Literal ID="Literal3" runat="server">得意先<br />コード/名</asp:Literal></td>
			<td class="row">
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            </td>
			<td class="column"><asp:Literal ID="Literal4" runat="server">請求先</asp:Literal></td>
			<td class="row">
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            </td>
            <td class="column"><asp:Literal ID="Literal14" runat="server">直送先</asp:Literal></td>
            <td>
                <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
            </td>
			<td rowspan="3">
                <asp:Button ID="BtnKensaku" runat="server" Text="検索" Width="100px" style="height: 21px" />
            </td>
		</tr>
		<tr>
			<td class="column"><asp:Literal ID="Literal5" runat="server">施設</asp:Literal></td>
			<td class="row">
                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
            </td>
			<td class="column"><asp:Literal ID="Literal6" runat="server">カテゴリ</asp:Literal></td>
			<td class="row">
                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
            </td>
			<td class="column"><asp:Literal ID="Literal7" runat="server">部門</asp:Literal></td>
			<td class="row">
                <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
            </td>
			<td class="column"><asp:Literal ID="Literal8" runat="server">担当者</asp:Literal></td>
			<td class="row">
                <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
            </td>
            <td class="column">
                <asp:Literal ID="Literal15" runat="server">受注日</asp:Literal>
            </td>
            <td>
                <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox>
            </td>
		</tr>
		<tr>
			<td class="column"><asp:Literal ID="Literal9" runat="server">締日</asp:Literal></td>
			<td class="row">
                <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
            </td>
			<td class="column"><asp:Literal ID="Literal10" runat="server">売上合計金額</asp:Literal></td>
			<td class="row">
                <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
            </td>
			<td class="column"><asp:Literal ID="Literal11" runat="server">計上日</asp:Literal></td>
			<td class="row">
                <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
            </td>
<%--              <td class="column">
                <asp:Literal ID="Literal12" runat="server">入金</asp:Literal>
            </td>
            <td>
                <asp:DropDownList ID="DrpNyukin" runat="server" Width="100">
                    <asp:ListItem Value="0">未入金</asp:ListItem>
                    <asp:ListItem Value="1">入金済</asp:ListItem>
                </asp:DropDownList>
            </td>--%>
		</tr>
	</tbody>
</table>
        </div>
        <br />
        <asp:Button ID="BtnDownlod" runat="server" Text="CSVダウンロード" Width="140px" style="height: 21px" OnClick="BtnDownlod_Click" />
        <%--<asp:Button ID="BtnBitumori" runat="server" Text="見積書" Width="100px" style="height: 21px" />
        <asp:Button ID="BtnNouhin" runat="server" Text="納品書" Width="100px" style="height: 21px" />--%>
        <br />
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        <div id="Itiran" runat="server">
            <telerik:RadGrid ID="RadG" runat="server" CssClass="def" PageSize="10" AllowPaging="True" EnableAJAX="True" EnableAJAXLoadingTemplate="True" Skin="ykuri" AllowCustomPaging="True" EnableEmbeddedSkins="False" GridLines="None" CellPadding="0" EnableEmbeddedBaseStylesheet="False" AutoGenerateColumns="False" OnItemDataBound="RadG_ItemDataBound" OnPageIndexChanged="RadG_PageIndexChanged" OnItemCreated="RadG_ItemCreated">
                 <PagerStyle Position="Top" AlwaysVisible="true" BackColor="#dfecfe" PagerTextFormat="ページ移動: {4} &amp;nbsp;ページ : &lt;strong&gt;{0:N0}&lt;/strong&gt; / &lt;strong&gt;{1:N0}&lt;/strong&gt; | 件数: &lt;strong&gt;{2:N0}&lt;/strong&gt; - &lt;strong&gt;{3:N0}件&lt;/strong&gt; / &lt;strong&gt;{5:N0}&lt;/strong&gt;件中" PageSizeLabelText="ページサイズ:" FirstPageToolTip="最初のページに移動" LastPageToolTip="最後のページに移動" NextPageToolTip="次のページに移動" PrevPageToolTip="前のページに移動" />
        <HeaderStyle Font-Size="8" HorizontalAlign="Center" CssClass="hd yt st"/>
        <ItemStyle Wrap="true" VerticalAlign="Middle" Font-Size="9" />
        <HeaderContextMenu EnableEmbeddedBaseStylesheet="False" CssClass="GridContextMenu GridContextMenu_Outlook">
        </HeaderContextMenu>
        <AlternatingItemStyle Font-Size="9" CssClass="alterRow"/>
        <MasterTableView CellPadding="2" GridLines="Both" BorderWidth="1" BorderColor="#000000" CellSpacing="0" AutoGenerateColumns="False" AllowMultiColumnSorting="false" AllowNaturalSort="false">
            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
            </RowIndicatorColumn>
            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
            </ExpandCollapseColumn>
            <Columns>
                <telerik:GridTemplateColumn UniqueName="ColSyusei">
                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                    <ItemTemplate>
                        <asp:Button ID="BtnSyusei" runat="server" Text="修正" Style="font-size: smaller" OnClick="BtnSyusei_Click"/>
                    </ItemTemplate>
                    <HeaderStyle Width="50" />
                </telerik:GridTemplateColumn>
                
                <telerik:GridTemplateColumn UniqueName="ColNo" HeaderText="売上No">
                    <ItemStyle HorizontalAlign="Center" Wrap="false"></ItemStyle>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn UniqueName="ColTokuisakiName" HeaderText="得意先">
                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                </telerik:GridTemplateColumn>
                
                <telerik:GridTemplateColumn UniqueName="ColSisetu" HeaderText="施設">
                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                </telerik:GridTemplateColumn>
                
                
                <telerik:GridTemplateColumn UniqueName="ColTanto" HeaderText="担当者">
                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                    <HeaderStyle />
                </telerik:GridTemplateColumn>
                
                <telerik:GridTemplateColumn UniqueName="ColNyuryoku" HeaderText="入力者">
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                </telerik:GridTemplateColumn>
                
                <telerik:GridTemplateColumn UniqueName="ColHinmei" HeaderText="品名">
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                </telerik:GridTemplateColumn>
                
                <telerik:GridTemplateColumn UniqueName="ColJutyu" HeaderText="計上日">
                    <HeaderStyle />
                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
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
        </ClientSettings>
    </telerik:RadGrid>
            <input type="hidden" id="count" runat="server" />
        </div>
         <telerik:RadAjaxManager id="Ram" runat="server" onajaxrequest="Ram_AjaxRequest" >
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