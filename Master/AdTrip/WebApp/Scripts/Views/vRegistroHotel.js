function vRegistroHotel() {
    this.service = 'hotel';
    this.ctrlActions = new ControlActions();
    this.SolicitudHotel;
    $.cloudinary.config({ cloud_name: 'datatek', api_key: '678592257184526' }); // Upload button
    let cloudinaryURL = 'https://res.cloudinary.com/datatek/image/upload/';
    this.membresia = 0;


    this.Create = function () {
        var hotelData = {};
        hotelData = this.ctrlActions.GetDataForm('frmEdition');
        //Hace el post al create

        this.ctrlActions.PostToAPI(this.service, hotelData, function () {
            swal("Registro completado", "Hotel creado", "success");
            setTimeout(function redirection() { window.location.href = '/Home/Index/'; }, 2000);
        });
    }

    this.CargarSolicitud = function (data) {
        if (data != null) {

            var dataEstado = data['Estado'];
            if (dataEstado =="Aprobada") {

                document.getElementById("txtCedulaJ").value = data['CedulaJuridica'];
                document.getElementById("txtCedulaJ").setAttribute('disabled', '');

                document.getElementById("txtIdGerente").value = data['IdUsuario'];
                document.getElementById("txtIdGerente").setAttribute('disabled', '');

                document.getElementById("txtClasificacion").value = data['Clasificacion'];
                document.getElementById("txtClasificacion").setAttribute('disabled', '');

                document.getElementById("txtNombre").value = data['Nombre'];
                document.getElementById("txtNombre").setAttribute('disabled', '');


                document.getElementById("txtCadena").value = data['Cadena'];
                document.getElementById("txtCadena").setAttribute('disabled', '');

                document.getElementById("txtDescripcion").value = data['Descripcion'];
                document.getElementById("txtDescripcion").setAttribute('disabled', '');

                document.getElementById("txtDireccion").value = data['Direccion'];

                document.getElementById("txtMembrecia").value = data['Membrecia'];
                this.membresia = data['Membrecia'];
                Pago(data['Membrecia']);
                document.getElementById("txtMembrecia").setAttribute('disabled', '');

                var imageLogo = document.getElementById("mngFoto");
                document.getElementById("txtFoto").value = imageLogo.src;

                var imageFoto = document.getElementById("mngLogo");
                document.getElementById("txtLogo").value = imageFoto.src;

            } else {
                swal("No se puede continuar", "La solicitud no se ha aprobado aun", "error");
                setTimeout(function redirection() { window.location.href = '/Home/vRegistroSolicitudHotel/'; }, 3000);
            }

            
        } else {
            swal("Error de datos", "La solicitud no existe", "error");
           
            setTimeout(function redirection() { window.location.href = '/Home/vRegistroSolicitudHotel/'; }, 3000);
        }

        function Pago (dt) {
            paypal.Button.render({
                // Configure environment
                env: 'sandbox',
                client: {
                    sandbox: 'AXgCkOz_JWQZPDVMFwqQZuroSlMI1LMHLdS78A2CmD1VgqB2ikwjAanRpzKwpUzoFHS_jspaN-LKBTL7',
                    production: 'demo_production_client_id'
                },
                // Customize button (optional)
                locale: 'es_CR',
                style: {
                    size: 'medium',
                    color: 'blue',
                    shape: 'pill',
                    height: 40
                },

                // Enable Pay Now checkout flow (optional)
                commit: true,

                // Set up a payment
                payment: function (data, actions) {
                    return actions.payment.create({
                        transactions: [{
                            amount: {
                                total: dt,
                                currency: 'USD'
                            }
                        }]
                    });
                },
                // Execute the payment
                onAuthorize: function (data, actions) {
                    return actions.payment.execute().then(function () {
                        // Show a confirmation message to the buyer
                        swal("Gracias", "El pago se ha realizado con éxito", "success");
                        $('#btnCreate').show();
                                         

                    });
                }
            }, '#paypal-button');
        };
    }

    this.ObtenerSolicitud = function(){
        var id = document.getElementById("txtSolicitud").value;

        if (id != "nulo") {
            this.ctrlActions.GetById("solicitudHotel/" + id, this.CargarSolicitud);
        } else {
            window.location.href = "/Home/vRegistroSolicitudHotel/";
        }

    }

    this.ValidarInputs = function () {
        if ($("#frmEdition").valid()) {
            this.Create();
            
            
            setTimeout(function redirection() { window.location.href = '/Home/vLogin'; }, 5000);
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
    }
}

//ON DOCUMENT READY

$(document).ready(function () {
    var vregistrohotel = new vRegistroHotel();

    $('#btnCreate').hide();

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



function limpiarFormulario() {
    $("#frmEdition")[0].reset();
}

ReglasValidacion = function () {
    $("#frmEdition").submit(function (e) {
        e.preventDefault();
    }).validate({
        lang: 'es',
        errorClass: "is-invalid",
        rules: {
            txtCedulaJ: { required: true },
            txtClasificacion: { required: true, digits: true },
            txtNombre: { required: true },
            txtCadena: { required: false },
            txtDescripcion: { required: true },
            txtProvincia: { required: true },
            txtCanton: { required: true },
            txtDistrito: { required: true },
            txtDireccion: { required: true },
            txtCorreo: { required: true, email: true },
            txtTelefonos: { required: true},
            txtSitio: { required: true, url: true },
            txtMembrecia: { required: true, digits: true },
            txtIdGerente: { required: true }
        },
        errorPlacement: function (error, element) {
            element: "div";
            $(error).addClass('input-group mb-3');
            error.css({ 'padding-left': '10px', 'margin-right': '20px', 'padding-bottom': '2px', 'color': 'red' });
            error.insertAfter(element);
        }
    });
}