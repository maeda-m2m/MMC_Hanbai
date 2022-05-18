<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrlFilter.ascx.cs" Inherits="Gyomu.Common.CtrlFilter" %>
<%@ Register Assembly="Core" Namespace="Core.Web" TagPrefix="cc1" %>
<%@ Register Src="CtlNengappiForm.ascx" TagName="CtlNengappiFromTo" TagPrefix="uc1" %>
<style type="text/css">
.hd
{
	background-color: #00008B;
     color:#FFFFFF;
}
.alt
{
	background-color: linen;
}
</style>
<script type="text/javascript">
    function DateInput(i) {
        var str = document.getElementById(i);
        if (str == null)
            return false;
        var val = str.control._dateInput._textBoxElement.value;
        var ary = val.split('/');
        var DD = new Date();
        var Year = DD.getYear();
        var Month = DD.getMonth() + 1;
        var Day = DD.getDate();

        if (val == "")
            return false;

        if (Month < 10) {
            Month = '0' + Month;
        }
        if (Day < 10) {
            Day = '0' + Day;
        }

        // 一桁の数値に0を追加する。
        for (var i = 0; i < ary.length; i++) {
            if (ary[i].match(/^\d{1}$/)) {
                ary[i] = '0' + ary[i];
            }
            if (i == 0) {
                val = ary[i];
            }
            else {
                val += '/' + ary[i];
            }
        }

        if (ary.length == 3) {
            if (ary[0].match(/^\d{2}$/)) {
                val = '20' + val;
            }
        }
        else if (ary.length == 2) {
            var today = Month + Day;
            var valday = ary[0] + ary[1];
            if (today > valday) Year = Year + 1;
            if (ary[0].match(/^\d{4}$/)) {
                val = val + '/' + Day;
            }
            else if (ary[0] > 12) {
                if (ary[1] > 12) {
                    val = '20' + ary[0] + '/' + Month + '/' + ary[1];
                } else {
                    val = '20' + val + '/' + Day;
                }
            }
            else {
                val = Year + '/' + val;
            }
        }
        else if (ary.length == 1) {
            if (ary[0].match(/^\d{4}$/)) {
                val = val + '/' + Month + '/' + Day;
            }
            else {
                val = Year + '/' + Month + '/' + val;
            }
        }
        if (!val.match(/^\d{4}\/\d{2}\/\d{2}$/)) {
            str.control._dateInput._textBoxElement.value = 'error';
            return false;
        }
        str.control._dateInput._textBoxElement.value = val;
    }
    function DateInputE(i) {
        if (event.keyCode == 13) {
            DateInput(i);
        }
    }

    //キー押下時 
    window.document.onkeydown = onKeyDown;

    //BackSpaceキー押下防止 
    function onKeyDown(e) {
        if (event.keyCode == 13) {
            return false;
        }
        if (navigator.appName == "Microsoft Internet Explorer") {
            //テキストボックス、パスワードボックスは許す 
            for (i = 0; i < document.all.tags("INPUT").length; i++) {
                if (document.all.tags("INPUT")(i).name == window.event.srcElement.name &&
                (document.all.tags("INPUT")(i).type == "text" || document.all.tags("INPUT")(i).type == "password")) {
                    return true;
                }
            }
            //テキストエリアは許す 
            for (i = 0; i < document.all.tags("TEXTAREA").length; i++) {
                if (document.all.tags("TEXTAREA")(i).name == window.event.srcElement.name) {
                    return true;
                }
            }
            if (event.keyCode == 8) {
                return false;
            }

        } else if (navigator.appName == "Netscape") {
            //テキストエリアは許す 
            for (i = 0; i < document.all.tags("TEXTAREA").length; i++) {
                if (document.all.tags("TEXTAREA")(i).name == window.event.srcElement.name) {
                    return true;
                }
            }
            if (e.which == 8) {
                return false;
            }
        }
    }
</script>
<asp:GridView ID="D" runat="server" AutoGenerateColumns="False" BorderColor="Black" BorderWidth="1px" CssClass="def" CellPadding="2">
    <Columns>
        <asp:TemplateField HeaderText="項目">
            <ItemTemplate>
                <asp:DropDownList ID="I" runat="server">
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="値">
            <ItemTemplate>
                <div id="V" runat="server">
                    <cc1:FilterTextBox ID="F" runat="server" TextMode="MultiLine">
                            <cc1:filteritem FilterType="EqualTo" Text="と等しい"></cc1:FilterItem>
<cc1:FilterItem FilterType="NotEqualTo" Text="と等しくない"></cc1:FilterItem>
<cc1:FilterItem FilterType="Contains" Text="を含む"></cc1:FilterItem>
<cc1:FilterItem FilterType="DoesNotContain" Text="を含まない"></cc1:FilterItem>
<cc1:FilterItem FilterType="StartsWith" Text="で始まる"></cc1:FilterItem>
<cc1:FilterItem FilterType="EndsWith" Text="で終わる"></cc1:FilterItem>
                    </cc1:FilterTextBox>
                    <asp:CheckBox ID="CK" runat="server" />
                    <uc1:CtlNengappiFromTo ID="N" runat="server" />
                </div>
            </ItemTemplate>
            <ItemStyle Wrap="False" />
        </asp:TemplateField>
    </Columns>
    <HeaderStyle CssClass="hd" />
    <AlternatingRowStyle CssClass="alt" />
</asp:GridView>
<asp:Button ID="B" runat="server" OnClick="B_Click" />