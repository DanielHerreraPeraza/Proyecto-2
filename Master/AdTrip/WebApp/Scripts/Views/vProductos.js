
function vProductos() {

    this.tblProductosId = 'tblProductos';
    this.service = 'producto';
    this.ctrlActions = new ControlActions();
    this.columns = "Codigo,Nombre,Precio,CantProductos,Categoria,TipoImpuesto,Impuesto,Estado";

    this.RetrieveAll = function () {

       var idServicio = sessionStorage.getItem('IdServicio');
        this.ctrlActions.FillTableId(this.service, this.tblProductosId, idServicio, false);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblProductosId, true);
    }


    this.LlenarCategorias = function () {

        var control = new ControlActions();
        control.GetById('categoria', function (categorias) {
            $(categorias).each(function (index, value) {
                if (value.IdEstado == "1") {
                    $('#dpCategorias').append("<option  value= '" + value.Codigo + "'> " + value.Nombre + "</option> ");
                    $('#dpModCategorias').append("<option  value= '" + value.Codigo + "'> " + value.Nombre + "</option> ");
                }
            });
        })
    }

    this.LlenarImpuestos = function () {
        var control = new ControlActions();
        control.GetById('impuesto', function (impuestos) {
            $(impuestos).each(function (index, value) {
                if (value.IdEstado == "1") {
                    $('#dpTipoImpuesto').append("<option  value= '" + value.Codigo + "'> " + value.Nombre + " ( " + value.Porcentaje + "% ) </option> ");
                    $('#dpModTipoImpuesto').append("<option  value= '" + value.Codigo + "'> " + value.Nombre + " ( " + value.Porcentaje + "% ) </option> ");
                }
            });
        })
    }


    this.Create = function () {

        var productoData = {};
        productoData = this.ctrlActions.GetDataForm('frmCreate');
        productoData.IdServicio = sessionStorage.getItem('IdServicio');
        productoData.IdCategoria = $('#dpCategorias').val();
        productoData.IdTipoImpuesto = $('#dpTipoImpuesto').val();
        productoData.Foto = $('#fotoProducto').attr('src');
        productoData["IdHotelB"] = sessionStorage.getItem('idHotelLogeado');
        //Hace el post al create
        this.ctrlActions.PostToAPI(this.service, productoData, function () {
            //Refresca la tabla
            var vproducto = new vProductos();

            vproducto.ReloadTable();
            $('.nav-tabs li').click();
            vproducto.limpiar();
        });

    }

    this.Update = function () {

        var productoData = {};
        productoData = this.ctrlActions.GetDataForm('frmEdition');
        //Hace el post al create
        productoData.IdServicio = sessionStorage.getItem('IdServicio');
        productoData.Foto= $("#modFotoProducto").attr('src');
        productoData.IdCategoria = $("#dpModCategorias").val();
        productoData.IdTipoImpuesto = $("#dpModTipoImpuesto").val();
        productoData.Estado = $("#txtEstado").val();
        productoData["IdHotelB"] = sessionStorage.getItem('idHotelLogeado');
        this.ctrlActions.PutToAPI(this.service, productoData, function () {
            //Refresca la tabla
            var vproducto = new vProductos();

            vproducto.ReloadTable();
            $('.nav-tabs li').click();
            vproducto.limpiarFormulario();
          
        });
    }

    this.Delete = function () {

        var productoData = {};
        productoData = this.ctrlActions.GetDataForm('frmEdition');
        productoData.FotoPerfil = $('#modFotoProducto').attr('src');
        productoData["IdHotelB"] = sessionStorage.getItem('idHotelLogeado');
        //Hace el post al create
        this.ctrlActions.DeleteToAPI(this.service, productoData, function () {

            this.ReloadTable();
        });
    }

    this.BindFields = function (data) {
        this.ctrlActions.BindFields('frmEdition', data);
        $('#modFotoProducto').attr("src", data.Foto);
        $('#modFotoProducto').attr("alt", data.Foto);

        if (data.IdEstado == "0") {
            $("#txtEstado option[value= 0 ]").prop('selected', true);
        } else if (data.IdEstado == "1") {
            $("#txtEstado option[value= 1 ]").prop('selected', true);
        }

        $("#dpModCategorias option[value= " + data.IdCategoria + " ]").prop('selected', true);
        $("#dpModTipoImpuesto option[value= " + data.IdTipoImpuesto + " ]").prop('selected', true);

        $('#modCodigo').prop('disabled', true);
        this.esconderListar();
        this.esconderCrear();
        this.mostrarModificar();
    }

    this.AgregarFoto = function () {

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

                var image = document.getElementById("fotoProducto");
                image.src = cloudinaryURL + id;
            }
        )

    };
    


    this.ModFotoPerfil = function () {

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

                var image = document.getElementById("modFotoProducto");
                image.src = cloudinaryURL + id;
            }
        )

    };

    function processImage(id) {
        let options = {
            client_hints: true,
        };
        return $.cloudinary.url(id, options);
    }

    this.IrListaServicios = function () {
        window.location = "vServicios";
    }

    this.ValidarInputs = function () {
        let lstInputs = document.querySelectorAll("#frmCreate input[ColumnDataName]");
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
            this.Create();
        } else {
            swal("Campos Requeridos", "Campos vacios: Por favor, llenar todos los campos", "warning");
        }
    }

    this.limpiar = function () {
        document.querySelector('#txtCodigo').value = '';
        document.querySelector('#txtNombre').value = '';
        document.querySelector('#txtDescripcion').value = '';
        document.getElementById("imagenesServicio").closest('tr').remove();
    }


    this.tab_click = function (ctl) {
        this.tab_handling(ctl);

        div = $(ctl).attr("data-div");

        if (div == "listaProductos") {
            this.mostrarListar();
            this.esconderCrear();
            this.esconderModificar();
        }
        else if (div == "registrarProductos") {
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
        $("#listaProductos").removeClass("d-none");
        $("#listaProductos").addClass("d-inline");
    }

    this.esconderListar = function () {
        $("#listaProductos").removeClass("d-inline");
        $("#listaProductos").addClass("d-none");
    }

    this.mostrarCrear = function () {
        $("#registrarProductos").removeClass("d-none");
        $("#registrarProductos").addClass("d-inline");
    }

    this.esconderCrear = function () {
        $("#registrarProductos").removeClass("d-inline");
        $("#registrarProductos").addClass("d-none");
    }

    this.mostrarModificar = function () {
        $("#modificarProductos").removeClass("d-none");
        $("#modificarServicios").addClass("d-inline");
    }

    this.esconderModificar = function () {
        $("#modificarProductos").removeClass("d-inline");
        $("#modificarProductos").addClass("d-none");
    }

    this.limpiarFormulario = function () {
        $("#frmEdition")[0].reset();
    }
}



