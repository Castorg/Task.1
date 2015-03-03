
function LoadJson() {
    var temp = new XMLHttpRequest();
    temp.open("GET", "/contact.json", true);
    temp.send();
   
    temp.onreadystatechange = function () {
        if (temp.readyState == 4) {
            document.getElementById("One").innerText = temp.response;
            document.getElementById("Two").innerHTML = JsonTree(JSON.parse(temp.response), 0);
        }
    }
}

function One() {
    document.getElementById('OneTab').className = 'SelectedTab';
    document.getElementById('TwoTab').className = 'Tab';

    document.getElementById('One').style.display = 'block';
    document.getElementById('Two').style.display = 'none';
}

function Two() {
    document.getElementById('OneTab').className = 'Tab';
    document.getElementById('TwoTab').className = 'SelectedTab';

    document.getElementById('One').style.display = 'none';
    document.getElementById('Two').style.display = 'block';
}

function LogOn() {
    var abc = document.getElementsByClassName('log');
    for (var i = 0; i < abc.length; i++) {
        abc[i].style.display = 'none';
    }
    var xyz = document.getElementsByClassName('HiddenFiend');
    for (var i = 0; i < xyz.length; i++) {
        xyz[i].style.display = 'inline';
    }
}

function LogOff() {
    var abc = document.getElementsByClassName('log');
    for (var i = 0; i < abc.length; i++) {
        abc[i].style.display = 'inline';
    }
    var xyz = document.getElementsByClassName('HiddenFiend');
    for (var i = 0; i < xyz.length; i++) {
        xyz[i].style.display = 'none';
    }
}

function JsonTree(object , rootlevel) {
    var json = "<ul>";
    for (prop in object) {
        var value = object[prop];
        switch (typeof (value)) {
            case "object":  
                var token = rootlevel;
                if (value != null) {
                    json += "<li>  " +
                        "<input type='image' src ='/3.jpg' OnClick='HiddenInnerInfo(this)'/>" +
                        "<span" + " data-toggle='collapse'>" + prop + "</span> " +
                        "<div id='" + token + "'class='collapse'>" + JsonTree(value, rootlevel + 1) + "</div>" +
                        "</li>";
                } else {
                    json += "<li><img src='/2.jpg'>" + prop + "</li>";
                }
                break;
            default:
                json += "<li><img src='/2.jpg'>" + prop + " : " + value + "</li>";
        }
    }
    return json + "</ul>";
}

function HiddenInnerInfo(thisvalue) {
    var qu = thisvalue.parentNode.childNodes;

    var a = qu[1].src;
    var b;
    if (!a.match("3.jpg")) 
         b = a.replace("1.jpg", "3.jpg");
    else 
         b = a.replace("3.jpg", "1.jpg");
    qu[1].src = b;

    if (qu[qu.length - 1].childNodes[0].style.display != "none")
        qu[qu.length - 1].childNodes[0].style.display = "none";
    else
        qu[qu.length - 1].childNodes[0].style.display = "list-item";
}

function Call() {
    var msg = document.getElementById("formLog");
    var log = msg[0].value;
    var pass = msg[1].value;
    var data = '<' + log + '><' + pass +'>';
    var temp = new XMLHttpRequest();
    temp.open("POST", ".log", true);
    temp.send(data);
    temp.onreadystatechange = function () {
        if (temp.readyState == 4) {
            var answer = temp.response;
            if (answer == "authorize") {
                LogOnComplete();
            }
        }
    }
}

function CheckCookie() {
    var answer = getCookie("keep-alive");
    if (answer != undefined) {
        if (answer == "1=keep") {
             LogOn();
        }
        else {
            LogOff();
        }
    }
}
function getCookie(name) {
    var matches = document.cookie.match(new RegExp("(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"));
    return matches ? decodeURIComponent(matches[1]) : undefined;

}

function LogOut() {
    delCookie("keep-alive");
    LogOff();
}
function delCookie(name) {
    var cookieDate = new Date();
    cookieDate.setTime(cookieDate.getTime() - 1);
    document.cookie = name + "=; expires=" + cookieDate.toGMTString();
    var answer = getCookie("isAuthorise");
}