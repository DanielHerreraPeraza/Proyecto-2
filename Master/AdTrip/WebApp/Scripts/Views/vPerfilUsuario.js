function vPerfilUsuario() {
	this.ctrlActions = new ControlActions();
	var DatosSessionUsuario = {};
	DatosSessionUsuario = JSON.parse(sessionStorage.getItem("usuario"));
	var IdUsuario = DatosSessionUsuario["Identificacion"];
	this.serviceIdUsuario = 'usuario/' + IdUsuario;
	this.service = 'usuario';

	this.RetrieveAllDatosUsuario = function () {

        this.ctrlActions.GetById(this.serviceIdUsuario, this.MostrarDatosUsuario);

        var allNav = document.querySelectorAll('.nav-item');
        var usuarioLogueado = sessionStorage.getItem('usuario');
        if (DatosSessionUsuario['Roles'].includes("CLT")) {
            this.ctrlActions.GetById('reserva/misReservas/' + IdUsuario, this.MostrarReservas)
        } else {
            $('.reservaciones').hide();
        }
	}

	this.MostrarDatosUsuario = function (data) {

		$('#P_Identificacion').append(data["Identificacion"]);	

		$('#P_NombreUsuario').append(data["PrimerNombre"]);	

		$('#P_SegundoNombre').append(data["SegundoNombre"]);	

		$('#P_Apellido').append(data["PrimerApellido"]);	

		$('#P_SegundoApellido').append(data["SegundoApellido"]);	

		$('#P_Provincia').append(data["Provincia"]);	

		$('#P_Canton').append(data["Canton"]);	

		$('#P_Distrito').append(data["Distrito"]);	

		$('#P_Direccion').append(data["DireccionExacta"]);	
		
		$('#P_Telefono').append(data["Telefono"]);	

		$('#P_Correo').append(data['Correo']);
			
		$('#P_Calificaciones').append(data["Calificacion"]);

		var vperfilUsuario = new vPerfilUsuario();
		vperfilUsuario.MostrarDatosModificar(data);
	}

	this.MostrarDatosModificar = function (data) {


		$('#txtIdentificacion').val(data["Identificacion"]);

		$('#txtPrimerNombre').val(data["PrimerNombre"]);

		$('#txtSegundoNombre').val(data["SegundoNombre"]);

		$('#txtPrimerApellido').val(data["PrimerApellido"]);

		$('#txtSegundoApellido').val(data["SegundoApellido"]);

		$('#txtCorreo').val(data["Correo"]);

		$('#txtTelefono').val(data["Telefono"]);

		$('#txtProvincia').val(data["Provincia"]);
		
		$('#txtCanton').val(data["Canton"]);

		$('#txtDistrito').val(data["Distrito"]);

		$('#txtDireccionExacta').val(data["DireccionExacta"]);

		$('#txtCalificacion').val(data["Calificacion"]);

		$('#txtEstado').val(data["ValorEstado"]);

	}

	this.Update = function () {

		var usuarioData = {};
		usuarioData = this.ctrlActions.GetDataForm('frmEdition');
		//Hace el post al create
        this.ctrlActions.PutToAPI(this.service, usuarioData, function () {
            location.reload();
		});
		//Refresca la tabla
		

	}

	this.ValidarInputsModificar = function () {
		if ($("#frmEdition").valid()) {
			this.Update();
		}
	}


    this.MostrarReservas = function (data) {
        var control = new ControlActions();


        if (data != null) {
            for (let i = 0; i < data.length; i++) {

                var meses = ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Setiembre', 'Octubre', 'Noviembre', 'Diciembre'];

            

                control.GetById('hotel/' + data[i].IdHotel, function (infoHotel) {



                    var fechaI = new Date(data[i].FechaInicio);
                    var fechaF = new Date(data[i].FechaFin);
                    var dia = fechaI.getDate();
                    var mes = fechaI.getMonth();
                    var anno = fechaI.getFullYear();

                    var fechaInicioString = dia + " - " + (mes + 1) + ' - ' + anno;
                    var fechaFinString = fechaF.getDate() + " - " + (fechaF.getMonth() + 1) + ' - ' + fechaF.getFullYear();

                    let cardReserva = ' <tr id="reserva' + i + '">' +
                                      '   <td class="dash-list-icon booking-list-date" > <div class="b-date"><h3 id="diaReserva' + i + '" >' + dia + '</h3><p id="mesReserva' + i + '">' + meses[mes] + '</p></div></td>' +
                                      '   <td class="dash-list-text booking-list-detail">' +
                                      '      <h3 id="nombreHotel' + i + '">' + infoHotel.Nombre + '</h3>' +
                                      '      <ul class="list-unstyled booking-info">' +
                                      '          <li><span id="fechaInicio' + i + '">Fecha de inicio: </span>' + fechaInicioString + '</li>' +
                                      '          <li><span id="fechaFin' + i + '">Fecha de finalización: </span>' + fechaFinString + '</li>' +
                                      '          <li><span>Información de hotel:</span> Dirección: <span id="direccion' + i + '">' + infoHotel.Direccion + '</span> <span class="line">|</span>Correo: <span id="correo' + i + '">' + infoHotel.Correo + '</span> <span class="line">|</span>Teléfonos: <span id="telefono' + i + '">' + infoHotel.Telefonos+'</span></li > ' +
                                      '          <li style="display:none"><span id="codigoReserva'+i+'">'+ data[i].Codigo +'</span></li > ' +
                                      '      </ul>' +
                                      '      <button id="button' + i + '" class="btn btn-color" value='+i+' onclick="IrDetalleReserva(this)" >Ver detalle de reserva</button>' +
                                      '   </td>' +
                                 //   '   <td class="dash-list-btn">' +
                                //    '      <button class="btn btn-primary" id="c' + i + '" style="background-color:#D8D8D8FF;" value='+i+' onclick="CancelarReserva(this)">Cancelar</button>' +
                               //     '      <button class="btn" id="e' + i + '" style="width: 40%;" value=' + i +' onclick="ExtenderReserva(this)">Extender reserva</button>' +
                              //      '   </td>' +
                                      ' </tr>';
                    $('#infoReservas').append(cardReserva); 
                });
            }
        } else {
            swal("Error de datos", "No ha realizado ninguna reserva", "error");
        }
    }


    this.tab_click = function (ctl) {
        this.tab_handling(ctl);

        div = $(ctl).attr("data-div");

        if (div == "perfil") {
            this.mostrarPerfilUsuario();
            this.esconderReservaciones();
        }
        else if (div == "reservaciones") {
            this.mostrarReservaciones();
            this.esconderPerfilUsuario();
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

    this.mostrarPerfilUsuario = function () {
        $("#perfil").removeClass("d-none");
        $("#perfil").addClass("d-inline");
        }

    this.esconderPerfilUsuario = function () {
        $("#perfil").removeClass("d-inline");
        $("#perfil").addClass("d-none");
    }

    this.mostrarReservaciones = function () {
        $("#reservaciones").removeClass("d-none");
        $("#reservaciones").addClass("d-inline");
    }

    this.esconderReservaciones = function () {
        $("#reservaciones").removeClass("d-inline");
        $("#reservaciones").addClass("d-none");
    }
}

$(document).ready(function () {
	var vperfilUsuario = new vPerfilUsuario();
    vperfilUsuario.RetrieveAllDatosUsuario();


    
    vperfilUsuario.initializepanel();
    $(".nav-tabs li").click(function () {
        vperfilUsuario.tab_click($(this));
    });
    $(function () {
        var showCanton = function (selectedProvincia) {
            $('#txtCanton option').hide();
            $('#txtCanton').find('option').filter("option[value ^= " + selectedProvincia + "]").show();
            //set default value
            var defaultCanton = "Seleccione una provincia";
            $('#txtCanton').val(defaultCanton);
        };

        var showDistrito = function (selectedCanton) {
            $('#txtDistrito option').hide();
            $('#txtDistrito').find('option').filter("option[value ^= " + selectedCanton + "]").show();
            //set default value
            var defaultDistrito = "Seleccione un cantón";
            $('#txtDistrito').val(defaultDistrito);
        };

        //set default provincia
        var provincia = $('#txtProvincia').val();
        showCanton(provincia);
        $('#txtProvincia').change(function () {
            showCanton($(this).val());
        });

        //set default canton
        var canton = $('#txtCanton').val();
        showDistrito(canton);
        $('#txtCanton').change(function () {
            showDistrito($(this).val());
        });

        $('#txtDistrito').change(function () {
        });
    });


	ReglasValidacionModificar();
});



ReglasValidacionModificar = function () {
	$("#frmEdition").submit(function (e) {
		e.preventDefault();
	}).validate({
		lang: 'es',
		errorClass: "is-invalid",
		rules: {
			txtIdentificacion: {
				required: true
			},
			txtPrimerNombre: {
				required: true
			},
			txtPrimerApellido: {
				required: true
			},
			txtSegundoApellido: {
				required: true
			},
			txtCorreo: {
				required: true
			},
			txtTelefono: {
				required: true
			},
			txtProvincia: {
				required: true
			},			
			txtCanton: {
				required: true
			},			
			txtDistrito: {
				required: true
			},
			txtDireccionExacta: {
				required: true
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



function IrDetalleReserva(data) {
    var control = new ControlActions();

    var id = data.value;
    var idReserva = 'codigoReserva' + id;
    var codigo = $('#' + idReserva).text();

    control.GetById('reserva/' + codigo, function (data) {
        sessionStorage.setItem('detallereserva', JSON.stringify(data));
        window.location.href = '/Home/vPerfilReserva';
    })


    


}
