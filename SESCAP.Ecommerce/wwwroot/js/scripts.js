const pwShowHide = document.querySelectorAll(".formulario__input-icon");



pwShowHide.forEach(inputIcon => {

    inputIcon.addEventListener("click", () => {

        let pwFields = inputIcon.parentElement.parentElement.querySelectorAll(".password");


        pwFields.forEach(password => {

            if (password.type === "password") {

                password.type = "text";
                inputIcon.classList.replace("fa-eye-slash", "fa-eye");
                return;
            }

            password.type = "password";
            inputIcon.classList.replace("fa-eye", "fa-eye-slash");

        });

    });

});

function formatar(mascara, documento) {
    var i = documento.value.length;
    var saida = mascara.substring(0, 1);
    var texto = mascara.substring(i);

    if (texto.substring(0, 1) != saida) {
        documento.value += texto.substring(0, 1);
    }
}

function onlynumber(evt) {
    var theEvent = evt || window.event;
    var key = theEvent.keyCode || theEvent.which;
    key = String.fromCharCode(key);
    var regex = /^[0-9.]+$/;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault) theEvent.preventDefault();
    }
}

let copyText = document.querySelector(".copy_text");
copyText.querySelector("button").addEventListener("click", function () {

    let input = copyText.querySelector("input.chave_pix_text");
    input.select();
    document.execCommand("copy");
    copyText.classList.add("active");
    window.getSelection().removeAllRanges();
    setTimeout(function () {
        copyText.classList.remove("active");
    }, 2500);

});

$(document).ready(function () {
    var intervalo = 8000;
    var statusAnterior = null;

    function monitorarStatus() {
        var statusDoPagamento = $("#respostaCielo").val();

        if (statusDoPagamento !== statusAnterior) {

            redirecionar(statusDoPagamento);
            statusAnterior = statusDoPagamento;

        }
    }

    function redirecionar(status) {

        var url = window.location.protocol + "//" + window.location.host + window.location.pathname + window.location.search;
        window.location.href = url;
    }

    setInterval(monitorarStatus, intervalo);
});