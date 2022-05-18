<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MsterTokuisaki.aspx.cs" Inherits="Gyomu.Master.MsterTokuisaki" %>

<%@ Register Src="~/CtlMenu.ascx" TagName="Menu" TagPrefix="uc" %>
<%@ Register Src="~/Master/CtlTokuisaki.ascx" TagName="M_Tokuisaki" TagPrefix="uc2" %>
<%@ Register Src="~/Common/CtrlFilter2.ascx" TagName="Filter2" TagPrefix="uc4" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="https://code.jquery.com/jquery-3.1.0.min.js"></script>

    <title>得意先マスタ</title>
    <telerik:RadScriptBlock ID="RSM" runat="server">
        <%--        <script type="text/javascript" src="../../Core.js"></script>--%>
        <%--        <script src="../../Common/CommonJs.js" type="text/javascript"></script>--%>
        <script type="text/jscript">

            function CntRow(cnt) {
                document.forms[0].count.value = cnt;
                return;
            }

        </script>
    </telerik:RadScriptBlock>
    <style type="text/css">
        .Hedar {
            background-color: #00008B;
            color: #FFFFFF;
            padding: 0.5em;
            font-size: 12px;
        }
    </style>

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
                <telerik:RadTab Text="得意先マスタ" Font-Size="12pt" NavigateUrl="MsterTokuisaki.aspx" Selected="True">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <div id="Masters" runat="server">
            <br />
            
            <uc4:Filter2 ID="Filter" runat="server" />
            <br />
            <asp:Button runat="server" ID="BtnSerch" OnClick="BtnSerch_Click1" Text="検索" />
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="新規登録" OnClick="Button1_Click" Width="100" />
            <asp:Button runat="server" ID="BtnCSVdownload" OnClick="BtnCSVdownload_Click" Text="CSVダウンロード" />
            <asp:Button runat="server" ID="BtnUpload" OnClick="BtnUpload_Click" Text="アップロード" />
            <asp:FileUpload runat="server" ID="FileUpload" />
            <br />
        </div>
        <div id="Touroku" runat="server">
            <br />
            <asp:Button ID="BtnToroku" runat="server" Text="登録" OnClick="BtnToroku_Click" Width="75" />
            <asp:Button ID="BtnBack" runat="server" Text="戻る" OnClick="BtnBack_Click" Width="75" />
            <br />
            <uc2:M_Tokuisaki ID="Tokuisaki" runat="server" />
        </div>


        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        <br />
        <div id="L" runat="server">
            <telerik:RadGrid runat="server" ID="RGTokuisakiList" OnItemDataBound="RGTokuisakiList_ItemDataBound" OnItemCommand="RGTokuisakiList_ItemCommand" AutoGenerateColumns="false" PageSize="30" AllowPaging="true" OnItemCreated="RGTokuisakiList_ItemCreated" OnPageIndexChanged="RGTokuisakiList_PageIndexChanged" OnPreRender="RGTokuisakiList_PreRender" AllowCustomPaging="false">
                <PagerStyle Position="Top" PageSizeControlType="None" />
                <MasterTableView>
                    <Columns>

                        <telerik:GridTemplateColumn>
                            <ItemTemplate>
                                <asp:Button runat="server" ID="BtnDelete" CommandName="Delete" Text="削除" OnClientClick="A();" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn>
                            <HeaderTemplate>
                                <p>得意先コード</p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="LblTokuisakiCode"></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn>
                            <HeaderTemplate>
                                <p>得意先名1</p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="LblTokuisakiName1"></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn>
                            <HeaderTemplate>
                                <p>得意先名２</p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="LblTokuisakiName2"></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn>
                            <HeaderTemplate>
                                <p>略称</p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="LblTokuisakiRyakusyou"></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>



                        <telerik:GridTemplateColumn>
                            <HeaderTemplate>
                                <p>担当者名</p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="LblTanto"></asp:Label>
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
                                <asp:Label runat="server" ID="LblTEL"></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn>
                            <HeaderTemplate>
                                <p>FAX</p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="LblFAX"></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn>
                            <HeaderTemplate>
                                <p>締日</p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="LblShimebi"></asp:Label>
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
        <input type="hidden" id="count" runat="server" />

        <div>
            <telerik:RadAjaxManager ID="Ram" runat="server" OnAjaxRequest="Ram_AjaxRequest">
                <%--<ClientEvents OnResponseEnd="OnResponseEnd" OnRequestStart="OnRequestStart" /> --%>
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="BtnL">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="TblK" />
                            <telerik:AjaxUpdatedControl ControlID="TblList" LoadingPanelID="LP" UpdatePanelHeight="200px" UpdatePanelRenderMode="Inline" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>

        </div>
    </form>
</body>
</html>
