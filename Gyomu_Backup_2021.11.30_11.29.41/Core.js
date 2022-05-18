var Core = new Object();

Core.GetCheckedValues = function(check_id_array, separator)
{
	var array = [];
	for (var i = 0; i < check_id_array.length; i++) {
		var chk = document.getElementById(check_id_array[i]);
		if (null == chk || chk.style.display=='none') 
			continue;		
		if (chk.checked) {
			array.push(chk.value);
		}
	}
	return array.join(separator);
}
Core.GetCheckedCount = function(check_id_array)
{
	var count = 0; 
	for (var i = 0; i < check_id_array.length; i++) {
		var chk = document.getElementById(check_id_array[i]);
		if (null == chk || chk.style.display=='none') continue;
		if (chk.checked) count++;
	}
	return count;
}

Core.GetDataArray = function(key, hidDataKey, hidData, separatorKey, separatorData, bErrorNotFoundKey)
{
	if (null == hidData) return [];
	if (hidData.value == "") return [];
	
	var arrayKey = hidDataKey.value.split(separatorKey);
	var arrayData = hidData.value.split(separatorData);
	
	var index = -1;
	for (var i = 0; i < arrayKey.length; i++) {
		if (key == arrayKey[i]) {
			index = i;
			break;
		}
	}
	if (-1 == index) {
		if (bErrorNotFoundKey)
			alert('JCore Error!! Not Found Key : ' + key);
		return [];
	}
	
	return arrayData[index].split(",");
}


Core.CheckMultiLine = function (name, tbx, rows)
{
	var t = tbx.value.split("\r\n");
	
	if (rows < t.length) {
		alert(name + "は" + rows + "行以上は入力できません。");
		var index = tbx.value.lastIndexOf("\r\n");
		tbx.value = tbx.value.substring(0, index) + tbx.value.substring(index + 2, tbx.value.length);
	}
};

Core.CheckAll = function(chk_id_array, bCheck)
{
	for (var i = 0; i < chk_id_array.length; i++) {
		var chk = document.getElementById(chk_id_array[i]);
		if (null != chk) chk.checked = bCheck;
    }
};

Core.Sum = function(_su, tbx_sum)
{
    var dSum = 0;
    for (var i = 0; i < _su.length; i++) {
        var t = document.getElementById(_su[i]);
        if (null == t) continue;
        if ("" == t.value) continue;
        if (isNaN(t.value)) continue;
        if (0 >= parseFloat(t.value)) {
            t.value = "";
            continue;
        }
        dSum += parseFloat(t.value);
    }
    document.getElementById(tbx_sum).value = (0 >= dSum) ? "" : dSum;  
}

Core.ResizeRadGrid = function(radgrid, arg) {
    Core._ResizeRadGrid(radgrid, 0, true, 0, 0, 20);
};

Core.ResizeRadGridEx = function(radgrid, padding_bottom, bSetWidth, nMaxHeight) {
Core._ResizeRadGrid(radgrid, padding_bottom, bSetWidth, nMaxHeight, 0, 20);
};

Core.ResizeRadGrid_Mitumori = function(radgrid, arg) {
    Core._ResizeRadGrid(radgrid, 0, true, 0, 0, 100);
}

