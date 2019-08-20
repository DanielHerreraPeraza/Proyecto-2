function vModificarHotel() {
    this.service = 'hotel';
    this.ctrlActions = new ControlActions();
    this.hotel;
    $.cloudinary.config({ cloud_name: 'datatek', api_key: '678592257184526' }); // Upload button
    let cloudinaryURL = 'https://res.cloudinary.com/datatek/image/upload/';


    this.Update = function () {
        var hotelData = {};
        var id = document.getElementById("txtHotel").value;
        hotelData = this.ctrlActions.GetDataForm('frmEdition');
        hotelData["IdHotelB"] = id;
        this.ctrlActions.PutToAPI(this.service, hotelData,
            setTimeout(function redirection() { window.location.href = '/Home/vPerfilHotel/' + id }, 3000));
        
        
    }
    this.CargarHotel = function (data) {
        if (data != null) {

            document.getElementById("txtCedulaJ").value = data['CedulaJuridica'];
            document.getElementById("txtCedulaJ").setAttribute('disabled', '');
            document.getElementById("txtIdGerente").value = data['IdGerente'];
            document.getElementById("txtIdGerente").setAttribute('disabled', '');

            clasi = data['Clasificacion'];
            if (clasi>0) {
                document.getElementById("txtClasificacion").value = data['Clasificacion'];
            } else {
                document.getElementById("txtClasificacion").value = 1;
            }
            
            document.getElementById("txtNombre").value = data['Nombre'];
            document.getElementById("txtCadena").value = data['Cadena'];
            document.getElementById("txtDescripcion").value = data['Descripcion'];
            document.getElementById("txtDireccion").value = data['Direccion'];

            document.getElementById("txtProvincia").value = data['Provincia'];
            document.getElementById("txtCanton").value = data['Canton'];
            document.getElementById("txtDistrito").value = data['Distrito'];
            document.getElementById("txtCorreo").value = data['Correo'];
            document.getElementById("txtTelefonos").value = data['Telefonos'];
            document.getElementById("txtSitio").value = data['URLSitio'];

            //document.getElementById("txtEstado").value = data['Estado'];

            document.getElementById("mngFoto").src = data['FotoPerfil'];
            var imageLogo = document.getElementById("mngFoto");
            document.getElementById("txtFoto").value = imageLogo.src;

            document.getElementById("mngLogo").src = data['Logo'];
            var imageFoto = document.getElementById("mngLogo");
            document.getElementById("txtLogo").value = imageFoto.src;


            //document.getElementById("txtLatitud").value = data['UbicacionX'];
            //document.getElementById("txtLongitud").value = data['UbicacionY'];
            //9.933292611992531
            //-84.05738587440794
            let lati = data['UbicacionX'];
            let longi = data['UbicacionY'];

            $('#Gmap').locationpicker({
                radius: 0,
                location: {
                    latitude: lati,
                    longitude: longi
                },
                enableAutocComplete: true,
                inputBinding: {

                    latitudeInput: $('#txtLatitud'),
                    longitudeInput: $('#txtLongitud')

                },

                markerIcon: '/Content/images/iconMarker.png',
                markerDraggable: true,
                markerVisible: true

            });


        } else {
            swal("Error de datos", "El hotel no existe", "error");

            setTimeout(function redirection() { window.location.href = '/Home/vHoteles/'; }, 3000);
        }

    }

    this.ObtenerHotel = function () {
        var id = document.getElementById("txtHotel").value;

        if (id != "nulo") {
            this.ctrlActions.GetById("hotel/" + id, this.CargarHotel);
        }

    }

    this.ValidarInputs = function () {
        if ($("#frmEdition").valid()) {
            this.Update();
        } else {

        }
    }

    this.uploadButtonFoto = function () {
        // Initiate upload
        cloudinary.openUploadWidget({ cloud_name: 'datatek', upload_preset: 'datatek-group', tags: ['cgal'] },
            function (error, result) {
                if (error) console.log(error);
                // If NO error, log image data to console
                let id = result[0].public_id;
                imagenUrl = processImage(id);
                document.querySelector("#txtFoto").value = cloudinaryURL + id;;
                var image = document.getElementById("mngFoto");
                image.src = cloudinaryURL + id;
            });
    };
    this.uploadButtonLogo = function () {
        // Initiate upload
        cloudinary.openUploadWidget({ cloud_name: 'datatek', upload_preset: 'datatek-group', tags: ['cgal'] },
            function (error, result) {
                if (error) console.log(error);
                // If NO error, log image data to console
                let id = result[0].public_id;
                imagenUrl = processImage(id);
                document.querySelector("#txtLogo").value = cloudinaryURL + id;;
                var image = document.getElementById("mngLogo");
                image.src = cloudinaryURL + id;
            });
    };

    function processImage(id) {
        let options = {
            client_hints: true,
        };
        return $.cloudinary.url(id, options);
    };
    this.volverPerfil = function () {
        var id = document.getElementById("txtHotel").value;
        window.location.href = '/Home/vPerfilHotel/' + id;
    };

    this.VerServicios = function () {
        sessionStorage.setItem('idHotelLogeado', document.getElementById("txtCedulaJ").value);
        sessionStorage.setItem('nombreHotelLogeado', document.getElementById("txtNombre").value);
        window.location = "/Home/vServicios";
    };
}

//ON DOCUMENT READY

$(document).ready(function () {
    var vmodificarhotel = new vModificarHotel();
    var element = document.getElementById("Hotel");
    element.classList.remove("d-none");
    var elementC = document.getElementById("HotelC");
    elementC.classList.remove("d-none");

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

    ReglasValidacion();

});

ReglasValidacion = function () {
    $("#frmEdition").submit(function (e) {
        e.preventDefault();
    }).validate({
        lang: 'es',
        errorClass: "is-invalid",
        rules: {
            txtCedulaJ: { required: true },
            txtClasificacion: { required: true, digits: true, min: 1, max: 5  },
            txtNombre: { required: true },
            txtCadena: { required: false },
            txtDescripcion: { required: true },
            txtProvincia: { required: true },
            txtCanton: { required: true },
            txtDistrito: { required: true },
            txtDireccion: { required: true },
            txtCorreo: { required: true, email: true },
            txtSitio: { required: true, url: true }
        },
        errorPlacement: function (error, element) {
            element: "div";
            $(error).addClass('input-group mb-3');
            error.css({ 'padding-left': '10px', 'margin-right': '20px', 'padding-bottom': '2px', 'color': 'red' });
            error.insertAfter(element);
        }
        });

}