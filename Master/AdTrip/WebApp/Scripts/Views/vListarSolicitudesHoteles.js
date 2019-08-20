
function vListarSolicitudesHoteles() {

    this.tblSolicitudesId = 'tblSolicitudesHoteles';
    this.service = 'solicitudHotel';
    this.ctrlActions = new ControlActions();
    //this.columns = "Id,Name,LastName,Age";

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblSolicitudesId, false);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblSolicitudesId, true);
    }

    this.Update = function () {

        var solicitudData = {};
        solicitudData = this.ctrlActions.GetDataForm('frmEdition');

        this.ctrlActions.PutToAPI(this.service, solicitudData, function () {
            var callback = new vListarSolicitudesHoteles();
            callback.ReloadTable();
            $('.nav-tabs li').click();
        });
    }

    this.AprobarSolicitud = function () {

        if (document.querySelector("#txtMembrecia").value > 0) {
            document.querySelector("#txtEstado").value = "Aprobada";

            //$('#txtEstado').val() = "Aprobada";
            var solicitudData = {};
            solicitudData = this.ctrlActions.GetDataForm('frmEdition');
            //Hace el post al create
            this.ctrlActions.PutToAPI(this.service, solicitudData, function () {
                var callback = new vListarSolicitudesHoteles();
                callback.ReloadTable();
                callback.limpiar();
                $('.nav-tabs li').click();
            });
            
        } else {
            swal("No se puede continuar", "Ingrese la membresía para poder aprobar la solicitud", "warning");
 
        }
    }

    this.RechazarSolicitud = function () {

        document.querySelector("#txtEstado").value = "Rechazada";

        //$('#txtEstado#').val() = "Rechazada";

        var solicitudData = {};
        solicitudData = this.ctrlActions.GetDataForm('frmEdition');
        //Hace el post al create
        this.ctrlActions.PutToAPI(this.service, solicitudData, function () {
            var callback = new vListarSolicitudesHoteles();
            callback.limpiar();
            callback.ReloadTable();
            $('.nav-tabs li').click();
        });
    }

    this.Delete = function () {
        var customerData = {};
        customerData = this.ctrlActions.GetDataForm('frmEdition');
        //Hace el post al create
        this.ctrlActions.DeleteToAPI(this.service, customerData, function () {
            var callback = new vListarSolicitudesHoteles();
            callback.ReloadTable();
        });
    }

    this.BindFields = function (data) {
        this.ctrlActions.BindFields('frmEdition', data);
        this.esconderListar();
        this.mostrarModificar();

    }
    this.limpiar = function () {
        $("#frmEdition")[0].reset();
    }

    this.tab_click = function (ctl) {
        this.tab_handling(ctl);

        div = $(ctl).attr("data-div");

        if (div == "listarSolicitudes") {
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
        $("#listarSolicitudes").removeClass("d-none");
        $("#listarSolicitudes").addClass("d-inline");
    }

    this.esconderListar = function () {
        $("#listarSolicitudes").removeClass("d-inline");
        $("#listarSolicitudes").addClass("d-none");
    }

    this.mostrarModificar = function () {
        $("#modificarSolicitudes").removeClass("d-none");
        $("#modificarSolicitudes").addClass("d-inline");
    }

    this.esconderModificar = function () {
        $("#modificarSolicitudes").removeClass("d-inline");
        $("#modificarSolicitudes").addClass("d-none");
    }

}

//ON DOCUMENT READY
$(document).ready(function () {

    var vlistarsolicitudeshoteles = new vListarSolicitudesHoteles();
    vlistarsolicitudeshoteles.RetrieveAll();

    vlistarsolicitudeshoteles.initializepanel();
    $(".nav-tabs li").click(function () {
        vlistarsolicitudeshoteles.tab_click($(this));
    });
});
