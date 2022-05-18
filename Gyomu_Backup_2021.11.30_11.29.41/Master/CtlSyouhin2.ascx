<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlSyouhin2.ascx.cs" Inherits="Gyomu.Master.CtlSyouhin2" %>
<br />
<asp:Label ID="lblMsg" runat="server" Text="カテゴリーマスタの登録はできません。"></asp:Label>
<br />
<br />
<asp:DataGrid runat="server" ID="ProductList" AutoGenerateColumns="False"  CssClass="scdl" HeaderStyle-Width="200px" OnItemDataBound="ProductList_ItemDataBound">
    <AlternatingItemStyle BackColor="#efefff" />
    <Columns>
        <asp:TemplateColumn HeaderText="商品コード" HeaderStyle-Width="" ItemStyle-Height="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
            <HeaderStyle Wrap="true" />
            <ItemStyle Wrap="true" HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="LblProductCode" runat="server" Text=""></asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>

        <asp:TemplateColumn HeaderText="商品名" ItemStyle-Width="100px">
            <HeaderStyle Wrap="true" />
            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="scdl" />
            <ItemTemplate>
                <asp:Label ID="LblProductName" runat="server" Text=""></asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>

        <asp:TemplateColumn HeaderText="メディア" ItemStyle-Width="100px">
            <HeaderStyle Wrap="true" />
            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="scdl" />
            <ItemTemplate>
                <asp:Label ID="LblMedia" runat="server" Text=""></asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>

        <asp:TemplateColumn HeaderText="仕入先コード" ItemStyle-Width="100px">
            <HeaderStyle Wrap="true" />
            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="scdl" />
            <ItemTemplate>
                <asp:Label ID="LblShiireCode" runat="server" Text=""></asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>

        <asp:TemplateColumn HeaderText="仕入先名" ItemStyle-Width="100px">
            <HeaderStyle Wrap="true" />
            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="scdl" />
            <ItemTemplate>
                <asp:Label ID="LblShiireName" runat="server" Text=""></asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
    <HeaderStyle Width="200px" BackColor="#00008B" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="12px"></HeaderStyle>
    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="10px" />
</asp:DataGrid>

