function vEscaner() {

    this.serviceQR = 'llaveQR/checkIn';
    this.serviceReserva = 'reserva';
    this.ctrlActions = new ControlActions();

    this.RetrieveQR = function (llaveId) {
        var data = {};
        data['idQR'] = llaveId;
        data['idHabitacion'] = $('#idHabitacion').val();
        data['idHotel'] = $('#idHotel').val();

        this.ctrlActions.PostToAPI(this.serviceQR, data, function (content) {

        });
    }

}

$(document).ready(function () {
    var vescaner = new vEscaner();

    let scanner = new Instascan.Scanner(
        {
            video: document.getElementById('preview')
        }
    );
    scanner.addListener('scan', function (content) {
        vescaner.RetrieveQR(content);
        //window.open(content, "_blank");
    });
    Instascan.Camera.getCameras().then(cameras => {
        if (cameras.length > 0) {
            scanner.start(cameras[0]);
        } else {
            console.error("No existen cámaras en el dispositivo!");
        }
    });
});