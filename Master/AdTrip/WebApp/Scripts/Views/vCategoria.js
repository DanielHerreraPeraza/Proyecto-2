function vCategoria() {

    this.tblCategoriaId = 'tblCategoria';
    this.service = 'categoria';
    this.ctrlActions = new ControlActions();
    this.columns = "Codigo,Nombre,Descripcion,Estado";

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblCategoriaId, false);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblCategoriaId, true);
        this.limpiarFormulario();
    }

    this.Create = function () {
        var categoriaData = {};
        categoriaData = this.ctrlActions.GetDataForm('frmCreate');
        categoriaData["IdHotelB"] = "nulo";
        this.ctrlActions.PostToAPI(this.service, categoriaData, function () {
            var callback = new vCategoria();
            callback.ReloadTable();
            $('.nav-tabs li').click();
        });
        
    }

    this.Update = function () {
        var categoriaData = {};
        categoriaData = this.ctrlActions.GetDataForm('frmEdition');
        categoriaData.Estado = document.getElementById('txtEstado').value;
        categoriaData["IdHotelB"] = "nulo";
        this.ctrlActions.PutToAPI(this.service, categoriaData, function () {
            var callback = new vCategoria();
            callback.ReloadTable();
            $('.nav-tabs li').click();
        });
    }

    this.Delete = function () {
        var categoriaData = {};
        categoriaData = this.ctrlActions.GetDataForm('frmEdition');
        categoriaData["IdHotelB"] = "nulo";
        this.ctrlActions.DeleteToAPI(this.service, categoriaData, function () {
            var callback = new vCategoria();
            callback.ReloadTable();
            $('.nav-tabs li').click();
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

        if (div == "listaCategoria") {
            this.mostrarListar();
            this.esconderCrear();
            this.esconderModificar();
        }
        else if (div == "registrarCategoria") {
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
        $("#listaCategoria").removeClass("d-none");
        $("#listaCategoria").addClass("d-inline");
    }

    this.esconderListar = function () {
        $("#listaCategoria").removeClass("d-inline");
        $("#listaCategoria").addClass("d-none");
    }

    this.mostrarCrear = function () {
        $("#crearCategoria").removeClass("d-none");
        $("#crearCategoria").addClass("d-inline");
    }

    this.esconderCrear = function () {
        $("#crearCategoria").removeClass("d-inline");
        $("#crearCategoria").addClass("d-none");
    }

    this.mostrarModificar = function () {
        $("#modificarCategoria").removeClass("d-none");
        $("#modificarCategoria").addClass("d-inline");
    }

    this.esconderModificar = function () {
        $("#modificarCategoria").removeClass("d-inline");
        $("#modificarCategoria").addClass("d-none");
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

    var vcategorias = new vCategoria();
    vcategorias.RetrieveAll();

    vcategorias.initializepanel();
    $(".nav-tabs li").click(function () {
        vcategorias.tab_click($(this));
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
            txtNombre: {
                required: true
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
            txtNombre: {
                required: true
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
