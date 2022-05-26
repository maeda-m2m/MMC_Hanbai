<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterJoueiKakaku.aspx.cs" Inherits="Gyomu.Master.MasterJoueiKakaku" %>

<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Common/CtrlFilter.ascx" TagName="CtlFilter" TagPrefix="uc3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>上映価格マスタ</title>
    <link href="../../Style/Grid.ykuri.css" rel="STYLESHEET" />
    <link href="../../Style/ComboBox.ykuri.css" type="text/css" rel="STYLESHEET" />
    <link href="../../MainStyle.css" type="text/css" rel="Stylesheet" />
    <link href="../sheet/MainStyles.css" rel="stylesheet" type="text/css" />
    <style>
        /*-----------------------------------------------------------------------*/
        @charset "UTF-8";

        html {
            font-size: 100%;
        }

        body {
            font-family: Meiryo;
        }
        /*-----------------------------------------------------------------------*/
        /*  上のCSS*/
        /*-----------------------------------------------------------------------*/
        header table {
            height: 20px;
            width: 80%;
            background-color: #c4ddff;
        }

        .insert_table {
            height: 30px;
            width: 80%;
            background-color: #c4ddff;
        }

        table th {
            background-color: #006bff;
            color: white;
            font-weight: normal;
        }
        /*-----------------------------------------------------------------------*/
        /*  下のCSS*/
        /*-----------------------------------------------------------------------*/
        #DGJoueiKakaku {
            height: 30%;
            width: 100%;
        }

        .main_head {
            background-color: #0066FF;
            color: white;
            text-align: center;
            font-size: large;
        }
        /*-----------------------------------------------------------------------*/
        .Btn10 {
            text-align: center;
            padding: 0.3em 1em;
            text-decoration: none;
            color: white;
            border: solid 2px #619fed;
            border-radius: 3px;
            transition: .4s;
            background-color: #92baec;
        }

            .Btn10:hover {
                background: #619fed;
                color: white;
                border: solid 2px #2c75d0;
            }
        /*-----------------------------------------------------------------------*/
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc:Menu ID="Menu" runat="server" />
            <telerik:RadTabStrip ID="RT" runat="server" Skin="Office2007" AutoPostBack="True" SelectedIndex="1" BackColor="#8dea8d">
                <Tabs>
                    <telerik:RadTab Text="上映会価格マスタ" Font-Size="12pt" NavigateUrl="MasterJoueiKakaku.aspx" Selected="true">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
        </div>
        <br />
        <asp:Panel runat="server" ID="insert_panel">

            <asp:Button ID="BtnToroku" runat="server" Text="登録" OnClick="BtnToroku_Click" CssClass="Btn10" />
            <asp:Button ID="BtnBack" runat="server" Text="戻る" OnClick="BtnBack_Click" CssClass="Btn10" />
            <asp:Label runat="server" ID="insert_lbl" ForeColor="Red" Font-Size="Large"></asp:Label>
            <br />
            <br />
            <table border="1" runat="server" class="insert_table">
                <tr>
                    <th>媒体</th>
                    <td>
                        <asp:DropDownList runat="server" ID="head_cmb_media">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>DVD</asp:ListItem>
                            <asp:ListItem>BD</asp:ListItem>
                        </asp:DropDownList></td>
                    <th>仕入先</th>
                    <td colspan="4">
                        <telerik:RadComboBox runat="server" ID="head_shiire_name" OnItemsRequested="head_shiire_name_ItemsRequested" EnableLoadOnDemand="true" AutoPostBack="true" Width="500px"></telerik:RadComboBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <th>範囲</th>
                    <td>
                        <telerik:RadComboBox runat="server" ID="head_rcmb_hani"></telerik:RadComboBox>
                    </td>
                    <th>席数</th>
                    <td>
                        <asp:TextBox runat="server" ID="head_txt_seki"></asp:TextBox>
                    </td>
                    <th>標準価格</th>
                    <td>
                        <asp:TextBox runat="server" ID="head_txt_hyoujun"></asp:TextBox></td>
                    <th>仕入価格</th>
                    <td>
                        <asp:TextBox runat="server" ID="head_txt_shiirekakaku"></asp:TextBox></td>
                </tr>
            </table>
        </asp:Panel>
        <div runat="server" id="main_panel">
            <header>
                <table border="1">
                    <tr>
                        <th>媒体</th>
                        <td>
                            <asp:DropDownList runat="server" ID="head_ddl1">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>DVD</asp:ListItem>
                                <asp:ListItem>BD</asp:ListItem>
                            </asp:DropDownList></td>
                        <th>仕入先</th>
                        <td colspan="4">
                            <telerik:RadComboBox runat="server" ID="head_rcb" AutoPostBack="true" EnableLoadOnDemand="true" OnItemsRequested="Unnamed_ItemsRequested" Width="500"></telerik:RadComboBox>
                        </td>
                        <td colspan="4"></td>
                        <td rowspan="2">
                            <asp:Button runat="server" ID="head_btn1" Text="検索" OnClick="head_btn1_Click" CssClass="Btn10" /></td>
                    </tr>
                    <tr>
                        <th>範囲</th>
                        <td>
                            <telerik:RadComboBox runat="server" ID="head_rcb1"></telerik:RadComboBox>
                        </td>
                        <th>席数</th>
                        <td>
                            <telerik:RadComboBox runat="server" ID="head_rcb2"></telerik:RadComboBox>
                        </td>
                        <th>標準価格</th>
                        <td>
                            <asp:TextBox runat="server" ID="head_txt2"></asp:TextBox></td>
                        <th>仕入価格</th>
                        <td>
                            <asp:TextBox runat="server" ID="head_txt3"></asp:TextBox></td>
                    </tr>
                </table>
                <br />
                <asp:Button runat="server" ID="head_shinki_btn1" Text="新規登録" OnClick="head_shinki_btn1_Click" CssClass="Btn10" />

                <asp:Label runat="server" ID="head_lbl1" ForeColor="Red" Font-Size="Large"></asp:Label>
                <asp:Label runat="server" ID="MesseageLabel" Font-Size="Large"></asp:Label>
                <asp:Button runat="server" ID="DownLoadButton" Text="CSVダウンロード" OnClick="DownLoadButton_Click" CssClass="Btn10" />
                <asp:Button runat="server" ID="UploadButton" Text="アップロード" OnClick="UploadButton_Click" CssClass="Btn10" />
                <asp:FileUpload runat="server" ID="FileUpload" />


            </header>
            <%-- <div runat="server" id="K">
            <uc3:CtlFilter ID="F" runat="server" />
        </div>--%>

            <br />

            <main>



                <telerik:RadGrid ID="DGJoueiKakaku1"
                    runat="server"
                    PageSize="20"
                    AllowPaging="True"
                    AllowCustomPaging="True"
                    Skin="ykuri"
                    EnableEmbeddedSkins="False"
                    CellPadding="0"
                    AutoGenerateColumns="False"
                    OnPageIndexChanged="DGJoueiKakaku1_PageIndexChanged"
                    OnEditCommand="DGJoueiKakaku1_EditCommand"
                    OnUpdateCommand="DGJoueiKakaku1_UpdateCommand"
                    OnItemCommand="DGJoueiKakaku1_ItemCommand" OnItemDataBound="DGJoueiKakaku1_ItemDataBound">
                    <PagerStyle
                        Position="Top"
                        AlwaysVisible="true"
                        PagerTextFormat="ページ移動: {4} &amp;nbsp;ページ : &lt;strong&gt;{0:N0}&lt;/strong&gt; / &lt;strong&gt;{1:N0}&lt;/strong&gt; | 件数: &lt;strong&gt;{2:N0}&lt;/strong&gt; - &lt;strong&gt;{3:N0}件&lt;/strong&gt; / &lt;strong&gt;{5:N0}&lt;/strong&gt;件中"
                        PageSizeLabelText="ページサイズ:"
                        FirstPageToolTip="最初のページに移動"
                        LastPageToolTip="最後のページに移動"
                        NextPageToolTip="次のページに移動"
                        PrevPageToolTip="前のページに移動" />
                    <HeaderStyle CssClass="main_head" HorizontalAlign="Center" />
                    <HeaderContextMenu EnableEmbeddedBaseStylesheet="False" CssClass="GridContextMenu GridContextMenu_Outlook">
                    </HeaderContextMenu>
                    <AlternatingItemStyle BackColor="#93bbff" />
                    <MasterTableView
                        CellPadding="2"
                        GridLines="Both"
                        BorderWidth="1"
                        BorderColor="#000000"
                        CellSpacing="0"
                        AutoGenerateColumns="False"
                        AllowMultiColumnSorting="false"
                        AllowNaturalSort="false"
                        EditMode="InPlace">
                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="Media" HeaderText="媒体"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ShiiresakiCode" HeaderText="仕入先コード"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ShiiresakiName" HeaderText="仕入先"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Range" HeaderText="範囲"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Capacity" HeaderText="席数"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="HyoujunKakaku" HeaderText="標準価格" UniqueName="hyoujunkakaku"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ShiireKakaku" HeaderText="仕入価格" UniqueName="shiirekakaku"></telerik:GridBoundColumn>
                            <telerik:GridEditCommandColumn ButtonType="PushButton" HeaderText="編集" EditText="編集" UpdateText="更新" CancelText="戻る"></telerik:GridEditCommandColumn>
                            <telerik:GridButtonColumn ButtonType="PushButton" CommandName="Delete" Text="削除" HeaderText="削除"></telerik:GridButtonColumn>
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


            </main>


        </div>









        <telerik:RadAjaxManager runat="server" OnAjaxRequest="Ram_AjaxRequest" ID="Ram"></telerik:RadAjaxManager>


    </form>
</body>
</html>
