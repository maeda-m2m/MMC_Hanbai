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

    .Btn3 {
        text-align: center;
        width: 95px;
        display: inline-block;
        text-decoration: none;
        color: red;
        border: solid 2px red;
        border-radius: 3px;
        transition: .4s;
        font-family: Meiryo;
        border-radius: 3px;
        background-color: white;
    }

        .Btn3:hover {
            background: red;
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

    #DivKakaku {
        width: 100%;
        overflow: scroll;
    }
</style>
<table border="1">
    <tbody>
        <tr>
            <td>
                <asp:Button ID="Button5" runat="server" Text="更新" OnClick="Button5_Click" CssClass="Btn2" />
            </td>
            <td>
                <p>※更新ボタンを押さなければデータは更新されません。</p>
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
    </tbody>
</table>

<input id="HidChkID" runat="server" type="hidden" />

<table>
    <tr>
        <td>
            <asp:Label runat="server" ID="LblStatus"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <p>赤枠で囲ってあるところは必須項目です。</p>
        </td>
    </tr>
</table>
<div runat="server" id="DivKakaku">

    <asp:DataGrid runat="server" ID="D" AutoGenerateColumns="False" OnItemDataBound="D_ItemDataBound" CssClass="scdl" HeaderStyle-Width="200px" OnItemCommand="D_ItemCommand">
        <AlternatingItemStyle BackColor="#efefff" />
        <Columns>
            <asp:TemplateColumn HeaderText="" ItemStyle-Width="60px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="scdl" />
                <ItemTemplate>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="Delete" runat="server" Text="×" CssClass="Btn3" CommandName="Del" Width="30px" Font-Bold="true" Font-Size="16px" />
                            </td>
                            <td>
                                <asp:ImageButton ImageUrl="~/Img/コピー.jpg" AlternateText="この行を複写" runat="server" ID="BtnCopy" Width="30px" CommandName="Copy" />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="許諾開始日" HeaderStyle-Width="" ItemStyle-Height="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" />
                <ItemTemplate>
                    <telerik:RadDatePicker runat="server" ID="RdpPermissionStart" Width="100px"></telerik:RadDatePicker>
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="許諾終了日" HeaderStyle-Width="" ItemStyle-Height="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" />
                <ItemTemplate>
                    <telerik:RadDatePicker runat="server" ID="RdpRightEnd" Width="100px"></telerik:RadDatePicker>
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="カテゴリー" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="scdl" BorderColor="Red" BorderWidth="2px" />
                <ItemTemplate>
                    <telerik:RadComboBox runat="server" ID="RcbCategory" Width="100px" OnSelectedIndexChanged="RcbCategory_SelectedIndexChanged" AutoPostBack="true">
                    </telerik:RadComboBox>
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="ジャケット印刷" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="scdl" />
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="ChkJacket" />
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="許諾書印刷" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="scdl" />
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="ChkKyodakusyo" />
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="返却" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="scdl" />
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="ChkReturn" />
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="報告書" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="scdl" />
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="ChkHoukokusyo" />
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="メディア" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="scdl" BorderColor="Red" BorderWidth="2px" />
                <ItemTemplate>
                    <telerik:RadComboBox runat="server" ID="RcbMedia" Width="80px">
                        <Items>
                            <telerik:RadComboBoxItem Text="" />
                            <telerik:RadComboBoxItem Text="DVD" Value="DVD" />
                            <telerik:RadComboBoxItem Text="BD" Value="BD" />
                            <telerik:RadComboBoxItem Text="CD" Value="CD" />
                            <telerik:RadComboBoxItem Text="HD" Value="HD" />
                            <telerik:RadComboBoxItem Text="LN" Value="LN" />
                        </Items>
                    </telerik:RadComboBox>
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="仕入先コード" ItemStyle-Width="70px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="scdl" BorderColor="Red" BorderWidth="2px" />
                <ItemTemplate>
                    <asp:HiddenField runat="server" ID="HidShiireCode" />
                    <asp:Label runat="server" ID="LblShiireCode"></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>


            <asp:TemplateColumn HeaderText="仕入先名" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="scdl" BorderColor="Red" BorderWidth="2px" />
                <ItemTemplate>
                    <telerik:RadComboBox ID="RadMakerRyaku" runat="server" Width="100px" AllowCustomText="false" ShowMoreResultsBox="true" ShowToggleImage="false" EnableLoadOnDemand="true" OnItemsRequested="RadMakerRyaku_ItemsRequested" OnClientSelectedIndexChanged="ShiiresakiChanged"></telerik:RadComboBox>
                    <input type="hidden" id="ShiireCode" runat="server" />
                    <input type="hidden" id="MakerNumber" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="品番" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="scdl" BorderColor="Red" BorderWidth="2px" />
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="TbxMakerHinban" Width="100px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="範囲" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" BorderColor="Red" BorderWidth="2px" />
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="TbxHanni" Width="100px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="倉庫" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" />
                <ItemTemplate>
                    <telerik:RadComboBox runat="server" ID="RcbWareHouse" AllowCustomText="True" EnableLoadOnDemand="True" MarkFirstMatch="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" Width="50px">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="" />
                            <telerik:RadComboBoxItem runat="server" Text="発注" Value="発注" />
                            <telerik:RadComboBoxItem runat="server" Text="在庫" Value="在庫" />
                            <telerik:RadComboBoxItem runat="server" Text="委託" Value="委託" />
                        </Items>
                    </telerik:RadComboBox>
                </ItemTemplate>
            </asp:TemplateColumn>


            <asp:TemplateColumn HeaderText="標準価格" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" BorderColor="Red" BorderWidth="2px" />
                <ItemTemplate>
                    <asp:TextBox ID="TbxHyoujunKakaku" runat="server" CssClass="Kakaku"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="仕入価格" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" BorderColor="Red" BorderWidth="2px" />
                <ItemTemplate>
                    <asp:TextBox ID="TbxShiireKakaku" runat="server" CssClass="Kakaku"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="キャンペーン開始" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" />
                <ItemTemplate>
                    <telerik:RadDatePicker runat="server" ID="RdpCpKaishi" Width="100px"></telerik:RadDatePicker>
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="キャンペーン終了" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" />
                <ItemTemplate>
                    <telerik:RadDatePicker runat="server" ID="RdpCpOwari" Width="100px"></telerik:RadDatePicker>
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="キャンペーン価格" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" />
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="TbxCpKakaku" CssClass="Kakaku"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="キャンペーン仕入価格" ItemStyle-Width="100px">
                <HeaderStyle Wrap="true" />
                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" />
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="TbxCpShiire" CssClass="Kakaku"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>

        <HeaderStyle Width="80px" BackColor="#00008B" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="12px"></HeaderStyle>
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="10px" />
    </asp:DataGrid>

</div>
<asp:Button runat="server" ID="BtnAdd" Text="＋" Font-Bold="true" Font-Size="Larger" CssClass="Btn2" Width="80px" Height="50px" OnClick="BtnAdd_Click" />
<script type="text/javascript">
    function ShiiresakiChanged(sender, eventArgs) {
        debugger;
        var id = sender.get_element().id;
        var rcb = $find(id);
        var lbl = document.getElementById(id.replace("RadMakerRyaku", "LblShiireCode"));
        lbl.innerText = rcb.get_selectedItem().get_value();
    }
</script>
