function vRegistroEmpleado() {
    this.service = 'usuario';
    this.ctrlActions = new ControlActions();

    this.FilterSelect = function (data) {
        if (data != null) {
            //$('#txtRoles option').hide();
            //foreach()
            //$('#txtRoles').find('option').filter("option[value = " + selectedProvincia + "]").show();
        }
    }

    this.ctrlActions.GetById('rol/rolesGerente/115800345', this.FilterSelect);

    this.Registrar = function () {
        var usuarioData = {};
        usuarioData = this.ctrlActions.GetDataForm('frmEdition');

        var rolesData = {};
        rolesData = { ...$('.selectpicker').val() };

        usuarioData['Roles'] = rolesData;

        this.ctrlActions.PostToAPI(this.service, usuarioData);
    }

    this.ValidarInputs = function () {
        if ($("#frmEdition").valid()) {
            this.Registrar();
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

//ON DOCUMENT READY
$(document).ready(function () {
    var vempleado = new vRegistroEmpleado();

    $('#txtDistrito').attr("disabled", true);

    $("#divRol option[value=" + 'ADM' + "]").hide();
    $("#divRol option[value=" + 'GRT' + "]").hide();

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
            txtIdentificacion: { required: true, digits: true },
            txtPrimerNombre: { required: true },
            txtPrimerApellido: { required: true },
            txtSegundoNombre: { required: false },
            txtSegundoApellido: { required: true },
            txtProvincia: { required: true },
            txtCanton: { required: true },
            txtDistrito: { required: true },
            txtDireccionExacta: { required: true },
            txtCorreo: { required: true, email: true },
            txtTelefono: { required: true, digits: true, minlength: 8 },
            txtContrasenna: { required: true },
            txtConfContrasenna: { required: true, equalTo: "#txtContrasenna" },
            txtRoles: { required: true }
        },
        errorPlacement: function (error, element) {
            element: "div";
            $(error).addClass('input-group mb-3');
            error.css({ 'padding-left': '10px', 'margin-right': '20px', 'padding-bottom': '2px', 'color': 'red' });
            error.insertAfter(element);
        }
    });
}