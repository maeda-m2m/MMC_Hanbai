<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderedList.aspx.cs" Inherits="Gyomu.Order.OrderedList" %>

<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register TagPrefix="uc2" TagName="CtlNengappiFromTo" Src="~/Common/CtlNengappiForm.ascx" %>
<%@ Register TagPrefix="uc3" TagName="CtlPager" Src="~/Common/CtlPager.ascx" %>

<%@ Register TagPrefix="cc1" Assembly="Core" Namespace="Core.Web" %>
<%@ Register TagPrefix="telerik" Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="Head1">
    <title>受注情報</title>
    <link href="../../Style/Grid.ykuri.css" rel="STYLESHEET" />
    <link href="../../Style/ComboBox.ykuri.css" type="text/css" rel="STYLESHEET" />
    <link href="../../MainStyle.css" type="text/css" rel="Stylesheet" />
    <link href="../sheet/MainStyles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body {
            font-family: Tahoma;
            font-size: 9pt;
        }

        .sl {
            border-bottom: solid 1px silver;
        }

        .g {
            BACKGROUND-COLOR: gray
        }

        .column {
            background-color: #00008B;
            color: #FFFFFF;
            padding: 0.5em;
            font-size: 10px;
        }

        .row {
        }

        .gridh {
            height: 550px;
            width: 95%
        }

        .Btn2 {
            margin-left: 5px;
            text-align: center;
            width: 95px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: #00aaff;
            border: solid 2px #00aaff;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            border-radius: 3px;
            background-color: white;
        }

            .Btn2:hover {
                background: #00aaff;
                color: white;
            }

        .Btn3 {
            font-size: 17px;
            margin-left: 5px;
            text-align: center;
            width: 95px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: white;
            border: solid 2px #ea0088;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            border-radius: 3px;
            background-color: #ea0088;
        }

            .Btn3:hover {
                background: white;
                color: #ea0088;
            }


        .Btn4 {
            font-size: 17px;
            margin-left: 5px;
            text-align: center;
            width: 95px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: white;
            border: solid 2px #7900ea;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            border-radius: 3px;
            background-color: #7900ea;
        }

            .Btn4:hover {
                background: white;
                color: #7900ea;
            }

        .Btn5 {
            font-size: 17px;
            margin-left: 5px;
            text-align: center;
            width: 95px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: white;
            border: solid 2px #00afea;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            border-radius: 3px;
            background-color: #00afea;
        }

            .Btn5:hover {
                background: white;
                color: #00afea;
            }

        .Btn6 {
            font-size: 17px;
            margin-left: 5px;
            text-align: center;
            width: 95px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: white;
            border: solid 2px #00d407;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            border-radius: 3px;
            background-color: #00d407;
        }

            .Btn6:hover {
                background: white;
                color: #00d407;
            }

        .Btn7 {
            font-size: 17px;
            margin-left: 5px;
            text-align: center;
            width: 95px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: white;
            border: solid 2px #eb6600;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            border-radius: 3px;
            background-color: #eb6600;
        }

            .Btn7:hover {
                background: white;
                color: #eb6600;
            }

        kMenu {
            background-color: #c1c2ff;
            width: 100px;
            text-align: center;
        }

        .Btn {
            text-align: center;
            width: 100px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: #2e2e2e;
            border: solid 2px #989ae1;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            background-color: #d3d4ff;
        }

            .Btn:hover {
                background: #989ae1;
                color: #2e2e2e;
            }
    </style>

    <script type="text/javascript" src="../../Core.js"></script>

    <script type="text/javascript">

        function CntRow(cnt) {
            document.forms[0].count.value = cnt;
            return;
        }

        function JtuRow(cnt) {

            document.forms[0].count.value = cnt;
            return;
        }

    </script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

