<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterSyohinAdd.aspx.cs" Inherits="Gyomu.Master.MasterSyohinAdd" %>

<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
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
            width: 90px;
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
            font-size:12px;
            text-align: center;
            width: 200px;
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

            .Btn3:hover {
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

    <link href="../../MainStyle.css" type="text/css" rel="STYLESHEET" />
    <link href="../../Style/Grid.ykuri.css" rel="STYLESHEET" />
    <link href="../../Style/ComboBox.ykuri.css" type="text/css" rel="STYLESHEET" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="MainMenu" runat="server">
                <uc:Menu ID="Menu" runat="server" />
            </div>
            <telerik:RadTabStrip ID="RT" runat="server" Skin="Office2007" AutoPostBack="True" SelectedIndex="1">
                <Tabs>
                    <telerik:RadTab Text="商品追加" Font-Size="12pt" NavigateUrl="MasterSyohinAdd.aspx" Selected="true">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <asp:Label ID="Err" runat="server" Text=""></asp:Label>
            <table>
                <tr>
                    <td>
                        <p>CSVファイルによる一括商品登録</p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="../Format/UploadFormat3.xlsx" class="Btn3" type="text/xml">フォーマットをダウンロード</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <p>データを入力した後、CSVファイルにて保存して下さい。
                        </p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input id="File1" type="file" runat="server" />
                    </td>
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="アップロード" OnClick="Button1_Click" />
                    </td>
                </tr>
            </table>

            <table border="1">
                <tbody>
                    <tr>
                        <td>
                            <asp:Button ID="Button5" runat="server" Text="登録" OnClick="Button5_Click" CssClass="Btn2" />

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
                            <telerik:RadComboBox ID="RadBaitai" runat="server" Width="500px" Culture="ja-JP" AllowCustomText="True" EnableLoadOnDemand="True" AutoPostBack="True" OnSelectedIndexChanged="RadBaitai_SelectedIndexChanged">
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
                            <telerik:RadComboBox ID="Souko" runat="server" Width="500px" Culture="ja-JP" AllowCustomText="True" EnableLoadOnDemand="True" AutoPostBack="True" OnSelectedIndexChanged="Souko_SelectedIndexChanged">
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
                            <telerik:RadComboBox ID="RadShiire" runat="server" Width="500" OnSelectedIndexChanged="RadShiire_SelectedIndexChanged" AllowCustomText="True" EnableLoadOnDemand="True" AutoPostBack="True" OnItemsRequested="RadShiire_ItemsRequested"></telerik:RadComboBox>
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

        </div>
        <div>
            <asp:DataGrid runat="server" ID="D" AutoGenerateColumns="False" OnItemDataBound="D_ItemDataBound" CssClass="scdl" HeaderStyle-Width="200px">
                <AlternatingItemStyle BackColor="#efefff" />
                <Columns>

                    <asp:TemplateColumn HeaderText="商品コード" HeaderStyle-Width="" ItemStyle-Height="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                        <HeaderStyle Wrap="true" />
                        <ItemStyle Wrap="true" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="LblSyouhinCode" runat="server" Text="" CssClass="Kakaku"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="商品名" ItemStyle-Width="100px">
                        <HeaderStyle Wrap="true" />
                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="scdl" />
                        <ItemTemplate>
                            <asp:Label ID="LblSyouhinMei" runat="server" Text="" CssClass="Kakaku"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="メーカー品番" ItemStyle-Width="100px">
                        <HeaderStyle Wrap="true" />
                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="scdl" />
                        <ItemTemplate>
                            <asp:Label ID="LblMakerHinban" runat="server" Text="" CssClass="Kakaku"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="媒体" ItemStyle-Width="100px">
                        <HeaderStyle Wrap="true" />
                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="scdl" />
                        <ItemTemplate>
                            <asp:Label ID="LblMedia" runat="server" Text="" CssClass="Kakaku"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="範囲" ItemStyle-Width="100px">
                        <HeaderStyle Wrap="true" />
                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" />
                        <ItemTemplate>
                            <asp:Label ID="LblRange" runat="server" Text="" CssClass="Kakaku"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="倉庫コード" ItemStyle-Width="100px">
                        <HeaderStyle Wrap="true" />
                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" />
                        <ItemTemplate>
                            <asp:Label ID="LblWarehouse" runat="server" Text="" CssClass="Kakaku"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>


                    <asp:TemplateColumn HeaderText="仕入先コード" ItemStyle-Width="100px">
                        <HeaderStyle Wrap="true" />
                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="scdl" />
                        <ItemTemplate>
                            <asp:Label ID="LblShiiresakiCode" runat="server" Text="" CssClass="Kakaku"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="仕入名" ItemStyle-Width="100px">
                        <HeaderStyle Wrap="true" />
                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="scdl" />
                        <ItemTemplate>
                            <asp:Label ID="LblShiireMei" runat="server" Text="" CssClass="Kakaku"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="標準価格" ItemStyle-Width="100px">
                        <HeaderStyle Wrap="true" />
                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="scdl" />
                        <ItemTemplate>
                            <asp:Label ID="LblHyojunKakaku" runat="server" Text="" CssClass="Kakaku"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="許諾開始" ItemStyle-Width="100px">
                        <HeaderStyle Wrap="true" />
                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="scdl" />
                        <ItemTemplate>
                            <asp:Label ID="LblPermissionStart" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="許諾終了" ItemStyle-Width="100px">
                        <HeaderStyle Wrap="true" />
                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="scdl" />
                        <ItemTemplate>
                            <asp:Label ID="LblPermissionEnd" runat="server" Text="" CssClass="Kakaku"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>


                </Columns>

                <HeaderStyle Width="80px" BackColor="#00008B" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="12px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="10px" />
            </asp:DataGrid>

        </div>
        <div>
            <table>
                <tr>
                    <td>
                        <p>上記の内容で</p>
                    </td>
                    <td>
                        <asp:Button ID="BtnRegister" runat="server" Text="登録" CssClass="Btn2" OnClick="BtnRegister_Click" />
                    </td>
                    <td>
                        <p>する</p>
                    </td>
                </tr>
            </table>
            <asp:Label ID="Message" runat="server" Text="" ForeColor="Green"></asp:Label>
        </div>
    </form>
</body>
</html>
