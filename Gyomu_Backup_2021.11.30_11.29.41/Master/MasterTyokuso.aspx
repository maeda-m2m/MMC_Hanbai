<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterTyokuso.aspx.cs" Inherits="Gyomu.Master.MasterTyokuso" %>

<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Src="~/Master/CtlTyokuso.ascx" TagName="M_Tyokuso" TagPrefix="uc2" %>
<%@ Register Src="~/Common/CtrlFilter2.ascx" TagName="Filter2" TagPrefix="uc4" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="https://code.jquery.com/jquery-3.1.0.min.js"></script>

    <title>直送先・施設マスタ</title>
    <style type="text/css">
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
    <form id="form1" runat="server">
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

                        $('#BtnUpload').click(function () {
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

                        $('#BtnCSVdownload').click(function () {
                            debugger;
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
                    });
                </script>
            </div>
        </div>


        <div id="MainMenu" runat="server">
            <uc:Menu ID="Menu" runat="server" />
        </div>
        <br />
        <telerik:RadTabStrip ID="RT" runat="server" Skin="Office2007" AutoPostBack="True" SelectedIndex="0">
            <Tabs>
                <telerik:RadTab Text="直送先・施設マスタ" Font-Size="12pt" NavigateUrl="MasterTyokuso.aspx" Selected="True">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <div id="Touroku" runat="server">
            <br />
            <asp:Button ID="BtnToroku" runat="server" Text="登録" OnClick="BtnToroku_Click" Width="75" />
            <asp:Button ID="BtnBack" runat="server" Text="戻る" OnClick="BtnBack_Click" Width="75" />
            <br />
            <uc2:M_Tyokuso ID="Tyokuso" runat="server"></uc2:M_Tyokuso>
        </div>

        <div id="Master" runat="server">
            <br />
            <uc4:Filter2 ID="Filter" runat="server" />
            <br />
            <asp:Button runat="server" ID="BtnSerch" OnClick="BtnSerch_Click" Text="検索" />
            <br />
            <br />
            <asp:Button ID="BtnSinki" runat="server" Text="新規登録" OnClick="BtnSinki_Click" Width="100" />
            <asp:Button runat="server" ID="BtnCSVdownload" OnClick="BtnCSVdownload_Click" Text="CSVダウンロード" />
            <asp:Button runat="server" ID="BtnUpload" OnClick="BtnUpload_Click" Text="アップロード" />
            <asp:FileUpload runat="server" ID="FileUpload" />
            <br />
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            <br />
            <div id="L" runat="server">
                <telerik:RadGrid runat="server" ID="RGTyokusou" Width="100%" OnItemDataBound="RGTyokusou_ItemDataBound" OnItemCommand="RGTyokusou_ItemCommand" AutoGenerateColumns="false" PageSize="30" AllowPaging="true" OnItemCreated="RGTyokusou_ItemCreated" OnPageIndexChanged="RGTyokusou_PageIndexChanged" AllowCustomPaging="false">
                    <PagerStyle Position="Top" PageSizeControlType="None" />
                    <MasterTableView>
                        <Columns>

                            <telerik:GridTemplateColumn>
                                <HeaderTemplate>
                                    <p>施設No</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblFacilityNo"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <HeaderTemplate>
                                    <p>コード</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblCode"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <HeaderTemplate>
                                    <p>施設名1</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblFacilityName1"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <HeaderTemplate>
                                    <p>施設名2</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblFacilityName2"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <HeaderTemplate>
                                    <p>略称</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblAbbreviation"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <HeaderTemplate>
                                    <p>施設担当者</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblFacilityResponsible"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                             
                            <telerik:GridTemplateColumn>
                                <HeaderTemplate>
                                    <p>郵便番号</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblPostNo"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <HeaderTemplate>
                                    <p>住所1</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblAddress1"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <HeaderTemplate>
                                    <p>住所2</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblAddress2"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
                                <HeaderTemplate>
                                    <p>電話番号</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblTell"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn>
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
