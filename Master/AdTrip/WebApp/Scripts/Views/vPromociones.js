function vPromociones() {

	this.tblPromocionesId = 'tblPromociones';
	this.service = 'promociones';
	this.ctrlActions = new ControlActions();
	this.columns = "Codigo,CantDisponible,FechaInicio,FechaFin,Descuento,TipoPromocion,Estado";

	var IdHotel = sessionStorage.getItem("idHotelLogeado");
	var serviceByIdHotel = 'promociones/hotel/' + IdHotel;

	/* Drop Down de tipos de habitacion por hotel
	var serviceTipoHabByHotel = 'list/LST_TIPO_HABITACIONES/' + IdHotel;
	
	this.loadTipoHabitaciones = function () {
		this.ctrlActions.GetUrlApiService(serviceTipoHabByHotel);
	}
	*/
	this.RetrieveAll = function () {
		this.ctrlActions.FillTable(serviceByIdHotel, this.tblPromocionesId, false);
	}

	this.ReloadTable = function () {
		this.ctrlActions.FillTable(serviceByIdHotel, this.tblPromocionesId, true);
	}

	this.Create = function () {

		var promocionesData = {};
		promocionesData = this.ctrlActions.GetDataForm('frmCreate');
		promocionesData['idHotel'] = IdHotel;
        promocionesData["IdHotelB"] = IdHotel;
		//Hace el post al create
        this.ctrlActions.PostToAPI(this.service, promocionesData, function () {
            var callback = new vPromociones();
            callback.ReloadTable();
            $('.nav-tabs li').click();
            callback.limpiarFormulario();
        });
	}


	this.Update = function () {

		var promocionesData = {};
		promocionesData = this.ctrlActions.GetDataForm('frmEdition');
		promocionesData['idHotel'] = IdHotel;
		//Hace el post al create
        promocionesData["IdHotelB"] = IdHotel;
        this.ctrlActions.PutToAPI(this.service, promocionesData, function () {
            var callback = new vPromociones();
            callback.ReloadTable();
            $('.nav-tabs li').click();
            callback.limpiarFormulario();
        });
	}

	this.Delete = function () {

		var promocionesData = {};
		promocionesData = this.ctrlActions.GetDataForm('frmEdition');
		promocionesData['idHotel'] = IdHotel;
		//Hace el post al create
        promocionesData["IdHotelB"] = IdHotel;
        this.ctrlActions.DeleteToAPI(this.service, promocionesData, function () {
            var callback = new vPromociones();
            callback.ReloadTable();
            $('.nav-tabs li').click();
            callback.limpiarFormulario();
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

		if (div == "listaPromociones") {
			this.mostrarListar();
			this.esconderCrear();
			this.esconderModificar();
		}
		else if (div == "registrarPromociones") {
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
		$("#listaPromociones").removeClass("d-none");
		$("#listaPromociones").addClass("d-inline");
	}

	this.esconderListar = function () {
		$("#listaPromociones").removeClass("d-inline");
		$("#listaPromociones").addClass("d-none");
	}

	this.mostrarCrear = function () {
		$("#crearPromociones").removeClass("d-none");
		$("#crearPromociones").addClass("d-inline");
	}

	this.esconderCrear = function () {
		$("#crearPromociones").removeClass("d-inline");
		$("#crearPromociones").addClass("d-none");
	}

	this.mostrarModificar = function () {
		$("#modificarPromociones").removeClass("d-none");
		$("#modificarPromociones").addClass("d-inline");
	}

	this.esconderModificar = function () {
		$("#modificarPromociones").removeClass("d-inline");
		$("#modificarPromociones").addClass("d-none");
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

//ON DOCUMENT READY
$(document).ready(function () {

	var vpromociones = new vPromociones();
	vpromociones.RetrieveAll();

	vpromociones.initializepanel();
	$(".nav-tabs li").click(function () {
		vpromociones.tab_click($(this));
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
			txtCantDisponible: {
				required: true,
				range: [1, 100]
			},
			txtFechaInicio: {
				required: true
			},
			txtFechaFin: {
				required: true
				//EndDate: { greaterThan: "#txtFechaInicio" }
			},
			txtTipoPromocion: {
				required: true
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
				required: true,
				minlength: 6,
				maxlength: 20
			},
			txtCantDisponible: {
				required: true,
				range: [1, 1000]
			},
			txtFechaInicio: {
				required: true

			},			
			txtFechaFin: {
				required: true
			//	EndDate: { greaterThan: "#txtFechaInicio" }
			},
			txtDescuento: {
				required: true,
				range: [1, 99]
			},
			txtTipoPromocion: {
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
/*
jQuery.validator.addMethod("greaterThan", function (value, element, params) {

	if (!/Invalid|NaN/.test(new Date(value))){
		return new Date(value) > new Date($(params).val());
	}
	return isNaN(value) && isNaN($(params).val()) || (Number(value) > Number($(params).val()));
}, 'Must be greather than {0}.');

*/