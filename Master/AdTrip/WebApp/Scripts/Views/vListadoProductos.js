function vListadoProductos() {


	//this.service = 'productos';
	//this.serviceCarrito = 'carrito';
	//this.serviceImagen = 'mediaProductos';
	this.ctrlActions = new ControlActions();
	var IdHotel = sessionStorage.getItem("idHotelLogeado");
	var serviceByIdHotel = 'producto/hotel/' + IdHotel;


	this.ObtainData = function () {

		//let url = this.ctrlActions.GetUrlApiService(this.serviceByIdHotel);
		//let data = this.ctrlActions.GetIDToAPI(url);
		//this.createCards(data);

		this.ctrlActions.GetById(serviceByIdHotel, this.CreateCards);
	};

	this.CreateCards = function (data) {
		let container = document.querySelector('.form-row');

		for (var i = 0; i < data.length; i++) {
			//let url = this.ctrlActions.GetUrlApiService("mediaProductos" + "/" + "producto" + "/" + data[i].idProducto);
			//let mediaProductos = this.ctrlActions.GetIDToAPI(url);
			let divCard = document.createElement('div');
			divCard.classList.add('col-4');
			divCard.classList.add('col-sm-6');
			divCard.classList.add('col-md-4');
			divCard.classList.add('col-lg-3');
			divCard.classList.add('card');
			let divContent = document.createElement('div');
			divContent.classList.add('our-team');

			let contenidoImg = document.createElement('div');
			contenidoImg.classList.add('picture');
			let contenido = data[i]['Foto'];
			let imagen = document.createElement('img');
			imagen.src = contenido;
			imagen.classList.add('img-fluid');
			imagen.style.maxHeight = "180px";
			imagen.style.minHeight = "180px";



			contenidoImg.appendChild(imagen);
			divContent.appendChild(contenidoImg);


			let divinfo = document.createElement('div');
			divinfo.classList.add('team-content');
			let element = document.createElement('h4');
			let textType = document.createTextNode(data[i]['Nombre']);

			element.appendChild(textType);
			divinfo.appendChild(element);

			let element1 = document.createElement('h5');
			let textType1 = document.createTextNode('Precio: $' + parseFloat(data[i]['Precio']).toFixed(2));


			element1.appendChild(textType1);
			divinfo.appendChild(element1);


			let btnAddToCart = document.createElement('button');
			let textadd = document.createTextNode('Comprar producto');
			btnAddToCart.classList.add('btnAddProducto');
			btnAddToCart.classList.add('btn');
			btnAddToCart.dataset.Id = data[i]['Codigo'];
			var id = data[i]['Codigo'];
			//btnAddToCart.onclick = function (id) {
			//CreateCarrito(id);
			//}

			btnAddToCart.appendChild(textadd);
			divinfo.appendChild(btnAddToCart);

			divContent.appendChild(divinfo);
			divCard.appendChild(divContent);
			container.appendChild(divCard);

			

			btnAddToCart.addEventListener('click', function (event) {
				sessionStorage.removeItem("idProductoSeleccionado");
				sessionStorage.setItem("idProductoSeleccionado", this.dataset.Id);
				CreateCarrito()
				
				event.preventDefault();
			});

		}
		$(".btnAddProducto").attr("data-toggle", "modal");
		$(".btnAddProducto").attr("data-target", "#edit-profile");

	};


	
};




$(document).ready(function () {

	var vlistadoProductos = new vListadoProductos();
	vlistadoProductos.ObtainData();
});



function CreateCarrito() {
	let scanner = new Instascan.Scanner(
		{
			video: document.getElementById('preview')
		}
	);

	scanner.addListener('scan', function (content) {
		window.open(content, "_blank");
		sessionStorage.removeItem("llaveQRCarrito");
		sessionStorage.setItem("llaveQRCarrito", content);
	
		var serviceCarrito = 'carrito';
		var id = content;
		var CorreoLogueado = sessionStorage.getItem("correoUsuario");
		var control = new ControlActions();
		let url = control.GetUrlApiService("llaveQR/" + id);
		let infoLlaveQR = control.GetIDToAPI(url);

		var carritoData = {};
		var IdProducto = sessionStorage.getItem("idProductoSeleccionado");
		var IdReserva = infoLlaveQR['IdReserva'];

		carritoData['Cantidad'] = 1;
		carritoData['IdProducto'] = IdProducto;
		carritoData['IdReserva'] = IdReserva;
		carritoData['IdLlaveQR'] = content;
		carritoData['Correo'] = CorreoLogueado;

		//Hace el post al create
	   // ObtainID(IdProducto);
		control.PostToAPI(serviceCarrito, carritoData);
	});

	Instascan.Camera.getCameras().then(cameras => {
		if (cameras.length > 0) {
			scanner.start(cameras[0]);
		} else {
			console.error("No existen camaras en el dispositivo!");
		}
	});	
	
		
	

}
