
Vue.filter('formatDate', function (val, arg) {
    if (!val) return;
    return moment(val).format(arg || "YYYY-MM-DD HH:mm:ss")

})