<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Kaihatsu.aspx.cs" Inherits="Gyomu.Kaihatsu" %>

<%@ Register Src="~/CtrlMitsuSyousai.ascx" TagName="Syosai" TagPrefix="uc2" %>
<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="Kaihatsu.css" type="text/css" rel="stylesheet" />
    <script src="http://code.jquery.com/jquery-migrate-1.2.1.min.js"></script>
    <title>見積明細</title>
    <style type="text/css">
        .HeadTD {
            text-align: center;
            background-color: #e6e6e6;
            border: 1px solid #e6e6e6;
            font: bold;
            height: 5px;
            font-size: 10px;
        }

        .HeadTB {
            background-color: white;
            width: 1230px;
            border: 1px solid black;
        }

        #HeadTB {
            background-color: white;
            width: 1300px;
            color: black;
            font: bold;
            font-size: 5px;
            height: 50px;
            border: 1px solid black;
        }

            #HeadTB p {
                font-size: 5px;
            }

        .RowTD {
            width: 90px;
            text-align: center;
            background-color: #d8f2ff;
            border: 1px solid #d8f2ff;
            font: bold;
            height: 10px;
        }


        .HeadSyouhiCD {
            text-align: center;
            background-color: #e6e6e6;
            border: 1px solid #e6e6e6;
            font: bold;
            width: 100px;
            height: 5px;
            font-size: 10px;
        }

        .HeadSyouhiC {
            text-align: center;
            background-color: #e6e6e6;
            border: 1px solid #e6e6e6;
            font: bold;
            width: 120px;
            height: 5px;
            font-size: 10px;
        }

        #head p {
            font-size: 10px;
        }

        .auto-style5 {
            background-color: #e6e6e6;
            height: 30px;
        }

        .SujiText {
            text-align: right;
        }

        .Text {
            width: 60px;
            height: 20px;
            font-size: 12px;
            border-radius: 3px;
            text-align: right;
        }

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

        .auto-style9 {
            background-color: #53d153;
            text-align: center;
            font-size: 12px;
            color: black;
            width: 80px;
            height: 20px;
        }

        .auto-style10 {
            background-color: #e6e6e6;
            height: 20px;
        }

        .auto-style11 {
            background-color: #c8ffc8;
            height: 20px;
        }
    </style>

    <script>
        import { } from "jquery";

        $(function () {
            $("input").keydown(function (e) {
                if ((e.which && e.which === 13) || (e.keyCode && e.keyCode === 13)) {
                    return false;
                } else {
                    return true;
                }
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <uc:Menu ID="Menu" runat="server" />
        <telerik:RadTabStrip ID="RT" runat="server" AutoPostBack="True" SelectedIndex="1" BackColor="#8dea8d">
            <Tabs>
                <telerik:RadTab Text="見積一覧" Font-Size="12pt" NavigateUrl="~/Mitumori/MitumoriItiran.aspx">
                </telerik:RadTab>
                <telerik:RadTab Text="見積入力" Font-Size="12pt" NavigateUrl="Kaihatsu.aspx" Selected="True">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <br />

        <table runat="server" id="SubMenu">
            <tr>
                <td>
                    <asp:Button ID="BtnKeisan" runat="server" Text="計算" CssClass="Btn10" OnClick="Keisan_Click" />
                </td>
                <td>
                    <asp:Button ID="Button5" runat="server" Text="登録" CssClass="Btn10" OnClick="Button5_Click" />
                </td>
                <td>
                    <asp:Button ID="Button7" runat="server" Text="複写" CssClass="Btn10" OnClick="Button7_Click" />
                    <script type="text/javascript">
                        function Copy() {
                            document.getElementById('Label94').innerHTML = "";
                            debugger;
                        }
                    </script>
                </td>
                <td>
                    <asp:Button ID="HatyuBtn" runat="server" Text="受注" CssClass="Btn10" OnClick="HatyuBtn_Click" />
                </td>
                <%--<td>
                    <asp:Button ID="BtnCsv" runat="server" Text="CSV" CssClass="Btn5" OnClick="BtnCsv_Click" />
                </td>--%>
                <td>
                    <asp:Button ID="DelBtn" runat="server" Text="削除" CssClass="Btn10" OnClick="DelBtn_Click" OnClientClick="A()" />
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
                    <asp:Button ID="Button8" runat="server" Text="一覧に戻る" CssClass="Btn10" OnClick="Button8_Click" />
                </td>

            </tr>
        </table>
        <table runat="server" id="SubMenu2">
            <tr>
                <td>
                    <asp:Button ID="Button6" runat="server" Text="戻る" CssClass="Btn10" OnClick="Button6_Click" />
                </td>
            </tr>
        </table>
        <br />




        <!-- ヘッダー部分---------------------------------------------------------------------------------->
        <asp:Label ID="Err" runat="server" Text="" ForeColor="Red"></asp:Label>
        <asp:Label ID="End" runat="server" Text="" ForeColor="Green"></asp:Label>

        <div id="header">
            <table runat="server" id="mInput">
                <tr>
                    <td class="InputTitle" runat="server">
                        <p>見積NO.</p>
                    </td>
                    <td class="waku">
                        <asp:Label ID="Label94" runat="server" Text="" Width="80px" Font-Size="12px"></asp:Label>
                    </td>
                    <td class="MiniTitle">
                        <p>カテゴリー</p>
                    </td>
                    <td class="waku">
                        <telerik:RadComboBox ID="RadComboCategory" runat="server" OnItemsRequested="RadComboCategory_ItemsRequested" AllowCustomText="True" EnableLoadOnDemand="True" MarkFirstMatch="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" OnSelectedIndexChanged="RadComboCategory_SelectedIndexChanged" Width="100px" AutoPostBack="true"></telerik:RadComboBox>
                        <input type="hidden" id="CategoryCode" runat="server" />
                    </td>
                    <td class="MiniTitle">
                        <p>部門</p>
                    </td>
                    <td class="waku">
                        <telerik:RadComboBox ID="RadComboBox4" runat="server"></telerik:RadComboBox>
                        <input type="hidden" id="BumonCode" runat="server" />
                        <input type="hidden" id="BumonKubun" runat="server" />
                    </td>
                    <td class="MiniTitle">
                        <p>担当者</p>
                    </td>
                    <td class="waku">
                        <asp:Label ID="Label1" runat="server" Text="担当者"></asp:Label>
                        <input type="hidden" id="UserID" runat="server" />
                    </td>
                    <td class="MiniTitle">
                        <p>照会日付</p>
                    </td>
                    <td class="waku">
                        <telerik:RadDatePicker ID="RadDatePicker1" runat="server" Width="100px"></telerik:RadDatePicker>
                    </td>
                    <td class="MiniTitle"></td>
                    <td class="waku"></td>
                    <td class="MiniTitle"></td>
                    <td class="waku"></td>

                </tr>
                <tr>
                    <td class="InputTitle" runat="server">
                        <p>得意先</p>
                    </td>
                    <td class="waku">
                        <asp:Button ID="Button1" runat="server" Text="得意先詳細" CssClass="Btn10" OnClick="Button1_Click" />
                    </td>
                    <td colspan="4" class="waku">
                        <telerik:RadComboBox ID="RadComboBox1" runat="server" Culture="ja-JP" OnItemsRequested="RadComboBox1_ItemsRequested" AllowCustomText="false" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" AutoPostBack="True" OnSelectedIndexChanged="RadComboBox1_SelectedIndexChanged" Width="300px"></telerik:RadComboBox>
                        <input type="hidden" id="TokuisakiCode" runat="server" />
                        <input type="hidden" id="CustomerCode" runat="server" />
                        <asp:TextBox ID="KariTokui" runat="server" Width="300px" AutoPostBack="true"></asp:TextBox>

                        <asp:Label runat="server" ID="LblAlert" Font-Size="Small" ForeColor="Red"></asp:Label>
                    </td>
                    <td class="MiniTitle">
                        <p>仮宛先</p>
                    </td>
                    <td class="waku">
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </td>
                    <td class="MiniTitle">
                        <p>見積日付</p>
                    </td>
                    <td class="waku">
                        <telerik:RadDatePicker ID="RadDatePicker2" runat="server" Width="100px"></telerik:RadDatePicker>
                    </td>

                    <td class="MiniTitle"></td>
                    <td class="waku"></td>
                    <td class="MiniChange">
                        <p>税区分</p>
                    </td>
                    <td class="waku">
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
                    <td class="InputTitle" runat="server">
                        <p>請求先</p>
                    </td>
                    <td class="auto-style5">
                        <asp:Button ID="Button2" runat="server" Text="請求先詳細" CssClass="Btn10" OnClick="Button2_Click" />
                    </td>
                    <td colspan="4" class="auto-style5">
                        <telerik:RadComboBox ID="RadComboBox3" runat="server" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" OnItemsRequested="RadComboBox3_ItemsRequested" Width="300px"></telerik:RadComboBox>
                        <asp:TextBox ID="kariSekyu" runat="server" Width="300px"></asp:TextBox>

                    </td>
                    <td class="MiniTitle">
                        <p>リレー状況</p>
                    </td>
                    <td class="waku">
                        <asp:TextBox ID="TextBox1" runat="server" Width="80px" Height="20px"></asp:TextBox>

                    </td>
                    <td class="MiniTitle"></td>
                    <td class="waku"></td>
                    <td class="MiniTitle">
                        <p>締め日</p>
                    </td>
                    <td class="waku">
                        <asp:Label ID="Shimebi" runat="server" Text=""></asp:Label>

                    </td>
                    <td class="MiniTitle">
                        <p>数量合計</p>
                    </td>
                    <td runat="server" class="sisan">
                        <asp:TextBox ID="TextBox10" runat="server" CssClass="Text" Width="80px" Height="20px" Text="0" TextMode="Number"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style9" runat="server">
                        <p>納品先</p>
                    </td>
                    <td class="auto-style10">
                        <asp:Button ID="Button3" runat="server" Text="納品先詳細" CssClass="Btn10" OnClick="Button3_Click" />
                    </td>
                    <td colspan="4" class="auto-style10">
                        <telerik:RadComboBox ID="RadComboBox2" runat="server" Culture="ja-JP" OnItemsRequested="RadComboBox2_ItemsRequested" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" Width="300px"></telerik:RadComboBox>
                        <input type="hidden" id="TyokusoCode" runat="server" />
                        <asp:TextBox ID="KariNouhin" runat="server" Width="300px"></asp:TextBox>

                    </td>
                    <td class="MiniTitle">
                        <p>掛率</p>
                    </td>
                    <td class="auto-style10">
                        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                        <asp:TextBox ID="TbxKake" runat="server" Width="80px" Height="20px" OnTextChanged="TbxKake_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td class="MiniTitle"></td>
                    <td class="auto-style10"></td>
                    <td class="MiniTitle">
                        <p>売上計</p>
                    </td>
                    <td runat="server" class="auto-style11">
                        <asp:TextBox ID="TextBox7" runat="server" Width="80px" Height="20px" CssClass="Text" AutoPostBack="true" Text="0"></asp:TextBox>
                        <input type="hidden" id="urikei" runat="server" />
                    </td>
                    <td class="MiniTitle">
                        <p>仕入計</p>
                    </td>
                    <td class="auto-style11">
                        <asp:TextBox ID="TextBox8" runat="server" Width="80px" Height="20px" CssClass="Text" AutoPostBack="true" Text="0"></asp:TextBox>
                        <input type="hidden" id="shikei" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="InputTitle" runat="server">
                        <p>使用施設名</p>
                    </td>
                    <td colspan="3" class="auto-style5">
                        <telerik:RadComboBox ID="FacilityRad" runat="server" Culture="ja-JP" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" AutoPostBack="True" Width="250px" OnSelectedIndexChanged="FacilityRad_SelectedIndexChanged" OnItemsRequested="FacilityRad_ItemsRequested"></telerik:RadComboBox>
                        <br />
                        <input type="hidden" id="FacilityAddress" runat="server" />
                        <asp:CheckBox ID="CheckBox5" runat="server" Text="複数" OnCheckedChanged="CheckBox5_CheckedChanged" AutoPostBack="True" />
                    </td>
                    <td class="MiniTitle">
                        <p>開始</p>
                    </td>
                    <td class="auto-style5">
                        <telerik:RadDatePicker ID="RadDatePicker3" runat="server" Width="100px" CssClass="CategoryChabge"></telerik:RadDatePicker>
                    </td>
                    <td class="MiniTitle">
                        <p>使用期間</p>
                    </td>
                    <td class="auto-style5">
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

                    <td class="MiniTitle">
                        <p>終了</p>
                    </td>
                    <td class="auto-style5">
                        <telerik:RadDatePicker ID="RadDatePicker4" runat="server" Width="100px" CssClass="CategoryChabge"></telerik:RadDatePicker>
                    </td>
                    <td class="MiniTitle">
                        <p>消費税額</p>
                    </td>
                    <td class="sisan">
                        <asp:TextBox ID="TextBox5" runat="server" Width="80px" Height="20px" CssClass="Text"></asp:TextBox>
                        <input type="hidden" runat="server" id="zeikei" />
                    </td>
                    <td class="MiniTitle">
                        <p>粗利計</p>
                    </td>
                    <td class="sisan">
                        <asp:TextBox ID="TextBox6" runat="server" Width="80px" Height="20px" CssClass="Text"></asp:TextBox>
                        <input type="hidden" runat="server" id="arakei" />
                    </td>


                </tr>
                <tr>
                    <td class="InputTitle" runat="server">
                        <p>備考</p>
                    </td>
                    <td colspan="9" class="waku">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                    <td class="MiniTitle">
                        <p>売上合計</p>
                    </td>

                    <td class="sisan">
                        <asp:TextBox ID="TextBox12" runat="server" Width="80px" Height="20px" CssClass="Text"></asp:TextBox>
                        <input type="hidden" runat="server" id="soukei" />
                    </td>
                    <td class="MiniTitle">
                        <p>利益率</p>
                    </td>
                    <td class="sisan">
                        <asp:TextBox ID="TextBox13" runat="server" Width="80px" Height="20px" CssClass="Text"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>

        <!-- ヘッダーテーブル -------------------------------------------------------------------------------------->

        <div>
            <img src="~/Img/明細ヘッダ見積.png" runat="server" width="1300" id="head" />
        </div>
        <table>
            <tr>
                <td>
                    <div>
                        <asp:DataGrid runat="server" ID="CtrlSyousai" AutoGenerateColumns="False" OnItemDataBound="CtrlSyousai_ItemDataBound" OnItemCommand="CtrlSyousai_ItemCommand">
                            <Columns>

                                <asp:TemplateColumn HeaderStyle-BorderStyle="None" ItemStyle-BorderStyle="None" ItemStyle-Width="30px">
                                    <ItemTemplate>
                                        <asp:Label ID="RowNo" runat="server" Text="" Font-Size="25px"></asp:Label>
                                        <asp:Button ID="Button4" runat="server" Text="削除" CssClass="Btn10" CommandName="Del" />
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
                </td>
            </tr>
        </table>
        <script type="text/javascript">
            function Keisan2(dg) {
                var dgc = document.getElementById(dg).Items.Count;
                var dgc2 = document.getElementById(dg).length
                //var ctrlsyousai = document.getElementById(ctrl).Items.Count;
                //var ctrlsyousai = document.getElementById(ctrl).length;
                debugger;
            }
        </script>

        <asp:Button ID="AddBtn" runat="server" Text="明細追加" CssClass="Btn10" OnClick="AddBtn_Click" />
        &nbsp;
        <asp:Button runat="server" ID="BtnCopyAdd" Text="明細複写" CssClass="Btn10" OnClick="BtnCopyAdd_Click" />
        <script>
            function AddFocus(Fcs) {
                var a = document.getElementById(Fcs);
                debugger;
                var y = a.scrollHeight - a.clientHeight;
                window.scroll(0, y);
                try {
                    var catecode = document.getElementById("RadComboCategory");
                    var val = catecode.value;
                    debugger;
                    switch (val) {
                        case "公共図書館":
                        case "学校図書館":
                        case "防衛省":
                        case "その他図書館":
                            debugger;
                            var classN = document.getElementsByClassName("CategoryChabge");
                            classN.css('display', 'none');
                            //$(".CategoryChabge").css('display', 'none');
                            //$(".Zasu").css('display', 'none');
                            debugger;
                            break;
                        case "上映会":
                            $(".CategoryChabge").css('display', '');
                            $(".Zasu").css('display', '');
                            debugger;
                            break;
                        default:
                            $(".CategoryChabge").css('display', '');
                            $(".Zasu").css('display', 'none');
                            debugger;
                    }
                }
                catch (e) {
                    var ex = e.value;
                    debugger;
                }
                debugger;
            }
        </script>
        &nbsp;
        <asp:Button ID="Button13" runat="server" Text="計算" CssClass="Btn10" OnClick="Keisan_Click" />

        <!--明細部分------------------------------------------------------------------------------------------------>

        <!-- 得意先詳細--------------------------------------------------------------------------------------->
        <asp:Table runat="server" ID="TBTokuisaki">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>顧客コード<span style="color: red">*</span></p>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="waku">
                    <asp:TextBox runat="server" ID="TbxCustomer"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>得意先コード<span style="color: red">*</span></p>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="waku">
                    <asp:TextBox runat="server" ID="TbxTokuisakiCode"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>得意先名<span style="color: red">*</span></p>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="waku">
                    <asp:TextBox runat="server" ID="TbxTokuisakiName"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>得意先フリガナ</p>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="waku">
                    <asp:TextBox runat="server" ID="TbxTokuisakiFurigana"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>得意先略称<span style="color: red">*</span></p>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="waku">
                    <asp:TextBox runat="server" ID="TbxTokuisakiRyakusyo"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                   <p>得意先担当者名</p>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="waku">
                    <asp:TextBox runat="server" ID="TbxTokuisakiStaff"></asp:TextBox>
                </asp:TableCell>

            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>郵便番号</p>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="6" CssClass="waku">
                    <asp:TextBox runat="server" ID="TbxTokuisakiPostNo" Width="400px"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>得意先住所</p>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="6" CssClass="waku">
                    <asp:TextBox runat="server" ID="TbxTokuisakiAddress" Width="400px"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>電話番号</p>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="waku">
                    <asp:TextBox runat="server" ID="TbxTokuisakiTEL"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                   <p>FAX</p>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="waku">
                    <asp:TextBox runat="server" ID="TbxTokuisakiFax"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>担当部署</p>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="waku">
                    <asp:TextBox runat="server" ID="TbxTokuisakiDepartment"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>税区分<span style="color: red">*</span></p>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="waku">
                    <telerik:RadComboBox runat="server" ID="RcbTax">
                        <Items>
                            <telerik:RadComboBoxItem Text="" Value="" />
                            <telerik:RadComboBoxItem Text="税込" Value="税込" />
                            <telerik:RadComboBoxItem Text="税抜" Value="税抜" />
                        </Items>
                    </telerik:RadComboBox>
                </asp:TableCell>

            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>掛率<span style="color: red">*</span></p>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="waku">
                    <asp:TextBox runat="server" ID="TbxKakeritsu"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>締日<span style="color: red">*</span></p>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="waku">
                    <telerik:RadComboBox runat="server" ID="RcbShimebi">
                        <Items>
                            <telerik:RadComboBoxItem Text="" Value="" />
                            <telerik:RadComboBoxItem Text="都度" Value="都度" />
                            <telerik:RadComboBoxItem Text="月末" Value="月末" />
                            <telerik:RadComboBoxItem Text="20日締め" Value="20" />
                        </Items>
                    </telerik:RadComboBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>担当スタッフNo.<span style="color: red">*</span></p>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="waku">
                    <asp:Label runat="server" ID="LblTantoStaffCode"></asp:Label>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>担当スタッフ名</p>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="waku">
                    <telerik:RadComboBox runat="server" ID="RadComboBox5" OnItemsRequested="RadComboBox5_ItemsRequested" EnableLoadOnDemand="true" AllowCustomText="true" OnSelectedIndexChanged="RadComboBox5_SelectedIndexChanged" AutoPostBack="true"></telerik:RadComboBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="4" CssClass="waku" HorizontalAlign="Center">
                    <asp:Button runat="server" ID="BtnToMaster" Text="得意先マスターに移動" OnClick="BtnToMaster_Click" Width="180px" CssClass="Btn10" />
                </asp:TableCell>
                <asp:TableCell ColumnSpan="4" CssClass="waku" HorizontalAlign="Center">
                    <asp:Button runat="server" ID="BtnTouroku" Text="見積ヘッダーに適応" OnClick="BtnTouroku_Click" CssClass="Btn10" Width="180px" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

        <!------------納入先詳細--------------------------------------------------------------------------->
        <asp:Panel runat="server" ID="NouhinsakiPanel">
            <div>
                <table id="NouhinsakiSyousai">
                    <tr>
                        <td colspan="2" class="MiniTitle">
                            <p>納品先コード</p>
                        </td>
                        <td colspan="2" class="waku">
                            <asp:TextBox ID="TextBox27" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="2" class="MiniTitle">
                            <p>納品先名</p>
                        </td>
                        <td colspan="2" class="waku">
                            <asp:TextBox ID="TextBox28" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="2" class="MiniTitle">
                            <p>郵便番号</p>
                        </td>
                        <td colspan="2" class="waku">
                            <asp:TextBox ID="TextBox35" runat="server"></asp:TextBox>
                        </td>

                    </tr>

                    <tr>
                        <td colspan="2" class="MiniTitle">
                            <p>住所</p>
                        </td>
                        <td colspan="2" class="waku">
                            <asp:TextBox ID="TextBox29" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="MiniTitle">
                            <p>電話番号</p>
                        </td>
                        <td class="waku">
                            <asp:TextBox ID="TextBox30" runat="server"></asp:TextBox>
                        </td>
                        <td class="MiniTitle">
                            <p>FAX番号</p>
                        </td>
                        <td class="waku">
                            <asp:TextBox ID="TextBox31" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="2" class="MiniTitle">
                            <p>納品先担当者名</p>
                        </td>
                        <td colspan="2" class="waku">
                            <asp:TextBox ID="TextBox32" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="2" class="MiniTitle">
                            <p>部署</p>
                        </td>
                        <td colspan="2" class="waku">
                            <asp:TextBox ID="TextBox33" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="4" class="waku">
                            <asp:Button ID="Button11" runat="server" Text="直送先マスタに移動" CssClass="Btn10" Width="150px" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <!-- 商品詳細-------------------------------------------------------------------------------------------------->
        <script src="JavaScript.js"></script>

    </form>
</body>
</html>
