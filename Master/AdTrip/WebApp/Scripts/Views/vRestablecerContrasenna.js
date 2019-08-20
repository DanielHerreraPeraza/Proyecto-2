function vRestablecerContrasenna() {

    this.service = 'seguridad';
    this.ctrlActions = new ControlActions();

    this.RestablecerContrasenna = function () {
        var correo = $('#txtCorreo').val();
        var identificacion = $('#txtIdentificacion').val();

        var usuario = {};
        usuario["Correo"] = correo;
        usuario["Identificacion"] = identificacion;

        this.ctrlActions.PutToAPI(this.service + "/RestablecerContrasenna", usuario, function () {
            setTimeout(function () {
                window.location.href = "/Home/vLogIn";
            }, 3000);
        });
    }

    this.ValidarInputs = function () {
        if ($("#frmEdition").valid()) {
            this.RestablecerContrasenna();
        }
    }
}

$(document).ready(function () {
    var vrestablecercontrasenna = new vRestablecerContrasenna();

    ReglasValidacion();
});

ReglasValidacion = function () {
    $("#frmEdition").submit(function (e) {
        e.preventDefault();
    }).validate({
        lang: 'es',
        errorClass: "is-invalid",
        rules: {
            txtCorreo: { required: true, EMAIL: true },
            txtIdentificacion: { required: true, rangelength: [9, 13], identificacion: true }
        },
        errorPlacement: function (error, element) {
            element: "div";
            $(error).addClass('input-group mb-3');
            error.css({ 'padding-left': '10px', 'margin-right': '20px', 'padding-bottom': '2px', 'color': 'red' });
            error.insertAfter(element);
        }
    });
}