var token = localStorage.getItem("token");
$(function () {
    $.ajaxSetup({
        dataType: "json",
        cache: false,
        headers: {
            "auth": token
        },
        xhrFields: {
            withCredentials: true
        },
        beforeSend: function (xhr, request) {
            console.log(token);
            xhr.setRequestHeader('auth', token);
        },
        complete: function (xhr) {
            //token过期，则跳转到登录页面
            if (xhr.responseJSON.code == 401) {
                parent.location.href = baseURL + 'login.html';
            }
        }
    });
});