function vTipoHabitaciones() {

	this.tblTipoHabitacionesId = 'tblTipoHabitaciones';
	this.service = 'tipoHabitaciones';
	this.serviceFotos = 'foto';
	this.ctrlActions = new ControlActions();
	this.columns = "Codigo,Nombre,Descripcion,NumCamas,CupoMax,Precio,Estado,HoraCheckIn,HoraChekOut,IdHotel";

	var IdHotel = sessionStorage.getItem("idHotelLogeado");
	var serviceByIdHotel = 'tipoHabitaciones/hotel/' + IdHotel;

	this.RetrieveAll = function () {
		this.ctrlActions.FillTable(serviceByIdHotel, this.tblTipoHabitacionesId, false);
	}

	this.ReloadTable = function () {
		this.ctrlActions.FillTable(serviceByIdHotel, this.tblTipoHabitacionesId, true);
	}

	this.Create = function () {

		var tipoHabitacionesData = {};
		var fotoTipoHabitaciones = {};


		tipoHabitacionesData = this.ctrlActions.GetDataForm('frmCreate');
		tipoHabitacionesData['Codigo'] = $('#txtCodigo').val() + IdHotel;
		tipoHabitacionesData['idHotel'] = IdHotel;
		tipoHabitacionesData.FotoPrincipal = $('#fotoPrincipalTipoHabitacion').attr('src');
		
		fotoTipoHabitaciones = this.ctrlActions.GetDataFotos('imagenesTipoHabitaciones');

        tipoHabitacionesData['IdHotelB'] = IdHotel;
		//Hace el post al create
		if (fotoTipoHabitaciones.length != 0) {
        this.ctrlActions.PostToAPI(this.service, tipoHabitacionesData, function () {
            $(fotoTipoHabitaciones).each(function (index, value) {
                var codigoTipoHab = $('#txtCodigo').val() + IdHotel;
                value.IdEntidad = codigoTipoHab;
                value.Entidad = 'TipoHabitacion';

                var control = new ControlActions();
                control.PostToAPI('foto', value, function () {

                    var control2 = new ControlActions();
                    control2.FillTable(this.service, this.tblTipoHabitacionesId, true);

                })
            });
            var callback = new vTipoHabitaciones();
            callback.ReloadTable();
            $('.nav-tabs li').click();
			});
		} else {
			swal('No se puede continuar con el registro', 'Para completar el registro, debe ingresar imágenes a las habitaciones', 'warning');
		}
	}

	// INICIO SECCION FOTOS 

	this.AgregarFotoPrincipal = function () {

		let imagenUrl = '';

		// with credentials available on
		// your Cloudinary account dashboard
		$.cloudinary.config({ cloud_name: 'datatek', api_key: '678592257184526' });

		let cloudinaryURL = 'https://res.cloudinary.com/datatek/image/upload/v1562616902/';
		// Initiate upload
		
		cloudinary.openUploadWidget({ cloud_name: 'datatek', upload_preset: 'datatek-group', tags: ['cgal'] },
			function (error, result) {
				if (error) console.log(error);
				// If NO error, log image data to console
				let id = result[0].public_id;

				imagenUrl = processImage(id);

				var image = document.getElementById("fotoPrincipalTipoHabitacion");
				image.src = cloudinaryURL + id;
			}
		)

	};

	this.AgregarFoto = function () {

		let imagenUrl = '';
		// Configure Cloudinary
		// with credentials available on
		// your Cloudinary account dashboard
		$.cloudinary.config({ cloud_name: 'datatek', api_key: '678592257184526' });


		// Initiate upload
		cloudinary.openUploadWidget({ cloud_name: 'datatek', upload_preset: 'datatek-group', tags: ['cgal'] },
			function (error, result) {
				if (error) console.log(error);
				// If NO error, log image data to console
				let id = result[0].public_id;

				imagenUrl = 'https://res.cloudinary.com/datatek/image/upload/v1562616902/' + id;


				var nColumnas = $("#imagenesTipoHabitaciones tr:last td").length;

				if (nColumnas == 0) {
					$("#imagenesTipoHabitaciones").append(" <tr> <td> <img style ='min-width:200px; max-width:200px; max-height:150px; min-height:150px;' class = 'ImagenServicio' src= '"
						+ imagenUrl + "'  alt= '" + id + "'   />  </td> </tr>");
				} else if (nColumnas == 4) {
					$("#imagenesTipoHabitaciones").append(" <tr> <td> <img style ='min-width:200px; max-width:200px; max-height:150px; min-height:150px;' class = 'ImagenServicio' src= '"
						+ imagenUrl + "'  alt= '" + id + "'   />  </td> </tr>");


				} else if (nColumnas < 4 && nColumnas > 0) {
					$("#imagenesTipoHabitaciones tr:last").append(" <td> <img style ='min-width:200px; max-width:200px; max-height:150px; min-height:150px;' id = 'ImagenServicio' src= '"
						+ imagenUrl + "'  alt= '" + id + "'   />  </td>");
				}


			});
	}

	this.AgregarNuevaFoto = function () {

		let imagenUrl = '';
		// Configure Cloudinary
		// with credentials available on
		// your Cloudinary account dashboard
		$.cloudinary.config({ cloud_name: 'datatek', api_key: '678592257184526' });

		// Initiate upload
		cloudinary.openUploadWidget({ cloud_name: 'datatek', upload_preset: 'datatek-group', tags: ['cgal'] },
			function (error, result) {
				if (error) console.log(error);
				// If NO error, log image data to console
				let id = result[0].public_id;

				imagenUrl = 'https://res.cloudinary.com/datatek/image/upload/v1562616902/' + id;

				var nColumnas = $("#modImgsServicio tr:last td").length;

				if (nColumnas == 0) {
					$("#modImgsServicio").append(" <tr> <td> <img  style ='min-width:200px; max-width:200px; max-height:150px; min-height:150px;' class = 'ImagenServicio' src= '"
						+ imagenUrl + "'  alt= '" + id + "'   />  </td> </tr>");
				} else if (nColumnas == 3) {
					$("#modImgsServicio").append(" <tr> <td> <img style ='min-width:200px; max-width:200px; max-height:150px; min-height:150px;' class = 'ImagenServicio' src= '"
						+ imagenUrl + "'  alt= '" + id + "'   />  </td> </tr>");

				} else if (nColumnas < 3 && nColumnas > 0) {
					$("#modImgsServicio tr:last").append(" <td> <img style ='min-width:200px; max-width:200px; max-height:150px; min-height:150px;' id = 'ImagenServicio' src= '"
						+ imagenUrl + "'  alt= '" + id + "'   />  </td>");
				}
			});
	}

	function postFotosServicio() {

		var fotosTipoHabitaciones = {};

		this.service = 'tipoHabitaciones';
		this.ctrlActions = new ControlActions();

		fotosTipoHabitaciones = this.ctrlActions.GetDataFotos('imagenesTipoHabitaciones');
		$(fotosTipoHabitaciones).each(function (index, value) {
			value.IdEntidad = $('#txtCodigo').val();
			value.Entidad = 'TipoHabitaciones';
			this.ctrlActions = new ControlActions();

			this.ctrlActions.PostToAPI('foto', value, function () {
				this.ReloadTable();
				$('.nav-tabs li').click();
				this.limpiar();
				this.limpiarTablaFotos();
			});

		})
	}


	function processImage(id) {
		let options = {
			client_hints: true,
		};
		return $.cloudinary.url(id, options);
	}


	//FIN SECCION FOTOS

	this.limpiarTablaFotos = function () {
		document.getElementById("imagenesTipoHabitaciones").closest('tr').remove();
	}

	this.Update = function () {

		var tipoHabitacionesData = {};
		tipoHabitacionesData = this.ctrlActions.GetDataForm('frmEdition');
		tipoHabitacionesData['idHotel'] = IdHotel;
		tipoHabitacionesData.FotoPrincipal = $('#modFotoServicio').attr('src');
		fotosTipoHabitaciones = this.ctrlActions.GetDataFotos('modImgsServicio');
		//Hace el post al create
        tipoHabitacionesData['IdHotelB'] = IdHotel;
		this.ctrlActions.PutToAPI(this.service, tipoHabitacionesData, function () {

			$(fotosTipoHabitaciones).each(function (index, value) {
				value.IdEntidad = $('#modCodigo').val();
				value.Entidad = 'TipoHabitaciones';
				var control = new ControlActions();

				control.PostToAPI('foto', value, function () {

				});

				var callback = new vTipoHabitaciones();
				callback.ReloadTable();
				$('.nav-tabs li').click();

			});
			//Refresca la tabla


		});
			}

	this.Delete = function () {

		var tipoHabitacionesData = {};
		tipoHabitacionesData = this.ctrlActions.GetDataForm('frmEdition');
		//Hace el post al create
        tipoHabitacionesData['IdHotelB'] = IdHotel;
        this.ctrlActions.DeleteToAPI(this.service, tipoHabitacionesData, function () {
            var callback = new vTipoHabitaciones();
            callback.ReloadTable();
            $('.nav-tabs li').click();
        });
	}

	this.BindFields = function (data) {
		this.ctrlActions.BindFields('frmEdition', data);

		this.ctrlActions.GetById("foto/Servicio/" + $('#modCodigo').val(), this.MostrarFotosServicios);

		$('#modFotoServicio').attr("src", data.FotoPrincipal);
		$('#modFotoServicio').attr("alt", data.FotoPrincipal);


		$('#modCodigo').prop('disabled', true);
		this.mostrarModificar();
		this.esconderListar();
		this.esconderCrear();
		
	}

	this.tab_click = function (ctl) {
		this.tab_handling(ctl);

		div = $(ctl).attr("data-div");

		if (div == "listaTipoHabitaciones") {
			this.mostrarListar();
			this.esconderCrear();
			this.esconderModificar();
		}
		else if (div == "registrarTipoHabitaciones") {
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
		$("#listaTipoHabitaciones").removeClass("d-none");
		$("#listaTipoHabitaciones").addClass("d-inline");
	}

	this.esconderListar = function () {
		$("#listaTipoHabitaciones").removeClass("d-inline");
		$("#listaTipoHabitaciones").addClass("d-none");
	}

	this.mostrarCrear = function () {
		$("#crearTipoHabitaciones").removeClass("d-none");
		$("#crearTipoHabitaciones").addClass("d-inline");
	}

	this.esconderCrear = function () {
		$("#crearTipoHabitaciones").removeClass("d-inline");
		$("#crearTipoHabitaciones").addClass("d-none");
	}

	this.mostrarModificar = function () {
		$("#modificarTipoHabitaciones").removeClass("d-none");
		$("#modificarTipoHabitaciones").addClass("d-inline");
	}

	this.esconderModificar = function () {
		$("#modificarTipoHabitaciones").removeClass("d-inline");
		$("#modificarTipoHabitaciones").addClass("d-none");
	}

	this.limpiarFormulario = function () {
		$("#frmEdition")[0].reset();
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

/*Cards tipo Habitaciones */


//ON DOCUMENT READY
$(document).ready(function () {

	var vtipoHabitaciones = new vTipoHabitaciones();
	vtipoHabitaciones.RetrieveAll();

	vtipoHabitaciones.initializepanel();
	$(".nav-tabs li").click(function () {
		vtipoHabitaciones.tab_click($(this));
	});

	ReglasValidacionModificar();
	ReglasValidacionCrear();

    var element = document.getElementById("Hotel");
    element.classList.remove("d-none");
    var elementC = document.getElementById("HotelC");
    elementC.classList.remove("d-none");
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
			},
			txtNumCamas: {
				required: true,
				range: [1, 10]
			},
			txtCupoMax: {
				required: true,
				range: [1, 20]
			},
			txtPrecio: {
				required: true,
				range: [1, 2000]
			},
			txtUEstado: {
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
				required: true
			},
			txtNombre: {
				required: true
			},
			txtDescripcion: {
				required: true
			},
			txtNumCamas: {
				required: true,
				range: [1,10]
			},
			txtCupoMax: {
				required: true,
				range: [1, 20]
			},
			txtPrecio: {
				required: true,
				range: [1, 2000]
			},
			txtNumHabitaciones: {
				required: true,
				range: [1, 200]
			},
			txtEstado: {
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

$.validator.addMethod("txtHoraCheckIn", function (value, element) {
	return this.optional(element) || /^([01]\d|2[0-3]|[0-9])(:[0-5]\d){1,2}$/.test(value);
}, "Please enter a valid time, between 00:00 and 23:59");

$.validator.addMethod("txtHoraCheckOut", function (value, element) {
	return this.optional(element) || /^([01]\d|2[0-3]|[0-9])(:[0-5]\d){1,2}$/.test(value);
}, "Please enter a valid time, between 00:00 and 23:59");





function ModFotoPerfil() {

	let imagenUrl = '';

	// with credentials available on
	// your Cloudinary account dashboard
	$.cloudinary.config({ cloud_name: 'datatek', api_key: '678592257184526' });

	let cloudinaryURL = 'https://res.cloudinary.com/datatek/image/upload/v1562616902/';
	// Initiate upload
	cloudinary.openUploadWidget({ cloud_name: 'datatek', upload_preset: 'datatek-group', tags: ['cgal'] },
		function (error, result) {
			if (error) console.log(error);
			// If NO error, log image data to console
			let id = result[0].public_id;

			imagenUrl = processImage(id);

			var image = document.getElementById("modFotoServicio");
			image.src = cloudinaryURL + id;
		}
	)
};