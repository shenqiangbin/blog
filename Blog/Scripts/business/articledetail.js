$(function () {


    //二维码
    var url = location.href;
    $("#qrcode").qrcode({
        render: "canvas",    //设置渲染方式，有table和canvas，使用canvas方式渲染性能相对来说比较好
        text: url,    //扫描二维码后显示的内容,可以直接填一个网址，扫描二维码后自动跳向该链接
        width: "150",               //二维码的宽度
        height: "150",              //二维码的高度
        background: "#ffffff",       //二维码的后景色
        foreground: "#07a0e1",        //二维码的前景色
        src: ''             //二维码中间的图片
    });

    //加载标签
    function initLabelContainer() {
        $.get("/backmgr/label/GetLablesByArticle", { articleId: articleId }, function (result) {
            if (result.code == 200) {
                $("#labelContainer").html('');
                var arr = result.data;
                if (arr.length!=0)
                    $("#labelContainer").append('<img src="/Images/label3.png" alt="标签" width="14"/>');                
                for (var i = 0; i < arr.length; i++) {
                    var item = arr[i];                                    
                    var div = $("<a href='#' class='label-item'>{1}</a>".replace("{1}", item.Name));
                    $("#labelContainer").append(div);
                }
            }
        });
    }
    initLabelContainer();

    //var testEditormdView = editormd.markdownToHTML("test-editormd-view", {
    //    htmlDecode: "style,script,iframe",  // you can filter tags decode
    //    emoji: false,
    //    taskList: true,
    //    tocm: true,
    //    tocContainer: "#custom-toc-container", // 自定义 ToC 容器层
    //    tex: true,  
    //    flowChart: true,
    //    sequenceDiagram: true,
    //});
    var html = $(".editormd-markdown-toc").html();
    $("#custom-toc-container").html(html);

    //实现部分行高亮显示
    $("ol.linenums").each(function (index, item) {
        $(item).children().each(function (indexLi, itemLi) {
            var innerHtml = $(itemLi).html();
            //如果包含"gl>"
            if (innerHtml.indexOf('gl</span><span class="pun">&gt;') > -1) {
                $(itemLi).addClass("special");
                $(itemLi).html(innerHtml.replace('gl</span><span class="pun">&gt;', ''));
            }
        });
    });

    $("#loadCommentBtn").click(function () {
        $(this).remove();
        $(".commentBox").removeClass('hidden');

        //初始化表情
        $("#content").emoji({
            button: "#btn",
            showTab: false,
            animation: 'none',
            icons: [{
                name: "QQ表情",
                path: "/scripts/emoji/img/qq/",
                maxNum: 91,
                excludeNums: [41, 45, 54, 15],
                file: ".gif"
            }]
        });

        getList(1);
    });

    $("#loadCommentBtn").click();

    $("#publishBtn").click(function () {
        var userName = $("#userName").val().trim();
        var email = $("#email").val().trim();
        var site = $("#site").val().trim();
        var content = $("#content").html();

        //长度和邮箱，网址 验证略
        if (!userName) {
            layer.msg("昵称不能为空");
            return;
        }
        if (!email) {
            layer.msg("邮箱不能为空");
            return;
        }
        if (!content) {
            layer.msg("评论不能为空");
            return;
        }

        var data = { articleId: articleId, Content: content, userName: userName, email: email, site: site };
        $.ajax({
            type: "POST",
            url: "/Comment/Add",
            data: data,
            success: function (result) {
                if (result.code == 200) {
                    $("#content").html("");
                    showMsg("发表成功");
                    getList(1);
                } else {
                    showMsg(result.msg);
                }
            },
            error: function (msg) {
                showMsg("发表失败");
            }
        });

        //$.post("/Comment/Add", AddAntiForgeryToken(data), function (result) {
        //    alert('post ok');
        //    console.log(result);
        //    if (result.code == 200) {
        //        showMsg("发表成功");
        //        getList();
        //    } else {
        //        showMsg("发表失败");
        //    }
        //}, function () {
        //    alert('error');
        //});
    });


   

});


AddAntiForgeryToken = function (data) {
    data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
    return data;
};

showMsg = function (msg) {
    layer.msg(msg);
}

/*获取评论信息*/
getList = function (page) {
    $.get("/comment/List", { articleId: articleId, page: page }, function (result) {
        if (result.code == 200) {
            $("#commentlist").html("");
            for (var i = 0; i < result.data.length; i++) {
                var record = result.data[i];
                addRecord(record);
            }

            $("input[name = pageNumber]").val(result.pageNumber);
            $("span[name = pageNumber]").html(result.pageNumber);
            $("#totalPage").html(result.pageCount);

            if (result.pageCount <= 1) {
                $("#pageBox").addClass("hidden");
            } else {
                $("#pageBox").removeClass("hidden");
            }

            if (result.pageNumber == 1) {
                $("#btnPreview").attr('disabled', "true");
            } else {
                $("#btnPreview").removeAttr('disabled');
            }
            if (result.pageNumber == result.pageCount) {
                $("#btnNext").attr('disabled', "true");
            } else {
                $("#btnNext").removeAttr('disabled');
            }

        }
    });
}

