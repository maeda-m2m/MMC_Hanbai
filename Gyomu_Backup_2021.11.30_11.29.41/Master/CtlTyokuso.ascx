<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlTyokuso.ascx.cs" Inherits="Gyomu.Master.CtlTyokuso" %>

<style type="text/css">
    th {
        background: #00008B;
        color: white;
    }
</style>

<table border="1">
    <tbody>
        <tr>
            <th>施設No
            </th>
            <td>
                <asp:TextBox ID="TbxFacility" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>コード</th>
            <td>
                <asp:TextBox runat="server" ID="TbxCode"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>直送先名</th>
            <td>
                <asp:TextBox ID="TbxTyokusousakiName1" runat="server" Width="350"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>直送先名2</th>
            <td>
                <asp:TextBox ID="TbxTyokusousakiName2" runat="server" Width="350"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>略称</th>
            <td>
                <asp:TextBox ID="TbxTyokusousakiRyakusyou" runat="server" Width="350"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>担当</th>
            <td>
                <asp:TextBox ID="TbxTyokusousakiTantou" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>郵便番号</th>
            <td>
                <asp:TextBox ID="TbxTyokusousakiYubin" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>住所1</th>
            <td>
                <asp:TextBox ID="TbxTyokusousakiAddress1" runat="server" Width="350"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>住所2</th>
            <td>
                <asp:TextBox ID="TbxTyokusousakiAddress2" runat="server" Width="350"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>市町村コード</th>
            <td>
                <telerik:RadComboBox ID="RcbCityCode" runat="server"></telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <th>Tell</th>
            <td>
                <asp:TextBox ID="TbxTyokusousakiTell" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>敬称</th>
            <td>
                <asp:TextBox ID="TbxKeisyo" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
    </tbody>
</table>
