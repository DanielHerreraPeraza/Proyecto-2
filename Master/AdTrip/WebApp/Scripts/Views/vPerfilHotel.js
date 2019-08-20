function vPerfilHotel() {
	this.service = 'hotel';
	this.ctrlActions = new ControlActions();
	this.DatosHotel;

	this.CargarHotel = function (data) {
		if (data != null) {
			DatosHotel = data;
			sessionStorage.removeItem("idHotelLogeado");
			sessionStorage.setItem("idHotelLogeado", data['CedulaJuridica']);

			document.getElementById("P_mngFoto").src = data['FotoPerfil'];
			document.getElementById("P_NombreHotel").innerText = data['Nombre'];
			$('#P_CedulaJ').append(data['CedulaJuridica']);

			$('#P_Cadena').append(data['Cadena']);

            $('#P_Correo').append(data['Correo']);
            $('#P_Telefonos').append(data['Telefonos']);


			$('#P_sitio').append(data['URLSitio']);

			//document.getElementById("txtUbicacionX").value = ""+
			//document.getElementById("txtUbicacionY").value = ""+

			let clasi = data['Clasificacion'];
			if (clasi == 1) {
				$('#P_Clasificacion').html('<li class="list-inline-item rating" id="P_lista_Clasificacion">'
					+ ' <span><i class="fa fa-star"></i></span> Clasificación internacional:'
					+ ' <span><i class="fa fa-star orange"></i></span>'
					+ ' <span><i class="fa fa-star lightgrey"></i></span>'
					+ ' <span><i class="fa fa-star lightgrey"></i></span>'
					+ ' <span><i class="fa fa-star lightgrey"></i></span>'
					+ ' <span><i class="fa fa-star lightgrey"></i></span>'
					+ '</li> ');

			} else if (clasi == 2) {
				$('#P_Clasificacion').html('<li class="list-inline-item rating" id="P_lista_Clasificacion">'
					+ ' <span><i class="fa fa-star"></i></span> Clasificación internacional:'
					+ ' <span><i class="fa fa-star orange"></i></span>'
					+ ' <span><i class="fa fa-star orange"></i></span>'
					+ ' <span><i class="fa fa-star lightgrey"></i></span>'
					+ ' <span><i class="fa fa-star lightgrey"></i></span>'
					+ ' <span><i class="fa fa-star lightgrey"></i></span>'
					+ '</li> ');

			} else if (clasi == 3) {
				$('#P_Clasificacion').html('<li class="list-inline-item rating" id="P_lista_Clasificacion">'
					+ ' <span><i class="fa fa-star"></i></span> Clasificación internacional:'
					+ ' <span><i class="fa fa-star orange"></i></span>'
					+ ' <span><i class="fa fa-star orange"></i></span>'
					+ ' <span><i class="fa fa-star orange"></i></span>'
					+ ' <span><i class="fa fa-star lightgrey"></i></span>'
					+ ' <span><i class="fa fa-star lightgrey"></i></span>'
					+ '</li> ');

			} else if (clasi == 4) {
				$('#P_Clasificacion').html('<li class="list-inline-item rating" id="P_lista_Clasificacion">'
					+ ' <span><i class="fa fa-star"></i></span> Clasificación internacional:'
					+ ' <span><i class="fa fa-star orange"></i></span>'
					+ ' <span><i class="fa fa-star orange"></i></span>'
					+ ' <span><i class="fa fa-star orange"></i></span>'
					+ ' <span><i class="fa fa-star orange"></i></span>'
					+ ' <span><i class="fa fa-star lightgrey"></i></span>'
					+ '</li> ');

			} else {

				$('#P_Clasificacion').html('<li class="list-inline-item rating" id="P_lista_Clasificacion">'
					+ ' <span><i class="fa fa-star"></i></span> Clasificación internacional:'
					+ ' <span><i class="fa fa-star orange"></i></span>'
					+ ' <span><i class="fa fa-star orange"></i></span>'
					+ ' <span><i class="fa fa-star orange"></i></span>'
					+ ' <span><i class="fa fa-star orange"></i></span>'
					+ ' <span><i class="fa fa-star orange"></i></span>'
					+ '</li> ');

			}


            let cali = data['PromCalificacion'];

            if (cali == 0) {
                $('#P_Votos').html('<li class="list-inline-item rating" id="P_lista_votos">'
                    + ' <span><i class="fa fa-star"></i></span> Voto de usuarios:'
                    + ' <span><i class="fa fa-star lightgrey"></i></span>'
                    + ' <span><i class="fa fa-star lightgrey"></i></span>'
                    + ' <span><i class="fa fa-star lightgrey"></i></span>'
                    + ' <span><i class="fa fa-star lightgrey"></i></span>'
                    + ' <span><i class="fa fa-star lightgrey"></i></span>'
                    + '</li> ');

            } else if (cali == 1) {
                $('#P_Votos').html('<li class="list-inline-item rating" id="P_lista_votos">'
                    + ' <span><i class="fa fa-star"></i></span> Voto de usuarios:'
                    + ' <span><i class="fa fa-star orange"></i></span>'
                    + ' <span><i class="fa fa-star lightgrey"></i></span>'
                    + ' <span><i class="fa fa-star lightgrey"></i></span>'
                    + ' <span><i class="fa fa-star lightgrey"></i></span>'
                    + ' <span><i class="fa fa-star lightgrey"></i></span>'
                    + '</li> ');

            } else if (cali == 2) {
                $('#P_Votos').html('<li class="list-inline-item rating" id="P_lista_votos">'
                    + ' <span><i class="fa fa-star"></i></span> Voto de usuarios:'
                    + ' <span><i class="fa fa-star orange"></i></span>'
                    + ' <span><i class="fa fa-star orange"></i></span>'
                    + ' <span><i class="fa fa-star lightgrey"></i></span>'
                    + ' <span><i class="fa fa-star lightgrey"></i></span>'
                    + ' <span><i class="fa fa-star lightgrey"></i></span>'
                    + '</li> ');

            } else if (cali == 3) {
                $('#P_Votos').html('<li class="list-inline-item rating" id="P_lista_votos">'
                    + ' <span><i class="fa fa-star"></i></span> Voto de usuarios:'
                    + ' <span><i class="fa fa-star orange"></i></span>'
                    + ' <span><i class="fa fa-star orange"></i></span>'
                    + ' <span><i class="fa fa-star orange"></i></span>'
                    + ' <span><i class="fa fa-star lightgrey"></i></span>'
                    + ' <span><i class="fa fa-star lightgrey"></i></span>'
                    + '</li> ');

            } else if (cali == 4) {
                $('#P_Votos').html('<li class="list-inline-item rating" id="P_lista_votos">'
                    + ' <span><i class="fa fa-star"></i></span> Voto de usuarios:'
                    + ' <span><i class="fa fa-star orange"></i></span>'
                    + ' <span><i class="fa fa-star orange"></i></span>'
                    + ' <span><i class="fa fa-star orange"></i></span>'
                    + ' <span><i class="fa fa-star orange"></i></span>'
                    + ' <span><i class="fa fa-star lightgrey"></i></span>'
                    + '</li> ');

            } else {

                $('#P_Votos').html('<li class="list-inline-item rating" id="P_lista_votos">'
                    + ' <span><i class="fa fa-star"></i></span> Voto de usuarios:'
                    + ' <span><i class="fa fa-star orange"></i></span>'
                    + ' <span><i class="fa fa-star orange"></i></span>'
                    + ' <span><i class="fa fa-star orange"></i></span>'
                    + ' <span><i class="fa fa-star orange"></i></span>'
                    + ' <span><i class="fa fa-star orange"></i></span>'
                    + '</li> ');

            }


			$('#P_Descripcion').append(data['Descripcion']);
			$('#P_Provincia').append(data['vProvincia']);
			$('#P_Canton').append(data['vCanton']);
			$('#P_Distrito').append(data['vDistrito']);
			$('#P_Direccion').append(data['Direccion']);

			let estado = data['Estado']

			if (estado == "Activo") {
				$('#btnCambioEstado').removeClass('btn-estadoCustom');
				$('#btnCambioEstado').removeClass('btn-danger');
				$('#btnCambioEstado').addClass('btn-success');
				$('#btnCambioEstado').text('Activo')
			} else {
				$('#btnCambioEstado').removeClass('btn-estadoCustom');
				$('#btnCambioEstado').removeClass('btn-success');
				$('#btnCambioEstado').addClass('btn-danger');
				$('#btnCambioEstado').text('Inactivo');
			}


			let lati = data['UbicacionX'];
			let longi = data['UbicacionY'];

			$('#Gmap').locationpicker({
				radius: 0,
				location: {
					latitude: lati,
					longitude: longi
				},

				markerIcon: '/Content/images/iconMarker.png',
				markerDraggable: false,
				markerVisible: true

			});

            if (window.sessionStorage.getItem("rolesUsuario") == null || window.sessionStorage.getItem("rolesUsuario").includes('CLT')) {
                $('#BotonesH').addClass('d-none');
            }

		} else {
			swal("Error de datos", "El hotel no existe", "error");
			setTimeout(function redirection() { window.location.href = '/Home/Index'; }, 3000);
		}
	}

	this.ObtenerHotel = function () {
		var id = document.getElementById("txtIdHotel").value;

		if (id != "nulo") {
            this.ctrlActions.GetById("hotel/" + id, this.CargarHotel);
            this.ctrlActions.GetById("servicio/" + id, this.LlenarCardsServicios);
            this.ctrlActions.GetById("tipoHabitaciones/hotel/" + id , this.ObtenerTiposHabitacion);
		} else {
			swal("Error de datos", "No existen datos", "error");
			setTimeout(function redirection() { window.location.href = '/Home/Index'; }, 3000);
		}

    }
    this.IrModificarH = function () {
        var id = document.getElementById("txtIdHotel").value;
        window.location.href = '/Home/vModificarHotel/' + id;
    }
    this.IrParametrizablesH = function () {
        var id = document.getElementById("txtIdHotel").value;
        window.location.href = '/Home/vParametrosHotel/' + id;
    }
    this.IrBitacoraH = function () {
        var id = document.getElementById("txtIdHotel").value;
        window.location.href = '/Home/vBitacoraHotel/' + id;
    }
    this.CambioEstado = function () {
        var id = document.getElementById("txtIdHotel").value;
        this.ctrlActions.GetById("hotel/cambioEstado/" + id, this.CambioBoton);
    }
    this.CambioBoton = function (data) {
        let estado = data['Estado']

		if (estado == "Activo") {
			$('#btnCambioEstado').removeClass('btn-estadoCustom');
			$('#btnCambioEstado').removeClass('btn-danger');
			$('#btnCambioEstado').addClass('btn-success');
			$('#btnCambioEstado').text('Activo')
		} else {
			$('#btnCambioEstado').removeClass('btn-estadoCustom');
			$('#btnCambioEstado').removeClass('btn-success');
			$('#btnCambioEstado').addClass('btn-danger');
			$('#btnCambioEstado').text('Inactivo');
		}
    }


    this.LlenarCardsServicios = function (data) {

        if (data != null) {
            var table = $("#tblServicios").DataTable({
                "processing": true, "language": {
                    "url": "https://cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
                }
            });

            for (let i = 0; i < data.length; i++) {


                let cardServicio = '<div class="list-block main-block t-list-block"> ' +
                    '                  <div class="list-content" style=" max-width: 350px, max-heigth: 200px;" >' +
                    '                       <div href="https://adtripapp.azurewebsites.net/Home/vPerfilServicio/' + data[i]['Codigo'] + '" class="main-img list-img t-list-img" style="padding-left:10%;" >' +
                    '                              <img style="width:70%" src="' + data[i]['FotoPerfil'] + '" class="img-fluid ' + data[i]['FotoPerfil'] + '" alt="Foto del producto"/> ' +
                    '                      </div>' +
                    '                      <div class="list-info t-list-info"> ' +
                    
                    '                           <h3 class="block-title">' + data[i]['Nombre'] + '</h3>' +
                    '                           <p class="descripcionservicio">' + data[i]['Descripcion'] + '</p>' +
                    '                           <a id="irPerfilServicio" href="https://adtripapp.azurewebsites.net/Home/vPerfilServicio/' + data[i]['Codigo'] + '" class="btn btn-color btn-lg irPerfilServicio">Ver perfil</a> ' +
                    '                       </div>' +
                    '                    </div>' +
                    '                </div>';



                table.row.add([
                    cardServicio
                ]).draw(false);
            }

        } else {
            swal("Error de datos", "No hay servicios", "error");
            table.row.add([
                '<div > ' +
                '     <h3> No hay servicios </h3>     ' +
                ' </div>'

            ]).draw(false);
        }
    }


    this.ObtenerTiposHabitacion = function (data) {

        var cantColumnas = $('#tblTiposHab >tbody >tr').length;


        var dropDownTipo =  '<tr style="margin-bottom: 4%;">' +
                            '   <td style="width:50%;">' +
                            '      <label>Tipo de habitacion</label>' +
                            '      <select class="form-control" id="' + cantColumnas + '" onchange="AnnadirCant(this)">' +
                            '           <option value="" selected="selected">Tipo de habitación...</option>' +
                            '      </select>' +
                            '      <p>Precio: <span id="precioHab' + cantColumnas + '"></span> </p>' +
                            '      <input style="display:none;" id="precioViejo' + cantColumnas + '" value="1"/>' +
                            '     <a id="minus' + cantColumnas + '" onclick="EliminarFila(' + cantColumnas + ');" style="display:none; visibility:hidden; float:right; cursor:pointer;" alt="Eliminar este tipo de habitación"><i class="fa fa-minus"></i></a>' +
                            '  </td>' +
                            '  <td style="width:20%;padding-top: 8%;">' +
                            '     <input min="1" class="form-control cantHab" onchange="CambiarPrecio(this);" type="number" name="10000" id="input' + cantColumnas + '" style="visibility:hidden; width:82%; margin-left:20%;"/>' +
                            '     <a  style="display:none"><span id="valorViejo' + cantColumnas + '"></span></a>' +
                            '     <a id="plus' + cantColumnas + '" onclick="AgregarMasTiposHabitacion();" style="visibility:hidden; float:right; cursor:pointer" title="Agregar más tipos de habitación"><i class="fa fa-plus"></i></a>' +
                            '  </td>' +
                            '</tr>';

        $('#tblTiposHab').append(dropDownTipo);

        for (var i in data) {
            var opcionTipo = '<option precio=' + data[i].Precio + ' value="' + data[i].Codigo + '">' + data[i].Nombre + '</option>';
            $('#' + cantColumnas).append(opcionTipo);
        }
        $('#input' + cantColumnas).val(1);
        $('#valorViejo' + cantColumnas).append('1');

        $('#0').attr('disabled', true);

    }

}

