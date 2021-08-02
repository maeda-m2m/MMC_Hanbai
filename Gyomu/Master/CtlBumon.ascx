<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlBumon.ascx.cs" Inherits="Gyomu.Master.CtlBumon" %>

<style type="text/css">
    th {
        background: #00008B;
        color:white;
    }

</style>

<table border="1">
	<tbody>
		<tr>
			<th>部門区分</th>
			<td>
                <asp:TextBox ID="TbxKubun" runat="server" Width="300"></asp:TextBox>
			</td>
		</tr>
        <tr>
            <th>部署</th>
            <td>
                <asp:TextBox ID="TbxBusyo" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
		<tr>
		</tr>
		<tr>
			<th>郵便番号</th>
			<td>
                <asp:TextBox ID="TbxYubin1" runat="server" Width="50"></asp:TextBox>
                -
                <asp:TextBox ID="TbxYubin2" runat="server" Width="50"></asp:TextBox>

			</td>
		</tr>
		<tr>
			<th>部門住所1</th>
			<td>
                <asp:TextBox ID="TbxJusyo1" runat="server" Width="350"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th>部門住所2</th>
			<td>
                <asp:TextBox ID="TbxJusyo2" runat="server" Width="350"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th>部門部署</th>
			<td>
                <asp:TextBox ID="TbxBumon" runat="server" Width="300"></asp:TextBox>
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