Core._ResizeRadGrid = function(radgrid, padding_bottom, bSetWidth, nMaxHeight, call_count, scroll_offset) {

    if (null == radgrid) return;
    if (null == radgrid._gridDataDiv) return;

    try {
        var bIEOld = (navigator.userAgent.indexOf("MSIE 7") > -1 || navigator.userAgent.indexOf("MSIE 6") > -1);
        var masterTable = radgrid.get_masterTableView().get_element();
        var pager_and_header_footer_height = $telerik.getLocation(radgrid._gridDataDiv).y - $telerik.getLocation(radgrid.get_element()).y;
        if (null != radgrid.get_masterTableViewFooter()) {
            pager_and_header_footer_height = pager_and_header_footer_height + radgrid.get_masterTableViewFooter().get_element().offsetHeight;
        }

        var grid_height = masterTable.offsetHeight + pager_and_header_footer_height + 2;  // スクロールなしで表示した時の表の高さ(+α余白2pixel)
        if (radgrid._gridDataDiv.scrollWidth > radgrid._gridDataDiv.offsetWidth) {
            // グリッド内で横スクロールが出る場合はスクロールバー分高さを増す。
            grid_height = grid_height + 30;
        }
        
        
        var grid_height_visible = document.documentElement.offsetHeight - $telerik.getLocation(radgrid.get_element()).y - 16;     // ブラウザ内で表示可能な高さ(document.body.clientHeightはNG 値がよく変わる)
        if (document.documentElement.clientWidth < document.documentElement.scrollWidth)
            grid_height_visible = grid_height_visible - scroll_offset;   // 縦スクロールバーが表示される場合はその分(scroll_offset分)縮める

        if (bIEOld) grid_height_visible = grid_height_visible - scroll_offset;

        if (0 >= grid_height_visible) {
            grid_height_visible = grid_height;
        }

        if (null != padding_bottom) grid_height_visible = grid_height_visible - padding_bottom;

        if (grid_height_visible < grid_height) {
            grid_height = grid_height_visible; // 表示できる高さに調整
            var table_height_min = pager_and_header_footer_height + 150; // 最低でも表示したい高さ
            if (table_height_min > grid_height) grid_height = table_height_min;
        }

        // 高さの設定
        if (0 < nMaxHeight) {
            // 高さ固定(指定した高さ以内のフルの高さで表示する)
            var nAllHeight = masterTable.offsetHeight + pager_and_header_footer_height; // 縦スクロール無しで表示できた場合の高さ
            if (nAllHeight > nMaxHeight)
                grid_height = nMaxHeight;
            else
                grid_height = nAllHeight;
        }

        var strHeight = grid_height.toString() + "px";
        radgrid.get_element().style.height = strHeight;

        var list_width = masterTable.offsetWidth; // スクロールなしで表示した時の表の幅

        if (122 > list_width) list_width = 122;

        var bOverWidth = false;
        var grid_width_visible = document.documentElement.offsetWidth - $telerik.getLocation(radgrid._gridDataDiv).x - 32;
        if (document.documentElement.clientHeight < document.documentElement.scrollHeight)
            grid_width_visible = grid_width_visible - 22;   // 縦スクロールバーが表示される場合はその分縮める
        if (bIEOld) grid_width_visible = grid_width_visible - 22;

        if (list_width > grid_width_visible) {
            list_width = grid_width_visible;
            bOverWidth = true;
        }

        if (bSetWidth) {
            var strWidth = list_width.toString() + "px";
            var strWidth_22 = (list_width + 22).toString() + "px";
            var masterTableHeader = radgrid.get_masterTableViewHeader().get_element();
            var masterTableFooter = null;
            if (null != radgrid.get_masterTableViewFooter())
                masterTableFooter = radgrid.get_masterTableViewFooter().get_element();



            //var s = "radgrid._gridDataDiv.style.width=" + radgrid._gridDataDiv.style.width + ":" + strWidth_22 + "<br/>";
            //s = s + "masterTable.style.width=masterTableHeader.width=" + masterTable.style.width + ":" + strWidth + "<br/>";
            //s = s + "radgrid.get_element().style.width=" + radgrid.get_element().style.width + ":" + strWidth_22 + "<br/>";
            //s = s + "radgrid.get_element().style.width=" + radgrid.get_element().style.width + ":" + strWidth;

            //arg.innerHTML = s;

            if (strWidth != masterTable.style.width) masterTable.style.width = strWidth;
            if (strWidth != masterTableHeader.style.width) masterTableHeader.style.width = strWidth;
            if (null != masterTableFooter)
                if (strWidth != masterTableFooter.style.width) masterTableFooter.style.width = strWidth;

            if (bOverWidth) {
                if (strWidth_22 != radgrid.get_element().style.width)
                    radgrid.get_element().style.width = strWidth_22;
            }
            else {
                if (strWidth != radgrid.get_element().style.width)
                    radgrid.get_element().style.width = strWidth;
            }


            if (strWidth_22 != radgrid._gridDataDiv.style.width) {
                if (!bIEOld) {
                    // IEが6.0の場合はこれを設定してしまうとOnResizeが無限にコールされる問題が発生
                    radgrid._gridDataDiv.style.width = strWidth_22;

                }
            }

        }

        radgrid.repaint();

    }
    catch (e) {
        if (60 < call_count) {
            alert("error:" + e.description);

            radgrid.get_element().style.width = "100%";
            radgrid.get_element().style.height = "400px";
            return;
        }
        // IE6でongridcreatedからの本ルーチンのコールでエラー。onmastertableviewcreatedからの呼び出しはOKだが、
        // タイミング的にヘッダ、テーブルがnullの場合がある為、遅延処理を行う。
        setTimeout(function() { Core._ResizeRadGrid(radgrid, padding_bottom, nMaxHeight, call_count++); }, 50);


    }

};

