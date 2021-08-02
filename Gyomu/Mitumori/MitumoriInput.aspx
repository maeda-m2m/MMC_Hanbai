<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MitumoriInput.aspx.cs" Inherits="Gyomu.Mitumori.MitumoriInput" %>

<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Src="~/Common/CtrlFilter.ascx" TagName="CtlFilter" TagPrefix="uc3" %>
<%@ Register Src="~/Mitumori/CtlMitumoriSyosai.ascx" TagName="Syosai" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Style/Grid.ykuri.css" rel="STYLESHEET" />
    <link href="../Style/ComboBox.ykuri.css" type="text/css" rel="STYLESHEET" />
    <link href="../MainStyle.css" rel="Stylesheet" type="text/css" />
    <link href="Seikyu.css" type="text/css" rel="stylesheet" />


    <style type="text/css">
        .Hedar {
            background-color: #00008B;
            color: #FFFFFF;
            padding: 0.5em;
            font-size: 10px;
        }

        .hoka {
            font-size: 10px;
            padding: 0.5em;
        }

        .tablesize {
            width: 100%;
        }

        .right {
            text-align: right;
            width: 100%;
        }

        .title {
            width: 60px;
            background-color: gray;
            color: white;
            font-size: 10px;
        }

        .Tsize {
            width: 100%;
        }

        .size {
            font-size: 10px
        }

        .classWidth {
            width: 50px;
        }
    </style>

    <script type="text/javascript" src="../../Core.js"></script>

    <script type="text/javascript">

        function Close() {
            window.close();
        }

        function CtnRowDlt(cnt) {
            document.forms[0].count.value = cnt;
            var btn = document.getElementById('BtnDlt');
            btn.click();
        }

        function CtnRowCopy(cnt) {
            document.forms[0].count.value = cnt;
            var btn = document.getElementById('BtnCopy');
            btn.click();
        }

        function CntSetRow(cnt) {
            document.forms[0].count.value = cnt;
            var btnselect = document.getElementById('BtnSet');
            btnselect.click();
        }

        function CntRow(cnt) {
            document.forms[0].count.value = cnt;
            return;
        }

        function CalcGoukeiTax(TbxJSuryouId, TbxJTankaId, TbxJGoukeiId, TbxSyouhiZeiId, TbxHSuryouId, TbxHTankaId, TbxHGoukeiId, lblArariID, TaxZei) {



            var TbxJSuryou = document.getElementById(TbxJSuryouId);
            var TbxJTanka = document.getElementById(TbxJTankaId);
            var TbxJGoukei = document.getElementById(TbxJGoukeiId);
            var TbxSyouhiZei = document.getElementById(TbxSyouhiZeiId);

            var TbxHSuryou = document.getElementById(TbxHSuryouId);
            var TbxHTanka = document.getElementById(TbxHTankaId);
            var TbxHGoukei = document.getElementById(TbxHGoukeiId);

            var JSuryou = decommas(TbxJSuryou.value);
            var JTanka = decommas(TbxJTanka.value);

            var HSuryou = decommas(TbxHSuryou.value);
            var HTanka = decommas(TbxHTanka.value);

            var TbxArari = document.getElementById(lblArariID);
            var Arari = 0;

            var TbxTax = document.getElementById(TaxZei);

            if ((JSuryou && !isNaN(JSuryou)) && (JTanka && !isNaN(JTanka))) {
                var JGoukei = eval(JSuryou) * eval(JTanka);

                if (TbxTax == null) {
                    var SyohiZei = eval(JGoukei) * 0;
                }
                else {
                    var SyohiZei = eval(JGoukei) * 0.10;
                }

                Arari += JGoukei;

                TbxJGoukei.value = Round(Round(JGoukei));
                TbxSyouhiZei.value = Round(SyohiZei);
                TbxArari.value = Round(Arari);
            }
            else {
                TbxJGoukei.value = "";
                TbxSyouhiZei.value = "";
            }

            if ((HSuryou && !isNaN(HSuryou)) && (HTanka && !isNaN(HTanka))) {
                var HGoukei = eval(HSuryou) * eval(HTanka);
                Arari -= HGoukei;

                TbxHGoukei.value = Round(Round(HGoukei));
                TbxArari.value = Round(Arari);
            }
            else {
                TbxHGoukei.value = "";
            }
            CreateGoukei();
        }

        //小数点以下０位四捨五入
        function Round(goukei) {
            return String(goukei);
        }

        // カンマを付加した文字列を返す
        function encommas(value) {
            return String(value).replace(/((?:^[-+])?\d{1,3})(?=(?:\d\d\d)+(?!\d))/g, '$1,');
        }

        // カンマを外した文字列を返す
        function decommas(value) {
            return value.replace(/,/g, '');
        }

        function CreateGoukei() {
            var HidCsv_TbxJutyuGoukei = document.getElementById('<%# HidCsv_TbxJutyuGoukei.ClientID %>');
            var HidCsv_TbxHattyuGoukei = document.getElementById('<%# HidCsv_TbxHattyuGoukei.ClientID %>');
            var HidCsv_TbxSyouhiZei = document.getElementById('<%# HidCsv_TbxSyouhiZei.ClientID %>');
            var HidCsv_TbxArari = document.getElementById('<%# HidCsv_TbxArari.ClientID %>');
            var HidCsv_Suryo = document.getElementById('<%# HidCsv_Suryo.ClientID %>');

            var ary_TbxTbxJutyuGoukeiId = HidCsv_TbxJutyuGoukei.value.split(',');
            var ary_TbxHaccyuGoukeiId = HidCsv_TbxHattyuGoukei.value.split(',');
            var ary_TbxSyohizeiId = HidCsv_TbxSyouhiZei.value.split(',');
            var ary_TbxArariId = HidCsv_TbxArari.value.split(',');
            var ary_TbxSuryoId = HidCsv_Suryo.value.split(',');



            var ShiireGokei = 0;
            var Gokei = 0;
            var Syohizei = 0;
            var Suryo = 0;
            var Arari = 0;
            var Ritu = 0;

            for (var i = 0; i < ary_TbxTbxJutyuGoukeiId.length; i++) {
                var TbxJutyu = document.getElementById(ary_TbxTbxJutyuGoukeiId[i]);
                var TbxHattyu = document.getElementById(ary_TbxHaccyuGoukeiId[i]);
                var TbxSyohiZei = document.getElementById(ary_TbxSyohizeiId[i]);
                var TbxArari = document.getElementById(ary_TbxArariId[i]);
                var TbxSosu = document.getElementById(ary_TbxSuryoId[i]);

                var strGoukei = decommas(TbxJutyu.value);
                var strShiireGoke = decommas(TbxHattyu.value);
                var strSyouhiZei = decommas(TbxSyohiZei.value);
                var strArari = decommas(TbxArari.value);
                var strSosu = decommas(TbxSosu.value);

                if (strGoukei != "" && !isNaN(strGoukei)) {
                    Gokei += eval(strGoukei);
                }

                if (strShiireGoke != "" && !isNaN(strShiireGoke)) {
                    ShiireGokei += eval(strShiireGoke);
                }

                if (strSyouhiZei != "" && !isNaN(strSyouhiZei)) {
                    Syohizei += eval(strSyouhiZei);
                }

                if (strSosu != "" && !isNaN(strSosu)) {
                    Suryo += eval(strSosu);
                }

                if (strArari != "" && !isNaN(strArari)) {
                    Arari += eval(strArari);
                }
            }

            //総合計 金額合計 +消費税
            var lblTotal = document.getElementById('<%#lblTotal.ClientID %>');
            //仕入合計 発注合計
            var lblShiireGokei = document.getElementById('<%#lblShiireGokei.ClientID %>');
            //金額合計 受注合計
            var lblGokei = document.getElementById('<%#lblGokei.ClientID %>');
            //消費税
            var lblSyouhizei = document.getElementById('<%#lblSyouhizei.ClientID %>');
            //総数量
            var lblSuryou = document.getElementById('<%#lblSuryou.ClientID %>');
            //粗利合計
            var lblArariGokei = document.getElementById('<%#lblArariGokei.ClientID %>');
            //粗利率 粗利/合計金額
            var lblArariritu = document.getElementById('<%#lblArariritu.ClientID %>');

            lblTotal.value = Round(Gokei + Syohizei);
            lblShiireGokei.value = Round(ShiireGokei);
            lblGokei.value = Round(Gokei);
            lblSyouhizei.value = Round(Syohizei);
            lblSuryou.value = Round(Suryo);
            lblArariGokei.value = Round(Arari);
            Ritu = Round((Arari / Gokei) * 100)
            lblArariritu.value = Round(Ritu);
        }


        //キー押下時 
        window.document.onkeydown = onKeyDown;

        //BackSpaceキー押下防止 
        function onKeyDown(e) {

            var activeElementId = document.activeElement.id;

            // ﾌｫｰｶｽがどこにもない状態でBackSpaceは無効にする。
            if (activeElementId == null || activeElementId == '') {
                if (event.keyCode == 8 || event.keyCode == 116) {
                    return false;
                }
            }

            // Enterでの登録を無効にする。
            if (event.keyCode == 13) {
                return false;
            }

            // 明細の数値テキストボックスに関しては、数値と.-のみ入力可能にする。
            if (
                activeElementId.indexOf('TbxjutyuSuryo') > 0 ||
                activeElementId.indexOf('TbxJutyuTanka') > 0 ||
                activeElementId.indexOf('lblJutyuKingaku') > 0) {
                var kc = event.keyCode;

                if (activeElementId.indexOf('TbxjutyuSuryo') > 0 && kc == 0x6E)
                    return false;
                if ((kc >= 37 && kc <= 40) || (kc >= 48 && kc <= 57) || (kc >= 96 && kc <= 105) ||
                    kc == 46 || kc == 8 || kc == 13 || kc == 9 || kc == 189 || kc == 190 || kc == 0x6D || kc == 0x6E)
                    return true;
                else
                    return false;
            }

            return true;
        }

        function isNum(tbxClientID) {
            if (tbxClientID != null && tbxClientID != "") {
                var tbx = document.getElementById(tbxClientID);
                if (tbx != null) {
                    var inputSuryou = decommas(tbx.value);

                    if (inputSuryou != "") {

                        if (!Number(inputSuryou) && inputSuryou != '0') {
                            alert('半角数字のみで入力して下さい');
                            tbx.focus();
                            MojiCheck = false;
                            return false;
                        }

                        tbx.value = Round(Number(inputSuryou));
                        MojiCheck = true;
                    }
                }
            }
        }

        var win = null;
        function OpenWinPost(target, w, h, etc) {
            win = window.open
                ("", target, "width=" + w + "px,height=" + h + "px,location=no,resizable=yes,scrollbars=yes" + etc);
            win.focus();
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="home" runat="server">
            <div id="MainMenu" runat="server">
                <uc:Menu ID="Menu" runat="server" />
                <br />
            </div>
            <telerik:RadTabStrip ID="RT" runat="server" Skin="Office2007" AutoPostBack="True" SelectedIndex="1">
                <Tabs>
                    <telerik:RadTab Text="見積一覧" Font-Size="12pt" NavigateUrl="~/Mitumori/MitumoriItiran.aspx">
                    </telerik:RadTab>
                    <telerik:RadTab Text="見積入力" Font-Size="12pt" NavigateUrl="~/Mitumori/MitumoriInput.aspx" Selected="True">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <br />
            <asp:Button ID="BtnToroku" runat="server" Text="登録" Width="100px" Style="height: 21px" OnClick="BtnToroku_Click" />
            <asp:Button ID="BtnJutyu" runat="server" Text="受注" Width="100px" Style="height: 21px" OnClick="BtnJutyu_Click" />

            <asp:Button ID="BtnSyusei" runat="server" Text="修正" Width="100px" Style="height: 21px" OnClick="BtnSyusei_Click" />
            <asp:Button ID="BtnDel" runat="server" Text="削除" Width="100px" Style="height: 21px" OnClick="BtnDel_Click" />
            <asp:Button ID="BtnBack" runat="server" Text="一覧に戻る" Width="100px" Style="height: 21px" OnClick="BtnBack_Click" />
            <br />
            <asp:Label ID="lblMes" runat="server" Text=""></asp:Label>
            <div class="tablesize" id="MitumoriGamen" runat="server">
                <table class="tablesize">
                    <tbody>
                        <tr>
                            <td>
                                <table border="1" class="tablesize">
                                    <tbody>
                                        <tr>
                                            <td class="title">見積入力</td>
                                            <td class="Hedar">NO</td>
                                            <td>
                                                <asp:Label ID="lblNo" runat="server" Text="" CssClass="size"></asp:Label>
                                            </td>
                                            <td class="Hedar">カテゴリー</td>
                                            <td>
                                                <asp:Label ID="lblCate" runat="server" Text="" CssClass="size"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="RadCate" runat="server" Width="120" Height="180" OnSelectedIndexChanged="RadCate_SelectedIndexChanged" AutoPostBack="true" OnItemsRequested="RadCate_ItemsRequested" AllowCustomText="True" EnableLoadOnDemand="True"></telerik:RadComboBox>
                                            </td>
                                            <td class="Hedar">部門</td>
                                            <td colspan="2">
                                                <telerik:RadComboBox ID="RadBumon" runat="server" Width="100" Height="180px" OnItemsRequested="RadBumon_ItemsRequested" AllowCustomText="True" EnableLoadOnDemand="True"></telerik:RadComboBox>
                                            </td>
                                            <td class="Hedar">担当者</td>
                                            <td colspan="2">
                                                <telerik:RadComboBox ID="RadTanto" runat="server" Width="110" EmptyMessage="-------" Height="180px" OnItemsRequested="RadTanto_ItemsRequested" AllowCustomText="True" EnableLoadOnDemand="True">
                                                </telerik:RadComboBox>
                                            </td>
                                            <td class="Hedar">登録者</td>
                                            <td colspan="3">
                                                <asp:Label ID="lblUser" runat="server" Text="" CssClass="size"></asp:Label>
                                            </td>
                                            <td class="Hedar">差出印字</td>
                                            <td>
                                                <telerik:RadComboBox ID="RadInji" runat="server" Width="80px" Culture="ja-JP">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="代表" Value="True" />
                                                        <telerik:RadComboBoxItem runat="server" Text="部門" Value="False" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td class="Hedar">差出日付</td>
                                            <td>
                                                <telerik:RadComboBox ID="RadDate" runat="server" Width="80px" Culture="ja-JP">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="有り" Value="True" />
                                                        <telerik:RadComboBoxItem runat="server" Text="無し" Value="False" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="title">得意先</td>
                                            <td colspan="2">
                                                <asp:Button ID="BtnTokuisaki" runat="server" Text="得意先詳細" Width="90" Height="30" OnClick="BtnTokuisaki_Click" />
                                            </td>
                                            <td class="Hedar">略名称</td>
                                            <td colspan="6">
                                                <telerik:RadComboBox ID="RadTokuiMeisyo" runat="server" Width="350" Height="180px" OnSelectedIndexChanged="RadTokuiMeisyo_SelectedIndexChanged" AutoPostBack="true" AllowCustomText="True" EnableLoadOnDemand="True" OnItemsRequested="RadTokuiMeisyo_ItemsRequested"></telerik:RadComboBox>
                                            </td>
                                            <td class="Hedar">仮宛先</td>
                                            <td>
                                                <asp:CheckBox ID="ChkKari" runat="server" CssClass="size" />
                                            </td>
                                            <td class="Hedar">見積日付</td>
                                            <td colspan="3">
                                                <telerik:RadDatePicker ID="RadDayMitumori" runat="server"></telerik:RadDatePicker>
                                            </td>
                                            <td colspan="2" class="hoka">リレー状況</td>
                                            <td class="hoka">印刷フォーム</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="title">請求先</td>
                                            <td colspan="2">
                                                <asp:Button ID="BtnSekyusaki" runat="server" Text="請求先詳細" Width="90" Height="30" OnClick="BtnSekyusaki_Click" />
                                            </td>
                                            <td class="Hedar">略名称</td>
                                            <td colspan="5">
                                                <telerik:RadComboBox ID="RadSekyuMeisyo" runat="server" Width="350" Height="180px" AllowCustomText="True" AutoPostBack="true" EnableLoadOnDemand="True" OnItemsRequested="RadTokuiMeisyo_ItemsRequested"></telerik:RadComboBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="使用期間" CssClass="size"></asp:Label>
                                                <telerik:RadComboBox ID="RadKikan" runat="server" Width="60px" Culture="ja-JP" OnSelectedIndexChanged="RadKikan_SelectedIndexChanged" AutoPostBack="true">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="" Value="" />
                                                        <telerik:RadComboBoxItem runat="server" Text="1日" Value="oneDay" />
                                                        <telerik:RadComboBoxItem runat="server" Text="2日" Value="TowDay" />
                                                        <telerik:RadComboBoxItem runat="server" Text="3日" Value="ThreeDay" />
                                                        <telerik:RadComboBoxItem runat="server" Text="4日" Value="fourDay" />
                                                        <telerik:RadComboBoxItem runat="server" Text="5日" Value="fiveDay" />
                                                        <telerik:RadComboBoxItem runat="server" Text="1ヶ月" Value="oneMonth" />
                                                        <telerik:RadComboBoxItem runat="server" Text="2ヶ月" Value="twoMonth" />
                                                        <telerik:RadComboBoxItem runat="server" Text="3ヶ月" Value="threeMonth" />
                                                        <telerik:RadComboBoxItem runat="server" Text="4ヶ月" Value="fourMonth" />
                                                        <telerik:RadComboBoxItem runat="server" Text="5ヶ月" Value="fiveMonth" />
                                                        <telerik:RadComboBoxItem runat="server" Text="6ヶ月" Value="sixMonth" />
                                                        <telerik:RadComboBoxItem runat="server" Text="1年" Value="oneYear" />
                                                        <telerik:RadComboBoxItem runat="server" Text="99年" Value="allYear" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                <asp:Label ID="Label3" runat="server" Text="日" CssClass="size"></asp:Label>
                                            </td>
                                            <td class="Hedar">掛率/100%</td>
                                            <td>
                                                <asp:Label ID="lblKakeritu" runat="server" Text="" CssClass="size"></asp:Label>
                                            </td>
                                            <td class="Hedar">照会日付</td>
                                            <td colspan="3">
                                                <telerik:RadDatePicker ID="RadDaySyoukai" runat="server"></telerik:RadDatePicker>
                                            </td>
                                            <td class="Hedar">数量合計</td>
                                            <td>
                                                <asp:TextBox ID="lblSuryou" runat="server" CssClass="size" Width="100"></asp:TextBox>
                                            </td>
                                            <td class="hoka">印刷フォーム</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="title">直送先</td>
                                            <td colspan="2">
                                                <asp:Button ID="BtnTyokuso" runat="server" Text="直送先詳細" Width="90" Height="30" OnClick="BtnTyokuso_Click" />
                                            </td>
                                            <td class="Hedar">略名称</td>
                                            <td colspan="5">

                                                <telerik:RadComboBox ID="RadTyokusoMeisyo" runat="server" Width="350" Height="180px" OnSelectedIndexChanged="RadTyokusoMeisyo_SelectedIndexChanged" AutoPostBack="true" EnableLoadOnDemand="True" OnItemsRequested="RadTyokusoMeisyo_ItemsRequested"></telerik:RadComboBox>

                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="使用期間複数" CssClass="size"></asp:Label>
                                                <asp:CheckBox ID="Chek" runat="server" CssClass="size" />

                                            </td>
                                            <td class="Hedar">税率</td>
                                            <td>
                                                <input type="text" id="TbxTax" runat="server" class="classWidth" readonly="readonly" />
                                            </td>
                                            <td class="Hedar">使用開始</td>
                                            <td colspan="3">
                                                <telerik:RadDatePicker ID="RadSiyouCalendar" runat="server" OnSelectedDateChanged="RadSiyouCalendar_SelectedDateChanged" AutoPostBack="true">
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td class="Hedar">金額合計</td>
                                            <td>
                                                <asp:TextBox ID="lblGokei" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="Hedar">仕入合計</td>
                                            <td>
                                                <asp:TextBox ID="lblShiireGokei" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="title">摘要</td>
                                            <td class="Hedar">1</td>
                                            <td colspan="5">
                                                <asp:TextBox ID="TbxTekiyou1" runat="server" Width="300"></asp:TextBox>
                                            </td>
                                            <td class="Hedar">2</td>
                                            <td colspan="2">
                                                <asp:TextBox ID="TbxTekiyou2" runat="server" Width="200"></asp:TextBox>
                                            </td>
                                            <td class="Hedar">税区分</td>
                                            <td>
                                                <telerik:RadComboBox ID="RadZeikubun" runat="server" Width="65" Culture="ja-JP" OnSelectedIndexChanged="RadZeikubun_SelectedIndexChanged" AutoPostBack="true">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="税込" Value="2" />
                                                        <telerik:RadComboBoxItem runat="server" Text="税抜" Value="1" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td class="Hedar">使用終了</td>
                                            <td colspan="3">
                                                <telerik:RadDatePicker ID="RadSyuryoCalendar" runat="server" DateInput-ReadOnly="true"></telerik:RadDatePicker>
                                            </td>
                                            <td class="Hedar">消費税</td>
                                            <td>
                                                <asp:TextBox ID="lblSyouhizei" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="Hedar">粗利合計</td>
                                            <td>
                                                <asp:TextBox ID="lblArariGokei" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="title">施設<asp:CheckBox ID="ChkHukusu" runat="server" CssClass="size" />
                                            </td>
                                            <td colspan="2">
                                                <asp:Button ID="BtnSisetu" runat="server" Text="施設詳細" Width="90" Height="30px" OnClick="BtnSisetu_Click" />
                                            </td>
                                            <td class="Hedar">略名称</td>
                                            <td colspan="4">

                                                <telerik:RadComboBox ID="RadShisetu" runat="server" Width="350" Height="180px" AutoPostBack="true" EnableLoadOnDemand="True" OnItemsRequested="RadShisetu_ItemsRequested" OnSelectedIndexChanged="RadShisetu_SelectedIndexChanged"></telerik:RadComboBox>

                                            </td>
                                            <td class="Hedar" id="Zasu" runat="server">
                                                <asp:Label ID="lblZasu" runat="server" Text="座数"></asp:Label>
                                            </td>
                                            <td id="TBZasu" runat="server">
                                                <telerik:RadComboBox ID="RadZasu" runat="server" Width="75" Culture="ja-JP" OnSelectedIndexChanged="RadZasu_SelectedIndexChanged" AutoPostBack="true">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="" Value="0" />
                                                        <telerik:RadComboBoxItem runat="server" Text="1" Value="1" />
                                                        <telerik:RadComboBoxItem runat="server" Text="51" Value="51" />
                                                        <telerik:RadComboBoxItem runat="server" Text="101" Value="101" />
                                                        <telerik:RadComboBoxItem runat="server" Text="201" Value="201" />
                                                        <telerik:RadComboBoxItem runat="server" Text="301" Value="301" />
                                                        <telerik:RadComboBoxItem runat="server" Text="401" Value="401" />
                                                        <telerik:RadComboBoxItem runat="server" Text="501" Value="501" />
                                                        <telerik:RadComboBoxItem runat="server" Text="601" Value="601" />
                                                        <telerik:RadComboBoxItem runat="server" Text="701" Value="701" />
                                                        <telerik:RadComboBoxItem runat="server" Text="801" Value="801" />
                                                        <telerik:RadComboBoxItem runat="server" Text="901" Value="901" />
                                                        <telerik:RadComboBoxItem runat="server" Text="1001" Value="1001" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td class="Hedar" id="Kaisu" runat="server">
                                                <asp:Label ID="lblKaisu" runat="server" Text="回数"></asp:Label>
                                            </td>
                                            <td id="TBKaisu" runat="server">
                                                <telerik:RadComboBox ID="RadKaisu" runat="server" Width="65" Culture="ja-JP" OnSelectedIndexChanged="RadKaisu_SelectedIndexChanged" AutoPostBack="true">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="" Value="0" />
                                                        <telerik:RadComboBoxItem runat="server" Text="1回/日" Value="1回/日" />
                                                        <telerik:RadComboBoxItem runat="server" Text="2回/日" Value="2回/日" />
                                                        <telerik:RadComboBoxItem runat="server" Text="3回/日" Value="3回/日" />
                                                        <telerik:RadComboBoxItem runat="server" Text="4回/日" Value="4回/日" />
                                                        <telerik:RadComboBoxItem runat="server" Text="5回/日" Value="5回/日" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td class="Hedar" id="Ryoukin" runat="server">
                                                <asp:Label ID="lblRyoukin" runat="server" Text="料金"></asp:Label></td>
                                            <td id="TBRyoukin" runat="server">
                                                <telerik:RadComboBox ID="RadRyokin" runat="server" Width="60" Culture="ja-JP" OnSelectedIndexChanged="RadRyokin_SelectedIndexChanged" AutoPostBack="true">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="" Value="0" />
                                                        <telerik:RadComboBoxItem runat="server" Text="無料" Value="無料" />
                                                        <telerik:RadComboBoxItem runat="server" Text="有料" Value="有料" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td class="Hedar" id="Basyo" runat="server">
                                                <asp:Label ID="lblBasyo" runat="server" Text="場所"></asp:Label></td>
                                            <td id="TBBasyo" runat="server">
                                                <telerik:RadComboBox ID="RadBasyo" runat="server" Width="60" Culture="ja-JP" OnSelectedIndexChanged="RadBasyo_SelectedIndexChanged" AutoPostBack="true">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="" Value="0" />
                                                        <telerik:RadComboBoxItem runat="server" Text="屋内" Value="屋内" />
                                                        <telerik:RadComboBoxItem runat="server" Text="野外" Value="野外" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td class="Hedar">総合計</td>
                                            <td>
                                                <asp:TextBox ID="lblTotal" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="Hedar">粗利率</td>
                                            <td>
                                                <asp:TextBox ID="lblArariritu" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr class="tablesize">
                            <td class="tablesize">
                                <div id="MainInput" runat="server" class="tablesize">
                                    <asp:GridView ID="G" runat="server" AutoGenerateColumns="false" ShowHeader="false" CellPadding="0" CssClass="tablesize">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <uc2:Syosai ID="CtlSyosai" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <asp:Button ID="BtnDlt" runat="server" OnClick="BtnDlt_Click" />
                                <asp:Button ID="BtnCopy" runat="server" OnClick="BtnCopy_Click" />
                                <div class="right">
                                    <asp:Button ID="BtnInsert" runat="server" Text="行追加" Width="100px" OnClick="BtnInsert_Click" Style="height: 21px" />
                                </div>





                            </td>
                        </tr>
                    </tbody>
                </table>
                <input type="hidden" id="count" runat="server" />
                <input id="HidMeisaiClientIdCsv" type="hidden" runat="server" />
            </div>
        </div>
        <asp:HiddenField ID="HidCsv_TbxHattyuGoukei" runat="server" />
        <asp:HiddenField ID="HidCsv_TbxJutyuGoukei" runat="server" />
        <asp:HiddenField ID="HidCsv_TbxSyouhiZei" runat="server" />
        <asp:HiddenField ID="HidCsv_TbxArari" runat="server" />
        <asp:HiddenField ID="HidCsv_Suryo" runat="server" />
        <telerik:RadAjaxManager ID="R" runat="server"></telerik:RadAjaxManager>
    </form>
</body>
</html>
