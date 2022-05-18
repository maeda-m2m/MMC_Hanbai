
document.getElementById("RadComboCategory").focus();


window.onload = function () {
    var rcb5 = document.getElementById("RadComboBox5");
    rcb5.style.display = "display";
    var check = document.getElementById("CheckBox1");
    let select = document.getElementById('RadComboCategory');
    var foc = document.getElementById('RadComboBox1');
    select.onchange = () => foc.focus();
    if (check.checked) {
        $(".MiniChange").css('background', '#ffb958');
        $(".MiniChange2").css('background', '#ffb958');
        $("#RadComboBox1").css('display', 'none');
        $("#RadComboBox3").css('display', 'none');
        $("#KariTokui").css('display', 'block');
        $("#kariSekyu").css('display', 'block');
        $("#TbxKake").css('display', 'block');
        $("#KariFaci").css('display', 'block');
        $("#LblAlert").text("得意先コードを得意先詳細にて入力してください");
    }
    if (check.checked === false) {
        var org = document.getElementsByClassName("MiniChange");
        var org2 = document.getElementsByClassName("MiniChange2");
    }

    var catecode = document.getElementById('RadComboCategory').value;
    var sd2 = "";
    for (var i = 0; i < document.getElementById('CtrlSyousai').rows.length; i++) {
        var ct = 'ctl0' + (i + 1);
        sd2 = 'CtrlSyousai_' + ct + '_StartDate';
    }
    var cc = document.getElementsByClassName('CategoryChabge');
    let sdd = document.getElementById(sd2);
    let sd = document.getElementById("StartDate");
    let ed = document.getElementById('EndDate');
    var zs = document.getElementsByClassName('Zasu');
    var rh = document.getElementById('RcbHanni');
    switch (catecode) {
        case "公共図書館":
        case "学校図書館":
        case "防衛省":
        case "その他図書館":
            for (var a = 0; a < cc.length; a++) {
                cc[a].style.display = 'none';
            }
            for (var b = 0; b < zs.length; b++) {
                zs[b].style.display = 'none';
            }
            break;
        case "上映会":
            for (var c = 0; c < cc.length; c++) {
                cc[c].style.display = '';
            }
            for (var d = 0; d < zs.length; d++) {
                zs[d].style.display = '';
            }
            break;
        default:
            for (var e = 0; e < cc.length; e++) {
                debugger;
                cc[c].style.display = 'none';
            }
            for (var f = 0; f < zs.length; f++) {
                zs[d].style.display = '';
            }
    }

    $(function () {
        $("#BtnProductMeisai").click(function () {
            $("#SyouhinSyousai").css('display', '');
        });
    });

    $(function () {
        $("#BtnFacilityMeisai").click(function () {
            $("#SisetuSyousai").css('display', '');
        });
    });





    $(function () {
        $("#CheckBox1").click(function () {
            var check = $("#CheckBox1").prop('checked');
            if (check === true) {
                $(".MiniChange").css('background', '#ffb958');
                $(".MiniChange2").css('background', '#ffb958');
                $("#LblAlert").text("得意先コードを得意先詳細にて入力してください。");
                $("#RadComboBox1").css('display', 'none');
                $("#RadComboBox3").css('display', 'none');
                $("#KariTokui").css('display', 'block');
                $("#kariSekyu").css('display', 'block');
                $("#TbxKake").css('display', 'block');
                $("#KariFaci").css('display', 'block');
                return;
            }
            if (check === false) {
                var org = $(".MiniChange");
                var org2 = $(".MiniChange2");
                org.css('background', '');
                org2.css('background', '#fff1a0');
                $("#RadComboBox1").css('display', 'block');
                $("#RadComboBox3").css('display', 'block');
                $("#KariTokui").css('display', 'none');
                $("#kariSekyu").css('display', 'none');
                $("#TbxKake").css('display', 'none');
                $("#KariFaci").css('display', 'none');
                $("#LblAlert").text("");
                return;
            }
        });
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
                    $(".Zasu").css('display', 'none');
                    break;
                case "上映会":
                    $(".CategoryChabge").css('display', '');
                    $(".Zasu").css('display', '');
                    break;
                default:
                    $(".CategoryChabge").css('display', '');
                    $(".Zasu").css('display', 'none');
                    break;
            }
        });
        document.getElementById('RadComboBox1').focus();
    });

    $(function () {
        $("#RadComboBox1").change(function () {
            document.getElementById('FacilityRad').focus();
        });
    });
    $(function () {
        $("#Button1").click(function () {
            $("#RadComboBox5").css('display', 'block');
        });
    });



};


