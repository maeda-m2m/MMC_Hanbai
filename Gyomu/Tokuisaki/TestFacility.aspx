<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestFacility.aspx.cs" Inherits="Gyomu.Tokuisaki.TestFacility" %>

<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Common/CtrlFilter.ascx" TagName="CtlFilter" TagPrefix="uc3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>テスト施設マスタ</title>
    <style>
        body {
            font-family: 'Yu Gothic UI';
        }

        .Button {
            padding: 0.3em 1em;
            color: white;
            border: solid 2px #619fed;
            border-radius: 10px;
            background-color: #92baec;
        }

            .Button:hover {
                cursor: pointer;
                transition: .4s;
                background: #619fed;
                color: white;
                border: solid 2px #2c75d0;
            }

        #SearchMenu {
            margin-top: 20px;
            margin-bottom: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <div>
                <uc:Menu ID="Menu" runat="server" />
                <telerik:RadTabStrip ID="RT" runat="server" Skin="Office2007" AutoPostBack="True" SelectedIndex="1" BackColor="#8dea8d">
                    <Tabs>
                        <telerik:RadTab Text="テスト施設マスタ" Font-Size="12pt" NavigateUrl="TestFacility.aspx" Selected="true">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>

                <div id="SearchMenu">

                    <asp:DropDownList runat="server" ID="CategoryDrop">
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                        <asp:ListItem Text="施設コード" Value="FacilityNo"></asp:ListItem>
                        <asp:ListItem Text="施設名" Value="FacilityName1"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox runat="server" ID="SearchText"></asp:TextBox>を含む
                    <asp:Button runat="server" ID="SearchButton" Text="検索" OnClick="SearchButton_Click" CssClass="Button" />
                    <asp:HiddenField runat="server" ID="SearchHidden" />
                </div>
                <telerik:RadGrid ID="MainRadGrid" runat="server" PageSize="20" AllowPaging="True" OnPageIndexChanged="MainRadGrid_PageIndexChanged">
                    <ItemStyle Height="50px" />

                    <PagerStyle Position="Top" AlwaysVisible="true" PageSizeControlType="None"
                        PagerTextFormat="ページ移動: {4} &amp;nbsp;ページ : &lt;strong&gt;{0:N0}&lt;/strong&gt; / &lt;strong&gt;{1:N0}&lt;/strong&gt; | 件数: &lt;strong&gt;{2:N0}&lt;/strong&gt; - &lt;strong&gt;{3:N0}件&lt;/strong&gt; / &lt;strong&gt;{5:N0}&lt;/strong&gt;件中"
                        FirstPageToolTip="最初のページに移動" LastPageToolTip="最後のページに移動" NextPageToolTip="次のページに移動" PrevPageToolTip="前のページに移動" />





                </telerik:RadGrid>



            </div>
        </div>
    </form>
</body>
</html>
