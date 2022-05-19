﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrlMitsuSyousai.ascx.cs" Inherits="Gyomu.CtrlMitsuSyousai" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<link href="Kaihatsu.css" type="text/css" rel="stylesheet" />
<script src="http://code.jquery.com/jquery-migrate-1.2.1.min.js"></script>
<%--<script src="JavaScript.js"></script>--%>



<style type="text/css">
    .SujiText {
        text-align: right;
    }

    .Zasu {
        display: block;
    }

    .CategoryChabge {
        display: block;
    }

    .BtnMeiasai {
        text-align: center;
        width: 95px;
        display: inline-block;
        padding: 0.3em 1em;
        text-decoration: none;
        color: #5d5d5d;
        border: solid 2px #8c8c8c;
        border-radius: 3px;
        transition: .4s;
        font-family: Meiryo;
        background-color: #e6e6e6;
    }

        .BtnMeiasai:hover {
            background: #717171;
            color: black;
            border: solid 2px #ababab;
        }

    .MiniTitle2 {
        background-color: #c4c4c4;
        text-align: center;
        font-size: 12px;
        font: bold;
        color: black;
        width: 100px;
        height: 20px;
    }

    #RcbHanni {
        display: none;
    }

    #SyouhinSyousai {
        display: none;
    }

    #SisetuSyousai {
        display: none;
    }
</style>



