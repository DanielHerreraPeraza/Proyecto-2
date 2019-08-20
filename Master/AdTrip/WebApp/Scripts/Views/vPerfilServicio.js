function vPerfilServicio() {

    this.service = 'servicio';
    this.ctrlActions = new ControlActions();


    this.LlenarInformacionServicio = function (data) {
        if (data != null) {
            servicioData = {};
            document.querySelector('.nombreServicio').append(data['Nombre']);
            document.querySelector('.nombreServicioItem').append(data['Nombre']);
            document.getElementById('descripcionServicio').append(data['Descripcion']);
            $('#fotoServicio').attr('src', (data['FotoPerfil']));
            servicioData.IdHotel = sessionStorage.getItem('IdHotel');
            sessionStorage.setItem('idServicio', data['Codigo']);
        }
    }


  


    this.LlenarCardsProductos = function (data) {

        if (data != null) {
            var table = $("#tblProductos").DataTable({
                "processing": true, "language": {
                    "url": "https://cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
                }
            });

            for (let i = 0; i < data.length; i++) {


                let cardProducto = '<div class="list-block main-block t-list-block"> ' +
                    '                  <div class="list-content" style=" max-width: 350px, max-heigth: 200px;" >' +
                    '                       <div class="main-img list-img t-list-img" >' +
                    '                              <img style="width:100%" src="' + data[i]['Foto'] + '" class="img-fluid fotoProducto" alt="Foto del producto"/> ' +
                    '                              <div class="main-mask"> ' +
                    '                                 <ul class="list-unstyled list-inline offer-price-1"> ' +
                    '                                     <li class="list-inline-item price" id="precioProducto"> $' + data[i]['Precio'] + '</li> ' +
                    '                                  </ul>' +
                    '                              </div>' +
                    '                      </div>' +
                    '                      <div class="list-info t-list-info"> ' +
                    '                           <h3 class="block-title">' + data[i]['Nombre'] + '</h3>' +
                    '                           <p class="block-minor categoriaProducto">' + data[i]['Categoria'] + '</p> ' +
                    '                           <p class="descripcionProducto">' + data[i]['Descripcion'] + '</p>' +
                    '                           <a href="https://adtripapp.azurewebsites.net/Home/vListadoProductos"  class="btn btn-color btn-lg">Comprar productos</a> ' +
                    '                           <p class="block-minor"> Impuesto ' +
                    '                              <a class="tipoImpuesto">' + data[i]['TipoImpuesto'] + '</a>' +
                    '                              cuyo valor es: ' +
                    '                              <a class="impuesto" > ' + data[i]['Impuesto'] + '%</a>' +
                    '                          </p> ' +
                    '                       </div>' +
                    '                    </div>' +
                    '                </div>';



                table.row.add([
                    cardProducto
                ]).draw(false);
            }

        } else {
            swal("Error de datos", "No hay productos", "error");
            table.row.add([
                '<div > ' +
                '     <h3> No hay productos</h3>     '+
                ' </div>'

            ]).draw(false);
        }
    }


    this.LlenarFotosServicios = function (data) {
        for (var i in data) {
            let cardFoto =  '  <div class="gallery-product col-12 col-md-6 col-lg-4 col-xl-3">' +
                            '      <div class="gallery-block" >' +
                            '          <div class="gallery-img ">' +
                            '                <img src="' + data[i].UrlFoto + '" class="img-fluid img-servicios" alt="' + data[i].UrlFoto + '">' +
                            '                <div class="gallery-mask">'+
                            '                    <a href="' + data[i].UrlFoto + '" title="' + data[i].UrlFoto + '" class="with-caption image-link">'+
                            '                      <span> '+
                            '                           <i class="fa fa-arrows-v" ></i> '+
                            '                       </span> '+
                            '                    </a> ' +
                            '                </div><!-- end gallery-mask -->'+
                            '         </div><!-- end gallery-img -->'+
                            '      </div> <!--end gallery - block-- >´'+
                            '  </div >< !--end gallery - product-- >';

            $('#galeriaServicios').append(cardFoto);
        }
    }


    this.LlenarDatos = function () {
        var idServicio = $('#txtIdServicio').val();

        if (idServicio != 'nulo') {
                
            this.ctrlActions.GetById("servicio/traerUno/" + idServicio, this.LlenarInformacionServicio);
            this.ctrlActions.GetById("producto/" + idServicio, this.LlenarCardsProductos);
            this.ctrlActions.GetById("foto/Servicio/" + idServicio, this.LlenarFotosServicios);
        }
    }
}


$(document).ready(function () {

    var vperfilservicio = new vPerfilServicio();
    vperfilservicio.LlenarFotosServicios();

});    


function VolverPerfilrHotel() {
    window.location = "https://adtripapp.azurewebsites.net/Home/vPerfilHotel/" + sessionStorage.getItem('idHotelLogeado');
}