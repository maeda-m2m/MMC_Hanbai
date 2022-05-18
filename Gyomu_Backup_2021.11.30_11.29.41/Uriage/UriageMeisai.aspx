<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UriageMeisai.aspx.cs" Inherits="Gyomu.Uriage.UriageMeisai" %>

<%@ Register Src="~/Uriage/UriageMeisaiRan.ascx" TagName="Syosai" TagPrefix="uc2" %>
<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="UriageMeisai.css" type="text/css" rel="stylesheet" />
    <script src="http://code.jquery.com/jquery-1.11.3.min.js"></script>
    <script src="http://code.jquery.com/jquery-migrate-1.2.1.min.js"></script>
    <script src="JavaScript.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>売上明細</title>
    <style type="text/css">
        .MiniChange {
            background-color: #8dea8d;
            text-align: center;
            font-size: 12px;
            font: bold;
            color: black;
            width: 100px;
            height: 20px;
        }

        #KariTokui {
            display: none;
        }

        #kariSekyu {
            display: none;
        }

        #TbxKake {
            display: none;
        }

        .CategoryChabge {
            display: block;
        }

        .Zasu {
            display: block;
        }

        #LblAlert {
            margin: 0;
            padding: 0;
        }

        #KariNouhin {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="margin-left: 10px">
        <uc:Menu ID="Menu" runat="server" />
        <telerik:RadTabStrip ID="RT" runat="server" AutoPostBack="True" SelectedIndex="1" BackColor="#d2eaf6">
            <Tabs>
                <telerik:RadTab Text="売上一覧" Font-Size="12pt" NavigateUrl="UriageIchiran.aspx">
                </telerik:RadTab>
                <telerik:RadTab Text="売上明細" Font-Size="12pt" NavigateUrl="UriageMeisai.aspx" Selected="True">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <br />
        <div>
            <table>
                <tr>
                    <td>
                        <asp:Button runat="server" ID="BtnKakutei" Text="確定" CssClass="Btn" OnClick="BtnKakutei_Click" />
                    </td>
                    <td>
                        <asp:Button runat="server" ID="BtnTouroku" Text="登録" CssClass="Btn" OnClick="BtnTouroku_Click" />
                    </td>
                    <td>
                        <asp:Button runat="server" ID="BtnDelete" Text="削除" CssClass="Btn" OnClick="BtnDelete_Click" OnClientClick="A()" />
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

                    </td>
                    <td>
                        <asp:Button runat="server" ID="BtnCsvDL" Text="CSVダウンロード" CssClass="Btn" Width="150px" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <asp:Label runat="server" ID="Err" ForeColor="Red"></asp:Label><asp:Label runat="server" ID="LblEnd" ForeColor="Green"></asp:Label>
        <%--ヘッダー部分--%>
        <div>
            <div id="header">
                <table runat="server" id="uInput">
                    <tr>
                        <td class="uInputTitle" runat="server">
                            <p>売上NO.</p>
                        </td>
                        <td class="uwaku">
                            <asp:Label ID="Label94" runat="server" Text="" Width="80px" Font-Size="12px"></asp:Label>
                        </td>
                        <td class="uMiniTitle">
                            <p>カテゴリー</p>
                        </td>
                        <td class="uwaku">
                            <telerik:RadComboBox ID="RadComboCategory" runat="server" OnItemsRequested="RadComboCategory_ItemsRequested" AllowCustomText="True" EnableLoadOnDemand="True" MarkFirstMatch="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" OnSelectedIndexChanged="RadComboCategory_SelectedIndexChanged" Width="100px"></telerik:RadComboBox>
                            <input type="hidden" id="CategoryCode" runat="server" />
                        </td>
                        <td class="uMiniTitle">
                            <p>部門</p>
                        </td>
                        <td class="uwaku">
                            <telerik:RadComboBox ID="RadComboBox4" runat="server"></telerik:RadComboBox>
                            <input type="hidden" id="BumonCode" runat="server" />
                            <input type="hidden" id="BumonKubun" runat="server" />
                        </td>
                        <td class="uMiniTitle">
                            <p>担当者</p>
                        </td>
                        <td class="uwaku">
                            <asp:Label ID="Label1" runat="server" Text="担当者"></asp:Label>
                            <input type="hidden" id="UserID" runat="server" />
                        </td>
                        <td class="uMiniTitle">
                            <p>照会日付</p>
                        </td>
                        <td class="uwaku">
                            <telerik:RadDatePicker ID="RadDatePicker1" runat="server" Width="100px"></telerik:RadDatePicker>
                        </td>
                        <td class="uMiniTitle"></td>
                        <td class="uwaku"></td>
                        <td class="uMiniTitle"></td>
                        <td class="uwaku"></td>

                    </tr>
                    <tr>
                        <td class="uInputTitle" runat="server">
                            <p>得意先</p>
                        </td>

                        <td colspan="5" class="uwaku">
                            <telerik:RadComboBox ID="RadComboBox1" runat="server" Culture="ja-JP" OnItemsRequested="RadComboBox1_ItemsRequested" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" AutoPostBack="True" OnSelectedIndexChanged="RadComboBox1_SelectedIndexChanged" Width="300px"></telerik:RadComboBox>
                            <input type="hidden" id="TokuisakiCode" runat="server" />
                            <input type="hidden" id="TokuisakiMei" runat="server" />
                            <asp:TextBox ID="KariTokui" runat="server" Width="300px" AutoPostBack="true"></asp:TextBox>

                            <asp:Label runat="server" ID="LblAlert" Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="uMiniTitle">
                            <p>仮宛先</p>
                        </td>
                        <td class="uwaku">
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </td>
                        <td class="uMiniTitle">
                            <p>売上日付</p>
                        </td>
                        <td class="uwaku">
                            <telerik:RadDatePicker ID="RadDatePicker2" runat="server" Width="100px"></telerik:RadDatePicker>
                        </td>

                        <td class="uMiniTitle"></td>
                        <td class="uwaku"></td>
                        <td class="uMiniChange">
                            <p>税区分</p>
                        </td>
                        <td class="uwaku">
                            <telerik:RadComboBox ID="RadZeiKubun" runat="server" Width="70px" OnSelectedIndexChanged="RadZeiKubun_SelectedIndexChanged" AutoPostBack="true">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Value="" />
                                    <telerik:RadComboBoxItem runat="server" Text="税込" Value="税込" />
                                    <telerik:RadComboBoxItem runat="server" Text="税抜" Value="税抜" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="uInputTitle" runat="server">
                            <p>請求先</p>
                        </td>

                        <td colspan="5" class="uwaku">
                            <telerik:RadComboBox ID="RadComboBox3" runat="server" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" OnItemsRequested="RadComboBox3_ItemsRequested" Width="300px"></telerik:RadComboBox>
                            <asp:TextBox ID="kariSekyu" runat="server" Width="300px"></asp:TextBox>

                        </td>
                        <td class="uMiniTitle">
                            <p>リレー状況</p>
                        </td>
                        <td class="uwaku">
                            <asp:TextBox ID="TextBox1" runat="server" Width="80px" Height="20px"></asp:TextBox>

                        </td>
                        <td class="uMiniTitle"></td>
                        <td class="uwaku"></td>
                        <td class="uMiniTitle">
                            <p>締め日</p>
                        </td>
                        <td class="uwaku">
                            <asp:Label ID="Shimebi" runat="server" Text=""></asp:Label>

                        </td>
                        <td class="uMiniTitle">
                            <p>数量合計</p>
                        </td>
                        <td runat="server" class="usisan">
                            <asp:TextBox ID="TextBox10" runat="server" CssClass="Text" Width="80px" Height="20px" Text="0" TextMode="Number"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="uInputTitle" runat="server">
                            <p>納品先</p>
                        </td>

                        <td colspan="5" class="uwaku">
                            <telerik:RadComboBox ID="RadComboBox2" runat="server" Culture="ja-JP" OnItemsRequested="RadComboBox2_ItemsRequested" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" Width="300px"></telerik:RadComboBox>
                            <input type="hidden" id="TyokusoCode" runat="server" />
                            <asp:TextBox ID="KariNouhin" runat="server" Width="300px"></asp:TextBox>

                        </td>
                        <td class="uMiniChange">
                            <p>掛率</p>
                        </td>
                        <td class="uwaku">
                            <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                            <asp:TextBox ID="TbxKake" runat="server" Width="80px" Height="20px" OnTextChanged="TbxKake_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td class="uMiniTitle"></td>
                        <td class="uwaku"></td>
                        <td class="uMiniTitle">
                            <p>売上計</p>
                        </td>
                        <td runat="server" class="usisan">
                            <asp:TextBox ID="TextBox7" runat="server" Width="80px" Height="20px" CssClass="Text" AutoPostBack="true" Text="0"></asp:TextBox>
                            <input type="hidden" id="urikei" runat="server" />
                        </td>
                        <td class="uMiniTitle">
                            <p>仕入計</p>
                        </td>
                        <td class="usisan">
                            <asp:TextBox ID="TextBox8" runat="server" Width="80px" Height="20px" CssClass="Text" AutoPostBack="true" Text="0"></asp:TextBox>
                            <input type="hidden" id="shikei" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="uInputTitle" runat="server">
                            <p>使用施設名</p>
                        </td>
                        <td colspan="3" class="uwaku">
                            <telerik:RadComboBox ID="FacilityRad" runat="server" Culture="ja-JP" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" AutoPostBack="True" Width="250px" OnSelectedIndexChanged="FacilityRad_SelectedIndexChanged" OnItemsRequested="FacilityRad_ItemsRequested"></telerik:RadComboBox>
                            <br />
                            <input type="hidden" id="FacilityAddress" runat="server" />
                            <asp:CheckBox ID="CheckBox5" runat="server" Text="複数" AutoPostBack="True" OnCheckedChanged="CheckBox5_CheckedChanged" />
                        </td>
                        <td class="uMiniTitle">
                            <p>開始</p>
                        </td>
                        <td class="uwaku">
                            <telerik:RadDatePicker ID="RadDatePicker3" runat="server" Width="100px" CssClass="CategoryChabge"></telerik:RadDatePicker>
                        </td>
                        <td class="uMiniTitle">
                            <p>使用期間</p>
                        </td>
                        <td class="uwaku">
                            <asp:DropDownList ID="DropDownList9" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList9_SelectedIndexChanged" Width="120px" CssClass="CategoryChabge">
                                <asp:ListItem>使用期間を選択</asp:ListItem>
                                <asp:ListItem>1日</asp:ListItem>
                                <asp:ListItem>2日</asp:ListItem>
                                <asp:ListItem>3日</asp:ListItem>
                                <asp:ListItem>4日</asp:ListItem>
                                <asp:ListItem>5日</asp:ListItem>
                                <asp:ListItem>1ヵ月</asp:ListItem>
                                <asp:ListItem>2ヵ月</asp:ListItem>
                                <asp:ListItem>3ヵ月</asp:ListItem>
                                <asp:ListItem>4ヵ月</asp:ListItem>
                                <asp:ListItem>5ヵ月</asp:ListItem>
                                <asp:ListItem>6ヵ月</asp:ListItem>
                                <asp:ListItem>1年</asp:ListItem>
                                <asp:ListItem>99年</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <asp:CheckBox ID="CheckBox4" runat="server" Text="複数" AutoPostBack="True" OnCheckedChanged="CheckBox4_CheckedChanged" CssClass="CategoryChabge" />
                        </td>

                        <td class="uMiniTitle">
                            <p>終了</p>
                        </td>
                        <td class="uwaku">
                            <telerik:RadDatePicker ID="RadDatePicker4" runat="server" Width="100px" CssClass="CategoryChabge"></telerik:RadDatePicker>
                        </td>
                        <td class="uMiniTitle">
                            <p>消費税額</p>
                        </td>
                        <td class="usisan">
                            <asp:TextBox ID="TextBox5" runat="server" Width="80px" Height="20px" CssClass="Text"></asp:TextBox>
                            <input type="hidden" runat="server" id="zeikei" />
                        </td>
                        <td class="uMiniTitle">
                            <p>粗利計</p>
                        </td>
                        <td class="usisan">
                            <asp:TextBox ID="TextBox6" runat="server" Width="80px" Height="20px" CssClass="Text"></asp:TextBox>
                            <input type="hidden" runat="server" id="arakei" />
                        </td>


                    </tr>
                    <tr>
                        <td class="uInputTitle" runat="server">
                            <p>備考</p>
                        </td>
                        <td colspan="9" class="uwaku">
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        </td>
                        <td class="uMiniTitle">
                            <p>売上合計</p>
                        </td>

                        <td class="usisan">
                            <asp:TextBox ID="TextBox12" runat="server" Width="80px" Height="20px" CssClass="Text"></asp:TextBox>
                            <input type="hidden" runat="server" id="soukei" />
                        </td>
                        <td class="uMiniTitle">
                            <p>利益率</p>
                        </td>
                        <td class="usisan">
                            <asp:TextBox ID="TextBox13" runat="server" Width="80px" Height="20px" CssClass="Text"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <img src="../Img/売上明細.png" runat="server" width="1300" />
            </div>
            <div>
                <asp:DataGrid runat="server" ID="CtrlSyousai" AutoGenerateColumns="False" OnItemDataBound="CtrlSyousai_ItemDataBound" OnItemCommand="CtrlSyousai_ItemCommand" Width="1300px">
                    <Columns>

                        <asp:TemplateColumn HeaderStyle-BorderStyle="None" ItemStyle-BorderStyle="None" ItemStyle-Width="30px">
                            <ItemTemplate>
                                <asp:Label ID="RowNo" runat="server" Text="" Font-Size="25px"></asp:Label>
                                <asp:Button ID="Button4" runat="server" Text="削除" CssClass="Btn" CommandName="Del" />
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderStyle-BorderStyle="None" ItemStyle-BorderStyle="None">
                            <ItemTemplate>
                                <uc2:Syosai ID="Syosai" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </div>

        </div>
    </form>
</body>
</html>
