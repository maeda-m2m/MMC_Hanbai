<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UriageMeisaiRan.ascx.cs" Inherits="Gyomu.Uriage.UriageMeisaiRan" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<link href="UriageMeisai.css" type="text/css" rel="stylesheet" />
<script src="http://code.jquery.com/jquery-migrate-1.2.1.min.js"></script>
<script src="../JavaScript.js"></script>

<style type="text/css">
    .SujiText {
        text-align: right;
    }

    .Zasu {
        display: block;
    }

    .CategoryChabge {
        display: block;
    }

    #KariFaci {
        display: none;
    }
</style>


<asp:Label ID="err" runat="server" Text="" ForeColor="#ff0000"></asp:Label>
<asp:Label ID="end" runat="server" Text="" ForeColor="Green"></asp:Label>
<div runat="server">
    <table class="uHeadTB" runat="server">
        <tr>
            <td class="uMeisaiProcd" runat="server" colspan="1">
                <asp:Label ID="LblProduct" runat="server" Text=""></asp:Label>
                <input type="hidden" id="HidCategoryCode" runat="server" />
            </td>
            <td class="uMeisaiProNm" runat="server" colspan="5">
                <telerik:RadComboBox ID="SerchProduct" runat="server" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" OnItemsRequested="SerchProduct_ItemsRequested" OnSelectedIndexChanged="SerchProduct_SelectedIndexChanged" EmptyMessage="" AutoPostBack="True" Width="600px"></telerik:RadComboBox>
                <input type="hidden" runat="server" id="procd" />
                <script>
                    function SerchFocus(fcs) {
                        debugger;
                        var ary = fcs.split('-');
                        var rcb = document.getElementById(ary[0]);
                        var btn = document.getElementById(ary[1]);
                        btn.focus();
                    }
                </script>
            </td>
            <td class="uMeisaiMTD" runat="server">
                <%--                <telerik:RadComboBox ID="Hanni" runat="server" Width="100px"></telerik:RadComboBox>--%>
                <asp:Label ID="LblHanni" runat="server" Text=""></asp:Label>
            </td>
            <td class="uMeisaiMTD" runat="server">
                <asp:Label ID="Baitai" runat="server" Text=""></asp:Label>
            </td>
            <td class="uMeisaisTD" runat="server">
                <asp:TextBox ID="Suryo" runat="server" Width="30px" Text="1" TextMode="Number"></asp:TextBox>
            </td>
            <td class="uMeisaikTD" runat="server">
                <asp:TextBox ID="HyoujyunTanka" runat="server" Width="80px" Height="20px" CssClass="SujiText"></asp:TextBox>
                <input type="hidden" id="ht" runat="server" />
                <input type="hidden" id="zeiht" runat="server" />
            </td>
            <td class="uMeisaikTD" runat="server">
                <asp:TextBox ID="Kingaku" runat="server" Width="80px" Height="20px" CssClass="SujiText"></asp:TextBox>
                <input type="hidden" id="kgk" runat="server" />
                <input type="hidden" id="zeikgk" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="5" class="uMeisaiBTD" runat="server">
                <telerik:RadComboBox ID="ShiyouShisetsu" runat="server" Width="300px" OnItemsRequested="ShiyouShisetsu_ItemsRequested" AllowCustomText="True" EnableLoadOnDemand="True" ShowMoreResultsBox="True" ShowToggleImage="False" EnableVirtualScrolling="True" OnSelectedIndexChanged="ShiyouShisetsu_SelectedIndexChanged" AutoPostBack="True"></telerik:RadComboBox>
                <input type="hidden" runat="server" id="FacilityCode" />
                <%--                <asp:TextBox ID="KariFaci" runat="server" Width="200px"></asp:TextBox>--%>
                <asp:Label runat="server" ID="Facility"></asp:Label>
                <input type="hidden" runat="server" id="FacilityAddress" />
            </td>
            <td class="uMeisaiMTD" runat="server">
                <telerik:RadComboBox ID="Zasu" runat="server" Width="100px" AllowCustomText="True" CssClass="Zasu">
                    <Items>
                        <telerik:RadComboBoxItem Text="" Value="0" runat="server" />
                        <telerik:RadComboBoxItem Text="1" Value="1" runat="server" />
                        <telerik:RadComboBoxItem Text="51" Value="51" runat="server" />
                        <telerik:RadComboBoxItem Text="101" Value="101" runat="server" />
                        <telerik:RadComboBoxItem Text="201" Value="201" runat="server" />
                        <telerik:RadComboBoxItem Text="301" Value="301" runat="server" />
                        <telerik:RadComboBoxItem Text="401" Value="401" runat="server" />
                        <telerik:RadComboBoxItem Text="501" Value="501" runat="server" />
                        <telerik:RadComboBoxItem Text="601" Value="601" runat="server" />
                        <telerik:RadComboBoxItem Text="701" Value="701" runat="server" />
                        <telerik:RadComboBoxItem Text="801" Value="801" runat="server" />
                        <telerik:RadComboBoxItem Text="901" Value="901" runat="server" />
                        <telerik:RadComboBoxItem Text="1001" Value="1001" runat="server" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="uMeisaiMTD" runat="server">
                <telerik:RadDatePicker ID="StartDate" runat="server" Width="100px" CssClass="CategoryChabge"></telerik:RadDatePicker>
                <asp:Label ID="Label11" runat="server" Text=""></asp:Label>
            </td>
            <td class="uMeisaiMTD" runat="server">
                <telerik:RadDatePicker ID="EndDate" runat="server" Width="100px" CssClass="CategoryChabge"></telerik:RadDatePicker>
                <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
            </td>
            <td class="uMeisaiTD" runat="server">
                <asp:Label ID="Kakeri" runat="server" Text=""></asp:Label><br />
                <asp:Label ID="zeiku" runat="server" Text=""></asp:Label>
            </td>
            <td class="uMeisaikTD" runat="server">
                <asp:TextBox ID="Tanka" runat="server" Width="80px" Height="20px" CssClass="SujiText"></asp:TextBox>
                <script type="text/javascript">
                    function Keisan(tbx) {
                        var ary = tbx.split('-');
                        debugger;
                        var tanka = document.getElementById(ary[0]).value;
                        var suryo = document.getElementById(ary[1]).value;
                        var Hyoutan = document.getElementById(ary[3]).value;
                        var tan = tanka.replace(",", "");
                        var hyo = Hyoutan.replace(",", "");
                        var num = tan * suryo;
                        var num1 = hyo * suryo;

                        var num = String(num).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                        var num1 = String(num1).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");
                        var num2 = String(num2).replace(/(\d)(?=(\d\d\d)+$)/g, "$1,");

                        document.getElementById(ary[2]).value = num;
                        document.getElementById(ary[5]).value = num1;
                        document.getElementById(ary[6]).value = num2;

                    }
                </script>
                <input type="hidden" id="tk" runat="server" />
            </td>
            <td class="uMeisaikTD" runat="server">
                <asp:TextBox ID="Uriage" runat="server" Width="80px" Height="20px" CssClass="SujiText"></asp:TextBox>
                <input type="hidden" id="ug" runat="server" />
            </td>
        </tr>
    </table>
    <input type="hidden" id="category" runat="server" />
    <input type="hidden" id="zeikubun" runat="server" />
</div>
