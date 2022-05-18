<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterOshirase.aspx.cs" Inherits="Gyomu.Master.MasterOshirase" %>

<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>お知らせ編集</title>
    <link href="MasterOshirase.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div runat="server" class="DivBase">
            <uc:Menu ID="Menu" runat="server" />
            <br />
            <telerik:RadTabStrip ID="RT" runat="server" AutoPostBack="True" Skin="Office2007" SelectedIndex="0">
                <Tabs>
                    <telerik:RadTab Text="お知らせ編集" Font-Size="12pt" NavigateUrl="MasterOshirase.aspx">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <br />
            <asp:Label runat="server" ID="lbl_head"></asp:Label>
            <div runat="server" id="DivList">
                <table runat="server" id="TBKensaku" style="border-collapse: collapse">
                    <tr>
                        <td style="border-bottom: 2px solid  #9DC3E6">
                            <p><b>お知らせ編集</b></p>
                        </td>
                        <td>
                            <asp:Button runat="server" ID="BtnCreate" OnClick="BtnCreate_Click" Text="新規投稿" CssClass="Btn" OnClientClick="Update()" />
                            <script type="text/javascript">
                                //function Update() {
                                //    debugger;
                                //    let create = document.getElementById('DivCreate');
                                //    let list = document.getElementById("DivList");
                                //    list.style.display = "none";
                                //    create.style.display = "";
                                //    debugger;
                                //}
                            </script>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="border-bottom: 2px solid #9DC3E6">
                            <table>
                                <tr>
                                    <td>
                                        <p>投稿者</p>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="TbxUserName"></asp:TextBox>
                                    </td>
                                    <td>
                                        <p>投稿日</p>
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker runat="server" ID="RdpCreateDate"></telerik:RadDatePicker>
                                    </td>
                                    <td>
                                        <p>投稿内容</p>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="TbxNaiyou"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="BtnSearch" OnClick="BtnSearch_Click" Text="検索" CssClass="Btn" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div>
                                <asp:DataGrid runat="server" ID="DGOshirase" OnItemDataBound="DGOshirase_ItemDataBound" OnItemCommand="DGOshirase_ItemCommand" AutoGenerateColumns="false" Width="1000px">
                                    <AlternatingItemStyle BackColor="#d4e8fb" />
                                    <Columns>

                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" BackColor="#0070C0" ForeColor="White" BorderColor="#0070C0" />
                                            <HeaderTemplate>
                                                <p><b>お知らせ内容</b></p>
                                            </HeaderTemplate>
                                            <ItemStyle BorderColor="#0070C0" />
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="LblOshirase"></asp:Label>
                                                <input type="hidden" runat="server" id="HidOshiraseNoRow" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" BackColor="#0070C0" ForeColor="White" BorderColor="#0070C0" />
                                            <HeaderTemplate>
                                                <p><b>投稿者</b></p>
                                            </HeaderTemplate>
                                            <ItemStyle BorderColor="#0070C0" />
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="LblUserName"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" BackColor="#0070C0" ForeColor="White" BorderColor="#0070C0" />
                                            <HeaderTemplate>
                                                <p><b>投稿日</b></p>
                                            </HeaderTemplate>
                                            <ItemStyle BorderColor="#0070C0" />
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="LblCreateDate"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" BackColor="#0070C0" ForeColor="White" BorderColor="#0070C0" />
                                            <HeaderTemplate>
                                                <p><b>有効</b></p>
                                            </HeaderTemplate>
                                            <ItemStyle BorderColor="#0070C0" />
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="LblAccept"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn>
                                            <HeaderStyle Width="150px" BorderColor="#0070C0" />
                                            <ItemStyle BorderColor="#0070C0" />
                                            <ItemTemplate>
                                                <asp:Button runat="server" ID="BtnEdit" CommandName="Edit" Text="編集" CssClass="Btn" Width="70px" />
                                                <asp:Button runat="server" ID="BtnDelete" OnClientClick="PopUP()" CommandName="Delete" Text="削除" CssClass="Btn" Width="70px" />
                                                <script type="text/javascript">
                                                    function PopUP() {
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
                        </td>
                    </tr>
                </table>
            </div>
            <div runat="server" id="DivCreate">
                <table>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Label runat="server" ID="LblStatus"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <input type="hidden" runat="server" id="HidOshiraseNo" />
                            <asp:TextBox runat="server" ID="TbxOshiraseNaiyou" Width="500px" Height="500px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>お知らせに表示</p>
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="ChkAccept" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;">
                            <asp:Button runat="server" ID="BtnUpdate" OnClick="BtnUpdate_Click" Text="登録" />
                            <asp:Button runat="server" ID="BtnList" Text="お知らせ一覧に戻る" OnClientClick="BackList()" OnClick="BtnList_Click" />
                            <script type="text/javascript">
                                function BackList() {
                                    let create = document.getElementById("DivCreate");
                                    let list = document.getElementById("DivList");
                                    list.style.display = "";
                                    create.style.display = "none";
                                }
                            </script>
                        </td>
                    </tr>
                </table>
            </div>
        </div>


    </form>
</body>
</html>
