<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoukiTokuisaki.aspx.cs" Inherits="Gyomu.Master.DoukiTokuisaki" %>
<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Src="~/Master/CtlTokuisaki.ascx" TagName="M_Tokuisaki" TagPrefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <LINK href="../../MainStyle.css" type="text/css" rel="STYLESHEET"/>
	<LINK href="../../Style/Grid.ykuri.css" rel="STYLESHEET"/>
    <LINK href="../../Style/ComboBox.ykuri.css" type="text/css" rel="STYLESHEET"/>
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
        <br />
        <telerik:RadTabStrip ID="RT" runat="server" Skin="Office2007" AutoPostBack="True" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab Text="得意先マスタ" Font-Size="12pt" NavigateUrl="MsterTokuisaki.aspx" Selected="True">
            </telerik:RadTab>
        </Tabs>
            </telerik:RadTabStrip>
                 <div id="Master" runat="server">
            <br />
            <asp:Button ID="BtnSinki" runat="server" Text="新規登録" OnClick="BtnSinki_Click" Width="100" />
            <br />
            <br />
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            <br />
            <div id="L" runat="server">
           <telerik:RadGrid ID="D" runat="server" CssClass="def" PageSize="50" AllowPaging="True" EnableAJAX="True" EnableAJAXLoadingTemplate="True" Skin="ykuri" AllowCustomPaging="True" EnableEmbeddedSkins="False" GridLines="None" CellPadding="0" EnableEmbeddedBaseStylesheet="False" OnItemDataBound="D_ItemDataBound" Width="1700" OnItemCreated="D_ItemCreated">
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
                    <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="SelectButton">
                        <ItemTemplate>
                            <table >
                                <tr>
                                    <td>
                                        <asp:Button ID="E" runat="server" Text="修正" OnClick="E_Click"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <HeaderStyle Width ="100" Font-Size="8" HorizontalAlign="Center"/>
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
                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
            </ClientSettings>
            <FilterMenu EnableEmbeddedSkins="False">
            </FilterMenu>
        </telerik:RadGrid>
                </div>
            </div>
          <input type="hidden" id="count" runat="server" />
    </form>
</body>
</html>
