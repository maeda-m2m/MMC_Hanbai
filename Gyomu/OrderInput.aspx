<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderInput.aspx.cs" Inherits="Gyomu.OrderInput" %>

<%@ Register Src="~/CtrlMitsuSyousai.ascx" TagName="Syosai" TagPrefix="uc2" %>
<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="http://code.jquery.com/jquery-1.11.3.min.js"></script>
    <script src="http://code.jquery.com/jquery-migrate-1.2.1.min.js"></script>
    <script src="JavaScript.js"></script>
    <link href="Kaihatsu.css" type="text/css" rel="stylesheet" />
    <title></title>
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
        <br />
        <table id="SubMenu" runat="server">
            <tr>
                <td>
                    <asp:Button ID="Keisan" runat="server" Text="計算" CssClass="Btn11" OnClick="Keisan_Click" />

                </td>
                <td>
                    <asp:Button ID="Register" runat="server" Text="登録" CssClass="Btn11" OnClick="Button5_Click" OnClientClick="return Check(this)" />
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

                </td>
                <td>
                    <asp:Button ID="DelBtn" runat="server" Text="削除" CssClass="Btn11" OnClick="DelBtn_Click" OnClientClick="A()" />
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
        <br />

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
                        <asp:Button ID="Button1" runat="server" Text="得意先詳細" CssClass="Btn11" OnClick="Button1_Click" />
                        <script type="text/javascript">
                            function Meisai2(ss) {
                                debugger;
                                document.getElementById(ss).style.display = "";
                                document.getElementById("mInput").style.display = "none";
                                document.getElementById("CtrlSyousai").style.display = "none";
                                document.getElementById("head").style.display = "none";
                                document.getElementById("Button13").style.display = "none";
                                document.getElementById("SubMenu").style.display = "none";
                                document.getElementById("SubMenu2").style.display = "";
                                debugger;
                            }
                        </script>
                        <script type="text/javascript">
                            function Close2(ss) {
                                debugger;
                                document.getElementById(ss).style.display = "none";
                                document.getElementById("mInput").style.display = "";
                                document.getElementById("CtrlSyousai").style.display = "";
                                document.getElementById("head").style.display = "";
                                document.getElementById("Button13").style.display = "";
                                document.getElementById("SubMenu").style.display = "";
                                document.getElementById("SubMenu2").style.display = "none";
                                debugger;
                            }
                        </script>
                    </td>
                    <td colspan="4" class="waku">
                        <telerik:RadComboBox ID="RadComboBox1" runat="server" Culture="ja-JP" OnItemsRequested="RadComboBox1_ItemsRequested" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" AutoPostBack="True" OnSelectedIndexChanged="RadComboBox1_SelectedIndexChanged" Width="300px"></telerik:RadComboBox>
                        <script type="text/javascript">
                            function TokuisakiSelect(sender, eventArgs) {
                                var rcbTokuisaki = $find("<%=RadComboBox1.ClientID%>");
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
                                var Label1 = document.getElementById("<%=JutyuNo.ClientID%>");
                                var shimebi = document.getElementById('Shimebi');
                                var RadZeiKubun = $find('RadZeiKubun');
                                var TbxCustomer = document.getElementById('TbxCustomer');
                                var TbxTokuisakiCode = document.getElementById('TbxTokuisakiCode');
                                var TbxTokuisakiName = document.getElementById('TbxTokuisakiName');
                                var TbxTokuisakiFurigana = document.getElementById('TbxTokuisakiFurigana');
                                var TbxTokuisakiRyakusyo = document.getElementById('TbxTokuisakiRyakusyo');
                                var TbxTokuisakiStaff = document.getElementById('TbxTokuisakiStaff');
                                var TbxTokuisakiPostNo = document.getElementById('TbxTokuisakiPostNo');
                                var TbxTokuisakiAddress = document.getElementById('TbxTokuisakiAddress');
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
                                //入力
                                RadComboBox3.set_text(TokuisakiRyakusyo);
                                HidKakeritsu.value = Kakeritsu;
                                shimebi.innerText = Shimebi;
                                HidShimebi.value = Shimebi;
                                RadZeiKubun.set_text(Zeikubun);
                                TbxCustomer.value = CustomerCode;
                                TbxTokuisakiCode.value = TokuisakiCode;
                                TbxTokuisakiName.value = TokuisakiName1;
                                TbxTokuisakiFurigana.value = TokuisakiFurifana;
                                TbxTokuisakiRyakusyo.value = TokuisakiRyakusyo;
                                TbxTokuisakiStaff.value = TokuisakiStaff;
                                TbxTokuisakiPostNo.value = TokuisakiPostNo;
                                TbxTokuisakiAddress.value = TokuisakiAddress1 + TokuisakiAddress2;
                                TbxTokuisakiTEL.value = TokuisakiTEL;
                                TbxTokuisakiFax.value = TokuisakiFAX;
                                TbxTokuisakiDepartment.value = TokuisakiDepartment;
                                RcbTax.set_text(Zeikubun);
                                TbxKakeritsu.value = Kakeritsu;
                                RcbShimebi.set_text(Shimebi);
                                LblTantoStaffCode.innerText = TantoStaffCode;
                                Label1.innerText = TantoStaffCode;
                                HidTantoStaffCode.value = TantoStaffCode;
                                debugger;
                            }
                        </script>
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
                        <asp:Button ID="Button2" runat="server" Text="請求先詳細" CssClass="Btn11" OnClick="Button2_Click" />
                    </td>
                    <td colspan="4" class="waku">
                        <telerik:RadComboBox ID="RadComboBox3" runat="server" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" OnItemsRequested="RadComboBox3_ItemsRequested" Width="300px"></telerik:RadComboBox>
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
                        <asp:Button ID="Button3" runat="server" Text="納品先詳細" CssClass="Btn11" OnClick="Button3_Click" />
                    </td>
                    <td colspan="4" class="waku">
                        <telerik:RadComboBox ID="RadComboBox2" runat="server" Culture="ja-JP" OnItemsRequested="RadComboBox2_ItemsRequested" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" Width="300px"></telerik:RadComboBox>
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
                    <td colspan="9" class="waku">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
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

        <table style="width: 100%">

            <!-- ヘッダーテーブル -------------------------------------------------------------------------------------->
            <tr>
                <td>
                    <div id="DivDataGrid">
                        <asp:DataGrid runat="server" ID="CtrlSyousai" AutoGenerateColumns="False" OnItemDataBound="CtrlSyousai_ItemDataBound" OnItemCommand="CtrlSyousai_ItemCommand">
                            <Columns>

                                <asp:TemplateColumn HeaderStyle-BorderStyle="None" ItemStyle-BorderStyle="None" ItemStyle-Width="30px">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="RowNo" runat="server" Text="" Font-Size="25px"></asp:Label>
                                        <asp:Button ID="Button4" runat="server" Text="削除" CssClass="Btn11" CommandName="Del" />
                                        <asp:Button ID="BtnAddRow" runat="server" Text="明細挿入" Width="90px" CssClass="Btn11" CommandName="Add" />
                                        <asp:Button runat="server" ID="BtnCopyAdd" Text="明細複写" Width="90px" CssClass="Btn11" CommandName="Copy" />

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
        <asp:Button ID="Button13" runat="server" Text="計算" CssClass="Btn11" OnClick="Keisan_Click" OnClientClick="Cul();" />
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

        <!--明細部分------------------------------------------------------------------------------------------------>


        <!-- 得意先詳細--------------------------------------------------------------------------------------->
        <asp:Panel runat="server" ID="TokuisakiSyousai">
            <div>
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
                            <asp:HiddenField runat="server" ID="HidTantoStaffCode" />

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
                            <asp:Button runat="server" ID="BtnToMaster" Text="得意先マスターに移動" OnClick="BtnToMaster_Click" Width="180px" CssClass="Btn11" />
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="4" CssClass="waku" HorizontalAlign="Center">
                            <asp:Button runat="server" ID="BtnTouroku" Text="見積ヘッダーに適応" OnClick="BtnTouroku_Click" CssClass="Btn11" Width="180px" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
        </asp:Panel>
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
                            <asp:Button ID="Button11" runat="server" Text="直送先マスタに移動" CssClass="Btn11" Width="150px" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <script src="JavaScript.js"></script>

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

                permisisionstart = new Date(permisisionstart);
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
                RdpPermissionstart.set_selectedDate(permisisionstart);
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
                        //単価＆売上金額（売上金額　＝　掛率計算済「税込」標準単価　×　数量）
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
        <script>
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

    </form>
</body>
</html>
