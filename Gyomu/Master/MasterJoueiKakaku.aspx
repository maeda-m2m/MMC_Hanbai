<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterJoueiKakaku.aspx.cs" Inherits="Gyomu.Master.MasterJoueiKakaku" %>

<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Common/CtrlFilter.ascx" TagName="CtlFilter" TagPrefix="uc3" %>

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
            <telerik:RadTabStrip ID="RT" runat="server" Skin="Office2007" AutoPostBack="True" SelectedIndex="1" BackColor="#8dea8d">
                <Tabs>
                    <telerik:RadTab Text="上映会価格マスタ" Font-Size="12pt" NavigateUrl="MasterJoueiKakaku.aspx" Selected="true">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>

        </div>
        <br />
        <div runat="server" id="K">
            <uc3:CtlFilter ID="F" runat="server" />
        </div>
        <br />
        <div>
            <asp:DataGrid runat="server" ID="DGJoueiKakaku" AutoGenerateColumns="false" OnItemDataBound="DGJoueiKakaku_ItemDataBound" OnItemCommand="DGJoueiKakaku_ItemCommand">
                <Columns>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <p>仕入先コード</p>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="LblShiiresakiCode"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <p>仕入先名</p>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="LblShiiresakiName"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <p>メディア</p>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="LblMedia"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <p>範囲</p>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="LblHanni"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <p>席数</p>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="LblCapacity"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <p>標準価格</p>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="LblHyoujunKakaku"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            <p>仕入価格</p>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="LblShiireKakaku"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:Button runat="server" ID="BtnEdit" CommandName="Edit" Text="編集" />
                            <asp:Button runat="server" ID="BtnDelete" CommandName="Delete" Text="削除" OnClientClick="A()" />
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

                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
    </form>
</body>
</html>
