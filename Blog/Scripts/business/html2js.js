$(function () {
    $("#btnGo").click(function () {
        var html = $("#htmlBox").val();
        if (!html) {
            $("#htmlBox").addClass("is-invalid");
            $("#htmlMsg").addClass("invalid-feedback");
            $("#htmlMsg").html("HTML不能空");
        } else {
            $("#htmlBox").removeClass("is-invalid");
            $("#htmlMsg").removeClass("invalid-feedback");
            $("#htmlMsg").html("");
        }
        html = html.replace(new RegExp("\"", "g"), "'");
        var arr = html.split("\n");

        var newArr = [];
        for (var i = 0; i < arr.length; i++) {
            if (arr[i]) {
                newArr.push("\"" + arr[i] + "\"");
            }
        }
        var s = newArr.join(" + \n");
        s = "var str = \n" + s;
        s = s + ";";

        $("#jsBox").val(s);
    });

    $("#btnGo").click();
});