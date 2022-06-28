<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterPortal.aspx.cs" Inherits="Gyomu.Tokuisaki.MasterPortal" %>

<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Common/CtrlFilter.ascx" TagName="CtlFilter" TagPrefix="uc3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>得意先マスター</title>
    <link href="css/MasterPortal.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">






        <div>




            <div>
                <uc:Menu ID="Menu" runat="server" />
                <telerik:RadTabStrip ID="RT" runat="server" Skin="Office2007" AutoPostBack="True" SelectedIndex="1" BackColor="#8dea8d">
                    <Tabs>
                        <telerik:RadTab Text="得意先ポータルマスタ" Font-Size="12pt" NavigateUrl="MasterPortal.aspx" Selected="true">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
            </div>



            <asp:Panel runat="server" ID="EditPanel">
                <div id="EditPanelDiv">
                    <asp:Button runat="server" ID="TourokuButton" Text="登録" OnClick="TourokuButton_Click" CssClass="Button" />
                    <asp:Button runat="server" ID="BackButton" Text="戻る" OnClick="BackButton_Click" CssClass="Button" />
                </div>
                <table id="TourokuTable" border="1">
                    <tr>
                        <th>商品コード</th>
                        <td>
                            <telerik:RadComboBox runat="server" ID="ShouhinCodeComboTouroku" AutoPostBack="true" EnableLoadOnDemand="true" OnItemsRequested="ShouhinCodeComboTouroku_ItemsRequested" Width="500px"></telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <th>商品名</th>
                        <td>
                            <asp:TextBox runat="server" ID="shouhinTxt" CssClass="TableInput"></asp:TextBox></td>
                    </tr>

                    <tr>
                        <th>メーカー</th>
                        <td>
                            <asp:TextBox runat="server" ID="MakerTxt" CssClass="TableInput"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>キャッチコピー</th>
                        <td>
                            <asp:TextBox runat="server" ID="CatchTxt" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>内容</th>
                        <td>
                            <asp:TextBox runat="server" ID="NaiyouTxt" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>監督</th>
                        <td>
                            <asp:TextBox runat="server" ID="KantokuTxt" CssClass="TableInput"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>出演</th>
                        <td>
                            <asp:TextBox runat="server" ID="ActorTxt" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>メーカー品番</th>
                        <td>
                            <asp:TextBox runat="server" ID="MakerCodeTxt" TextMode="Number" CssClass="TableInput"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>仕様</th>
                        <td>
                            <asp:TextBox runat="server" ID="ShiyouTxt" CssClass="TableInput"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>コピーライト</th>
                        <td>
                            <asp:TextBox runat="server" ID="CopyrightTxt" CssClass="TableInput"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>メディア</th>
                        <td>
                            <asp:TextBox runat="server" ID="MediaTxt" CssClass="TableInput"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>上映時間</th>
                        <td>
                            <asp:TextBox runat="server" ID="TimeTxt" TextMode="Number" CssClass="TableInput"></asp:TextBox></td>
                    </tr>
                </table>
                <asp:HiddenField runat="server" ID="shouhinCodeHidden" />
                <asp:HiddenField runat="server" ID="EditCheckHidden" />
            </asp:Panel>





            <asp:Panel runat="server" ID="MainPanel">
                <div>
                    <table id="SearchTable" border="1">
                        <tr>
                            <th>商品コード</th>
                            <td>
                                <telerik:RadComboBox runat="server" ID="ShouhinCodeCombo" AutoPostBack="true" EnableLoadOnDemand="true" OnItemsRequested="ShouhinCodeCombo_ItemsRequested" Width="300px"></telerik:RadComboBox>
                            </td>
                            <th>メーカー品番</th>
                            <td>
                                <telerik:RadComboBox runat="server" ID="MakerCodeCombo" AutoPostBack="true" EnableLoadOnDemand="true" OnItemsRequested="MakerCodeCombo_ItemsRequested" Width="300px"></telerik:RadComboBox>
                            </td>
                            <td rowspan="2">
                                <asp:Button runat="server" ID="SearchButton" Text="検索" OnClick="SearchButton_Click" CssClass="Button" /></td>
                        </tr>
                        <tr>
                            <th>商品名</th>
                            <td>
                                <telerik:RadComboBox runat="server" ID="ShouhinNameCombo" AutoPostBack="true" EnableLoadOnDemand="true" OnItemsRequested="ShouhinNameCombo_ItemsRequested" Width="400px"></telerik:RadComboBox>
                            </td>
                            <th>メーカー</th>
                            <td>
                                <telerik:RadComboBox runat="server" ID="MakerCombo" AutoPostBack="true" EnableLoadOnDemand="true" OnItemsRequested="MakerCombo_ItemsRequested" Width="400px"></telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <th>メディア</th>
                            <td>
                                <asp:DropDownList runat="server" ID="MediaDrop">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem>DVD</asp:ListItem>
                                    <asp:ListItem>BD</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>

                    </table>

                </div>

                <div id="ButtonMenuDiv">
                    <asp:Button runat="server" ID="NewTouroku" Text="新規登録" OnClick="NewTouroku_Click" CssClass="Button" />
                    <asp:Button runat="server" ID="DownLoadButton" Text="CSVダウンロード" OnClick="DownLoadButton_Click" CssClass="Button" />
                    <asp:Button runat="server" ID="UploadButton" Text="アップロード" OnClick="UploadButton_Click" CssClass="Button" />
                    <asp:FileUpload runat="server" ID="FileUpload" />
                </div>

                <main>

                    <telerik:RadGrid ID="MainRadGrid" runat="server" PageSize="20" AllowPaging="True" AutoGenerateColumns="False"
                        OnPageIndexChanged="MainRadGrid_PageIndexChanged"
                        OnItemCommand="MainRadGrid_ItemCommand" OnItemDataBound="MainRadGrid_ItemDataBound">

                        <PagerStyle Position="Top" AlwaysVisible="true" PageSizeControlType="None"
                            PagerTextFormat="ページ移動: {4} &amp;nbsp;ページ : &lt;strong&gt;{0:N0}&lt;/strong&gt; / &lt;strong&gt;{1:N0}&lt;/strong&gt; | 件数: &lt;strong&gt;{2:N0}&lt;/strong&gt; - &lt;strong&gt;{3:N0}件&lt;/strong&gt; / &lt;strong&gt;{5:N0}&lt;/strong&gt;件中"
                            FirstPageToolTip="最初のページに移動" LastPageToolTip="最後のページに移動" NextPageToolTip="次のページに移動" PrevPageToolTip="前のページに移動" />


                        <AlternatingItemStyle BackColor="#93bbff" />

                        <MasterTableView>

                            <Columns>
                                <telerik:GridBoundColumn DataField="Syouhincode" HeaderText="商品コード"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SyouhinMei" HeaderText="商品名"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Media" HeaderText="メディア"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ShouhinNumber" HeaderText="メーカー品番"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="OfficialName" HeaderText="メーカー"></telerik:GridBoundColumn>
                                <telerik:GridButtonColumn ButtonType="PushButton" CommandName="Shousai" Text="詳細" HeaderText="詳細" ButtonCssClass="Button"></telerik:GridButtonColumn>
                            </Columns>

                        </MasterTableView>


                    </telerik:RadGrid>
                </main>
            </asp:Panel>




            <%--***********************--%>
        </div>






        <telerik:RadAjaxManager runat="server" ID="Ram" OnAjaxRequest="Ram_AjaxRequest"></telerik:RadAjaxManager>
    </form>
</body>
</html>
