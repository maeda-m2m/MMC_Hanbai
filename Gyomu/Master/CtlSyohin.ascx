<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlSyohin.ascx.cs" Inherits="Gyomu.Master.CtlSyohin" %>

<style type="text/css">
    th {
        background: #00008B;
        color: white;
    }

    .Grid {
        text-align: center;
    }

    .Btn2 {
        text-align: center;
        width: 95px;
        display: inline-block;
        padding: 0.3em 1em;
        text-decoration: none;
        color: #00008B;
        border: solid 2px #00008B;
        border-radius: 3px;
        transition: .4s;
        font-family: Meiryo;
        border-radius: 3px;
        background-color: white;
    }

        .Btn2:hover {
            background: #00008B;
            color: white;
        }


    #Delete {
        text-align: center;
        width: 95px;
        display: inline-block;
        padding: 0.3em 1em;
        text-decoration: none;
        color: #00aaff;
        border: solid 2px #00aaff;
        border-radius: 3px;
        transition: .4s;
        font-family: Meiryo;
        border-radius: 3px;
        background-color: white;
    }

        #Delete:hover {
            background: #00aaff;
            color: white;
        }

    .Kakaku {
        margin: 0;
        padding: 0;
        width: 60px;
        margin-left: 5px;
    }
</style>

<table border="1">
    <tbody>
        <tr>
            <td>
                <asp:Button ID="Button5" runat="server" Text="更新" OnClick="Button5_Click" CssClass="Btn2" />

                <td></td>
            </td>
        </tr>
        <tr>
            <th>商品コード</th>
            <td>
                <asp:TextBox ID="TbxCode" runat="server" Width="500"></asp:TextBox></td>
        </tr>
        <tr>
            <th>商品名</th>
            <td>
                <asp:TextBox ID="TbxSyohinMei" runat="server" Width="500"></asp:TextBox></td>
        </tr>
        <%--		<tr>
			<th>明細区分</th>
			<td>
                <asp:TextBox ID="TbxKubun" runat="server" Width="500"></asp:TextBox></td>
		</tr>--%>
        <tr>
            <th>媒体名</th>
            <td>
                <telerik:RadComboBox ID="RadBaitai" runat="server" Width="500px" Culture="ja-JP" AllowCustomText="True" EnableLoadOnDemand="True" AutoPostBack="True">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Value="" />
                        <telerik:RadComboBoxItem runat="server" Text="字幕" Value="字幕" />
                        <telerik:RadComboBoxItem runat="server" Text="吹替" Value="吹替" />
                        <telerik:RadComboBoxItem runat="server" Text="DVD" Value="DVD" />
                        <telerik:RadComboBoxItem runat="server" Text="BD" Value="BD" />
                    </Items>
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <th>範囲</th>
            <td>
                <asp:TextBox ID="TbxTosyoCode" runat="server" Width="500"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>倉庫コード</th>
            <td>
                <telerik:RadComboBox ID="Souko" runat="server" Width="500px" Culture="ja-JP" AllowCustomText="True" EnableLoadOnDemand="True" AutoPostBack="True">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Value="" />
                        <telerik:RadComboBoxItem runat="server" Text="発注" Value="発注" />
                        <telerik:RadComboBoxItem runat="server" Text="在庫" Value="在庫" />
                    </Items>
                </telerik:RadComboBox>

            </td>
        </tr>
        <tr>
            <th>仕入先名</th>
            <td>
                <telerik:RadComboBox ID="RadShiire" runat="server" Width="500" OnSelectedIndexChanged="RadShiire_SelectedIndexChanged" AllowCustomText="True" EnableLoadOnDemand="True" AutoPostBack="True"></telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <th>利用状態</th>
            <td>
                <asp:RadioButtonList ID="ChkJotai" runat="server">
                    <asp:ListItem Value="有効" Selected="True">有効</asp:ListItem>
                    <asp:ListItem Value="無効">無効</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </tbody>
</table>

<input id="HidChkID" runat="server" type="hidden" />

<table>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            </td>

        </td>
    </tr>
</table>
<div>

    <asp:DataGrid runat="server" ID="D" AutoGenerateColumns="False" OnItemDataBound="D_ItemDataBound" CssClass="scdl" HeaderStyle-Width="200px" OnItemCommand="D_ItemCommand">
        <AlternatingItemStyle BackColor="#efefff" />
        <Columns>
            <asp:TemplateColumn HeaderText="" ItemStyle-Width="90px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="scdl" />
                <ItemTemplate>
                    <asp:Button ID="Delete" runat="server" Text="追加" CssClass="Btn2" CommandName="add" />
                </ItemTemplate>
            </asp:TemplateColumn>


            <asp:TemplateColumn HeaderText="カテゴリーコード" HeaderStyle-Width="" ItemStyle-Height="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" />
                <ItemTemplate>
                    <asp:Label ID="LblCatecode" runat="server" Text="" CssClass="Kakaku"></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="カテゴリー" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="scdl" />
                <ItemTemplate>
                    <asp:Label ID="Lblcatename" runat="server" Text="" CssClass="Kakaku"></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="メーカー名" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="scdl" />
                <ItemTemplate>
                    <telerik:RadComboBox ID="RadMakerRyaku" runat="server" AllowCustomText="True" EnableLoadOnDemand="True" MarkFirstMatch="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" Width="100px" OnItemsRequested="RadMakerRyaku_ItemsRequested"></telerik:RadComboBox>
                    <input type="hidden" id="ShiireCode" runat="server" />
                    <input type="hidden" id="MakerNumber" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="範囲" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="scdl" />
                <ItemTemplate>
                    <telerik:RadComboBox ID="Hanni" runat="server" CssClass="Kakaku" Width="100px"></telerik:RadComboBox>
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="標準価格" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" />
                <ItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="Kakaku"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="仕入価格" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" />
                <ItemTemplate>
                    <asp:TextBox ID="Shiire" runat="server" CssClass="Kakaku"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateColumn>


            <asp:TemplateColumn HeaderText="許諾開始日" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="scdl" />
                <ItemTemplate>
                    <telerik:RadDatePicker ID="RadDatePicker3" runat="server"></telerik:RadDatePicker>
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="権利終了" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="scdl" />
                <ItemTemplate>
                    <telerik:RadDatePicker ID="RadDatePicker4" runat="server"></telerik:RadDatePicker>
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="ジャケット印刷" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="scdl" />
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox3" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="返却処理" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="scdl" />
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox4" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>

        </Columns>

        <HeaderStyle Width="80px" BackColor="#00008B" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="12px"></HeaderStyle>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="10px" />
    </asp:DataGrid>
</div>

<%--<telerik:RadAjaxManager ID="Ram" runat="server" OnAjaxRequest="Ram_AjaxRequest">
    <%--<ClientEvents OnResponseEnd="OnResponseEnd" OnRequestStart="OnRequestStart" /> --%>
<%--    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="BtnL">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="TblK" />
                <telerik:AjaxUpdatedControl ControlID="TblList" LoadingPanelID="LP"
                    UpdatePanelHeight="200px" UpdatePanelRenderMode="Inline" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>--%>
