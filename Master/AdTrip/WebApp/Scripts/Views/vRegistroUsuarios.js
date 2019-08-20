function vRegistroUsuarios() {
    this.service = 'usuario';
    this.ctrlActions = new ControlActions();
    var rolesUsuario = this.ctrlActions.GetRolesUsuario();
    this.IdSolicitud;

    if (rolesUsuario == null) {
        $('#divRol').hide();
    }
    //if (rolesUsuario.includes('GRT')) {
    //    $("#divRol option[value=" + 'ADM' + "]").hide();
    //    $("#divRol option[value=" + 'GRT' + "]").hide();
    //}

    this.Registrar = function () {
        this.IdSolicitud = document.getElementById("txtIdSolicitud").value;
        var usuarioData = {};
        usuarioData = this.ctrlActions.GetDataForm('frmEdition');


        var rolesData = {};
        rolesData = { ...$('#txtRol').val() };

        usuarioData['Roles'] = rolesData;
        var roles = usuarioData['Roles'];

        if (jQuery.isEmptyObject(roles)) {
            usuarioData['Roles'] = ["CLT"];
        }
        if (usuarioData['Roles'][0] == "CLT" && this.IdSolicitud != "nulo") {
            usuarioData['Roles'] = ["GRT"];
            var correo = $('#txtCorreo').val();
            if (sessionStorage.getItem('usuario') == null) {
                sessionStorage.setItem('rolesUsuario', ["GRT"]);
                sessionStorage.setItem('correoUsuario', correo);
            }

            this.ctrlActions.PostToAPI(this.service, usuarioData, function () {
                var control = new ControlActions();
                var cuentaData = {};
                var usuarioData = {};
                usuarioData = control.GetDataForm('frmEdition');
                cuentaData.TipoCuenta = 'Gerente';
                cuentaData.IdUsuario = usuarioData['Identificacion'];

                control.PostToAPI('cuenta', cuentaData, function () {
                    if (sessionStorage.getItem('usuario') == null) {
                        sessionStorage.removeItem('rolesUsuario');
                        sessionStorage.removeItem('correoUsuario');
                    }

                });
                swal("Registro completado", "Puede continuar con el registro del hotel. Se le enviará un correo electrónico para activar su cuenta", "success");
                setTimeout(function redirection() { window.location.href = '/Home/vLogin/' + this.IdSolicitud; }, 3000);

            });


        } else {
            var correo = $('#txtCorreo').val();
            if (usuarioData['Roles'][0] == "CLT") {
                var correo = $('#txtCorreo').val();
                if (sessionStorage.getItem('usuario') == null) {
                    sessionStorage.setItem('rolesUsuario', ["CLT"]);
                    sessionStorage.setItem('correoUsuario', correo);
                }

            }


            this.ctrlActions.PostToAPI(this.service, usuarioData, function () {
                var control = new ControlActions();
                var cuentaData = {};
                var usuarioData = {};
                usuarioData = control.GetDataForm('frmEdition');
                cuentaData.TipoCuenta = 'Cliente';
                cuentaData.IdUsuario = usuarioData['Identificacion'];

                control.PostToAPI('cuenta', cuentaData, function () {

                });
                if (usuarioData['Roles'][0] == "CLT") {
                    if (sessionStorage.getItem('usuario') == null) {
                        sessionStorage.removeItem('rolesUsuario');
                        sessionStorage.removeItem('correoUsuario');
                    }

                }
                swal("Registro completado", "Cuenta creada, se le enviará un correo electrónico para activar su cuenta", "success");
                setTimeout(function redirection() { window.location.href = '/Home/vLogin/'; }, 3000);
            });

        }

    }

    this.LlenarRoles = function (usuarioData) {
        if (rolesUsuario == null) {
            return;
        }
    };

    this.ValidarInputs = function () {
        if ($("#frmEdition").valid()) {
            this.Registrar();
        }
    }

    this.ObtenerSolicitud = function () {
        var id = document.getElementById("txtIdSolicitud").value;

        if (id != "nulo") {
            this.ctrlActions.GetById("solicitudHotel/" + id, this.GuardarSolicitud);
        } else {

            this.IdSolicitud = "nulo";
        }

    }

    this.GuardarSolicitud = function (data) {
        if (data != null) {

            var dataEstado = data['Estado'];
            if (dataEstado == "Aprobada") {

                this.IdSolicitud = data['CodigoSolicitud'];
                document.getElementById("txtIdentificacion").value = data['IdUsuario'];
                document.getElementById("txtIdentificacion").setAttribute('disabled', '');

                document.getElementById("txtCorreo").value = data['CorreoUsuario'];
                document.getElementById("txtCorreo").setAttribute('disabled', '');

                document.getElementById("txtPrimerNombre").value = data['NombreUsuario'];



            } else {
                swal("No se puede continuar", "La solicitud no se ha aprobado aún", "error");
                setTimeout(function redirection() { window.location.href = '/Home/vRegistroSolicitudHotel/'; }, 3000);
            }
        }

    }
}

