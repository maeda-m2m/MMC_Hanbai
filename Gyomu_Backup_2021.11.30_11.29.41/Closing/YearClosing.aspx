<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YearClosing.aspx.cs" Inherits="Gyomu.Closing.YearClosing" %>

<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc:Menu ID="Menu" runat="server" />
        </div>
        <div>
            <telerik:RadTabStrip ID="RT" runat="server" AutoPostBack="True" SelectedIndex="0" BackColor="#d2eaf6">
                <Tabs>
                    <telerik:RadTab Text="月次処理" Font-Size="12pt" NavigateUrl="MonthClosing.aspx">
                    </telerik:RadTab>
                    <telerik:RadTab Text="年次処理" Font-Size="12pt" NavigateUrl="YearClosing.aspx" Selected="true">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
        </div>
        <div>
        </div>
    </form>
</body>
</html>
