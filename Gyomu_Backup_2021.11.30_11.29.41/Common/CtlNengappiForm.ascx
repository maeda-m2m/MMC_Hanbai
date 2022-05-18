<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlNengappiForm.ascx.cs" Inherits="Gyomu.Common.CtlNengappiForm" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table id="T" cellspacing="0" cellpadding="1" border="0">
<tr>
		<td noWrap>
			<table id="TblFrom" cellspacing="0" cellpadding="0" border="0" runat="server">
				<tr>
					<td noWrap>
						<telerik:RadDatePicker id="RdpFrom" ToolTip="" MinDate="1950-01-01" 
                            Runat="server" Skin="Web20" Width="120px">

						</telerik:RadDatePicker></td>
				</tr>
			</table>
		</td>
		<td noWrap>
			<asp:DropDownList id="DdlKikan" runat="server">
				<asp:ListItem Value="0">指定しない</asp:ListItem>
				<asp:ListItem Value="1">指定日</asp:ListItem>
				<asp:ListItem Value="2">以前</asp:ListItem>
				<asp:ListItem Value="3">以降</asp:ListItem>
				<asp:ListItem Value="4">から</asp:ListItem>
			</asp:DropDownList></td>
	</tr>
	<tr>
		<td noWrap>
			<table id="TblTo" cellspacing="0" cellpadding="0" border="0" runat="server">
				<tr>
					<td noWrap>
						<telerik:RadDatePicker id="RdpTo" Runat="server" MinDate="1950-01-01" 
                            Skin="Web20" Width="120px">
							<Calendar ShowRowHeaders="False" Skin="Web20"></Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
							<DateInput DisplayDateFormat="yyyy/MM/dd" LabelCssClass="radLabelCss_Default" Width="76px"
								DateFormat="yyyy/MM/dd" Font-Size="10pt" RangeValidation="Immediate" DisplayPromptChar="_"
								PromptChar=" "></DateInput>
						</telerik:RadDatePicker></td>
				</tr>
			</table>
		</td>
		<td noWrap></td>
	</tr>
</table>

     
