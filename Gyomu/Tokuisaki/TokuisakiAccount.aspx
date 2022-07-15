<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TokuisakiAccount.aspx.cs" Inherits="Gyomu.Tokuisaki.TokuisakiAccount" %>



<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Common/CtrlFilter.ascx" TagName="CtlFilter" TagPrefix="uc3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>得意先アカウントマスタ</title>
    <link href="css/TokuisakiAccount.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">



        <div>



            <div>
                <uc:Menu ID="Menu" runat="server" />
                <telerik:RadTabStrip ID="RT" runat="server" Skin="Office2007" AutoPostBack="True" SelectedIndex="1" BackColor="#8dea8d">
                    <Tabs>
                        <telerik:RadTab Text="得意先アカウントマスタ" Font-Size="12pt" NavigateUrl="TokuisakiAccount.aspx" Selected="true">
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
                        <th>カテゴリ</th>
                        <td>
                            <asp:DropDownList runat="server" ID="CategoryDropTouroku">

                                <asp:ListItem Text="バス" Value="203"> </asp:ListItem>
                                <asp:ListItem Text="キッズ・BGV" Value="209"> </asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <th>事業所名</th>
                        <td>
                            <asp:TextBox runat="server" ID="CompanyText" placeholder="例）株式会社ムービーマネジメントカンパニー"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>ジギョウショメイ</th>
                        <td>
                            <asp:TextBox runat="server" ID="CompanykanaText" placeholder="例）ムービーマネジメントカンパニー"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>御担当者</th>
                        <td>
                            <asp:TextBox runat="server" ID="TantouText" placeholder="例）山田 太郎"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>郵便番号</th>
                        <td>
                            <asp:TextBox runat="server" ID="PostNumberText" autocomplete="shipping postal-code" MaxLength="8" placeholder="例）1500022"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>住所</th>
                        <td>
                            <asp:TextBox runat="server" ID="CityText" placeholder="例）東京都渋谷区恵比寿南1-1-10"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>建物</th>
                        <td>
                            <asp:TextBox runat="server" ID="AddressText2" placeholder="例）サウスコラム小林8F"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>メールアドレス</th>
                        <td>
                            <asp:TextBox runat="server" ID="mailText" TextMode="Email" placeholder="例）info@mmc-inc.jp"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>電話番号</th>
                        <td>
                            <asp:TextBox runat="server" ID="PhoneNumberText" TextMode="Phone" placeholder="例）03-5768-0821"></asp:TextBox></td>

                    </tr>
                    <tr>
                        <th>ID</th>
                        <td>
                            <asp:TextBox runat="server" ID="IDText" ReadOnly="true"></asp:TextBox></td>

                    </tr>
                    <tr>
                        <th>パスワード</th>
                        <td>
                            <asp:TextBox runat="server" ID="PWText"></asp:TextBox></td>

                    </tr>
                    <tr>
                        <th>有効/無効</th>
                        <td>
                            <asp:DropDownList runat="server" ID="AvailableDrop">
                                <asp:ListItem Text="有効" Value="True"></asp:ListItem>
                                <asp:ListItem Text="無効" Value="False"></asp:ListItem>
                            </asp:DropDownList>
                        </td>

                    </tr>
                </table>

                <asp:HiddenField runat="server" ID="EditCheckHidden" />
                <asp:HiddenField runat="server" ID="ShisetsuCodeHidden" />
                <asp:HiddenField runat="server" ID="AccountIDHidden" />


            </asp:Panel>





            <asp:Panel runat="server" ID="MainPanel">

                <div>

                    <table id="SearchTable" border="1">
                        <tr>
                            <th>カテゴリ</th>
                            <td>
                                <asp:DropDownList runat="server" ID="CategoryDropSearch" Width="160px">
                                    <asp:ListItem Text="" Value=""> </asp:ListItem>
                                    <asp:ListItem Text="バス" Value="203"> </asp:ListItem>
                                    <asp:ListItem Text="キッズ・BGV" Value="209"> </asp:ListItem>
                                </asp:DropDownList></td>

                            <th>事業所No/事業所名</th>
                            <td>
                                <telerik:RadComboBox runat="server" ID="ShisetsuNoCombo" AutoPostBack="true" EnableLoadOnDemand="true" OnItemsRequested="ShisetsuNoCombo_ItemsRequested" Width="400px"></telerik:RadComboBox>
                            </td>

                        </tr>

                        <tr>
                            <th>郵便番号</th>
                            <td>
                                <telerik:RadComboBox runat="server" ID="YuubinCombo" AutoPostBack="true" EnableLoadOnDemand="true" OnItemsRequested="YuubinCombo_ItemsRequested"></telerik:RadComboBox>
                            </td>
                            <th>住所</th>
                            <td colspan="3">
                                <telerik:RadComboBox runat="server" ID="AddressCombo" AutoPostBack="true" EnableLoadOnDemand="true" OnItemsRequested="AddressCombo_ItemsRequested" Width="400px"></telerik:RadComboBox>
                            </td>

                            <td>
                                <asp:Button runat="server" ID="SearchButton" Text="検索" OnClick="SearchButton_Click" CssClass="Button" /></td>
                        </tr>



                        <tr>
                            <th>御担当者</th>
                            <td>
                                <telerik:RadComboBox runat="server" ID="TantouCombo" AutoPostBack="true" EnableLoadOnDemand="true" OnItemsRequested="TantouCombo_ItemsRequested"></telerik:RadComboBox>
                            </td>
                            <th>ID</th>
                            <td>
                                <telerik:RadComboBox runat="server" ID="IDCombo" AutoPostBack="true" EnableLoadOnDemand="true" OnItemsRequested="IDCombo_ItemsRequested" Width="400px"></telerik:RadComboBox>
                            </td>
                            <th>有効/無効</th>
                            <td>
                                <asp:DropDownList runat="server" ID="AvailableDropSearch">
                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                    <asp:ListItem Text="有効" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="無効" Value="False"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>



                    </table>

                </div>

                <div id="ButtonMenuDiv">

                    <asp:Button runat="server" ID="NewTouroku" Text="新規作成" OnClick="NewTouroku_Click" CssClass="Button" />
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
                                <telerik:GridBoundColumn DataField="CategoryCode" HeaderText="カテゴリコード"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FacilityNo" HeaderText="施設No."></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FacilityName1" HeaderText="施設名"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PostNo" HeaderText="郵便番号"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Address1" HeaderText="住所"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FacilityResponsible" HeaderText="御担当者"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="password" HeaderText="パスワード"></telerik:GridBoundColumn>
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
