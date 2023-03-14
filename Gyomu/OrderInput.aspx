<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderInput.aspx.cs" Inherits="Gyomu.OrderInput" %>

<%@ Register Src="~/CtrlMitsuSyousai.ascx" TagName="Syosai" TagPrefix="uc2" %>
<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="ja">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="https://code.jquery.com/jquery-3.1.0.min.js"></script>
    <script src="JavaScript.js"></script>
    <link href="Kaihatsu.css" type="text/css" rel="stylesheet" />
    <title>受注明細</title>
    <style type="text/css">
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

        .Btn11 {
            text-align: center;
            width: 95px;
            display: inline-block;
            padding: 0.3em 1em;
            text-decoration: none;
            color: #5d5d5d;
            border: solid 2px #ffd900;
            border-radius: 3px;
            transition: .4s;
            font-family: Meiryo;
            background-color: #ffef93;
        }

            .Btn11:hover {
                background: #ffd900;
                color: black;
            }

        .InputTitle {
            background-color: #ffd900;
            text-align: center;
            font-size: 12px;
            font: bold;
            color: #5d5d5d;
            width: 80px;
            height: 30px;
        }

        .MiniTitle {
            background-color: #fff1a0;
            text-align: center;
            font-size: 12px;
            font: bold;
            color: #5d5d5d;
            width: 100px;
            height: 20px;
        }

        #mInput2 {
            margin: 0;
            padding: 0;
            width: 100%;
            border: 2px solid #ffd900;
        }

        .sisan {
            background-color: #fff8d0;
        }

        .waku {
            background-color: #e6e6e6;
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

        #CtrlSyousai {
            width: 100%;
        }

        #DivDataGrid {
            height: 500px;
            overflow: scroll;
            width: 100%;
        }

        .gv a {
            background-color: white;
            text-decoration: none;
            color: darkgreen;
            font-size: 15px;
        }

            .gv a:link {
                background-color: white;
                text-decoration: none;
                color: #ffd900;
                font-weight: bold;
                font-size: 15px;
            }

            .gv a:visited {
                background-color: darkgreen;
                text-decoration: none;
                color: #ffd900;
                font-weight: bold;
                font-size: 15px;
            }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <uc:Menu ID="Menu" runat="server" />

        <telerik:RadTabStrip ID="RT" runat="server" AutoPostBack="True" SelectedIndex="1" BackColor="#ffef93" ForeColor="#5d5d5d">
            <Tabs>
                <telerik:RadTab Text="受注一覧" Font-Size="12pt" NavigateUrl="Jutyu/JutyuJoho.aspx" ForeColor="#5d5d5d">
                </telerik:RadTab>
                <telerik:RadTab Text="受注入力" Font-Size="12pt" NavigateUrl="Jutyu/JutyuInput.aspx" Selected="True" ForeColor="#5d5d5d">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <table>
            <tr>
                <td style="height: 5px;"></td>
            </tr>
        </table>
        <table id="SubMenu" runat="server">
            <tr>
                <td>
                    <asp:Button ID="Keisan" runat="server" Text="計算" CssClass="Btn11" OnClick="Keisan_Click" />
                </td>
                <td>
                    <asp:Button ID="Register" runat="server" Text="登録" CssClass="Btn11" OnClick="Button5_Click" OnClientClick="return Check(this)" />

                </td>
                <td>
                    <asp:Button ID="DelBtn" runat="server" Text="削除" CssClass="Btn11" OnClick="DelBtn_Click" OnClientClick="A()" />
                    <script type="text/javascript"></script>

                </td>
                <td>
                    <asp:Button ID="HatyuBtn" runat="server" Text="発注" CssClass="Btn11" OnClick="HatyuBtn_Click" />
                </td>
                <td>
                    <asp:Button ID="Back" runat="server" Text="一覧に戻る" CssClass="Btn11" OnClick="Back_Click" />
                </td>

            </tr>
        </table>
        <table runat="server" id="SubMenu2">
            <tr>
                <td>
                    <asp:Button ID="Button6" runat="server" Text="戻る" CssClass="Btn11" OnClick="Button6_Click" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="height: 5px;"></td>
            </tr>
        </table>
        <asp:Label ID="Err" runat="server" Text="" ForeColor="Red"></asp:Label>
        <asp:Label ID="End" runat="server" Text="" ForeColor="Green"></asp:Label>
        <div id="header">
            <table runat="server" id="mInput2">
                <tr>
                    <td runat="server" style="background-color: #ffd900; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 80px; height: 30px;">
                        <p>受注NO.</p>
                    </td>
                    <td class="waku">
                        <asp:Label ID="JutyuNo" runat="server" Text="" Width="80px" Font-Size="12px"></asp:Label>
                    </td>
                    <td style="background-color: #fff1a0; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 100px; height: 20px;">
                        <p>カテゴリー</p>
                    </td>
                    <td class="waku">
                        <telerik:RadComboBox ID="RadComboCategory" runat="server" OnItemsRequested="RadComboCategory_ItemsRequested" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" OnSelectedIndexChanged="RadComboCategory_SelectedIndexChanged" AutoPostBack="True" Width="100px"></telerik:RadComboBox>
                        <input type="hidden" id="CategoryCode" runat="server" />
                    </td>
                    <td style="background-color: #fff1a0; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 100px; height: 20px;">
                        <p>部門</p>
                    </td>
                    <td class="waku">
                        <telerik:RadComboBox ID="RadComboBox4" runat="server"></telerik:RadComboBox>
                        <input type="hidden" id="BumonCode" runat="server" />
                        <input type="hidden" id="BumonKubun" runat="server" />
                    </td>
                    <td style="background-color: #fff1a0; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 100px; height: 20px;">
                        <p>担当者</p>
                    </td>
                    <td class="waku" style="width: 90px">
                        <asp:Label ID="Tantou" runat="server" Text="担当者"></asp:Label>
                        <input type="hidden" id="UserID" runat="server" />
                    </td>
                    <td style="background-color: #fff1a0; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 100px; height: 20px;">
                        <p>照会日付</p>
                    </td>
                    <td class="waku">
                        <telerik:RadDatePicker ID="RadDatePicker1" runat="server" Width="100px"></telerik:RadDatePicker>
                    </td>
                    <td style="background-color: #fff1a0; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 100px; height: 20px;"></td>
                    <td class="waku"></td>
                    <td style="background-color: #fff1a0; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 100px; height: 20px;"></td>
                    <td class="waku"></td>

                </tr>
                <tr>
                    <td style="background-color: #ffd900; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 80px; height: 30px;" runat="server">
                        <p>得意先</p>
                    </td>
                    <td class="waku">
                        <asp:Button ID="Button1" runat="server" Text="詳細" CssClass="Btn11" />
                        <script type="text/javascript"></script>
                        <script type="text/javascript"></script>
                    </td>
                    <td colspan="4" class="waku">
                        <telerik:RadComboBox OnClientSelectedIndexChanged="TokuisakiSelect" ID="RadComboBox1" runat="server" Culture="ja-JP" OnItemsRequested="RadComboBox1_ItemsRequested" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" AutoPostBack="True" OnSelectedIndexChanged="RadComboBox1_SelectedIndexChanged" Width="300px"></telerik:RadComboBox>
                        <script type="text/javascript"><%=RadComboBox1.ClientID%><%=JutyuNo.ClientID%><%=LblTantoStaffCode.ClientID%><%=HidTantoStaffCode.ClientID%><%=HidKakeritsu.ClientID%><%=UriageGokei.ClientID%></script>
                        <input type="hidden" id="TokuisakiCode" runat="server" />
                        <input type="hidden" id="TokuisakiMei" runat="server" />
                        <input type="hidden" id="CustomerCode" runat="server" />

                        <asp:TextBox ID="KariTokui" runat="server" Width="300px"></asp:TextBox>
                    </td>
                    <td style="background-color: #fff1a0; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 100px; height: 20px;">
                        <p>仮宛先</p>
                    </td>
                    <td class="waku">
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </td>
                    <td style="background-color: #fff1a0; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 100px; height: 20px;">
                        <p>受注日付</p>
                    </td>
                    <td class="waku">
                        <telerik:RadDatePicker ID="RadDatePicker2" runat="server" Width="100px"></telerik:RadDatePicker>
                    </td>

                    <td style="background-color: #fff1a0; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 100px; height: 20px;"></td>
                    <td class="waku"></td>
                    <td class="MiniChange2" style="background-color: #fff1a0; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 100px; height: 20px;">
                        <p>税区分</p>
                    </td>
                    <td class="waku">
                        <telerik:RadComboBox ID="RadZeiKubun" runat="server" Width="70px">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Value="" />
                                <telerik:RadComboBoxItem runat="server" Text="税込" Value="税込" />
                                <telerik:RadComboBoxItem runat="server" Text="税抜" Value="税抜" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #ffd900; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 80px; height: 30px;" runat="server">
                        <p>請求先</p>
                    </td>
                    <td class="waku">
                        <%--                        <asp:Button ID="Button2" runat="server" Text="請求先詳細" CssClass="Btn11" OnClick="Button2_Click" />--%>
                    </td>
                    <td colspan="4" class="waku">
                        <telerik:RadComboBox ID="RadComboBox3" OnClientSelectedIndexChanged="SeikyusakiSelected" runat="server" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" OnItemsRequested="RadComboBox3_ItemsRequested" Width="300px"></telerik:RadComboBox>
                        <asp:TextBox ID="kariSekyu" runat="server" Width="300px"></asp:TextBox>

                    </td>
                    <td style="background-color: #fff1a0; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 100px; height: 20px;">
                        <p>リレー状況</p>
                    </td>
                    <td class="waku">
                        <asp:Label ID="Relay" runat="server" Text="見積"></asp:Label>
                    </td>
                    <td style="background-color: #fff1a0; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 100px; height: 20px;">
                        <p></p>
                    </td>
                    <td class="waku"></td>
                    <td style="background-color: #fff1a0; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 100px; height: 20px;">
                        <p>締め日</p>
                    </td>
                    <td class="waku">
                        <asp:Label ID="Shimebi" runat="server" Text=""></asp:Label>
                        <asp:HiddenField runat="server" ID="HidShimebi" />
                    </td>
                    <td style="background-color: #fff1a0; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 100px; height: 20px;">
                        <p>数量合計</p>
                    </td>
                    <td style="background-color: #fff8d0;">
                        <asp:TextBox ID="TextBox10" runat="server" CssClass="Text" Width="80px" Height="20px" Text="0" TextMode="Number"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td style="background-color: #ffd900; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 80px; height: 30px;" runat="server">
                        <p>納品先</p>
                    </td>
                    <td class="waku">
                        <%--                        <asp:Button ID="Button3" runat="server" Text="納品先詳細" CssClass="Btn11" OnClick="Button3_Click" />--%>
                    </td>
                    <td colspan="4" class="waku">
                        <telerik:RadComboBox ID="RadComboBox2" runat="server" Culture="ja-JP" OnClientSelectedIndexChanged="Nouhinsaki2" OnItemsRequested="RadComboBox2_ItemsRequested" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" Width="300px"></telerik:RadComboBox>
                        <input type="hidden" id="TyokusoCode" runat="server" />
                        <input type="hidden" runat="server" id="TyokusoJusyo" />
                        <asp:TextBox ID="KariNouhin" runat="server" Width="300px"></asp:TextBox>

                    </td>
                    <td class="MiniChange2" style="background-color: #fff1a0; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 100px; height: 20px;">
                        <p>掛率</p>
                    </td>
                    <td class="waku">
                        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                        <asp:HiddenField runat="server" ID="HidKakeritsu" />
                        <asp:TextBox ID="TbxKake" runat="server" Width="80px" Height="20"></asp:TextBox>

                    </td>
                    <td style="background-color: #fff1a0; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 100px; height: 20px;"></td>
                    <td class="waku"></td>
                    <td style="background-color: #fff1a0; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 100px; height: 20px;">
                        <p>売上計</p>
                    </td>
                    <td style="background-color: #fff8d0;">
                        <asp:TextBox ID="Uriagekeijyou" runat="server" Width="80px" Height="20px" CssClass="Text" AutoPostBack="true" Text="0"></asp:TextBox>
                        <input type="hidden" id="urikei" runat="server" />
                    </td>
                    <td style="background-color: #fff1a0; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 100px; height: 20px;">
                        <p>仕入計</p>
                    </td>
                    <td style="background-color: #fff8d0;">
                        <asp:TextBox ID="Shiirekei" runat="server" Width="80px" Height="20px" CssClass="Text" AutoPostBack="true" Text="0"></asp:TextBox>
                        <input type="hidden" id="shikei" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #ffd900; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 80px; height: 30px;" runat="server">
                        <p>使用施設名</p>
                    </td>
                    <td colspan="3" class="waku">
                        <telerik:RadComboBox ID="FacilityRad" runat="server" Culture="ja-JP" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" AutoPostBack="True" Width="250px" OnSelectedIndexChanged="FacilityRad_SelectedIndexChanged" OnItemsRequested="FacilityRad_ItemsRequested"></telerik:RadComboBox>
                        <br />
                        <input type="hidden" id="FacilityAddress" runat="server" />
                        <asp:CheckBox ID="CheckBox5" runat="server" Text="複数" OnCheckedChanged="CheckBox5_CheckedChanged" AutoPostBack="True" />
                    </td>
                    <td style="background-color: #fff1a0; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 100px; height: 20px;">
                        <p>開始</p>
                    </td>
                    <td class="waku">
                        <telerik:RadDatePicker ID="RadDatePicker3" runat="server" Width="100px" CssClass="CategoryChabge"></telerik:RadDatePicker>
                    </td>
                    <td style="background-color: #fff1a0; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 100px; height: 20px;">
                        <p>使用期間</p>
                    </td>
                    <td class="waku">
                        <asp:DropDownList ID="DropDownList9" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList9_SelectedIndexChanged" Width="70px" CssClass="CategoryChabge">
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
                        <asp:CheckBox ID="CheckBox4" runat="server" Text="複数" AutoPostBack="True" OnCheckedChanged="CheckBox4_CheckedChanged" />
                    </td>

                    <td style="background-color: #fff1a0; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 100px; height: 20px;">
                        <p>終了</p>
                    </td>
                    <td class="waku">
                        <telerik:RadDatePicker ID="RadDatePicker4" runat="server" Width="100px" CssClass="CategoryChabge"></telerik:RadDatePicker>
                    </td>
                    <td style="background-color: #fff1a0; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 100px; height: 20px;">
                        <p>消費税額</p>
                    </td>
                    <td style="background-color: #fff8d0;">
                        <asp:TextBox ID="Zei" runat="server" Width="80px" Height="20px" CssClass="Text"></asp:TextBox>
                        <input type="hidden" runat="server" id="zeikei" />
                    </td>
                    <td style="background-color: #fff1a0; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 100px; height: 20px;">
                        <p>粗利計</p>
                    </td>
                    <td style="background-color: #fff8d0;">
                        <asp:TextBox ID="TextBox6" runat="server" Width="80px" Height="20px" CssClass="Text"></asp:TextBox>
                        <input type="hidden" runat="server" id="arakei" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #ffd900; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 80px; height: 30px;" runat="server">
                        <p>備考</p>
                    </td>
                    <td colspan="5" class="waku">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                    <td class="MiniTitle">
                        <p>希望納期</p>
                    </td>
                    <td colspan="3" class="waku">
                        <asp:TextBox runat="server" ID="TbxKibouNouki" Width="200px"></asp:TextBox>
                    </td>

                    <td style="background-color: #fff1a0; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 100px; height: 20px;">
                        <p>売上合計</p>
                    </td>

                    <td style="background-color: #fff8d0;">
                        <asp:TextBox ID="UriageGokei" runat="server" Width="80px" Height="20px" CssClass="Text"></asp:TextBox>
                        <input type="hidden" runat="server" id="soukei" />
                    </td>
                    <td style="background-color: #fff1a0; text-align: center; font-size: 12px; font: bold; color: #5d5d5d; width: 100px; height: 20px;">
                        <p>利益率</p>
                    </td>
                    <td style="background-color: #fff8d0;">
                        <asp:TextBox ID="TextBox13" runat="server" Width="80px" Height="20px" CssClass="Text"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div runat="server" style="width: 100%;">
            <table runat="server" style="text-align: center; width: 100%; border-collapse: collapse; background-color: white; border: 2px solid #ffd900" id="head">
                <tr>
                    <td rowspan="3" style="text-align: center; background-color: #ffef93; width: 90px;">
                        <table>
                            <tr>
                                <td>
                                    <p>削除</p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>明細挿入</p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>明細複写</p>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table style="width: 100%; border-collapse: collapse; background-color: #e6e6e6;">
                            <tr>
                                <td style="width: 120px; border: 2px solid white;">
                                    <p>メーカー品番</p>
                                </td>
                                <td style="width: 50%; border: 2px solid white;">
                                    <p>商品名</p>
                                </td>
                                <td style="border: 2px solid white;">
                                    <p>使用範囲</p>
                                </td>
                                <td style="border: 2px solid white;">
                                    <p>媒体</p>
                                </td>
                                <td style="border: 2px solid white;">
                                    <p>数量</p>
                                </td>
                                <td style="width: 100px; border: 2px solid white;">
                                    <p>標準単価</p>
                                </td>
                                <td style="width: 100px; border: 2px solid white;">
                                    <p>金額</p>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%; border-collapse: collapse; background-color: #e6e6e6;">
                            <tr>
                                <td style="width: 50%; border: 2px solid white;">
                                    <p>使用範囲</p>
                                </td>
                                <td style="border: 2px solid white;">
                                    <p>席数</p>
                                </td>
                                <td style="border: 2px solid white;">
                                    <p>使用開始</p>
                                </td>
                                <td style="border: 2px solid white;">
                                    <p>使用終了</p>
                                </td>
                                <td style="border: 2px solid white;">
                                    <table>
                                        <tr>
                                            <td>
                                                <p>掛率</p>
                                            </td>
                                            <td>
                                                <p>税区分</p>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 100px; border: 2px solid white;">
                                    <p>単価</p>
                                </td>
                                <td style="width: 100px; border: 2px solid white;">
                                    <p>売上金額</p>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%; border-collapse: collapse; background-color: #e6e6e6;">
                            <tr>
                                <td style="width: 300px; border: 2px solid white;">
                                    <p>摘要</p>
                                </td>
                                <td style="border: 2px solid white;">
                                    <p>発注先</p>
                                </td>
                                <td style="border: 2px solid white;">
                                    <p>倉庫</p>
                                </td>
                                <td style="width: 100px; border: 2px solid white;">
                                    <p>仕入単価</p>
                                </td>
                                <td style="width: 100px; border: 2px solid white;">
                                    <p>仕入金額</p>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <table runat="server" id="TBAddRow">
            <tr>
                <td>
                    <p>追加行：</p>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="TbxAddRow" Text="1" TextMode="Number" Width="25px"></asp:TextBox>
                </td>
                <td>行</td>
                <td>
                    <asp:Button runat="server" ID="BtnTool6" ToolTip="「明細追加」や「明細複写」で追加する行数を選択する。" Text="❔" OnClientClick="return false;" />
                </td>
            </tr>
        </table>
        <asp:HiddenField runat="server" ID="HidNewRow" />
        <asp:HiddenField runat="server" ID="HidSyokaiDate" />

        <table style="width: 100%">

            <!-- ヘッダーテーブル -------------------------------------------------------------------------------------->
            <tr>
                <td>
                    <div runat="server" id="DivDataGrid">
                        <asp:DataGrid runat="server" ID="CtrlSyousai" PageSize="10" AllowPaging="true" AutoGenerateColumns="False" OnItemDataBound="CtrlSyousai_ItemDataBound" OnItemCommand="CtrlSyousai_ItemCommand" OnPageIndexChanged="CtrlSyousai_PageIndexChanged">
                            <PagerStyle Position="TopAndBottom" Mode="NumericPages" CssClass="gv" />
                            <Columns>
                                <asp:TemplateColumn HeaderStyle-BorderStyle="None" ItemStyle-BorderStyle="None" ItemStyle-Width="30px">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="RowNo" runat="server" Text="" Font-Size="25px"></asp:Label>
                                        <asp:Button ID="Button4" runat="server" Text="削除" CssClass="Btn11" CommandName="Del" />
                                        <asp:Button ID="BtnAddRow" runat="server" Text="明細挿入" CssClass="Btn11" CommandName="Add" />
                                        <asp:Button runat="server" ID="BtnCopyAdd" Text="明細複写" CssClass="Btn11" CommandName="Copy" />
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
        <script></script>

        &nbsp;
        <asp:Button ID="Button13" runat="server" Text="計算" CssClass="Btn11" OnClick="Keisan_Click" OnClientClick="Cul();" />
        <script type="text/javascript"></script>

        <!--明細部分------------------------------------------------------------------------------------------------>


        <!-- 得意先詳細--------------------------------------------------------------------------------------->
        <asp:Table runat="server" ID="TBSyousais">
            <asp:TableRow>
                <asp:TableCell>
                    <table>
                        <tr>
                            <td style="border-bottom: 2px solid #ffd900">
                                <p>得意先詳細</p>
                            </td>
                            <td style="border-bottom: 2px solid #ffd900">
                                <p>請求先詳細</p>
                            </td>
                            <td style="border-bottom: 2px solid #ffd900">
                                <p>納品先詳細</p>
                            </td>
                            <td style="border-bottom: 2px solid #ffd900">
                                <p>使用施設詳細</p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Table runat="server" ID="TBTokuisaki">
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>顧客コード<span style="color: red">*</span></p>
                                        </asp:TableCell>
                                        <asp:TableCell ColumnSpan="2" CssClass="waku">
                                            <asp:TextBox runat="server" ID="TbxCustomer"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
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
                                            <telerik:RadComboBox ID="RcbTokuisakiNameSyousai" runat="server" Culture="ja-JP" OnItemsRequested="RadComboBox1_ItemsRequested" AllowCustomText="false" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" Width="300px" OnClientSelectedIndexChanged="TokuisakiSelect"></telerik:RadComboBox>
                                            <p style="font-size: 10px">得意先として印刷時に反映されます。</p>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
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
                                            <asp:TextBox runat="server" ID="TbxTokuisakiRyakusyo"></asp:TextBox><br />
                                            <p style="font-size: 10px">得意先として画面上に反映されます。</p>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
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
                                        <asp:TableCell ColumnSpan="2" CssClass="waku">
                                            <asp:TextBox runat="server" ID="TbxTokuisakiPostNo"></asp:TextBox>
                                            <asp:Button runat="server" ID="BtnPostNoSerch4" CssClass="Btn11" Text="郵便番号検索" OnClick="BtnPostNoSerch4_Click" />

                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>得意先住所</p>
                                        </asp:TableCell>
                                        <asp:TableCell ColumnSpan="2" CssClass="waku">
                                            <asp:TextBox runat="server" ID="TbxTokuisakiAddress" Width="200px"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>得意先住所2</p>
                                        </asp:TableCell>
                                        <asp:TableCell ColumnSpan="2" CssClass="waku">
                                            <asp:TextBox runat="server" ID="TbxTokuisakiAddress1" Width="200px"></asp:TextBox>
                                        </asp:TableCell>

                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>電話番号</p>
                                        </asp:TableCell>
                                        <asp:TableCell ColumnSpan="2" CssClass="waku">
                                            <asp:TextBox runat="server" ID="TbxTokuisakiTEL"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="3">
                                            <asp:Button runat="server" ID="BtnCopy1" Text="郵便番号、住所、電話番号をコピーする" OnClick="BtnCopy1_Click" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
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
                                    </asp:TableRow>
                                    <asp:TableRow>
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
                                    </asp:TableRow>
                                    <asp:TableRow>
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
                                        <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>担当スタッフ名</p>
                                        </asp:TableCell>
                                        <asp:TableCell ColumnSpan="2" CssClass="waku">
                                            <telerik:RadComboBox runat="server" ID="RadComboBox5" OnItemsRequested="RadComboBox5_ItemsRequested" EnableLoadOnDemand="true" AllowCustomText="true" OnSelectedIndexChanged="RadComboBox5_SelectedIndexChanged"></telerik:RadComboBox>
                                        </asp:TableCell>

                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>担当スタッフNo.<span style="color: red">*</span></p>
                                        </asp:TableCell>
                                        <asp:TableCell ColumnSpan="2" CssClass="waku">
                                            <asp:Label runat="server" ID="LblTantoStaffCode"></asp:Label>
                                            <asp:HiddenField runat="server" ID="HidTantoStaffCode" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="4">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button runat="server" ID="BtnToMaster" Text="得意先マスターに移動" OnClick="BtnToMaster_Click" Width="180px" CssClass="Btn11" />
                                                    </td>
                                                    <td>
                                                        <%--                                                        <asp:Button runat="server" ID="BtnTouroku" Text="見積ヘッダーに適応" OnClick="BtnTouroku_Click" CssClass="Btn10" Width="180px" />--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </td>
                            <td style="vertical-align: top">
                                <table>
                                    <tr>
                                        <td style="vertical-align: top">
                                            <asp:Table runat="server" ID="TBSeikyusaki" CaptionAlign="Top">
                                                <asp:TableRow>
                                                    <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>顧客コード<span style="color: red">*</span></p>
                                                    </asp:TableCell>
                                                    <asp:TableCell ColumnSpan="2" CssClass="waku">
                                                        <asp:TextBox runat="server" ID="TbxCustomerCode2"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>得意先コード<span style="color: red">*</span></p>
                                                    </asp:TableCell>
                                                    <asp:TableCell ColumnSpan="2" CssClass="waku">
                                                        <asp:TextBox runat="server" ID="TbxTokuisakiCode2"></asp:TextBox>
                                                    </asp:TableCell>

                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>請求先名<span style="color: red">*</span></p>
                                                    </asp:TableCell>
                                                    <asp:TableCell ColumnSpan="2" CssClass="waku">
                                                        <telerik:RadComboBox ID="RcbSeikyusaki" runat="server" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" OnItemsRequested="RadComboBox3_ItemsRequested" Width="300px" OnClientSelectedIndexChanged="SeikyusakiSelected"></telerik:RadComboBox>
                                                        <p style="font-size: 10px">請求先として印刷時に反映されます。</p>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>請求先フリガナ</p>
                                                    </asp:TableCell>
                                                    <asp:TableCell ColumnSpan="2" CssClass="waku">
                                                        <asp:TextBox runat="server" ID="TbxTokuisakiFurigana2"></asp:TextBox>
                                                    </asp:TableCell>

                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>請求先略称<span style="color: red">*</span></p>
                                                    </asp:TableCell>
                                                    <asp:TableCell ColumnSpan="2" CssClass="waku">
                                                        <asp:TextBox runat="server" ID="TbxTokuisakiRyakusyou2"></asp:TextBox><br />
                                                        <p style="font-size: 10px">請求先として画面上に反映されます。</p>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                   <p>請求先担当者名</p>
                                                    </asp:TableCell>
                                                    <asp:TableCell ColumnSpan="2" CssClass="waku">
                                                        <asp:TextBox runat="server" ID="TbxTokuisakiStaff2"></asp:TextBox>
                                                    </asp:TableCell>

                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>郵便番号</p>
                                                    </asp:TableCell>
                                                    <asp:TableCell ColumnSpan="2" CssClass="waku">
                                                        <asp:TextBox runat="server" ID="TbxPostNo2"></asp:TextBox>
                                                        <asp:Button runat="server" ID="BtnPostNoSerch3" CssClass="Btn11" Text="郵便番号検索" OnClick="BtnPostNoSerch3_Click" />

                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>請求先住所</p>
                                                    </asp:TableCell>
                                                    <asp:TableCell ColumnSpan="2" CssClass="waku">
                                                        <asp:TextBox runat="server" ID="TbxTokuisakiAddress2" Width="200px"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>請求先住所2</p>
                                                    </asp:TableCell>
                                                    <asp:TableCell ColumnSpan="2" CssClass="waku">
                                                        <asp:TextBox runat="server" ID="TbxTokuisakiAddress3" Width="200px"></asp:TextBox>
                                                    </asp:TableCell>

                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>電話番号</p>
                                                    </asp:TableCell>
                                                    <asp:TableCell ColumnSpan="2" CssClass="waku">
                                                        <asp:TextBox runat="server" ID="TbxTel2"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell ColumnSpan="3">
                                                        <asp:Button runat="server" ID="BtnCopy2" Text="郵便番号、住所、電話番号をコピーする" OnClick="BtnCopy2_Click" />
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                   <p>FAX</p>
                                                    </asp:TableCell>
                                                    <asp:TableCell ColumnSpan="2" CssClass="waku">
                                                        <asp:TextBox runat="server" ID="TbxFAX2"></asp:TextBox>
                                                    </asp:TableCell>

                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>担当部署</p>
                                                    </asp:TableCell>
                                                    <asp:TableCell ColumnSpan="2" CssClass="waku">
                                                        <asp:TextBox runat="server" ID="TbxDepartment2"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>税区分<span style="color: red">*</span></p>
                                                    </asp:TableCell>
                                                    <asp:TableCell ColumnSpan="2" CssClass="waku">
                                                        <telerik:RadComboBox runat="server" ID="RcbZeikubun2">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="" Value="" />
                                                                <telerik:RadComboBoxItem Text="税込" Value="税込" />
                                                                <telerik:RadComboBoxItem Text="税抜" Value="税抜" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <%--                <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>掛率<span style="color: red">*</span></p>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="waku">
                    <asp:TextBox runat="server" ID="TbxKakeritsu2"></asp:TextBox>
                </asp:TableCell>--%>
                                                    <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>締日<span style="color: red">*</span></p>
                                                    </asp:TableCell>
                                                    <asp:TableCell ColumnSpan="2" CssClass="waku">
                                                        <telerik:RadComboBox runat="server" ID="RadShimebi2">
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
                                                    <asp:TableCell ColumnSpan="4" CssClass="waku">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Button runat="server" ID="BtnToMasterS" Text="得意先マスターに移動" OnClick="BtnToMasterS_Click" Width="180px" CssClass="Btn11" />
                                                                </td>
                                                                <td>
                                                                    <%--                                                                    <asp:Button runat="server" ID="BtnSekyu" Text="見積ヘッダーに適応" OnClick="BtnSekyu_Click" CssClass="Btn10" Width="180px" />--%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <%--            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>担当スタッフNo.<span style="color: red">*</span></p>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="waku">
                    <asp:Label runat="server" ID="LblTantoStaffNo2"></asp:Label>
                    <asp:HiddenField runat="server" ID="HidTantoStaffCode2" />
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>担当スタッフ名</p>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="waku">
                    <telerik:RadComboBox runat="server" ID="RcbStaffName" OnItemsRequested="RadComboBox5_ItemsRequested" EnableLoadOnDemand="true" AllowCustomText="true" AutoPostBack="true" OnSelectedIndexChanged="RadComboBox5_SelectedIndexChanged"></telerik:RadComboBox>
                </asp:TableCell>
            </asp:TableRow>--%>
                                            </asp:Table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="vertical-align: top">
                                <table runat="server" id="TBNouhinsaki">
                                    <tr>
                                        <td runat="server" class="MiniTitle">
                                            <p>施設No.<span style="color: red">*</span></p>
                                        </td>
                                        <td runat="server" class="waku">
                                            <asp:TextBox runat="server" ID="TbxNouhinSisetsu"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td runat="server" class="MiniTitle">
                                            <p>施設行コード</p>
                                        </td>
                                        <td runat="server" class="waku">
                                            <asp:TextBox runat="server" ID="TbxCode"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td runat="server" class="MiniTitle">
                                            <p>直送先名</p>
                                        </td>
                                        <td class="waku">
                                            <telerik:RadComboBox runat="server" ID="RcbNouhinsakiMei" OnItemsRequested="RcbNouhinsakiMei_ItemsRequested" AllowCustomText="false" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" OnClientSelectedIndexChanged="Nouhinsaki2"></telerik:RadComboBox>
                                            <br />
                                            <p style="font-size: 10px">納品先として印刷時に反映されます。</p>
                                            <script type="text/javascript"><%=RcbNouhinsakiMei.ClientID%><%=RadComboBox2.ClientID%></script>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td runat="server" class="MiniTitle">
                                            <p>直送先名2</p>
                                        </td>
                                        <td class="waku">
                                            <asp:TextBox runat="server" ID="TbxNouhinTyokusousakiMei2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td runat="server" class="MiniTitle">
                                            <p>略称</p>
                                        </td>
                                        <td class="waku">
                                            <asp:TextBox runat="server" ID="TbxNouhinsakiRyakusyou"></asp:TextBox><br />
                                            <p style="font-size: 10px">納品先として画面上に反映されます。</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td runat="server" class="MiniTitle">
                                            <p>担当</p>
                                        </td>
                                        <td class="waku">
                                            <asp:TextBox runat="server" ID="TbxNouhinsakiTanto"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td runat="server" class="MiniTitle">
                                            <p>郵便番号</p>
                                        </td>
                                        <td class="waku">
                                            <asp:TextBox runat="server" ID="TbxNouhinsakiYubin"></asp:TextBox>
                                            <asp:Button runat="server" ID="BtnPostNoSerch2" CssClass="Btn11" Text="郵便番号検索" OnClick="BtnPostNoSerch2_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td runat="server" class="MiniTitle">
                                            <p>住所1</p>
                                        </td>
                                        <td class="waku">
                                            <asp:TextBox runat="server" ID="TbxNouhinsakiAddress"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td runat="server" class="MiniTitle">
                                            <p>住所2</p>
                                        </td>
                                        <td class="waku">
                                            <asp:TextBox runat="server" ID="TbxNouhinsakiAddress2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td runat="server" class="MiniTitle">
                                            <p>市町村コード</p>
                                        </td>
                                        <td class="waku">
                                            <telerik:RadComboBox runat="server" ID="RcbNouhinsakiCity"></telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td runat="server" class="MiniTitle">
                                            <p>電話番号</p>
                                        </td>
                                        <td class="waku">
                                            <asp:TextBox runat="server" ID="TbxNouhinsakiTell"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button runat="server" ID="BtnCopy3" Text="郵便番号、住所、電話番号をコピーする" OnClick="BtnCopy3_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td runat="server" class="MiniTitle">
                                            <p>敬称</p>
                                        </td>
                                        <td class="waku">
                                            <asp:TextBox runat="server" ID="TbxNouhinsakiKeisyo"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="waku">
                                            <asp:Button runat="server" ID="BtnNouhinMasta" OnClick="BtnNouhinMasta_Click" Text="納品先マスタに移動" CssClass="Btn11" Width="150px" />
                                        </td>
                                        <td class="waku">
                                            <%--                                            <asp:Button runat="server" ID="BtnNouhisakiTekiou" OnClick="BtnNouhisakiTekiou_Click" Text="見積ヘッダーに適応" CssClass="Btn10" Width="150px" />--%>
                                        </td>
                                    </tr>
                                </table>

                            </td>
                            <td style="vertical-align: top">
                                <table runat="server" id="TBFacilityDetail">
                                    <tr>
                                        <td class="MiniTitle" style="width: 120px; height: 20px;">
                                            <p>施設No.<span style="color: red;">*</span></p>
                                        </td>
                                        <td class="waku">
                                            <asp:TextBox ID="TbxFacilityCode" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MiniTitle" style="width: 120px; height: 20px;">
                                            <p>施設行コード<span style="color: red;">*</span></p>
                                        </td>
                                        <td class="waku">
                                            <asp:TextBox ID="TbxFacilityRowCode" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MiniTitle" style="width: 120px; height: 20px;">
                                            <p>施設名1<span style="color: red;">*</span></p>
                                        </td>
                                        <td class="waku">
                                            <telerik:RadComboBox runat="server" ID="RcbFacility" OnItemsRequested="RcbFacility_ItemsRequested" OnClientSelectedIndexChanged="FacilitySelected" Culture="ja-JP" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True"></telerik:RadComboBox>
                                            <script type="text/javascript"></script>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MiniTitle" style="width: 120px; height: 20px;">
                                            <p>施設名2</p>
                                        </td>
                                        <td class="waku">
                                            <asp:TextBox ID="TbxFacilityName2" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MiniTitle" style="width: 120px; height: 20px;">
                                            <p>施設名略称<span style="color: red;">*</span></p>
                                        </td>
                                        <td class="waku">
                                            <asp:TextBox ID="TbxFaci" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MiniTitle" style="width: 120px; height: 20px;">
                                            <p>施設担当者名</p>
                                        </td>
                                        <td class="waku">
                                            <asp:TextBox runat="server" ID="TbxFacilityResponsible"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MiniTitle" style="width: 120px; height: 20px;">
                                            <p>郵便番号</p>
                                        </td>
                                        <td class="waku">
                                            <asp:TextBox ID="TbxYubin" runat="server"></asp:TextBox>
                                            <asp:Button runat="server" ID="BtnPostNoSerch" Text="郵便番号検索" OnClick="BtnPostNoSerch_Click" CssClass="Btn11" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MiniTitle" style="width: 120px; height: 20px;">
                                            <p>住所1<span style="color: red;">*</span></p>
                                        </td>
                                        <td class="waku">
                                            <asp:TextBox ID="TbxFaciAdress1" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MiniTitle" style="width: 120px; height: 20px;">
                                            <p>住所2<span style="color: red;">*</span></p>
                                        </td>
                                        <td class="waku">
                                            <asp:TextBox ID="TbxFaciAdress2" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MiniTitle" style="width: 120px; height: 20px;">
                                            <p>市町村コード</p>
                                        </td>
                                        <td class="waku">
                                            <telerik:RadComboBox runat="server" ID="RcbCity" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" placeholder="市町村名を入力"></telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="MiniTitle" style="width: 120px; height: 20px;">
                                            <p>電話番号</p>
                                        </td>
                                        <td class="waku">
                                            <asp:TextBox ID="TbxTel" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button runat="server" ID="BtnCopy4" Text="郵便番号、住所、電話番号をコピーする" OnClick="BtnCopy4_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td runat="server" class="MiniTitle">
                                            <p>敬称</p>
                                        </td>
                                        <td class="waku">
                                            <asp:TextBox runat="server" ID="TbxKeisyo"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button runat="server" ID="BtnFacilityMaster" Text="直送先・施設マスタに移動" CssClass="Btn11" Width="160px" />
                                        </td>
                                        <td>
                                            <%--                                            <asp:Button runat="server" ID="BtnHeaderFit" Text="見積ヘッダーに適応" CssClass="Btn10" Width="150px" OnClick="BtnHeaderFit_Click" />--%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <script type="text/javascript">
            function Check() {
                var bool;
                var Err = document.getElementById('Err');
                var RadComboCategory = $find('RadComboCategory');
                var RadComboBox1 = $find('RadComboBox1');
                var RadComboBox3 = $find('RadComboBox3');
                var FacilityRad = $find('FacilityRad');
                var RadDatePicker3 = $find('RadDatePicker3');
                var RadDatePicker4 = $find('RadDatePicker4');
                var label3 = document.getElementById("Label3");
                var shimebi = document.getElementById("Shimebi");
                var CtrlSyousai = "CtrlSyousai";
                var ctl = "ctl";
                var Syosai = "Syosai";

                if (RadComboCategory.get_selectedItem().get_value() != "") {
                    switch (RadComboCategory.get_selectedItem().get_value()) {
                        case '101':
                        case '102':
                        case '103':
                        case '199':
                            if (RadComboBox1.get_text() != "") {
                            }
                            else {
                                debugger;
                                RadComboBox1._element.style.border = "solid 1px red";
                                bool = false;
                                break;
                            }
                            if (FacilityRad.get_text() != "") {

                            }
                            else {
                                debugger;
                                FacilityRad._element.style.border = "solid 1px red";
                                bool = false;
                            }
                            if (label3.value != "") {

                            }
                            else {
                                label3.value = "掛率が表示されていません。得意先詳細を確認して下さい。";
                                label3.style.color = "red";
                                bool = false;
                            }
                            if (shimebi.value != "") {

                            }
                            else {
                                shimebi.value = "締日が表示されていません。得意先詳細を確認して下さい";
                                shimebi.style.color = "red";
                                bool = false;
                            }
                        default:
                            if (RadComboBox1.get_text() != "") {
                            }
                            else {
                                debugger;
                                RadComboBox1._element.style.border = "solid 1px red";
                                bool = false;
                            }
                            if (FacilityRad.get_text() != "") {

                            }
                            else {
                                debugger;
                                FacilityRad._element.style.border = "solid 1px red";
                                bool = false;
                            }
                            if (label3.value != "") {

                            }
                            else {
                                label3.value = "掛率が表示されていません。得意先詳細を確認して下さい。";
                                label3.style.color = "red";
                                bool = false;
                            }
                            if (shimebi.value != "") {

                            }
                            else {
                                shimebi.value = "締日が表示されていません。得意先詳細を確認して下さい";
                                shimebi.style.color = "red";
                                bool = false;
                            }
                    }
                }
                else {
                    Err.innerText = "カテゴリーを選択して下さい";
                    bool = false;
                }
                var dg = document.getElementById('CtrlSyousai');
                var c = dg.rows.length;
                for (var i = 1; i < c; i++) {
                    var x = i + 1;
                    var SerchProduct = $find(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "SerchProduct");
                    var Baitai = document.getElementById(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "Baitai");
                    var HyoujyunTanka = document.getElementById(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "HyoujyunTanka");
                    var Kingaku = document.getElementById(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "Kingaku");
                    var Tanka = document.getElementById(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "Tanka");
                    var Uriage = document.getElementById(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "Uriage");
                    var ShiyouShisetsu = $find(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "ShiyouShisetsu");
                    var StartDate = $find(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "StartDate");
                    var EndDate = $find(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "EndDate");
                    var Kakeri = document.getElementById(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "Kakeri");
                    var zeiku = document.getElementById(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "zeiku");
                    var Hachu = $find(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "Hachu");
                    var WareHouse = document.getElementById(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "WareHouse");
                    var ShiireTanka = document.getElementById(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "ShiireTanka");
                    var ShiireKingaku = document.getElementById(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "ShiireKingaku");
                    var RcbHanni = $find(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "RcbHanni");
                    var Zasu = $find(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "Zasu");
                    debugger;
                    var catecode = RadComboCategory.get_selectedItem().get_value();
                    switch (catecode) {
                        case '101':
                        case '102':
                        case '103':
                        case '199':
                        case '205':
                            if (StartDate.get_selectedDate.get_value == "") {
                                StartDate._element.style.border = "solid 1px red";
                                bool = false;
                            }
                            if (EndDate.get_selectedDate.get_value == "") {
                                EndDate._element.style.border = "solid 1px red";
                                bool = false;
                            }
                            if (RcbHanni.get_text == "") {
                                RcbHanni._element.style.border = "solid 1px red";
                                bool = false;
                            }
                            if (Zasu.get_text == "") {
                                Zasu._element.style.border = "solid 1px red";
                                bool = false;
                            }
                            break;
                        default:
                            if (StartDate.get_selectedDate.get_value == "") {
                                StartDate._element.style.border = "solid 1px red";
                                bool = false;
                            }
                            if (EndDate.get_selectedDate.get_value == "") {
                                EndDate._element.style.border = "solid 1px red";
                                bool = false;
                            }
                    }
                    if (SerchProduct.get_text() == "") {
                        SerchProduct._element.style.border = "solid 1px red";
                        bool = false;
                    }
                    if (Baitai.value == "") {
                        Baitai.value = "媒体が表示されていません。商品詳細を確認して下さい。";
                        bool = false;
                    }
                    if (HyoujyunTanka.value == "") {
                        HyoujyunTanka.style.border = "solid 1px red";
                        bool = false;
                    }
                    if (Kingaku.value == "") {
                        Kingaku.style.border = "solid 1px red";
                        bool = false;
                    }
                    if (Tanka.value == "") {
                        Tanka.style.border = "solid 1px red";
                        bool = false;
                    }
                    if (Uriage.value == "") {
                        Uriage.style.border = "solid 1px red";
                        bool = false;
                    }
                    if (ShiyouShisetsu.get_text() == "") {
                        ShiyouShisetsu._element.style.border = "solid 1px red";
                        bool = false;
                    }
                    if (Kakeri.value == "") {
                        Kakeri.value = "掛率が表示されていません。得意先詳細を確認してください。";
                        bool = false;
                    }
                    if (zeiku.value == "") {
                        zeiku.value = "税区分が表示されていません。得意先詳細を確認してください。";
                        bool = false;
                    }
                    if (WareHouse.value == "") {
                        WareHouse.value = "倉庫が表示されていません。商品詳細を確認してください。";
                        bool = false;
                    }
                    if (ShiireTanka.value == "") {
                        ShiireTanka.style.border = "solid 1px red";
                        bool = false;
                    }
                    if (ShiireKingaku.value == "") {
                        ShiireKingaku.style.border = "solid 1px red";
                        bool = false;
                    }
                }
                debugger;
                if (bool == false) {
                    Err.innerText = "赤枠を入力して下さい";
                    return false;
                }
            }
        </script>
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
        <script type="text/javascript">
            function Meisai2(ss) {
                debugger;
                document.getElementById(ss).style.display = "";
                document.getElementById("mInput2").style.display = "none";
                document.getElementById("CtrlSyousai").style.display = "none";
                document.getElementById("head").style.display = "none";
                document.getElementById("TBAddRow").style.display = "none";
                document.getElementById("SubMenu").style.display = "none";
                document.getElementById("SubMenu2").style.display = "";
                document.getElementById("DivDataGrid").style.display = "none";

                debugger;
            }
        </script>
        <script type="text/javascript">
            function Close2(ss) {
                debugger;
                document.getElementById(ss).style.display = "none";
                document.getElementById("mInput2").style.display = "";
                document.getElementById("CtrlSyousai").style.display = "";
                document.getElementById("head").style.display = "";
                document.getElementById("Button13").style.display = "";
                document.getElementById("SubMenu").style.display = "";
                document.getElementById("SubMenu2").style.display = "none";
                document.getElementById("DivDataGrid").style.display = "";

                debugger;
            }
        </script>
        <script type="text/javascript">
            function TokuisakiSelect(sender, eventArgs) {
                var id = sender.get_element().id;
                var rcbTokuisaki = $find(id);
                var selectedvalue = rcbTokuisaki.get_selectedItem().get_value();
                var AryValue = selectedvalue.split(',');
                var CustomerCode = AryValue[0];
                var TokuisakiCode = AryValue[1];
                var TokuisakiName1 = AryValue[2];
                var TantoStaffCode = AryValue[5];
                var TokuisakiFurifana = AryValue[6];
                var TokuisakiRyakusyo = AryValue[7];
                var TokuisakiPostNo = AryValue[8];
                var TokuisakiAddress1 = AryValue[9];
                var TokuisakiAddress2 = AryValue[10];
                var TokuisakiTEL = AryValue[11];
                var TokuisakiFAX = AryValue[12];
                var TokuisakiStaff = AryValue[13];
                var TokuisakiDepartment = AryValue[14];
                var Kakeritsu = AryValue[16];
                var Shimebi = AryValue[17];
                var Zeikubun = AryValue[19];

                //得意先を選択した際に入力される内容
                var RadComboBox3 = $find('RadComboBox3');
                var shimebi = document.getElementById('Shimebi');
                var RadZeiKubun = $find('RadZeiKubun');
                var TbxCustomer = document.getElementById('TbxCustomer');
                var TbxTokuisakiCode = document.getElementById('TbxTokuisakiCode');
                var TbxTokuisakiName = $find('RcbTokuisakiNameSyousai');
                var TbxTokuisakiFurigana = document.getElementById('TbxTokuisakiFurigana');
                var TbxTokuisakiRyakusyo = document.getElementById('TbxTokuisakiRyakusyo');
                var TbxTokuisakiStaff = document.getElementById('TbxTokuisakiStaff');
                var TbxTokuisakiPostNo = document.getElementById('TbxTokuisakiPostNo');
                var TbxTokuisakiAddress = document.getElementById('TbxTokuisakiAddress');
                var TbxTokuisakiAddress1 = document.getElementById('TbxTokuisakiAddress1');
                var TbxTokuisakiTEL = document.getElementById('TbxTokuisakiTEL');
                var TbxTokuisakiFax = document.getElementById('TbxTokuisakiFax');
                var TbxTokuisakiDepartment = document.getElementById('TbxTokuisakiDepartment');
                var RcbTax = $find('RcbTax');
                var TbxKakeritsu = document.getElementById('TbxKakeritsu');
                var RcbShimebi = $find('RcbShimebi');
                var HidShimebi = document.getElementById("HidShimebi");
                var LblTantoStaffCode = document.getElementById("<%=LblTantoStaffCode.ClientID%>");
                var HidTantoStaffCode = document.getElementById("<%=HidTantoStaffCode.ClientID%>");
                var HidKakeritsu = document.getElementById("<%=HidKakeritsu.ClientID%>");
                const Textbox12 = document.getElementById("<%=UriageGokei.ClientID%>");
                //請求先詳細にもまず記載する
                var TbxCustomerCode2 = document.getElementById('TbxCustomerCode2');
                var TbxTokuisakiCode2 = document.getElementById('TbxTokuisakiCode2');
                var RcbSeikyusaki = $find('RcbSeikyusaki');
                var TbxTokuisakiFurigana2 = document.getElementById('TbxTokuisakiFurigana2');
                var TbxTokuisakiRyakusyou2 = document.getElementById('TbxTokuisakiRyakusyou2');
                var TbxTokuisakiStaff2 = document.getElementById('TbxTokuisakiStaff2');
                var TbxPostNo2 = document.getElementById('TbxPostNo2');
                var TbxTokuisakiAddress2 = document.getElementById('TbxTokuisakiAddress2');
                var TbxTokuisakiAddress3 = document.getElementById('TbxTokuisakiAddress3');
                var TbxTel2 = document.getElementById('TbxTel2');
                var TbxFAX2 = document.getElementById('TbxFAX2');
                var TbxDepartment2 = document.getElementById('TbxDepartment2');
                var RcbZeikubun2 = $find("RcbZeikubun2");
                var RadShimebi2 = $find('RadShimebi2');

                //入力
                RadComboBox3.set_text(TokuisakiRyakusyo);
                HidKakeritsu.value = Kakeritsu;
                debugger;
                shimebi.innerText = Shimebi;
                HidShimebi.value = Shimebi;
                RadZeiKubun.set_text(Zeikubun);
                TbxCustomer.value = CustomerCode;
                TbxTokuisakiCode.value = TokuisakiCode;
                TbxTokuisakiName.set_text(TokuisakiName1);
                TbxTokuisakiFurigana.value = TokuisakiFurifana;
                TbxTokuisakiRyakusyo.value = TokuisakiRyakusyo;
                TbxTokuisakiStaff.value = TokuisakiStaff;
                TbxTokuisakiPostNo.value = TokuisakiPostNo;
                TbxTokuisakiAddress.value = TokuisakiAddress1;
                TbxTokuisakiAddress1.value = TokuisakiAddress2;
                TbxTokuisakiTEL.value = TokuisakiTEL;
                TbxTokuisakiFax.value = TokuisakiFAX;
                TbxTokuisakiDepartment.value = TokuisakiDepartment;
                RcbTax.set_text(Zeikubun);
                TbxKakeritsu.value = Kakeritsu;
                RcbShimebi.set_text(Shimebi);
                LblTantoStaffCode.innerText = TantoStaffCode;
                HidTantoStaffCode.value = TantoStaffCode;
                ////////請求先記載/////////////
                TbxCustomerCode2.value = CustomerCode;
                TbxTokuisakiCode2.value = TokuisakiCode;
                RcbSeikyusaki.set_text(TokuisakiName1);
                TbxTokuisakiFurigana2.value = TokuisakiFurifana;
                TbxTokuisakiRyakusyou2.value = TokuisakiRyakusyo;
                TbxTokuisakiStaff2.value = TokuisakiStaff;
                TbxPostNo2.value = TokuisakiPostNo;
                TbxTokuisakiAddress2.value = TokuisakiAddress1;
                TbxTokuisakiAddress3.value = TokuisakiAddress2;
                TbxTel2.value = TokuisakiTEL;
                TbxFAX2.value = TokuisakiFAX;
                TbxDepartment2.value = TokuisakiDepartment;
                RcbZeikubun2.set_text(Zeikubun);
                RadShimebi2.set_text(Shimebi);
                rcbTokuisaki.clearItems();

            }
        </script>
        <script type="text/javascript">
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
        <script type="text/javascript">
            function Cul() {
                debugger;
                var Zeikubun = $find('RadZeiKubun');
                var dg = document.getElementById('CtrlSyousai');
                var TbxUriageKei = document.getElementById('TextBox7');
                var TbxTax = document.getElementById('TextBox5');
                var TbxUriageGokei = document.getElementById('TextBox12');
                var TbxSuryo = document.getElementById('TextBox10');
                var TbxShiireKei = document.getElementById('TextBox8');
                var TbxArariKei = document.getElementById('TextBox6');
                var TbxRieki = document.getElementById('TextBox13');
                var suryo = 0;
                var UriageKei = 0;
                var Tax = 0;
                var UriageGokei = 0;
                var Shiire = 0;
                var c = dg.rows.length;
                var CtrlSyousai = "CtrlSyousai";
                var ctl = "ctl";
                var Syosai = "Syosai";
                var boolZei = Zeikubun.get_text();
                if (boolZei == "税込") {
                    for (var i = 1; i < c; i++) {
                        var x = i + 1;
                        var Kingaku = document.getElementById(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "Kingaku").value.replace(",", "");
                        var Uriage = document.getElementById(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "Uriage").value.replace(",", "");
                        var ShiireKingaku = document.getElementById(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "ShiireKingaku").value.replace(",", "");
                        var Kazu = document.getElementById(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "Suryo").value.replace(",", "");
                        var TbxMakerNo = document.getElementById(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "TbxMakerNo").value;
                        if (TbxMakerNo != "0") {
                            suryo += Number(Kazu);
                        }
                        Tax += Uriage - Kingaku;
                        UriageKei += Number(Uriage);
                        UriageGokei += Number(Uriage);
                        Shiire += Number(ShiireKingaku);
                    }
                    TbxTax.value = String(Tax).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                    TbxUriageKei.value = String(UriageKei).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                    TbxUriageGokei.value = String(UriageKei).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                    TbxSuryo.value = suryo;
                    TbxShiireKei.value = String(Shiire).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                    TbxArariKei.value = String(UriageKei - Shiire).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                    TbxRieki.value = String((UriageKei - Shiire) * 100 / UriageKei);
                }
                if (boolZei == "税抜") {
                    for (var i = 1; i < c; i++) {
                        var x = i + 1;
                        var Kingaku = document.getElementById(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "Kingaku").value.replace(",", "");
                        var Uriage = document.getElementById(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "Uriage").value.replace(",", "");
                        var ShiireKingaku = document.getElementById(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "ShiireKingaku").value.replace(",", "");
                        var Kazu = document.getElementById(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "Suryo").value.replace(",", "");
                        var TbxMakerNo = document.getElementById(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "TbxMakerNo").value;
                        if (TbxMakerNo != "0") {
                            suryo += Number(Kazu);
                        }
                        UriageKei += Number(Uriage);
                        Shiire += Number(ShiireKingaku);
                    }
                    TbxUriageKei.value = String(UriageKei).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                    TbxTax.value = String(UriageKei * 0.1).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                    TbxUriageGokei.value = String(UriageKei * 1.1).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                    TbxSuryo.value = suryo;
                    TbxShiireKei.value = String(Shiire).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                    TbxArariKei.value = String(UriageKei - Shiire).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                    TbxRieki.value = String((UriageKei * 1.1 - Shiire) * 100 / UriageKei);
                }
                window.scrollTo(0, 0);
            }
        </script>
        <script type="text/javascript">
            function Nouhinsaki2(sender, eventArgs) {
                var selval = $find("<%=RadComboBox2.ClientID%>");
                var val = selval.get_selectedItem().get_value();
                var settext = $find("<%=RcbNouhinsakiMei.ClientID%>");
                settext.set_text(selval.get_selectedItem().get_text());

                var Ary = val.split('/');
                var TbxNouhinSisetsu = document.getElementById('TbxNouhinSisetsu');
                var TbxCode = document.getElementById('TbxCode');
                var TbxNouhinTyokusousakiMei2 = document.getElementById('TbxNouhinTyokusousakiMei2');
                var TbxNouhinsakiRyakusyou = document.getElementById('TbxNouhinsakiRyakusyou');
                var TbxNouhinsakiTanto = document.getElementById('TbxNouhinsakiTanto');
                var TbxNouhinsakiYubin = document.getElementById('TbxNouhinsakiYubin');
                var TbxNouhinsakiAddress = document.getElementById('TbxNouhinsakiAddress');
                var TbxNouhinsakiAddress2 = document.getElementById('TbxNouhinsakiAddress2');
                var TbxNouhinsakiTell = document.getElementById('TbxNouhinsakiTell');
                var TbxNouhinsakiKeisyo = document.getElementById('TbxNouhinsakiKeisyo');
                var RcbNouhinsakiCity = $find('RcbNouhinsakiCity');
                var RcbNouhinsakiMei = $find('RcbNouhinsakiMei');
                //施設にも
                var FacilityRad = $find('FacilityRad');
                var TbxFacilityCode = document.getElementById('TbxFacilityCode');
                var TbxFacilityRowCode = document.getElementById('TbxFacilityRowCode');
                var TbxFacilityName2 = document.getElementById('TbxFacilityName2');
                var TbxFaci = document.getElementById('TbxFaci');
                var TbxFacilityResponsible = document.getElementById('TbxFacilityResponsible');
                var TbxYubin = document.getElementById('TbxYubin');
                var TbxFaciAdress1 = document.getElementById('TbxFaciAdress1');
                var TbxFaciAdress2 = document.getElementById('TbxFaciAdress2');
                var TbxTel = document.getElementById('TbxTel');
                var TbxKeisyo = document.getElementById('TbxKeisyo');
                var RcbFacility = $find('RcbFacility');
                var RcbCity = $find('RcbCity');

                FacilityRad.set_text(Ary[4]);
                FacilityRad.set_value(val);
                TbxNouhinSisetsu.value = Ary[0];
                TbxFacilityCode.value = Ary[0];
                TbxCode.value = Ary[1];
                TbxFacilityRowCode.value = Ary[1];
                RcbNouhinsakiMei.set_text(Ary[2]);
                RcbFacility.set_text(Ary[2]);
                TbxNouhinTyokusousakiMei2.value = Ary[3];
                TbxFacilityName2.value = Ary[3];
                TbxNouhinsakiRyakusyou.value = Ary[4];
                TbxFaci.value = Ary[4];
                TbxNouhinsakiTanto.value = Ary[5];
                TbxFacilityResponsible.value = Ary[5];
                TbxNouhinsakiYubin.value = Ary[6];
                TbxYubin.value = Ary[6];
                TbxNouhinsakiAddress.value = Ary[7];
                TbxFaciAdress1.value = Ary[7];
                TbxNouhinsakiAddress2.value = Ary[8];
                TbxFaciAdress2.value = Ary[8];
                TbxNouhinsakiTell.value = Ary[9];
                TbxTel.value = Ary[9];
                RcbNouhinsakiCity.findItemByValue(Ary[10]).select();
                RcbCity.findItemByValue(Ary[10]).select();
                var faccombo = $find('CtrlSyousai_ctl02_Syosai_SerchProduct');
                var input = faccombo.get_inputDomElement();
                input.focus();
                selval.clearItems();
                settext.clearItems();
            }
        </script>
        <script type="text/javascript">
            function FacilitySelected(sender, eventArgs) {
                var RcbFacility = $find('RcbFacility');
                var selectedvalue = RcbFacility.get_selectedItem().get_value().split('/');
                document.getElementById('TbxFacilityCode').value = selectedvalue[0];
                document.getElementById('TbxFacilityRowCode').value = selectedvalue[1];
                RcbFacility.set_text(selectedvalue[2]);
                document.getElementById('TbxFacilityName2').value = selectedvalue[3];
                document.getElementById('TbxFaci').value = selectedvalue[4];
                document.getElementById('TbxFacilityResponsible').value = selectedvalue[5];
                document.getElementById('TbxYubin').value = selectedvalue[6];
                document.getElementById('TbxFaciAdress1').value = selectedvalue[7];
                document.getElementById('TbxFaciAdress2').value = selectedvalue[8];
                $find('RcbCity').set_value(selectedvalue[9]);
                document.getElementById('TbxTel').value = selectedvalue[10];
                document.getElementById('TbxKeisyo').value = selectedvalue[11];
            }
        </script>

        <script type="text/javascript">
            function SeikyusakiSelected(sender, eventArgs) {
                var rcbTokuisaki = $find("<%=RadComboBox3.ClientID%>");
                var selectedvalue = rcbTokuisaki.get_selectedItem().get_value();
                var AryValue = selectedvalue.split(',');
                var CustomerCode = AryValue[0];
                var TokuisakiCode = AryValue[1];
                var TokuisakiName1 = AryValue[2];
                var TantoStaffCode = AryValue[5];
                var TokuisakiFurifana = AryValue[6];
                var TokuisakiRyakusyo = AryValue[7];
                var TokuisakiPostNo = AryValue[8];
                var TokuisakiAddress1 = AryValue[9];
                var TokuisakiAddress2 = AryValue[10];
                var TokuisakiTEL = AryValue[11];
                var TokuisakiFAX = AryValue[12];
                var TokuisakiStaff = AryValue[13];
                var TokuisakiDepartment = AryValue[14];
                var Kakeritsu = AryValue[16];
                var Shimebi = AryValue[17];
                var Zeikubun = AryValue[19];
                var TbxCustomerCode2 = document.getElementById('TbxCustomerCode2');
                var TbxTokuisakiCode2 = document.getElementById('TbxTokuisakiCode2');
                var TbxTokuisakiMei2 = document.getElementById('TbxTokuisakiMei2');
                var TbxTokuisakiFurigana2 = document.getElementById('TbxTokuisakiFurigana2');
                var TbxTokuisakiRyakusyou2 = document.getElementById('TbxTokuisakiRyakusyou2');
                var TbxTokuisakiStaff2 = document.getElementById('TbxTokuisakiStaff2');
                var TbxPostNo2 = document.getElementById('TbxPostNo2');
                var TbxTokuisakiAddress2 = document.getElementById('TbxTokuisakiAddress2');
                var TbxTel2 = document.getElementById('TbxTel2');
                var TbxFAX2 = document.getElementById('TbxFAX2');
                var TbxDepartment2 = document.getElementById('TbxDepartment2');
                var RcbZeikubun2 = $find("RcbZeikubun2");
                //var TbxKakeritsu2 = document.getElementById('TbxKakeritsu2');
                var RadShimebi2 = $find('RadShimebi2');
                var HidTantoStaffCode2 = document.getElementById('HidTantoStaffCode2');
                //var LblTantoStaffNo2 = document.getElementById('LblTantoStaffNo2');


                TbxCustomerCode2.value = CustomerCode;
                TbxTokuisakiCode2.value = TokuisakiCode;
                TbxTokuisakiMei2.value = TokuisakiName1;
                TbxTokuisakiFurigana2.value = TokuisakiFurifana;
                TbxTokuisakiRyakusyou2.value = TokuisakiRyakusyo;
                TbxTokuisakiStaff2.value = TokuisakiStaff;
                TbxPostNo2.value = TokuisakiPostNo;
                TbxTokuisakiAddress2.value = TokuisakiAddress1 + TokuisakiAddress2;
                TbxTel2.value = TokuisakiTEL;
                TbxFAX2.value = TokuisakiFAX;
                TbxDepartment2.value = TokuisakiDepartment;
                RcbZeikubun2.set_text(Zeikubun);
                //TbxKakeritsu2.value = Kakeritsu;
                RadShimebi2.set_text(Shimebi);
                //LblTantoStaffNo2.innerText = TantoStaffCode;
                HidTantoStaffCode2.value = TantoStaffCode;
                rcbTokuisaki.clearItems();
            }
        </script>


        <script type="text/javascript">
            function MeisaiFacility(btn) {
                let TBFacilityDetail = document.getElementById('TBFacilityDetail');
                TBFacilityDetail.style.display = "";
                document.getElementById("mInput").style.display = "none";
                document.getElementById("CtrlSyousai").style.display = "none";
                document.getElementById("head").style.display = "none";
                document.getElementById("TBAddRow").style.display = "none";
                document.getElementById("SubMenu").style.display = "none";
                document.getElementById("SubMenu2").style.display = "";
                document.getElementById("DivDataGrid").style.display = "none";
            }
        </script>
        <script type="text/javascript">
            function Close3(Btn) {
                let TBFacilityDetail = document.getElementById('TBFacilityDetail');
                TBFacilityDetail.style.display = "none";
                document.getElementById("mInput").style.display = "";
                document.getElementById("CtrlSyousai").style.display = "";
                document.getElementById("head").style.display = "";
                document.getElementById("TBAddRow").style.display = "";
                document.getElementById("SubMenu").style.display = "";
                document.getElementById("SubMenu2").style.display = "none";
                document.getElementById("DivDataGrid").style.display = "";
            }
        </script>
        <script type="text/javascript">
            function select(sender, eventArgs) {
                var combo2 = document.activeElement.id;
                var onk = event.keyCode;
                var clid = combo2.split("_");
                var combo3 = $find(combo2.replace("_Input", ""));
                var vv;
                if (onk == 13) {
                    var c = combo3.get_items().get_count();
                    if (c == 1) {
                        vv = combo3.get_items().getItem(0).get_value();
                    }
                    else {
                        vv = combo3.get_selectedItem().get_value();
                    }
                }
                else {
                    vv = combo3.get_selectedItem().get_value();
                }
                var Aryval = vv.split('^');
                var syouhincode = Aryval[0];
                var media = Aryval[10];
                var hanni = Aryval[14];
                var categorycode = Aryval[4];
                var categoryname = Aryval[5];
                var makernumber = Aryval[13];
                var permisisionstart = Aryval[2];
                var rightend = Aryval[3];
                var cpkaishi = Aryval[18];
                var cpowari = Aryval[19];
                var hyoujunkakaku = Aryval[16];
                var shiirename = Aryval[12];
                var shiirecode = Aryval[11];
                var shiirekakaku = Aryval[17];
                var syouhinmei = Aryval[1];
                var warehouse = Aryval[15];
                var cpkakaku = Aryval[20];
                var cpshiire = Aryval[21];

                combo3.clearItems();


                var TbxProductCode = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "TbxProductCode");
                var Baitai = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "Baitai")
                var LblCateCode = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "LblCateCode");
                var LblCategoryName = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "LblCategoryName");
                var LblProduct = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "LblProduct");
                var TbxMakerNo = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "TbxMakerNo");
                var RdpPermissionstart = $find(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "RdpPermissionstart");
                var RdpRightEnd = $find(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "RdpRightEnd");
                var RdpCpStart = $find(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "RdpCpStart");
                var RdpCpEnd = $find(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "RdpCpEnd");
                var TbxHyoujun = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "TbxHyoujun");
                var LblHanni = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "LblHanni");
                var TbxHanni = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "TbxHanni");
                var RcbHanni = $find(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "RcbHanni");
                var RcbShiireName = $find(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "RcbShiireName");
                var Hachu = $find(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "Hachu");
                var LblShiireCode = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "LblShiireCode");
                var HidShiireCode = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "HidShiireCode");
                var RcbMedia = $find(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "RcbMedia");
                var TbxProductName = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "TbxProductName");
                var WareHouse = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "WareHouse");
                var TbxWareHouse = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "TbxWareHouse");
                var SerchProduct = $find(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "SerchProduct");
                var TbxShiirePrice = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "TbxShiirePrice");
                var HidMedia = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "HidMedia");
                var HidHanni = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "HidHanni");
                var HidJoueiHanni = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "HidJoueiHanni");
                var TbxCpKakaku = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "TbxCpKakaku");
                var TbxCpShiire = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "TbxCpShiire");
                var ShiyouShisetsu = $find(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "ShiyouShisetsu");
                var StartDate = $find(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "StartDate");
                var EndDate = $find(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "EndDate");
                var SyokaiDate = $find('RadDatePicker1');
                var HidSyokaiDate = document.getElementById("HidSyokaiDate");
                rightend = new Date(rightend);
                cpkaishi = new Date(cpkaishi);
                cpowari = new Date(cpowari);
                TbxProductCode.value = syouhincode;
                Baitai.innerText = media;
                RcbMedia.set_text(media);
                HidMedia.value = media;
                HidShiireCode.value = shiirecode;
                LblCateCode.innerText = categorycode;
                LblCategoryName.innerText = categoryname;
                LblProduct.innerText = makernumber;
                TbxMakerNo.value = makernumber;
                RdpPermissionstart.set_selectedDate(new Date(permisisionstart));
                RdpRightEnd.set_selectedDate(rightend);
                RdpCpStart.set_selectedDate(cpkaishi);
                RdpCpEnd.set_selectedDate(cpowari);
                TbxHyoujun.value = String(hyoujunkakaku).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                TbxShiirePrice.value = String(shiirekakaku).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                LblHanni.innerText = hanni;
                TbxHanni.value = hanni;
                HidHanni.value = hanni;
                TbxCpKakaku.value = String(cpkakaku).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                TbxCpShiire.value = String(cpshiire).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                RcbShiireName.set_text(shiirename);
                RcbShiireName.set_value(shiirecode);
                Hachu.set_text(shiirename);
                Hachu.set_value(shiirecode);
                LblShiireCode.innerText = shiirecode;
                SerchProduct.set_text(syouhinmei);
                TbxProductName.value = syouhinmei;
                WareHouse.innerText = warehouse;
                TbxWareHouse.value = warehouse;
                debugger;
                if (categorycode != "205") {
                    var Kakeri = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "Kakeri");
                    var zeikubun = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "zeiku");
                    var suryo = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "Suryo");
                    var HyoujyunTanka = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "HyoujyunTanka");
                    var Kingaku = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "Kingaku");
                    var Tanka = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "Tanka");
                    var Uriage = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "Uriage");
                    var ShiireTanka = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "ShiireTanka");
                    var ShiireKingaku = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "ShiireKingaku");
                    var HidColor = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "HidColor");

                    var kakaku;
                    var shiire;
                    var Syokai;
                    if (SyokaiDate != null) {
                        Syokai = SyokaiDate.get_selectedDate();
                    }
                    else {
                        Syokai = HidSyokaiDate.value;
                    }
                    var inputElement = combo3.get_inputDomElement();
                    if (Syokai >= Date.parse(cpkaishi)) {
                        if (Syokai <= Date.parse(cpowari)) {
                            kakaku = cpkakaku;
                            shiire = cpshiire;
                            inputElement.style.color = 'orange';
                            HidColor.value = 'orange';
                        }
                        else {
                            kakaku = hyoujunkakaku;
                            shiire = shiirekakaku;
                            inputElement.style.color = 'black';
                        }
                    }
                    else {
                        kakaku = hyoujunkakaku;
                        shiire = shiirekakaku;
                        inputElement.style.color = 'black';
                    }
                    if (Syokai <= Date.parse(permisisionstart)) {
                        kakaku = hyoujunkakaku;
                        shiire = shiirekakaku;
                        inputElement.style.color = 'blue';
                    }
                    var Kyodaku = RdpRightEnd.get_selectedDate();
                    if (Syokai >= Date.parse(Kyodaku)) {
                        inputElement.style.color = 'red';
                    }
                    if (zeikubun.innerText == "税込") {//税区分が税込の場合、商品選択時の計算
                        //標準単価＆金額（金額 ＝　標準単価 × 数量）
                        HyoujyunTanka.value = String(kakaku).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                        var kin = kakaku * suryo.value;
                        Kingaku.value = String(kin).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                        //仕入単価＆仕入金額（仕入金額　＝　仕入単価　×　数量　×　税込なので1.1）
                        ShiireTanka.value = String(shiire).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                        var shiirekin = shiire * suryo.value * 1.1;
                        shiirekin = Math.trunc(shiirekin);
                        ShiireKingaku.value = String(shiirekin).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                        //単価＆売上金額（売上金額　＝　掛率計算済「税込」標準単価　×　数量　×　税込なので1.1）
                        var tanka = kakaku * Kakeri.innerText * 1.1 / 100;
                        tanka = Math.trunc(tanka);
                        Tanka.value = String(tanka).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                        var uriage = tanka * suryo.value;
                        Uriage.value = String(uriage).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                    }
                    else {//税抜の場合の計算
                        //標準単価＆金額（金額 ＝　標準単価 × 数量）
                        HyoujyunTanka.value = String(kakaku).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                        var kin = kakaku * suryo.value;
                        Kingaku.value = String(kin).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                        //仕入単価＆仕入金額（仕入金額　＝　仕入単価　×　数量）
                        ShiireTanka.value = String(shiire).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                        var shiirekin = shiire * suryo.value;
                        shiirekin = Math.trunc(shiirekin);
                        ShiireKingaku.value = String(shiirekin).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                        //単価＆売上金額（売上金額　＝　掛率計算済「税抜」標準単価　×　数量）
                        var tanka = kakaku * Kakeri.innerText / 100;
                        tanka = Math.trunc(tanka);
                        Tanka.value = String(tanka).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                        var uriage = tanka * suryo.value;
                        Uriage.value = String(uriage).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                    }

                    if (syouhincode == "1999") {
                        HyoujyunTanka.readOnly = true;
                        Kingaku.readOnly = true;
                        ShiireTanka.readOnly = true;
                        ShiireKingaku.readOnly = true;

                        HyoujyunTanka.style.backgroundColor = "lightgray";
                        Kingaku.style.backgroundColor = "lightgray";
                        ShiireTanka.style.backgroundColor = "lightgray";
                        ShiireKingaku.style.backgroundColor = "lightgray";

                        suryo.style.display = "none";
                    }

                    const BtnAddRow = document.getElementById(clid[0] + "_" + clid[1] + "_" + "BtnAddRow");
                    if (ShiyouShisetsu.get_text() == "") {
                        ShiyouShisetsu.focus();
                    }
                    else {
                        switch (categorycode) {
                            case "101":
                            case "102":
                            case "103":
                            case "199":
                                BtnAddRow.focus();
                                break;
                            default:
                                if (StartDate.get_selectedDate() != Date("")) {
                                    BtnAddRow.focus();
                                }
                                else {
                                    StartDate.focus();
                                }
                                break;
                        }
                    }
                }
                combo2.clearItems();
            }

        </script>
        <script type="text/javascript">
            function Keisan(tbx) {
                var ary = tbx.split('-');
                var tanka = document.getElementById(ary[0]).value;
                var suryo = document.getElementById(ary[1]).value;
                var Hyoutan = document.getElementById(ary[3]).value;
                document.getElementById(ary[7]).value = Hyoutan;
                var Shitan = document.getElementById(ary[4]).value;
                var tan = tanka.replace(",", "");
                var hyo = Hyoutan.replace(",", "");
                var shi = Shitan.replace(",", "");
                var num = tan * suryo;
                var num1 = hyo * suryo;
                var num2 = shi * suryo;

                var num = String(num).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                var numm = num1.toString();
                if (numm != "NaN") {
                    var num1 = String(num1).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                    document.getElementById(ary[5]).value = num1;
                }
                var num2 = String(num2).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");

                document.getElementById(ary[2]).value = num;
                document.getElementById(ary[6]).value = num2;

            }
        </script>
        <script type="text/javascript">
            function SelectFacility(sender, eventArgs) {
                var id = sender.get_element().id;
                var rcb = $find(id)
                var IDtemplate = id.replace("ShiyouShisetsu", "temp");
                var TbxFacilityCode = document.getElementById(IDtemplate.replace("temp", "TbxFacilityCode"));
                var TbxFacilityRowCode = document.getElementById(IDtemplate.replace("temp", "TbxFacilityRowCode"));
                var TbxFacilityName = document.getElementById(IDtemplate.replace("temp", "TbxFacilityName"));
                var TbxFacilityName2 = document.getElementById(IDtemplate.replace("temp", "TbxFacilityName2"));
                var TbxFaci = document.getElementById(IDtemplate.replace("temp", "TbxFaci"));
                var TbxFacilityResponsible = document.getElementById(IDtemplate.replace("temp", "TbxFacilityResponsible"));
                var TbxYubin = document.getElementById(IDtemplate.replace("temp", "TbxYubin"));
                var TbxFaciAdress = document.getElementById(IDtemplate.replace("temp", "TbxFaciAdress"));
                var TbxTel = document.getElementById(IDtemplate.replace("temp", "TbxTel"));
                var RcbCity = $find(IDtemplate.replace("temp", "RcbCity"));

                var items = rcb.get_selectedItem().get_value().split('/');

                TbxFacilityCode.value = items[0];
                TbxFacilityRowCode.value = items[1];
                TbxFacilityName.value = items[2];
                TbxFacilityName2.value = items[3];
                TbxFaci.value = items[4];
                TbxFacilityResponsible.value = items[5];
                TbxYubin.value = items[6];
                TbxFaciAdress.value = items[7] + items[8];
                TbxTel.value = items[9];
                RcbCity.findItemByValue(items[10]).select();
            }
        </script>
        <script type="text/javascript">
            function SerchFocus(fcs) {
                var ary = fcs.split('-');
                var rcb = document.getElementById(ary[0]);
                var btn = document.getElementById(ary[1]);
                btn.focus();
            }
        </script>
        <script type="text/javascript">
            function Meisai(ss) {
                let ss2 = document.getElementById(ss);
                ss2.style.display = "";
            }
        </script>
        <script type="text/javascript">
            function BtnUpdateFaci() {
                try {
                    var TbxFaci = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "TbxFaci");
                    var ShiyouShisetsu = $find(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "ShiyouShisetsu");
                    ShiyouShisetsu.set_text = TbxFaci.innerText;
                    const SisetuSyousai = document.getElementById("SisetuSyousai");
                    SisetuSyousai.style.display = '';
                }
                catch {
                    var Err = document.getElementById(clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "ShiyouShisetsu");
                    Err.innerText = "施設名略称を入力して下さい";
                }
            }
        </script>
        <script type="text/javascript">
            function HyoujunKeyDown(hy) {
                var hyoujun = document.getElementById(hy).onkeydown;
                var g = event.keyCode;
                if ((g == 13)
                ) {
                    return false;
                }
                if ((g == 9)) {
                    var combo2 = document.activeElement.id;
                    var clid = combo2.split("_");
                    var combo3 = $find(combo2.replace("_Input", ""));
                    const tanka = (clid[0] + "_" + clid[1] + "_" + clid[2] + "_" + "Tanka");
                    tanka.focus();
                }
            }
        </script>
        <script type="text/javascript">
            function TankaKeyDown(ta) {
                var tanka = document.getElementById(ta);
                if ((tanka.which == 13)
                ) {
                    tanka.which = null;
                    return false;
                }
                if ((tanka.which == 9)) {
                    var combo2 = document.activeElement.id;
                    var clid = combo2.split("_");
                    var combo3 = $find(combo2.replace("_Input", ""));
                    const shiire = document.getElementById('ShiireTanka');
                    shiire.focus();
                }
            }
        </script>
        <script type="text/javascript">
            function ShiireKeyDown(shi) {
                var shiire = document.getElementById(shi);
                if ((shiire.which == 13)
                ) {
                    shiire.which = null;
                    return false;
                }
                if ((shiire.which == 9)) {
                    var combo2 = document.activeElement.id;
                    var clid = combo2.split("_");
                    var combo3 = $find(combo2.replace("_Input", ""));

                    const bar = document.getElementById('BtnAddRow');
                    bar.focus();
                }
            }
        </script>
        <script type="text/javascript">
            function Close(ss) {
                let ss2 = document.getElementById(ss);
                ss2.style.display = "none";
            }
        </script>
        <telerik:RadAjaxManager runat="server" ID="Ram" OnAjaxRequest="Ram_AjaxRequest">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="DivDataGrid">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="CtrlSyousai" LoadingPanelID="Lp" UpdatePanelRenderMode="Block" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel runat="server" ID="Lp" Skin="Telerik" Height="500px">
        </telerik:RadAjaxLoadingPanel>
    </form>
</body>
</html>
