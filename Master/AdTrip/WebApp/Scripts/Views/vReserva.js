function vReserva() {

    this.ctrlActions = new ControlActions();
    this.service = 'reserva';




    this.RealizarReserva = function () {
        var listaHab = [];
        var CantHab = [];
        var reservaData = {};
        
        reservaData = this.ctrlActions.GetDataForm('frmReserva');
        reservaData['Codigo'] = generar(9);
        reservaData.IdUsuario = sessionStorage.getItem('idUsuario');
        reservaData.IdHotel = document.getElementById("txtIdHotel").value;

        var allSelect = document.querySelector('#tblTiposHab').querySelectorAll("select");
        var allCant = document.querySelector('#tblTiposHab').querySelectorAll(".cantHab");

        allSelect.forEach(function (item) {
            listaHab.push(item.value);
        });

        allCant.forEach(function (item) {
            CantHab.push(parseInt(item.value));
        })

        reservaData.TipoHabitaciones = listaHab;
        reservaData.CantHabitaciones = CantHab;
        reservaData.Promocion = 'No aplica';

        reservaData["IdHotelB"] = document.getElementById("txtIdHotel").value;
        this.ctrlActions.PostToAPI(this.service, reservaData, function () {
            var vreserva = new vReserva();
            vreserva.limpiarFormularioReserva();
            swal("Reserva realizada", "Su reservación se ha realizado correctamente", "success");
            window.location.href = '/Home/vPerfilHotel/' + document.getElementById("txtIdHotel").value;
       
        });
    };


    this.limpiarFormularioReserva = function() {
        var vperfilhotel = new vPerfilHotel();
        var idHotel = document.getElementById("txtIdHotel").value;

        $('#txtFechaInicio').val('');
        $('#txtFechaFin').val('');
        $('#tblTiposHab tr').remove();
        $('#txtPrecio').val(0);
        $('#input0').val(0);;
        $('#precioHab0').text('');

        if (idHotel != "nulo") {
            this.ctrlActions.GetById("tipoHabitaciones/hotel/" + idHotel, vperfilhotel.ObtenerTiposHabitacion);
        }
    };


    this.ValidarInputs = function () {
        $('#add-card').hide();
        let lstInputs = document.querySelectorAll("#frmReserva input[ColumnDataName]");
        let cont = 0;
        for (ind = 0; ind < lstInputs.length; ind++) {
            if (lstInputs[ind].value != "") {
                lstInputs[ind].classList.remove("is-invalid");
                cont++;
            } else {
                lstInputs[ind].classList.add("is-invalid");
            }
        }
        if (cont == lstInputs.length) {
           
            var precio = $('#txtPrecio').val();
            this.ValidarHabs();
            
           
        } else {
            swal("Campos Requeridos", "Campos vacios: Por favor, llenar todos los campos", "warning");
        }
    }


    this.ValidarCantPersonas = function () {
        var listaHab = [];
        var CantHab = [];
        var reservaData = {};
        reservaData = this.ctrlActions.GetDataForm('frmReserva');
        reservaData['Codigo'] = generar(9);
        reservaData.IdUsuario = sessionStorage.getItem('idUsuario');
        reservaData.IdHotel = document.getElementById("txtIdHotel").value;

        reservaData.CantPersonas = $('#txtHuespedes').val();

        reservaData.TipoHabitaciones = listaHab;
        reservaData.CantHabitaciones = CantHab;
        reservaData.Promocion = 'No aplica';


        var allSelect = document.querySelector('#tblTiposHab').querySelectorAll("select");
        var allCant = document.querySelector('#tblTiposHab').querySelectorAll(".cantHab");

        allSelect.forEach(function (item) {
            listaHab.push(item.value);
        });

        allCant.forEach(function (item) {
            CantHab.push(parseInt(item.value));
        })

        this.ctrlActions.PostToAPI('reserva/valcantpersonas', reservaData, function () {
            $('#add-card').show();
            var vreserva = new vReserva();
            vreserva.CargarDatosPopUp();
        });

    }


    this.ValidarHabs = function () {
        var listaHab = [];
        var CantHab = [];
        var reservaData = {};
        reservaData = this.ctrlActions.GetDataForm('frmReserva');
        reservaData['Codigo'] = generar(9);
        reservaData.IdUsuario = sessionStorage.getItem('idUsuario');
        reservaData.IdHotel = document.getElementById("txtIdHotel").value;

        var allSelect = document.querySelector('#tblTiposHab').querySelectorAll("select");
        var allCant = document.querySelector('#tblTiposHab').querySelectorAll(".cantHab");

        allSelect.forEach(function (item) {
            listaHab.push(item.value);
        });

        allCant.forEach(function (item) {
            CantHab.push(parseInt(item.value));
        })

        reservaData.TipoHabitaciones = listaHab;
        reservaData.CantHabitaciones = CantHab;
        reservaData.Promocion = 'No aplica';

           
        this.ctrlActions.PostToAPI(this.service + '/validarhabs', reservaData, function () {
            var vreserva = new vReserva();
            vreserva.ValidarCantPersonas();
        });
    }

    this.IrInicioSesion = function () {
        window.location.href = '/Home/vLogin';
    }

    this.CargarDatosPopUp = function () {

        var precio = $('#txtPrecio').val();

        $('#precioTotal').append('$' + precio)
        Pago(precio);
        $('#btnReservar').prop('disabled', true);


        function Pago(total) {
            paypal.Button.render({
                // Configure environment
                env: 'sandbox',
                client: {
                    sandbox: 'AXgCkOz_JWQZPDVMFwqQZuroSlMI1LMHLdS78A2CmD1VgqB2ikwjAanRpzKwpUzoFHS_jspaN-LKBTL7',
                    production: 'demo_production_client_id'
                },
                // Customize button (optional)
                locale: 'es_CR',
                style: {
                    size: 'medium',
                    color: 'blue',
                    shape: 'pill',
                    height: 40
                },

                // Enable Pay Now checkout flow (optional)
                commit: true,

                // Set up a payment
                payment: function (data, actions) {
                    return actions.payment.create({
                        transactions: [{
                            amount: {
                                total: total,
                                currency: 'USD'
                            }
                        }]
                    });
                },
                // Execute the payment
                onAuthorize: function (data, actions) {
                    return actions.payment.execute().then(function () {
                        // Show a confirmation message to the buyer
                       
                        var vreserva = new vReserva();
                        vreserva.RealizarReserva();

                    });
                }
            }, '#paypal-button');
        };

    }
}





