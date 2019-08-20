function vListadoHabitaciones() {

	this.serviceByHotel = 'tipoHabitaciones';
	this.ctrlActions = new ControlActions();
	var IdHotel = sessionStorage.getItem("idHotelLogeado");
	//var IdHotel;
	var serviceByIdHotel = 'tipoHabitaciones/hotel/' + IdHotel;


	this.RetrieveAllTipoHabitaciones = function () {

		IdHotel = $('#txtHotel').val();


		this.ctrlActions.GetById(serviceByIdHotel, this.CargarCardTipoHabitaciones);

	}

	this.BindFields = function (data) {


	}

	this.CargarCardTipoHabitaciones = function (data) {
		if (data != null) {
			var table = $('#tblTipoHabitaciones').DataTable();
			for (let i = 0; i < data.length; i++) {

				let cardHabitacion = '<div class="list-block main-block h-list-block">' +
					' <div class="list-content">' +
					'   <div class="main-img list-img h-list-img">' +
					'     <a href="#">' +
					' <img src="' + data[i]['FotoPrincipal'] + '" class="img-fluid" alt="hotel-img" />' +
					'      </a>' +
					'   <div class="main-mask">' +
					'      <h5 class="page-item offer-price-1"  style="color: white" id="P_Cla' + data[i]['Clasificacion'] + '_' + data[i]['CedulaJuridica'] + '">' +
					'<li class="list-inline-item price">Precio:$' + data[i]['Precio'] + '<span class="divider">|</span><span class="pkg"> La Noche</span></li>' +
					' </h5>' +
					'    </div><!-- end main-mask -->' +
					' </div><!-- end h-list-img -->' +
					'<div class="list-info h-list-info">' +
					'  <h3 class="block-title"><a href="#">' + data[i]['Nombre'] + '</a></h3>' +
					' <p class="block-minor">Numero de camas: ' + data[i]['NumCamas'] + '</p>' +
					//  ' <p class="block-minor">Cupo maximo: ' + data[i]['CupoMax'] + '</p>' +
					'  <p>' + data[i]['Descripcion'] + '</p>' +
					'  <a href="" id="' + data[i]['Codigo'] + '" class="btn" data-toggle="modal" data-target="#edit-profile" onclick="RetriveInfoHab(id)"' +
					'    style="background-color: #00CCCC; color: white">Ver más</a>' +
					' </div><!-- end h-list-info -->' +
					'</div><!-- end list-content -->' +
					'</div> <!--end h - list - block-- >';


				table.row.add([
					cardHabitacion
				]).draw(false);
			}

		} else {
			swal("Error de datos", "La hotel no existe", "error");
			setTimeout(function redirection() { window.location.href = '/Home/Index'; }, 3000);
		}
	}

	this.LlenarFotosTipoHabitaciones = function (data) {
		$('#galeriaTipoHabitaciones').html("");
		for (var i in data) {
			let cardFoto = '  <div class="gallery-product col-12 col-md-6 col-lg-4 col-xl-3">' +
				'      <div class="gallery-block" >' +
				'          <div class="gallery-img ">' +
				'                <img src="' + data[i].UrlFoto + '" class="img-fluid img-servicios" alt="' + data[i].UrlFoto + '">' +
				'                <div class="gallery-mask">' +
				'                    <a href="' + data[i].UrlFoto + '" title="' + data[i].UrlFoto + '" class="with-caption image-link">' +
				'                      <span> ' +
				'                           <i class="fa fa-arrows-v" ></i> ' +
				'                       </span> ' +
				'                    </a> ' +
				'                </div><!-- end gallery-mask -->' +
				'         </div><!-- end gallery-img -->' +
				'      </div> <!--end gallery - block-- >´' +
				'  </div >< !--end gallery - product-- >';

			$('#galeriaTipoHabitaciones').append(cardFoto);
		}
		
	}

	this.MostrarInfoHab = function (data) {


		$('#Pp_Descripcion').html('<span id="add_here"> ' + data["Descripcion"] + '</span>');

		$('#P_NumCamas').html("Numero de camas: " + data["NumCamas"]);

		$('#P_CupoMax').html("Cupo máximo: " + data["CupoMax"]);

		$('#P_Precio').html(" Precio: $ " + data["Precio"]);



	}



}

$(document).ready(function () {

	var vlistadoHabitaciones = new vListadoHabitaciones();
	vlistadoHabitaciones.RetrieveAllTipoHabitaciones();


});

function RetriveInfoHab(id) {
	var control = new ControlActions();
	var serviceIdHabitacion = 'tipoHabitaciones/' + id;

	control.GetById("foto/TipoHabitacion/" + id, function (data) {
		var f = new vListadoHabitaciones();
		f.LlenarFotosTipoHabitaciones(data)
	});

	control.GetById(serviceIdHabitacion, function (data) {
		var th = new vListadoHabitaciones();
		th.MostrarInfoHab(data)
	}


	)
};