$(document).ready(function () {
	var vperfilHotel = new vPerfilHotel();
    $('#txtPrecio').val(0);
    $('input[type="number"]').val(1);

    var element = document.getElementById("Hotel");
    element.classList.remove("d-none");

    var elementC = document.getElementById("HotelC");
    elementC.classList.remove("d-none");
});

    
function CambiarPrecio(data) {

    let idInput = data.id;
    let id = idInput.substring(5, idInput.length);
    let idPrecio = 'precioHab' + id;
    let idValorViejo = 'valorViejo' + id;
    let valor = $("#" + idPrecio).text();
    let precio = valor.substring(1, valor.length);
    let cantidad = data.value;
    var valorViejo = parseInt($('#' + idValorViejo).text());

    let precioTotal = 0;
   


    let fInicio = new Date($('#txtFechaInicio').val());
    let fFin = new Date($('#txtFechaFin').val());
    var diasdif = fFin.getTime() - fInicio.getTime();
    var contdias = Math.round(diasdif / (1000 * 60 * 60 * 24));

   

    if (parseInt(cantidad) > parseInt(valorViejo)) {
        precioTotal = parseFloat(precioTotal) + (parseFloat(precio) * parseFloat(cantidad) * parseFloat(contdias));
    } else if (parseInt(cantidad) < parseInt(valorViejo)) {
        precioTotal = parseFloat($('#txtPrecio').val()) - (parseFloat(precio) * parseFloat(cantidad) * parseFloat(contdias));
    }

    $('#valorViejo' + id).text(cantidad);

    $('#txtPrecio').val(precioTotal);

}

