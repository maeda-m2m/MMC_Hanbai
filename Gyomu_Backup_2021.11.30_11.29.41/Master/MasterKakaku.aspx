<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterKakaku.aspx.cs" Inherits="Gyomu.Master.MasterKakaku" %>

<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Src="~/Master/CtlKakaku.ascx" TagName="Kakaku" TagPrefix="uc2" %>
<%@ Register Src="~/Common/CtrlFilter.ascx" TagName="CtlFilter" TagPrefix="uc3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
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

    <script type="text/javascript">
        function ChkAll(bool) {
            var idAry = document.getElementById('HidChkID').value.split(',');
            for (var i = 0; i < idAry.length; i++) {
                var chk = document.getElementById(idAry[i]);
                if (chk.disabled == false)
                    chk.checked = bool;
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="MainMenu" runat="server">
            <uc:Menu ID="Menu" runat="server" />
        </div>
        <telerik:RadTabStrip ID="RT" runat="server" Skin="Office2007" AutoPostBack="True" SelectedIndex="1">
            <Tabs>
                <telerik:RadTab Text="商品マスタ" Font-Size="12pt" NavigateUrl="MasterSyohin.aspx">
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
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            <br />
            <asp:Button ID="BtnSyusei" runat="server" Text="価格修正へ移動" OnClick="BtnSyusei_Click" />
            <asp:Button runat="server" ID="CSVdownload" Text="CSVダウンロード" OnClick="CSVdownload_Click" />
            <asp:FileUpload runat="server" ID="Fu" />
            <asp:Button runat="server" ID="CSVupload" Text="CSVアップロード" OnClick="CSVupload_Click" />
            <asp:Button runat="server" ID="CSVformat" Text="アップロードフォーマットをダウンロード" OnClick="CSVformat_Click" />
            <br />
            <br />
            <div id="L" runat="server">
                <telerik:RadGrid ID="D" runat="server" CssClass="def" PageSize="15" AllowPaging="True" EnableAJAX="True" EnableAJAXLoadingTemplate="True" Skin="ykuri" AllowCustomPaging="True" EnableEmbeddedSkins="False" GridLines="None" CellPadding="0" EnableEmbeddedBaseStylesheet="False" OnItemDataBound="D_ItemDataBound" OnItemCreated="D_ItemCreated" OnPageIndexChanged="D_PageIndexChanged" OnPreRender="D_PreRender" Width="200">
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
                                    <%--<asp:Button ID="E" runat="server" Text="選択"></asp:Button>--%>
                                    <table>
                                        <tr>
                                            <td>
                                                <%--<asp:Button ID="E" runat="server" Text="修正"></asp:Button>--%>
                                                <input id="E" runat="server" type="checkbox" />
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
            <input type="hidden" id="count" runat="server" />
            <input id="HidChkID" runat="server" type="hidden" />
        </div>
        <div id="Touroku" runat="server">
            <br />
            <asp:Button ID="BtnTouroku" runat="server" Text="登録" OnClick="BtnTouroku_Click" />
            <asp:Button ID="BtnBack" runat="server" Text="戻る" OnClick="BtnBack_Click" />
            <br />
            <br />
            <uc2:Kakaku ID="Kakaku" runat="server"></uc2:Kakaku>
        </div>
        <telerik:RadAjaxManager ID="Ram" runat="server" OnAjaxRequest="Ram_AjaxRequest">
            <%--<ClientEvents OnResponseEnd="OnResponseEnd" OnRequestStart="OnRequestStart" /> --%>
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="BtnL">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="TblK" />
                        <telerik:AjaxUpdatedControl ControlID="TblList" LoadingPanelID="LP" UpdatePanelHeight="200px" UpdatePanelRenderMode="Inline" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

    </form>
</body>
</html>
