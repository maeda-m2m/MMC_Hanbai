<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShouhinSearch.aspx.cs" Inherits="Gyomu.Tokuisaki.ShouhinSearch" %>


<%@ Register Src="~/CtrlMitsuSyousai.ascx" TagName="Syosai" TagPrefix="uc2" %>
<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="css/ShouhinSearch.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

        <header style="background-color: white;">
            <uc:Menu ID="Menu" runat="server" />
            <telerik:RadTabStrip ID="RT" runat="server" AutoPostBack="True" SelectedIndex="0" BackColor="#D2EAF6">
                <Tabs>

                    <telerik:RadTab Text="特集マスター" NavigateUrl="TokushuMenu.aspx">
                    </telerik:RadTab>
                    <telerik:RadTab Text="特集商品検索" NavigateUrl="ShouhinSearch.aspx">
                    </telerik:RadTab>
                    <telerik:RadTab Text="特集商品編集" NavigateUrl="ShouhinCheck.aspx">
                    </telerik:RadTab>
                    <telerik:RadTab Text="お知らせ" NavigateUrl="Oshirase.aspx">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
        </header>
        <div>


            <div id="ShousaiMainDiv">
                <div id="KensakuHidediv">
                    <p id="btn_p">▼検索項目</p>
                </div>
                <div id="shousai_div">



                    <table id="shousai_table">
                        <tr>
                            <th>商品名</th>
                            <td>
                                <asp:TextBox runat="server" ID="ShouhinNameLabel"></asp:TextBox></td>
                            <th>メディア</th>
                            <td>
                                <asp:TextBox runat="server" ID="MediaLabel"></asp:TextBox></td>
                            <th>価格</th>
                            <td>
                                <asp:DropDownList runat="server" ID="KakakuDropDown">
                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                    <asp:ListItem Text="10,000円以下" Value="0,10000"></asp:ListItem>
                                    <asp:ListItem Text="10,000円~15,000円" Value="10000,15000"></asp:ListItem>
                                    <asp:ListItem Text="15,000円~20,000円" Value="15000,20000"></asp:ListItem>
                                    <asp:ListItem Text="20,000円~25,000円" Value="20000,25000"></asp:ListItem>
                                    <asp:ListItem Text="25,000円~35,000円" Value="25000,35000"></asp:ListItem>
                                    <asp:ListItem Text="30,000円以上" Value="30000,100000"></asp:ListItem>
                                </asp:DropDownList>
                            </td>

                        </tr>

                        <tr>

                            <th>監督</th>
                            <td>
                                <asp:TextBox runat="server" ID="DirectiorLabel"></asp:TextBox></td>

                            <th>出演者</th>
                            <td>
                                <asp:TextBox runat="server" ID="ActorLabel1"></asp:TextBox></td>


                            <th>メーカー</th>
                            <td>
                                <asp:TextBox runat="server" ID="CompanyLabel"></asp:TextBox></td>


                        </tr>

                        <tr>
                            <th>上映時間</th>
                            <td>
                                <asp:DropDownList runat="server" ID="JoueiTimeDrop">
                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                    <asp:ListItem Text="60分以下" Value="0,60"></asp:ListItem>
                                    <asp:ListItem Text="60分~100分" Value="60,100"></asp:ListItem>
                                    <asp:ListItem Text="100分~120分" Value="100,120"></asp:ListItem>
                                    <asp:ListItem Text="120分~130分" Value="120,130"></asp:ListItem>
                                    <asp:ListItem Text="130分以上" Value="130,400"></asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <th>仕様</th>
                            <td>
                                <asp:TextBox runat="server" ID="ShiyouLabel"></asp:TextBox></td>

                            <th>商品コード</th>
                            <td>
                                <asp:TextBox runat="server" ID="ShouhinCodeLabel"></asp:TextBox></td>



                        </tr>

                    </table>

                    <div id="shousai_btn_div">
                        <asp:Button runat="server" ID="shousai_btn" Text="検索" OnClick="shousai_btn_Click" />
                        <asp:Button runat="server" ID="ClearButton" Text="クリア" OnClick="ClearButton_Click" />



                    </div>

                </div>

            </div>

            <div id="MainDiv">

                <div id="main">

                    <nav>
                        <asp:Button runat="server" ID="Sentei" Text="チェックしたタイトルを選定" OnClick="Sentei_Click" />
                        <asp:DropDownList runat="server" ID="SearchCategoryDrop" DataSourceID="SqlDataSource2" DataTextField="Categoryname" DataValueField="CategoryCode" OnSelectedIndexChanged="SearchCategoryDrop_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                        <%--   <asp:Button runat="server" ID="ALL_select_btn" Text="全選択" OnClick="ALL_select_btn_Click" />
                        <asp:Button runat="server" ID="ALL_uncheck" Text="全てのチェックを外す" OnClick="ALL_uncheck_Click" />--%>


                        <asp:SqlDataSource runat="server" ID="SqlDataSource2" ConnectionString='<%$ ConnectionStrings:GyomuConnectionString %>' SelectCommand="SELECT DISTINCT [CategoryCode], [Categoryname] FROM [M_Kakaku_2] WHERE CategoryCode = '203' or CategoryCode = '205' or CategoryCode = '209'"></asp:SqlDataSource>
                    </nav>

                    <div id="MainGridDiv" style="overflow: auto; height: 90%">
                        <asp:GridView runat="server" ID="MainGrid" AutoGenerateColumns="False" GridLines="None" OnRowDataBound="MainGrid_RowDataBound">

                            <Columns>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox runat="server" ID="HederCheck" AutoPostBack="true" OnCheckedChanged="HederCheck_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chec1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="商品コード">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="ShouhinCodeLabel" Text='<%# Eval("SyouhinCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="商品名">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="btn" CssClass="ShouhinButton"></asp:Label>
                                        <asp:HiddenField runat="server" ID="HidData" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="メディア">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="MediaLabel" Text='<%# Eval("Media") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                            </Columns>

                        </asp:GridView>
                    </div>
                </div>
                <div id="SenteiFinish">
                    <p>登録済み商品</p>
                    <div id="SenteiCategoryDiv">
                        <table>
                            <tr>
                                <th>特集名</th>
                                <td>
                                    <asp:DropDownList runat="server" ID="TokushuNameDrop" DataSourceID="SqlDataSource1" DataTextField="tokusyu_name" DataValueField="tokusyu_code" OnSelectedIndexChanged="TokushuNameDrop_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>


                                    <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:GyomuConnectionString %>' SelectCommand="SELECT [tokusyu_code], [tokusyu_name] FROM [M_tokusyu] WHERE tokusyu_code != '1' "></asp:SqlDataSource>
                                </td>
                            </tr>
                            <tr>
                                <th>カテゴリ</th>
                                <td>
                                    <asp:DropDownList runat="server" ID="TokushuCategoryDrop" DataSourceID="SqlDataSource2" DataTextField="Categoryname" DataValueField="CategoryCode" OnSelectedIndexChanged="TokushuCategoryDrop_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                            </tr>

                        </table>

                        <asp:Button runat="server" ID="ShouhinEditButton" Text="編集" OnClick="ShouhinEditButton_Click" />
                    </div>


                    <div id="SenteiFinishList" style="overflow: auto; height: 65%">

                        <asp:GridView runat="server" ID="SelectedGrid" AutoGenerateColumns="false" OnRowCommand="SelectedGrid_RowCommand" GridLines="None">

                            <EmptyDataTemplate>
                                <div>
                                    <p style="color: red;">この特集、カテゴリには商品が登録されていません。</p>
                                </div>

                            </EmptyDataTemplate>
                            <Columns>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="Button1" ImageUrl="~/Tokuisaki/image/DeleteImage.png" runat="server" ToolTip="削除" CommandName="sakujo" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('削除を行います。よろしいですか？')" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="SyouhinMei" HeaderText="商品名"></asp:BoundField>

                                <asp:TemplateField HeaderText="メディア">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="MediaLabel" Text='<%# Eval("Media") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hidden" Value='<%# Eval("SyouhinCode") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>

                </div>
            </div>


            <div>

                <div class="overlay" id="overlay">

                    <!-- モーダルウィンドウ -->
                    <div class="modal" id="modal">
                        <div class="close" id="close">×</div>

                        <div id="TableDiv">
                            <table id="ModalMainTable">
                                <tr>
                                    <th>商品名</th>
                                    <td>
                                        <asp:Label runat="server" ID="ShouhinLabel"></asp:Label></td>
                                </tr>
                                <tr>
                                    <th>メーカー</th>
                                    <td>
                                        <asp:Label runat="server" ID="MakerLabel"></asp:Label></td>
                                </tr>
                                <tr>
                                    <th>価格</th>
                                    <td>
                                        <asp:Label runat="server" ID="KakakuLabel"></asp:Label></td>
                                </tr>
                                <tr>
                                    <th>メーカー価格</th>
                                    <td>
                                        <asp:Label runat="server" ID="MakerKakakuLabel"></asp:Label></td>
                                </tr>
                                <tr>
                                    <th>仕様</th>
                                    <td>
                                        <asp:Label runat="server" ID="AttribuLabel"></asp:Label></td>
                                </tr>
                                <tr>
                                    <th>上映時間</th>
                                    <td>
                                        <asp:Label runat="server" ID="TimeLabel"></asp:Label></td>
                                </tr>
                                <tr>
                                    <th>監督</th>
                                    <td>
                                        <asp:Label runat="server" ID="ManagerLabel"></asp:Label></td>
                                </tr>
                                <tr>
                                    <th>出演</th>
                                    <td>
                                        <asp:Label runat="server" ID="ActorLabel"></asp:Label></td>
                                </tr>
                            </table>
                            <div id="CopyrightTable">
                                <asp:Label runat="server" ID="CopyrightLabel"></asp:Label>
                            </div>


                        </div>


                    </div>
                </div>

            </div>








            <footer>
                <p>@ 2022 movies management company Co., Ltd.</p>
            </footer>


            <%--全体のdiv--%>
        </div>

    </form>
    <script>
        'use strict';




        function keydown(e) {
            if (e.keyCode === 13) {
                document.getElementById('shousai_btn').focus();
            }
        }
        window.onkeydown = keydown;



        /*モーダルウィンドウにデータを表示するjavascript。*/
        function Create(data) {



            let modal = document.getElementById('modal');
            let overlay = document.getElementById('overlay');

            modal.classList.add('active');
            overlay.classList.add('active');


            var ShouhinLabel = document.getElementById("ShouhinLabel");
            var MakerLabel = document.getElementById("MakerLabel");
            var KakakuLabel = document.getElementById("KakakuLabel");
            var MakerKakakuLabel = document.getElementById("MakerKakakuLabel");
            var AttribuLabel = document.getElementById("AttribuLabel");
            var TimeLabel = document.getElementById("TimeLabel");
            var ManagerLabel = document.getElementById("ManagerLabel");
            var ActorLabel = document.getElementById("ActorLabel");
            var CopyrightLabel = document.getElementById("CopyrightLabel");


            /**/
            var AryData = data.split('jj');

            ShouhinLabel.textContent = AryData[0];
            MakerLabel.textContent = AryData[3];

            var kakaku1 = Number(AryData[4]).toLocaleString();
            KakakuLabel.textContent = kakaku1 + '円';
            /* KakakuLabel.textContent = AryData[4];*/
            var kakaku2 = Number(AryData[5]).toLocaleString();
            MakerKakakuLabel.textContent = kakaku2 + '円';

            /*  MakerKakakuLabel.textContent = AryData[5];*/

            AttribuLabel.textContent = AryData[6];

            if (AryData[7] == '') {
                TimeLabel.textContent = AryData[7];
            } else {
                TimeLabel.textContent = AryData[7] + '分';
            }




            ManagerLabel.textContent = AryData[8];
            ActorLabel.textContent = AryData[9];
            CopyrightLabel.textContent = AryData[10];



        }


        /*document.addEventListener('DOMContentLoaded', function () {*/
        window.onload = function () {

            let modal = document.getElementById('modal');
            let closeBtn = document.getElementById('close');
            let overlay = document.getElementById('overlay');

            // モダルの閉じるボタンをクリックしたら、モダルとオーバーレイのactiveクラスを外す
            closeBtn.addEventListener('click', function () {
                modal.classList.remove('active');
                overlay.classList.remove('active');
            });

            /*  オーバーレイをクリックしたら、モダルとオーバーレイのactiveクラスを外す*/
            overlay.addEventListener('click', function () {
                modal.classList.remove('active');
                overlay.classList.remove('active');
            });

        };



    </script>
</body>
</html>
