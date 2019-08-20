function vHabitaciones() {

	this.tblHabitacionesId = 'tblHabitaciones';
	this.service = 'habitaciones';
	this.ctrlActions = new ControlActions();
	this.columns = "Codigo,Estado,IdTipoHab,NombreTipoHab";

	var IdHotel = sessionStorage.getItem("idHotelLogeado");
	var serviceByIdHotel = 'habitaciones/hotel/' + IdHotel;

	/* Drop Down de tipos de habitacion por hotel
	var serviceTipoHabByHotel = 'list/LST_TIPO_HABITACIONES/' + IdHotel;
	
	this.loadTipoHabitaciones = function () {
		this.ctrlActions.GetUrlApiService(serviceTipoHabByHotel);
	}
	*/
	this.RetrieveAll = function () {
		this.ctrlActions.FillTable(serviceByIdHotel, this.tblHabitacionesId, false);
		this.ctrlActions.GetById("tipoHabitaciones/hotel/" + IdHotel, this.ObtenerTiposHabitacion);
	}

	this.ReloadTable = function () {
		this.ctrlActions.FillTable(serviceByIdHotel, this.tblHabitacionesId, true);
	}


	this.ObtenerTiposHabitacion = function (data) {

		var cantColumnas = $('#tblTiposHab >tbody >tr').length;


		var dropDownTipo = '<tr style="margin-bottom: 4%;">' +
							'   <td style="width:50%;">' +
							'      <label>Tipo de habitacin</label>' +
							'      <select class="form-control" id="' + cantColumnas + '">' +
							'           <option value="" selected="selected">Tipo de habitación...</option>' +
							'      </select>' +
							'  </td>' +
							'</tr>';

		$('#tblTiposHab').append(dropDownTipo);
		for (var i in data) {
			var opcionTipo = '<option value="' + data[i].Codigo + '">' + data[i].Nombre + '</option>';

			$('#' + cantColumnas).append(opcionTipo);

		}
	}

	this.Create = function () {

		var habitacionesData = {};
		//habitacionesData = this.ctrlActions.GetDataForm('frmCreate');

		habitacionesData['idHotel'] = IdHotel;

		var e = document.getElementById("0");
		habitacionesData.IdTipoHab = e.options[e.selectedIndex].value;
	
		//Hace el post al create
        this.ctrlActions.PostToAPI(this.service, habitacionesData, function () {
            var callback = new vHabitaciones();
            callback.ReloadTable();
            $('.nav-tabs li').click();
            callback.limpiarFormulario();
        });
	}


	this.Update = function () {

		var habitacionesData = {};
		habitacionesData = this.ctrlActions.GetDataForm('frmEdition');
		//Hace el post al create
        habitacionesData["IdHotelB"] = IdHotel;
		this.ctrlActions.PutToAPI(this.service, habitacionesData, function () {
			$('.nav-tabs li').click();
            var callback = new vHabitaciones();
			callback.ReloadTable();
            callback.limpiarFormulario();
        });
	}

	this.Delete = function () {

		var habitacionesData = {};
		habitacionesData = this.ctrlActions.GetDataForm('frmEdition');
        habitacionesData['idHotel'] = IdHotel;
        habitacionesData["IdHotelB"] = IdHotel;
		//Hace el post al create
        this.ctrlActions.DeleteToAPI(this.service, habitacionesData, function () {
            var vhabitaciones = new vHabitaciones();
            vhabitaciones.ReloadTable();
            vhabitaciones.limpiarFormulario();
        });
	}

	this.BindFields = function (data) {
		this.ctrlActions.BindFields('frmEdition', data);
		this.esconderListar();
		this.esconderCrear();
		this.mostrarModificar();
	}

	
	this.tab_click = function (ctl) {
		this.tab_handling(ctl);

		div = $(ctl).attr("data-div");

		if (div == "listaHabitaciones") {
			this.mostrarListar();
			this.esconderCrear();
			this.esconderModificar();
		}
		else if (div == "registrarHabitaciones") {
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
		$("#listaHabitaciones").removeClass("d-none");
		$("#listaHabitaciones").addClass("d-inline");
	}

	this.esconderListar = function () {
		$("#listaHabitaciones").removeClass("d-inline");
		$("#listaHabitaciones").addClass("d-none");
	}

	this.mostrarCrear = function () {
		$("#crearHabitaciones").removeClass("d-none");
		$("#crearHabitaciones").addClass("d-inline");
	}

	this.esconderCrear = function () {
		$("#crearHabitaciones").removeClass("d-inline");
		$("#crearHabitaciones").addClass("d-none");
	}

	this.mostrarModificar = function () {
		$("#modificarHabitaciones").removeClass("d-none");
		$("#modificarHabitaciones").addClass("d-inline");
	}

	this.esconderModificar = function () {
		$("#modificarHabitaciones").removeClass("d-inline");
		$("#modificarHabitaciones").addClass("d-none");
	}

	this.limpiarFormulario = function () {
		$("#frmEdition")[0].reset();
	}
}

//ON DOCUMENT READY
$(document).ready(function () {


	var vhabitaciones = new vHabitaciones();
	vhabitaciones.RetrieveAll();

	vhabitaciones.initializepanel();
	$(".nav-tabs li").click(function () {
		vhabitaciones.tab_click($(this));
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

ReglasValidacionCrear = function () {
	$("#frmCreate").submit(function (e) {
		e.preventDefault();
	}).validate({
		lang: 'es',
		errorClass: "is-invalid",
		rules: {
			txtIdTipoHab: {
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