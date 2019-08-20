function vHoteles() {
    this.service = 'hotel';
    this.ctrlActions = new ControlActions();
    this.DatosHotel;

    this.CargarCardHoteles = function (data) {
        if (data != null) {
            var table = $(tblHoteles).DataTable({
                "processing": true, "language": {
                    "url": "https://cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
                }});

			for (let i = 0; i < data.length; i++) {
                let clasi = data[i]['Clasificacion'];
                let claHtml;
                if (clasi == 1) {
                    claHtml = '<li class="list-inline-item rating" >'
                        + ' <span><i class="fa fa-star orange"></i></span>'
                        + ' <span><i class="fa fa-star lightgrey"></i></span>'
                        + ' <span><i class="fa fa-star lightgrey"></i></span>'
                        + ' <span><i class="fa fa-star lightgrey"></i></span>'
                        + ' <span><i class="fa fa-star lightgrey"></i></span>'
                        + '</li> ';

                } else if (clasi == 2) {
                    claHtml = '<li class="list-inline-item rating" >'
                        + ' <span><i class="fa fa-star orange"></i></span>'
                        + ' <span><i class="fa fa-star orange"></i></span>'
                        + ' <span><i class="fa fa-star lightgrey"></i></span>'
                        + ' <span><i class="fa fa-star lightgrey"></i></span>'
                        + ' <span><i class="fa fa-star lightgrey"></i></span>'
                        + '</li> ';

                } else if (clasi == 3) {
                    claHtml = '<li class="list-inline-item rating" >'
                        + ' <span><i class="fa fa-star orange"></i></span>'
                        + ' <span><i class="fa fa-star orange"></i></span>'
                        + ' <span><i class="fa fa-star orange"></i></span>'
                        + ' <span><i class="fa fa-star lightgrey"></i></span>'
                        + ' <span><i class="fa fa-star lightgrey"></i></span>'
                        + '</li> ';

                } else if (clasi == 4) {
                    claHtml = '<li class="list-inline-item rating" >'
                        + ' <span><i class="fa fa-star orange"></i></span>'
                        + ' <span><i class="fa fa-star orange"></i></span>'
                        + ' <span><i class="fa fa-star orange"></i></span>'
                        + ' <span><i class="fa fa-star orange"></i></span>'
                        + ' <span><i class="fa fa-star lightgrey"></i></span>'
                        + '</li> ';

                } else {

                    claHtml = '<li class="list-inline-item rating" >'
                        + ' <span><i class="fa fa-star orange"></i></span>'
                        + ' <span><i class="fa fa-star orange"></i></span>'
                        + ' <span><i class="fa fa-star orange"></i></span>'
                        + ' <span><i class="fa fa-star orange"></i></span>'
                        + ' <span><i class="fa fa-star orange"></i></span>'
                        + '</li> ';

                }

                let cardHotel = '<div class="list-block main-block h-list-block">' +
                    ' <div class="list-content">' +
                    '   <div class="main-img list-img h-list-img mh-100x" style=" max-height: 300px;">' +
                    '     <a href="/Home/vPerfilHotel/' + data[i]['CedulaJuridica'] + '">' +
                    ' <img src="' + data[i]['FotoPerfil'] + '" class="img-fluid" alt="hotel-img" />' +
                    '      </a>' +
                    '   <div class="main-mask">' +
                    '      <h5 class="page-item offer-price-1"  style="color: white" id="P_Cla' + data[i]['Clasificacion'] + '_' + data[i]['CedulaJuridica'] + '">' +
                    '<li class="list-inline-item " >Clasificación: '+ data[i]['Clasificacion'] + ' </li>' +
                    claHtml +
                    ' </h5>' +
                    '    </div><!-- end main-mask -->' +
                    ' </div><!-- end h-list-img -->' +
                    '<div class="list-info h-list-info">' +
                    '  <h3 class="block-title"><a href="/Home/vPerfilHotel/' + data[i]['CedulaJuridica'] + '">' + data[i]['Nombre'] + '</a></h3>' +
                    ' <p class="block-minor">Lugar: ' + data[i]['vProvincia'] + ', ' + data[i]['vCanton'] + '</p>' +
                    '  <p>' + data[i]['Descripcion'] + '</p>' +		
					'  <a href="/Home/vPerfilHotel/' + data[i]['CedulaJuridica'] + '" class="btn btn-orange btn-lg" '+
                    '    style="background-color: #00CCCC; color: white">Ver más</a>' +
                    ' </div><!-- end h-list-info -->' +
                    '</div><!-- end list-content -->' +
					'</div> <!--end h - list - block-- >';

				/*
				id = "'+data[i]['CedulaJuridica'] 
				var button = $(data[i]['CedulaJuridica']);

				button.onclick = function () {
					sessionStorage.setItem("IdHotelLogeado", data[i]['CedulaJuridica']);
				}; */
			



                table.row.add([
                    cardHotel
                ]).draw(false);
            }

        } else {
            swal("Error de datos", "No hay hoteles", "error");

        }
    }

    this.RetrieveAllHoteles = function () {
        var usuario = JSON.parse(window.sessionStorage.getItem("usuario"));
        if (usuario != null) {
            this.ctrlActions.GetById("hotel/usuario/" + usuario['Identificacion'], this.CargarCardHoteles);
        } else {
            this.ctrlActions.GetById("hotel/usuario/visitante", this.CargarCardHoteles);
        }

    }



    this.BindFields = function (data) {


    }

    this.UsuarioLogeado = function () {

        var usuario = JSON.parse(window.sessionStorage.getItem("usuario"));
        if (usuario != null) {

        } else {
            this.Logeado = -1;
        }

    }


}
$(document).ready(function () {
    var vhoteles = new vHoteles();
    vhoteles.RetrieveAllHoteles();
});