<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlTyokuso.ascx.cs" Inherits="Gyomu.Master.CtlTyokuso" %>

<style type="text/css">
    th {
	        background: #00008B;
        color:white;
}
</style>

<table border="1">
	<tbody>
		<tr>
            <th>
                施設No
            </th>
            <td>
                <asp:TextBox ID="TbxFacility" runat="server"></asp:TextBox>
            </td>
            </tr>
        <tr>
			<th>コード</th>
			<td>
                <asp:TextBox ID="TbxCode" runat="server" Width ="300"></asp:TextBox>
            </td>
		</tr>
		<tr>
			<th>直送先名</th>
			<td>
                <asp:TextBox ID="TbxFacilityName1" runat="server" Width ="350"></asp:TextBox>
            </td>
		</tr>
        <tr>
			<th>直送先名2</th>
			<td>
                <asp:TextBox ID="TbxFacilityName2" runat="server" Width ="350"></asp:TextBox>
            </td>
		</tr>
        		<tr>
			<th>略称</th>
			<td>
                <asp:TextBox ID="TbxAbbreviation" runat="server" Width ="350"></asp:TextBox>
            </td>
		</tr>
		<tr>
			<th>担当</th>
			<td>
                <asp:TextBox ID="TbxFacilityResponsible" runat="server" Width ="300"></asp:TextBox>
            </td>
		</tr>
		<tr>
			<th>郵便番号</th>
			<td>
                <asp:TextBox ID="TbxPost" runat="server" Width ="300"></asp:TextBox>
            </td>
		</tr>
		<tr>
			<th>住所1</th>
			<td>
                <asp:TextBox ID="TbxAddress1" runat="server" Width ="350"></asp:TextBox>
            </td>
		</tr>
		<tr>
			<th>住所2</th>
			<td>
                <asp:TextBox ID="TbxAddress2" runat="server" Width ="350"></asp:TextBox>
            </td>
		</tr>
        <tr>
			<th>市町村コード</th>
			<td>
                <telerik:RadComboBox ID="RadCityCode" runat="server"></telerik:RadComboBox>
            </td>
		</tr>
		<tr>
			<th>Tell</th>
			<td>
                <asp:TextBox ID="TbxTEll" runat="server" Width ="300"></asp:TextBox>
            </td>
            </tr>
         <tr>
			<th>敬称</th>
			<td>
                <asp:TextBox ID="TbxTitles" runat="server" Width ="300"></asp:TextBox>
            </td>
		</tr>
        <tr>
			<th>利用状況</th>
			<td>
                <asp:CheckBox ID="ChkYuko" runat="server" Text="有効"/>
            </td>
		</tr>
	</tbody>
</table>
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