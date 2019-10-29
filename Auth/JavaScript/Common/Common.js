function GetParameterByName(name) {
    url = window.location.href;
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}

function LoginCheck() {
    var lCookies = document.cookie;

    if (lCookies == "") {
        return false;
    } else {
        return true;
    }
}

function GetCookieValue(key) {
    debugger;
    var lValue = "";
    var lKeyValPaireArr = document.cookie.split(";");
    for (var i = 0; i < lKeyValPaireArr.length; i++) {
        if (lKeyValPaireArr[i].split("=")[0] == key) {
            lValue = lKeyValPaireArr[i].split("=")[1];
        }
    }
    return lValue;
}

function RemoveCookie(name) {
    document.cookie = name + "=0;expires=" + new Date(0).toUTCString() + ";path=/;";
}

