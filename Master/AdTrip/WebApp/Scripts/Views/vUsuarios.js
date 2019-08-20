function vUsuarios() {
    this.tblUsuariosId = 'tblUsuarios';
    this.service = 'usuario';
    this.ctrlActions = new ControlActions();
    this.columns = "Identificacion,PrimerNombre,PrimerApellido,SegundoApellido,Estado";

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblUsuariosId, false);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblUsuariosId, true);
    }

    this.Create = function () {
        var rolData = {};
        rolData = this.ctrlActions.GetDataForm('frmCreate');

        this.ctrlActions.PostToAPI(this.service, rolData, function () {
            var callback = new vUsuarios();
            callback.ReloadTable();
            $('.nav-tabs li').click();
        });
    }

    this.Update = function () {
        var rolData = {};
        rolData = this.ctrlActions.GetDataForm('frmEdition');

        this.ctrlActions.PutToAPI(this.service, rolData, function () {
            var callback = new vUsuarios();
            callback.ReloadTable();
            callback.limpiarFormulario();
            $('.nav-tabs li').click();
        });
    }

    this.Delete = function () {
        var rolData = {};
        rolData = this.ctrlActions.GetDataForm('frmEdition');

        this.ctrlActions.DeleteToAPI(this.service, rolData, function () {
            var callback = new vUsuarios();
            callback.ReloadTable();
        });
    }

    this.BindFields = function (data) {
        this.ctrlActions.BindFields('frmEdition', data);
        this.esconderListar();
        this.mostrarModificar();
    }

    this.tab_click = function (ctl) {
        this.tab_handling(ctl);

        div = $(ctl).attr("data-div");

        if (div == "listaUsuarios") {
            this.mostrarListar();
            this.esconderModificar();
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
        $("#listaUsuarios").removeClass("d-none");
        $("#listaUsuarios").addClass("d-inline");
    }

    this.esconderListar = function () {
        $("#listaUsuarios").removeClass("d-inline");
        $("#listaUsuarios").addClass("d-none");
    }

    this.mostrarModificar = function () {
        $("#modificarUsuarios").removeClass("d-none");
        $("#modificarUsuarios").addClass("d-inline");
    }

    this.esconderModificar = function () {
        $("#modificarUsuarios").removeClass("d-inline");
        $("#modificarUsuarios").addClass("d-none");
    }

    this.limpiarFormulario = function () {
        $("#frmEdition")[0].reset();
    }
}

//ON DOCUMENT READY
$(document).ready(function () {

    var vusuarios = new vUsuarios();
    vusuarios.RetrieveAll();

    $("#txtEstado option[value=" + '2' + "]").hide();
    $("#txtEstado option[value=" + '3' + "]").hide();

    vusuarios.initializepanel();
    $(".nav-tabs li").click(function () {
        vusuarios.tab_click($(this));
    });

    $('#txtCanton').attr("disabled", true);
    $('#txtDistrito').attr("disabled", true);

});