function AnnadirCant(data) {

    var precio = $('option:selected', data).attr('precio');
    let idPrecioViejo = 'precioViejo' + data.id;

    let precioViejo = $('#' + idPrecioViejo).val();

   
    let idCantHab = 'input' + data.id;
    let idPrecio = 'precioHab' + data.id;
    var idPlus = 'plus' + data.id;
    let idValorViejo = 'valorViejo' + data.id;
    var valorViejo = parseInt($('#' + idValorViejo).text());
    var inputCant = document.getElementById(idCantHab);


    let fInicio = new Date($('#txtFechaInicio').val());
    let fFin = new Date($('#txtFechaFin').val());
    var diasdif = fFin.getTime() - fInicio.getTime();
    var contdias = Math.round(diasdif / (1000 * 60 * 60 * 24));
    let precioTemporal = $('#txtPrecio').val();
    let precioTotal = 0;

    if (precioViejo == "1") {
       
        precioTotal = parseFloat(precioTotal) + (parseFloat(precio) * 1 * parseFloat(contdias));

    } else {
        let cantidadHabitaciones = $('#' + idCantHab).val();

        precioTotal = parseFloat($('#txtPrecio').val()) - (parseFloat(precioViejo) * cantidadHabitaciones * parseFloat(contdias));
        precioTotal = parseFloat(precioTotal) + (parseFloat(precio) * 1 * parseFloat(contdias));
    }

    $('#' + idPrecio).empty();
    $('#' + idPrecio).append("$" + precio);


    $('#' + idPrecioViejo).val(precio);


    if (isNaN(precioTotal)) {
        $('#txtPrecio').val(precioTemporal);
        $('#' + idPrecio).empty();
        $('#' + idPlus).hide();
        $('#' + idCantHab).hide();
    } else {
        $('#txtPrecio').val(precioTotal);

    }
    

    var plusSelect = document.getElementById(idPlus);
    inputCant.style.visibility = 'visible';
    plusSelect.style.visibility = 'visible';

}