$(document).ready(function () {
    var reserva = new vReserva();
    

    if (sessionStorage.getItem('idUsuario') != null) {
        $('#IrInicio').hide();
        $('#formReserva').show();
    } else {
        $('#IrInicio').show();
        $('#formReserva').hide();
    }



    $('.datepicker').click(function () {
        var CantHab = [];
        let fInicio = new Date($('#txtFechaInicio').val());
        let fFin = new Date($('#txtFechaFin').val());
        var diasdif = fFin.getTime() - fInicio.getTime();
        var contdias = Math.round(diasdif / (1000 * 60 * 60 * 24));

        var allCant = document.querySelector('#tblTiposHab').querySelectorAll("input");

        allCant.forEach(function (item) {
            CantHab.push(parseInt(item.value));
        })
        let totalPagar = 0;

        for (var i in CantHab) {
            let valor = $('#precioHab' + i).text();
            let precio = valor.substring(1, valor.length);
            let cantHab = CantHab[i];
            totalPagar = totalPagar + (precio * cantHab * contdias);
        }

        if (isNaN(totalPagar)) {

        } else {
            $('#txtPrecio').val(totalPagar);
           
        }
        $('#0').attr('disabled', false);
    })


    
})

function generar(longitud) {
    var caracteres = "012346789";
    var codigo = "";
    for (i = 0; i < longitud; i++) codigo += caracteres.charAt(Math.floor(Math.random() * caracteres.length));
    return codigo;
}

var inputFechaInicio = document.getElementById('txtFechaInicio');
var inputFechaFin = document.getElementById('txtFechaFin');



