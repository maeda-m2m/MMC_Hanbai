<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrlFilter2.ascx.cs" Inherits="Gyomu.Common.CtrlFilter2" %>

<style type="text/css">
    body {
        font-family: 'Open Sans', sans-serif;
    }

    .custom-select-wrapper {
        position: relative;
        display: inline-block;
        user-select: none;
    }

        .custom-select-wrapper select {
            display: none;
        }

    .custom-select {
        position: relative;
        display: inline-block;
    }

    .custom-select-trigger {
        position: relative;
        display: block;
        width: 130px;
        padding: 0 84px 0 22px;
        font-size: 22px;
        font-weight: 300;
        color: #fff;
        line-height: 60px;
        background: #5c9cd8;
        border-radius: 4px;
        cursor: pointer;
    }

        .custom-select-trigger:after {
            position: absolute;
            display: block;
            content: '';
            width: 10px;
            height: 10px;
            top: 50%;
            right: 25px;
            margin-top: -3px;
            border-bottom: 1px solid #fff;
            border-right: 1px solid #fff;
            transform: rotate(45deg) translateY(-50%);
            transition: all .4s ease-in-out;
            transform-origin: 50% 0;
        }

    .custom-select.opened .custom-select-trigger:after {
        margin-top: 3px;
        transform: rotate(-135deg) translateY(-50%);
    }

    .custom-options {
        position: absolute;
        display: block;
        top: 100%;
        left: 0;
        right: 0;
        min-width: 100%;
        margin: 15px 0;
        border: 1px solid #b5b5b5;
        border-radius: 4px;
        box-sizing: border-box;
        box-shadow: 0 2px 1px rgba(0,0,0,.07);
        background: #fff;
        transition: all .4s ease-in-out;
        opacity: 0;
        visibility: hidden;
        pointer-events: none;
        transform: translateY(-15px);
    }

    .custom-select.opened .custom-options {
        opacity: 1;
        visibility: visible;
        pointer-events: all;
        transform: translateY(0);
    }

    .custom-options:before {
        position: absolute;
        display: block;
        content: '';
        bottom: 100%;
        right: 25px;
        width: 7px;
        height: 7px;
        margin-bottom: -4px;
        border-top: 1px solid #b5b5b5;
        border-left: 1px solid #b5b5b5;
        background: #fff;
        transform: rotate(45deg);
        transition: all .4s ease-in-out;
    }

    .option-hover:before {
        background: #f9f9f9;
    }

    .custom-option {
        position: relative;
        display: block;
        padding: 0 22px;
        border-bottom: 1px solid #b5b5b5;
        font-size: 18px;
        font-weight: 600;
        color: #b5b5b5;
        line-height: 47px;
        cursor: pointer;
        transition: all .4s ease-in-out;
    }

        .custom-option:first-of-type {
            border-radius: 4px 4px 0 0;
        }

        .custom-option:last-of-type {
            border-bottom: 0;
            border-radius: 0 0 4px 4px;
        }

        .custom-option:hover,
        .custom-option.selection {
            background: #f9f9f9;
        }

    p {
        margin: 0;
        padding: 0;
    }
</style>
<asp:DataGrid runat="server" ID="FilterGrid" BorderStyle="None" AutoGenerateColumns="false" OnItemDataBound="Filter_ItemDataBound" OnItemCommand="Filter_ItemCommand">
    <HeaderStyle BackColor="#eeeeee" BorderColor="#eeeeee" />
    <ItemStyle BorderColor="#f2f2f2" />
    <AlternatingItemStyle BackColor="#f2f2f2" />

    <Columns>
        <asp:TemplateColumn>
            <HeaderTemplate>
                <p>項目</p>
            </HeaderTemplate>
            <HeaderStyle CssClass="HeaderStyle" />
            <ItemStyle CssClass="ItemStyle" />
            <ItemTemplate>
                <asp:DropDownList runat="server" ID="DdlKoumoku"></asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateColumn>

        <asp:TemplateColumn>
            <HeaderTemplate>
                <p>値</p>
            </HeaderTemplate>
            <HeaderStyle CssClass="HeaderStyle" HorizontalAlign="Center" />
            <ItemStyle CssClass="ItemStyle" />
            <ItemTemplate>
                <asp:TextBox runat="server" ID="TbxAtai" TextMode="MultiLine"></asp:TextBox>
                <asp:DropDownList runat="server" ID="DdlFilterItem">
                    <asp:ListItem Text="と等しい" Value="EqualTo"></asp:ListItem>
                    <asp:ListItem Text="と等しくない" Value="NotEqualTo"></asp:ListItem>
                    <asp:ListItem Text="を含む" Value="Contains"></asp:ListItem>
                    <asp:ListItem Text="を含まない" Value="DoesNotContain"></asp:ListItem>
                    <asp:ListItem Text="で始まる" Value="StartWith"></asp:ListItem>
                    <asp:ListItem Text="で終わる" Value="EndWith"></asp:ListItem>
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateColumn>

        <asp:TemplateColumn>
            <HeaderStyle CssClass="HeaderStyle" />
            <ItemStyle CssClass="ItemStyle" />
            <ItemTemplate>
                <asp:Button runat="server" ID="BtnDelFil" Text="×" CommandName="Delete" />
            </ItemTemplate>
        </asp:TemplateColumn>

    </Columns>