function AgregarMasTiposHabitacion() {
    var control = new ControlActions();

    var idHotel = document.getElementById("txtIdHotel").value;

    control.GetById("tipoHabitaciones/hotel/" + idHotel, function (data) {


        var cantColumnas = $('#tblTiposHab tr').length;


        var dropDownTipo = '<tr style="margin-bottom: 4%;" id="fila' + cantColumnas + '">' +
                            '   <td style="width:50%;">' +
                            '      <label>Tipo de habitacion</label>' +
                            '      <select class="form-control" id="' + cantColumnas + '" onchange="AnnadirCant(this)">' +
                            '           <option value="" selected="selected">Tipo de habitación...</option>' +
                            '      </select>' +
                            '     <a id="minus' + cantColumnas + '" onclick="EliminarFila(' + cantColumnas + ');" style=" float:right; cursor:pointer;" title="Eliminar este tipo de habitación"><i class="fa fa-minus"></i></a>' +
                            '      <p>Precio: <span id="precioHab' + cantColumnas + '"></span> </p>' +
                            '      <input style="display:none;" id="precioViejo ' + cantColumnas + '" value="1"/>' +
                            '      <a id="valorViejo' + cantColumnas + '" style="display:none">0</a>' +
                            '  </td>' +
                            '  <td style="width:20%;padding-top: 8%;">' +
                            '     <input min="1" class="form-control" onchange="CambiarPrecio(this);" type="number" name="10000" id="input' + cantColumnas + '" style="visibility:hidden; width:82%; margin-left:20%;"/>' +
                            '     <a id="plus' + cantColumnas + '" onclick="AgregarMasTiposHabitacion();" style="visibility:hidden; float:right; cursor:pointer" title="Agregar más tipos de habitación"><i class="fa fa-plus"></i></a>' +
                            '  </td>' +
                            '</tr>';

        $('#tblTiposHab').append(dropDownTipo);

        for (var i in data) {
            var opcionTipo = '<option precio=' + data[i].Precio + ' value="' + data[i].Codigo + '">' + data[i].Nombre + '</option>';
            $('#' + cantColumnas).append(opcionTipo);
        }
        $('#input' + cantColumnas).val(1);
        $('#valorViejo' + cantColumnas).append('1');
    });

}


function EliminarFila(index) {
    let cantidad = $('#input' + index).val();
    let valor = $('#precioHab' + index).text();
    let precio = valor.substring(1, valor.length);

    let fInicio = new Date($('#txtFechaInicio').val());
    let fFin = new Date($('#txtFechaFin').val());
    var diasdif = fFin.getTime() - fInicio.getTime();
    var contdias = Math.round(diasdif / (1000 * 60 * 60 * 24));

    let precioTotal = parseFloat($('#txtPrecio').val()) - (parseFloat(precio) * parseFloat(cantidad) * parseFloat(contdias));
    $('#txtPrecio').val(precioTotal);
    $("#fila" + index).remove();


}