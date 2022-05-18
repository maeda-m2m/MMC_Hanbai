<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlKakaku.ascx.cs" Inherits="Gyomu.Master.CtlKakaku" %>

    <style type="text/css">
    th {
        background: #00008B;
        color:white;
    }
</style>


<table border="1">
	<tbody>
		<tr>
            <th colspan="2" class="heade">一括編集画面</th>
			<th class="heade">区分コード</th>
			<td> 
                <asp:TextBox ID="TbCode" runat="server" Width="80" OnTextChanged="TbCode_TextChanged" AutoPostBack="true"></asp:TextBox>
            </td>
			<th class="heade">カテゴリー区分</th>
			<td>
                <asp:TextBox ID="TbCate" runat="server" Width="80" OnTextChanged="TbCate_TextChanged" AutoPostBack="true"></asp:TextBox>
            </td>
			<th class="heade">利用状態</th>
			<td>
                <asp:CheckBox ID="ChRiyou" runat="server" Text="有効" OnCheckedChanged="ChRiyou_CheckedChanged" AutoPostBack="true"/>
            </td>
		</tr>
		<tr>
			<th class="heade">期限開始</th>
			<td>
                <telerik:RadDatePicker ID="DateKaisi" Runat="server" OnSelectedDateChanged="DateKaisi_SelectedDateChanged" AutoPostBack="true">
                </telerik:RadDatePicker>
            </td>
			<th class="heade">期限終了</th>
			<td>
                <telerik:RadDatePicker ID="DateOwari" Runat="server" OnSelectedDateChanged="DateOwari_SelectedDateChanged" AutoPostBack="true" >
                </telerik:RadDatePicker>
            </td>
			<th class="heade">標準価格</th>
			<td>
                <asp:TextBox ID="TBHyojun" runat="server" Width="100" OnTextChanged="TBHyojun_TextChanged" AutoPostBack="true"></asp:TextBox>
            </td>
			<th class="heade">仕入価格</th>
			<td>
                <asp:TextBox ID="TBShiire" runat="server" Width="100" OnTextChanged="TBShiire_TextChanged" AutoPostBack="true"></asp:TextBox>
            </td>
		</tr>
		<tr>
			<th class="heade">CP開始</th>
			<td>
                <telerik:RadDatePicker ID="DateCPKaishi" Runat="server" OnSelectedDateChanged="DateCPKaishi_SelectedDateChanged" AutoPostBack="true">
                </telerik:RadDatePicker>
            </td>
			<th class="heade">CP終了</th>
			<td>
                <telerik:RadDatePicker ID="DateCPOwari" Runat="server" OnSelectedDateChanged="DateCPOwari_SelectedDateChanged" AutoPostBack="true">
                </telerik:RadDatePicker>
            </td>
			<th class="heade">CP価格</th>
			<td>
                <asp:TextBox ID="TBCPKakaku" runat="server" Width="100" OnTextChanged="TBCPKakaku_TextChanged" AutoPostBack="true"></asp:TextBox>
            </td>
			<th class="heade">CP仕入</th>
			<td>
                <asp:TextBox ID="TBCPSiire" runat="server" Width="100" OnTextChanged="TBCPSiire_TextChanged" AutoPostBack="true"></asp:TextBox>
            </td>
		</tr>
	</tbody>
</table>
<br />
<asp:Label ID="Label1" runat="server" Text="左✓がついているものは一括編集できます。"></asp:Label>
<br />
<br />
<asp:GridView ID="G" runat="server" AutoGenerateColumns="False" OnRowDataBound="G_RowDataBound" HeaderStyle-BackColor="#00008B" HeaderStyle-ForeColor="White">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="C" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="商品名">
            <ItemTemplate>
                <asp:Label ID="lblSyouhin" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="仕入先">
            <ItemTemplate>
                <asp:Label ID="lblShiire" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="媒体">
            <ItemTemplate>
                <asp:Label ID="lblBaitai" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="カテゴリー">
            <ItemTemplate>
                <asp:Label ID="lblCate" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="期限開始">
            <ItemTemplate>
                <telerik:RadDatePicker ID="RadKaishi" runat="server"></telerik:RadDatePicker>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="期限終了">            
            <ItemTemplate>
                <telerik:RadDatePicker ID="RadOwari" runat="server"></telerik:RadDatePicker>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="標準価格">
            <ItemTemplate>
               <asp:TextBox ID="TbxHyojun" runat="server" Width="80"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="仕入価格">
            <ItemTemplate>
                <asp:TextBox ID="TbxShiire" runat="server" Width="80"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="CP開始" >
            <ItemTemplate>
                <telerik:RadDatePicker ID="RadCPKaishi" runat="server"></telerik:RadDatePicker>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="CP終了">
            <ItemTemplate>
                <telerik:RadDatePicker ID="RadCPOwari" runat="server"></telerik:RadDatePicker>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="CP価格">
            <ItemTemplate>
                <asp:TextBox ID="TbxCPKakaku" runat="server" Width="80"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="CP仕入">
            <ItemTemplate>
                <asp:TextBox ID="TbxCPShiire" runat="server" Width="80"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

<telerik:RadAjaxManager id="Ram" runat="server" onajaxrequest="Ram_AjaxRequest">
            <%--<ClientEvents OnResponseEnd="OnResponseEnd" OnRequestStart="OnRequestStart" /> --%>
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="BtnL">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="TblK" />
                        <telerik:AjaxUpdatedControl ControlID="TblList" LoadingPanelID="LP" 
                            UpdatePanelHeight="200px" UpdatePanelRenderMode="Inline" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
       </telerik:RadAjaxManager>