this.GetDataUsuario = function (formId) {
    var data = {};

    $('#' + formId + ' *').not(".selectpicker").filter(':input').each(function (input) {
        var columnDataName = $(this).attr("ColumnDataName");
        if (columnDataName.localeCompare('') != 0) {
            data[columnDataName] = this.value;
        }
    });

    return data;
}

$(document).ready(function () {
    var vusuario = new vRegistroUsuarios();

    $('#txtDistrito').attr("disabled", true);

    $("#divRol option[value=" + 'ADM' + "]").hide();
    $("#divRol option[value=" + 'GRT' + "]").hide();

    if (window.sessionStorage.getItem("rolesUsuario") != null && window.sessionStorage.getItem("rolesUsuario").includes('GRT')) {
        let usuarioId = window.sessionStorage.getItem("idUsuario");

        $('#txtRoles option').hide();
        $('#txtRoles').find('option').filter("option[class = " + usuarioId + "]").show();
    }

    $(function () {
        var showCanton = function (selectedProvincia) {
            $('#txtCanton option').hide();
            $('#txtCanton').find('option').filter("option[value ^= " + selectedProvincia + "]").show();
            //set default value
            var defaultCanton = "Seleccione una provincia";
            $('#txtCanton').val(defaultCanton);
        };

        var showDistrito = function (selectedCanton) {
            $('#txtDistrito option').hide();
            $('#txtDistrito').find('option').filter("option[value ^= " + selectedCanton + "]").show();
            //set default value
            var defaultDistrito = "Seleccione un cantón";
            $('#txtDistrito').val(defaultDistrito);
        };

        //set default provincia
        var provincia = $('#txtProvincia').val();
        showCanton(provincia);
        $('#txtProvincia').change(function () {
            showCanton($(this).val());
        });

        //set default canton
        var canton = $('#txtCanton').val();
        showDistrito(canton);
        $('#txtCanton').change(function () {
            $('#txtDistrito').removeAttr("disabled");
            showDistrito($(this).val());
        });

        $('#txtDistrito').change(function () {

        });
    });

    ReglasValidacion();
});

function limpiarFormulario() {
    $("#frmEdition")[0].reset();
}

ReglasValidacion = function () {
    $("#frmEdition").submit(function (e) {
        e.preventDefault();
    }).validate({
        lang: 'es',
        errorClass: "is-invalid",
        rules: {
            txtIdentificacion: { required: true, rangelength: [9, 13], identificacion: true },
            txtPrimerNombre: { required: true },
            txtPrimerApellido: { required: true },
            txtSegundoNombre: { required: false },
            txtSegundoApellido: { required: true },
            txtProvincia: { required: true },
            txtCanton: { required: true },
            txtDistrito: { required: true },
            txtDireccionExacta: { required: true },
            txtCorreo: "required EMAIL",
            txtTelefono: { required: true, digits: true, rangelength: [7, 12] },
            txtContrasenna: { required: true, contrasenna: true },
            txtConfContrasenna: { required: true, equalTo: "#txtContrasenna" }
        },
        errorPlacement: function (error, element) {
            element: "div";
            $(error).addClass('input-group mb-3');
            error.css({ 'padding-left': '10px', 'margin-right': '20px', 'padding-bottom': '2px', 'color': 'red' });
            error.insertAfter(element);
        }
    });
}