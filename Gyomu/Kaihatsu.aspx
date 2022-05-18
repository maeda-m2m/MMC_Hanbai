<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Kaihatsu.aspx.cs" Inherits="Gyomu.Kaihatsu" %>

<%@ Register Src="~/CtrlMitsuSyousai.ascx" TagName="Syosai" TagPrefix="uc2" %>
<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="Kaihatsu.css" type="text/css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.1.0.min.js"></script>
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

        #overlay {
            position: fixed;
            top: 0;
            z-index: 100;
            width: 100%;
            height: 100%;
            display: none;
            background: rgba(0,0,0,0.6);
        }

        .cv-spinner {
            height: 100%;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .spinner {
            width: 40px;
            height: 40px;
            border: 4px #ddd solid;
            border-top: 4px #2e93e6 solid;
            border-radius: 50%;
            animation: sp-anime 0.8s infinite linear;
        }

        @keyframes sp-anime {
            100% {
                transform: rotate(360deg);
            }
        }

        .is-hide {
            display: none;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div id="overlay">
            <div class="cv-spinner">
                <span class="spinner"></span>
                <script type="text/javascript">
                    jQuery(function ($) {
                        debugger;
                        $(document).ajaxSend(function () {
                            $("#overlay").fadeIn(300);
                        });

                        $('#Button5').click(function () {
                            $.ajax({
                                type: 'GET',
                                success: function (data) {
                                    console.log(data);
                                }
                            }).done(function () {
                                setTimeout(function () {
                                    $("#overlay").fadeOut(300);
                                }, 10000000);
                            });
                        });

                        $('#Button7').click(function () {
                            $.ajax({
                                type: 'GET',
                                success: function (data) {
                                    console.log(data);
                                }
                            }).done(function () {
                                setTimeout(function () {
                                    $("#overlay").fadeOut(300);
                                }, 1000);
                            });
                        });

                        $('#Button8').click(function () {
                            debugger;
                            $.ajax({
                                type: 'GET',
                                success: function (data) {
                                    console.log(data);
                                }
                            }).done(function () {
                                setTimeout(function () {
                                    $("#overlay").fadeOut(300);
                                }, 1000);
                            });
                        });

                        $('#RadComboCategory').change(function () {
                            debugger;
                            $.ajax({
                                type: 'GET',
                                success: function (data) {
                                    console.log(data);
                                }
                            }).done(function () {
                                setTimeout(function () {
                                    $("#overlay").fadeOut(300);
                                }, 1000);
                            });
                        });

                        $('#RadComboBox1').change(function () {
                            debugger;
                            $.ajax({
                                type: 'GET',
                                success: function (data) {
                                    console.log(data);
                                }
                            }).done(function () {
                                setTimeout(function () {
                                    $("#overlay").fadeOut(300);
                                }, 1000);
                            });
                        });

                        $('#DropDownList9').change(function () {
                            debugger;
                            $.ajax({
                                type: 'GET',
                                success: function (data) {
                                    console.log(data);
                                }
                            }).done(function () {
                                setTimeout(function () {
                                    $("#overlay").fadeOut(300);
                                }, 1000);
                            });
                        });

                        $('#CheckBox4').Check(function () {
                            debugger;
                            $.ajax({
                                type: 'GET',
                                success: function (data) {
                                    console.log(data);
                                }
                            }).done(function () {
                                setTimeout(function () {
                                    $("#overlay").fadeOut(300);
                                }, 1000);
                            });
                        });

                        $('#CheckBox5').click(function () {
                            debugger;
                            $.ajax({
                                type: 'GET',
                                success: function (data) {
                                    console.log(data);
                                }
                            }).done(function () {
                                setTimeout(function () {
                                    $("#overlay").fadeOut(300);
                                }, 1000);
                            });
                        });


                        $('.Btn10').click(function () {
                            debugger;
                            $.ajax({
                                type: 'GET',
                                success: function (data) {
                                    console.log(data);
                                }
                            }).done(function () {
                                setTimeout(function () {
                                    $("#overlay").fadeOut(300);
                                }, 1000);
                            });
                        });

                        //$('#CheckBox5').click(function () {
                        //    debugger;
                        //    $.ajax({
                        //        type: 'GET',
                        //        success: function (data) {
                        //            console.log(data);
                        //        }
                        //    }).done(function () {
                        //        setTimeout(function () {
                        //            $("#overlay").fadeOut(300);
                        //        }, 100000);
                        //    });
                        //});

                        //$('#CheckBox5').click(function () {
                        //    debugger;
                        //    $.ajax({
                        //        type: 'GET',
                        //        success: function (data) {
                        //            console.log(data);
                        //        }
                        //    }).done(function () {
                        //        setTimeout(function () {
                        //            $("#overlay").fadeOut(300);
                        //        }, 100000);
                        //    });
                        //});

                        //$('#CheckBox5').click(function () {
                        //    debugger;
                        //    $.ajax({
                        //        type: 'GET',
                        //        success: function (data) {
                        //            console.log(data);
                        //        }
                        //    }).done(function () {
                        //        setTimeout(function () {
                        //            $("#overlay").fadeOut(300);
                        //        }, 100000);
                        //    });
                        //});
                    });
                </script>
            </div>
        </div>
        <div runat="server" id="DivMenu">
            <uc:Menu ID="Menu" runat="server" />
        </div>
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
                    <asp:Button ID="BtnKeisan" runat="server" Text="計算" CssClass="Btn10" OnClientClick="Cul(); return false;" />
                </td>
                <td>
                    <asp:Button ID="Button5" runat="server" Text="登録" CssClass="Btn10" OnClick="Button5_Click" OnClientClick="return Check(this)" />
                    <script type="text/javascript">
                        function Check() {
                            var bool = true;
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
                                            RadComboBox1._element.style.border = "solid 2px red";
                                            RadComboBox1._element.style.backgroundColor = "#ffdce4";
                                            bool = false;
                                            break;
                                        }
                                        if (FacilityRad.get_text() != "") {

                                        }
                                        else {
                                            FacilityRad._element.style.border = "solid 2px red";
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
                                            RadComboBox1._element.style.border = "solid 2px red";
                                            bool = false;
                                        }
                                        if (FacilityRad.get_text() != "") {

                                        }
                                        else {
                                            FacilityRad._element.style.border = "solid 2px red";
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
                                var Zasu = document.getElementById(CtrlSyousai + "_" + ctl + "0" + x + "_" + Syosai + "_" + "TbxZasu");
                                var catecode = RadComboCategory.get_selectedItem().get_value();
                                switch (catecode) {
                                    case '101':
                                    case '102':
                                    case '103':
                                    case '199':
                                        if (StartDate.get_selectedDate.get_value == "") {
                                            StartDate._element.style.border = "solid 2px red";
                                            bool = false;
                                        }
                                        if (EndDate.get_selectedDate.get_value == "") {
                                            EndDate._element.style.border = "solid 2px red";
                                            bool = false;
                                        }
                                        if (RcbHanni.get_text == "") {
                                            RcbHanni._element.style.border = "solid 2px red";
                                            bool = false;
                                        }
                                        break;
                                    case '205':
                                        if (StartDate.get_selectedDate.get_value == "") {
                                            StartDate._element.style.border = "solid 2px red";
                                            bool = false;
                                        }
                                        if (EndDate.get_selectedDate.get_value == "") {
                                            EndDate._element.style.border = "solid 2px red";
                                            bool = false;
                                        }
                                        if (RcbHanni.get_text == "") {
                                            RcbHanni._element.style.border = "solid 2px red";
                                            bool = false;
                                        }
                                        if (Zasu.value == "") {
                                            Zasu.style.border = "solid 2px red";
                                            bool = false;
                                        }
                                        break;
                                    default:
                                        if (StartDate.get_selectedDate.get_value == "") {
                                            StartDate._element.style.border = "solid 2px red";
                                            bool = false;
                                        }
                                        if (EndDate.get_selectedDate.get_value == "") {
                                            EndDate._element.style.border = "solid 2px red";
                                            bool = false;
                                        }
                                }
                                if (SerchProduct.get_text() == "") {
                                    SerchProduct._element.style.border = "solid 2px red";
                                    bool = false;
                                }
                                if (Baitai.value == "") {
                                    Baitai.value = "媒体が表示されていません。商品詳細を確認して下さい。";
                                    bool = false;
                                }
                                if (HyoujyunTanka.value == "") {
                                    HyoujyunTanka.style.border = "solid 2px red";
                                    bool = false;
                                }
                                if (Kingaku.value == "") {
                                    Kingaku.style.border = "solid 2px red";
                                    bool = false;
                                }
                                if (Tanka.value == "") {
                                    Tanka.style.border = "solid 2px red";
                                    bool = false;
                                }
                                if (Uriage.value == "") {
                                    Uriage.style.border = "solid 2px red";
                                    bool = false;
                                }
                                if (ShiyouShisetsu.get_text() == "") {
                                    ShiyouShisetsu._element.style.border = "solid 2px red";
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
                                    ShiireTanka.style.border = "solid 2px red";
                                    bool = false;
                                }
                                if (ShiireKingaku.value == "") {
                                    ShiireKingaku.style.border = "solid 2px red";
                                    bool = false;
                                }
                            }
                            if (bool == false) {
                                debugger;
                                Err.innerText = "赤枠を入力して下さい";
                                return false;
                            }
                            Cul();
                        }
                    </script>
                </td>
                <td>
                    <asp:Button ID="Button7" runat="server" Text="複写" CssClass="Btn10" OnClick="Button7_Click" />
                    <script type="text/javascript">
                        function Copy() {
                            document.getElementById('Label94').innerHTML = "";
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
                                return (false)
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
                    <asp:Button ID="Button6" runat="server" Text="戻る" CssClass="Btn10" />
                </td>
            </tr>
        </table>
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
                        <telerik:RadComboBox ID="RadComboCategory" runat="server" OnItemsRequested="RadComboCategory_ItemsRequested" AllowCustomText="True" EnableLoadOnDemand="True" MarkFirstMatch="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" Width="100px" OnSelectedIndexChanged="RadComboCategory_SelectedIndexChanged" AutoPostBack="true"></telerik:RadComboBox>
                        <input type="hidden" id="CategoryCode" runat="server" />
                    </td>
                    <td class="MiniTitle">
                        <p>部門</p>
                    </td>
                    <td class="waku">
                        <telerik:RadComboBox ID="RadComboBox4" runat="server" Width="100px"></telerik:RadComboBox>
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
                        <telerik:RadDatePicker ID="RadDatePicker1" runat="server" Width="100px">
                            <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" Culture="ja-JP" FastNavigationNextText="&amp;lt;&amp;lt;"></Calendar>

                            <DateInput runat="server" DisplayDateFormat="yyyy/MM/dd" DateFormat="yyyy/MM/dd" LabelWidth="40%">
                                <EmptyMessageStyle Resize="None"></EmptyMessageStyle>

                                <ReadOnlyStyle Resize="None"></ReadOnlyStyle>

                                <FocusedStyle Resize="None"></FocusedStyle>

                                <DisabledStyle Resize="None"></DisabledStyle>

                                <InvalidStyle Resize="None"></InvalidStyle>

                                <HoveredStyle Resize="None"></HoveredStyle>

                                <EnabledStyle Resize="None"></EnabledStyle>
                            </DateInput>

                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                        </telerik:RadDatePicker>
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
                        <asp:Button ID="Button1" runat="server" Text="得意先詳細" CssClass="Btn10" />
                        <script type="text/javascript">
                            function Meisai2(ss) {
                                debugger;
                                document.getElementById(ss).style.display = "";
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
                            function Close2(ss) {
                                document.getElementById(ss).style.display = "none";
                                document.getElementById("mInput").style.display = "";
                                document.getElementById("CtrlSyousai").style.display = "";
                                document.getElementById("head").style.display = "";
                                document.getElementById("TBAddRow").style.display = "";
                                document.getElementById("SubMenu").style.display = "";
                                document.getElementById("SubMenu2").style.display = "none";
                                document.getElementById("DivDataGrid").style.display = "";
                                document.getElementById("NouhinsakiPanel").style.display = "none";
                                document.getElementById("TBTokuisaki").style.display = "none";
                                document.getElementById("TBSeikyusaki").style.display = "none";
                                document.getElementById("TBFacilityDetail").style.display = "none";
                            }
                        </script>
                    </td>
                    <td colspan="4" class="waku">
                        <telerik:RadComboBox ID="RadComboBox1" runat="server" Culture="ja-JP" OnItemsRequested="RadComboBox1_ItemsRequested" AllowCustomText="false" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" Width="300px" OnClientSelectedIndexChanged="TokuisakiSelect" AutoPostBack="true" OnSelectedIndexChanged="RadComboBox1_SelectedIndexChanged"></telerik:RadComboBox>
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
                                var Label1 = document.getElementById("<%=Label1.ClientID%>");
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
                                const Textbox12 = document.getElementById("<%=TextBox12.ClientID%>");
                                debugger;
                                //請求先詳細にもまず記載する
                                var TbxCustomerCode2 = document.getElementById('TbxCustomerCode2');
                                var TbxTokuisakiCode2 = document.getElementById('TbxTokuisakiCode2');
                                var TbxTokuisakiMei2 = document.getElementById('TbxTokuisakiMei2');
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
                                //var TbxKakeritsu2 = document.getElementById('TbxKakeritsu2');
                                var RadShimebi2 = $find('RadShimebi2');
                                //var HidTantoStaffCode2 = document.getElementById('HidTantoStaffCode2');
                                //var LblTantoStaffNo2 = document.getElementById('LblTantoStaffNo2');

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
                                TbxTokuisakiAddress.value = TokuisakiAddress1;
                                TbxTokuisakiAddress1.value = TokuisakiAddress2;
                                TbxTokuisakiTEL.value = TokuisakiTEL;
                                TbxTokuisakiFax.value = TokuisakiFAX;
                                TbxTokuisakiDepartment.value = TokuisakiDepartment;
                                RcbTax.set_text(Zeikubun);
                                TbxKakeritsu.value = Kakeritsu;
                                RcbShimebi.set_text(Shimebi);
                                LblTantoStaffCode.innerText = TantoStaffCode;
                                Label1.innerText = TantoStaffCode;
                                HidTantoStaffCode.value = TantoStaffCode;
                                ////////請求先記載/////////////
                                TbxCustomerCode2.value = CustomerCode;
                                TbxTokuisakiCode2.value = TokuisakiCode;
                                TbxTokuisakiMei2.value = TokuisakiName1;
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
                                //TbxKakeritsu2.value = Kakeritsu;
                                RadShimebi2.set_text(Shimebi);
                                //LblTantoStaffNo2.innerText = TantoStaffCode;
                                //HidTantoStaffCode2.value = String(TantoStaffCode);

                            }
                        </script>
                        <input type="hidden" id="TokuisakiCode" runat="server" />
                        <input type="hidden" id="CustomerCode" runat="server" />
                        <asp:TextBox ID="KariTokui" runat="server" Width="300px" AutoPostBack="true"></asp:TextBox>

                        <asp:Label runat="server" ID="LblAlert" Font-Size="Small" ForeColor="Red"></asp:Label>
                        <asp:Button runat="server" ID="BtnTool2" ToolTip="登録したい得意先名を検索し、一覧上から選択して登録。得意先マスタに登録されていない場合は「スポット得意先」を登録。" Text="❔" OnClientClick="return false;" />

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
                        <telerik:RadComboBox ID="RadComboBox3" runat="server" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" OnItemsRequested="RadComboBox3_ItemsRequested" Width="300px" OnClientSelectedIndexChanged="SeikyusakiSelected"></telerik:RadComboBox>
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
                                debugger;
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

                            }
                        </script>
                        <asp:TextBox ID="kariSekyu" runat="server" Width="300px"></asp:TextBox>
                        <asp:Button runat="server" ID="BtnTool3" ToolTip="登録したい請求先名を検索し、一覧上から選択して登録。得意先マスタに登録されていない場合は「スポット得意先」を登録。" Text="❔" OnClientClick="return false;" />

                    </td>
                    <td class="MiniTitle">
                        <p>リレー状況</p>
                    </td>
                    <td class="waku">
                        <asp:TextBox ID="TextBox1" runat="server" Width="80px"></asp:TextBox>
                    </td>
                    <td class="MiniTitle"></td>
                    <td class="waku"></td>
                    <td class="MiniTitle">
                        <p>締め日</p>
                    </td>
                    <td class="waku">
                        <asp:Label ID="Shimebi" runat="server" Text=""></asp:Label>
                        <asp:HiddenField runat="server" ID="HidShimebi" />
                    </td>
                    <td class="MiniTitle">
                        <p>数量合計</p>
                    </td>
                    <td runat="server" class="sisan">
                        <asp:TextBox ID="TextBox10" runat="server" CssClass="Text" Width="70px" Height="20px" placeholder="0" TextMode="Number"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style9" runat="server">
                        <p>納品先</p>
                    </td>
                    <td class="auto-style10">
                        <asp:Button ID="Button3" runat="server" Text="納品先詳細" CssClass="Btn10" />
                        <script type="text/javascript">
                            function MeisaiNouhin(ss) {
                                document.getElementById(ss).style.display = "";
                                document.getElementById("mInput").style.display = "none";
                                document.getElementById("CtrlSyousai").style.display = "none";
                                document.getElementById("head").style.display = "none";
                                document.getElementById("TBAddRow").style.display = "none";
                                document.getElementById("SubMenu").style.display = "none";
                                document.getElementById("SubMenu2").style.display = "";
                                document.getElementById("DivDataGrid").style.display = "none";

                            }
                        </script>
                    </td>
                    <td colspan="4" class="auto-style10">
                        <telerik:RadComboBox ID="RadComboBox2" runat="server" Culture="ja-JP" OnItemsRequested="RadComboBox2_ItemsRequested" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" Width="300px" OnClientSelectedIndexChanged="Nouhinsaki2"></telerik:RadComboBox>
                        <asp:Button runat="server" ID="BtnTool4" ToolTip="登録したい納品先名を検索し、一覧上から選択して登録。直送先・施設マスタに登録されていない場合は「スポット得意先」を登録。" Text="❔" OnClientClick="return false;" />

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
                            }
                        </script>

                        <input type="hidden" id="TyokusoCode" runat="server" />

                    </td>
                    <td class="MiniTitle">
                        <p>掛率</p>
                    </td>
                    <td class="waku">
                        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                        <asp:HiddenField runat="server" ID="HidKakeritsu" />
                        <asp:TextBox ID="TbxKake" runat="server" Width="80px" Height="20px" OnTextChanged="TbxKake_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td class="MiniTitle"></td>
                    <td class="waku"></td>
                    <td class="MiniTitle">
                        <p>売上計</p>
                    </td>
                    <td runat="server" class="sisan">
                        <asp:TextBox ID="TextBox7" runat="server" Width="70px" Height="20px" CssClass="Text" AutoPostBack="true" placeholder="0"></asp:TextBox>
                        <input type="hidden" id="urikei" runat="server" />
                    </td>
                    <td class="MiniTitle">
                        <p>仕入計</p>
                    </td>
                    <td class="sisan">
                        <asp:TextBox ID="TextBox8" runat="server" Width="70px" Height="20px" CssClass="Text" AutoPostBack="true" placeholder="0"></asp:TextBox>
                        <input type="hidden" id="shikei" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="InputTitle" runat="server">
                        <p>使用施設名</p>
                    </td>
                    <td colspan="3" class="waku">
                        <table>
                            <tr>
                                <td colspan="2">
                                    <telerik:RadComboBox ID="FacilityRad" runat="server" Culture="ja-JP" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" AutoPostBack="True" Width="250px" OnSelectedIndexChanged="FacilityRad_SelectedIndexChanged" OnItemsRequested="FacilityRad_ItemsRequested"></telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button runat="server" ID="BtnFacilityDetail" Text="施設詳細" CssClass="Btn10" />
                                </td>

                                <td>
                                    <asp:CheckBox ID="CheckBox5" runat="server" Text="複数" OnCheckedChanged="CheckBox5_CheckedChanged" AutoPostBack="True" />
                                    <asp:Button runat="server" ID="BtnTool1" ToolTip="明細にヘッダーと同じ施設を登録する場合は「複数」に✓。明細毎に異なる施設を登録する場合はヘッダーの施設名に「複数施設」を登録。" Text="❔" OnClientClick="return false;" />
                                </td>
                            </tr>
                        </table>
                        <input type="hidden" id="FacilityAddress" runat="server" />
                    </td>
                    <td class="MiniTitle">
                        <p>開始</p>
                    </td>
                    <td class="waku">
                        <telerik:RadDatePicker ID="RadDatePicker3" runat="server" Width="100px" CssClass="CategoryChabge"></telerik:RadDatePicker>
                    </td>
                    <td class="MiniTitle">
                        <p>使用期間</p>
                    </td>
                    <td class="waku">
                        <table>
                            <tr>
                                <td>
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

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="CheckBox4" runat="server" Text="複数" AutoPostBack="True" OnCheckedChanged="CheckBox4_CheckedChanged" CssClass="CategoryChabge" />
                                    <asp:Button runat="server" ID="BtnTool5" ToolTip="明細に同じ使用期間を登録する場合は「複数」に✓" Text="❔" OnClientClick="return false;" />
                                </td>
                            </tr>
                        </table>
                    </td>

                    <td class="MiniTitle">
                        <p>終了</p>
                    </td>
                    <td class="waku">
                        <telerik:RadDatePicker ID="RadDatePicker4" runat="server" Width="100px" CssClass="CategoryChabge"></telerik:RadDatePicker>
                    </td>
                    <td class="MiniTitle">
                        <p>消費税額</p>
                    </td>
                    <td class="sisan">
                        <asp:TextBox ID="TextBox5" runat="server" Width="70px" Height="20px" CssClass="Text" placeholder="0"></asp:TextBox>
                        <input type="hidden" runat="server" id="zeikei" />
                    </td>
                    <td class="MiniTitle">
                        <p>粗利計</p>
                    </td>
                    <td class="sisan">
                        <asp:TextBox ID="TextBox6" runat="server" Width="70px" Height="20px" CssClass="Text" placeholder="0"></asp:TextBox>
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
                        <asp:TextBox ID="TextBox12" runat="server" Width="70px" Height="20px" CssClass="Text" placeholder="0"></asp:TextBox>
                        <input type="hidden" runat="server" id="soukei" />
                    </td>
                    <td class="MiniTitle">
                        <p>利益率</p>
                    </td>
                    <td class="sisan">
                        <asp:TextBox ID="TextBox13" runat="server" Width="70px" Height="20px" CssClass="Text" placeholder="0"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>

        <!-- ヘッダーテーブル -------------------------------------------------------------------------------------->

        <div runat="server" style="width: 100%;">
            <table runat="server" style="text-align: center; width: 100%; border-collapse: collapse; background-color: white; border: 2px solid #47ab47" id="head">
                <tr>
                    <td rowspan="3" style="text-align: center; background-color: #bae5ba; width: 90px;">
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
        </div>
        <table style="width: 100%">
            <tr>
                <td>
                    <div runat="server" id="DivDataGrid">
                        <asp:DataGrid runat="server" ID="CtrlSyousai" AutoGenerateColumns="False" OnItemDataBound="CtrlSyousai_ItemDataBound" OnItemCommand="CtrlSyousai_ItemCommand" BorderColor="White">
                            <Columns>

                                <asp:TemplateColumn HeaderStyle-BorderStyle="None" ItemStyle-BorderStyle="None" ItemStyle-Width="30px">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>

                                        <asp:Label ID="RowNo" runat="server" Text="" Font-Size="25px"></asp:Label>
                                        <asp:Button ID="Button4" runat="server" Text="削除" Width="90px" CssClass="Btn10" CommandName="Del" />
                                        <asp:Button ID="BtnAddRow" runat="server" Text="明細挿入" Width="90px" CssClass="Btn10" CommandName="Add" />
                                        <asp:Button runat="server" ID="BtnCopyAdd" Text="明細複写" Width="90px" CssClass="Btn10" CommandName="Copy" />
                                        <asp:Button runat="server" ID="BtnTool5" ToolTip="「削除」明細を1行削除。「明細挿入」空の明細を1行追加。「明細複写」クリックした明細のデータを複写して追加。" Text="❔" OnClientClick="return false;" />

                                        <input type="hidden" runat="server" id="HidAddFLG" /><%--dataT_rowにデータを持っているかいないかのフラグ--%>
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
            }
        </script>

        <script>
            function AddFocus(Fcs) {
                var a = document.getElementById(Fcs);
                var y = a.scrollHeight - a.clientHeight;
                window.scroll(0, y);
                try {
                    var catecode = document.getElementById("RadComboCategory");
                    var val = catecode.value;
                    switch (val) {
                        case "公共図書館":
                        case "学校図書館":
                        case "防衛省":
                        case "その他図書館":
                            var classN = document.getElementsByClassName("CategoryChabge");
                            classN.css('display', 'none');
                            //$(".CategoryChabge").css('display', 'none');
                            //$(".Zasu").css('display', 'none');
                            break;
                        case "上映会":
                            $(".CategoryChabge").css('display', '');
                            $(".Zasu").css('display', '');
                            break;
                        default:
                            $(".CategoryChabge").css('display', '');
                            $(".Zasu").css('display', 'none');
                    }
                }
                catch (e) {
                    var ex = e.value;
                }
            }
        </script>
        &nbsp;
        <%--        <asp:Button ID="Button13" runat="server" Text="計算" CssClass="Btn10" OnClientClick="Cul(); return false;" />--%>
        <script type="text/javascript">
            function Cul() {
                var Zeikubun = $find('RadZeiKubun');
                var Kakeritsu = document.getElementById('Label3');
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
                debugger;
                if (boolZei == "税込") {
                    for (var i = 1; i < c; i++) {
                        var x = i + 1;
                        if (x < 10) {
                            var count = "0" + x;
                        }
                        else {
                            var count = x;
                        }
                        var Kingaku = document.getElementById(CtrlSyousai + "_" + ctl + count + "_" + Syosai + "_" + "Kingaku").value.replace(",", "");
                        var Uriage = document.getElementById(CtrlSyousai + "_" + ctl + count + "_" + Syosai + "_" + "Uriage").value.replace(",", "");
                        var ShiireKingaku = document.getElementById(CtrlSyousai + "_" + ctl + count + "_" + Syosai + "_" + "ShiireKingaku").value.replace(",", "");
                        var Kazu = document.getElementById(CtrlSyousai + "_" + ctl + count + "_" + Syosai + "_" + "Suryo").value.replace(",", "");
                        var TbxMakerNo = document.getElementById(CtrlSyousai + "_" + ctl + count + "_" + Syosai + "_" + "TbxMakerNo").value;
                        if (TbxMakerNo != "0") {
                            suryo += Number(Kazu);
                        }
                        if (Kingaku != "OPEN") {
                            Tax += Math.floor(Kingaku * Kakeritsu.innerText / 100 * 0.1);
                            UriageKei += Number(Uriage);
                            UriageGokei += Number(Uriage);
                        }
                        else {
                            Tax = "OPEN";
                            UriageKei = "OPEN";
                            UriageKei = "OPEN";
                        }
                        if (ShiireKingaku != "OPEN") {
                            Shiire += Number(ShiireKingaku);
                        }
                        else {
                            Shiire = "OPEN";
                        }
                    }
                    if (Tax != "OPEN") {
                        TbxTax.value = String(Tax).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                        TbxUriageKei.value = String(UriageKei).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                        TbxUriageGokei.value = String(UriageKei).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                        TbxSuryo.value = suryo;
                    }
                    else {
                        TbxTax.value = "OPEN";
                        TbxUriageKei.value = "OPEN";
                        TbxUriageGokei.value = "OPEN";
                        TbxSuryo.value = suryo
                    }
                    if (Shiire != "OPEN") {
                        TbxShiireKei.value = String(Shiire).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                        TbxArariKei.value = String(UriageKei - Shiire).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                        TbxRieki.value = Math.floor(((UriageKei - Shiire) * 100) / UriageKei);
                    }
                    else {
                        TbxShiireKei.value = "OPEN";
                        TbxArariKei.value = "OPEN";
                        TbxRieki.value = "OPEN";
                    }
                }
                if (boolZei == "税抜") {
                    for (var i = 1; i < c; i++) {
                        var x = i + 1;
                        if (x < 10) {
                            var count = "0" + x;
                        }
                        else {
                            var count = x;
                        }
                        debugger
                        var Kingaku = document.getElementById(CtrlSyousai + "_" + ctl + count + "_" + Syosai + "_" + "Kingaku").value.replace(",", "");
                        var Uriage = document.getElementById(CtrlSyousai + "_" + ctl + count + "_" + Syosai + "_" + "Uriage").value.replace(",", "");
                        var ShiireKingaku = document.getElementById(CtrlSyousai + "_" + ctl + count + "_" + Syosai + "_" + "ShiireKingaku").value.replace(",", "");
                        var Kazu = document.getElementById(CtrlSyousai + "_" + ctl + count + "_" + Syosai + "_" + "Suryo").value.replace(",", "");
                        var TbxMakerNo = document.getElementById(CtrlSyousai + "_" + ctl + count + "_" + Syosai + "_" + "TbxMakerNo").value;
                        if (TbxMakerNo != "NEBIKI" && TbxMakerNo != "SOURYOU" && TbxMakerNo != "KIZAI" && TbxMakerNo != "HOSYOU") {
                            suryo += Number(Kazu);
                        }
                        if (Kingaku != "OPEN") {
                            UriageKei += Number(Uriage);
                            Shiire += Number(ShiireKingaku);
                        }
                        else {
                            UriageKei = "OPEN";
                            Shiire = "OPEN";
                            break;
                        }
                    }
                    if (UriageKei != "OPEN") {
                        TbxUriageKei.value = String(UriageKei).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                        var tax = Math.floor(UriageKei * 0.1);
                        TbxTax.value = String(tax).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                        var gokeitax = Math.floor(UriageKei * 1.1);
                        TbxUriageGokei.value = String(gokeitax).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                        TbxSuryo.value = suryo;
                        TbxShiireKei.value = String(Shiire).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                        TbxArariKei.value = String(UriageKei - Shiire).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                        TbxRieki.value = Math.floor(((UriageKei - Shiire) * 100) / UriageKei);
                    }
                    else {
                        TbxUriageKei.value = "OPEN";
                        TbxTax.value = "OPEN";
                        TbxUriageGokei.value = "OPEN";
                        TbxSuryo.value = suryo;
                        TbxShiireKei.value = "OPEN";
                        TbxArariKei.value = "OPEN";
                        TbxRieki.value = "OPEN";
                    }
                }
                window.scrollTo(0, 0);
            }
        </script>

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
                <asp:TableCell ColumnSpan="2" CssClass="waku">
                    <asp:TextBox runat="server" ID="TbxTokuisakiAddress" Width="200px"></asp:TextBox>
                </asp:TableCell>
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
                    <telerik:RadComboBox runat="server" ID="RadComboBox5" OnItemsRequested="RadComboBox5_ItemsRequested" EnableLoadOnDemand="true" AllowCustomText="true" AutoPostBack="true" OnSelectedIndexChanged="RadComboBox5_SelectedIndexChanged"></telerik:RadComboBox>
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

        <%--        請求先詳細---------------------------------------------------------------------------------------------------------------%>
        <asp:Table runat="server" ID="TBSeikyusaki">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>顧客コード<span style="color: red">*</span></p>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="waku">
                    <asp:TextBox runat="server" ID="TbxCustomerCode2"></asp:TextBox>
                </asp:TableCell>
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
                    <asp:TextBox runat="server" ID="TbxTokuisakiMei2"></asp:TextBox>
                </asp:TableCell>
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
                    <asp:TextBox runat="server" ID="TbxTokuisakiRyakusyou2"></asp:TextBox>
                </asp:TableCell>
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
                <asp:TableCell ColumnSpan="6" CssClass="waku">
                    <asp:TextBox runat="server" ID="TbxPostNo2" Width="400px"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p>請求先住所</p>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="waku">
                    <asp:TextBox runat="server" ID="TbxTokuisakiAddress2" Width="200px"></asp:TextBox>
                </asp:TableCell>
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
                <asp:TableCell ColumnSpan="2" CssClass="MiniTitle">
                    <p></p>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" CssClass="waku">
                    <p></p>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
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
            <asp:TableRow>
                <asp:TableCell ColumnSpan="4" CssClass="waku" HorizontalAlign="Center">
                    <asp:Button runat="server" ID="BtnToMasterS" Text="得意先マスターに移動" OnClick="BtnToMasterS_Click" Width="180px" CssClass="Btn10" />
                </asp:TableCell>
                <asp:TableCell ColumnSpan="4" CssClass="waku" HorizontalAlign="Center">
                    <asp:Button runat="server" ID="BtnSekyu" Text="見積ヘッダーに適応" OnClick="BtnSekyu_Click" CssClass="Btn10" Width="180px" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

        <!------------納入先詳細--------------------------------------------------------------------------->
        <asp:Panel runat="server" ID="NouhinsakiPanel">
            <table>
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
                        <p>コード</p>
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
                        <telerik:RadComboBox runat="server" ID="RcbNouhinsakiMei" OnItemsRequested="RcbNouhinsakiMei_ItemsRequested" AllowCustomText="false" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" OnClientSelectedIndexChanged="Nouhinsaki"></telerik:RadComboBox>
                        <script type="text/javascript">
                            function Nouhinsaki(sender, eventArgs) {
                                var selval = $find("<%=RcbNouhinsakiMei.ClientID%>");
                                var settext = $find("<%=RadComboBox2.ClientID%>");
                                var val = selval.get_selectedItem().get_value();
                                settext.set_text(selval.get_selectedItem().get_text());
                                var Ary = val.split(',');
                                var TbxNouhinSisetsu = document.getElementById('TbxNouhinSisetsu');
                                var TbxNouhinTyokusousakiMei2 = document.getElementById('TbxNouhinTyokusousakiMei2');
                                var TbxNouhinsakiRyakusyou = document.getElementById('TbxNouhinsakiRyakusyou');
                                var TbxNouhinsakiTanto = document.getElementById('TbxNouhinsakiTanto');
                                var TbxNouhinsakiYubin = document.getElementById('TbxNouhinsakiYubin');
                                var TbxNouhinsakiAddress = document.getElementById('TbxNouhinsakiAddress');
                                var TbxNouhinsakiAddress2 = document.getElementById('TbxNouhinsakiAddress2');
                                var TbxNouhinsakiTell = document.getElementById('TbxNouhinsakiTell');
                                var TbxNouhinsakiKeisyo = document.getElementById('TbxNouhinsakiKeisyo');
                                var RcbNouhinsakiCity = document.getElementById('RcbNouhinsakiCity');

                                TbxNouhinSisetsu.value = Ary[0];
                                TbxNouhinTyokusousakiMei2.value = Ary[2];
                                TbxNouhinsakiRyakusyou.value = Ary[3];
                                TbxNouhinsakiTanto.value = Ary[4];
                                TbxNouhinsakiYubin.value = Ary[5];
                                TbxNouhinsakiAddress.value = Ary[6];
                                TbxNouhinsakiAddress2.value = Ary[7];
                                TbxNouhinsakiTell.value = Ary[8];
                                RcbNouhinsakiCity.get_value() = Ary[9];
                            }
                        </script>
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
                        <asp:TextBox runat="server" ID="TbxNouhinsakiRyakusyou"></asp:TextBox>
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
                    <td runat="server" class="MiniTitle">
                        <p>敬称</p>
                    </td>
                    <td class="waku">
                        <asp:TextBox runat="server" ID="TbxNouhinsakiKeisyo"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="waku">
                        <asp:Button runat="server" ID="BtnNouhinMasta" OnClick="BtnNouhinMasta_Click" Text="納品先マスタに移動" CssClass="Btn10" Width="150px" />
                    </td>
                    <td class="waku">
                        <asp:Button runat="server" ID="BtnNouhisakiTekiou" OnClick="BtnNouhisakiTekiou_Click" Text="見積ヘッダーに適応" CssClass="Btn10" Width="150px" />
                    </td>
                </tr>
            </table>
        </asp:Panel>

        <table runat="server" id="TBFacilityDetail">
            <tr>
                <td class="MiniTitle" style="width: 120px; height: 20px;">
                    <p>施設コード<span style="color: red;">*</span></p>
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
                <td runat="server" class="MiniTitle">
                    <p>敬称</p>
                </td>
                <td class="waku">
                    <asp:TextBox runat="server" ID="TbxKeisyo"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Button runat="server" ID="BtnFacilityMaster" Text="直送先・施設マスタに移動" CssClass="Btn10" Width="160px" />
                </td>
                <td>
                    <asp:Button runat="server" ID="BtnHeaderFit" Text="見積ヘッダーに適応" CssClass="Btn10" Width="150px" OnClick="BtnHeaderFit_Click" />
                </td>
            </tr>
        </table>
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
        <!-- 商品詳細-------------------------------------------------------------------------------------------------->
        <script src="JavaScript.js"></script>
    </form>
</body>
</html>
