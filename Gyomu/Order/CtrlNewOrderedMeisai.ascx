<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrlNewOrderedMeisai.ascx.cs" Inherits="Gyomu.Order.CtrlNewOrderedMeisai" %>
<link href="NewOrderedInput.css" type="text/css" rel="stylesheet" />
<style type="text/css">
    .auto-style1 {
        background-color: #e5e5e5;
        height: 27px;
    }
</style>
<asp:Label ID="Err" runat="server" Text="" ForeColor="Red"></asp:Label>
<table class="MeisaiTB" style="width:1300px">
    <tr>
        <td rowspan="2" class="RowNoTD" style="text-align:center">
            <asp:Label ID="RowNo" CssClass="RowNoClass" runat="server" Font-Bold="true"></asp:Label>
            <br />
            <asp:CheckBox runat="server" ID="ChkHand" Text="手入力" AutoPostBack="true" OnCheckedChanged="ChkHand_CheckedChanged" />
            <input type="hidden" runat="server" id="OrderedNo" />
            <input type="hidden" runat="server" id="HidTokuisakiCode" />
            <input type="hidden" runat="server" id="HidTokuisakiName" />
            <input type="hidden" runat="server" id="HidCategoryCode" />
            <input type="hidden" runat="server" id="HidCategoryName" />
            <input type="hidden" runat="server" id="HidShiiresakiCode" />
            <input type="hidden" runat="server" id="HidShiiresakiName" />
            <input type="hidden" runat="server" id="HidFacilityCode" />
        </td>
        <td class="MeisaiTD" style="width:100px">
            <asp:Label runat="server" ID="LblMakerNo"></asp:Label>
            <asp:TextBox runat="server" ID="TbxMakerNo" Width="100px" OnTextChanged="TbxMakerNo_TextChanged" AutoPostBack="true"></asp:TextBox>
        </td>
        <td class="ProductTD" style="width: 200px">
            <telerik:RadComboBox runat="server" ID="ProductRad" AllowCustomText="true" EnableLoadOnDemand="true" ShowMoreResultsBox="true" ShowToggleImage="false" EnableVirtualScrolling="true" OnItemsRequested="ProductRad_ItemsRequested" OnSelectedIndexChanged="ProductRad_SelectedIndexChanged" Width="300px" AutoPostBack="true"></telerik:RadComboBox>
            <asp:TextBox runat="server" ID="TbxProduct" Width="300px" OnTextChanged="TbxProduct_TextChanged" AutoPostBack="true"></asp:TextBox>
        </td>
        <td class="MeisaiTD">
            <asp:Button runat="server" ID="BtnSyouhinSyousai" Text="商品詳細" CssClass="Btn" OnClick="BtnSyouhinSyousai_Click" />

        </td>

        <td class="MeisaiTD">
            <telerik:RadComboBox runat="server" ID="HanniRad" AllowCustomText="true" EnableLoadOnDemand="true" ShowMoreResultsBox="true" ShowToggleImage="false" EnableVirtualScrolling="true" OnItemsRequested="HanniRad_ItemsRequested" OnSelectedIndexChanged="HanniRad_SelectedIndexChanged" Width="100px" AutoPostBack="true"></telerik:RadComboBox>
        </td>
        <td class="MeisaiTD">
            <telerik:RadComboBox runat="server" ID="MediaRad" Width="100px" OnSelectedIndexChanged="MediaRad_SelectedIndexChanged" AutoPostBack="true">
                <Items>
                    <telerik:RadComboBoxItem Text="" Value="" />
                    <telerik:RadComboBoxItem Text="DVD" Value="DVD" />
                    <telerik:RadComboBoxItem Text="BD" Value="BD" />
                </Items>
            </telerik:RadComboBox>
        </td>
        <td class="MeisaiTD">
            <asp:TextBox runat="server" ID="TbxSuryo" Width="50px" TextMode="Number" Text="1"></asp:TextBox>
        </td>
        <td class="MeisaiTD" style="text-align: right;">
            <asp:TextBox runat="server" ID="TbxShiireTanka" Width="100px" CssClass="tbx"></asp:TextBox>
        </td>
        <td class="MeisaiTD" style="text-align: right;">
            <asp:TextBox runat="server" ID="TbxShiireKingaku" Width="100px" CssClass="tbx"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="4" class="auto-style1" >
            <telerik:RadComboBox runat="server" ID="FacilityRad" AllowCustomText="true" EnableLoadOnDemand="true" ShowMoreResultsBox="true" ShowToggleImage="false" EnableVirtualScrolling="true" OnItemsRequested="FaclityRad_ItemsRequested" OnSelectedIndexChanged="FaclityRad_SelectedIndexChanged" Width="300px"></telerik:RadComboBox>
        </td>
        <td class="auto-style1">
            <telerik:RadComboBox ID="ZasuRad" runat="server" Width="100px" AllowCustomText="True">
                <Items>
                    <telerik:RadComboBoxItem Text="" Value="0" runat="server" />
                    <telerik:RadComboBoxItem Text="1" Value="1" runat="server" />
                    <telerik:RadComboBoxItem Text="51" Value="51" runat="server" />
                    <telerik:RadComboBoxItem Text="101" Value="101" runat="server" />
                    <telerik:RadComboBoxItem Text="201" Value="201" runat="server" />
                    <telerik:RadComboBoxItem Text="301" Value="301" runat="server" />
                    <telerik:RadComboBoxItem Text="401" Value="401" runat="server" />
                    <telerik:RadComboBoxItem Text="501" Value="501" runat="server" />
                    <telerik:RadComboBoxItem Text="601" Value="601" runat="server" />
                    <telerik:RadComboBoxItem Text="701" Value="701" runat="server" />
                    <telerik:RadComboBoxItem Text="801" Value="801" runat="server" />
                    <telerik:RadComboBoxItem Text="901" Value="901" runat="server" />
                    <telerik:RadComboBoxItem Text="1001" Value="1001" runat="server" />
                </Items>
            </telerik:RadComboBox>
        </td>

        <td class="auto-style1">
            <telerik:RadDatePicker runat="server" ID="SiyoukaishiRDP"></telerik:RadDatePicker>
        </td>
        <td class="auto-style1">
            <telerik:RadDatePicker runat="server" ID="SiyouOwariRDP"></telerik:RadDatePicker>
        </td>
        <td class="auto-style1">
            <telerik:RadComboBox runat="server" ID="WareHouseRad" Width="100px" OnSelectedIndexChanged="WareHouseRad_SelectedIndexChanged" AutoPostBack="true">
                <Items>
                    <telerik:RadComboBoxItem Text="" Value="" />
                    <telerik:RadComboBoxItem Text="発注" Value="発注" />
                    <telerik:RadComboBoxItem Text="在庫" Value="在庫" />
                </Items>
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Panel runat="server" ID="SyouhinSyousai">
                <table>
                    <tr>
                        <td class="TitleTD">
                            <p>商品コード</p>
                        </td>
                        <td class="MeisaiTD">
                            <asp:TextBox runat="server" ID="TbxSyouhinCode"></asp:TextBox>
                        </td>
                        <td class="TitleTD">
                            <p>仕入コード</p>
                        </td>
                        <td class="MeisaiTD">
                            <asp:TextBox runat="server" ID="TbxShiireCode"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TitleTD">
                            <p>商品名</p>
                        </td>
                        <td class="MeisaiTD">
                            <asp:TextBox runat="server" ID="TbxSyouhinMei"></asp:TextBox>
                        </td>
                        <td class="TitleTD">
                            <p>仕入先名</p>
                        </td>
                        <td class="MeisaiTD">
                            <asp:TextBox runat="server" ID="TbxShiiresakiMei"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TitleTD">
                            <p>メーカー品番</p>
                        </td>
                        <td class="MeisaiTD">
                            <asp:TextBox runat="server" ID="TbxMakerHinban"></asp:TextBox>
                        </td>
                        <td class="TitleTD" style="width: 150px">
                            <p>カテゴリーコード</p>
                        </td>
                        <td class="MeisaiTD">
                            <asp:TextBox runat="server" ID="TbxCategoryCode"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TitleTD">
                            <p>メディア</p>
                        </td>
                        <td class="MeisaiTD">
                            <asp:TextBox runat="server" ID="TbxMedia"></asp:TextBox>
                        </td>
                        <td class="TitleTD">
                            <p>カテゴリー名</p>
                        </td>
                        <td class="MeisaiTD">
                            <asp:TextBox runat="server" ID="TbxCategoryName"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TitleTD">
                            <p>使用範囲</p>
                        </td>
                        <td class="MeisaiTD">
                            <asp:TextBox runat="server" ID="TbxHanni"></asp:TextBox>
                        </td>
                        <td class="TitleTD">
                            <p>入力者</p>
                        </td>
                        <td class="MeisaiTD">
                            <asp:TextBox runat="server" ID="TbxNyuryokuSya"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center" class="MeisaiTD">
                            <asp:Button runat="server" ID="BtnClode" Text="閉じる" OnClick="BtnClode_Click" CssClass="Btn" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
