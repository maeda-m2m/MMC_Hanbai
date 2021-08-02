<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlTokuisaki.ascx.cs" Inherits="Gyomu.Master.CtlTokuisaki" %>

<style type="text/css">
    th {
        background: #00008B;
        color: white;
    }

    .auto-style1 {
        font-family: "Segoe UI", Arial, Helvetica, sans-serif;
        font-size: 12px;
        color: #333333;
        vertical-align: middle;
    }
</style>

<table border="1">
    <tbody>
        <tr>
            <th>顧客コード<span style="color: red">*</span>
            </th>
            <td>
                <asp:TextBox runat="server" ID="TbxCustomerCode" placeholder="アルファベット1文字"></asp:TextBox>
            </td>
            <th>得意先コード<span style="color: red">*</span></th>
            <td>
                <asp:TextBox ID="TbxTokuiCode" runat="server" placeholder="数字"></asp:TextBox>
            </td>
            <th>得意先名1</th>
            <td>
                <asp:TextBox ID="TbxTokuiMei1" runat="server"></asp:TextBox>
            </td>
            <th>得意先名2</th>
            <td>
                <asp:TextBox ID="TbxTokuiMei2" runat="server"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <th>得意先略称<span style="color: red">*</span></th>
            <td>
                <asp:TextBox runat="server" ID="TbxTokuisakiRyakusyo"></asp:TextBox>
            </td>
            <th>得意先振り仮名</th>
            <td>
                <asp:TextBox ID="TbxHurigana" runat="server"></asp:TextBox>
            </td>
            <th>得意先Tell<span style="color: red">*</span></th>
            <td>
                <asp:TextBox ID="TbxTokuiTell" runat="server" placeholder="ハイフン「-」を含む"></asp:TextBox>
            </td>
            <th>得意先Fax</th>
            <td>
                <asp:TextBox ID="TbxTokuiFax" runat="server" placeholder="ハイフン「-」を含む"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>得意先住所1<span style="color: red">*</span></th>
            <td>
                <asp:TextBox ID="TbxTokuiJusyo1" runat="server"></asp:TextBox>
            </td>
            <th>得意先住所2</th>
            <td>
                <asp:TextBox ID="TbxTokuiJusyo2" runat="server"></asp:TextBox>
            </td>
            <th>得意先郵便</th>
            <td>
                <asp:TextBox ID="TbxTokuiYubin" runat="server" placeholder="ハイフン「-」を含む"></asp:TextBox>
            </td>
            <th>市町村名<span style="color: red">*</span></th>
            <td>
                <telerik:RadComboBox ID="RadCityCode" runat="server" ShowMoreResultsBox="true" EnableVirtualScrolling="true" AllowCustomText="true" ShowToggleImage="false" EnableLoadOnDemand="true" OnItemsRequested="RadCityCode_ItemsRequested">
                </telerik:RadComboBox>
            </td>

        </tr>
        <tr>
            <th>得意先担当者<span style="color: red">*</span></th>
            <td>
                <asp:TextBox ID="TbxTokuiTanto" runat="server"></asp:TextBox>
            </td>
            <th>得意先担当部署</th>
            <td>
                <asp:TextBox ID="TbxTokuiBusyo" runat="server"></asp:TextBox>
            </td>
            <th>敬称<span style="color: red">*</span></th>
            <td>
                <asp:DropDownList ID="DrpKeisyo" runat="server">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="1">様</asp:ListItem>
                    <asp:ListItem Value="2">御中</asp:ListItem>
                </asp:DropDownList>
            </td>
            <th>掛率(%)<span style="color: red">*</span></th>
            <td>
                <asp:TextBox ID="TbxKakeritu" runat="server"></asp:TextBox>
            </td>

        </tr>
        <%--        <tr>
            <th>得意先担当Tell<span style="color: red">*</span></th>
            <td>
                <asp:TextBox ID="TbxTokuiTantoTell" runat="server"></asp:TextBox>
            </td>
            <th>得意先担当Mail</th>
            <td>
                <asp:TextBox ID="TbxTokuiTantoMail" runat="server"></asp:TextBox>
            </td>
        </tr>--%>
        <%--        <tr>
                        <th>官民区分</th>
            <td>
                <asp:DropDownList ID="DrpKanmin" runat="server">
                    <asp:ListItem Value=""> </asp:ListItem>
                    <asp:ListItem Value="0">その他</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>--%>
        <tr>
            <th>主担当名<span style="color: red">*</span></th>
            <td>
                <telerik:RadComboBox ID="RadTanto" runat="server">
                </telerik:RadComboBox>
            </td>
            <th>締日区分<span style="color: red">*</span></th>
            <td>
                <asp:DropDownList ID="DrpSimebi" runat="server">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="05">5日締め</asp:ListItem>
                    <asp:ListItem Value="10">10日締め</asp:ListItem>
                    <asp:ListItem Value="15">15日締め</asp:ListItem>
                    <asp:ListItem Value="20">20日締め</asp:ListItem>
                    <asp:ListItem Value="25">25日締め</asp:ListItem>
                    <asp:ListItem Value="99">月末締め</asp:ListItem>
                    <asp:ListItem Value="00">随時締め</asp:ListItem>
                </asp:DropDownList>
            </td>
            <th>税込通知</th>
            <td>
                <asp:DropDownList ID="DrpTuti" runat="server">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="0">伝票単位</asp:ListItem>
                    <asp:ListItem Value="1">請求単位</asp:ListItem>
                    <asp:ListItem Value="2">免税</asp:ListItem>
                    <asp:ListItem Value="3">無税</asp:ListItem>
                    <asp:ListItem Value="4">明細単位</asp:ListItem>
                </asp:DropDownList>
            </td>
            <th>口座番号</th>
            <td>
                <asp:TextBox ID="TbxKoza" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <%--            <th>請求先略称</th>
            <td class="auto-style1">
                <asp:TextBox ID="TbxRyakusyo" runat="server"></asp:TextBox>
            </td>--%>
        </tr>
        <tr>
            <%--            <th>売上端数方法<span style="color: red">*</span></th>
            <td>
                <asp:DropDownList ID="DrpUHasu" runat="server">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="1">四捨五入</asp:ListItem>
                    <asp:ListItem Value="2">切上</asp:ListItem>
                    <asp:ListItem Value="3">切捨て</asp:ListItem>
                </asp:DropDownList>
            </td>
            <th>消費税端数方法</th>
            <td>
                <asp:DropDownList ID="DrpSZHasu" runat="server">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="1">四捨五入</asp:ListItem>
                    <asp:ListItem Value="2">切上</asp:ListItem>
                    <asp:ListItem Value="3">切捨て</asp:ListItem>
                </asp:DropDownList>
            </td>--%>
            <%--            <th>与信額</th>
            <td>
                <asp:TextBox ID="TbxYosingaku" runat="server"></asp:TextBox>
            </td>
        </tr>--%>
        <tr>
            <%--            <th>会社銀行名</th>
            <td>
                <asp:DropDownList ID="drpGinko" runat="server">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="9">なでしこ</asp:ListItem>
                </asp:DropDownList>
            </td>--%>
        </tr>
        <tr>
            <th>利用状態<span style="color: red">*</span></th>
            <td>
                <asp:CheckBox ID="ChkRiyo" runat="server" Text="有効" />
            </td>
            <th>更新ユーザー</th>
            <td>
                <asp:Label ID="lblUser" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <p>※　<span style="color: red">*</span>がついている項目は必須項目です。</p>
            </td>
        </tr>
    </tbody>
</table>
