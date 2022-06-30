<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterPortalImage.aspx.cs" Inherits="Gyomu.Tokuisaki.MasterPortalImage" %>

<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Common/CtrlFilter.ascx" TagName="CtlFilter" TagPrefix="uc3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>得意先画像マスタ</title>
    <link href="css/MasterPortalImage.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>


            <div>
                <uc:Menu ID="Menu" runat="server" />
                <telerik:RadTabStrip ID="RT" runat="server" Skin="Office2007" AutoPostBack="True" SelectedIndex="1" BackColor="#8dea8d">
                    <Tabs>
                        <telerik:RadTab Text="得意先画像マスタ" Font-Size="12pt" NavigateUrl="MasterPortalImage.aspx" Selected="true">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
            </div>



            <table id="SearchTable" border="1">
                <tr>
                    <th>商品コード/商品名</th>
                    <td>
                        <telerik:RadComboBox runat="server" ID="ShouhinCodeCombo" AutoPostBack="true" EnableLoadOnDemand="true" OnItemsRequested="ShouhinCodeCombo_ItemsRequested" Width="500" Height="180px"></telerik:RadComboBox>
                    </td>
                    <%-- <th>商品名</th>
                    <td>
                        <telerik:RadComboBox runat="server" ID="ShouhinNameCombo" AutoPostBack="true" EnableLoadOnDemand="true" OnItemsRequested="ShouhinNameCombo_ItemsRequested" Width="300" Height="180px"></telerik:RadComboBox>
                    </td>--%>
                    <td>
                        <asp:Button runat="server" ID="SearchButton" Text="検索" OnClick="SearchButton_Click" CssClass="Button" /></td>
                </tr>
                <%-- <tr>
                    <asp:RadioButtonList runat="server" ID="ShouhinRadioButton" RepeatDirection="Horizontal">
                        <asp:ListItem Text="両方" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="画像有り"></asp:ListItem>
                        <asp:ListItem Text="画像無し"></asp:ListItem>
                    </asp:RadioButtonList>
                </tr>--%>
            </table>







            <div id="UploadDiv">
                <asp:Button runat="server" ID="ImageUploadButton" Text="アップロード" OnClick="ImageUploadButton_Click" CssClass="Button" />
                <asp:FileUpload runat="server" ID="FileUpload" AllowMultiple="true" />
            </div>






            <main>
                <telerik:RadGrid ID="MainRadGrid" runat="server" PageSize="20" AllowPaging="True" AutoGenerateColumns="False"
                    OnPageIndexChanged="MainRadGrid_PageIndexChanged" OnItemCommand="MainRadGrid_ItemCommand" OnItemDataBound="MainRadGrid_ItemDataBound">

                    <PagerStyle Position="Top" AlwaysVisible="true" PageSizeControlType="None"
                        PagerTextFormat="ページ移動: {4} &amp;nbsp;ページ : &lt;strong&gt;{0:N0}&lt;/strong&gt; / &lt;strong&gt;{1:N0}&lt;/strong&gt; | 件数: &lt;strong&gt;{2:N0}&lt;/strong&gt; - &lt;strong&gt;{3:N0}件&lt;/strong&gt; / &lt;strong&gt;{5:N0}&lt;/strong&gt;件中"
                        FirstPageToolTip="最初のページに移動" LastPageToolTip="最後のページに移動" NextPageToolTip="次のページに移動" PrevPageToolTip="前のページに移動" />


                    <AlternatingItemStyle BackColor="#93bbff" />

                    <MasterTableView>

                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="画像">

                                <ItemTemplate>
                                    <asp:Image runat="server" ID="ShouhinImage" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="Syouhincode" HeaderText="商品コード"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SyouhinMei" HeaderText="商品名"></telerik:GridBoundColumn>

                            <telerik:GridButtonColumn ButtonType="PushButton" CommandName="Delete" Text="画像削除" HeaderText="削除" ButtonCssClass="Button"></telerik:GridButtonColumn>
                        </Columns>

                    </MasterTableView>


                </telerik:RadGrid>
            </main>









            <%--*********************************************--%>
        </div>
        <telerik:RadAjaxManager runat="server" ID="Ram" OnAjaxRequest="Ram_AjaxRequest"></telerik:RadAjaxManager>
    </form>
    <script>
        'use strict';



        function keydown(e) {
            if (e.keyCode === 13) {
                document.getElementById('SearchButton').focus();
            }
        }

        window.onkeydown = keydown;

    </script>
</body>
</html>
