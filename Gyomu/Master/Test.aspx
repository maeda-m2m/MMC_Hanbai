<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="Gyomu.Master.Test" %>

<%@ Register Src="~/CtlMenu.ascx" TagName="HMenu" TagPrefix="uc1" %>
<%@ Register Src="~/Master/CtlSyohin.ascx" TagName="M_Syohin" TagPrefix="uc2" %>
<%@ Register Src="~/Master/CtlSyouhin2.ascx" TagName="M_Syohin2" TagPrefix="uc4" %>
<%@ Register Src="~/Common/CtrlFilter.ascx" TagName="Filter" TagPrefix="uc3" %>

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
    <form runat="server" id="form1">
        <asp:DataGrid runat="server" ID="testG" OnItemDataBound="testG_ItemDataBound" AllowPaging="true" OnPageIndexChanged="testG_PageIndexChanged" PageSize="20" AutoGenerateColumns="false">
            <PagerStyle Mode="NumericPages" Position="Top" PageButtonCount="3" />
            <Columns>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:Label runat="server" ID="LblSyouhinCode"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>

                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:Label runat="server" ID="LblSyouhinName"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>

            </Columns>
        </asp:DataGrid>
        <asp:Button runat="server" ID="Btn" Text="保存" OnClick="Btn_Click" />
        <asp:ScriptManager runat="server" ID="SM"></asp:ScriptManager>
    </form>
</body>
</html>
