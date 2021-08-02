<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterTanto.aspx.cs" Inherits="Gyomu.Master.MasterTanto" %>

<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Src="~/Master/CtlTanto.ascx" TagName="M_Tanto" TagPrefix="uc2" %>
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
        <br />
        <telerik:RadTabStrip ID="RT" runat="server" Skin="Office2007" AutoPostBack="True" SelectedIndex="0">
            <Tabs>
                <telerik:RadTab Text="担当マスタ" Font-Size="12pt" NavigateUrl="MasterTanto.aspx" Selected="True">
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
            <asp:Button ID="BtnSinki" runat="server" Text="新規登録" OnClick="BtnSinki_Click" Width="100" />
            <asp:Button ID="BtnHihyouji" runat="server" Text="非表示リスト" OnClick="BtnHihyouji_Click" />
            <br />
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            <br />
            <div id="L" runat="server">
                <telerik:RadGrid ID="D" runat="server" CssClass="def" PageSize="15" AllowPaging="True" EnableAJAX="True" EnableAJAXLoadingTemplate="True" Skin="ykuri" AllowCustomPaging="True" EnableEmbeddedSkins="False" GridLines="None" CellPadding="0" EnableEmbeddedBaseStylesheet="False" OnItemDataBound="D_ItemDataBound" OnItemCreated="D_ItemCreated" Width="200" OnPageIndexChanged="D_PageIndexChanged" OnItemCommand="D_ItemCommand">
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
                                                <asp:Button ID="E" runat="server" Text="修正" OnClick="E_Click" Width="55px"></asp:Button>
                                            </td>
                                            <td>
                                                <asp:Button ID="Del" runat="server" Text="削除" OnClick="Del_Click" Width="55px" OnClientClick="A()" />
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
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="ColID" HeaderText="UserID">
                                <HeaderStyle Width="50" Font-Size="12" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" Font-Size="12"></ItemStyle>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn UniqueName="ColYomi" HeaderText="読み方">
                                <HeaderStyle Width="50" Font-Size="12" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" Font-Size="12"></ItemStyle>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn UniqueName="ColName" HeaderText="名前">
                                <HeaderStyle Width="50" Font-Size="12" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" Font-Size="12"></ItemStyle>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn UniqueName="ColBusyo" HeaderText="部署">
                                <HeaderStyle Width="50" Font-Size="12" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" Font-Size="12"></ItemStyle>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn UniqueName="ColPass" HeaderText="パスワード">
                                <HeaderStyle Width="50" Font-Size="12" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" Font-Size="12"></ItemStyle>
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
            <asp:Button ID="BtnToroku" runat="server" Text="登録" OnClick="BtnToroku_Click" Width="75" />
            <asp:Button ID="BtnBack" runat="server" Text="戻る" OnClick="BtnBack_Click" Width="75" />
            <br />
            <br />
            <uc2:M_Tanto ID="Tanto" runat="server" />
        </div>
    </form>
</body>
</html>
