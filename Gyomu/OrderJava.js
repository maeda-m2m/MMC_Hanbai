jQuery(window).ready(function (e) {

    window.onload = function () {
        var check = $("#KariCheck").prop('checked');

        if (check === true) {
            debugger;
            $(".MiniChange2").css('background', '#ffb958');
            $("#RadComboBox1").css('display', 'none');
            $("#RadComboBox3").css('display', 'none');
            $("#KariTokui").css('display', 'block');
            $("#kariSekyu").css('display', 'block');
            $("#TbxKake").css('display', 'block');
            $("#KariFaci").css('display', 'block');
        }
        if (check === false) {
            debugger;
            var org = $(".MiniChange2");
            org.css('background', '#fff1a0');
            $("#RadComboBox1").css('display', 'block');
            $("#RadComboBox3").css('display', 'block');
            $("#KariTokui").css('display', '');
            $("#kariSekyu").css('display', '');
            $("#TbxKake").css('display', '');
            $("#KariFaci").css('display', '');
        }

        var catecode = $("#RadComboCategory").val();
        switch (catecode) {
            case "公共図書館":
            case "学校図書館":
            case "防衛省":
            case "その他図書館":
                $(".CategoryChabge").css('display', 'none');
                $("#Zasu").css('display', '');
                debugger;
                break;
            case "上映会":
                $(".CategoryChabge").css('display', '');
                $("#Zasu").css('display', 'block');
                debugger;
                break;
            default:
                $(".CategoryChabge").css('display', '');
                $("#Zasu").css('display', '');
                debugger;
        }
    };

    $(function () {
        $("KariCheck").click(function () {
            var check = $("#CheckBox1").prop('checked');
            if (check === true) {
                $(".MiniChange2").css('background', '#ffb958');
                $("#RadComboBox1").css('display', 'none');
                $("#RadComboBox3").css('display', 'none');
                $("#KariTokui").css('display', 'block');
                $("#kariSekyu").css('display', 'block');
                $("#TbxKake").css('display', 'block');
                $("#KariFaci").css('display', 'block');
                debugger;
                return;
            }
            if (check === false) {
                var org = $(".MiniChange2");
                org.css('background', '#fff1a0');
                $("#RadComboBox1").css('display', 'block');
                $("#RadComboBox3").css('display', 'block');
                $("#KariTokui").css('display', 'none');
                $("#kariSekyu").css('display', 'none');
                $("#TbxKake").css('display', 'none');
                $("#KariFaci").css('display', 'none');
                debugger;
                return;
            }
        });

        $(function () {
            $("#RadComboCategory").change(function () {

                var catecode = $("#RadComboCategory").val();
                switch (catecode) {
                    case "公共図書館":
                    case "学校図書館":
                    case "防衛省":
                    case "その他図書館":
                        $(".CategoryChabge").css('display', 'none');
                        $("#Zasu").css('display', '');
                        debugger;
                        break;
                    case "上映会":
                        $(".CategoryChabge").css('display', '');
                        $("#Zasu").css('display', 'block');
                        debugger;
                        break;
                    default:
                        $(".CategoryChabge").css('display', '');
                        $("#Zasu").css('display', '');
                        debugger;
                }
            });
        });
    });
});