</head>
<body>
    <form id="form2" runat="server">
        <div id="MainMenu" runat="server">
            <uc:Menu ID="Menu" runat="server" />
        </div>
        <br />
        <div id="Kensaku" runat="server">
            <telerik:RadTabStrip ID="RT" runat="server" AutoPostBack="True" SelectedIndex="1" BorderColor="#c1c2ff" BackColor="#c1c2ff" ForeColor="#c1c2ff">
                <Tabs>
                    <telerik:RadTab Text="発注一覧" Font-Size="12pt" NavigateUrl="OrderedList.aspx" Selected="true">
                    </telerik:RadTab>
                    <telerik:RadTab Text="発注入力" Font-Size="12pt" NavigateUrl="NewOrderedInput.aspx">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>

            <br />
            <table style="background-color: #e5e5e5">
                <tr>
                    <td class="kMenu" style="background-color: #c1c2ff; text-align: center; font-family: Meiryo; color: #5d5d5d;">
                        <p>発注書</p>
                    </td>
                    <td>
                        <asp:DropDownList ID="DrpFlg" runat="server" Width="120">
                            <asp:ListItem Selected="True" Value="False">発注書未作成</asp:ListItem>
                            <asp:ListItem Value="True">発注書作成済</asp:ListItem>
                        </asp:DropDownList>

                    </td>
                    <td class="kMenu" runat="server" style="background-color: #c1c2ff; text-align: center; font-family: Meiryo; color: #5d5d5d;">
                        <p>カテゴリー</p>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="RadCate" runat="server" Width="120" AllowCustomText="True" EnableLoadOnDemand="True" MarkFirstMatch="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" OnItemsRequested="RadCate_ItemsRequested"></telerik:RadComboBox>
                    </td>
                    <td class="kMenu" style="background-color: #c1c2ff; text-align: center; font-family: Meiryo; color: #5d5d5d;">
                        <p>仕入先名</p>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="RadShiire" runat="server" Width="120" AllowCustomText="True" EnableLoadOnDemand="True" MarkFirstMatch="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" OnItemsRequested="RadShiire_ItemsRequested"></telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:Button runat="server" ID="BtnSerch" Text="検索" OnClick="BtnSerch_Click" CssClass="Btn" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        <br />
        <asp:Button ID="BtnDownlod" runat="server" Text="CSVダウンロード" Width="180px" CssClass="Btn" OnClick="BtnDownlod_Click" />

        <asp:Button ID="BtnOrder" runat="server" Text="発注書" Width="100px" CssClass="Btn" OnClick="BtnOrdered_Click" />
        <asp:Button ID="BtnEdit" runat="server" Text="修正" CssClass="Btn" Width="100px" OnClick="BtnEdit_Click" />
        <asp:Button ID="BtnOrdered" runat="server" Text="仕入" CssClass="Btn" Width="100px" />
        <asp:Button ID="BtnDel" runat="server" Text="削除" CssClass="Btn" Width="100px" OnClick="BtnDel_Click" OnClientClick="A()" />
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
        <br />
        <br />
        <div id="Itiran" runat="server">

            <telerik:RadGrid ID="RadG" runat="server" CssClass="def" PageSize="1000" AllowPaging="True" EnableAJAX="True" EnableAJAXLoadingTemplate="True" Skin="ykuri" AllowCustomPaging="True" EnableEmbeddedSkins="False" GridLines="None" CellPadding="0" EnableEmbeddedBaseStylesheet="False" AutoGenerateColumns="False" OnItemDataBound="RadG_ItemDataBound" OnPageIndexChanged="RadG_PageIndexChanged" OnItemCreated="RadG_ItemCreated">
                <PagerStyle Position="Top" AlwaysVisible="true" BackColor="#888ac4" PagerTextFormat="ページ移動: {4} &amp;nbsp;ページ : &lt;strong&gt;{0:N0}&lt;/strong&gt; / &lt;strong&gt;{1:N0}&lt;/strong&gt; | 件数: &lt;strong&gt;{2:N0}&lt;/strong&gt; - &lt;strong&gt;{3:N0}件&lt;/strong&gt; / &lt;strong&gt;{5:N0}&lt;/strong&gt;件中" PageSizeLabelText="ページサイズ:" FirstPageToolTip="最初のページに移動" LastPageToolTip="最後のページに移動" NextPageToolTip="次のページに移動" PrevPageToolTip="前のページに移動" />
                <HeaderStyle Font-Size="8" HorizontalAlign="Center" CssClass="hd yt st" />
                <ItemStyle Wrap="true" VerticalAlign="Middle" Font-Size="8" />
                <HeaderContextMenu EnableEmbeddedBaseStylesheet="False">
                </HeaderContextMenu>
                <AlternatingItemStyle Font-Size="9" BackColor="#d1d2fc" ForeColor="#5d5d5d" />
                <MasterTableView CellPadding="2" GridLines="Both" BorderWidth="1" BorderColor="#888ac4" CellSpacing="0" AutoGenerateColumns="False" AllowMultiColumnSorting="false" AllowNaturalSort="false">
                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="ColChk_Row">
                            <ItemStyle HorizontalAlign="Center" Wrap="false"></ItemStyle>
                            <HeaderStyle Width="24" BackColor="#c1c2ff" ForeColor="#303030" />
                            <ItemTemplate>
                                <input id="ChkRow" runat="server" type="checkbox" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColMitumori" HeaderText="発注No">
                            <HeaderStyle Width="50" Font-Size="15" HorizontalAlign="Center" BackColor="#c1c2ff" ForeColor="#303030" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" ForeColor="#5d5d5d"></ItemStyle>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColCategori" HeaderText="カテゴリ">
                            <HeaderStyle Width="65" Font-Size="15" HorizontalAlign="Center" BackColor="#c1c2ff" ForeColor="#303030" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" ForeColor="#5d5d5d" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColShiireCode" HeaderText="仕入先コード">
                            <HeaderStyle Width="65" Font-Size="15" HorizontalAlign="Center" BackColor="#c1c2ff" ForeColor="#303030" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" ForeColor="#5d5d5d" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColShiireName" HeaderText="仕入先名">
                            <HeaderStyle Width="65" Font-Size="15" HorizontalAlign="Center" BackColor="#c1c2ff" ForeColor="#303030" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" ForeColor="#5d5d5d" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColSuryo" HeaderText="数量">
                            <HeaderStyle Width="65" Font-Size="15" HorizontalAlign="Center" BackColor="#c1c2ff" ForeColor="#303030" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" Font-Size="15" ForeColor="#5d5d5d" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColShiirekingaku" HeaderText="仕入金額">
                            <HeaderStyle Width="65" Font-Size="15" HorizontalAlign="Center" BackColor="#c1c2ff" ForeColor="#303030" />
                            <ItemStyle HorizontalAlign="Right" Wrap="false" Font-Size="15" ForeColor="#5d5d5d" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="ColOrderedDate" HeaderText="発注日">
                            <HeaderStyle Width="65" Font-Size="15" HorizontalAlign="Center" BackColor="#c1c2ff" ForeColor="#303030" />
                            <ItemStyle HorizontalAlign="Right" Wrap="false" Font-Size="15" ForeColor="#5d5d5d" />
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
                    <Scrolling AllowScroll="True" FrozenColumnsCount="0" ScrollHeight="600px" EnableColumnClientFreeze="True" UseStaticHeaders="True" />

                </ClientSettings>
            </telerik:RadGrid>
            <input type="hidden" id="count" runat="server" />
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
