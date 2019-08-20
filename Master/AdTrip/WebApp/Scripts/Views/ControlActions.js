function ControlActions() {

    this.URL_API = "https://adtripapi.azurewebsites.net/api/";

    this.GetUrlApiService = function (service) {
        return this.URL_API + service;
    }

    this.GetTableColumsDataName = function (tableId) {
        var val = $('#' + tableId).attr("ColumnsDataName");

        return val;
    }

    this.FillTable = function (service, tableId, refresh) {

        if (!refresh) {
            columns = this.GetTableColumsDataName(tableId).split(',');
            var arrayColumnsData = [];


            $.each(columns, function (index, value) {
                var obj = {};
                obj.data = value;
                arrayColumnsData.push(obj);
            });

            $('#' + tableId).DataTable({
                "processing": true,
                "ajax": {
                    "url": this.GetUrlApiService(service),
                    dataSrc: 'Data'
                },
                "columns": arrayColumnsData,
                "language": {
                    "url": "https://cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json",
                }

            });
        } else {
            //RECARGA LA TABLA
            $('#' + tableId).DataTable().ajax.reload();
        }

    }

    this.FillTableId = function (service, tableId, id, refresh) {

        if (!refresh) {
            columns = this.GetTableColumsDataName(tableId).split(',');
            var arrayColumnsData = [];


            $.each(columns, function (index, value) {
                var obj = {};
                obj.data = value;
                arrayColumnsData.push(obj);
            });

            $('#' + tableId).DataTable({
                "processing": true,
                "ajax": {
                    "url": this.GetUrlApiService(service) + "/" + id,
                    dataSrc: 'Data'
                },
                "columns": arrayColumnsData,
                "language": {
                    "url": "https://cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
                }
            });
        } else {
            //RECARGA LA TABLA
            var table = $('#' + tableId).DataTable();
            table.ajax.reload();
        }

    }

    this.GetSelectedRow = function () {
        var data = sessionStorage.getItem(tableId + '_selected');

        return data;
    };

    this.BindFields = function (formId, data) {
        $('#' + formId + ' *').filter(':input').each(function (input) {
            var columnDataName = $(this).attr("ColumnDataName");
            this.value = data[columnDataName];
        });
    }

    this.GetDataForm = function (formId) {
        var data = {};

        $('#' + formId + ' *').filter(':input').each(function (input) {
            var columnDataName = $(this).attr("ColumnDataName");
            data[columnDataName] = this.value;
        });

        return data;
    }

    this.GetDataFotos = function (bodyTableId) {
        var arrayFotos = [];
        var contador = 0;

        $("#" + bodyTableId + " tr td").each(function (index, value) {
            var foto = {};
            foto.UrlFoto = value.firstElementChild.currentSrc;
            foto.TipoFoto = "Secundaria";
            arrayFotos.push(foto);
            contador++;
        });
        return arrayFotos;
    }

    this.ShowMessage = function (type, message) {
        if (type == 'E') {
            $("#alert_container").removeClass("alert alert-success alert-dismissable")
            $("#alert_container").addClass("alert alert-danger alert-dismissable");
            $("#alert_message").text(message);
            swal("Advertencia", message, "error");
        } else if (type == 'I') {
            $("#alert_container").removeClass("alert alert-danger alert-dismissable")
            $("#alert_container").addClass("alert alert-success alert-dismissable");
            $("#alert_message").text(message);
        }
        $('.alert').show();
    };

    this.PostToAPI = function (service, data, callBackFunction) {
        var datos = this.GetUsuarioBitacora(data);
        var jqxhr = $.post(this.GetUrlApiService(service), datos, function (response) {
            var ctrlActions = new ControlActions();
            ctrlActions.ShowMessage('I', response.Message);

            callBackFunction();
        })
            .fail(function (response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
            })
    };

    this.PostToValida = function (service, data, callBackFunction) {
        //este solo para validaciones que requieran un cuerpo pero que no tienen un mensaje de respuesta
        var jqxhr = $.post(this.GetUrlApiService(service), data, function (response) {
            //var ctrlActions = new ControlActions();
            //ctrlActions.ShowMessage('I', response.Message);
            callBackFunction(response.Data);
        })
            .fail(function (response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
            })
    };

    this.PutToAPI = function (service, data, callBackFunction) {
        var datos = this.GetUsuarioBitacora(data);
        var jqxhr = $.put(this.GetUrlApiService(service), datos, function (response) {
            var ctrlActions = new ControlActions();
            callBackFunction();
        })
            .fail(function (response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
            })
    };

    this.DeleteToAPI = function (service, data, callBackFunction) {
        var datos = this.GetUsuarioBitacora(data);
        var jqxhr = $.delete(this.GetUrlApiService(service), datos, function (response) {
            var ctrlActions = new ControlActions();
            ctrlActions.ShowMessage('I', response.Message);
            callBackFunction();
        })
            .fail(function (response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
            })
    };

    this.GetFotosToApi = function (service, entidad, idEntidad, callbackFunction) {
        var jqxhr = $.get(this.GetUrlApiService(service) + "/" + entidad + "/" + idEntidad, function (response) {
            callbackFunction(response.Data);
        })
            .fail(function (response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
            })
    }

    this.GetToApi = function (service, callbackFunction) {
        var jqxhr = $.get(this.GetUrlApiService(service), data, function (response) {
            var ctrlActions = new ControlActions();
            ctrlActions.ShowMessage('I', response.Message);
            callbackFunction();
        })
            .fail(function (response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
            })
    }

    this.GetById = function (service, callbackFunction) {
        var jqxhr = $.get(this.GetUrlApiService(service), function (response) {
            //var ctrlActions = new ControlActions();
            //ctrlActions.ShowMessage('I', response.Message);//no trae respuesta, la respuesta es el objeto
            callbackFunction(response.Data);// la diferencia es una B cuando debe ser una b
        })
            .fail(function (response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);

            })
    }

    this.GetByIdData = function (service, data, callbackFunction) {
        var jqxhr = $.get(this.GetUrlApiService(service), data, function (response) {
            //var ctrlActions = new ControlActions();
            //ctrlActions.ShowMessage('I', response.Message);//no trae respuesta, la respuesta es el objeto
            callbackFunction(response.Data);// la diferencia es una B cuando debe ser una b
        })
            .fail(function (response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);

            })
    }

    this.GetArrayFotosToApi = function (service, callbackFunction) {
        var jqxhr = $.get(this.GetUrlApiService(service), function (response) {
            callbackFunction(response.Data);
        });
    }

    this.Login = function (service, data, inicio) {
        var jqxhr = $.post(this.GetUrlApiService(service), data, function (response) {
            var ctrlActions = new ControlActions();
            sessionStorage.setItem('usuario', JSON.stringify(response.Data));
            sessionStorage.setItem('idUsuario', response.Data["Identificacion"]);
            sessionStorage.setItem('rolesUsuario', response.Data["Roles"]);
            sessionStorage.setItem('correoUsuario', response.Data["Correo"]);
            sessionStorage.setItem('nombreUsuario', response.Data["PrimerNombre"] + " " +
                response.Data["PrimerApellido"] + " " + response.Data["SegundoApellido"]);


            $.ajax({
                type: "POST",
                url: '/home/SetUser',
                data: response.Data,
                dataType: 'json',
                success: window.location.href = inicio
            }).done(function () {
                window.location = inicio;
            });

        })
            .fail(function (response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
            })
    };

    this.GetRolesUsuario = function () {
        let roles = window.sessionStorage.getItem("rolesUsuario");
        return roles != null ? roles.split(',') : null;
	};

	this.GetIDToAPI = function (url) {
		var response = {};
		try {
			$.ajax({
				type: "GET",
				url: url,
				cache: false,
				async: false,
				success: function (data) {
					response = data['Data'];
				}
			});
		} catch (err) {
			//console.log(err);
		}
		return response;
	};

    $.validator.addMethod("contrasenna", function (value, element) {
        return this.optional(element) || /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{6,})/i.test(value);
    }, "Debe contener al menos una letra minúscula, una letra mayúscula, un número, un caracter especial(!@#\$%\^&\*), y tener al menos 6 dígitos");

    $.validator.addMethod("EMAIL", function (value, element) {
        return this.optional(element) || /^[a-zA-Z0-9._-]+@[a-zA-Z0-9-]+\.[a-zA-Z.]{2,5}$/i.test(value);
    }, "Por favor, escribe una dirección de correo válida.");

    $.validator.addMethod("identificacion", function (value, element) {
        return this.optional(element) || /^[a-zA-Z0-9]+$/i.test(value);
    }, "Digite únicamente números o letras.");

    this.ReporteGananciasAdmin = function (service, tipoReporte, callBackFunction) {
        var jqxhr = $.get(this.GetUrlApiService(service) + "/" + tipoReporte, function (response) {
            callBackFunction(response.Data);
        });
    }

    this.ReporteGananciasGerente = function (service, tipoReporte, id, callBackFunction) {
        var jqxhr = $.get(this.GetUrlApiService(service) + "/" + tipoReporte + "/" + id, function (response) {
            callBackFunction(response.Data);
        });
    }

    this.GetUsuarioBitacora = function (data) {
        //datos necesarios pra bitacora
        if (sessionStorage.getItem("correoUsuario") != null) {
            var rol = null;
            var roles = sessionStorage.getItem("rolesUsuario");
            if (roles.includes('ADM')) {
                rol = ["ADM"];
            } else if (roles.includes('GRT')) {
                rol = ["GRT"];
            } else if (roles.includes('CLT')) {
                rol = ["CLT"];
            } else {
                rol = ["EMP"];
            }
            data["CorreoUB"] = sessionStorage.getItem("correoUsuario");
            data["RolB"] = rol;
        } else {

        }
        return data;
	};

	this.GetCarrito = function (service, data, callbackFunction) {
		var jqxhr = $.post(this.GetUrlApiService(service), data, function (response) {
			var ctrlActions = new ControlActions();
			callbackFunction(response);
		})
			.fail(function (response) {
				var data = response.responseJSON;
				var ctrlActions = new ControlActions();
				ctrlActions.ShowMessage('E', data.ExceptionMessage);
			})
	}

	this.GetLlaveQR = function (service, data, callbackFunction) {
	    var datos = this.GetUsuarioBitacora(data);
	    var jqxhr = $.post(this.GetUrlApiService(service), datos, function (response) {
	        var ctrlActions = new ControlActions();
	        ctrlActions.ShowMessage('I', response.Message);

	        callbackFunction(response.Message);
	    })
            .fail(function (response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
            })
	}
}

//Custom jquery actions
$.put = function (url, data, callback) {
    if ($.isFunction(data)) {
        type = type || callback,
            callback = data,
            data = {}
    }
    return $.ajax({
        url: url,
        type: 'PUT',
        success: callback,
        data: JSON.stringify(data),
        contentType: 'application/json'
    });
}
//$.post = function (url, data, callback) {
//    if ($.isFunction(data)) {
//        type = type || callback,
//            callback = data,
//            data = {}
//    }
//    return $.ajax({
//        url: url,
//        type: 'POST',
//        success: callback,
//        data: JSON.stringify(data),
//        contentType: 'application/json'
//    });
//}

$.delete = function (url, data, callback) {
    if ($.isFunction(data)) {
        type = type || callback,
            callback = data,
            data = {}
    }
    return $.ajax({
        url: url,
        type: 'DELETE',
        success: callback,
        data: JSON.stringify(data),
        contentType: 'application/json'
    });
}

$.postFoto = function (url, data, callback) {
    if ($.isFunction(data)) {
        type = type || callback,
            callback = data,
            data = {}
    }
    return $.ajax({
        url: url,
        type: 'POST',
        success: callback,
        data: JSON.stringify(data),
        contentType: 'application/json'
    });
}
