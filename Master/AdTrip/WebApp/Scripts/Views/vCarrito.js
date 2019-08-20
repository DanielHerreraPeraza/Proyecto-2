function vCarrito() {

	/* Set values */
	var promoCode;
	var promoPrice;
	var fadeTime = 300;

	this.service = 'carrito';
	this.ctrlActions = new ControlActions();

	var usuarioLogueadoDist = {};
	usuarioLogueadoDist = JSON.parse(window.sessionStorage.getItem("usuarioLogueado"));

	var IdLLaveQR = sessionStorage.getItem("llaveQRCarrito");
	var CorreoLogueado = sessionStorage.getItem("correoUsuario");
    var serviceByIdReserva = 'carrito/llaveQR';
	var infoCarritoLogeuado = {};
    infoCarritoLogeuado['IdLLaveQR'] = IdLLaveQR;
    infoCarritoLogeuado['Correo'] = CorreoLogueado;

    this.ObtainData = function () {
        this.ctrlActions.GetCarrito(serviceByIdReserva, infoCarritoLogeuado, function (data) {
			var vcarrito2 = new vCarrito();
			vcarrito2.showCartItems(data['Data']);
		});
		//let infoCarrito = this.ctrlActions.GetIDToAPI(url);
		//this.showCartItems(infoCarrito);

	};

    this.showCartItems = function (infoCarrito) {
		let container = document.querySelector('.basket');
		var sum = 0;
		var cantProductos = 0;
		for (var i = 0; i < infoCarrito.length; i++) {

			//let url = this.ctrlActions.GetUrlApiService("mediaProductos" + "/" + "producto" + "/" + infoCarrito[i].IdProducto);
		//let mediaProductos = this.ctrlActions.GetIDToAPI(url);

			var idProducto = infoCarrito[i].IdProducto;
			let urlProducto = this.ctrlActions.GetUrlApiService("producto/idProducto/" + idProducto);
			let infoProducto = this.ctrlActions.GetIDToAPI(urlProducto);


			let contenidoImg = document.createElement('div');
			let contenido = infoProducto['Foto'];
			let imagen = document.createElement('img');
			imagen.src = contenido;
			imagen.classList.add('product-image');
			imagen.classList.add('img-fluid');
			imagen.style.maxHeight = "100px";
			imagen.style.minHeight = "100px";
			imagen.style.maxWidth = "170px";

			let divinfo = document.createElement('div');
			divinfo.classList.add('basket-product');

			contenidoImg.classList.add('product-image');
			contenidoImg.appendChild(imagen);

			divinfo.appendChild(contenidoImg);




			let elementName = document.createElement('div');
			elementName.classList.add('product-details');
			//elementName.classList.add('item-quantity');
			let textTypeName = document.createTextNode(infoProducto['Nombre']);

			elementName.appendChild(textTypeName);
			divinfo.appendChild(elementName);


			let elementPrecio = document.createElement('div');
			elementPrecio.classList.add('price');
			let textTypePrecio = document.createTextNode(parseFloat(infoProducto['Precio']).toFixed(2));

			elementPrecio.appendChild(textTypePrecio);
			divinfo.appendChild(elementPrecio);


			let elementCantidad = document.createElement('div');
			elementCantidad.classList.add('quantity');
			let inputCantidad = document.createElement('input');
			inputCantidad.classList.add('quantity-field');
			inputCantidad.type = 'number';
			inputCantidad.value = (infoCarrito[i]['Cantidad']);



			//elementCantidad.classList.add('quantity-field');
			//let textTypeCantidad = document.createElement(infoCarrito[i]['Cantidad']);

			//	inputCantidad.appendChild(textTypeCantidad);

			elementCantidad.appendChild(inputCantidad);

			divinfo.appendChild(elementCantidad);


			let elementSubtotal = document.createElement('div');
			elementSubtotal.classList.add('subtotal');
			let impuesto = infoProducto['IdTipoImpuesto'] / 100;
			let textTypeSubtotal = document.createTextNode(parseFloat(infoProducto['Precio'] * impuesto + infoProducto['Precio'] * infoCarrito[i]['Cantidad']));

			elementSubtotal.appendChild(textTypeSubtotal);
			divinfo.appendChild(elementSubtotal);


			container.appendChild(divinfo);

			var subtotal = (infoProducto['Precio'] * impuesto + infoProducto['Precio'] * infoCarrito[i]['Cantidad']);

			sum = sum + subtotal;
			cantProductos = cantProductos + 1;

			$('#basket-subtotal').html(sum.toFixed(2));
			$('#basket-total').html(sum.toFixed(2));
			$('.total-items').html(cantProductos);

		}

		this.updateQuantity;

	};

	

	$('.quantity .quantity-field').change(function () {
		this.updateQuantity();
	});


	/* Update quantity */
	this.updateQuantity = function (quantityInput) {
		/* Calculate line price */
		var productRow = $(quantityInput).parent().parent();
		var price = parseFloat(productRow.children('.price').text());
		var quantity = $(quantityInput).val();
		var linePrice = price * quantity;

		/* Update line price display and recalc cart totals */
		productRow.children('.subtotal').each(function () {
			$(this).fadeOut(fadeTime, function () {
				$(this).text(linePrice.toFixed(2));
				recalculateCart();
				$(this).fadeIn(fadeTime);
			});
		});

		productRow.find('.item-quantity').text(quantity);
		updateSumItems();
	}

    this.LoadCheckout = function () {
        var vcarrito = new vCarrito();
        var total = parseFloat($('#basket-total').text());
        vcarrito.Checkout(total);
    }
	//$('#btnCheckout').click(function () {
	//	window.location.href = "vClientCheckout";
	//});
    this.Checkout = function (total) {
        Pago(total);

        function Pago(total) {
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
                                total: total,
                                currency: 'USD'
                            }
                        }]
                    });
                },
                // Execute the payment
                onAuthorize: function (data, actions) {
                    return actions.payment.execute().then(function () {

                        var data = {};
                        data['idQR'] = sessionStorage.getItem("llaveQRCarrito");

                        var carrito = new vCarrito();
                        carrito.ctrlActions.GetLlaveQR('llaveQR/checkOut', data, function (content) {
                            swal("Gracias por su visita!", content, "success");

                            setTimeout(function () {
                                window.location.href = "/Home/vPerfilUsuario";
                            }, 1000);
                        });

                    });
                }
            }, '#paypal-button');
        };

    }
}
function ReloadData() {
	var vcarrito = new vCarrito();
	vcarrito.ObtainData();
}


$(document).ready(function () {

	var vcarrito = new vCarrito();
	vcarrito.ObtainData();
});


function updateCarrito(idCarritoUpdate) {
	var control = new ControlActions();
	var IdCarrito = idCarritoUpdate;
	//	var inputCantidad = this.dataset.cantidad;
	cantidad = document.querySelector('.quantity-field').value;
	var carritoData = {};
	carritoData['IdCarrito'] = IdCarrito;
	carritoData['Cantidad'] = cantidad;
	if (cantidad == 0 || cantidad < 0) {
		swal("Cantidad no permitida", "La cantidad de un producto no puede ser menor o igual a 0", "error");
	} else if (cantidad > 10) {
		swal("Error", "La cantidad de un producto no puede ser mayor a 10", "error");
	} else {
		control.PutToAPI('carrito', carritoData, function () { });

	}
}