//显示错误信息
function ShowMessage(msg) {
    $(".errorMsg").text(msg);
    $(".errorMsg").show();
}

//隐藏错误信息
function HideMessage() {
    $(".errorMsg").hide();
}

//监测必须输入或选择时内容是否为空
function CheckMustInputData() {
    $(".mustInputMessage").remove();
    var isTrue = true;
    $(".mustinput").each(function () {
        if ($(this).val() == "") {
            $(this).focus();
            $(this).attr("style","border:1px soild red");
            var htmlStr = "<span style='color:red;' class='mustInputMessage'>请输入" + $(this).attr("placeholder") + "</span>";
            $(this).parent().append(htmlStr);
            isTrue = false;
            return false;
        }
    });
    $(".mustselect").each(function () {
        if ($(this).val() == "") {
            $(this).focus();
            $(this).attr("style", "border:1px soild red");
            var htmlStr = "<span style='color:red;' class='errorMsg'>请选择" + $(this).attr("placeholder") + "</span>";
            $(this).parent().append(htmlStr);
            isTrue = false;
            return false;
        }
    });
    return isTrue;
}

/*弹出层*/
/*
	参数解释：
	title	标题
	url		请求的url
	id		需要操作的数据id
	w		弹出层宽度（缺省调默认值）
	h		弹出层高度（缺省调默认值）
*/
function layer_show(title, url, w, h) {
    if (title == null || title == '') {
        title = false;
    };
    if (url == null || url == '') {
        url = "404.html";
    };
    if (w == null || w == '') {
        w = 800;
    };
    if (h == null || h == '') {
        h = ($(window).height() - 50);
    };
    layer.open({
        type: 2,
        area: [w + 'px', h + 'px'],
        fix: false, //不固定
        maxmin: true,
        shade: 0.4,
        title: title,
        content: url
    });
}

/*关闭弹出框口*/
function layer_close() {
    var index = parent.layer.getFrameIndex(window.name);
    parent.layer.close(index);
}