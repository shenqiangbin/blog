$(document).ready(function (e) {
    //当滚动条的位置处于距顶部100像素以下时，跳转链接出现，否则消失
    $(function () {
        $(window).scroll(function () {
            if ($(window).scrollTop() > 100) { //大于100行才出现跳转箭头
                $("#top").fadeIn(500);  //大于1500行时跳转箭头慢慢透明显示
            }
            else {
                $("#top").fadeOut(500);  //大于1500行时跳转箭头慢慢透明消失
            }
        });
        //当点击跳转链接后，回到页面顶部位置
        $("#top").click(function () {
            $('body,html').animate({ scrollTop: 0 }, 500);//1s完成回到顶部
            return false;
        });
    });
});