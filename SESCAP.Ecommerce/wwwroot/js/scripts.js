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
    navigator.clipboard.writeText(input.value);
    copyText.classList.add("active");
    window.getSelection().removeAllRanges();
    setTimeout(function () {
        copyText.classList.remove("active");
    }, 2500);

});

$(document).ready(function () {
    var intervalo = 15000;
    var statusAnterior = null;

    function monitorarStatus() {
        var statusDoPagamento = $("#respostaCielo").val();

        if (statusDoPagamento !== statusAnterior) {

            redirecionar(statusDoPagamento);
            statusAnterior = statusDoPagamento;

        }
    }

    function redirecionar(_status) {

        var url = window.location.protocol + "//" + window.location.host + window.location.pathname + window.location.search;
        window.location.href = url;
        
    }

    setInterval(monitorarStatus, intervalo);
});


document.addEventListener('DOMContentLoaded', function(){

    var url_string = window.location.href;
    var url = new URL(url_string);
    var data = url.searchParams.get("paymentId");

    var paymentId = data;

    var botaoConfirmacaoPagamento = document.getElementById('respostaCielo');
    var botaoMovimentacao = document.getElementById('btn_movimentacoes');
    var botaoHome = document.getElementById('btn_home');

    function consultarStatusPagamento(paymentId){

        fetch(`/pagamento/consultar-status-pagamento-pix?paymentId=${paymentId}`)
        .then(response =>{
            if(!response.ok)
            {
                throw new Error('Erro ao consultar status do pagamento');
            }
            return response.json();
        })
        .then(data => {
            var statusPagamento = data.payment.status;

            console.log('Status do pagamento: ', statusPagamento);
            
            if(statusPagamento == 2){

                botaoConfirmacaoPagamento.innerHTML = "<p class='confirm_text'>Finalizando, aguarde...</p>";
                botaoConfirmacaoPagamento.style = "background: #fff3cd !important; color: #664d03 !important; pointer-events: none !important;";
                botaoMovimentacao.style = "pointer-events: none !important;";
                botaoHome.style = "pointer-events: none !important;";
            }
        })
        .catch(error => {
            console.error('Erro na requisição', error);
        });
    }
    setInterval(function(){
        consultarStatusPagamento(paymentId)
    }, 2000);
});


