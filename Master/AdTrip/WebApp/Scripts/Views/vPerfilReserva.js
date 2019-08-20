function vPerfilReserva() {

    this.service = 'reserva';
    this.ctrlActions = new ControlActions();
    var DatosSessionReserva = {};
    DatosSessionReserva = JSON.parse(sessionStorage.getItem("detallereserva"));
    this.codigoHab = 0;

    this.CargarReserva = function () {

        const formatter = new Intl.NumberFormat('en-US', {
            style: 'currency',
            currency: 'USD',
            minimumFractionDigits: 0
        })

        var meses = ['enero', 'febrero', 'marzo', 'abril', 'mayo', 'junio', 'julio', 'agosto', 'setiembre', 'octubre', 'noviembre', 'diciembre'];

        var fechaInicio = new Date(DatosSessionReserva["FechaInicio"]);
        var fechaFin = new Date(DatosSessionReserva["FechaFin"]);

        var anno = fechaInicio.getFullYear();
        var mes = fechaInicio.getMonth();
        var dia = fechaInicio.getDate();

        $('#fechaInicio').append(dia + ' de ' + meses[mes] + ' de ' + anno);
        $('#codigo').append(DatosSessionReserva["Codigo"]);



        this.ctrlActions.GetById('hotel/' + DatosSessionReserva["IdHotel"], function (infoHotel) 
        {
            $('#direccion').append(infoHotel["Direccion"]);
            $('#telefonos').append(infoHotel["Telefonos"]);
            $('#correoHotel').append(infoHotel["Correo"]);
            $('#nombreHotel').append(infoHotel["Nombre"]);
        });

        this.ctrlActions.GetById('reserva/obtenerInfoHabs/' + DatosSessionReserva["Codigo"], function (infoHabs) {

            for (var i = 0; i < infoHabs.length; i++) {
                var count = 0;
                var control = new ControlActions();
                this.codigoHab = infoHabs[i]["IdHabitacion"];
                control.GetById('tipoHabitaciones/' + infoHabs[i]["TipoHab"], function (data) {
                    var vperfilreserva = new vPerfilReserva();
                    vperfilreserva.ObtenerHabitaciones(data, infoHabs[count]["IdHabitacion"]);
                    count++;
                });
            }
        });

        $('#cantPersonas').append(DatosSessionReserva["CantPersonas"]);

        $('#fechaFin').append(fechaFin.getDate() + ' de ' + meses[fechaFin.getMonth()] + ' de ' + fechaFin.getFullYear());
        $('#idHotel').append(DatosSessionReserva["IdHotel"]);

        $('#precioReserva').append(formatter.format(DatosSessionReserva["Precio"]));


    }

    this.ObtenerHabitaciones = function (dataTipo, idHabs) {

        options = {
            hour: 'numeric', minute: 'numeric',
            hour12: true,
            timeZone: 'America/Costa_Rica',
        };


        var tiempoIn = new Date(dataTipo.HoraCheckIn);
        var tiempoOut = new Date(dataTipo.HoraCheckOut);

        let habitacion = '<div style="padding: 2% 0">' +
                        '  <ul class="list-unstyled booking-info">' +
                        '     <li style="margin-bottom:1%"><span style="margin-right:1%;">Número de habitación:    </span>' + idHabs + ' <span class="line" style="margin-right:2%; margin-left:2%;">   |   </span> <span style="margin-right:1%;">Horas de check:</span> Hora de check-in: <span style="margin-right:2%; margin-left:1%;">' + new Intl.DateTimeFormat('es-CR', options).format(tiempoIn) + '</span> <span class="line" style="margin-right:2%;"> | </span> Hora de check-out: <span style="margin-right:1%; margin-left: 1%">' + new Intl.DateTimeFormat('es-CR', options).format(tiempoOut) + '</span></li>' +
                        '     <li><span style="margin-right:1%; margin-left:2%;">Capacidad máxima de personas: </span>' + dataTipo.CupoMax + '  <span class="line" style="margin-right:2%; margin-left:2%;">   |   </span><span style="margin-right:1%;"> Tipo de habitación: </span>' + dataTipo.Nombre + '</li>' +
                        '  </ul>' +
                        '</div>';

        $('#infoHabs').append(habitacion);
    }

    this.ManejarPuerta = function() {
        if (DatosSessionReserva["Estado"] == '1') {
            $('#btnPuerta').text('Entrar');
            $('#btnCheckout').show();
        }

        if (DatosSessionReserva["Estado"] == '2') {
            $('#btnPuerta').text('Check in');
            $('#btnCheckout').hide();
        }

        if (DatosSessionReserva["Estado"] == '0') {
            $('#btnPuerta').hide();
            $('#btnCheckout').hide();
        }
    }

    this.serviceQR = 'llaveQR/';

    $('#btnPuerta').click(function (event) {
        Puerta();

        event.preventDefault();
    });

    $('#btnCheckout').click(function (event) {
        Checkout();

        event.preventDefault();
    });

    this.RetrieveQR = function (llaveId) {
        var data = {};
        data['idQR'] = llaveId;
        data['idHabitacion'] = $('#idHabitacion').val();
        data['idHotel'] = DatosSessionReserva["IdHotel"];
        data['Codigo'] = DatosSessionReserva["Codigo"];

        this.ctrlActions.GetLlaveQR(this.serviceQR + 'checkIn', data, function (content) {
            swal("Felicidades!", content, "success");

            setTimeout(function () {
                window.location.href = "/Home/vPerfilUsuario";
            }, 1000);
        });
    }

    this.StartCheckout = function (llaveId) {
        var data = {};
        data['idQR'] = llaveId;
        //data['idHabitacion'] = $('#idHabitacion').val();
        data['idHotel'] = DatosSessionReserva["IdHotel"];
        data['Codigo'] = DatosSessionReserva["Codigo"];

        sessionStorage.removeItem("llaveQRCarrito");
        sessionStorage.setItem("llaveQRCarrito", llaveId);

        setTimeout(function () {
            window.location.href = "/Home/vCarrito";
        }, 1000);
    }
}

function Puerta() {
    var vperfilreserva = new vPerfilReserva();

    let scanner = new Instascan.Scanner(
        {
            video: document.getElementById('preview')
        }
    );
    scanner.addListener('scan', function (content) {
        vperfilreserva.RetrieveQR(content);
        scanner.stop()
        //window.open(content, "_blank");
    });
    Instascan.Camera.getCameras().then(cameras => {
        if (cameras.length > 0) {
            scanner.start(cameras[0]);
        } else {
            console.error("¡No existen cámaras en el dispositivo!");
        }
    });
}

function Checkout() {
    var vperfilreserva = new vPerfilReserva();

    let scanner = new Instascan.Scanner(
        {
            video: document.getElementById('preview')
        }
    );
    scanner.addListener('scan', function (content) {
        vperfilreserva.StartCheckout(content);
        scanner.stop()
        //window.open(content, "_blank");
    });
    Instascan.Camera.getCameras().then(cameras => {
        if (cameras.length > 0) {
            scanner.start(cameras[0]);
        } else {
            console.error("¡No existen cámaras en el dispositivo!");
        }
    });
}


$(document).ready(function () {
    var vperfilreserva = new vPerfilReserva();

    vperfilreserva.CargarReserva();

    vperfilreserva.ManejarPuerta();
})