var content = $("#commentlistModel").prop("outerHTML");

addRecord = function (record) {
    var c2 = content
             .replace("{0}", record.UserName)
             .replace("{1}", record.Content)
             .replace("{2}", data_string(record.CreateTime))
             .replace("{3}", record.CommentId)
             .replace("{3}", record.CommentId)
             .replace("{5}", record.Site)
             .replace("{5}", record.Site);

    //构建回复列表
    var arr = new Array();
    if (record.Children) {
        for (var i = 0; i < record.Children.length; i++) {
            var replyHtml = buildReplyItem(record.Children[i]);
            arr.push(replyHtml);
        }
    }
    var arrStr = arr.join('');
    c2 = c2.replace("{4}", arrStr);
    $("#commentlist").append(c2);
}

//构建回复列表项
buildReplyItem = function (comment) {
    //console.log(comment);
    var html = "<div class='replyContainer'>" +
                "<div class='replytitle'>{0}<a target='_blank' style='margin-left:10px;font-size:small;vertical-align:top' href='{5}'>{5}</a></div>" +
                "<div class='replyContent'>{1}</div> " +
                "<div class='replyDate'>{2}</div>" +
                "</div>";
    html = html
        .replace("{0}", comment.UserName)
        .replace("{1}", comment.Content)
        .replace("{5}", comment.Site)
        .replace("{5}", comment.Site)
        .replace("{2}", data_string(comment.CreateTime));
    return html;
}

function data_string(str) {
    var d = eval('new ' + str.substr(1, str.length - 2));
    var ar_date = [d.getFullYear(), d.getMonth() + 1, d.getDate(), d.getHours(), d.getMinutes(), d.getSeconds()];
    for (var i = 0; i < ar_date.length; i++) ar_date[i] = dFormat(ar_date[i]);
    return ar_date.slice(0, 3).join('-') + ' ' + ar_date.slice(3).join(':');

    function dFormat(i) { return i < 10 ? "0" + i.toString() : i; }
}

previewPage = function () {
    var pageNumber = $("input[name = pageNumber]").val();
    pageNumber--
    getList(pageNumber);
}

nextPage = function () {
    var pageNumber = $("input[name = pageNumber]").val();
    pageNumber++;
    getList(pageNumber);
}

/*点击回复按钮*/
showReplyBox = function (btn) {
    $(".replyBox").remove();
    var html =
        "<div class='replyBox'>" +
        "    <p>" +
        "        <span style='width:74px;display: inline-block;'><span class ='must'>*</span>昵称：</span>" +
        "        <input type='text' id='userName' value='' style='width:335px'/>" +
        "    </p>" +
        "    <p>" +
        "        <span style='width:74px;display: inline-block;'><span class ='must'>*</span>邮箱：</span>" +
        "        <input type='text' id='email' value='' style='width:335px'/>" +
        "    </p>" +
        "    <p>" +
        "        <span style='width:74px;display: inline-block;'>个人站点：</span>" +
        "        <input type='text' id='site' value='' style='width:335px'/>" +
        "    </p>" +
        "    <div id='content2' class ='commentcontent commentcontent2' contenteditable='true'></div>" +
        "    <img id='btn2' class ='emojiBtn' src='/Images/emoji.png' />" +
        "    <button id='publishBtn2' class ='publishBtn'>发表</button>" +
        "</div>";

    $(".replyBox").remove();
    $(btn).parent().parent().append(html);

    $("#content2").emoji({
        button: "#btn2",
        showTab: false,
        animation: 'none',
        icons: [{
            name: "QQ表情",
            path: "/scripts/emoji/img/qq/",
            maxNum: 91,
            excludeNums: [41, 45, 54, 15],
            file: ".gif"
        }]
    });

    $("#publishBtn2").click(function () {
        var content = $("#content2").html();
        var replyListDiv = $(this).parent().parent().find('div.replyList');
        var commentId = replyListDiv.attr('cid');

        var userName = $(".replyBox #userName").val().trim();
        var email = $(".replyBox #email").val().trim();
        var site = $(".replyBox #site").val().trim();

        var data = { articleId: articleId, Content: content, CommentId: commentId, userName: userName, email: email, site: site };
        var replyBoxDiv = $(this).parent();

        $.post("/Comment/Add", data, function (result) {
            //console.log(result);
            if (result.code == 200) {
                var record = result.data;
                var html = buildReplyItem(record);
                replyListDiv.append(html);
                replyBoxDiv.remove();
            } else {
                alert("发表失败");
            }
        });
    });
}