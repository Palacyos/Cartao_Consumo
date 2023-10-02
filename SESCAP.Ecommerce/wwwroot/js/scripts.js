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


var json_bandeiras = {
    "tipos": [
        {
            "sigla": "CreditCard",
            "nome": "Credito",
            "bandeiras": [
                "Master",
                "Visa",
                "Elo",
                "Amex",
                "Diners",
                "JCB",
                "Hipercard"

            ]
        },

        {
            "sigla": "DebitCard",
            "nome": "Debito",
            "bandeiras": [
                "Master",
                "Visa",
                "Elo"
            ]
        }
    ]
};

function buscarBandeiras(e) {
    document.querySelector("#bandeira").innerHTML = '';
    var bandeira_select = document.querySelector("#bandeira");

    var num_tipos = json_bandeiras.tipos.length;
    var j_index = -1;

    for (var x = 0; x < num_tipos; x++) {
        if (json_bandeiras.tipos[x].sigla == e) {
            j_index = x;
        }
    }

    if (j_index != -1) {

        json_bandeiras.tipos[j_index].bandeiras.forEach(function (bandeira) {
            var ban_opts = document.createElement('option');
            ban_opts.setAttribute('value', bandeira)
            ban_opts.innerHTML = bandeira;
            bandeira_select.appendChild(ban_opts);

        });
    } else {
        document.querySelector("#bandeira").innerHTML = '';
    }
}

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
    //var regex = /^[0-9.,]+$/;
    var regex = /^[0-9.]+$/;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault) theEvent.preventDefault();
    }
}