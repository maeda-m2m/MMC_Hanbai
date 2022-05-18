<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NyukinDenpyo.aspx.cs" Inherits="Gyomu.Uriage.NyukinDenpyo" %>
<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>入金伝票</title>
    <style type="text/css">
        body
        {
            font-size: 9pt;
            font-family: Tahoma;
        }
        .btnMaeAto
        {
            width: 40px;
        }
        .cel1
        {
            color: White;
            text-align: right;
            white-space: nowrap;
            width: 70px;
            font: 9pt;
            font-family: Tahoma;
            border: solid 1px black;
            padding: 0 0 0 0;
            border: solid 1px black;
            height: 2em;
        }
        .cel2
        {
            border: solid 1px black;
            margin-left: 40px;
            white-space:nowrap;
        }
    </style>
    <link href="../../MainStyle.css" rel="stylesheet" type="text/css" />
    <telerik:RadScriptBlock ID="Rsb" runat="server">

        <script type="text/javascript">
            var KeyCode = 0;

            window.document.onkeydown = function(evt) {
                if (evt) {
                    KeyCode = evt.keyCode;
                } else {
                    KeyCode = event.keyCode;
                }
            }

            function EnterKeyClick() {
                if (KeyCode != 13) { return false; }
                document.getElementById('<%=BtnReg.ClientID %>').click();
            }

            function OnBlur1(Cmd) {
                if (KeyCode != 9) { return false; }
                if (event.shiftKey) { return false; }
                PostBack(Cmd);
            }

            function PostBack(Cmd) {
                __doPostBack("<%=BtnPostBack.UniqueID %>", Cmd);
            }

            function RdpNyukinBi_DateInput_OnBlur() {
                TabKeyClick("RdpNyukinBi.DateInput");
            }

            function FocusTo(id) {
                var ctl = document.getElementById(id);
                if (ctl == null) { return false; }
                if (event.shiftKey) { return false; }
                ctl.focus();
            }
        </script>
            </telerik:RadScriptBlock>
