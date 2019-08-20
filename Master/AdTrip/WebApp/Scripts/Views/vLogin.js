function vLogin() {

    this.service = 'seguridad';
    this.ctrlActions = new ControlActions();

    this.Login = function () {
        var data = {};
        data = this.ctrlActions.GetDataForm('frmEdition');
        var paginaInicio = "/home/vHoteles";//pagina de inicio del usuario
        var correoLogin = $("#txtCorreo").val();
        var solicitud = $("#txtIdSolicitud").val();

        var roles = window.sessionStorage.getItem("rolesUsuario");

        if (solicitud != "nulo") {

            var correoSoli = $('#txtCorreoSoli').val();

            if (correoLogin == correoSoli) {
                paginaInicio = "/home/vRegistroHotel/" + solicitud;
            }
        }

        this.ctrlActions.Login(this.service + "/login", data, paginaInicio);
    }

    this.Register = function () {
        window.location.href = "/Home/vRegistroUsuarios";
    }

    this.ValidarInputs = function () {
        if ($("#frmEdition").valid()) {
            this.Login();
        }
    }
    this.ObtenerSolicitud = function () {
        var id = document.getElementById("txtIdSolicitud").value;

        if (id != "nulo") {
            this.ctrlActions.GetById("solicitudHotel/" + id, this.GuardarSolicitud);
        } else {
            //window.location.href = "/Home/vRegistroSolicitudHotel/";
        }

    }
    this.GuardarSolicitud = function (data) {
        if (data != null) {
            document.getElementById("txtCorreoSoli").value = data['CorreoUsuario'];
        } else {
            CorreoUsuarioSolicitud = "nulo";
        }

    }
}

//ON DOCUMENT READY
$(document).ready(function () {
    var vlogIn = new vLogin();
    ReglasValidacionLogin();
});

ReglasValidacionLogin = function () {
    $("#frmEdition").submit(function (e) {
        e.preventDefault();
    }).validate({
        lang: 'es',
        errorClass: "is-invalid",
        rules: {
            txtCorreo: "required EMAIL",
            txtContrasenna: { required: true/*, minlength: 6*/}
        },
        errorPlacement: function (error, element) {
            element: "div";
            $(error).addClass('input-group mb-3');
            error.css({ 'padding-left': '10px', 'margin-right': '20px', 'padding-bottom': '2px', 'color': 'red' });
            error.insertAfter(element);
        }
    });
}

function onSignIn(googleUser) {
    this.ctrlActions = new ControlActions();

    var profile = googleUser.getBasicProfile();

    var usuario = {};
    usuario['Contrasenna'] = googleUser.getAuthResponse().id_token;
    this.ctrlActions.Login("seguridad/googleLogin", usuario, "vHoteles");
}