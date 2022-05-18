<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterSyohin.aspx.cs" Inherits="Gyomu.Master.MasterSyohin" %>

<%@ Register Src="~/Common/CtrlFilter2.ascx" TagName="Menu2" TagPrefix="uc4" %>
<%@ Register Src="~/CtlMenu.ascx" TagName="HMenu" TagPrefix="uc1" %>
<%@ Register Src="~/Master/CtlSyohin.ascx" TagName="M_Syohin" TagPrefix="uc2" %>
<%@ Register Src="~/Common/CtrlFilter.ascx" TagName="Filter" TagPrefix="uc3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    <title>商品登録</title>
    <style type="text/css">
        .sl {
            width: 35px;
        }

        #L {
            width: 100%;
        }
        /*↓スピナー↓*/
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
        /*↑スピナー↑*/
    </style>
    <telerik:RadScriptBlock ID="RSM" runat="server">

        <script type="text/jscript">

            function CntRow(cnt) {
                document.forms[0].count.value = cnt;
                return;
            }
        </script>
    </telerik:RadScriptBlock>
</head>
<body>
    <form runat="server" id="form1">
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

        <script type="text/javascript">
            function A(btn) {
                debugger;
                var a1 = window.confirm('本当に削除しますか？');
                if (a1 === true) {
                    return true;
                }
                else if (a1 === false) {
                    var b = document.getElementById(btn);
                    b.addEventListener('click', function (e) {
                        e.preventDefault();
                    }, false);
                    return false;
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

                        $('#Button1').click(function () {
                            $.ajax({
                                type: 'GET',
                                success: function (data) {
                                    console.log(data);
                                }

                            }).done(function () {
                                setTimeout(function () {

                                    $("#overlay").fadeOut(300);
                                }, 25000000);
                            });
                        });

                        $('#Syohin_Button5').click(function () {
                            $.ajax({
                                type: 'GET',
                                success: function (data) {
                                    console.log(data);
                                }
                            }).done(function () {
                                setTimeout(function () {
                                    $("#overlay").fadeOut(300);
                                }, 100000);
                            });
                        });


                        $('#BtnCsvDownload').click(function () {
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
        <div id="MainMenu" runat="server">
            <uc1:HMenu runat="server" ID="Menu" />
        </div>
        <telerik:RadTabStrip ID="RT" runat="server" Skin="Office2007" AutoPostBack="True" SelectedIndex="1">
            <Tabs>
                <telerik:RadTab Text="商品マスタ" Font-Size="12pt" NavigateUrl="MasterSyohin.aspx" Selected="true">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <div id="Touroku" runat="server">
            <br />
            <asp:Button ID="BtnBack" runat="server" Text="戻る" Width="75" OnClick="BtnBack_Click" />
            <br />
            <br />
            <uc2:M_Syohin ID="Syohin" runat="server" />
        </div>
        <div id="Master" runat="server">
            <br />
            <uc4:Menu2 runat="server" ID="F2" />
            <p style="font-size: 12px;">※カテゴリーを検索する際は、「〇」か「×」で検索して下さい。</p>
            <asp:Button runat="server" ID="BtnSerch2" OnClick="BtnSerch2_Click" Text="検索" />
            <br />
            <br />
            <asp:Button ID="BtnSinki" runat="server" Text="新規登録" Width="100" OnClick="BtnSinki_Click" />
            <asp:Button runat="server" ID="BtnCsvDownload" Text="CSVダウンロード" OnClick="BtnCsvDownload_Click" />

            <asp:Button ID="Button1" runat="server" Text="アップロード" OnClick="Button1_Click1" />
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            <asp:HiddenField runat="server" ID="HidMaxCount" />
            <asp:HiddenField runat="server" ID="HidNowCount" />
            <br />
            <div id="L" runat="server">
            </div>
            <div>
                <telerik:RadGrid runat="server" Width="100%" ID="RGproductlist" OnItemDataBound="RGproductlist_ItemDataBound" OnItemCommand="RGproductlist_ItemCommand" AutoGenerateColumns="false" PageSize="30" AllowPaging="true" OnItemCreated="RGproductlist_ItemCreated" OnPageIndexChanged="RGproductlist_PageIndexChanged" OnPreRender="RGproductlist_PreRender" AllowCustomPaging="false">
                    <PagerStyle Position="Top" PageSizeControlType="None" />
                    <HeaderContextMenu>
                    </HeaderContextMenu>

                    <MasterTableView>
                        <Columns>
                            <telerik:GridTemplateColumn>
                                <HeaderTemplate>
                                    <p>商品コード</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblSyouhinCode"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <HeaderTemplate>
                                    <p>商品名</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblSyouhinMei"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <p>公共図書館</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblKoukyou"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <p>学校図書館</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblGakkou"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <p>防衛省</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblBouei"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <p>その他図書館</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblSonotaTosyokan"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <p>ホテル</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblHotel"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <p>レジャーホテル</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="Lblrejahotel"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <p>バス</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblBus"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <p>船舶</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblSenpaku"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <p>上映会</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblJouei"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <p>カフェ</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="Lblcafe"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <p>健康ランド</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblKenkou"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <p>福祉施設</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblFukushi"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <p>キッズ・BGV</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="Lblkiz"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <p>その他</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblSonota"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <p>VOD</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblVOD"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <p>VOD配信</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblVODhaishin"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <p>DOD</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblDOD"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <p>DEX</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblDEX"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="BtnSyousai" CommandName="BtnSyousai" Text="詳細" />
                                    <asp:HiddenField runat="server" ID="HidSyousai" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </div>
        <input type="hidden" id="count" runat="server" />
        <telerik:RadAjaxManager runat="server" OnAjaxRequest="Ram_AjaxRequest" ID="Ram"></telerik:RadAjaxManager>
    </form>
</body>
</html>
