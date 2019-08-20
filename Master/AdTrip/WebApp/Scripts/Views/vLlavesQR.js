function vLlavesQR() {

    this.tblLlavesQRId = 'tblLlavesQR';
    this.service = 'llaveQR';
    this.ctrlActions = new ControlActions();
    //this.columns = "Codigo,Nombre,Descripcion,Estado";

    this.RetrieveAll = function () {
        var DatosSessionReserva = JSON.parse(sessionStorage.getItem("detallereserva"));
        var reserva = DatosSessionReserva["Codigo"];
        this.ctrlActions.FillTable(this.service +"/llavesReserva/"+reserva, this.tblLlavesQRId, false);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblLlavesQRId, true);

    }

    this.Create = function () {
        var data = {};

        //data = this.ctrlActions.GetDataForm('frmCreate');
        //data["IdHotelB"] = "nulo";
        data["EstadoQR"] = "1";
        var DatosSessionReserva = JSON.parse(sessionStorage.getItem("detallereserva"));
        data["IdUsuario"] = sessionStorage.getItem("correoUsuario");
        data["IdReserva"] = DatosSessionReserva["Codigo"];

        var tipo = $("#txtTipo").val();
        if (tipo == "0") {

            data["IdHotelB"] = DatosSessionReserva["IdHotel"];
            this.ctrlActions.PostToAPI(this.service, data, function () {
                var callback = new vLlavesQR();
                callback.ReloadTable();
                $('.nav-tabs li').click();
            });
        } else {
            

            var correo = $("#txtCorreo").val();
            if (correo == "") {
                swal("No se puede continuar", "No ha ingresado el correo electrónico", "error");
            } else {
                data["IdUsuario"] = correo;
                data["IdHotelB"] = DatosSessionReserva["IdHotel"];
                this.ctrlActions.PostToAPI(this.service + "/conCorreo", data, function () {
                    var callback = new vLlavesQR();
                    callback.ReloadTable();
                    $('.nav-tabs li').click();

                    $("#txtCorreo").val() = "";
                });
            }

        }


    }

    this.Update = function () {
        var data = {};
        data["EstadoQR"] = document.getElementById('txtEstado').value;
        data["CodigoQR"] = document.getElementById('txtCodigo').value;
        //data["IdHotelB"] = "nulo";
        var DatosSessionReserva = JSON.parse(sessionStorage.getItem("detallereserva"));
        data["IdHotelB"] = DatosSessionReserva["IdHotel"];
        this.ctrlActions.PutToAPI(this.service, data, function () {
            var callback = new vLlavesQR();
            callback.ReloadTable();
            $('.nav-tabs li').click();
        });
    }

    this.Delete = function () {
        var data = {};
        //data = this.ctrlActions.GetDataForm('frmEdition');
        data["CodigoQR"] = document.getElementById('txtCodigo').value;
        //data["IdHotelB"] = "nulo";
        var DatosSessionReserva = JSON.parse(sessionStorage.getItem("detallereserva"));
        data["IdHotelB"] = DatosSessionReserva["IdHotel"];
        this.ctrlActions.DeleteToAPI(this.service, data, function () {
            var callback = new vLlavesQR();
            callback.ReloadTable();
            $('.nav-tabs li').click();
        });
    }

    this.BindFields = function (data) {
        this.ctrlActions.BindFields('frmEdition', data);
        //NUEVO
        document.getElementById('txtEstado').value = data["EstadoQR"];
        document.getElementById("mngQR").src = data["ImagenQR"];
        this.esconderCrear();
        this.esconderListar();
        this.mostrarModificar();
    }

    this.tab_click = function (ctl) {
        this.tab_handling(ctl);
        div = $(ctl).attr("data-div");

        if (div == "listaLlaveQR") {
            this.mostrarListar();
            this.esconderCrear();
            this.esconderModificar();
        }
        else if (div == "registrarLlaveQR") {
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
        $("#listaLlaveQR").removeClass("d-none");
        $("#listaLlaveQR").addClass("d-inline");
    }

    this.esconderListar = function () {
        $("#listaLlaveQR").removeClass("d-inline");
        $("#listaLlaveQR").addClass("d-none");
    }

    this.mostrarCrear = function () {
        $("#registrarLlaveQR").removeClass("d-none");
        $("#registrarLlaveQR").addClass("d-inline");
    }

    this.esconderCrear = function () {
        $("#registrarLlaveQR").removeClass("d-inline");
        $("#registrarLlaveQR").addClass("d-none");
    }

    this.mostrarModificar = function () {
        $("#modificarLlaveQR").removeClass("d-none");
        $("#modificarLlaveQR").addClass("d-inline");
    }

    this.esconderModificar = function () {
        $("#modificarLlaveQR").removeClass("d-inline");
        $("#modificarLlaveQR").addClass("d-none");
    }

    this.limpiarFormulario = function () {
        $("#frmEdition")[0].reset();
        $("#frmCreate")[0].reset();
    }

    this.ValidarInputsCrear = function () {
        if ($("#frmCreate").valid()) {
            this.Create();
        }
    }

}
$(document).ready(function () {

    var vllaves = new vLlavesQR();
    vllaves.RetrieveAll();

    DatosSessionReserva = JSON.parse(sessionStorage.getItem("detallereserva"));
    var estadoReserva = DatosSessionReserva["Estado"];

    if (estadoReserva == 0) {
        $("#btnCreate").addClass("d-none");
        $("#btnUpdate").addClass("d-none");
    }

    vllaves.initializepanel();
    $(".nav-tabs li").click(function () {
        vllaves.tab_click($(this));
    });

    ReglasValidacionCrear();
});

ReglasValidacionCrear = function () {
    $("#frmCreate").submit(function (e) {
        e.preventDefault();
    }).validate({
        lang: 'es',
        errorClass: "is-invalid",
        rules: {
            txtCorreo: {
                required: false, email: true
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
