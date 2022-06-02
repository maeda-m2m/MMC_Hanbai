<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrlMitsuSyousai.ascx.cs" Inherits="Gyomu.CtrlMitsuSyousai" %>



<asp:Label ID="err" runat="server" Text="" ForeColor="#ff0000"></asp:Label>
<asp:Label ID="end" runat="server" Text="" ForeColor="Green"></asp:Label>
<div runat="server" style="width: 100%">
    <table class="HeadTB" runat="server" style="width: 100%">
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
                <input type="hidden" id="HidCategoryCode" runat="server" />
            </td>
            <td class="MeisaiProNm" runat="server" colspan="5">
                <telerik:RadComboBox ID="SerchProduct" AutoPostBack="false" EnableLoadOnDemand="true" ShowToggleImage="False" runat="server" AllowCustomText="True" ShowMoreResultsBox="True" EnableVirtualScrolling="True" OnItemsRequested="SerchProduct_ItemsRequested" EmptyMessage="" Width="500px" OnClientSelectedIndexChanged="select">
                </telerik:RadComboBox>
                <telerik:RadComboBox ID="SerchProductJouei" EnableLoadOnDemand="true" ShowToggleImage="False" runat="server" AllowCustomText="True" ShowMoreResultsBox="True" EnableVirtualScrolling="True" OnItemsRequested="SerchProductJouei_ItemsRequested" EmptyMessage="" Width="500px" OnSelectedIndexChanged="SerchProductJouei_SelectedIndexChanged" AutoPostBack="true"></telerik:RadComboBox>
                <asp:Label runat="server" ID="LblSerchProduct"></asp:Label>
                <asp:Button runat="server" ID="BtnTool1" ToolTip="登録する商品名を検索し、一覧上から選択。" Text="❔" OnClientClick="return false;" />
                <asp:HiddenField runat="server" ID="HidColor" />
                <asp:HiddenField runat="server" ID="HidCp" />
                <asp:HiddenField runat="server" ID="HidCpOver" />
                <input type="hidden" runat="server" id="procd" />
            </td>
            <td class="MeisaiMTD" runat="server">
                <asp:Label ID="LblHanni" runat="server" Text=""></asp:Label>
                <telerik:RadComboBox runat="server" ID="RcbHanni" AutoPostBack="true" EnableLoadOnDemand="true" ShowToggleImage="False" AllowCustomText="True" ShowMoreResultsBox="True" EnableVirtualScrolling="True" OnSelectedIndexChanged="RcbHanni_SelectedIndexChanged" CssClass="Zasu" ViewStateMode="Enabled" EnableViewState="true" OnItemsRequested="RcbHanni_ItemsRequested"></telerik:RadComboBox>
                <asp:HiddenField runat="server" ID="HidHanni" />
            </td>
            <td class="MeisaiMTD" runat="server">
                <asp:Label ID="Baitai" runat="server" Text=""></asp:Label>
                <asp:HiddenField runat="server" ID="HidMedia" />
            </td>
            <td class="MeisaisTD" runat="server">
                <asp:TextBox ID="Suryo" runat="server" Width="30px" Text="1" TextMode="Number"></asp:TextBox>
                <asp:Label runat="server" ID="LblSuryo"></asp:Label>
            </td>
            <td class="MeisaikTD" runat="server">
                <asp:TextBox ID="HyoujyunTanka" runat="server" Width="80px" Height="20px" CssClass="SujiText" EnableViewState="true" ViewStateMode="Enabled"></asp:TextBox>
                <asp:Label runat="server" ID="LblHyoujunTanka"></asp:Label>
                <input type="hidden" id="ht" runat="server" />
                <input type="hidden" id="zeiht" runat="server" />
            </td>
            <td class="MeisaikTD" runat="server">
                <asp:TextBox ID="Kingaku" runat="server" Width="80px" Height="20px" CssClass="SujiText"></asp:TextBox>
                <asp:Label runat="server" ID="LblHyoujunKingaku"></asp:Label>
                <input type="hidden" id="kgk" runat="server" />
                <input type="hidden" id="zeikgk" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="5" class="MeisaiBTD" runat="server">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Facility" runat="server" Text=""></asp:Label>
                            <telerik:RadComboBox ID="ShiyouShisetsu" runat="server" Width="300px" OnItemsRequested="ShiyouShisetsu_ItemsRequested" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" OnClientSelectedIndexChanged="SelectFacility"></telerik:RadComboBox>
                        </td>
                        <td>
                            <asp:Button ID="BtnFacilityMeisai" runat="server" Text="施設詳細" CssClass="BtnMeiasai" CommandName="FacilityDetails" />
                        </td>
                    </tr>
                </table>
                <input type="hidden" runat="server" id="FacilityCode" />
                <input type="hidden" runat="server" id="FacilityAddress" />
            </td>
            <td class="MeisaiMTD" runat="server">
                <table runat="server" id="TBZasu" style="width: 150px">
                    <tr>
                        <td>
                            <p style="font-size: 9px;">入力後はTabキーを押してください</p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="TbxZasu" Width="100px" TextMode="Number" AutoPostBack="true" OnTextChanged="TbxZasu_TextChanged"></asp:TextBox>
                            <asp:Label runat="server" ID="LblZasu"></asp:Label>
                        </td>
                    </tr>
                </table>

            </td>
            <td class="MeisaiMTD" runat="server">
                <telerik:RadDatePicker ID="StartDate" runat="server" Width="100px" CssClass="CategoryChabge"></telerik:RadDatePicker>
                <asp:Label runat="server" ID="LblStartDate"></asp:Label>
            </td>
            <td class="MeisaiMTD" runat="server">
                <telerik:RadDatePicker ID="EndDate" runat="server" Width="100px" CssClass="CategoryChabge"></telerik:RadDatePicker>
                <asp:Label runat="server" ID="LblEndDate"></asp:Label>
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
                <asp:Label runat="server" ID="LblTanka"></asp:Label>
                <input type="hidden" id="tk" runat="server" />
            </td>
            <td class="MeisaikTD" runat="server">
                <asp:TextBox ID="Uriage" runat="server" Width="80px" Height="20px" CssClass="SujiText"></asp:TextBox>
                <asp:Label runat="server" ID="LblUriage"></asp:Label>
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
                <asp:Label runat="server" ID="LblTekiyo"></asp:Label>
            </td>
            <td colspan="2" class="MeisaiTD" runat="server">
                <telerik:RadComboBox ID="Hachu" runat="server" Culture="ja-JP" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" OnItemsRequested="Hachu_ItemsRequested" OnSelectedIndexChanged="Hachu_SelectedIndexChanged" AutoPostBack="true"></telerik:RadComboBox>
                <asp:Label runat="server" ID="LblHachu"></asp:Label>
            </td>
            <td class="MeisaiTD" runat="server">
                <asp:Label ID="WareHouse" runat="server" Text="不明"></asp:Label>
            </td>
            <td class="MeisaikTD" runat="server">
                <asp:TextBox ID="ShiireTanka" runat="server" Width="80px" Height="20px" CssClass="SujiText"></asp:TextBox>
                <asp:Label runat="server" ID="LblShiireTanka"></asp:Label>
                <input type="hidden" id="st" runat="server" />
                <input type="hidden" id="zeist" runat="server" />
            </td>
            <td class="MeisaikTD" runat="server">
                <asp:TextBox ID="ShiireKingaku" runat="server" Width="80px" Height="20px" CssClass="SujiText"></asp:TextBox>
                <asp:Label runat="server" ID="LblShiireKingaku"></asp:Label>
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

                    &nbsp;
                    <asp:Button ID="ButtonClose" runat="server" Text="閉じる" CssClass="BtnMeiasai" OnClick="Button1_Click" />
                </td>

            </tr>

        </table>
    </div>
</asp:Panel>