$(document).ready(function () {

    var vproductos = new vProductos();
    vproductos.RetrieveAll();

    vproductos.initializepanel();
    vproductos.LlenarCategorias();
    vproductos.LlenarImpuestos();

    $(".nav-tabs li").click(function () {
        vproductos.tab_click($(this));
    });


    $("#txtPrecio").on({
        "focus": function (event) {
            $(event.target).select();
        },
        "keyup": function (event) {
            $(event.target).val(function (index, value) {
                return value.replace(/\D/g, "")
                    .replace(/\B(?=(\d{2})+(?!\d)\.?)/g, ",");
            });
        }
    });

    $("#txtCantProductos").on({
        "focus": function (event) {
            $(event.target).select();
        },
        "keyup": function (event) {
            $(event.target).val(function (index, value) {
                return value.replace(/\D/g, "")
                    .replace(/\B(?=(\d{3})+(?!\d)\.?)/g);
            });
        }
    });


    $("#modPrecio").on({
        "focus": function (event) {
            $(event.target).select();
        },
        "keyup": function (event) {
            $(event.target).val(function (index, value) {
                return value.replace(/\D/g, "")
                    .replace(/\B(?=(\d{2})+(?!\d)\.?)/g, ",");
            });
        }
    });

    $("#modCantProductos").on({
        "focus": function (event) {
            $(event.target).select();
        },
        "keyup": function (event) {
            $(event.target).val(function (index, value) {
                return value.replace(/\D/g, "")
                    .replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".");
            });
        }
    });


    document.querySelector("#txtCantProductos").setAttribute("min", 0);
    document.querySelector("#txtCantProductos").setAttribute("max", 100);
    document.querySelector("#modCantProductos").setAttribute("min", 0);
    document.querySelector("#modCantProductos").setAttribute("max", 100);
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
            txtCodigo: {
                required: true
            },
            txtNombre: {
                required: true
            },
            txtDescripcion: {
                required: true
            },
            txtProveedor: {
                required: true
            },
            txtPrecio: {
                required: true
            },
            txtCantProductos: {
                required: true
            },
            dpCategoria: {
                required: true
            },
            dpTipoImpuesto: {
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
            txtProveedor: {
                required: true
            },
            txtPrecio: {
                required: true
            },
            txtCantProductos: {
                required: true
            },
            dpCategoria: {
                required: true
            },
            dpTipoImpuesto: {
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