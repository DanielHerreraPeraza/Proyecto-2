

function vServicios() {

    this.tblServiciosId = 'tblServicios';
    this.service = 'servicio';
    this.ctrlActions = new ControlActions();
	this.columns = "Codigo,Nombre,Descripcion,Estado";

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblServiciosId, false);
    }
       
    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblServiciosId, true);
    }

    this.RetrieveAllById = function () {
        this.ctrlActions.FillTableId(this.service, this.tblServiciosId, sessionStorage.getItem('idHotelLogeado'), false);
    }
  
    this.Create = function () {
        
        var servicioData = {};
        var fotosServicios = {};

        servicioData = this.ctrlActions.GetDataForm('frmCreate');
        servicioData.IdHotel = sessionStorage.getItem('idHotelLogeado');
        servicioData.FotoPerfil = $('#fotoServicio').attr('src');
        //Hace el post al create

        fotosServicios = this.ctrlActions.GetDataFotos('imagenesServicio');

        if (fotosServicios.length != 0) {

            servicioData["IdHotelB"] = sessionStorage.getItem('idHotelLogeado'); 
            this.ctrlActions.PostToAPI(this.service, servicioData, function () {

                $(fotosServicios).each(function (index, value) {
                    value.IdEntidad = $('#txtCodigo').val();
                    value.Entidad = 'Servicio';
                    var control = new ControlActions();

                    control.PostToAPI('foto', value, function () {
                        var vservicio = new vServicios();
                        vservicio.ReloadTable();
                        vservicio.limpiarFormulario();
                        $('.nav-tabs li').click();
                    });
                });

               
            });
        } else {
            swal('No se puede continuar con el registro', 'Para completar el registro, debe ingresar imágenes al servicio', 'warning');
        }
       
    }

    this.CrearFoto = function (servicioFoto, foto ) {

        this.crlAction.PostToAPI(servicioFoto, foto, function () {
            var vservicio = new vServicios();
            vservicio.ReloadTable();
            $('.nav-tabs li').click();
            vservicio.limpiar();
            vservicio.limpiarTablaFotos();
        });
    }


    this.Update = function () {

        var servicioData = {};
        servicioData = this.ctrlActions.GetDataForm('frmEdition');
        servicioData.IdHotel = sessionStorage.getItem('idHotelLogeado');
        servicioData.FotoPerfil = $('#modFotoServicio').attr('src');
        servicioData.Estado = $('#txtEstado').val();
        fotosServicios = this.ctrlActions.GetDataFotos('modImgsServicio');

        //Hace el post al create
        servicioData["IdHotelB"] = sessionStorage.getItem('idHotelLogeado'); 
        this.ctrlActions.PutToAPI(this.service, servicioData, function () {


            $(fotosServicios).each(function (index, value) {
                value.IdEntidad = $('#modCodigo').val();
                value.Entidad = 'Servicio';
                var control = new ControlActions();

                control.PostToAPI('foto', value, function () {

                });
            });

            var vservicio = new vServicios();
            vservicio.ReloadTable();
            $('.nav-tabs li').click();
            vservicio.limpiar();
            vservicio.limpiarTablaFotos();

        });
    }

   this.Delete = function () {

        var servicioData = {};
        servicioData = this.ctrlActions.GetDataForm('frmEdition');
        servicioData.FotoPerfil = $('#fotoServicio').attr('src');

        //Hace el post al create
       servicioData["IdHotelB"] = sessionStorage.getItem('idHotelLogeado'); 
        this.ctrlActions.DeleteToAPI(this.service, servicioData, function () {

            var vservicio = new vServicios();
            vservicio.ReloadTable();
            vservicio.limpiarFormulario();
        });
        //Refresca la tabla
        //this.ReloadTable();
    }

    this.BindFields = function (data) {

        this.ctrlActions.BindFields('frmEdition', data);
       
        this.ctrlActions.GetById("foto/Servicio/" + $('#modCodigo').val(), this.MostrarFotosServicios);

        $('#modFotoServicio').attr("src", data.FotoPerfil);
        $('#modFotoServicio').attr("alt", data.FotoPerfil);
      
        document.getElementById("txtEstado").value = data['IdEstado'];

        $('#modCodigo').prop('disabled', true);
        this.esconderListar();
        this.esconderCrear();
        this.mostrarModificar();

       

    }
    
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

                var nColumnas = $("#imagenesServicio tr:last td").length;

                if (nColumnas == 0) {
                    $("#imagenesServicio").append(" <tr> <td> <img  style ='min-width:150px; max-width:150px; max-height:150px; min-height:150px;' class = 'ImagenServicio' src= '"
                        + imagenUrl + "'  alt= '" + id + "'   />  </td> </tr>");
                } else  if (nColumnas == 3) {
                    $("#imagenesServicio").append(" <tr> <td> <img style ='min-width:150px; max-width:150px; max-height:150px; min-height:150px;' class = 'ImagenServicio' src= '"
                        + imagenUrl + "'  alt= '" + id + "'   />  </td> </tr>");
                    
           
                } else if (nColumnas < 3 && nColumnas > 0 ) {
                    $("#imagenesServicio tr:last").append(" <td> <img style ='min-width:150px; max-width:150px; max-height:150px; min-height:150px;' id = 'ImagenServicio' src= '"
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
                    $("#modImgsServicio").append(" <tr> <td> <img  style ='min-width:150px; max-width:150px; max-height:150px; min-height:150px;' class = 'ImagenServicio' src= '"
                        + imagenUrl + "'  alt= '" + id + "'   />  </td> </tr>");
                } else if (nColumnas == 3) {
                    $("#modImgsServicio").append(" <tr> <td> <img style ='min-width:150px; max-width:150px; max-height:150px; min-height:150px;' class = 'ImagenServicio' src= '"
                        + imagenUrl + "'  alt= '" + id + "'   />  </td> </tr>");

                } else if (nColumnas < 3 && nColumnas > 0) {
                    $("#modImgsServicio tr:last").append(" <td> <img style ='min-width:150px; max-width:150px; max-height:150px; min-height:150px;' id = 'ImagenServicio' src= '"
                        + imagenUrl + "'  alt= '" + id + "'   />  </td>");
                }
            });
    }

   
    function processImage(id) {

        let options = {
            client_hints: true,

        };

        return $.cloudinary.url(id, options);

    }

    this.AgregarFotoPerfil = function () {

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

                var image = document.getElementById("fotoServicio");
                image.src = cloudinaryURL + id;
            }
        )

    };
    

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

    this.IrPerfilServicio = function () {

        var servicioData = {};
        servicioData = this.ctrlActions.GetDataForm('frmEdition');
        servicioData.IdHotel = sessionStorage.getItem('IdHotel');
        servicioData.FotoPerfil = $('#modFotoServicio').attr('src');

        sessionStorage.setItem('IdServicio', servicioData.Codigo);
        sessionStorage.setItem('NombreServicio', servicioData.Nombre);
        sessionStorage.setItem('DescripcionServicio', servicioData.Descripcion);
        sessionStorage.setItem('FotoServicio', servicioData.FotoPerfil);

        window.location = "/Home/vPerfilServicio/" + servicioData.Codigo;
    }


    this.VerProductos = function () {
        var servicioData = {};
        servicioData = this.ctrlActions.GetDataForm('frmEdition');
        sessionStorage.setItem('IdServicio', servicioData.Codigo);
        window.location = "/Home/vProductos";
    }

    this.limpiar = function () {
        document.querySelector('#txtCodigo').value = '';
        document.querySelector('#txtNombre').value = '';
        document.querySelector('#txtDescripcion').value = '';
    }

    this.limpiarTablaFotos = function () {
        $('#imagenesServicio tr').remove();
        $('#modImgsServicio tr').remove();
    }

    this.tab_click = function (ctl) {
        this.tab_handling(ctl);

        div = $(ctl).attr("data-div");

        if (div == "listaServicios") {
            this.mostrarListar();
            this.esconderCrear();
            this.esconderModificar();
        }
        else if (div == "registrarServicios") {
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
        $("#listaServicios").removeClass("d-none");
        $("#listaServicios").addClass("d-inline");
    }

    this.esconderListar = function () {
        $("#listaServicios").removeClass("d-inline");
        $("#listaServicios").addClass("d-none");
    }

    this.mostrarCrear = function () {
        $("#registrarServicios").removeClass("d-none");
        $("#registrarServicios").addClass("d-inline");
    }

    this.esconderCrear = function () {
        $("#registrarServicios").removeClass("d-inline");
        $("#registrarServicios").addClass("d-none");
    }

    this.mostrarModificar = function () {
        $("#modificarServicios").removeClass("d-none");
        $("#modificarServicios").addClass("d-inline");
    }

    this.esconderModificar = function () {
        $("#modificarServicios").removeClass("d-inline");
        $("#modificarServicios").addClass("d-none");
        $('#galeriaServicios div').remove();
    }

    this.limpiarFormulario = function () {
        $("#frmEdition")[0].reset();

    }

  

    this.LlenarFotosServicios = function (data) {
        if (data.length != 0) {
            for (var i in data) {
                let cardFoto = '  <div class="gallery-product col-12 col-md-6 col-lg-4 col-xl-3">' +
                    '      <div class="gallery-block" >' +
                    '          <div class="gallery-img ">' +
                    '                <img src="' + data[i].UrlFoto + '" class="img-fluid img-servicios" alt="' + data[i].UrlFoto + '">' +
                    '         </div><!-- end gallery-img -->' +
                    '      </div> <!--end gallery - block-- >´' +
                    '  </div >< !--end gallery - product-- >';

                $('#galeriaServicios').append(cardFoto);
            }
        } else {
            $('#galeriaServicios div').remove();
        }
    }

    this.MostrarFotosServicios = function (data) {
        if (data.length != 0) {
            for (var i in data) {
                let cardFoto =  '  <div class="gallery-product col-12 col-md-6 col-lg-4 col-xl-3">' +
                                '      <div class="gallery-block">' +
                                '          <div class="gallery-img ">' +
                                '                <img src="' + data[i].UrlFoto + '" class="img-fluid img-servicios" alt="' + data[i].UrlFoto + '">' +
                                '                <div class="gallery-mask" style="height:30%; top:0%; rigth:0; transition:none;">' +
                                '                    <a id="' + data[i].UrlFoto + '" name= "' + data[i].IdEntidad + '" title="Eliminar foto" class="image-link eliminar" style="transform:none; top:6%; rigth:10px; margin-top:4%;">' +
                                '                      <span> ' +
                                '                           <i class="fa fa-trash"></i> ' +
                                '                       </span> ' +
                                '                    </a> ' +
                                '                </div><!-- end gallery-mask -->' +
                                '         </div><!-- end gallery-img -->' +
                                '      </div> <!--end gallery - block-->' +
                    '  </div ><!--end gallery-product-->';

                $('#galeriaServicios').append(cardFoto);
            }
        } else {
            $('#galeriaServicios div').remove();
            $('#galeriaServicios').append('<div style="margin:4%; margin-left:28%;" class="page-heading vServicio">' +
                                          '   <h4>Este servicio no tiene fotos registradas</h4>' +
                                          '</div>'
            );
        }
    }


    

}

