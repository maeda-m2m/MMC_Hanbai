<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrlMitsuSyousai.ascx.cs" Inherits="Gyomu.CtrlMitsuSyousai" %>
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
<div runat="server">
    <table class="HeadTB" runat="server">
        <tr>
            <td class="MeisaiProcd" runat="server" colspan="1">
                <asp:Label ID="LblProduct" runat="server" Text=""></asp:Label>
                <asp:Button runat="server" Text="商品詳細" ID="BtnProductMeisai" CssClass="BtnMeiasai" OnClick="Button6_Click" />
                <script type="text/javascript">
                    function Meisai() {
                        debugger;
                        var ss = document.getElementById("SyouhinSyousai");
                        ss.style.display;
                        debugger;
                    }
                </script>
                <input type="hidden" id="HidCategoryCode" runat="server" />
            </td>
            <td class="MeisaiProNm" runat="server" colspan="5">
                <telerik:RadComboBox ID="SerchProduct" runat="server" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" OnItemsRequested="SerchProduct_ItemsRequested" OnSelectedIndexChanged="SerchProduct_SelectedIndexChanged" EmptyMessage="" AutoPostBack="True" Width="600px"></telerik:RadComboBox>
                <input type="hidden" runat="server" id="procd" />
                <script>
                    function SerchFocus(fcs) {
                        debugger;
                        var ary = fcs.split('-');
                        var rcb = document.getElementById(ary[0]);
                        var btn = document.getElementById(ary[1]);
                        btn.focus();
                    }
                </script>
            </td>
            <td class="MeisaiMTD" runat="server">
                <%--                <telerik:RadComboBox ID="Hanni" runat="server" Width="100px"></telerik:RadComboBox>--%>
                <asp:Label ID="LblHanni" runat="server" Text=""></asp:Label>
                <telerik:RadComboBox runat="server" ID="RcbHanni" OnSelectedIndexChanged="RcbHanni_SelectedIndexChanged" CssClass="Zasu" AutoPostBack="true" ViewStateMode="Enabled" EnableViewState="true"></telerik:RadComboBox>
            </td>
            <td class="MeisaiMTD" runat="server">
                <asp:Label ID="Baitai" runat="server" Text=""></asp:Label>
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
                <telerik:RadComboBox ID="ShiyouShisetsu" runat="server" Width="300px" OnItemsRequested="ShiyouShisetsu_ItemsRequested" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" OnSelectedIndexChanged="ShiyouShisetsu_SelectedIndexChanged" AutoPostBack="True"></telerik:RadComboBox>
                <input type="hidden" runat="server" id="FacilityCode" />
                <asp:TextBox ID="KariFaci" runat="server" Width="200px"></asp:TextBox>
                <asp:Label ID="Facility" runat="server" Text=""></asp:Label><asp:Button ID="BtnFacilityMeisai" runat="server" Text="施設詳細" CssClass="BtnMeiasai" CommandName="FacilityDetails" OnClick="Button2_Click" />
                <input type="hidden" runat="server" id="FacilityAddress" />
            </td>
            <td class="MeisaiMTD" runat="server">
                <telerik:RadComboBox ID="Zasu" runat="server" Width="100px" AllowCustomText="True" CssClass="Zasu" OnSelectedIndexChanged="Zasu_SelectedIndexChanged" AutoPostBack="true">
                    <Items>
                        <telerik:RadComboBoxItem Text="50" Value="50" runat="server" Selected="true" />
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
                </telerik:RadComboBox>
            </td>
            <td class="MeisaiMTD" runat="server">
                <telerik:RadDatePicker ID="StartDate" runat="server" Width="100px" CssClass="CategoryChabge"></telerik:RadDatePicker>
                <asp:Label ID="Label11" runat="server" Text=""></asp:Label>
            </td>
            <td class="MeisaiMTD" runat="server">
                <telerik:RadDatePicker ID="EndDate" runat="server" Width="100px" CssClass="CategoryChabge"></telerik:RadDatePicker>
                <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
            </td>
            <td class="MeisaiTD" runat="server">
                <asp:Label ID="Kakeri" runat="server" Text=""></asp:Label><br />
                <asp:Label ID="zeiku" runat="server" Text=""></asp:Label>
            </td>
            <td class="MeisaikTD" runat="server">
                <asp:TextBox ID="Tanka" runat="server" Width="80px" Height="20px" CssClass="SujiText"></asp:TextBox>
                <script type="text/javascript">
                    function Keisan(tbx) {
                        var ary = tbx.split('-');
                        debugger;
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
                        var num1 = String(num1).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                        var num2 = String(num2).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");

                        document.getElementById(ary[2]).value = num;
                        document.getElementById(ary[5]).value = num1;
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
                <telerik:RadComboBox runat="server" ID="Tekiyo" AllowCustomText="true" EnableLoadOnDemand="true" AutoPostBack="true" OnItemsRequested="Teikyo_ItemsRequested" Width="500px"></telerik:RadComboBox>
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
                    <asp:Button runat="server" ID="BtnClose" OnClick="BtnClose_Click" Text="閉じる" CssClass="BtnMeiasai" />
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
                    <p>市町村コード</p>
                </td>
                <td class="waku">
                    <telerik:RadComboBox runat="server" ID="RcbCity" OnItemsRequested="RcbCity_ItemsRequested" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" placeholder="市町村名を入力"></telerik:RadComboBox>
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
                    <p>施設名略称</p>
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
                    &nbsp;
                    <asp:Button ID="Button4" runat="server" Text="閉じる" CssClass="BtnMeiasai" OnClick="Button1_Click" />
                </td>

            </tr>

        </table>
    </div>
</asp:Panel>



