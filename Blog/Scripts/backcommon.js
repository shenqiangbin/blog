//全局的ajax访问，处理ajax清求时session超时
$.ajaxSetup({
    contentType: "application/x-www-form-urlencoded;charset=utf-8",
    complete: function (XMLHttpRequest, textStatus) {
        //通过XMLHttpRequest取得响应头，sessionstatus，
        var sessionstatus = XMLHttpRequest.getResponseHeader("userstatus");
        if (sessionstatus == "unauthorize") {
            window.location.href = "/backmgr/account/unauthorize";
            //如果超时就处理 ，指定要跳转的页面(比如登陆页)
            //$('body').append('<div style="height: 100%;position: absolute;z-index: 999999999;background-color: #F8F8F8;width: 100%;top: 0;text-align: center;padding-top: 200px;">对不起，无权操作</div>');
            //setTimeout(function () { window.location.href = "/"; }, 2000);

        }
    }
});

var msger = {
    tip: function (msg) {
        layer.msg(msg);
    }
}

function removejscssfile(filename, filetype) {
    debugger;
    var targetelement = (filetype == "js") ? "script" : (filetype == "css") ? "link" : "none";
    var targetattr = (filetype == "js") ? "src" : (filetype == "css") ? "href" : "none";
    var allsuspects = document.getElementsByTagName(targetelement);
    for (var i = allsuspects.length; i >= 0; i--) {
        
        if (allsuspects[i] && allsuspects[i].getAttribute(targetattr) != null && allsuspects[i].getAttribute(targetattr).indexOf(filename) != -1)
            allsuspects[i].parentNode.removeChild(allsuspects[i]);
    }

}

function loadjscssfile(filename, filetype) {
    if (filetype == "js") {
        var fileref = document.createElement('script');
        fileref.setAttribute("type", "text/javascript");
        fileref.setAttribute("src", filename);
    }
    else if (filetype == "css") {
        var fileref = document.createElement("link");
        fileref.setAttribute("rel", "stylesheet");
        fileref.setAttribute("type", "text/css");
        fileref.setAttribute("href", filename);
    }

    if (typeof fileref != "undefined")
        document.getElementsByTagName("head")[0].appendChild(fileref);
}