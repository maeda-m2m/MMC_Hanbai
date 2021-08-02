<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlMitumoriSyosai.ascx.cs" Inherits="Gyomu.Mitumori.CtlMitumoriSyosai" %>

<style type="text/css">
      .Hedar{
            background-color:#00008B;
            color:#FFFFFF;
            padding: 0.5em;
            font-size:12px;
        }
      .Komoku{
          background-color:#E6E6FA;
           text-align: left;
      }
      .size{
          width:100%;
      }

    .ccs {
        border: none;
        padding: 0.5em;
        font-size: 12px;
        /*text-align: left;*/
        font-weight: normal;
          height: 18px;
    }

</style>


<table border="1" class="size">
	<tbody>
		 <tr class="Hedar" id="h1" runat="server">
			<td rowspan="3"></td>
			<td rowspan="3" colspan="2">行・区分</td>
			<td colspan="7">商品名</td>
			<td colspan="2">使用期間</td>
			<td colspan="4"></td>
			<td>消費税</td>
            <td colspan="2">粗利合計</td>
		</tr>
		<tr class="Hedar" id="h2" runat="server">
			<td >商品番号</td>
            <td>メーカー品番</td>
            <td>媒体</td>
            <td>範囲</td>
			<th class="ccs"><asp:Label ID="KmkKaisu" runat="server" Text="回数"></asp:Label></th>
			<th class="ccs">
                <asp:Label ID="KmkRyokin" runat="server" Text="料金"></asp:Label></th>
			<th class="ccs">
                <asp:Label ID="KmkBasyo" runat="server" Text="場所"></asp:Label></th>
			<td>発注No</td>
			<td>発注先No</td>
			<td rowspan="2" colspan="2">数量</td>
			<td rowspan="2" colspan="2">単価</td>
            <td rowspan="2" colspan="1">金額</td>
            <td rowspan="2" colspan="2">摘要</td>
		</tr>
		<tr class="Hedar" id="h3" runat="server">
			<td colspan="4">施設</td>
			<th colspan="3" class="ccs">
                <asp:Label ID="titolZasu" runat="server" Text="座数"></asp:Label></th>
			<td colspan="2">発注先名称</td>
		</tr>
		<tr>
			<td rowspan="3">
                <input id="DltBtn" type="button" runat="server" style="width: auto; font-size: x-small;" tabindex="-1" />
                <input id="CopyBtn" type="button" runat="server" style="width: auto; font-size: x-small;" tabindex="-1" />
            </td>
			<td>
                <asp:Label ID="lblGyou" runat="server" Text=""></asp:Label>
            </td>
			<td rowspan="2">商品</td>
			<td>
                <asp:Button ID="BtnSyohin" runat="server" Text="商品詳細" Width="110" Height="30" OnClick="BtnSyohin_Click"/>
            </td>
			<td colspan="3">
                <telerik:RadComboBox ID="RadSyohinmeisyou" runat="server" Width="400" Height="180px" OnSelectedIndexChanged="RadSyohinmeisyou_SelectedIndexChanged" AutoPostBack="true" EnableLoadOnDemand="True" OnItemsRequested="RadSyohinmeisyou_ItemsRequested" EmptyMessage="カテゴリーを選択してから入力してください。"></telerik:RadComboBox>
			</td>
			<th colspan="3" class="ccs"></th>
			<td colspan="2"><telerik:RadDatePicker ID="RadHattyuKaisi" runat="server"></telerik:RadDatePicker>
                <asp:Label ID="lbl5" runat="server" Text="～ "></asp:Label>
                <telerik:RadDatePicker ID="RadHattyuSyuryo" runat="server"></telerik:RadDatePicker></td>
			<td colspan="4">
                <asp:Label ID="labelHyoujun" runat="server" Text="標準単価"></asp:Label>
                 <input type="text" id="TbxHyojunKakaku" runat="server" ReadOnly="readonly"/>
            </td>
			<td class="Komoku">
                <input type="text" runat="server" id="lblSyohiZei" />
                </td>
			<td class="Komoku">粗利</td>
			<td>
                <input type="text" runat="server" id="lblArari" />
            </td>
		</tr>
		<tr>
			<td>
                <asp:Label ID="lblKubun" runat="server" Text=""></asp:Label>
            </td>
			<td>
                <asp:Label ID="lblHinmei" runat="server" Text=""></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblHinban" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblMedia" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblHani" runat="server" Text=""></asp:Label>
            </td>
			<th class="ccs">
                <asp:Label ID="lblKaisu" runat="server" Text=""></asp:Label>
            </th>
            <th class="ccs">
                <asp:Label ID="lblRyoukin" runat="server" Text=""></asp:Label>
            </th>
			<th class="ccs">
                <asp:Label ID="lblBasyo" runat="server" Text=""></asp:Label>
			</th>
			<td class="Komoku">
                <asp:TextBox ID="TbxHattyuNo" runat="server"></asp:TextBox>
            </td>
			<td class="Komoku">
                <asp:TextBox ID="TbxHattyuSakiNo" runat="server"></asp:TextBox>
            </td>
			<td class="Komoku">受注</td>
			<td>
                <input type ="text" id="TbxjutyuSuryo" runat="server" />
            </td>
			<td class="Komoku">受注</td>
			<td>
                <input type="text" id="TbxJutyuTanka" runat="server" />
            </td>
			<td class="Komoku">
                <input type="text" id="lblJutyuKingaku" runat="server" />
                </td>
			<td class="Komoku">受注</td>
			<td>
                <asp:TextBox ID="TbxJTekiyou" runat="server" MaxLength="15"></asp:TextBox>
            </td>
		</tr>
		<tr>
			<td>売上</td>
			<td>施設</td>
			<td>
                <asp:Button ID="BtnShisetuSyosai" runat="server" Text="施設詳細" Width="110" Height="30" OnClick="BtnShisetuSyosai_Click"/>
            </td>
			<td colspan="3">
                <telerik:RadComboBox ID="RadShisetuName" runat="server" Width="400" Height="180px" AutoPostBack="true" EnableLoadOnDemand="True" OnItemsRequested="RadTyokusoMeisyo_ItemsRequested"></telerik:RadComboBox>
			</td>
			<th class="Komoku">
                <asp:Label ID="KmkZasu" runat="server" Text="座数" CssClass="ccs"></asp:Label></th>
			<th class="ccs">
                <asp:Label ID="lblZasu" runat="server" Text=""></asp:Label>
            </th>
			<th class="ccs"></th>
			<td colspan="2" class="Komoku">
                <asp:Label ID="lblHattyusaki" runat="server" Text=""></asp:Label>
			</td>
			<td class="Komoku">発注</td>
			<td>
                <input type ="text" id="TbxHaccyuSuryou" runat="server" />
            </td>
			<td class="Komoku">発注</td>
			<td>
                <input type ="text" id="TbxHaccyuTanka" runat="server" ReadOnly="readonly"/>
            </td>
			<td class="Komoku">
                <input type ="text" id="lblHattyuKingaku" runat="server" ReadOnly="readonly"/>
                </td>
			<td class="Komoku">発注</td>
			<td>
                <asp:TextBox ID="TbxHTekiyo" runat="server" MaxLength="15"></asp:TextBox>
            </td>
		</tr>
	</tbody>
</table>
<asp:Label ID="lblCateG" runat="server" Text="" CssClass="none"></asp:Label>

