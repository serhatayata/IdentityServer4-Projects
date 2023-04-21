// Log messages
function log() {
    document.getElementById('results').innerText = '';

    Array.prototype.forEach.call(arguments, function (msg) {
        if (msg instanceof Error) {
            msg = "Error: " + msg.message;
        }
        else if (typeof msg !== 'string') {
            msg = JSON.stringify(msg, null, 2);
        }
        document.getElementById('results').innerHTML += msg + '\r\n';
    });
}

// Click events
document.getElementById("login").addEventListener("click", login, false);
document.getElementById("api").addEventListener("click", api, false);
document.getElementById("logout").addEventListener("click", logout, false);

// Daha sonra ise OpenID Connect protokolünü kullanmak üzere 
// oidc-client kütüphanesi ile gelen UserManager sınıfı kullanılır
// Belirli konfigürasyon ayarları yapılır ve bunu UserManager'a yollarız

var config = {
    authority: "https://localhost:5001", // identity server url
    client_id: "jsClient", // identity server tarafındaki client'ın client id'si
    client_secret: "secret", // identity server tarafındaki client'ın client secret bilgisi
    redirect_uri: "https://localhost:5003/callback.html", // loginden sonra yönlendirilecek url
    response_type: "code", // Grant type : Authorization code
    scope: "openid profile", // istenen scope bilgileri
    post_logout_redirect_uri: "https://localhost:5003/index.html", // logout işleminden sonraki yönlendirilecek url
};
var mgr = new Oidc.UserManager(config);

mgr.getUser().then(function (user) {
    if (user) {
        log("User logged in", user.profile);
    }
    else {
        log("User not logged in");
    }
});

// Click eventlerinde belirtilen login,api ve logout metotları tanımlanır
// signinRedirect ve signoutRedirect metotları ise login ve logout için kullanılır

function login() {
    mgr.signinRedirect();
}

function api() {
    mgr.getUser().then(function (user) {
        var url = "https://localhost:5002/api/catalog"; // API Url

        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function () {
            log(xhr.status, JSON.parse(xhr.responseText));
        }
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    });
}

function logout() {
    mgr.signoutRedirect();
}