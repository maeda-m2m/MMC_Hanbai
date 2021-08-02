
document.getElementById("RadComboCategory").focus();
debugger;


window.onload = function () {
    var rcb5 = document.getElementById("RadComboBox5");
    rcb5.style.display = "display";
    var check = document.getElementById("CheckBox1");
    let select = document.getElementById('RadComboCategory');
    var foc = document.getElementById('RadComboBox1');
    debugger;
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
        debugger;
    }
    if (check.checked === false) {
        var org = document.getElementsByClassName("MiniChange");
        var org2 = document.getElementsByClassName("MiniChange2");
        //org.style.backgroundColor = 'e6e6e6';
        //org2.style.backgroundColor = 'fff1a0';

        //$("#RadComboBox1").css('display', 'block');
        //$("#RadComboBox3").css('display', 'block');
        //$("#KariTokui").css('display', '');
        //$("#kariSekyu").css('display', '');
        //$("#TbxKake").css('display', '');
        //$("#KariFaci").css('display', '');
        //$("#LblAlert").text("");
    }


    //$('#RadComboBox1').change(function () {
    //    var tokui = $('#RadComboBox1').val();
    //    debugger;
    //    $("#RadComboBox3").text(tokui);
    //});


    var catecode = document.getElementById('RadComboCategory').value;
    debugger;
    var sd2 = "";
    for (var i = 0; i < document.getElementById('CtrlSyousai').rows.length; i++) {
        var ct = 'ctl0' + (i + 1);
        sd2 = 'CtrlSyousai_' + ct + '_StartDate';
        debugger;
    }
    var cc = document.getElementsByClassName('CategoryChabge');
    let sdd = document.getElementById(sd2);
    let sd = document.getElementById("StartDate");
    let ed = document.getElementById('EndDate');
    var zs = document.getElementsByClassName('Zasu');
    var rh = document.getElementById('RcbHanni');
    debugger;
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
            debugger;
            break;
        case "上映会":
            for (var c = 0; c < cc.length; c++) {
                cc[c].style.display = '';
            }
            for (var d = 0; d < zs.length; d++) {
                zs[d].style.display = '';
            }
            debugger;
            break;
        default:
            for (var e = 0; e < cc.length; e++) {
                cc[c].style.display = 'none';
            }
            for (var f = 0; f < zs.length; f++) {
                zs[d].style.display = '';
            }
            debugger;
    }

    $(function () {
        $("#BtnProductMeisai").click(function () {
            debugger;
            $("#SyouhinSyousai").css('display', '');
        });
    });

    $(function () {
        $("#BtnFacilityMeisai").click(function () {
            debugger;
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
                debugger;
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
                debugger;
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
                    debugger;
                    break;
                case "上映会":
                    $(".CategoryChabge").css('display', '');
                    $(".Zasu").css('display', '');
                    debugger;
                    break;
                default:
                    $(".CategoryChabge").css('display', '');
                    $(".Zasu").css('display', 'none');
                    debugger;
                    break;
            }
            debugger;
        });
        document.getElementById('RadComboBox1').focus();
        debugger;
    });

    $(function () {
        $("#RadComboBox1").change(function () {
            debugger;
            document.getElementById('FacilityRad').focus();
        });
    });
    $(function () {
        $("#Button1").click(function () {
            debugger;
            $("#RadComboBox5").css('display', 'block');
        });
    });



};


