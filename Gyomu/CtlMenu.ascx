<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlMenu.ascx.cs" Inherits="Gyomu.CtlMenu" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<style type="text/css">
    .logo {
        width: 110px;
        height: 65px;
    }

    .account {
        text-decoration: none;
        color: black;
        border-style: solid;
        border-color: black;
        border-width: 1px;
        text-align: center;
        width: 95px;
        border-radius: 3px;
        transition: .4s;
        background-color: transparent;
    }

    .auto-style1 {
        width: 130px;
    }
</style>

<script type="text/javascript">

    window.document.onkeydown = onKeyDown;
    function onKeyDown(e) {
        if (event.keyCode == 13) {
            return false;
        }
    }

    function showClock1() {

        var now = new Date();
        var year = now.getFullYear();
        var mon = now.getMonth() + 1; //１を足すこと
        var day = now.getDate();
        var you = now.getDay(); //曜日(0～6=日～土)

        //曜日の選択肢
        var youbi = new Array("日", "月", "火", "水", "木", "金", "土");
        //出力用

        var now = new Date();
        var nowH = now.getHours();
        var nowM = now.getMinutes();
        var nowS = now.getSeconds();
        if (nowH < 10) { nowH = "0" + nowH; }
        if (nowM < 10) { nowM = "0" + nowM; }
        if (nowS < 10) { nowS = "0" + nowS; }
        now = nowH + ":" + nowM + ":" + nowS;

        var msg = year + "/" + mon + "/" + day + " (" + youbi[you] + ") " + now;
        document.getElementById("Lbldate").innerText = msg;
    }
    setInterval('showClock1()', 1000);
</script>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
<table border="0" style="margin: 0px; width: 100%;">
    <tbody>
        <tr>
            <td class="auto-style1">
                <img alt="" src="Img/ロゴ.gif" class="logo" id="rogo" runat="server" /></td>
            <td>

                <telerik:RadMenu ID="RadMenu1" runat="server">

                    <Items>
                        <telerik:RadMenuItem runat="server" Text="見積管理">
                            <Items>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Mitumori/MitumoriItiran.aspx" Text="見積一覧表">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Kaihatsu.aspx" Text="見積入力">
                                </telerik:RadMenuItem>
                            </Items>
                        </telerik:RadMenuItem>

                        <telerik:RadMenuItem runat="server" Text="受注情報">
                            <Items>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Jutyu/JutyuJoho.aspx" Text="受注一覧">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/OrderInput.aspx" Text="受注入力">
                                </telerik:RadMenuItem>
                            </Items>
                        </telerik:RadMenuItem>

                        <telerik:RadMenuItem runat="server" Text="発注情報">
                            <Items>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Order/OrderedList.aspx" Text="発注一覧">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Order/NewOrderedInput.aspx" Text="発注入力">
                                </telerik:RadMenuItem>
                            </Items>
                        </telerik:RadMenuItem>

                        <telerik:RadMenuItem runat="server" Text="仕入計上">
                            <Items>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Appropriate/ApproptiateList.aspx" Text="仕入一覧">
                                </telerik:RadMenuItem>
                            </Items>
                        </telerik:RadMenuItem>


                        <telerik:RadMenuItem runat="server" Text="売上管理">
                            <Items>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Uriage/UriageIchiran.aspx" Text="売上一覧">
                                </telerik:RadMenuItem>
                                <%--                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Uriage/NyukinDenpyo.aspx" Text="入金伝票">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Uriage/NyukinRireki.aspx" Text="入金履歴">
                                </telerik:RadMenuItem>--%>
                            </Items>
                        </telerik:RadMenuItem>

                        <telerik:RadMenuItem runat="server" Text="返却">
                            <Items>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Return/ReturnList.aspx" Text="返却一覧"></telerik:RadMenuItem>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Return/ReturnInput.aspx" Text="返却処理"></telerik:RadMenuItem>
                            </Items>
                        </telerik:RadMenuItem>

                        <telerik:RadMenuItem runat="server" Text="在庫">
                            <Items>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Inventory/Inventory.aspx" Text="在庫管理"></telerik:RadMenuItem>
                            </Items>
                        </telerik:RadMenuItem>

                        <telerik:RadMenuItem runat="server" Text="締処理">
                            <Items>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Closing/Ledger.aspx" Text="元帳"></telerik:RadMenuItem>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Closing/MonthClosing.aspx" Text="月次処理"></telerik:RadMenuItem>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Closing/YearClosing.aspx" Text="年次処理"></telerik:RadMenuItem>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Closing/Reconciliation.aspx" Text="消込"></telerik:RadMenuItem>
                            </Items>
                        </telerik:RadMenuItem>

                        <telerik:RadMenuItem runat="server" Text="マスタ管理" Value="Master">
                            <Items>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Master/MasterTanto.aspx" Text="担当者マスタ">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Master/MsterTokuisaki.aspx" Text="得意先マスタ">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Master/MasterTyokuso.aspx" Text="直送先・施設マスタ">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Master/MasterShiire.aspx" Text="仕入先マスタ">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Master/MasterBumon.aspx" Text="部門マスタ">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Master/MasterSyohin.aspx" Text="商品・価格マスタ">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Master/MasterJoueiKakaku.aspx" Text="上映会価格マスタ">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Master/MasterOshirase.aspx" Text="お知らせマスタ">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Tokuisaki/MasterPortal.aspx" Text="得意先ポータルマスタ">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Tokuisaki/MasterPortalImage.aspx" Text="得意先ポータル画像マスタ">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem runat="server" NavigateUrl="~/Tokuisaki/TokuisakiAccount.aspx" Text="得意先アカウントマスタ">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem runat="server" Text="得意先ポータルマスタ">
                                    <Items>

                                        <telerik:RadMenuItem runat="server" Text="特集マスター" NavigateUrl="~/Tokuisaki/TokushuMenu.aspx"></telerik:RadMenuItem>
                                        <telerik:RadMenuItem runat="server" Text="特集商品検索" NavigateUrl="~/Tokuisaki/ShouhinSearch.aspx"></telerik:RadMenuItem>
                                        <telerik:RadMenuItem runat="server" Text="特集商品編集" NavigateUrl="~/Tokuisaki/ShouhinCheck.aspx"></telerik:RadMenuItem>
                                        <telerik:RadMenuItem runat="server" Text="お知らせ" NavigateUrl="~/Tokuisaki/Oshirase.aspx"></telerik:RadMenuItem>

                                    </Items>
                                </telerik:RadMenuItem>
                            </Items>
                        </telerik:RadMenuItem>

                        <telerik:RadMenuItem runat="server" Text="ログアウト">
                            <Items>
                                <telerik:RadMenuItem runat="server" Text="ログアウト" Value="ログアウト" NavigateUrl="~/Login.aspx">
                                </telerik:RadMenuItem>
                            </Items>
                        </telerik:RadMenuItem>
                    </Items>
                </telerik:RadMenu>

            </td>
            <td>
                <table width="30">
                    <tr>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td>
                <asp:Label ID="Lbldate" runat="server" ClientIDMode="Static" Text=" "></asp:Label>
                <br />
                <asp:Label ID="lblName" runat="server" Text=""></asp:Label><br />
                <a href="~/Master/AccountPage.aspx" runat="server" class="account">会社情報管理</a>
            </td>
        </tr>

    </tbody>
</table>