</head>
<body>
    <form id="form1" runat="server">
            <div id="MainMenu" runat="server">
            <uc:menu ID="Menu" runat="server" />
        </div>
        <br />
        <div id="Nyuryoku" runat="server">
            <telerik:RadTabStrip ID="RT" runat="server" Skin="Office2007" AutoPostBack="True" SelectedIndex="1">
                <Tabs>
            <telerik:RadTab Text="売上管理" Font-Size="12pt" NavigateUrl="~/Uriage/UriageJoho.aspx">
            </telerik:RadTab>
            <telerik:RadTab Text="入金伝票" Font-Size="12pt" NavigateUrl="~/Uriage/NyukinDenpyo.aspx">
            </telerik:RadTab>
                     <telerik:RadTab Text="入金履歴" Font-Size="12pt" NavigateUrl="~/Uriage/NyukinRireki.aspx" Selected="True">
            </telerik:RadTab>
        </Tabs>
            </telerik:RadTabStrip>
            <br />
               <table class="border">
        <tr>
            <td>
                <table class="border" style="border: solid 1px black; height: 2.2em;">
                    <tr>
                        <td>
                            <asp:RadioButton ID="RdoAdd" runat="server" Text="追加ﾓｰﾄﾞ" AutoPostBack="True" GroupName="Mode" Checked="True" OnCheckedChanged="RdoAdd_CheckedChanged" />
                        </td>
                        <td>
                            <asp:RadioButton ID="RdoShusei" runat="server" Text="修正ﾓｰﾄﾞ" AutoPostBack="True" GroupName="Mode" OnCheckedChanged="RdoShusei_CheckedChanged" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <div id="DivDenBan" runat="server">
                    <table id="TblDenban" runat="server" class="border" style="border: solid 1px black; height: 2.2em;">
                        <tr>
                            <td>
                                伝票番号：
                            </td>
                            <td>
                                <asp:TextBox ID="TbxDenBan" runat="server" Width="50px" BackColor="#FFCCFF"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td>
                <table class="border" style="border: solid 1px black; height: 2.2em;">
                    <tr>
                        <td>
                            <asp:Button ID="BtnReg" runat="server" Text="登録" Style="width: 80px" OnClick="BtnReg_Click" />
                        </td>
                        <td>
                            <asp:Button ID="BtnClear" runat="server" Text="クリア" Style="width: 80px" OnClick="BtnClear_Click" />
                        </td>
                        <td>
                            <asp:Button ID="BtnDel" runat="server" Text="伝票削除" Style="width: 80px" OnClick="BtnDel_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <div id="DivSearch" runat="server">
                    <table id="TblSearch" runat="server" class="border" style="border: solid 1px black; height: 2.2em;">
                        <tr>
                            <td>
                                <asp:DropDownList ID="DdlSortType" runat="server" AutoPostBack="True" >
                                    <asp:ListItem Value="ORDER BY UriageKanriCode ">伝票番号順</asp:ListItem>
                                    <asp:ListItem Value="ORDER BY NyukinBi ">入金日順</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="BtnLatest" runat="server" Text="<<" CssClass="btnMaeAto" />
                            </td>
                            <td>
                                <asp:Button ID="BtnLeft" runat="server" Text="<" CssClass="btnMaeAto"/>
                            </td>
                            <td>
                                <asp:Button ID="BtnRight" runat="server" Text=">" CssClass="btnMaeAto"  />
                            </td>
                            <td>
                                <asp:Button ID="BtnLast" runat="server" Text=">>" CssClass="btnMaeAto" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                入金処理方法
            </td>
            <td>
                <%--<asp:RadioButton ID="RdoIkkatu" runat="server" Text="一括入金" GroupName="NyukinType" AutoPostBack="True" OnCheckedChanged="RdoIkkatu_CheckedChanged"  />
 --%>               <asp:RadioButton ID="RdoJuchu" runat="server" Text="受注単位" GroupName="NyukinType" AutoPostBack="True" OnCheckedChanged="RdoJuchu_CheckedChanged" Checked="True"/>
                <asp:RadioButton ID="RdoKobetu" runat="server" Text="個別単位" GroupName="NyukinType" AutoPostBack="True" OnCheckedChanged="RdoKobetu_CheckedChanged" />
            </td>
            <td>
                <div id="DivJuchuNo" runat="server">
                    <table id="TblJuchuNo" runat="server" class="border" style="border: solid 1px black; margin-left:2px;">
                        <tr>
                            <td>
                                受注番号：
                            </td>
                            <td>
                                <asp:TextBox ID="TbxJuchuNo" runat="server" Width="120px" BackColor="#FFCCFF" OnTextChanged="TbxJuchuNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td>
                <asp:Label ID="LblDate" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table class="border">
        <tr>
            <td>
                <table class="border">
                    <tr>
                        <td class=" yt cel1">
                            入金日<span style="color: Red;">※</span>
                        </td>
                        <td class="cel2">
                            <telerik:RadDatePicker ID="RdpNyukinBi" runat="server" Culture="Japanese (Japan)" Skin="Web20" Width="100px" AutoPostBack="True" OnSelectedDateChanged="RdpNyukinBi_SelectedDateChanged" >
                                <Calendar ID="Calendar4" runat="server" ShowRowHeaders="False" Skin="Web20">
                                </Calendar>
                                <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="カレンダーを表示します。" />
                                <DateInput ID="DateInput4" runat="server" DateFormat="yyyy/MM/dd" DisplayDateFormat="yyyy/MM/dd" Font-Size="9pt" RangeValidation="Immediate" Width="85px" AutoPostBack="True" BackColor="#FFCCFF">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </td>
                        <td style="white-space: nowrap;">
                            &nbsp;&nbsp;&nbsp;
                            <asp:Label ID="LblMsg" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="border">
                    <tr>
                        <td class=" yt cel1">
                            得意先ｺｰﾄﾞ<span style="color: Red;">※</span>
                        </td>
                        <td class="cel2">
                            <asp:TextBox ID="TbxTokuiCode" runat="server" Width="100px" MaxLength="6" BackColor="#FFCCFF"></asp:TextBox>
                        </td>
                        <td class=" yt cel1">
                            得意先名
                        </td>
                        <td class="cel2">
                            <telerik:RadComboBox ID="RadTokuiMeisyo" runat="server" AllowCustomText="True" AutoPostBack="true" EnableLoadOnDemand="True" Height="180px" OnItemsRequested="RadTokuiMeisyo_ItemsRequested" Width="200" OnSelectedIndexChanged="RadTokuiMeisyo_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td class=" yt cel1">
                            得意先担当者
                        </td>
                        <td class="cel2">
                            <asp:TextBox ID="TbxTokuiTantouName" runat="server" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="border">
                    <tr>
                        <td class=" yt cel1">
                            締日
                        </td>
                        <td class="cel2" style="width: 120px;">
                            <asp:Label ID="LblShimeBi" runat="server"></asp:Label>
                        </td>
                        <td class=" yt cel1">
                            集金予定
                        </td>
                        <td class="cel2" style="width: 120px;">
                            <asp:Label ID="LblShukinYotei" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="TblIkkatu" runat="server" class="border">
                    <tr>
                        <td class=" yt cel1" style="background-color: Teal">
                            請求月
                        </td>
                        <td class="cel2" style="width: 70px;">
                            <table class="border" style="border-style: none;">
                                <tr>
                                    <td class="fit" style="border-style: none;">
                                        <asp:TextBox ID="TbxSeikyuYear" runat="server" Width="35px" MaxLength="4" BackColor="#FFCCFF"></asp:TextBox>
                                    </td>
                                    <td class="fit" style="border-style: none;">
                                        <asp:TextBox ID="TbxSeikyuMonth" runat="server" Width="20px" MaxLength="2" BackColor="#FFCCFF"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class=" yt cel1" style="background-color: Teal">
                            <asp:Label ID="LblSeikyuKikanHeader" runat="server" Text="請求期間"></asp:Label>
                        </td>
                        <td class="cel2" style="width: 120px; white-space:normal;">
                            <asp:Label ID="LblSeikyuKikan" runat="server"></asp:Label>
                        </td>
                        <td id="TdSeikyuZanHeader" runat="server" class=" yt cel1" style="background-color: Teal">
                            請求残
                        </td>
                        <td id="TdSeikyuZan" runat="server" class="cel2" style="width: 75px; text-align: right;">
                            <asp:Label ID="LblSeikyuZan" runat="server"></asp:Label>
                        </td>
                        <td class=" yt cel1" style="background-color: Teal">
                            <asp:Label ID="LblKonkaiSeikyuGakuHeader" runat="server" Text="請求額(税込)"></asp:Label>
                        </td>
                        <td class="cel2" style="width: 75px; text-align: right;">
                            <asp:Label ID="LblKonkaiSeikyuGaku" runat="server"></asp:Label>
                        </td>
                        <td class=" yt cel1" style="background-color: Teal">
                            消費税</td>
                        <td class="cel2" style="width: 75px; text-align: right;">
                            <asp:Label ID="LblSyouhiZei" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table id="TblJyuchuKobetu" runat="server" class="border">
                    <tr>
                        <td class=" yt cel1" style="background-color: Teal">
                            売上計上日
                        </td>
                        <td class="cel2" style="width: 70px;">
                            <asp:Label ID="LblUriageKeijouBi" runat="server"></asp:Label>
                        </td>
                        <td class=" yt cel1" style="background-color: Teal">
                            <asp:Label ID="Label4" runat="server" Text="売上金額(税込)"></asp:Label>
                        </td>
                        <td class="cel2" style="width: 75px; text-align: right;">
                            <asp:Label ID="LblUriageKingaku" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="border">
                    <tr>
                        <td class=" yt cel1" style="background-color: Teal">
                            入金済金額
                        </td>
                        <td class="cel2" style="width: 70px; text-align: right;">
                            <asp:Label ID="LblNyukinZumiKingaku" runat="server"></asp:Label>
                        </td>
                        <td id="TdNyukinKikanHeader" runat="server" class=" yt cel1" style="background-color: Teal">
                            入金期間
                        </td>
                        <td id="TdNyukinKikan" runat="server" class="cel2" style="width: 120px; white-space:normal">
                            <asp:Label ID="LblNyukinKikan" runat="server"></asp:Label>
                        </td>
                        <td class=" yt cel1" style="background-color: Teal">
                            今回入金額
                        </td>
                        <td class="cel2" style="width: 75px; text-align: right;">
                            <asp:Label ID="LblNyukinGaku" runat="server"></asp:Label>
                        </td>
                        <td class=" yt cel1" style="background-color: Teal">
                            入金残高
                        </td>
                        <td class="cel2" style="width: 75px; text-align: right;">
                            <asp:Label ID="LblNyukinZandaka" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table class="border" style="margin-top: 2px;">
        <tr>
            <td>
                <table class="border">
                    <tr>
                        <td class=" yt cel1" style="border-bottom: solid 1px white;">
                            部署<span style="color: Red;">※</span>
                        </td>
                        <td class="cel2" style="width: 120px;">
                            <telerik:RadComboBox ID="RadBumon" runat="server" BackColor="#FFCCFF" Height="150" Width="200" ></telerik:RadComboBox>
                        </td>
                        <td class=" yt cel1" style="border-bottom: solid 1px white;">
                            担当者<span style="color: Red;">※</span>
                        </td>
                        <td class="cel2">
                            <telerik:RadComboBox ID="RadTanto" runat="server" BackColor="#FFCCFF" Height="150" ></telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=" yt cel1" style="border-bottom: solid 1px white;">
                            摘要
                        </td>
                        <td class="cel2" colspan="3">
                            <asp:TextBox ID="TbxTekiyou" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table class="border" style="margin-top: 2px;">
        <tr>
            <td>
                <table class="border">
                    <tr>
                        <td class=" yt cel1" style="border-bottom: solid 1px white;">
                            現金
                        </td>
                        <td class="cel2">
                            <asp:TextBox ID="TbxGenkin" runat="server" Width="90px" Style="text-align: right;" MaxLength="14" BackColor="#FFCCFF" ></asp:TextBox>
                        </td>
                        <td class="cel2" colspan="2">
                        </td>
                        <td class="cel2">
                            <asp:TextBox ID="TbxGenkinBikou" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=" yt cel1" style="border-bottom: solid 1px white;">
                            小切手
                        </td>
                        <td class="cel2">
                            <asp:TextBox ID="TbxKogitte" runat="server" Width="90px" Style="text-align: right;" MaxLength="14" BackColor="#FFCCFF" ></asp:TextBox>
                        </td>
                        <td class="yt cel1">
                            期日
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="RdpKogitteKijitu" runat="server" Culture="Japanese (Japan)" Skin="Web20" Width="100px">
                                <Calendar ID="Calendar5" runat="server" ShowRowHeaders="False" Skin="Web20">
                                </Calendar>
                                <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="カレンダーを表示します。" />
                                <DateInput ID="DateInput5" runat="server" DateFormat="yyyy/MM/dd" DisplayDateFormat="yyyy/MM/dd" Font-Size="9pt" RangeValidation="Immediate" Width="85px">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </td>
                        <td class="cel2">
                            <asp:TextBox ID="TbxKogitteBikou" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=" yt cel1" style="border-bottom: solid 1px white;">
                            振込
                        </td>
                        <td class="cel2">
                            <asp:TextBox ID="TbxFurikomi" runat="server" Width="90px" Style="text-align: right;" MaxLength="14" BackColor="#FFCCFF"></asp:TextBox>
                        </td>
                        <td class="cel2" colspan="2">
                        </td>
                        <td class="cel2">
                            <asp:TextBox ID="TbxFurikomiBikou" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=" yt cel1" style="border-bottom: solid 1px black;">
                            手形
                        </td>
                        <td class="cel2">
                            <asp:TextBox ID="TbxTegata" runat="server" Width="90px" Style="text-align: right;" MaxLength="14" BackColor="#FFCCFF"></asp:TextBox>
                        </td>
                        <td class="yt cel1" style="border-bottom: solid 1px white;">
                            期日
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="RdpTegataKijitu" runat="server" Culture="Japanese (Japan)" Skin="Web20" Width="100px">
                                <Calendar ID="Calendar6" runat="server" ShowRowHeaders="False" Skin="Web20">
                                </Calendar>
                                <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="カレンダーを表示します。" />
                                <DateInput ID="DateInput6" runat="server" DateFormat="yyyy/MM/dd" DisplayDateFormat="yyyy/MM/dd" Font-Size="9pt" RangeValidation="Immediate" Width="85px">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </td>
                        <td class="cel2">
                            <asp:TextBox ID="TbxTegataBikou" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="cel2" colspan="2">
                        </td>
                        <td class="yt cel1">
                            振出人
                        </td>
                        <td>
                            <asp:TextBox ID="TbxFuridashiNin" runat="server" Width="100px"></asp:TextBox>
                        </td>
                        <td class="cel2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class=" yt cel1" style="border-bottom: solid 1px white;">
                            調整
                        </td>
                        <td class="cel2">
                            <asp:TextBox ID="TbxChousei" runat="server" Width="90px" Style="text-align: right;" MaxLength="14" BackColor="#FFCCFF"></asp:TextBox>
                        </td>
                        <td class="cel2" colspan="2">
                        </td>
                        <td class="cel2">
                            <asp:TextBox ID="TbxChouseiBikou" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=" yt cel1" style="border-bottom: solid 1px white;">
                            相殺
                        </td>
                        <td class="cel2">
                            <asp:TextBox ID="TbxSousai" runat="server" Width="90px" Style="text-align: right;" MaxLength="14" BackColor="#FFCCFF"></asp:TextBox>
                        </td>
                        <td class="cel2" colspan="2">
                        </td>
                        <td class="cel2">
                            <asp:TextBox ID="TbxSousaiBikou" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="G" runat="server" Skin="Yd" PageSize="2000" EnableAJAX="True" EnableAJAXLoadingTemplate="True" AutoGenerateColumns="False" GridLines="Both" EnableEmbeddedSkins="False" Width="0px" Style="margin-top: 3px;" EnableEmbeddedBaseStylesheet="False" OnItemDataBound="G_ItemDataBound" OnPreRender="G_PreRender">
