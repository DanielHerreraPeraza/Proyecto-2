function vRegistroSolicitudHotel() {
    this.service = 'solicitudHotel';
    this.ctrlActions = new ControlActions();
    this.Logeado = 0;
    this.existe = true;


    this.Create = function () {
        var solicitudHotelData = {};
        solicitudHotelData = this.ctrlActions.GetDataForm('frmEdition');
        //Hace el post al create
        this.ctrlActions.PostToAPI(this.service, solicitudHotelData, this.limpiar());
    }


    this.UsuarioLogeado = function () {

        var usuario = JSON.parse(window.sessionStorage.getItem("usuario"));
        if (usuario != null) {

            //llemar datos de usuario en los imputs
            document.getElementById("txtIDUsuario").value = usuario['Identificacion'];
            document.getElementById("txtIDUsuario").setAttribute('disabled', '');
            //$('#txtIDUsuario').addClass('d-none');
            document.getElementById("txtNombreUsuario").value = usuario['PrimerNombre'];
            document.getElementById("txtNombreUsuario").setAttribute('disabled', '');
            //$('#txtIDUsuario').addClass('d-none');            
            document.getElementById("txtCorreoUsuario").value = usuario['Correo'];
            document.getElementById("txtCorreoUsuario").setAttribute('disabled', '');
            //$('#txtIDUsuario').addClass('d-none');
            return 1;

        } else {
            return -1;
        }

    }
    this.ValidarInputs = function () {
        if ($("#frmEdition").valid()) {
            //el usuario existe?

            var usuarioD = {};
            usuarioD["Identificacion"] = document.getElementById("txtIDUsuario").value;
            usuarioD["Correo"] = document.getElementById("txtCorreoUsuario").value;

            this.ctrlActions.PostToValida("usuario/validarExiste", usuarioD, function (data) {
                var soli = new vRegistroSolicitudHotel();
                var existe = soli.ValidarExiste(data);
                var logeado = soli.UsuarioLogeado();
                //si existe y no está logeado
                if (existe == true && logeado == -1) {
                    swal("Inicie sesión", "El correo electrónico ya está asociado a otra cuenta, para continuar con el registro por favor inicie sesión", "warning");
                    document.querySelector("#txtCorreoUsuario").classList.add("is-invalid");
                } else {
                    soli.Create();
                }

            });
        }
    }

    this.limpiar = function () {
        $("#frmEdition")[0].reset();
        setTimeout(function redirection() { window.location.href = '/Home/Index/'; }, 3000);
    }

    this.ValidarExiste = function (data) {
        var existe = false;

        if (data != null) {
            existe = true;
        }

        return existe;
    }

}

$(document).ready(function () {
    var vregistrosolicitudhotel = new vRegistroSolicitudHotel();
    vregistrosolicitudhotel.UsuarioLogeado();
    ReglasValidacion();
});

ReglasValidacion = function () {
    $("#frmEdition").submit(function (e) {
        e.preventDefault();
    }).validate({
        lang: 'es',
        errorClass: "is-invalid",
        rules: {
            txtEmpresa: { required: true },
            txtCedulaJ: { required: true },
            txtClasificacion: { required: true, digits: true, min: 1, max: 5 },
            txtNombre: { required: true },
            txtCadena: { required: false },
            txtDescripcion: { required: true },
            txtDireccion: { required: true },
            txtIDUsuario: { required: true },
            txtNombreUsuario: { required: true },
            txtCorreoUsuario: { required: true, email: true }

        },
        errorPlacement: function (error, element) {
            element: "div";
            $(error).addClass('input-group mb-3');
            error.css({ 'padding-left': '10px', 'margin-right': '20px', 'padding-bottom': '2px', 'color': 'red' });
            error.insertAfter(element);
        }
    });
}