<asp:Label ID="err" runat="server" Text="" ForeColor="#ff0000"></asp:Label>
<asp:Label ID="end" runat="server" Text="" ForeColor="Green"></asp:Label>
<div runat="server" style="width: 100%">
    <table class="HeadTB" runat="server">
        <tr>
            <td class="MeisaiProcd" runat="server" colspan="1" style="width: 200px;">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="LblProduct" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Button runat="server" Text="商品詳細" ID="BtnProductMeisai" CssClass="BtnMeiasai" />
                        </td>
                    </tr>
                </table>
                <script type="text/javascript">
                    function Meisai(ss) {
                        let ss2 = document.getElementById(ss);
                        ss2.style.display = "";
                    }
                </script>
                <input type="hidden" id="HidCategoryCode" runat="server" />
            </td>
            <td class="MeisaiProNm" runat="server" colspan="5">
                <telerik:RadComboBox ID="SerchProduct" EnableLoadOnDemand="true" ShowToggleImage="False" runat="server" AllowCustomText="True" ShowMoreResultsBox="True" EnableVirtualScrolling="True" OnItemsRequested="SerchProduct_ItemsRequested" EmptyMessage="" Width="500px" OnClientSelectedIndexChanged="select"></telerik:RadComboBox>
                <telerik:RadComboBox ID="SerchProductJouei" EnableLoadOnDemand="true" ShowToggleImage="False" runat="server" AllowCustomText="True" ShowMoreResultsBox="True" EnableVirtualScrolling="True" OnItemsRequested="SerchProductJouei_ItemsRequested" EmptyMessage="" Width="500px" OnSelectedIndexChanged="SerchProductJouei_SelectedIndexChanged" AutoPostBack="true"></telerik:RadComboBox>
                <asp:Button runat="server" ID="BtnTool1" ToolTip="登録する商品名を検索し、一覧上から選択。" Text="❔" OnClientClick="return false;" />
                <asp:HiddenField runat="server" ID="HidColor" />
                <asp:HiddenField runat="server" ID="HidCp" />
                <asp:HiddenField runat="server" ID="HidCpOver" />
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
                            var Syokai = SyokaiDate.get_selectedDate();
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
                            debugger;
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
                                debugger;
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
                <input type="hidden" runat="server" id="procd" />
                <script>
                    function SerchFocus(fcs) {
                        var ary = fcs.split('-');
                        var rcb = document.getElementById(ary[0]);
                        var btn = document.getElementById(ary[1]);
                        btn.focus();
                    }
                </script>
            </td>
            <td class="MeisaiMTD" runat="server">
                <%--                                <telerik:RadComboBox ID="Hanni" runat="server" Width="100px"></telerik:RadComboBox>--%>
                <asp:Label ID="LblHanni" runat="server" Text=""></asp:Label>
                <telerik:RadComboBox runat="server" ID="RcbHanni" EnableLoadOnDemand="true" ShowToggleImage="False" AllowCustomText="True" ShowMoreResultsBox="True" EnableVirtualScrolling="True" OnSelectedIndexChanged="RcbHanni_SelectedIndexChanged" CssClass="Zasu" ViewStateMode="Enabled" EnableViewState="true" OnItemsRequested="RcbHanni_ItemsRequested"></telerik:RadComboBox>
                <asp:HiddenField runat="server" ID="HidHanni" />
                <asp:HiddenField runat="server" ID="HidJoueiHanni" />
            </td>
            <td class="MeisaiMTD" runat="server">
                <asp:Label ID="Baitai" runat="server" Text=""></asp:Label>
                <asp:HiddenField runat="server" ID="HidMedia" />
            </td>
            <td class="MeisaisTD" runat="server">
                <asp:TextBox ID="Suryo" runat="server" Width="30px" Text="1" TextMode="Number"></asp:TextBox>
            </td>
            <td class="MeisaikTD" runat="server">
                <asp:TextBox ID="HyoujyunTanka" runat="server" Width="80px" Height="20px" CssClass="SujiText" EnableViewState="true" ViewStateMode="Enabled"></asp:TextBox>
                <input type="hidden" id="ht" runat="server" />
                <input type="hidden" id="zeiht" runat="server" />
            </td>
            <td class="MeisaikTD" runat="server">
                <asp:TextBox ID="Kingaku" runat="server" Width="80px" Height="20px" CssClass="SujiText"></asp:TextBox>
                <input type="hidden" id="kgk" runat="server" />
                <input type="hidden" id="zeikgk" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="5" class="MeisaiBTD" runat="server">
                <table>
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="ShiyouShisetsu" runat="server" Width="300px" OnItemsRequested="ShiyouShisetsu_ItemsRequested" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" OnClientSelectedIndexChanged="SelectFacility"></telerik:RadComboBox>
                            <script type="text/javascript">
                                function SelectFacility(sender, eventArgs) {
                                    debugger;
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
                        </td>
                        <td>
                            <asp:Button ID="BtnFacilityMeisai" runat="server" Text="施設詳細" CssClass="BtnMeiasai" CommandName="FacilityDetails" />
                        </td>
                    </tr>
                </table>
                <input type="hidden" runat="server" id="FacilityCode" />
                <asp:Label ID="Facility" runat="server" Text=""></asp:Label>
                <input type="hidden" runat="server" id="FacilityAddress" />
            </td>
            <td class="MeisaiMTD" runat="server">
                <%--                <telerik:RadComboBox ID="Zasu" runat="server" Width="100px" AllowCustomText="True" CssClass="Zasu" OnSelectedIndexChanged="Zasu_SelectedIndexChanged" AutoPostBack="true">
                    <Items>
                        <telerik:RadComboBoxItem Text="" Selected="true" />
                        <telerik:RadComboBoxItem Text="50" Value="50" runat="server" />
                        <telerik:RadComboBoxItem Text="100" Value="100" runat="server" />
                        <telerik:RadComboBoxItem Text="150" Value="150" runat="server" />
                        <telerik:RadComboBoxItem Text="200" Value="200" runat="server" />
                        <telerik:RadComboBoxItem Text="250" Value="250" runat="server" />
                        <telerik:RadComboBoxItem Text="300" Value="300" runat="server" />
                        <telerik:RadComboBoxItem Text="400" Value="400" runat="server" />
                        <telerik:RadComboBoxItem Text="500" Value="500" runat="server" />
                        <telerik:RadComboBoxItem Text="700" Value="700" runat="server" />
                        <telerik:RadComboBoxItem Text="800" Value="800" runat="server" />
                        <telerik:RadComboBoxItem Text="1000" Value="1000" runat="server" />
                        <telerik:RadComboBoxItem Text="1000以上" Value="1000以上" runat="server" />
                    </Items>
                </telerik:RadComboBox>--%>
                <table runat="server" id="TBZasu" style="width: 150px">
                    <tr>
                        <td>
                            <p style="font-size: 9px;">入力後はTabキーを押してください</p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="TbxZasu" Width="100px" TextMode="Number" AutoPostBack="true" OnTextChanged="TbxZasu_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                </table>

            </td>
            <td class="MeisaiMTD" runat="server">
                <telerik:RadDatePicker ID="StartDate" runat="server" Width="100px" CssClass="CategoryChabge"></telerik:RadDatePicker>
            </td>
            <td class="MeisaiMTD" runat="server">
                <telerik:RadDatePicker ID="EndDate" runat="server" Width="100px" CssClass="CategoryChabge"></telerik:RadDatePicker>
            </td>
            <td class="MeisaiTD" runat="server">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Kakeri" runat="server" Text=""></asp:Label><br />
                        </td>
                        <td>
                            <asp:Label ID="zeiku" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="MeisaikTD" runat="server">
                <asp:TextBox ID="Tanka" runat="server" Width="80px" Height="20px" CssClass="SujiText"></asp:TextBox>
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
                <input type="hidden" id="tk" runat="server" />
            </td>
            <td class="MeisaikTD" runat="server">
                <asp:TextBox ID="Uriage" runat="server" Width="80px" Height="20px" CssClass="SujiText"></asp:TextBox>
                <input type="hidden" id="ug" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="6" class="MeisaiBTD" runat="server">
                <telerik:RadComboBox runat="server" ID="Tekiyo" AllowCustomText="true" EnableLoadOnDemand="true" Width="500px">
                    <Items>
                        <telerik:RadComboBoxItem Text="1月分" Value="1月分" />
                        <telerik:RadComboBoxItem Text="2月分" Value="2月分" />
                        <telerik:RadComboBoxItem Text="3月分" Value="3月分" />
                        <telerik:RadComboBoxItem Text="4月分" Value="4月分" />
                        <telerik:RadComboBoxItem Text="5月分" Value="5月分" />
                        <telerik:RadComboBoxItem Text="6月分" Value="6月分" />
                        <telerik:RadComboBoxItem Text="7月分" Value="7月分" />
                        <telerik:RadComboBoxItem Text="8月分" Value="8月分" />
                        <telerik:RadComboBoxItem Text="9月分" Value="9月分" />
                        <telerik:RadComboBoxItem Text="10月分" Value="10月分" />
                        <telerik:RadComboBoxItem Text="11月分" Value="11月分" />
                        <telerik:RadComboBoxItem Text="12月分" Value="12月分" />
                        <telerik:RadComboBoxItem Text="屋内" Value="屋内" />
                        <telerik:RadComboBoxItem Text="屋外" Value="屋外" />
                        <telerik:RadComboBoxItem Text="キャンペーン" Value="キャンペーン" />

                    </Items>
                </telerik:RadComboBox>
            </td>
            <td colspan="2" class="MeisaiTD" runat="server">
                <telerik:RadComboBox ID="Hachu" runat="server" Culture="ja-JP" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" OnItemsRequested="Hachu_ItemsRequested" OnSelectedIndexChanged="Hachu_SelectedIndexChanged" AutoPostBack="true"></telerik:RadComboBox>
            </td>
            <td class="MeisaiTD" runat="server">
                <asp:Label ID="WareHouse" runat="server" Text="不明"></asp:Label>
            </td>
            <td class="MeisaikTD" runat="server">
                <asp:TextBox ID="ShiireTanka" runat="server" Width="80px" Height="20px" CssClass="SujiText"></asp:TextBox>
                <input type="hidden" id="st" runat="server" />
                <input type="hidden" id="zeist" runat="server" />
            </td>
            <td class="MeisaikTD" runat="server">
                <asp:TextBox ID="ShiireKingaku" runat="server" Width="80px" Height="20px" CssClass="SujiText"></asp:TextBox>
                <input type="hidden" id="sk" runat="server" />
                <input type="hidden" id="zeisk" runat="server" />
            </td>
        </tr>
    </table>
    <input type="hidden" id="category" runat="server" />
    <input type="hidden" id="zeikubun" runat="server" />


</div>
<asp:Panel runat="server" ID="SyouhinSyousai">
    <div>
        <table>
            <tr>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>商品コード<span style="color: red">*</span></p>
                </td>
                <td class="waku">
                    <asp:TextBox runat="server" ID="TbxProductCode"></asp:TextBox>
                </td>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>仕入コード<span style="color: red"></span></p>
                </td>
                <td class="waku">
                    <asp:Label runat="server" ID="LblShiireCode"></asp:Label>
                    <asp:HiddenField runat="server" ID="HidShiireCode" />
                </td>
            </tr>
            <tr>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>商品名<span style="color: red">*</span></p>
                </td>
                <td class="waku">
                    <asp:TextBox runat="server" ID="TbxProductName"></asp:TextBox>
                </td>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>仕入名<span style="color: red">*</span></p>
                </td>
                <td class="waku">
                    <telerik:RadComboBox runat="server" ID="RcbShiireName" OnItemsRequested="RcbShiireName_ItemsRequested" OnSelectedIndexChanged="RcbShiireName_SelectedIndexChanged" AllowCustomText="true" EnableLoadOnDemand="true" EnableVirtualScrolling="true" ShowMoreResultsBox="true"></telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>メーカー品番<span style="color: red">*</span></p>
                </td>
                <td class="waku">
                    <asp:TextBox runat="server" ID="TbxMakerNo"></asp:TextBox>
                </td>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>仕入価格<span style="color: red">*</span></p>
                </td>
                <td class="waku">
                    <asp:TextBox runat="server" ID="TbxShiirePrice"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>媒体<span style="color: red">*</span></p>
                </td>
                <td class="waku">
                    <telerik:RadComboBox runat="server" ID="RcbMedia">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="" Value="" />
                            <telerik:RadComboBoxItem runat="server" Text="DVD" Value="DVD" />
                            <telerik:RadComboBoxItem runat="server" Text="BD" Value="BD" />
                            <telerik:RadComboBoxItem runat="server" Text="CD" Value="CD" />
                            <telerik:RadComboBoxItem runat="server" Text="HD" Value="HD" />
                            <telerik:RadComboBoxItem runat="server" Text="LN" Value="LN" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>キャンペーン価格</p>
                </td>
                <td class="waku">
                    <asp:TextBox runat="server" ID="TbxCpKakaku"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>範囲<span style="color: red">*</span></p>
                </td>
                <td class="waku">
                    <asp:TextBox runat="server" ID="TbxHanni"></asp:TextBox>
                </td>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>キャンペーン仕入</p>
                </td>
                <td class="waku">
                    <asp:TextBox runat="server" ID="TbxCpShiire"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>カテゴリーコード</p>
                </td>
                <td class="waku">
                    <asp:Label runat="server" ID="LblCateCode"></asp:Label>
                </td>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>キャンペーン開始</p>
                </td>
                <td class="waku">
                    <telerik:RadDatePicker ID="RdpCpStart" runat="server"></telerik:RadDatePicker>
                </td>

            </tr>
            <tr>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>カテゴリー名</p>
                </td>
                <td class="waku">
                    <asp:Label runat="server" ID="LblCategoryName"></asp:Label>
                </td>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>キャンペーン終了</p>
                </td>
                <td class="waku">
                    <telerik:RadDatePicker ID="RdpCpEnd" runat="server"></telerik:RadDatePicker>
                </td>
            </tr>

            <tr>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>直送先</p>
                </td>
                <td class="waku">
                    <asp:TextBox runat="server" ID="TbxTyokuso"></asp:TextBox>
                </td>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>倉庫</p>
                </td>
                <td class="waku">
                    <asp:TextBox runat="server" ID="TbxWareHouse"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>標準価格<span style="color: red">*</span></p>
                </td>
                <td class="waku">
                    <asp:TextBox runat="server" ID="TbxHyoujun"></asp:TextBox>
                </td>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>利用状態</p>
                </td>
                <td class="waku">
                    <asp:TextBox runat="server" ID="TbxRiyo"></asp:TextBox>
                </td>

            </tr>

            <tr>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>許諾開始</p>
                </td>
                <td class="waku">
                    <telerik:RadDatePicker ID="RdpPermissionstart" runat="server"></telerik:RadDatePicker>
                </td>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>許諾終了</p>
                </td>
                <td class="waku">
                    <telerik:RadDatePicker ID="RdpRightEnd" runat="server"></telerik:RadDatePicker>
                </td>

            </tr>
            <tr>
                <td colspan="4" style="text-align: center" class="waku">
                    <p><span style="color: red">*</span>は必須項目です。</p>
                    <asp:Button runat="server" ID="BtnInsert" OnClick="BtnInsert_Click" Text="商品明細に反映" CssClass="BtnMeiasai" Width="125px" />
                    &nbsp;
                    &nbsp;
                    <asp:Button runat="server" ID="BtnClose" Text="閉じる" CssClass="BtnMeiasai" />
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>

<asp:Panel runat="server" ID="SisetuSyousai">
    <div>
        <table>
            <tr>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>施設コード<span style="color: red;">*</span></p>
                </td>
                <td class="waku">
                    <asp:TextBox ID="TbxFacilityCode" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>施設行コード<span style="color: red;">*</span></p>
                </td>
                <td class="waku">
                    <asp:TextBox ID="TbxFacilityRowCode" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>市町村コード</p>
                </td>
                <td class="waku">
                    <telerik:RadComboBox runat="server" ID="RcbCity" placeholder="市町村名を入力"></telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>施設名1<span style="color: red;">*</span></p>
                </td>
                <td class="waku">
                    <asp:TextBox ID="TbxFacilityName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>施設名2</p>
                </td>
                <td class="waku">
                    <asp:TextBox ID="TbxFacilityName2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>施設名略称<span style="color: red;">*</span></p>
                </td>
                <td class="waku">
                    <asp:TextBox ID="TbxFaci" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>施設担当者名</p>
                </td>
                <td class="waku">
                    <asp:TextBox runat="server" ID="TbxFacilityResponsible"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>郵便番号</p>
                </td>
                <td class="waku">
                    <asp:TextBox ID="TbxYubin" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>施設住所<span style="color: red;">*</span></p>
                </td>
                <td class="waku">
                    <asp:TextBox ID="TbxFaciAdress" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="MiniTitle2" style="width: 120px; height: 20px;">
                    <p>電話番号</p>
                </td>
                <td class="waku">
                    <asp:TextBox ID="TbxTel" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="waku">&nbsp;
                    <asp:Button ID="BtnUpdateFaci" runat="server" Text="商品明細に反映" CssClass="BtnMeiasai" OnClick="BtnUpdateFaci_Click" Width="140px" OnClientClick="BtnUpdateFaci()" />
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

                    &nbsp;
                    <asp:Button ID="Button4" runat="server" Text="閉じる" CssClass="BtnMeiasai" OnClick="Button1_Click" />
                    <script type="text/javascript">
                        function Close(ss) {
                            let ss2 = document.getElementById(ss);
                            ss2.style.display = "none";
                        }
                    </script>
                </td>

            </tr>

        </table>
    </div>
</asp:Panel>