<HeaderContextMenu EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False" CssClass="GridContextMenu GridContextMenu_Yd"></HeaderContextMenu>

        <MasterTableView NoMasterRecordsText="該当のデータはありません。">
            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
            </RowIndicatorColumn>

<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
            <Columns>
                <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column" UniqueName="C">
                    <HeaderTemplate>
                        <asp:CheckBox ID="Ch" runat="server" AutoPostBack="True" oncheckedchanged="Ch_CheckedChanged"/>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="C" runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn FilterControlAltText="Filter column1 column" HeaderText="NO" UniqueName="NO" DataField="No">
                    <HeaderStyle Wrap="false" />
                    <ItemStyle Wrap="false" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn FilterControlAltText="Filter column3 column" HeaderText="伝番/商品ｺｰﾄﾞ" UniqueName="SyouhiCode" DataField="SyouhiCode">
                    <HeaderStyle Wrap="false" />
                    <ItemStyle Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn FilterControlAltText="Filter column2 column" HeaderText="仕様/入金内訳" UniqueName="Syiyou" DataField="Syiyou">
                    <HeaderStyle Wrap="false" />
                    <ItemStyle Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn FilterControlAltText="Filter column2 column" HeaderText="計上日/入金日" UniqueName="KeijouBi" DataField="KeijouBi">
                    <HeaderStyle Wrap="false" />
                    <ItemStyle Wrap="false" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn FilterControlAltText="Filter column4 column" HeaderText="数量" UniqueName="Suuryou" DataField="Suuryou">
                    <HeaderStyle Wrap="false" />
                    <ItemStyle Wrap="false" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn FilterControlAltText="Filter column5 column" HeaderText="単価" UniqueName="Tanka_Kinshu" DataField="Tanka_Kinshu">
                    <HeaderStyle Wrap="false" />
                    <ItemStyle Wrap="false" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn FilterControlAltText="Filter column6 column" HeaderText="金額(税込)" UniqueName="Kingaku" DataField="Kingaku">
                    <HeaderStyle Wrap="false" />
                    <ItemStyle Wrap="false" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn FilterControlAltText="Filter column6 column" HeaderText="消費税" UniqueName="Zei" DataField="Zei">
                    <HeaderStyle Wrap="false" />
                    <ItemStyle Wrap="false" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>

            </Columns>
            <EditFormSettings>
                <EditColumn InsertImageUrl="Update.gif" UpdateImageUrl="Update.gif" EditImageUrl="Edit.gif" CancelImageUrl="Cancel.gif">
                </EditColumn>
            </EditFormSettings>
            <HeaderStyle BackColor="Navy" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" Wrap="True" />
        </MasterTableView>
        <FilterMenu EnableImageSprites="False" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False">
        </FilterMenu>
    </telerik:RadGrid>
    <div style="color:Red; font-weight:bold;">[注]&nbsp;この金額登録は基本的にはTabキーを利用して操作を行ってください。</div>
    <div style="color:Red; font-weight:bold;">[注]&nbsp;色付きの金額入力欄でTabキーを押すと、サーバーと通信し、金額計算と入力補完を行います。</div>
   <%--     <div style="color:Red; font-weight:bold;">[注]&nbsp;入力欄上でEnterキーを押すと、登録を行います。</div>
    <div style="color:Red; font-weight:bold;">[注]&nbsp;請求月は入金日、締日、集金予定を元に自動で表示されます。</div>--%>
    <asp:Button ID="BtnPostBack" runat="server" Style="display: none;" OnClick="BtnPostBack_Click"/>
    <telerik:RadAjaxManager ID="Ram" runat="server">
    </telerik:RadAjaxManager>
            </div>
    </form>
</body>
</html>
