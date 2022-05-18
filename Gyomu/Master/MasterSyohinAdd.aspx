<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterSyohinAdd.aspx.cs" Inherits="Gyomu.Master.MasterSyohinAdd" %>

<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="https://code.jquery.com/jquery-3.1.0.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>

    <title>新規商品登録</title>
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

        #overlay {
            position: fixed;
            top: 0;
            z-index: 100;
            width: 100%;
            height: 100%;
            display: none;
            background: rgba(0,0,0,0.6);
        }

        .cv-spinner {
            height: 100%;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .spinner {
            width: 40px;
            height: 40px;
            border: 4px #ddd solid;
            border-top: 4px #2e93e6 solid;
            border-radius: 50%;
            animation: sp-anime 0.8s infinite linear;
        }

        @keyframes sp-anime {
            100% {
                transform: rotate(360deg);
            }
        }

        .is-hide {
            display: none;
        }
    </style>

    <link href="../../MainStyle.css" type="text/css" rel="STYLESHEET" />
    <link href="../../Style/Grid.ykuri.css" rel="STYLESHEET" />
    <link href="../../Style/ComboBox.ykuri.css" type="text/css" rel="STYLESHEET" />

</head>
<body>
    <script type="text/javascript">
        const elements = ':focusable:not(a)';
        document.onkeydown = keydown;
        var beforeKey;

        function keydown(event) {
            var code = event.keyCode;//13   がエンターキー
            if (code == 13) {
                debugger;
                var focusD = document.activeElement;
                if (!String(focusD).includes('Delete')) {
                    debugger;
                    if (!String(focusD).includes('BtnCopy')) {
                        debugger;
                        //const t = event.target;
                        //const { selectionStart: start, selectionEnd: end } = t;
                        //t.value = `${t.value.slice(0, start)}\n${t.value.slice(end)}`;
                        //t.selectionStart = t.selectionEnd = start + 1;
                        //return;
                        let sortedList = $(elements).sort((a, b) => {
                            debugger;
                            if (a.tabIndex && b.tabIndex) {
                                return a.tabIndex - b.tabIndex;
                            } else if (a.tabIndex && !b.tabIndex) {
                                return -1;
                            } else if (!a.tabIndex && b.tabIndex) {
                                return 1;
                            }
                            return 0;
                        });

                        if (event.target.tabIndex < 0) {
                            // tabindexがマイナスの場合、DOM上で次の項目へ移動するためソート前の項目から検索する
                            sortedList = elements;
                        }

                        // 現在の項目位置から、移動先を取得する
                        const index = $(sortedList).index(event.target);
                        const nextFilter = event.shiftKey ? `:lt(${index}):last` : `:gt(${index}):first`;
                        const nextTarget = $(sortedList).filter(nextFilter);

                        // shift + enterでtagindexがマイナスの項目へ移動するのを防ぐ
                        if (!nextTarget.length || nextTarget[0].tabIndex < 0) return;

                        // フォーカス移動＋文字列選択
                        nextTarget.focus();
                        if (typeof nextTarget.select === 'function' && nextTarget[0].tagName === 'INPUT') {
                            nextTarget.select();
                        }
                        return false;
                    }
                }
            }
        }
    </script>

    <div id="overlay">
        <div class="cv-spinner">
            <span class="spinner"></span>
            <script type="text/javascript">
                jQuery(function ($) {
                    $(document).ajaxSend(function () {
                        $("#overlay").fadeIn(300);
                    });

                    $('#Button5').click(function () {
                        $("#overlay").fadeIn(300);
                    }).done(function () {
                        setTimeout(function () {
                            $("#overlay").fadeOut(300);
                        }, 100000);
                    });

                });
            </script>
        </div>
    </div>

    <form id="form1" runat="server">
        <div id="MainMenu" runat="server">
            <uc:Menu runat="server" ID="Menu" />
        </div>
        <telerik:RadTabStrip ID="RT" runat="server" Skin="Office2007" AutoPostBack="True" SelectedIndex="0">
            <Tabs>
                <telerik:RadTab Text="商品登録" Font-Size="12pt" NavigateUrl="MasterSyohinAdd.aspx" Selected="True">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <br />
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

                    <asp:TemplateColumn HeaderText="仕入先コード" ItemStyle-Width="100px">
                        <HeaderStyle Wrap="true" />
                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" />
                        <ItemTemplate>
                            <asp:Label runat="server" ID="LblShiireCode"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>


                    <asp:TemplateColumn HeaderText="仕入先名" ItemStyle-Width="100px">
                        <HeaderStyle Wrap="true" />
                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="scdl" BorderColor="Red" BorderWidth="2px" />
                        <ItemTemplate>
                            <telerik:RadComboBox ID="RadMakerRyaku" runat="server" Width="100px" OnItemsRequested="RadMakerRyaku_ItemsRequested" AllowCustomText="false" ShowMoreResultsBox="true" ShowToggleImage="false" EnableLoadOnDemand="true" OnClientSelectedIndexChanged="ShiiresakiChanged"></telerik:RadComboBox>
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
                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" />
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

    </form>
</body>
</html>
