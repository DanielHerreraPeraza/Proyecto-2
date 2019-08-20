function vImpuesto() {

    this.tblImpuestoId = 'tblImpuesto';
    this.service = 'impuesto';
    this.ctrlActions = new ControlActions();
    this.columns = "Codigo,Nombre,Porcentaje,Descripcion,Estado";

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblImpuestoId, false);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblImpuestoId, true);
        this.limpiarFormulario();
    }

    this.Create = function () {
        var impuestoData = {};
        impuestoData = this.ctrlActions.GetDataForm('frmCreate');
        this.ctrlActions.PostToAPI(this.service, impuestoData, function () {
            var callback = new vImpuesto();
            callback.ReloadTable();
            $('.nav-tabs li').click();
        });
    }

    this.Update = function () {

        var impuestoData = {};
        impuestoData = this.ctrlActions.GetDataForm('frmEdition');
        impuestoData.Estado = document.getElementById('txtEstado').value;

        this.ctrlActions.PutToAPI(this.service, impuestoData, function () {
            var callback = new vImpuesto();
            callback.ReloadTable();
        });

        $('.nav-tabs li').click();

    }

    this.Delete = function () {

        var impuestoData = {};
        impuestoData = this.ctrlActions.GetDataForm('frmEdition');
        this.ctrlActions.DeleteToAPI(this.service, impuestoData, function () {
            var callback = new vImpuesto();
            callback.ReloadTable();
        });
    }



    this.BindFields = function (data) {
        this.ctrlActions.BindFields('frmEdition', data);

        //NUEVO
        if (data.IdEstado == "0") {
            $("#txtEstado option[value= 0 ]").prop('selected', true);
        } else if (data.IdEstado == "1") {
            $("#txtEstado option[value= 1 ]").prop('selected', true);
        }

        this.esconderCrear();
        this.esconderListar();
        this.mostrarModificar();
    }

    this.tab_click = function (ctl) {
        this.tab_handling(ctl);

        div = $(ctl).attr("data-div");

        if (div == "listaImpuesto") {
            this.mostrarListar();
            this.esconderCrear();
            this.esconderModificar();
        }
        else if (div == "registrarImpuesto") {
            this.mostrarCrear();
            this.esconderListar();
            this.esconderModificar();
            this.limpiarFormulario();
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
        $("#listaImpuesto").removeClass("d-none");
        $("#listaImpuesto").addClass("d-inline");
    }

    this.esconderListar = function () {
        $("#listaImpuesto").removeClass("d-inline");
        $("#listaImpuesto").addClass("d-none");
    }

    this.mostrarCrear = function () {
        $("#crearImpuesto").removeClass("d-none");
        $("#crearImpuesto").addClass("d-inline");
    }

    this.esconderCrear = function () {
        $("#crearImpuesto").removeClass("d-inline");
        $("#crearImpuesto").addClass("d-none");
    }

    this.mostrarModificar = function () {
        $("#modificarImpuesto").removeClass("d-none");
        $("#modificarImpuesto").addClass("d-inline");
    }

    this.esconderModificar = function () {
        $("#modificarImpuesto").removeClass("d-inline");
        $("#modificarImpuesto").addClass("d-none");
    }

    this.limpiarFormulario = function () {
        $("#frmEdition")[0].reset();
        $("#frmCreate")[0].reset();
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

$(document).ready(function () {

    var vimpuestos = new vImpuesto();
    vimpuestos.RetrieveAll();

    vimpuestos.initializepanel();
    $(".nav-tabs li").click(function () {
        vimpuestos.tab_click($(this));
    });

    document.querySelector("#txtPorcentaje").setAttribute("min", "0");
    document.querySelector("#txtPorcentaje").setAttribute("max", "99");
    document.querySelector("#txtPorcentajeR").setAttribute("min", "0");
    document.querySelector("#txtPorcentajeR").setAttribute("max", "99");

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
            txtCodigo: {
                required: true, digits: true, number: true
            },
            txtNombre: {
                required: true
            },
            txtPorcentaje: {
                required: true, number: true, digits: true 
            },
            txtDescripcion: {
                required: true
            }

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
        lang: 'es',
        errorClass: "is-invalid",
        rules: {
            txtCodigo: {
                required: true, digits: true, number: true
            },
            txtNombreR: {
                required: true
            },
            txtPorcentajeR: {
                required: true, number: true, digits: true 
            },
            txtDescripcionR: {
                required: true
            }
        },
        errorPlacement: function (error, element) {
            element: "div";
            $(error).addClass('input-group mb-3');
            error.css({ 'padding-left': '10px', 'margin-right': '20px', 'padding-bottom': '2px', 'color': 'red' });
            error.insertAfter(element);
        }
    });
}