function processImage(id) {
    let options = {
        client_hints: true,
    };
    return $.cloudinary.url(id, options);
}

$(document).ready(function () {

    var vservicio = new vServicios();
    vservicio.RetrieveAllById();

    vservicio.initializepanel();
    $(".nav-tabs li").click(function () {
        vservicio.tab_click($(this));
    });

    ReglasValidacionModificar();
    ReglasValidacionCrear();

    $('.nombreHotel').append(sessionStorage.getItem('nombreHotelLogeado'));

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

function VolverModificarHotel() {
    window.location = "https://adtripapp.azurewebsites.net/Home/vModificarHotel/" + sessionStorage.getItem('idHotelLogeado');
}



$(function () {
    $(document).on('click', 'a.eliminar', function (event) {
        let id = this.id;
        let name = this.name;
        var control = new ControlActions();
        var vservicios = new vServicios();

        var fotoData = {};

        fotoData.Entidad = 'Servicio';
        fotoData.IdEntidad = name;
        fotoData.UrlFoto = id;
        control.DeleteToAPI('foto', fotoData, function () {
            $('#galeriaServicios div').remove();

            control.GetById("foto/Servicio/" + name, vservicios.MostrarFotosServicios);

        })

    })

});



function ModFotoPerfil() {

    let imagenUrl = '';

    // with credentials available on
    // your Cloudinary account dashboard
    $.cloudinary.config({ cloud_name: 'datatek', api_key: '678592257184526' });

    let cloudinaryURL = 'https://res.cloudinary.com/datatek/image/upload/c_scale,w_120/v1562616902/';
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