</asp:DataGrid>
<asp:Button runat="server" ID="BtnAdd" Text="＋" OnClick="BtnAdd_Click" />
<script type="text/javascript">
    function Add() {
        debugger;
        var view = document.getElementById('F2_Filter');
        var r = view.rows.length;
        var ctl = "ctl" + 0 + (r + 1);
        view.append('<tr><td class="ItemStyle"><select name="F2$Filter$' + ctl + '$DdlKoumoku" id="F2_Filter_' + ctl + '_DdlKoumoku" class="custom - select sources"><option value="--選択して下さい--">--選択して下さい--</option><option value="SyouhinCode">SyouhinCode</option><option value="SyouhinMei">SyouhinMei</option><option value="公共図書館">公共図書館</option><option value="学校図書館">学校図書館</option><option value="防衛省">防衛省</option><option value="その他図書館">その他図書館</option><option value="ホテル">ホテル</option><option value="レジャーホテル">レジャーホテル</option><option value="バス">バス</option><option value="船舶">船舶</option><option value="上映会">上映会</option><option value="カフェ">カフェ</option><option value="健康ランド">健康ランド</option><option value="福祉施設">福祉施設</option><option value="キッズ・BGV">キッズ・BGV</option><option value="その他">その他</option><option value="VOD">VOD</option><option value="VOD配信">VOD配信</option><option value="DOD">DOD</option><option value="DEX">DEX</option></select></td><td class="ItemStyle"><textarea name="F2$Filter$' + ctl + '$TbxAtai" rows="2" cols="20" id="F2_Filter_' + ctl + '_TbxAtai"></textarea><select name="F2$Filter$' + ctl + '$DdlFilterItem" id="F2_Filter_' + ctl + '_DdlFilterItem" class="custom - select sources"><option value="EqualTo">と等しい</option><option value="NotEqualTo">と等しくない</option><option value="Contains">を含む</option><option value="DoesNotContain">を含まない</option><option value="StartWith">で始まる</option><option value="EndWith">で終わる</option></select></td><td class="ItemStyle"><input type="submit" name="F2$Filter$' + ctl + '$BtnDelFil" value="×" onclick="Del(1); return false; " id="F2_Filter_' + ctl + '_BtnDelFil"></td></tr>');
    }
</script>

<script type="text/javascript">
    $(".custom-select").each(function () {
        var classes = $(this).attr("class"),
            id = $(this).attr("id"),
            name = $(this).attr("name");
        var template = '<div class="' + classes + '">';
        template += '<span class="custom-select-trigger">' + $(this).attr("placeholder") + '</span>';
        template += '<div class="custom-options">';
        $(this).find("option").each(function () {
            template += '<span class="custom-option ' + $(this).attr("class") + '" data-value="' + $(this).attr("value") + '">' + $(this).html() + '</span>';
        });
        template += '</div></div>';

        $(this).wrap('<div class="custom-select-wrapper"></div>');
        $(this).hide();
        $(this).after(template);
    });
    $(".custom-option:first-of-type").hover(function () {
        $(this).parents(".custom-options").addClass("option-hover");
    }, function () {
        $(this).parents(".custom-options").removeClass("option-hover");
    });
    $(".custom-select-trigger").on("click", function () {
        $('html').one('click', function () {
            $(".custom-select").removeClass("opened");
        });
        $(this).parents(".custom-select").toggleClass("opened");
        event.stopPropagation();
    });
    $(".custom-option").on("click", function () {
        $(this).parents(".custom-select-wrapper").find("select").val($(this).data("value"));
        $(this).parents(".custom-options").find(".custom-option").removeClass("selection");
        $(this).addClass("selection");
        $(this).parents(".custom-select").removeClass("opened");
        $(this).parents(".custom-select").find(".custom-select-trigger").text($(this).text());
    });
</script>
