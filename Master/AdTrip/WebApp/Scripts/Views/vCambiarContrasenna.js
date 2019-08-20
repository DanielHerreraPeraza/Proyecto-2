function vModificarContrasenna() {

    this.service = 'seguridad';
    this.ctrlActions = new ControlActions();

    this.ModificarContrasenna = function () {
        var usuario = JSON.parse(window.sessionStorage.getItem("usuario"));
        usuario["Contrasenna"] = $('#txtContrasennaActual').val();
        usuario["ContrasennaNueva"] = $('#txtContrasennaNueva').val();

        this.ctrlActions.PutToAPI(this.service + "/CambiarContrasenna", usuario, function () {
            setTimeout(function () {
                window.location.href = "/Home/vPerfilUsuario";
            }, 3000);
        });
    }

    this.ValidarInputs = function () {
        if ($("#frmEdition").valid()) {
            this.ModificarContrasenna();
        }
    }
}

$(document).ready(function () {
    var vmodificarcontrasenna = new vModificarContrasenna();

    ReglasValidacion();
});

ReglasValidacion = function () {
    $("#frmEdition").submit(function (e) {
        e.preventDefault();
    }).validate({
        lang: 'es',
        errorClass: "is-invalid",
        rules: {
            txtContrasennaActual: { required: true },
            txtContrasennaNueva: { required: true, contrasenna: true },
            txtConfirmacionContrasenna: { required: true, equalTo: '#txtContrasennaNueva'}
        },
        errorPlacement: function (error, element) {
            element: "div";
            $(error).addClass('input-group mb-3');
            error.css({ 'padding-left': '10px', 'margin-right': '20px', 'padding-bottom': '2px', 'color': 'red' });
            error.insertAfter(element);
        }
    });
}