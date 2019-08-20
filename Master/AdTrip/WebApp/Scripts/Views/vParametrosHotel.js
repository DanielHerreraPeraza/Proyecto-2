function vParametrosHotel() {
    this.service = 'parametrizablesHotel';
    this.ctrlActions = new ControlActions();
    this.hotel;

    this.Update = function () {
        let hotelData = {};
        let id = document.getElementById("txtIdHotel").value;
        hotelData = this.ctrlActions.GetDataForm('frmEdition');
        hotelData["IdHotelB"] = id;
        this.ctrlActions.PutToAPI(this.service, hotelData,
            setTimeout(function redirection() { window.location.href = '/Home/vPerfilHotel/' + id }, 3000));
    }
    this.CargarParametros = function (data) {
        if (data != null) {

            document.getElementById("txtComision").value = data['Comision'];

            if (window.sessionStorage.getItem("rolesUsuario") == null || window.sessionStorage.getItem("rolesUsuario").includes('ADM')==false) {
                document.getElementById("txtComision").setAttribute('disabled', '');
            }
            document.getElementById("txtMensaje").value = data['Mensaje'];    

            document.getElementById("txtDescripcion").value = data['Politica'];
            document.getElementById("txtPorcentaje").value = data['Porciento'];
            document.getElementById("txtDias").value = data['Dias'];


        } else {
            swal("Error de datos", "El hotel no existe", "error");

            setTimeout(function redirection() { window.location.href = '/Home/vHoteles/'; }, 3000);
        }

    }

    this.ObtenerParametros = function () {
        var id = document.getElementById("txtIdHotel").value;
        if (id != "nulo") {
            this.ctrlActions.GetById(this.service+"/" + id, this.CargarParametros);
        }

    }

    this.ValidarInputs = function () {
        if ($("#frmEdition").valid()) {
            this.Update();
        } else {

        }
    }

    this.volverPerfil = function () {
        var id = document.getElementById("txtIdHotel").value;
        window.location.href = '/Home/vPerfilHotel/' + id;
    };

}

//ON DOCUMENT READY

$(document).ready(function () {
    var vparametrosHotel = new vParametrosHotel();

    ReglasValidacion();
    var element = document.getElementById("Hotel");
    element.classList.remove("d-none");
    var elementC = document.getElementById("HotelC");
    elementC.classList.remove("d-none");

});

ReglasValidacion = function () {
    $("#frmEdition").submit(function (e) {
        e.preventDefault();
    }).validate({
        lang: 'es',
        errorClass: "is-invalid",
        rules: {
            txtIdHotel: { required: true },
            txtComision: { required: true , max: 100, min: 0},
            txtMensaje: { required: true },
            txtDescripcion: { required: false },
            txtPorcentaje: { required: true, max: 100, min: 0},
            txtDias: { required: true, digits: true, min: 1}          

        },
        errorPlacement: function (error, element) {
            element: "div";
            $(error).addClass('input-group mb-3');
            error.css({ 'padding-left': '10px', 'margin-right': '20px', 'padding-bottom': '2px', 'color': 'red' });
            error.insertAfter(element);
        }
    });
}