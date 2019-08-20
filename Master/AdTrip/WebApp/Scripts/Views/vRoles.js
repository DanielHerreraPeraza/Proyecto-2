function vRoles() {

    this.tblRolesId = 'tblRoles';
    this.service = 'rol';
    this.serviceUsuario = 'rol/rolesGerente/';
    this.ctrlActions = new ControlActions();
    this.columns = "Codigo,Nombre,Descripcion,Estado";
    var usuarioId = window.sessionStorage.getItem("idUsuario");
    var roles = window.sessionStorage.getItem("rolesUsuario");

    this.RetrieveAll = function () {
        if (roles.includes('ADM')) {
            this.ctrlActions.FillTable(this.service, this.tblRolesId, false);
        }
        else {
            this.ctrlActions.FillTable(this.serviceUsuario + usuarioId, this.tblRolesId, false);
        }
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblRolesId, true);
    }

    this.Create = function () {
        var rolData = {};
        rolData = this.ctrlActions.GetDataForm('frmCreate');

        var vistasData = {};
        vistasData = { ...$('#txtVistas').val() };

        var hotelesData = {};
        hotelesData = { ...$('#txtHoteles').val() };

        rolData['Vistas'] = vistasData;
        rolData['Hoteles'] = hotelesData;
        this.ctrlActions.PostToAPI(this.service, rolData, function () {
            var vrol = new vRoles();
            vrol.ReloadTable();
            vrol.limpiarFrmCrear();
            $('.nav-tabs li').click();
        });
    }

    this.Update = function () {
        var rolData = {};
        rolData = this.ctrlActions.GetDataForm('frmEdition');

        this.ctrlActions.PutToAPI(this.service, rolData, function () {
            var vrol = new vRoles();
            vrol.ReloadTable();
            vrol.limpiarFrmModificar();
            $('.nav-tabs li').click();
        });
    }

    this.Delete = function () {

        var rolData = {};
        rolData = this.ctrlActions.GetDataForm('frmEdition');

        this.ctrlActions.DeleteToAPI(this.service, rolData);

        this.ReloadTable();
        this.limpiarFormulario();
        $('.nav-tabs li').click();

    }

    this.BindFields = function (data) {
        this.ctrlActions.BindFields('frmEdition', data);
        this.esconderListar();
        this.esconderCrear();
        this.mostrarModificar();
    }

    this.tab_click = function (ctl) {
        this.tab_handling(ctl);

        div = $(ctl).attr("data-div");

        if (div == "listaRoles") {
            this.mostrarListar();
            this.esconderCrear();
            this.esconderModificar();
        }
        else if (div == "registrarRoles") {
            this.mostrarCrear();
            this.esconderListar();            
            this.esconderModificar();
            this.limpiarFrmCrear();
        }
    }

    this.tab_handling = function (ctl) {
        $(".nav-tabs li").removeClass("active");
        ctl.addClass("active");
    }

    this.initializepanel = function () {
        $("#home_content").show();
        $(".nav-tabs li a").attr("href", "javascript:;");
    }

    this.mostrarListar = function () {
        $("#listaRoles").removeClass("d-none");
        $("#listaRoles").addClass("d-inline");
    }

    this.esconderListar = function () {
        $("#listaRoles").removeClass("d-inline");
        $("#listaRoles").addClass("d-none");
    }

    this.mostrarCrear = function () {
        $("#crearRoles").removeClass("d-none");
        $("#crearRoles").addClass("d-inline");
    }

    this.esconderCrear = function () {
        $("#crearRoles").removeClass("d-inline");
        $("#crearRoles").addClass("d-none");
    }

    this.mostrarModificar = function () {
        $("#modificarRoles").removeClass("d-none");
        $("#modificarRoles").addClass("d-inline");
    }

    this.esconderModificar = function () {
        $("#modificarRoles").removeClass("d-inline");
        $("#modificarRoles").addClass("d-none");
    }

    this.limpiarFrmModificar = function () {
        $('#frmEdition')[0].reset();
    }

    this.limpiarFrmCrear = function () {
        $('#frmCreate')[0].reset();
        $("#txtVistas").selectpicker('refresh');
        $("#txtHoteles").selectpicker('refresh');
    }

    this.ValidarInputsModificar = function () {
        if ($("#frmEdition").valid()) {
            this.Update();
        }
    }

    this.ValidarInputsCrear = function () {
        if ($("#frmCreate").valid()) {
            this.Create();
        }
    }
}

//ON DOCUMENT READY
$(document).ready(function () {

    var vroles = new vRoles();
    vroles.RetrieveAll();

    if (window.sessionStorage.getItem("rolesUsuario").includes('GRT')) {
        let usuarioId = window.sessionStorage.getItem("idUsuario");

        $('#txtHoteles option').hide();
        $('#txtHoteles').find('option').filter("option[class = " + usuarioId + "]").show();
    }

    vroles.initializepanel();
    $(".nav-tabs li").click(function () {
        vroles.tab_click($(this));
    });

    ReglasValidacionModificar();
    ReglasValidacionCrear();
});

ReglasValidacionModificar = function () {
    $("#frmEdition").submit(function (e) {
        e.preventDefault();
    }).validate({
        lang: 'es',
        errorClass: "is-invalid",
        rules: {
            txtUNombre: { required: true },
            txtUDescripcion: {  required: true }
        },
        errorPlacement: function (error, element) {
            element: "div";
            $(error).addClass('input-group mb-3');
            error.css({ 'padding-left': '10px', 'margin-right': '20px', 'padding-bottom': '2px', 'color': 'red' });
            error.insertAfter(element);
        }
    });
}

ReglasValidacionCrear = function () {
    $("#frmCreate").submit(function (e) {
        e.preventDefault();
    }).validate({
        ignore: [],
        lang: 'es',
        errorClass: "is-invalid",
        rules: {
            txtCodigo: { required: true },
            txtNombre: { required: true },
            txtDescripcion: { required: true },
            txtVistas: { required: true },
            txtHoteles: { required: true }
        },
        messages: {
            'txtVistas': {
                required: 'Seleccione al menos una vista'
            },
            'txtHoteles': {
                required: 'Seleccione al menos un hotel'
            }
        },
        errorPlacement: function (error, element) {
            element: "div";
            $(error).addClass('input-group mb-3');
            error.css({ 'padding-left': '10px', 'margin-right': '20px', 'padding-bottom': '2px', 'color': 'red' });
            if (element.attr("name") == "txtHoteles") {
                error.insertAfter("#divHoteles");
            } else {
                if (element.attr("name") == "txtVistas") {
                    error.insertAfter("#divVistas");
                } else {
                    error.insertAfter(element);
                }
            }
        }
    });
}